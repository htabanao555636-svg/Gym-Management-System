Imports Gym_Management_System.Models
Imports Gym_Management_System.Services
Imports Gym_Management_System.Utilities

Namespace Controllers
    ''' <summary>
    ''' Controller coordinating cashiering, payment verification, and receipt printing.
    ''' </summary>
    Public Class PaymentController
        Private ReadOnly _paymentService As New PaymentService()

        Public Function GetPayments(Optional search As String = Nothing,
                                    Optional method As String = "All",
                                    Optional startDate As Date? = Nothing,
                                    Optional endDate As Date? = Nothing) As List(Of Payment)
            Try
                Return _paymentService.GetPayments(search, method, startDate, endDate)
            Catch ex As Exception
                Return New List(Of Payment)()
            End Try
        End Function

        Public Function GetPaymentById(id As Integer) As Payment
            Try
                Return _paymentService.GetPaymentById(id)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function ProcessPayment(payment As Payment) As (Success As Boolean, Payment As Payment, ErrorMessage As String)
            Try
                Dim processed = _paymentService.ProcessPayment(payment)
                Return (True, processed, String.Empty)
            Catch ex As ArgumentException
                Return (False, Nothing, ex.Message)
            Catch ex As Exception
                Return (False, Nothing, ex.Message)
            End Try
        End Function

        Public Sub PrintReceipt(payment As Payment, Optional parent As IWin32Window = Nothing)
            If payment Is Nothing Then Return
            Dim printer As New ReceiptPrinter(payment)
            If parent IsNot Nothing Then
                printer.ShowPreview(parent)
            Else
                printer.PrintDirect()
            End If
        End Sub

        Public Function GetNextReferenceNumber() As String
            Try
                Return _paymentService.GenerateReferenceNumber()
            Catch ex As Exception
                Return $"PAY-{DateTime.Now:yyyyMMdd}-0001"
            End Try
        End Function
    End Class
End Namespace
