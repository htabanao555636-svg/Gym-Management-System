Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Dialog form for adding or editing a Membership Plan (Admin only).
    ''' Compatible with Visual Studio Windows Forms Designer.
    ''' </summary>
    Partial Public Class MembershipPlanDialog
        Inherits Form

        Private ReadOnly _planController As New MembershipPlanController()
        Private ReadOnly _existingPlan As MembershipPlan
        Private ReadOnly _isNew As Boolean

        Public Sub New()
            InitializeComponent()
            _isNew = True
            cboDurationUnit.SelectedIndex = 1
            cboStatus.SelectedIndex = 0
        End Sub

        Public Sub New(plan As MembershipPlan)
            Me.New()
            _existingPlan = plan
            _isNew = (plan Is Nothing)
            If Not _isNew Then
                Me.Text = "Edit Membership Plan"
                lblHeaderTitle.Text = "✏️ Edit Membership Plan"
                btnSave.Text = "Save Changes"
                LoadPlanData()
            End If
        End Sub

        Private Sub LoadPlanData()
            If Not _isNew AndAlso _existingPlan IsNot Nothing Then
                txtName.Text = _existingPlan.PlanName
                txtDescription.Text = _existingPlan.Description
                nudDuration.Value = Math.Max(1, _existingPlan.Duration)
                cboDurationUnit.SelectedItem = _existingPlan.DurationUnit
                nudPrice.Value = _existingPlan.Price
                cboStatus.SelectedItem = _existingPlan.Status
            End If
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            lblError.Text = ""
            Dim plan As MembershipPlan = If(_isNew, New MembershipPlan(), _existingPlan)
            plan.PlanName = txtName.Text.Trim()
            plan.Description = txtDescription.Text.Trim()
            plan.Duration = CInt(nudDuration.Value)
            plan.DurationUnit = If(cboDurationUnit.SelectedItem IsNot Nothing, cboDurationUnit.SelectedItem.ToString(), "Months")
            plan.Price = nudPrice.Value
            plan.Status = If(cboStatus.SelectedItem IsNot Nothing, cboStatus.SelectedItem.ToString(), "Active")

            Dim res = _planController.SavePlan(plan, _isNew)
            If res.Success Then
                MessageBox.Show("Membership plan saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
