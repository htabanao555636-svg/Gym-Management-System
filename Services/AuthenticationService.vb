Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Services
    ''' <summary>
    ''' Handles user authentication, credential validation, and session life cycle.
    ''' </summary>
    Public Class AuthenticationService
        ''' <summary>
        ''' Authenticates user against MySQL database. Returns User model if valid.
        ''' </summary>
        Public Function Authenticate(username As String, password As String) As User
            If String.IsNullOrWhiteSpace(username) Then
                Throw New ArgumentException("Username is required.")
            End If
            If String.IsNullOrWhiteSpace(password) Then
                Throw New ArgumentException("Password is required.")
            End If

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT user_id, username, password_hash, password_salt, first_name, middle_name,
                           last_name, email, contact_number, role, status, created_at, updated_at
                    FROM users
                    WHERE username = @username;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@username", username.Trim())
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim status As String = reader("status").ToString()
                            If Not String.Equals(status, "Active", StringComparison.OrdinalIgnoreCase) Then
                                Throw New UnauthorizedAccessException("This user account is currently deactivated. Please contact an administrator.")
                            End If

                            Dim storedHash As String = reader("password_hash").ToString()
                            Dim storedSalt As String = reader("password_salt").ToString()

                            If PasswordHasher.VerifyPassword(password, storedHash, storedSalt) Then
                                Dim user As New User() With {
                                    .UserID = Convert.ToInt32(reader("user_id")),
                                    .Username = reader("username").ToString(),
                                    .PasswordHash = storedHash,
                                    .PasswordSalt = storedSalt,
                                    .FirstName = reader("first_name").ToString(),
                                    .MiddleName = If(reader.IsDBNull(reader.GetOrdinal("middle_name")), "", reader("middle_name").ToString()),
                                    .LastName = reader("last_name").ToString(),
                                    .Email = reader("email").ToString(),
                                    .ContactNumber = If(reader.IsDBNull(reader.GetOrdinal("contact_number")), "", reader("contact_number").ToString()),
                                    .Role = reader("role").ToString().ToUpperInvariant(),
                                    .Status = status,
                                    .CreatedAt = Convert.ToDateTime(reader("created_at")),
                                    .UpdatedAt = Convert.ToDateTime(reader("updated_at"))
                                }

                                ' Start session
                                SessionManager.StartSession(user)
                                ActivityLogService.Log("Login", $"User '{user.Username}' ({user.Role}) logged in successfully.", user.UserID)
                                Return user
                            Else
                                Throw New UnauthorizedAccessException("Invalid username or password.")
                            End If
                        Else
                            Throw New UnauthorizedAccessException("Invalid username or password.")
                        End If
                    End Using
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Logs out the current user and clears session state.
        ''' </summary>
        Public Sub Logout()
            If SessionManager.IsLoggedIn Then
                Dim user = SessionManager.CurrentUser
                ActivityLogService.Log("Logout", $"User '{user.Username}' logged out.", user.UserID)
                SessionManager.ClearSession()
            End If
        End Sub
    End Class
End Namespace
