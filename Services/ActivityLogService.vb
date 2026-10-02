Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Services
    ''' <summary>
    ''' Handles recording and querying system audit logs.
    ''' </summary>
    Public Class ActivityLogService
        ''' <summary>
        ''' Logs a user or system activity.
        ''' </summary>
        Public Shared Sub Log(action As String, description As String, Optional userId As Integer? = Nothing)
            Try
                Dim uid As Integer? = userId
                If Not uid.HasValue AndAlso SessionManager.CurrentUser IsNot Nothing Then
                    uid = SessionManager.CurrentUser.UserID
                End If

                Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                    Dim sql As String = "
                        INSERT INTO activity_logs (user_id, action, description, created_at)
                        VALUES (@userId, @action, @desc, @createdAt);"
                    Using cmd As New MySqlCommand(sql, conn)
                        If uid.HasValue Then
                            cmd.Parameters.AddWithValue("@userId", uid.Value)
                        Else
                            cmd.Parameters.AddWithValue("@userId", DBNull.Value)
                        End If
                        cmd.Parameters.AddWithValue("@action", action)
                        cmd.Parameters.AddWithValue("@desc", description)
                        cmd.Parameters.AddWithValue("@createdAt", DateTime.Now)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Catch ex As Exception
                ' Logging failure should not crash the primary transaction, but can be written to debug
                System.Diagnostics.Debug.WriteLine($"Failed to write activity log: {ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' Retrieves activity logs with optional filtering by date range, action, or user.
        ''' </summary>
        Public Function GetLogs(Optional startDate As Date? = Nothing, Optional endDate As Date? = Nothing, Optional search As String = Nothing, Optional actionFilter As String = "All") As List(Of ActivityLog)
            Dim list As New List(Of ActivityLog)()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT l.log_id, l.user_id, l.action, l.description, l.created_at,
                           u.username, u.first_name, u.last_name, u.role
                    FROM activity_logs l
                    LEFT JOIN users u ON l.user_id = u.user_id
                    WHERE 1=1 "

                If startDate.HasValue Then
                    sql &= " AND DATE(l.created_at) >= @startDate"
                End If
                If endDate.HasValue Then
                    sql &= " AND DATE(l.created_at) <= @endDate"
                End If
                If Not String.IsNullOrWhiteSpace(actionFilter) AndAlso Not String.Equals(actionFilter, "All", StringComparison.OrdinalIgnoreCase) Then
                    sql &= " AND l.action LIKE @actionFilter"
                End If
                If Not String.IsNullOrWhiteSpace(search) Then
                    sql &= " AND (l.action LIKE @search OR l.description LIKE @search OR u.username LIKE @search OR u.first_name LIKE @search OR u.last_name LIKE @search)"
                End If

                sql &= " ORDER BY l.created_at DESC LIMIT 1000;"

                Using cmd As New MySqlCommand(sql, conn)
                    If startDate.HasValue Then cmd.Parameters.AddWithValue("@startDate", startDate.Value.Date)
                    If endDate.HasValue Then cmd.Parameters.AddWithValue("@endDate", endDate.Value.Date)
                    If Not String.IsNullOrWhiteSpace(actionFilter) AndAlso Not String.Equals(actionFilter, "All", StringComparison.OrdinalIgnoreCase) Then
                        cmd.Parameters.AddWithValue("@actionFilter", $"%{actionFilter.Trim()}%")
                    End If
                    If Not String.IsNullOrWhiteSpace(search) Then cmd.Parameters.AddWithValue("@search", $"%{search.Trim()}%")

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim uId As Integer? = Nothing
                            Dim uOrd As Integer = reader.GetOrdinal("user_id")
                            If Not reader.IsDBNull(uOrd) Then
                                uId = Convert.ToInt32(reader.GetValue(uOrd))
                            End If

                            Dim log As New ActivityLog() With {
                                .LogID = Convert.ToInt32(reader("log_id")),
                                .UserID = uId,
                                .Action = reader("action").ToString(),
                                .Description = reader("description").ToString(),
                                .CreatedAt = Convert.ToDateTime(reader("created_at")),
                                .Username = If(reader.IsDBNull(reader.GetOrdinal("username")), "System", reader("username").ToString()),
                                .Role = If(reader.IsDBNull(reader.GetOrdinal("role")), "SYSTEM", reader("role").ToString())
                            }
                            Dim fn As String = If(reader.IsDBNull(reader.GetOrdinal("first_name")), "", reader("first_name").ToString())
                            Dim ln As String = If(reader.IsDBNull(reader.GetOrdinal("last_name")), "", reader("last_name").ToString())
                            log.FullName = If(String.IsNullOrWhiteSpace(fn) AndAlso String.IsNullOrWhiteSpace(ln), "System Process", $"{fn} {ln}".Trim())
                            list.Add(log)
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function
    End Class
End Namespace
