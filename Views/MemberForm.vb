Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Dialog form for adding or editing a gym member.
    ''' Compatible with Visual Studio Windows Forms Designer.
    ''' </summary>
    Partial Public Class MemberForm
        Inherits Form

        Private ReadOnly _memberController As New MemberController()
        Private ReadOnly _existingMember As Member
        Private ReadOnly _isNew As Boolean

        Public Sub New()
            InitializeComponent()
            _isNew = True
            cboGender.SelectedIndex = 0
            cboStatus.SelectedIndex = 0
            dtpBirthDate.Value = Date.Today.AddYears(-22)
            UpdateAgeDisplay(Nothing, Nothing)
            txtMembershipNo.Text = _memberController.GetNextMembershipNumber()
        End Sub

        Public Sub New(member As Member)
            Me.New()
            _existingMember = member
            _isNew = (member Is Nothing)
            If Not _isNew Then
                Me.Text = "Edit Member Details"
                lblHeaderTitle.Text = "✏️ Edit Member Profile"
                btnSave.Text = "Save Changes"
                LoadMemberData()
            End If
        End Sub

        Private Sub dtpBirthDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpBirthDate.ValueChanged
            UpdateAgeDisplay(sender, e)
        End Sub

        Private Sub UpdateAgeDisplay(sender As Object, e As EventArgs)
            If dtpBirthDate IsNot Nothing AndAlso lblAge IsNot Nothing Then
                Dim age As Integer = Gym_Management_System.Utilities.ValidationHelper.CalculateAge(dtpBirthDate.Value)
                lblAge.Text = $"Age: {age} yrs"
            End If
        End Sub

        Private Sub LoadMemberData()
            If _isNew Then
                txtMembershipNo.Text = _memberController.GetNextMembershipNumber()
            Else
                txtMembershipNo.Text = _existingMember.MembershipNumber
                txtFirstName.Text = _existingMember.FirstName
                txtMiddleName.Text = _existingMember.MiddleName
                txtLastName.Text = _existingMember.LastName
                cboGender.SelectedItem = _existingMember.Gender
                dtpBirthDate.Value = _existingMember.BirthDate
                txtContactNumber.Text = _existingMember.ContactNumber
                txtEmail.Text = _existingMember.Email
                txtAddress.Text = _existingMember.Address
                txtEmergencyName.Text = _existingMember.EmergencyContactName
                txtEmergencyContact.Text = _existingMember.EmergencyContactNumber
                cboStatus.SelectedItem = _existingMember.Status
                UpdateAgeDisplay(Nothing, Nothing)
            End If
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            lblValidationMsg.Text = ""

            Dim member As Member = If(_isNew, New Member(), _existingMember)
            member.MembershipNumber = txtMembershipNo.Text.Trim()
            member.FirstName = txtFirstName.Text.Trim()
            member.MiddleName = txtMiddleName.Text.Trim()
            member.LastName = txtLastName.Text.Trim()
            member.Gender = If(cboGender.SelectedItem IsNot Nothing, cboGender.SelectedItem.ToString(), "Male")
            member.BirthDate = dtpBirthDate.Value.Date
            member.ContactNumber = txtContactNumber.Text.Trim()
            member.Email = txtEmail.Text.Trim()
            member.Address = txtAddress.Text.Trim()
            member.EmergencyContactName = txtEmergencyName.Text.Trim()
            member.EmergencyContactNumber = txtEmergencyContact.Text.Trim()
            member.Status = If(cboStatus.SelectedItem IsNot Nothing, cboStatus.SelectedItem.ToString(), "Active")

            Dim result = _memberController.SaveMember(member, _isNew)
            If result.Success Then
                MessageBox.Show(If(_isNew, "Member registered successfully!", "Member profile updated successfully!"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                lblValidationMsg.Text = result.ErrorMessage
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace
