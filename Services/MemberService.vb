Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Services
    ''' <summary>
    ''' Business logic and data access for Gym Members.
    ''' </summary>
    Public Class MemberService
        ''' <summary>
        ''' Generates a unique sequential membership number: MEM-YYYY-XXXX
        ''' </summary>
        Public Function GenerateMembershipNumber() As String
            Dim year As Integer = DateTime.Now.Year
            Dim prefix As String = $"MEM-{year}-"
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT membership_number 
                    FROM members 
                    WHERE membership_number LIKE @pattern 
                    ORDER BY member_id DESC LIMIT 1;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@pattern", $"{prefix}%")
                    Dim result As Object = cmd.ExecuteScalar()
                    Dim nextSeq As Integer = 1
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        Dim lastNumStr As String = result.ToString()
                        Dim parts As String() = lastNumStr.Split("-"c)
                        If parts.Length >= 3 AndAlso Integer.TryParse(parts(2), nextSeq) Then
                            nextSeq += 1
                        End If
                    End If
                    Return $"{prefix}{nextSeq.ToString("D4")}"
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Retrieves all members matching optional search and status criteria.
        ''' </summary>
        Public Function GetMembers(Optional search As String = Nothing, Optional statusFilter As String = "All") As List(Of Member)
            Dim list As New List(Of Member)()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT member_id, membership_number, first_name, middle_name, last_name,
                           gender, birth_date, contact_number, email, address,
                           emergency_contact_name, emergency_contact_number, registration_date,
                           status, photo_path, created_at, updated_at
                    FROM members
                    WHERE 1=1 "

                If Not String.IsNullOrWhiteSpace(statusFilter) AndAlso Not String.Equals(statusFilter, "All", StringComparison.OrdinalIgnoreCase) Then
                    sql &= " AND status = @status"
                End If

                If Not String.IsNullOrWhiteSpace(search) Then
                    sql &= " AND (membership_number LIKE @search OR first_name LIKE @search OR last_name LIKE @search OR CONCAT(first_name, ' ', last_name) LIKE @search OR CONCAT(first_name, ' ', COALESCE(middle_name, ''), ' ', last_name) LIKE @search OR contact_number LIKE @search OR email LIKE @search)"
                End If

                sql &= " ORDER BY member_id DESC;"

                Using cmd As New MySqlCommand(sql, conn)
                    If Not String.IsNullOrWhiteSpace(statusFilter) AndAlso Not String.Equals(statusFilter, "All", StringComparison.OrdinalIgnoreCase) Then
                        cmd.Parameters.AddWithValue("@status", statusFilter)
                    End If
                    If Not String.IsNullOrWhiteSpace(search) Then
                        cmd.Parameters.AddWithValue("@search", $"%{search.Trim()}%")
                    End If

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapReaderToMember(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Gets a member by their primary key ID.
        ''' </summary>
        Public Function GetMemberById(memberId As Integer) As Member
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "SELECT * FROM members WHERE member_id = @id;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", memberId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapReaderToMember(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets a member by their membership card/code number.
        ''' </summary>
        Public Function GetMemberByNumber(membershipNumber As String) As Member
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "SELECT * FROM members WHERE membership_number = @number;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@number", membershipNumber.Trim())
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapReaderToMember(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Adds a new member to the system.
        ''' </summary>
        Public Function AddMember(member As Member) As Integer
            ValidateMember(member, isNew:=True)

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                ' Ensure unique membership number
                If String.IsNullOrWhiteSpace(member.MembershipNumber) Then
                    member.MembershipNumber = GenerateMembershipNumber()
                End If

                Dim sql As String = "
                    INSERT INTO members (membership_number, first_name, middle_name, last_name, gender,
                                         birth_date, contact_number, email, address,
                                         emergency_contact_name, emergency_contact_number,
                                         registration_date, status, photo_path)
                    VALUES (@num, @first, @mid, @last, @gender, @bdate, @contact, @email,
                            @address, @eName, @eContact, @regDate, @status, @photo);
                    SELECT LAST_INSERT_ID();"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@num", member.MembershipNumber.Trim())
                    cmd.Parameters.AddWithValue("@first", member.FirstName.Trim())
                    cmd.Parameters.AddWithValue("@mid", If(String.IsNullOrWhiteSpace(member.MiddleName), DBNull.Value, member.MiddleName.Trim()))
                    cmd.Parameters.AddWithValue("@last", member.LastName.Trim())
                    cmd.Parameters.AddWithValue("@gender", member.Gender)
                    cmd.Parameters.AddWithValue("@bdate", member.BirthDate.Date)
                    cmd.Parameters.AddWithValue("@contact", member.ContactNumber.Trim())
                    cmd.Parameters.AddWithValue("@email", If(String.IsNullOrWhiteSpace(member.Email), DBNull.Value, member.Email.Trim()))
                    cmd.Parameters.AddWithValue("@address", If(String.IsNullOrWhiteSpace(member.Address), DBNull.Value, member.Address.Trim()))
                    cmd.Parameters.AddWithValue("@eName", If(String.IsNullOrWhiteSpace(member.EmergencyContactName), DBNull.Value, member.EmergencyContactName.Trim()))
                    cmd.Parameters.AddWithValue("@eContact", If(String.IsNullOrWhiteSpace(member.EmergencyContactNumber), DBNull.Value, member.EmergencyContactNumber.Trim()))
                    cmd.Parameters.AddWithValue("@regDate", member.RegistrationDate.Date)
                    cmd.Parameters.AddWithValue("@status", member.Status)
                    cmd.Parameters.AddWithValue("@photo", If(String.IsNullOrWhiteSpace(member.PhotoPath), DBNull.Value, member.PhotoPath))

                    Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    member.MemberID = newId
                    ActivityLogService.Log("Add Member", $"Registered new member {member.FullName} ({member.MembershipNumber}).")
                    Return newId
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Updates an existing member's information.
        ''' </summary>
        Public Sub UpdateMember(member As Member)
            ValidateMember(member, isNew:=False)

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    UPDATE members SET
                        first_name = @first,
                        middle_name = @mid,
                        last_name = @last,
                        gender = @gender,
                        birth_date = @bdate,
                        contact_number = @contact,
                        email = @email,
                        address = @address,
                        emergency_contact_name = @eName,
                        emergency_contact_number = @eContact,
                        status = @status,
                        photo_path = @photo
                    WHERE member_id = @id;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", member.MemberID)
                    cmd.Parameters.AddWithValue("@first", member.FirstName.Trim())
                    cmd.Parameters.AddWithValue("@mid", If(String.IsNullOrWhiteSpace(member.MiddleName), DBNull.Value, member.MiddleName.Trim()))
                    cmd.Parameters.AddWithValue("@last", member.LastName.Trim())
                    cmd.Parameters.AddWithValue("@gender", member.Gender)
                    cmd.Parameters.AddWithValue("@bdate", member.BirthDate.Date)
                    cmd.Parameters.AddWithValue("@contact", member.ContactNumber.Trim())
                    cmd.Parameters.AddWithValue("@email", If(String.IsNullOrWhiteSpace(member.Email), DBNull.Value, member.Email.Trim()))
                    cmd.Parameters.AddWithValue("@address", If(String.IsNullOrWhiteSpace(member.Address), DBNull.Value, member.Address.Trim()))
                    cmd.Parameters.AddWithValue("@eName", If(String.IsNullOrWhiteSpace(member.EmergencyContactName), DBNull.Value, member.EmergencyContactName.Trim()))
                    cmd.Parameters.AddWithValue("@eContact", If(String.IsNullOrWhiteSpace(member.EmergencyContactNumber), DBNull.Value, member.EmergencyContactNumber.Trim()))
                    cmd.Parameters.AddWithValue("@status", member.Status)
                    cmd.Parameters.AddWithValue("@photo", If(String.IsNullOrWhiteSpace(member.PhotoPath), DBNull.Value, member.PhotoPath))
                    cmd.ExecuteNonQuery()

                    ActivityLogService.Log("Edit Member", $"Updated member record for {member.FullName} ({member.MembershipNumber}).")
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Soft deactivates or updates a member's status (Active, Inactive, Suspended).
        ''' </summary>
        Public Sub SetMemberStatus(memberId As Integer, newStatus As String)
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "UPDATE members SET status = @status WHERE member_id = @id;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@status", newStatus)
                    cmd.Parameters.AddWithValue("@id", memberId)
                    cmd.ExecuteNonQuery()

                    ActivityLogService.Log("Member Status Change", $"Changed status of member ID {memberId} to '{newStatus}'.")
                End Using
            End Using
        End Sub

        Private Sub ValidateMember(member As Member, isNew As Boolean)
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If String.IsNullOrWhiteSpace(member.FirstName) Then Throw New ArgumentException("First Name is required.")
            If String.IsNullOrWhiteSpace(member.LastName) Then Throw New ArgumentException("Last Name is required.")
            If String.IsNullOrWhiteSpace(member.ContactNumber) Then Throw New ArgumentException("Contact number is required.")
            If Not ValidationHelper.IsValidPhone(member.ContactNumber) Then Throw New ArgumentException("Invalid contact number format.")
            If Not ValidationHelper.IsValidEmail(member.Email, required:=False) Then Throw New ArgumentException("Invalid email address format.")
            If Not ValidationHelper.IsValidAge(member.BirthDate) Then Throw New ArgumentException("Member age must be between 10 and 100 years.")
        End Sub

        Private Function MapReaderToMember(reader As MySqlDataReader) As Member
            Return New Member() With {
                .MemberID = Convert.ToInt32(reader("member_id")),
                .MembershipNumber = reader("membership_number").ToString(),
                .FirstName = reader("first_name").ToString(),
                .MiddleName = If(reader.IsDBNull(reader.GetOrdinal("middle_name")), "", reader("middle_name").ToString()),
                .LastName = reader("last_name").ToString(),
                .Gender = reader("gender").ToString(),
                .BirthDate = Convert.ToDateTime(reader("birth_date")),
                .ContactNumber = reader("contact_number").ToString(),
                .Email = If(reader.IsDBNull(reader.GetOrdinal("email")), "", reader("email").ToString()),
                .Address = If(reader.IsDBNull(reader.GetOrdinal("address")), "", reader("address").ToString()),
                .EmergencyContactName = If(reader.IsDBNull(reader.GetOrdinal("emergency_contact_name")), "", reader("emergency_contact_name").ToString()),
                .EmergencyContactNumber = If(reader.IsDBNull(reader.GetOrdinal("emergency_contact_number")), "", reader("emergency_contact_number").ToString()),
                .RegistrationDate = Convert.ToDateTime(reader("registration_date")),
                .Status = reader("status").ToString(),
                .PhotoPath = If(reader.IsDBNull(reader.GetOrdinal("photo_path")), "", reader("photo_path").ToString()),
                .CreatedAt = Convert.ToDateTime(reader("created_at")),
                .UpdatedAt = Convert.ToDateTime(reader("updated_at"))
            }
        End Function
    End Class
End Namespace
