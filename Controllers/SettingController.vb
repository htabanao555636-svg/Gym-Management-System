Imports Gym_Management_System.Services

Namespace Controllers
    ''' <summary>
    ''' Controller managing gym configuration and system settings.
    ''' </summary>
    Public Class SettingController
        Private ReadOnly _settingService As New SettingService()

        Public Function GetAllSettings() As Dictionary(Of String, String)
            Try
                Return _settingService.GetAllSettings()
            Catch ex As Exception
                Return New Dictionary(Of String, String)()
            End Try
        End Function

        Public Function GetSetting(key As String, Optional defaultValue As String = "") As String
            Try
                Return _settingService.GetSetting(key, defaultValue)
            Catch ex As Exception
                Return defaultValue
            End Try
        End Function

        Public Function SaveSetting(key As String, value As String, Optional description As String = "") As (Success As Boolean, ErrorMessage As String)
            Try
                _settingService.SaveSetting(key, value, description)
                Return (True, String.Empty)
            Catch ex As UnauthorizedAccessException
                Return (False, ex.Message)
            Catch ex As Exception
                Return (False, "Unable to save setting.")
            End Try
        End Function
    End Class
End Namespace
