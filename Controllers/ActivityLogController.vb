Imports Gym_Management_System.Models
Imports Gym_Management_System.Services

Namespace Controllers
    ''' <summary>
    ''' Controller managing system activity audit logs (Admin Only).
    ''' </summary>
    Public Class ActivityLogController
        Private ReadOnly _logService As New ActivityLogService()

        Public Function GetLogs(Optional startDate As Date? = Nothing, Optional endDate As Date? = Nothing, Optional search As String = Nothing, Optional actionFilter As String = "All") As List(Of ActivityLog)
            Try
                Return _logService.GetLogs(startDate, endDate, search, actionFilter)
            Catch ex As Exception
                Return New List(Of ActivityLog)()
            End Try
        End Function
    End Class
End Namespace
