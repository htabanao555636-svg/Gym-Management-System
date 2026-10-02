Namespace Models
    ''' <summary>
    ''' Represents a gym membership plan offering.
    ''' </summary>
    Public Class MembershipPlan
        Public Property PlanID As Integer
        Public Property PlanName As String
        Public Property Description As String
        Public Property Duration As Integer
        Public Property DurationUnit As String = "Months" ' Days, Months, Years
        Public Property Price As Decimal
        Public Property Status As String = "Active" ' Active, Inactive
        Public Property CreatedAt As DateTime
        Public Property UpdatedAt As DateTime

        ''' <summary>
        ''' Formatted duration string (e.g., '1 Month', '3 Months').
        ''' </summary>
        Public ReadOnly Property DurationDisplay As String
            Get
                Dim unit As String = DurationUnit
                If Duration = 1 AndAlso unit.EndsWith("s", StringComparison.OrdinalIgnoreCase) Then
                    unit = unit.Substring(0, unit.Length - 1)
                End If
                Return $"{Duration} {unit}"
            End Get
        End Property

        Public ReadOnly Property IsActive As Boolean
            Get
                Return String.Equals(Status, "Active", StringComparison.OrdinalIgnoreCase)
            End Get
        End Property
    End Class
End Namespace
