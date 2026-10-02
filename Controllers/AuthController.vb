Imports Gym_Management_System.Models
Imports Gym_Management_System.Services
Imports Gym_Management_System.Utilities

Namespace Controllers
    ''' <summary>
    ''' Controller coordinating user authentication and session operations.
    ''' </summary>
    Public Class AuthController
        Private ReadOnly _authService As New AuthenticationService()

        ''' <summary>
        ''' Attempts to login user with given credentials.
        ''' Returns (Success As Boolean, User As User, ErrorMessage As String)
        ''' </summary>
        Public Function Login(username As String, password As String) As (Success As Boolean, User As User, ErrorMessage As String)
            Try
                Dim user As User = _authService.Authenticate(username, password)
                Return (True, user, String.Empty)
            Catch ex As UnauthorizedAccessException
                Return (False, Nothing, ex.Message)
            Catch ex As ArgumentException
                Return (False, Nothing, ex.Message)
            Catch ex As Exception
                Return (False, Nothing, "Unable to complete login. Please verify database connectivity.")
            End Try
        End Function

        ''' <summary>
        ''' Logs out current user and ends session.
        ''' </summary>
        Public Sub Logout()
            _authService.Logout()
        End Sub

        ''' <summary>
        ''' Returns currently logged-in user.
        ''' </summary>
        Public Function GetCurrentUser() As User
            Return SessionManager.CurrentUser
        End Function
    End Class
End Namespace
