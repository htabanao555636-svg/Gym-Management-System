Imports System.Drawing
Imports System.Drawing.Printing
Imports Gym_Management_System.Models
Imports Gym_Management_System.Services

Namespace Utilities
    ''' <summary>
    ''' Generates and prints formatted receipts for cashiering payments.
    ''' </summary>
    Public Class ReceiptPrinter
        Private ReadOnly _payment As Payment
        Private ReadOnly _settingService As New SettingService()

        Public Sub New(payment As Payment)
            _payment = payment
        End Sub

        ''' <summary>
        ''' Displays a Print Preview dialog for the receipt.
        ''' </summary>
        Public Sub ShowPreview(parentForm As IWin32Window)
            Dim doc As New PrintDocument()
            AddHandler doc.PrintPage, AddressOf PrintPageHandler
            Using previewDlg As New PrintPreviewDialog()
                previewDlg.Document = doc
                previewDlg.Width = 600
                previewDlg.Height = 700
                previewDlg.StartPosition = FormStartPosition.CenterParent
                previewDlg.ShowDialog(parentForm)
            End Using
        End Sub

        ''' <summary>
        ''' Sends the receipt directly to the default printer.
        ''' </summary>
        Public Sub PrintDirect()
            Dim doc As New PrintDocument()
            AddHandler doc.PrintPage, AddressOf PrintPageHandler
            doc.Print()
        End Sub

        Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
            Dim g As Graphics = e.Graphics
            Dim fontTitle As New Font("Segoe UI", 13, FontStyle.Bold)
            Dim fontHeader As New Font("Segoe UI", 9, FontStyle.Regular)
            Dim fontBody As New Font("Segoe UI", 9, FontStyle.Regular)
            Dim fontBold As New Font("Segoe UI", 9, FontStyle.Bold)
            Dim fontFooter As New Font("Segoe UI", 8, FontStyle.Italic)

            Dim brush As Brush = Brushes.Black
            Dim penLine As New Pen(Color.Gray, 1) With {.DashStyle = Drawing2D.DashStyle.Dash}

            Dim gymName As String = _settingService.GetSetting("gym_name", "TITAN FITNESS & GYM CENTER")
            Dim gymAddr As String = _settingService.GetSetting("gym_address", "123 Fitness Boulevard, Metro City")
            Dim gymContact As String = _settingService.GetSetting("gym_contact", "+63 912 345 6789")
            Dim footerText As String = _settingService.GetSetting("receipt_footer", "Thank you for training with us! Stay fit and stay strong.")

            Dim y As Single = 30
            Dim left As Single = 40
            Dim right As Single = 360
            Dim center As Single = (left + right) / 2

            Dim sfCenter As New StringFormat() With {.Alignment = StringAlignment.Center}
            Dim sfRight As New StringFormat() With {.Alignment = StringAlignment.Far}

            ' Header
            g.DrawString(gymName, fontTitle, brush, center, y, sfCenter)
            y += 24
            g.DrawString(gymAddr, fontHeader, brush, center, y, sfCenter)
            y += 18
            g.DrawString(gymContact, fontHeader, brush, center, y, sfCenter)
            y += 22

            g.DrawLine(penLine, left, y, right, y)
            y += 10

            g.DrawString("OFFICIAL PAYMENT RECEIPT", fontBold, brush, center, y, sfCenter)
            y += 22

            ' Receipt Details
            g.DrawString($"Ref No: {_payment.ReferenceNumber}", fontBold, brush, left, y)
            y += 18
            g.DrawString($"Date: {_payment.PaymentDate:yyyy-MM-dd HH:mm}", fontBody, brush, left, y)
            y += 18
            g.DrawString($"Cashier: {_payment.ProcessedByName}", fontBody, brush, left, y)
            y += 22

            g.DrawLine(penLine, left, y, right, y)
            y += 10

            ' Member Info
            g.DrawString("Member ID / No:", fontBold, brush, left, y)
            g.DrawString(_payment.MembershipNumber, fontBody, brush, right, y, sfRight)
            y += 18
            g.DrawString("Member Name:", fontBold, brush, left, y)
            g.DrawString(_payment.MemberName, fontBody, brush, right, y, sfRight)
            y += 18

            If Not String.IsNullOrWhiteSpace(_payment.PlanName) AndAlso _payment.PlanName <> "N/A" Then
                g.DrawString("Plan Enrolled:", fontBold, brush, left, y)
                g.DrawString(_payment.PlanName, fontBody, brush, right, y, sfRight)
                y += 18
            End If

            g.DrawString("Payment Method:", fontBold, brush, left, y)
            g.DrawString(_payment.PaymentMethod, fontBody, brush, right, y, sfRight)
            y += 22

            g.DrawLine(penLine, left, y, right, y)
            y += 10

            ' Amount
            Dim fontAmount As New Font("Segoe UI", 12, FontStyle.Bold)
            g.DrawString("TOTAL AMOUNT PAID:", fontAmount, brush, left, y)
            g.DrawString($"{_payment.Amount:C}", fontAmount, brush, right, y, sfRight)
            y += 30

            If Not String.IsNullOrWhiteSpace(_payment.Remarks) Then
                g.DrawString($"Remarks: {_payment.Remarks}", fontFooter, brush, left, y)
                y += 18
            End If

            g.DrawLine(penLine, left, y, right, y)
            y += 15

            ' Footer
            Dim rectFooter As New RectangleF(left, y, right - left, 40)
            g.DrawString(footerText, fontFooter, brush, rectFooter, sfCenter)

            e.HasMorePages = False
        End Sub
    End Class
End Namespace
