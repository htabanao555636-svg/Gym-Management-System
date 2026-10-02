Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Services
    ''' <summary>
    ''' Business logic for member subscriptions and memberships.
    ''' </summary>
    Public Class MembershipService
        Private ReadOnly _planService As New MembershipPlanService()

        ''' <summary>
        ''' Refreshes expired statuses in the database based on the current date.
        ''' </summary>
        Public Sub SyncExpiredMemberships()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                ' Expire memberships past end_date
                Dim sqlExpire As String = "
                    UPDATE memberships 
                    SET status = 'Expired' 
                    WHERE end_date < CURDATE() AND status IN ('Active', 'Expiring Soon');"
                Using cmd As New MySqlCommand(sqlExpire, conn)
                    cmd.ExecuteNonQuery()
                End Using

                ' Mark Expiring Soon (within 7 days)
                Dim sqlSoon As String = "
                    UPDATE memberships 
                    SET status = 'Expiring Soon' 
                    WHERE end_date >= CURDATE() AND end_date <= DATE_ADD(CURDATE(), INTERVAL 7 DAY) AND status = 'Active';"
                Using cmd As New MySqlCommand(sqlSoon, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Gets all memberships with member and plan joins.
        ''' </summary>
        Public Function GetMemberships(Optional search As String = Nothing, Optional statusFilter As String = "All") As List(Of Membership)
            SyncExpiredMemberships()
            Dim list As New List(Of Membership)()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT ms.membership_id, ms.member_id, ms.plan_id, ms.start_date, ms.end_date,
                           ms.amount, ms.status, ms.created_by, ms.created_at, ms.updated_at,
                           m.first_name, m.middle_name, m.last_name, m.membership_number,
                           p.plan_name, u.username as creator_username
                    FROM memberships ms
                    INNER JOIN members m ON ms.member_id = m.member_id
                    INNER JOIN membership_plans p ON ms.plan_id = p.plan_id
                    INNER JOIN users u ON ms.created_by = u.user_id
                    WHERE 1=1 "

                If Not String.IsNullOrWhiteSpace(statusFilter) AndAlso Not String.Equals(statusFilter, "All", StringComparison.OrdinalIgnoreCase) Then
                    sql &= " AND ms.status = @status"
                End If

                If Not String.IsNullOrWhiteSpace(search) Then
                    sql &= " AND (m.membership_number LIKE @search OR m.first_name LIKE @search OR m.last_name LIKE @search OR p.plan_name LIKE @search)"
                End If

                sql &= " ORDER BY ms.membership_id DESC;"

                Using cmd As New MySqlCommand(sql, conn)
                    If Not String.IsNullOrWhiteSpace(statusFilter) AndAlso Not String.Equals(statusFilter, "All", StringComparison.OrdinalIgnoreCase) Then
                        cmd.Parameters.AddWithValue("@status", statusFilter)
                    End If
                    If Not String.IsNullOrWhiteSpace(search) Then
                        cmd.Parameters.AddWithValue("@search", $"%{search.Trim()}%")
                    End If

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapReaderToMembership(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Gets the current active membership for a specific member, if one exists.
        ''' </summary>
        Public Function GetActiveMembershipForMember(memberId As Integer) As Membership
            SyncExpiredMemberships()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT ms.membership_id, ms.member_id, ms.plan_id, ms.start_date, ms.end_date,
                           ms.amount, ms.status, ms.created_by, ms.created_at, ms.updated_at,
                           m.first_name, m.middle_name, m.last_name, m.membership_number,
                           p.plan_name, u.username as creator_username
                    FROM memberships ms
                    INNER JOIN members m ON ms.member_id = m.member_id
                    INNER JOIN membership_plans p ON ms.plan_id = p.plan_id
                    INNER JOIN users u ON ms.created_by = u.user_id
                    WHERE ms.member_id = @memberId AND ms.status IN ('Active', 'Expiring Soon') AND ms.end_date >= CURDATE()
                    ORDER BY ms.end_date DESC LIMIT 1;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@memberId", memberId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapReaderToMembership(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets the most recent pending payment membership for a member, if any exists.
        ''' </summary>
        Public Function GetPendingMembershipForMember(memberId As Integer) As Membership
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT ms.membership_id, ms.member_id, ms.plan_id, ms.start_date, ms.end_date,
                           ms.amount, ms.status, ms.created_by, ms.created_at, ms.updated_at,
                           m.first_name, m.middle_name, m.last_name, m.membership_number,
                           p.plan_name, u.username as creator_username
                    FROM memberships ms
                    INNER JOIN members m ON ms.member_id = m.member_id
                    INNER JOIN membership_plans p ON ms.plan_id = p.plan_id
                    INNER JOIN users u ON ms.created_by = u.user_id
                    WHERE ms.member_id = @memberId AND ms.status = 'Pending Payment'
                    ORDER BY ms.membership_id DESC LIMIT 1;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@memberId", memberId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapReaderToMembership(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Registers or renews a membership, computing start/end dates and creating the record in 'Pending Payment' status.
        ''' The subscription is only activated once payment is processed in the Payment Module.
        ''' </summary>
        Public Function EnrollMembership(memberId As Integer, planId As Integer, Optional customStartDate As Date? = Nothing) As Membership
            Dim plan As MembershipPlan = _planService.GetPlanById(planId)
            If plan Is Nothing Then
                Throw New ArgumentException("Selected membership plan not found.")
            End If

            ' Determine start date
            Dim existingActive As Membership = GetActiveMembershipForMember(memberId)
            Dim startDate As Date = Date.Today
            If customStartDate.HasValue Then
                startDate = customStartDate.Value.Date
            ElseIf existingActive IsNot Nothing AndAlso existingActive.EndDate >= Date.Today Then
                ' Renewal from current expiration + 1 day
                startDate = existingActive.EndDate.AddDays(1)
            End If

            Dim endDate As Date = _planService.CalculateEndDate(startDate, plan)
            Dim creatorId As Integer = If(SessionManager.CurrentUser IsNot Nothing, SessionManager.CurrentUser.UserID, 1)

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    INSERT INTO memberships (member_id, plan_id, start_date, end_date, amount, status, created_by)
                    VALUES (@memberId, @planId, @start, @end, @amount, 'Pending Payment', @creator);
                    SELECT LAST_INSERT_ID();"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@memberId", memberId)
                    cmd.Parameters.AddWithValue("@planId", planId)
                    cmd.Parameters.AddWithValue("@start", startDate)
                    cmd.Parameters.AddWithValue("@end", endDate)
                    cmd.Parameters.AddWithValue("@amount", plan.Price)
                    cmd.Parameters.AddWithValue("@creator", creatorId)

                    Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    ActivityLogService.Log("Enroll Membership", $"Enrolled member ID {memberId} into plan '{plan.PlanName}' (Valid {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}) - Status: Pending Payment (Awaiting Cashiering).")

                    Return New Membership() With {
                        .MembershipID = newId,
                        .MemberID = memberId,
                        .PlanID = planId,
                        .StartDate = startDate,
                        .EndDate = endDate,
                        .Amount = plan.Price,
                        .Status = "Pending Payment",
                        .CreatedBy = creatorId,
                        .PlanName = plan.PlanName
                    }
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Cancels an active membership.
        ''' </summary>
        Public Sub CancelMembership(membershipId As Integer, reason As String)
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "UPDATE memberships SET status = 'Cancelled' WHERE membership_id = @id;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", membershipId)
                    cmd.ExecuteNonQuery()
                    ActivityLogService.Log("Cancel Membership", $"Cancelled membership ID {membershipId}. Reason: {reason}")
                End Using
            End Using
        End Sub

        Private Function MapReaderToMembership(reader As MySqlDataReader) As Membership
            Dim fn As String = reader("first_name").ToString()
            Dim mn As String = If(reader.IsDBNull(reader.GetOrdinal("middle_name")), "", reader("middle_name").ToString())
            Dim ln As String = reader("last_name").ToString()
            Dim mName As String = If(String.IsNullOrWhiteSpace(mn), $"{fn} {ln}", $"{fn} {mn.Substring(0, 1)}. {ln}")

            Return New Membership() With {
                .MembershipID = Convert.ToInt32(reader("membership_id")),
                .MemberID = Convert.ToInt32(reader("member_id")),
                .PlanID = Convert.ToInt32(reader("plan_id")),
                .StartDate = Convert.ToDateTime(reader("start_date")),
                .EndDate = Convert.ToDateTime(reader("end_date")),
                .Amount = Convert.ToDecimal(reader("amount")),
                .Status = reader("status").ToString(),
                .CreatedBy = Convert.ToInt32(reader("created_by")),
                .CreatedAt = Convert.ToDateTime(reader("created_at")),
                .UpdatedAt = Convert.ToDateTime(reader("updated_at")),
                .MemberName = mName,
                .MembershipNumber = reader("membership_number").ToString(),
                .PlanName = reader("plan_name").ToString(),
                .CreatedByName = reader("creator_username").ToString()
            }
        End Function
    End Class
End Namespace
