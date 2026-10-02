Namespace Models
    ''' <summary>
    ''' Represents a staff member record mapped to system user credentials.
    ''' </summary>
    Public Class Staff
        Public Property StaffID As Integer
        Public Property EmployeeID As String
        Public Property UserID As Integer
        Public Property FirstName As String
        Public Property MiddleName As String
        Public Property LastName As String
        Public Property Email As String
        Public Property ContactNumber As String
        Public Property Username As String
        Public Property Role As String = "STAFF"
        Public Property Status As String = "Active"
        Public Property DateCreated As DateTime

        Public ReadOnly Property FullName As String
            Get
                If Not String.IsNullOrWhiteSpace(MiddleName) Then
                    Return $"{FirstName} {MiddleName.Substring(0, 1)}. {LastName}"
                Else
                    Return $"{FirstName} {LastName}"
                End If
            End Get
        End Property

        Public ReadOnly Property IsActive As Boolean
            Get
                Return String.Equals(Status, "Active", StringComparison.OrdinalIgnoreCase)
            End Get
        End Property
    End Class
End Namespace
