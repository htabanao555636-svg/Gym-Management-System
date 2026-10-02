Imports Gym_Management_System.Utilities

Namespace Models
    ''' <summary>
    ''' Represents a gym member entity.
    ''' </summary>
    Public Class Member
        Public Property MemberID As Integer
        Public Property MembershipNumber As String
        Public Property FirstName As String
        Public Property MiddleName As String
        Public Property LastName As String
        Public Property Gender As String ' Male, Female, Other
        Public Property BirthDate As Date
        Public Property ContactNumber As String
        Public Property Email As String
        Public Property Address As String
        Public Property EmergencyContactName As String
        Public Property EmergencyContactNumber As String
        Public Property RegistrationDate As Date = Date.Today
        Public Property Status As String = "Active" ' Active, Inactive, Suspended
        Public Property PhotoPath As String
        Public Property CreatedAt As DateTime
        Public Property UpdatedAt As DateTime

        ''' <summary>
        ''' Gets the member's full name.
        ''' </summary>
        Public ReadOnly Property FullName As String
            Get
                If Not String.IsNullOrWhiteSpace(MiddleName) Then
                    Return $"{FirstName} {MiddleName.Substring(0, 1)}. {LastName}"
                Else
                    Return $"{FirstName} {LastName}"
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets the member's calculated age.
        ''' </summary>
        Public ReadOnly Property Age As Integer
            Get
                Return ValidationHelper.CalculateAge(BirthDate)
            End Get
        End Property

        Public ReadOnly Property IsActive As Boolean
            Get
                Return String.Equals(Status, "Active", StringComparison.OrdinalIgnoreCase)
            End Get
        End Property
    End Class
End Namespace
