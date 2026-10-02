Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Services
    ''' <summary>
    ''' Service layer for Gym Membership Plans.
    ''' </summary>
    Public Class MembershipPlanService
        ''' <summary>
        ''' Gets all membership plans with optional status filtering.
        ''' </summary>
        Public Function GetPlans(Optional activeOnly As Boolean = False) As List(Of MembershipPlan)
            Dim list As New List(Of MembershipPlan)()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "SELECT * FROM membership_plans WHERE 1=1"
                If activeOnly Then
                    sql &= " AND status = 'Active'"
                End If
                sql &= " ORDER BY price ASC;"

                Using cmd As New MySqlCommand(sql, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapReaderToPlan(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Gets a plan by ID.
        ''' </summary>
        Public Function GetPlanById(planId As Integer) As MembershipPlan
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "SELECT * FROM membership_plans WHERE plan_id = @id;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", planId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapReaderToPlan(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Adds a new plan (Admin only).
        ''' </summary>
        Public Function AddPlan(plan As MembershipPlan) As Integer
            PermissionHelper.RequirePermission(PermissionHelper.AppFeature.MembershipPlansManage)
            ValidatePlan(plan)

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    INSERT INTO membership_plans (plan_name, description, duration, duration_unit, price, status)
                    VALUES (@name, @desc, @dur, @unit, @price, @status);
                    SELECT LAST_INSERT_ID();"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@name", plan.PlanName.Trim())
                    cmd.Parameters.AddWithValue("@desc", If(String.IsNullOrWhiteSpace(plan.Description), DBNull.Value, plan.Description.Trim()))
                    cmd.Parameters.AddWithValue("@dur", plan.Duration)
                    cmd.Parameters.AddWithValue("@unit", plan.DurationUnit)
                    cmd.Parameters.AddWithValue("@price", plan.Price)
                    cmd.Parameters.AddWithValue("@status", plan.Status)

                    Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    plan.PlanID = newId
                    ActivityLogService.Log("Add Membership Plan", $"Created new plan '{plan.PlanName}' priced at {plan.Price:C}.")
                    Return newId
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Updates an existing plan (Admin only).
        ''' </summary>
        Public Sub UpdatePlan(plan As MembershipPlan)
            PermissionHelper.RequirePermission(PermissionHelper.AppFeature.MembershipPlansManage)
            ValidatePlan(plan)

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    UPDATE membership_plans SET
                        plan_name = @name,
                        description = @desc,
                        duration = @dur,
                        duration_unit = @unit,
                        price = @price,
                        status = @status
                    WHERE plan_id = @id;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", plan.PlanID)
                    cmd.Parameters.AddWithValue("@name", plan.PlanName.Trim())
                    cmd.Parameters.AddWithValue("@desc", If(String.IsNullOrWhiteSpace(plan.Description), DBNull.Value, plan.Description.Trim()))
                    cmd.Parameters.AddWithValue("@dur", plan.Duration)
                    cmd.Parameters.AddWithValue("@unit", plan.DurationUnit)
                    cmd.Parameters.AddWithValue("@price", plan.Price)
                    cmd.Parameters.AddWithValue("@status", plan.Status)
                    cmd.ExecuteNonQuery()

                    ActivityLogService.Log("Edit Membership Plan", $"Updated plan ID {plan.PlanID} ('{plan.PlanName}').")
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Toggles plan active status (Admin only).
        ''' </summary>
        Public Sub SetPlanStatus(planId As Integer, status As String)
            PermissionHelper.RequirePermission(PermissionHelper.AppFeature.MembershipPlansManage)
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "UPDATE membership_plans SET status = @status WHERE plan_id = @id;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@status", status)
                    cmd.Parameters.AddWithValue("@id", planId)
                    cmd.ExecuteNonQuery()

                    ActivityLogService.Log("Plan Status Change", $"Changed plan ID {planId} status to '{status}'.")
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Calculates the end date given a start date and membership plan.
        ''' </summary>
        Public Function CalculateEndDate(startDate As Date, plan As MembershipPlan) As Date
            If plan Is Nothing Then Return startDate
            Select Case plan.DurationUnit.Trim().ToLowerInvariant()
                Case "days", "day"
                    Return startDate.AddDays(plan.Duration)
                Case "years", "year"
                    Return startDate.AddYears(plan.Duration)
                Case Else ' Months
                    Return startDate.AddMonths(plan.Duration)
            End Select
        End Function

        Private Sub ValidatePlan(plan As MembershipPlan)
            If plan Is Nothing Then Throw New ArgumentNullException(NameOf(plan))
            If String.IsNullOrWhiteSpace(plan.PlanName) Then Throw New ArgumentException("Plan Name is required.")
            If plan.Duration <= 0 Then Throw New ArgumentException("Duration must be greater than zero.")
            If plan.Price < 0D Then Throw New ArgumentException("Price cannot be negative.")
        End Sub

        Private Function MapReaderToPlan(reader As MySqlDataReader) As MembershipPlan
            Return New MembershipPlan() With {
                .PlanID = Convert.ToInt32(reader("plan_id")),
                .PlanName = reader("plan_name").ToString(),
                .Description = If(reader.IsDBNull(reader.GetOrdinal("description")), "", reader("description").ToString()),
                .Duration = Convert.ToInt32(reader("duration")),
                .DurationUnit = reader("duration_unit").ToString(),
                .Price = Convert.ToDecimal(reader("price")),
                .Status = reader("status").ToString(),
                .CreatedAt = Convert.ToDateTime(reader("created_at")),
                .UpdatedAt = Convert.ToDateTime(reader("updated_at"))
            }
        End Function
    End Class
End Namespace
