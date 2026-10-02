Namespace Models
    ''' <summary>
    ''' Represents extra gym services/amenities (personal training, locker rental, towel service, etc.).
    ''' </summary>
    Public Class GymService
        Public Property ServiceID As Integer
        Public Property ServiceName As String
        Public Property Description As String
        Public Property Price As Decimal
        Public Property Status As String = "Active"
    End Class
End Namespace
