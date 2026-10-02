Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Dialog form for adding or editing staff and administrator user accounts.
    ''' Compatible with Visual Studio Windows Forms Designer.
    ''' </summary>
    Partial Public Class StaffDialog
        Inherits Form

        Private ReadOnly _staffController As New StaffController()
        Private ReadOnly _existingStaff As Staff
        Private ReadOnly _isNew As Boolean

        Public Sub New()
            InitializeComponent()
            _isNew = True
            cboRole.SelectedIndex = 0
            cboStatus.SelectedIndex = 0
        End Sub

        Public Sub New(staff As Staff)
            Me.New()
            _existingStaff = staff
            _isNew = (staff Is Nothing)
            If Not _isNew Then
                Me.Text = "Edit Staff Account"
                lblHeaderTitle.Text = "✏️ Edit Account Profile"
                btnSave.Text = "Save Changes"
                LoadStaffData()
            End If
        End Sub

        Private Sub LoadStaffData()
            If Not _isNew AndAlso _existingStaff IsNot Nothing Then
                txtUsername.Text = _existingStaff.Username
                txtUsername.ReadOnly = True
                txtPassword.Enabled = False
                lblPassword.Text = "Password (Protected)"
                txtFirstName.Text = _existingStaff.FirstName
                txtMiddleName.Text = _existingStaff.MiddleName
                txtLastName.Text = _existingStaff.LastName
                txtEmail.Text = _existingStaff.Email
                txtContactNumber.Text = _existingStaff.ContactNumber
                cboRole.SelectedItem = _existingStaff.Role.ToUpperInvariant()
                cboStatus.SelectedItem = _existingStaff.Status
            End If
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            lblError.Text = ""
            Dim staff As Staff = If(_isNew, New Staff(), _existingStaff)
            staff.Username = txtUsername.Text.Trim()
            staff.FirstName = txtFirstName.Text.Trim()
            staff.MiddleName = txtMiddleName.Text.Trim()
            staff.LastName = txtLastName.Text.Trim()
            staff.Email = txtEmail.Text.Trim()
            staff.ContactNumber = txtContactNumber.Text.Trim()
            staff.Role = If(cboRole.SelectedItem IsNot Nothing, cboRole.SelectedItem.ToString(), "STAFF")
            staff.Status = If(cboStatus.SelectedItem IsNot Nothing, cboStatus.SelectedItem.ToString(), "Active")

            Dim password As String = txtPassword.Text

            Dim res = _staffController.SaveStaff(staff, password, _isNew)
            If res.Success Then
                MessageBox.Show("Staff account saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
