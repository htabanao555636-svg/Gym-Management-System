Namespace Models
    ''' <summary>
    ''' Represents a payment or cashiering transaction.
    ''' </summary>
    Public Class Payment
        Public Property PaymentID As Integer
        Public Property ReferenceNumber As String
        Public Property MemberID As Integer
        Public Property MembershipID As Integer?
        Public Property Amount As Decimal
        Public Property PaymentMethod As String = "Cash" ' Cash, GCash, Bank Transfer
        Public Property PaymentDate As DateTime = DateTime.Now
        Public Property PaymentStatus As String = "Paid" ' Paid, Cancelled, Refunded
        Public Property ProcessedBy As Integer
        Public Property Remarks As String

        ' Joined display properties
        Public Property MemberName As String
        Public Property MembershipNumber As String
        Public Property PlanName As String
        Public Property ProcessedByName As String
    End Class
End Namespace
