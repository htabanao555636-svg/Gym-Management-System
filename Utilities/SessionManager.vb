Imports Gym_Management_System.Models

Namespace Utilities
    ''' <summary>
    ''' Manages the current logged-in user session and state across the application.
    ''' </summary>
    Public Class SessionManager
        Private Shared _currentUser As User
        Private Shared _loginTime As DateTime?

        ''' <summary>
        ''' Gets the currently authenticated user.
        ''' </summary>
        Public Shared ReadOnly Property CurrentUser As User
            Get
                Return _currentUser
            End Get
        End Property

        ''' <summary>
        ''' Gets the timestamp when the current session was initiated.
        ''' </summary>
        Public Shared ReadOnly Property LoginTime As DateTime?
            Get
                Return _loginTime
            End Get
        End Property

        ''' <summary>
        ''' Returns true if an active user session is authenticated.
        ''' </summary>
        Public Shared ReadOnly Property IsLoggedIn As Boolean
            Get
                Return _currentUser IsNot Nothing
            End Get
        End Property

        ''' <summary>
        ''' Initializes a new session for the authenticated user.
        ''' </summary>
        Public Shared Sub StartSession(user As User)
            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If
            _currentUser = user
            _loginTime = DateTime.Now
        End Sub

        ''' <summary>
        ''' Clears the active session and logs out the user.
        ''' </summary>
        Public Shared Sub ClearSession()
            _currentUser = Nothing
            _loginTime = Nothing
        End Sub

        ''' <summary>
        ''' Helper to check if the current user has Admin privileges.
        ''' </summary>
        Public Shared ReadOnly Property IsAdmin As Boolean
            Get
                Return _currentUser IsNot Nothing AndAlso _currentUser.IsAdmin
            End Get
        End Property

        ''' <summary>
        ''' Helper to check if the current user has Staff privileges.
        ''' </summary>
        Public Shared ReadOnly Property IsStaff As Boolean
            Get
                Return _currentUser IsNot Nothing AndAlso String.Equals(_currentUser.Role, "STAFF", StringComparison.OrdinalIgnoreCase)
            End Get
        End Property
    End Class
End Namespace
