Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Dialog form for processing a cashiering payment transaction.
    ''' Compatible with Visual Studio Windows Forms Designer.
    ''' </summary>
    Partial Public Class PaymentDialog
        Inherits Form

        Private ReadOnly _paymentController As New PaymentController()
        Private ReadOnly _memberController As New MemberController()
        Private ReadOnly _membershipController As New MembershipController()

        Private _membersList As List(Of Member)
        Private _selectedMemberMemberships As List(Of Membership)
        Private _processedPayment As Payment

        Public ReadOnly Property ProcessedPayment As Payment
            Get
                Return _processedPayment
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            txtRefNo.Text = _paymentController.GetNextReferenceNumber()
            cboPaymentMethod.SelectedIndex = 0
            LoadMembers(Nothing)
        End Sub

        Public Sub New(preselectedMemberId As Integer?)
            InitializeComponent()
            txtRefNo.Text = _paymentController.GetNextReferenceNumber()
            cboPaymentMethod.SelectedIndex = 0
            LoadMembers(preselectedMemberId)
        End Sub

        Private Sub LoadMembers(preselectedId As Integer?)
            _membersList = _memberController.GetMembers()
            cboMembers.Items.Clear()
            Dim selIdx As Integer = -1
            For i As Integer = 0 To _membersList.Count - 1
                Dim m = _membersList(i)
                cboMembers.Items.Add($"{m.MembershipNumber} - {m.FullName}")
                If preselectedId.HasValue AndAlso m.MemberID = preselectedId.Value Then
                    selIdx = i
                End If
            Next
            If selIdx >= 0 Then
                cboMembers.SelectedIndex = selIdx
            ElseIf cboMembers.Items.Count > 0 Then
                cboMembers.SelectedIndex = 0
            End If
        End Sub

        Private Sub cboMembers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMembers.SelectedIndexChanged
            If _membersList Is Nothing OrElse cboMembers.SelectedIndex < 0 OrElse cboMembers.SelectedIndex >= _membersList.Count Then Return
            Dim member = _membersList(cboMembers.SelectedIndex)

            ' Load subscriptions for member
            _selectedMemberMemberships = _membershipController.GetMemberships(member.MembershipNumber)
            cboMemberships.Items.Clear()
            cboMemberships.Items.Add("(None / General Payment)")

            Dim defaultIdx As Integer = 0
            For i As Integer = 0 To _selectedMemberMemberships.Count - 1
                Dim ms = _selectedMemberMemberships(i)
                Dim statusTag = If(String.Equals(ms.Status, "Pending Payment", StringComparison.OrdinalIgnoreCase), "⭐ [PENDING PAYMENT]", $"[{ms.Status}]")
                cboMemberships.Items.Add($"{ms.PlanName} ({ms.StartDate:yyyy-MM-dd} to {ms.EndDate:yyyy-MM-dd}) - {ms.Amount:C}  {statusTag}")

                ' Automatically select the pending payment subscription if present
                If defaultIdx = 0 AndAlso String.Equals(ms.Status, "Pending Payment", StringComparison.OrdinalIgnoreCase) Then
                    defaultIdx = i + 1
                End If
            Next

            If defaultIdx = 0 AndAlso _selectedMemberMemberships.Count > 0 Then
                defaultIdx = 1
            End If

            cboMemberships.SelectedIndex = defaultIdx
        End Sub

        Private Sub cboMemberships_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMemberships.SelectedIndexChanged
            If cboMemberships.SelectedIndex > 0 AndAlso _selectedMemberMemberships IsNot Nothing AndAlso (cboMemberships.SelectedIndex - 1) < _selectedMemberMemberships.Count Then
                Dim selectedMs = _selectedMemberMemberships(cboMemberships.SelectedIndex - 1)
                nudAmount.Value = selectedMs.Amount
                txtRemarks.Text = $"Payment for {selectedMs.PlanName} subscription"
            End If
        End Sub

        Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
            lblError.Text = ""
            If _membersList Is Nothing OrElse cboMembers.SelectedIndex < 0 OrElse cboMembers.SelectedIndex >= _membersList.Count Then
                lblError.Text = "Please select a member."
                Return
            End If
            If nudAmount.Value <= 0 Then
                lblError.Text = "Payment amount must be greater than zero."
                Return
            End If

            Dim member = _membersList(cboMembers.SelectedIndex)
            Dim membershipId As Integer? = Nothing
            If cboMemberships.SelectedIndex > 0 AndAlso _selectedMemberMemberships IsNot Nothing AndAlso (cboMemberships.SelectedIndex - 1) < _selectedMemberMemberships.Count Then
                membershipId = _selectedMemberMemberships(cboMemberships.SelectedIndex - 1).MembershipID
            End If

            Dim payment As New Payment() With {
                .ReferenceNumber = txtRefNo.Text.Trim(),
                .MemberID = member.MemberID,
                .MembershipID = membershipId,
                .Amount = nudAmount.Value,
                .PaymentMethod = If(cboPaymentMethod.SelectedItem IsNot Nothing, cboPaymentMethod.SelectedItem.ToString(), "Cash"),
                .PaymentStatus = "Paid",
                .Remarks = txtRemarks.Text.Trim()
            }

            Dim res = _paymentController.ProcessPayment(payment)
            If res.Success Then
                _processedPayment = res.Payment
                _processedPayment.MemberName = member.FullName
                _processedPayment.MembershipNumber = member.MembershipNumber
                _processedPayment.ProcessedByName = If(Gym_Management_System.Utilities.SessionManager.CurrentUser IsNot Nothing, Gym_Management_System.Utilities.SessionManager.CurrentUser.Username, "Admin")

                Dim askPrint As String = $"Payment successfully processed and subscription activated!{Environment.NewLine}" &
                                        $"Reference No: {_processedPayment.ReferenceNumber}{Environment.NewLine}" &
                                        $"Amount: {_processedPayment.Amount:C}{Environment.NewLine}{Environment.NewLine}" &
                                        $"Would you like to print or preview the receipt?"
                If MessageBox.Show(askPrint, "Payment Complete - Subscription Activated", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    _paymentController.PrintReceipt(_processedPayment, Me)
                End If

                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                lblError.Text = res.ErrorMessage
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace
