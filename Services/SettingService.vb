Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Services
    ''' <summary>
    ''' Manages gym system configuration settings.
    ''' </summary>
    Public Class SettingService
        Public Function GetAllSettings() As Dictionary(Of String, String)
            Dim dict As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "SELECT setting_key, setting_value FROM system_settings;"
                Using cmd As New MySqlCommand(sql, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            dict(reader("setting_key").ToString()) = reader("setting_value").ToString()
                        End While
                    End Using
                End Using
            End Using
            Return dict
        End Function

        Public Function GetSetting(key As String, Optional defaultValue As String = "") As String
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "SELECT setting_value FROM system_settings WHERE setting_key = @key;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@key", key)
                    Dim val As Object = cmd.ExecuteScalar()
                    If val IsNot Nothing AndAlso Not DBNull.Value.Equals(val) Then
                        Return val.ToString()
                    End If
                    Return defaultValue
                End Using
            End Using
        End Function

        Public Sub SaveSetting(key As String, value As String, Optional description As String = "")
            PermissionHelper.RequirePermission(PermissionHelper.AppFeature.Settings)
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    INSERT INTO system_settings (setting_key, setting_value, description)
                    VALUES (@key, @val, @desc)
                    ON DUPLICATE KEY UPDATE setting_value = @val, description = IF(@desc != '', @desc, description);"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@key", key)
                    cmd.Parameters.AddWithValue("@val", value)
                    cmd.Parameters.AddWithValue("@desc", description)
                    cmd.ExecuteNonQuery()
                    ActivityLogService.Log("Update Setting", $"Updated setting '{key}'.")
                End Using
            End Using
        End Sub
    End Class
End Namespace
