Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Services
    ''' <summary>
    ''' User and Staff account administration (Admin Only).
    ''' </summary>
    Public Class StaffService
        ''' <summary>
        ''' Gets all user/staff accounts.
        ''' </summary>
        Public Function GetStaffList(Optional search As String = Nothing) As List(Of Staff)
            PermissionHelper.RequirePermission(PermissionHelper.AppFeature.StaffManagement)
            Dim list As New List(Of Staff)()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT user_id, username, first_name, middle_name, last_name,
                           email, contact_number, role, status, created_at
                    FROM users
                    WHERE 1=1 "

                If Not String.IsNullOrWhiteSpace(search) Then
                    sql &= " AND (username LIKE @search OR first_name LIKE @search OR last_name LIKE @search OR email LIKE @search)"
                End If

                sql &= " ORDER BY user_id ASC;"

                Using cmd As New MySqlCommand(sql, conn)
                    If Not String.IsNullOrWhiteSpace(search) Then cmd.Parameters.AddWithValue("@search", $"%{search.Trim()}%")
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapReaderToStaff(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Gets a staff/user account by ID.
        ''' </summary>
        Public Function GetStaffById(userId As Integer) As Staff
            PermissionHelper.RequirePermission(PermissionHelper.AppFeature.StaffManagement)
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "SELECT * FROM users WHERE user_id = @id;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", userId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapReaderToStaff(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Creates a new staff or admin user account.
        ''' </summary>
        Public Function AddStaff(staff As Staff, initialPassword As String) As Integer
            PermissionHelper.RequirePermission(PermissionHelper.AppFeature.StaffManagement)
            ValidateStaff(staff)
            If String.IsNullOrWhiteSpace(initialPassword) OrElse initialPassword.Length < 6 Then
                Throw New ArgumentException("Initial password must be at least 6 characters long.")
            End If

            ' Check username uniqueness
            If IsUsernameTaken(staff.Username) Then
                Throw New InvalidOperationException($"Username '{staff.Username}' is already taken.")
            End If

            Dim hash As String = ""
            Dim salt As String = ""
            PasswordHasher.HashPassword(initialPassword, hash, salt)

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    INSERT INTO users (username, password_hash, password_salt, first_name, middle_name, last_name, email, contact_number, role, status)
                    VALUES (@user, @hash, @salt, @first, @mid, @last, @email, @contact, @role, @status);
                    SELECT LAST_INSERT_ID();"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@user", staff.Username.Trim())
                    cmd.Parameters.AddWithValue("@hash", hash)
                    cmd.Parameters.AddWithValue("@salt", salt)
                    cmd.Parameters.AddWithValue("@first", staff.FirstName.Trim())
                    cmd.Parameters.AddWithValue("@mid", If(String.IsNullOrWhiteSpace(staff.MiddleName), DBNull.Value, staff.MiddleName.Trim()))
                    cmd.Parameters.AddWithValue("@last", staff.LastName.Trim())
                    cmd.Parameters.AddWithValue("@email", staff.Email.Trim())
                    cmd.Parameters.AddWithValue("@contact", If(String.IsNullOrWhiteSpace(staff.ContactNumber), DBNull.Value, staff.ContactNumber.Trim()))
                    cmd.Parameters.AddWithValue("@role", staff.Role.ToUpperInvariant())
                    cmd.Parameters.AddWithValue("@status", staff.Status)

                    Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    staff.UserID = newId
                    ActivityLogService.Log("Add Staff User", $"Created account for {staff.FullName} ({staff.Username}, Role: {staff.Role}).")
                    Return newId
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Updates staff account profile and role.
        ''' </summary>
        Public Sub UpdateStaff(staff As Staff)
            PermissionHelper.RequirePermission(PermissionHelper.AppFeature.StaffManagement)
            ValidateStaff(staff)

            ' Prevent changing admin role if it is the primary admin
            If staff.UserID = 1 AndAlso Not String.Equals(staff.Role, "ADMIN", StringComparison.OrdinalIgnoreCase) Then
                Throw New InvalidOperationException("The primary administrator account role cannot be changed.")
            End If

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    UPDATE users SET
                        first_name = @first,
                        middle_name = @mid,
                        last_name = @last,
                        email = @email,
                        contact_number = @contact,
                        role = @role,
                        status = @status
                    WHERE user_id = @id;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", staff.UserID)
                    cmd.Parameters.AddWithValue("@first", staff.FirstName.Trim())
                    cmd.Parameters.AddWithValue("@mid", If(String.IsNullOrWhiteSpace(staff.MiddleName), DBNull.Value, staff.MiddleName.Trim()))
                    cmd.Parameters.AddWithValue("@last", staff.LastName.Trim())
                    cmd.Parameters.AddWithValue("@email", staff.Email.Trim())
                    cmd.Parameters.AddWithValue("@contact", If(String.IsNullOrWhiteSpace(staff.ContactNumber), DBNull.Value, staff.ContactNumber.Trim()))
                    cmd.Parameters.AddWithValue("@role", staff.Role.ToUpperInvariant())
                    cmd.Parameters.AddWithValue("@status", staff.Status)
                    cmd.ExecuteNonQuery()

                    ActivityLogService.Log("Edit Staff User", $"Updated user account {staff.Username} (ID {staff.UserID}).")
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Resets password for a user.
        ''' </summary>
        Public Sub ResetPassword(userId As Integer, newPassword As String)
            PermissionHelper.RequirePermission(PermissionHelper.AppFeature.StaffManagement)
            If String.IsNullOrWhiteSpace(newPassword) OrElse newPassword.Length < 6 Then
                Throw New ArgumentException("Password must be at least 6 characters long.")
            End If

            Dim hash As String = ""
            Dim salt As String = ""
            PasswordHasher.HashPassword(newPassword, hash, salt)

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "UPDATE users SET password_hash = @hash, password_salt = @salt WHERE user_id = @id;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@hash", hash)
                    cmd.Parameters.AddWithValue("@salt", salt)
                    cmd.Parameters.AddWithValue("@id", userId)
                    cmd.ExecuteNonQuery()

                    ActivityLogService.Log("Reset Password", $"Reset password for user ID {userId}.")
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Toggles user active/inactive status. Primary admin is protected.
        ''' </summary>
        Public Sub SetStaffStatus(userId As Integer, status As String)
            PermissionHelper.RequirePermission(PermissionHelper.AppFeature.StaffManagement)
            If userId = 1 AndAlso String.Equals(status, "Inactive", StringComparison.OrdinalIgnoreCase) Then
                Throw New InvalidOperationException("The master administrator account cannot be deactivated.")
            End If

            If SessionManager.CurrentUser IsNot Nothing AndAlso SessionManager.CurrentUser.UserID = userId Then
                Throw New InvalidOperationException("You cannot deactivate your own currently active account.")
            End If

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "UPDATE users SET status = @status WHERE user_id = @id;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@status", status)
                    cmd.Parameters.AddWithValue("@id", userId)
                    cmd.ExecuteNonQuery()

                    ActivityLogService.Log("User Status Change", $"Changed status of user ID {userId} to '{status}'.")
                End Using
            End Using
        End Sub

        Private Function IsUsernameTaken(username As String) As Boolean
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "SELECT COUNT(*) FROM users WHERE username = @user;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@user", username.Trim())
                    Return Convert.ToInt64(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        End Function

        Private Sub ValidateStaff(staff As Staff)
            If staff Is Nothing Then Throw New ArgumentNullException(NameOf(staff))
            If String.IsNullOrWhiteSpace(staff.Username) Then Throw New ArgumentException("Username is required.")
            If String.IsNullOrWhiteSpace(staff.FirstName) Then Throw New ArgumentException("First Name is required.")
            If String.IsNullOrWhiteSpace(staff.LastName) Then Throw New ArgumentException("Last Name is required.")
            If String.IsNullOrWhiteSpace(staff.Email) OrElse Not ValidationHelper.IsValidEmail(staff.Email) Then
                Throw New ArgumentException("A valid email address is required.")
            End If
            If Not ValidationHelper.IsValidPhone(staff.ContactNumber, required:=False) Then
                Throw New ArgumentException("Invalid contact number format.")
            End If
        End Sub

        Private Function MapReaderToStaff(reader As MySqlDataReader) As Staff
            Dim uid As Integer = Convert.ToInt32(reader("user_id"))
            Return New Staff() With {
                .StaffID = uid,
                .UserID = uid,
                .EmployeeID = $"EMP-{uid:D4}",
                .Username = reader("username").ToString(),
                .FirstName = reader("first_name").ToString(),
                .MiddleName = If(reader.IsDBNull(reader.GetOrdinal("middle_name")), "", reader("middle_name").ToString()),
                .LastName = reader("last_name").ToString(),
                .Email = reader("email").ToString(),
                .ContactNumber = If(reader.IsDBNull(reader.GetOrdinal("contact_number")), "", reader("contact_number").ToString()),
                .Role = reader("role").ToString(),
                .Status = reader("status").ToString(),
                .DateCreated = Convert.ToDateTime(reader("created_at"))
            }
        End Function
    End Class
End Namespace
