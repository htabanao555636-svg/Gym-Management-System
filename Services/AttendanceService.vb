Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Services
    ''' <summary>
    ''' Manages gym check-ins, check-outs, and active occupancy.
    ''' </summary>
    Public Class AttendanceService
        Private ReadOnly _membershipService As New MembershipService()
        Private ReadOnly _memberService As New MemberService()

        ''' <summary>
        ''' Verifies member eligibility and checks the member in.
        ''' Throws InvalidOperationException if member has no active membership or is already checked in.
        ''' </summary>
        Public Function CheckIn(memberId As Integer) As Attendance
            Dim member As Member = _memberService.GetMemberById(memberId)
            If member Is Nothing Then
                Throw New ArgumentException("Member not found.")
            End If
            If Not String.Equals(member.Status, "Active", StringComparison.OrdinalIgnoreCase) Then
                Throw New InvalidOperationException($"Cannot check in: Member account is currently {member.Status}.")
            End If

            ' Verify active membership
            Dim activeMembership As Membership = _membershipService.GetActiveMembershipForMember(memberId)
            If activeMembership Is Nothing Then
                Dim pendingMs As Membership = _membershipService.GetPendingMembershipForMember(memberId)
                If pendingMs IsNot Nothing Then
                    Throw New InvalidOperationException($"Cannot check in: Member subscription '{pendingMs.PlanName}' is Pending Payment. Please settle payment at the cashier first to activate gym access.")
                End If
                Throw New InvalidOperationException("Cannot check in: Member does NOT have an active membership. Please enroll or renew their plan.")
            End If

            ' Verify not already checked in
            If IsMemberCurrentlyInside(memberId) Then
                Throw New InvalidOperationException($"Member {member.FullName} is already checked in and currently inside the gym.")
            End If

            Dim recordedBy As Integer = If(SessionManager.CurrentUser IsNot Nothing, SessionManager.CurrentUser.UserID, 1)

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    INSERT INTO attendance (member_id, check_in, check_out, recorded_by)
                    VALUES (@memberId, @checkIn, NULL, @recordedBy);
                    SELECT LAST_INSERT_ID();"

                Using cmd As New MySqlCommand(sql, conn)
                    Dim nowTime As DateTime = DateTime.Now
                    cmd.Parameters.AddWithValue("@memberId", memberId)
                    cmd.Parameters.AddWithValue("@checkIn", nowTime)
                    cmd.Parameters.AddWithValue("@recordedBy", recordedBy)

                    Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    ActivityLogService.Log("Attendance Check-In", $"Checked in {member.FullName} ({member.MembershipNumber}) at {nowTime:hh:mm:ss tt}.")

                    Return New Attendance() With {
                        .AttendanceID = newId,
                        .MemberID = memberId,
                        .CheckIn = nowTime,
                        .RecordedBy = recordedBy,
                        .MemberName = member.FullName,
                        .MembershipNumber = member.MembershipNumber
                    }
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Gets the active currently-inside attendance session for a member.
        ''' </summary>
        Public Function GetCurrentSession(memberId As Integer) As Attendance
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT a.attendance_id, a.member_id, a.check_in, a.check_out, a.recorded_by,
                           m.first_name, m.middle_name, m.last_name, m.membership_number,
                           u.username as recorded_by_username
                    FROM attendance a
                    INNER JOIN members m ON a.member_id = m.member_id
                    INNER JOIN users u ON a.recorded_by = u.user_id
                    WHERE a.member_id = @memberId AND a.check_out IS NULL
                    ORDER BY a.check_in DESC LIMIT 1;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@memberId", memberId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapReaderToAttendance(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Checks out a currently checked-in member session, recording duration and activity log.
        ''' </summary>
        Public Function CheckOut(attendanceId As Integer) As Attendance
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim querySql As String = "
                    SELECT a.attendance_id, a.member_id, a.check_in, a.check_out, a.recorded_by,
                           m.first_name, m.middle_name, m.last_name, m.membership_number,
                           u.username as recorded_by_username
                    FROM attendance a
                    INNER JOIN members m ON a.member_id = m.member_id
                    INNER JOIN users u ON a.recorded_by = u.user_id
                    WHERE a.attendance_id = @id AND a.check_out IS NULL;"
                Dim att As Attendance = Nothing
                Using qCmd As New MySqlCommand(querySql, conn)
                    qCmd.Parameters.AddWithValue("@id", attendanceId)
                    Using reader = qCmd.ExecuteReader()
                        If reader.Read() Then
                            att = MapReaderToAttendance(reader)
                        End If
                    End Using
                End Using

                If att Is Nothing Then
                    Throw New InvalidOperationException("Active attendance session not found or already checked out.")
                End If

                Dim nowTime As DateTime = DateTime.Now
                Dim updateSql As String = "
                    UPDATE attendance 
                    SET check_out = @now 
                    WHERE attendance_id = @id AND check_out IS NULL;"
                Using uCmd As New MySqlCommand(updateSql, conn)
                    uCmd.Parameters.AddWithValue("@now", nowTime)
                    uCmd.Parameters.AddWithValue("@id", attendanceId)
                    uCmd.ExecuteNonQuery()
                End Using

                att.CheckOut = nowTime
                ActivityLogService.Log("Attendance Check-Out", $"Checked out {att.MemberName} ({att.MembershipNumber}) at {nowTime:hh:mm:ss tt}. Total time inside: {att.DurationText}.")
                Return att
            End Using
        End Function

        ''' <summary>
        ''' Checks out a member by their member ID if they are currently inside.
        ''' </summary>
        Public Function CheckOutByMemberId(memberId As Integer) As Attendance
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim querySql As String = "
                    SELECT a.attendance_id, a.member_id, a.check_in, a.check_out, a.recorded_by,
                           m.first_name, m.middle_name, m.last_name, m.membership_number,
                           u.username as recorded_by_username
                    FROM attendance a
                    INNER JOIN members m ON a.member_id = m.member_id
                    INNER JOIN users u ON a.recorded_by = u.user_id
                    WHERE a.member_id = @memberId AND a.check_out IS NULL
                    ORDER BY a.check_in DESC LIMIT 1;"
                Dim att As Attendance = Nothing
                Using qCmd As New MySqlCommand(querySql, conn)
                    qCmd.Parameters.AddWithValue("@memberId", memberId)
                    Using reader = qCmd.ExecuteReader()
                        If reader.Read() Then
                            att = MapReaderToAttendance(reader)
                        End If
                    End Using
                End Using

                If att Is Nothing Then
                    Throw New InvalidOperationException("This member does not have an active check-in session.")
                End If

                Dim nowTime As DateTime = DateTime.Now
                Dim updateSql As String = "
                    UPDATE attendance 
                    SET check_out = @now 
                    WHERE attendance_id = @id AND check_out IS NULL;"
                Using uCmd As New MySqlCommand(updateSql, conn)
                    uCmd.Parameters.AddWithValue("@now", nowTime)
                    uCmd.Parameters.AddWithValue("@id", att.AttendanceID)
                    uCmd.ExecuteNonQuery()
                End Using

                att.CheckOut = nowTime
                ActivityLogService.Log("Attendance Check-Out", $"Checked out {att.MemberName} ({att.MembershipNumber}) at {nowTime:hh:mm:ss tt}. Total time inside: {att.DurationText}.")
                Return att
            End Using
        End Function

        ''' <summary>
        ''' Returns true if the member is currently checked in without a checkout timestamp.
        ''' </summary>
        Public Function IsMemberCurrentlyInside(memberId As Integer) As Boolean
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "SELECT COUNT(*) FROM attendance WHERE member_id = @memberId AND check_out IS NULL;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@memberId", memberId)
                    Return Convert.ToInt64(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Gets all members currently inside the gym.
        ''' </summary>
        Public Function GetCurrentlyInsideMembers() As List(Of Attendance)
            Dim list As New List(Of Attendance)()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT a.attendance_id, a.member_id, a.check_in, a.check_out, a.recorded_by,
                           m.first_name, m.middle_name, m.last_name, m.membership_number,
                           u.username as recorded_by_username
                    FROM attendance a
                    INNER JOIN members m ON a.member_id = m.member_id
                    INNER JOIN users u ON a.recorded_by = u.user_id
                    WHERE a.check_out IS NULL
                    ORDER BY a.check_in DESC;"

                Using cmd As New MySqlCommand(sql, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapReaderToAttendance(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Gets today's attendance logs or filtered history.
        ''' </summary>
        Public Function GetAttendanceHistory(Optional targetDate As Date? = Nothing, Optional search As String = Nothing) As List(Of Attendance)
            Dim list As New List(Of Attendance)()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT a.attendance_id, a.member_id, a.check_in, a.check_out, a.recorded_by,
                           m.first_name, m.middle_name, m.last_name, m.membership_number,
                           u.username as recorded_by_username
                    FROM attendance a
                    INNER JOIN members m ON a.member_id = m.member_id
                    INNER JOIN users u ON a.recorded_by = u.user_id
                    WHERE 1=1 "

                If targetDate.HasValue Then
                    sql &= " AND DATE(a.check_in) = @targetDate"
                End If

                If Not String.IsNullOrWhiteSpace(search) Then
                    sql &= " AND (m.membership_number LIKE @search OR m.first_name LIKE @search OR m.last_name LIKE @search OR CONCAT(m.first_name, ' ', m.last_name) LIKE @search)"
                End If

                sql &= " ORDER BY a.check_in DESC LIMIT 500;"

                Using cmd As New MySqlCommand(sql, conn)
                    If targetDate.HasValue Then cmd.Parameters.AddWithValue("@targetDate", targetDate.Value.Date)
                    If Not String.IsNullOrWhiteSpace(search) Then cmd.Parameters.AddWithValue("@search", $"%{search.Trim()}%")

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapReaderToAttendance(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Private Function MapReaderToAttendance(reader As MySqlDataReader) As Attendance
            Dim fn As String = reader("first_name").ToString()
            Dim mn As String = If(reader.IsDBNull(reader.GetOrdinal("middle_name")), "", reader("middle_name").ToString())
            Dim ln As String = reader("last_name").ToString()
            Dim mName As String = If(String.IsNullOrWhiteSpace(mn), $"{fn} {ln}", $"{fn} {mn.Substring(0, 1)}. {ln}")

            Dim checkOutVal As DateTime? = Nothing
            Dim coOrdinal As Integer = reader.GetOrdinal("check_out")
            If Not reader.IsDBNull(coOrdinal) Then
                checkOutVal = Convert.ToDateTime(reader.GetValue(coOrdinal))
            End If

            Return New Attendance() With {
                .AttendanceID = Convert.ToInt32(reader("attendance_id")),
                .MemberID = Convert.ToInt32(reader("member_id")),
                .CheckIn = Convert.ToDateTime(reader("check_in")),
                .CheckOut = checkOutVal,
                .RecordedBy = Convert.ToInt32(reader("recorded_by")),
                .MemberName = mName,
                .MembershipNumber = reader("membership_number").ToString(),
                .RecordedByName = reader("recorded_by_username").ToString()
            }
        End Function
    End Class
End Namespace
