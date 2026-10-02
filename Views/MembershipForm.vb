Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Dialog form for enrolling a member in a membership plan or renewing their subscription.
    ''' Compatible with Visual Studio Windows Forms Designer.
    ''' </summary>
    Partial Public Class MembershipForm
        Inherits Form

        Private ReadOnly _membershipController As New MembershipController()
        Private ReadOnly _memberController As New MemberController()
        Private ReadOnly _planController As New MembershipPlanController()
        Private ReadOnly _preselectedMemberId As Integer?

        Private _membersList As List(Of Member)
        Private _plansList As List(Of MembershipPlan)

        Public Sub New()
            InitializeComponent()
            dtpStartDate.Value = Date.Today
            LoadDropdownData()
        End Sub

        Public Sub New(memberId As Integer?)
            Me.New()
            _preselectedMemberId = memberId
            LoadDropdownData()
        End Sub

        Private Sub LoadDropdownData()
            _membersList = _memberController.GetMembers(statusFilter:="Active")
            cboMembers.Items.Clear()
            Dim selectedIdx As Integer = -1
            For i As Integer = 0 To _membersList.Count - 1
                Dim m = _membersList(i)
                cboMembers.Items.Add($"{m.MembershipNumber} - {m.FullName}")
                If _preselectedMemberId.HasValue AndAlso m.MemberID = _preselectedMemberId.Value Then
                    selectedIdx = i
                End If
            Next

            If selectedIdx >= 0 Then
                cboMembers.SelectedIndex = selectedIdx
            ElseIf cboMembers.Items.Count > 0 Then
                cboMembers.SelectedIndex = 0
            End If

            _plansList = _planController.GetPlans(activeOnly:=True)
            cboPlans.Items.Clear()
            For Each p In _plansList
                cboPlans.Items.Add($"{p.PlanName} ({p.DurationDisplay} - {p.Price:C})")
            Next
            If cboPlans.Items.Count > 0 Then cboPlans.SelectedIndex = 0

            UpdateDatesAndPricing(Nothing, Nothing)
        End Sub

        Private Sub cboPlans_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPlans.SelectedIndexChanged
            UpdateDatesAndPricing(sender, e)
        End Sub

        Private Sub dtpStartDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpStartDate.ValueChanged
            UpdateDatesAndPricing(sender, e)
        End Sub
        Private Sub cboMembers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMembers.SelectedIndexChanged
            UpdateDatesAndPricing(sender, e)
        End Sub

        Private Sub UpdateDatesAndPricing(sender As Object, e As EventArgs)
            If cboPlans Is Nothing OrElse cboPlans.SelectedIndex < 0 OrElse _plansList Is Nothing OrElse _plansList.Count = 0 OrElse cboPlans.SelectedIndex >= _plansList.Count Then Return
            Dim plan = _plansList(cboPlans.SelectedIndex)
            Dim startDate = dtpStartDate.Value.Date
            Dim endDate = _planController.CalculateEndDate(startDate, plan)

            If lblEndDate IsNot Nothing Then
                lblEndDate.Text = $"{endDate:yyyy-MM-dd} ({plan.DurationDisplay})"
            End If
            If lblAmount IsNot Nothing Then
                lblAmount.Text = plan.Price.ToString("C")
            End If
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            lblError.Text = ""
            If _membersList Is Nothing OrElse cboMembers.SelectedIndex < 0 OrElse cboMembers.SelectedIndex >= _membersList.Count Then
                lblError.Text = "Please select a member."
                Return
            End If
            If _plansList Is Nothing OrElse cboPlans.SelectedIndex < 0 OrElse cboPlans.SelectedIndex >= _plansList.Count Then
                lblError.Text = "Please select a membership plan."
                Return
            End If

            Dim member = _membersList(cboMembers.SelectedIndex)
            Dim plan = _plansList(cboPlans.SelectedIndex)
            Dim startDate = dtpStartDate.Value.Date

            Dim enrollResult = _membershipController.Enroll(member.MemberID, plan.PlanID, startDate)
            If Not enrollResult.Success Then
                lblError.Text = enrollResult.ErrorMessage
                Return
            End If

            Dim msg As String = $"Membership subscription enrolled successfully with status 'Pending Payment'!{Environment.NewLine}{Environment.NewLine}" &
                                $"Member: {member.FullName} ({member.MembershipNumber}){Environment.NewLine}" &
                                $"Plan: {plan.PlanName} ({plan.Price:C}){Environment.NewLine}" &
                                $"Validity: {startDate:yyyy-MM-dd} to {enrollResult.Membership.EndDate:yyyy-MM-dd}{Environment.NewLine}{Environment.NewLine}" &
                                $"Please proceed to the Cashiering / Payments module to complete payment and activate gym access."
            MessageBox.Show(msg, "Subscription Enrolled (Pending Payment)", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace
