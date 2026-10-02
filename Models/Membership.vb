Namespace Models
    ''' <summary>
    ''' Represents a member's active or past subscription to a membership plan.
    ''' </summary>
    Public Class Membership
        Public Property MembershipID As Integer
        Public Property MemberID As Integer
        Public Property PlanID As Integer
        Public Property StartDate As Date
        Public Property EndDate As Date
        Public Property Amount As Decimal
        Public Property Status As String = "Active" ' Active, Expiring Soon, Expired, Cancelled
        Public Property CreatedBy As Integer
        Public Property CreatedAt As DateTime
        Public Property UpdatedAt As DateTime

        ' Navigation / Joined properties for display
        Public Property MemberName As String
        Public Property MembershipNumber As String
        Public Property PlanName As String
        Public Property CreatedByName As String

        ''' <summary>
        ''' Computes the dynamic status based on EndDate and current date.
        ''' </summary>
        Public ReadOnly Property ComputedStatus As String
            Get
                If String.Equals(Status, "Cancelled", StringComparison.OrdinalIgnoreCase) Then
                    Return "Cancelled"
                End If

                Dim today As Date = Date.Today
                If EndDate < today Then
                    Return "Expired"
                ElseIf EndDate <= today.AddDays(7) Then
                    Return "Expiring Soon"
                Else
                    Return "Active"
                End If
            End Get
        End Property

        ''' <summary>
        ''' Days remaining until expiration. Negative if already expired.
        ''' </summary>
        Public ReadOnly Property DaysRemaining As Integer
            Get
                Return (EndDate.Date - Date.Today).Days
            End Get
        End Property
    End Class
End Namespace
