Imports Gym_Management_System.Models
Imports Gym_Management_System.Services

Namespace Controllers
    ''' <summary>
    ''' Controller managing Staff and User account operations (Admin Only).
    ''' </summary>
    Public Class StaffController
        Private ReadOnly _staffService As New StaffService()

        Public Function GetStaffList(Optional search As String = Nothing) As List(Of Staff)
            Try
                Return _staffService.GetStaffList(search)
            Catch ex As Exception
                Return New List(Of Staff)()
            End Try
        End Function

        Public Function GetStaffById(id As Integer) As Staff
            Try
                Return _staffService.GetStaffById(id)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function SaveStaff(staff As Staff, initialPassword As String, isNew As Boolean) As (Success As Boolean, ErrorMessage As String)
            Try
                If isNew Then
                    _staffService.AddStaff(staff, initialPassword)
                Else
                    _staffService.UpdateStaff(staff)
                End If
                Return (True, String.Empty)
            Catch ex As UnauthorizedAccessException
                Return (False, ex.Message)
            Catch ex As InvalidOperationException
                Return (False, ex.Message)
            Catch ex As ArgumentException
                Return (False, ex.Message)
            Catch ex As Exception
                Return (False, "Unable to save staff account.")
            End Try
        End Function

        Public Function ResetPassword(userId As Integer, newPassword As String) As (Success As Boolean, ErrorMessage As String)
            Try
                _staffService.ResetPassword(userId, newPassword)
                Return (True, String.Empty)
            Catch ex As UnauthorizedAccessException
                Return (False, ex.Message)
            Catch ex As ArgumentException
                Return (False, ex.Message)
            Catch ex As Exception
                Return (False, "Unable to reset password.")
            End Try
        End Function

        Public Function SetStatus(userId As Integer, status As String) As (Success As Boolean, ErrorMessage As String)
            Try
                _staffService.SetStaffStatus(userId, status)
                Return (True, String.Empty)
            Catch ex As UnauthorizedAccessException
                Return (False, ex.Message)
            Catch ex As InvalidOperationException
                Return (False, ex.Message)
            Catch ex As Exception
                Return (False, "Unable to change status.")
            End Try
        End Function
    End Class
End Namespace
