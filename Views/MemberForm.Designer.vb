Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class MemberForm
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblHeaderTitle = New System.Windows.Forms.Label()
            Me.pnlFooter = New System.Windows.Forms.Panel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.pnlBtnSpacer = New System.Windows.Forms.Panel()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.pnlBody = New System.Windows.Forms.Panel()
            Me.lblValidationMsg = New System.Windows.Forms.Label()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.txtEmergencyContact = New System.Windows.Forms.TextBox()
            Me.lblEmergencyContact = New System.Windows.Forms.Label()
            Me.txtEmergencyName = New System.Windows.Forms.TextBox()
            Me.lblEmergencyName = New System.Windows.Forms.Label()
            Me.txtAddress = New System.Windows.Forms.TextBox()
            Me.lblAddress = New System.Windows.Forms.Label()
            Me.txtEmail = New System.Windows.Forms.TextBox()
            Me.lblEmail = New System.Windows.Forms.Label()
            Me.txtContactNumber = New System.Windows.Forms.TextBox()
            Me.lblContactNumber = New System.Windows.Forms.Label()
            Me.lblAge = New System.Windows.Forms.Label()
            Me.dtpBirthDate = New System.Windows.Forms.DateTimePicker()
            Me.lblBirthDate = New System.Windows.Forms.Label()
            Me.cboGender = New System.Windows.Forms.ComboBox()
            Me.lblGender = New System.Windows.Forms.Label()
            Me.txtLastName = New System.Windows.Forms.TextBox()
            Me.lblLastName = New System.Windows.Forms.Label()
            Me.txtMiddleName = New System.Windows.Forms.TextBox()
            Me.lblMiddleName = New System.Windows.Forms.Label()
            Me.txtFirstName = New System.Windows.Forms.TextBox()
            Me.lblFirstName = New System.Windows.Forms.Label()
            Me.txtMembershipNo = New System.Windows.Forms.TextBox()
            Me.lblMembershipNo = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlFooter.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(31, 41, 55)
            Me.pnlHeader.Controls.Add(Me.lblHeaderTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Padding = New System.Windows.Forms.Padding(20, 14, 20, 10)
            Me.pnlHeader.Size = New System.Drawing.Size(544, 55)
            Me.pnlHeader.TabIndex = 0
            '
            ' lblHeaderTitle
            '
            Me.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblHeaderTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblHeaderTitle.ForeColor = System.Drawing.Color.White
            Me.lblHeaderTitle.Location = New System.Drawing.Point(20, 14)
            Me.lblHeaderTitle.Name = "lblHeaderTitle"
            Me.lblHeaderTitle.Size = New System.Drawing.Size(504, 31)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "👤 Add New Gym Member"
            Me.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            ' pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
            Me.pnlFooter.Controls.Add(Me.btnSave)
            Me.pnlFooter.Controls.Add(Me.pnlBtnSpacer)
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 556)
            Me.pnlFooter.Name = "pnlFooter"
            Me.pnlFooter.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
            Me.pnlFooter.Size = New System.Drawing.Size(544, 65)
            Me.pnlFooter.TabIndex = 2
            '
            ' btnCancel
            '
            Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(229, 231, 235)
            Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
            Me.btnCancel.FlatAppearance.BorderSize = 0
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.5!)
            Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.btnCancel.Location = New System.Drawing.Point(424, 12)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(100, 41)
            Me.btnCancel.TabIndex = 2
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            ' pnlBtnSpacer
            '
            Me.pnlBtnSpacer.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlBtnSpacer.Location = New System.Drawing.Point(414, 12)
            Me.pnlBtnSpacer.Name = "pnlBtnSpacer"
            Me.pnlBtnSpacer.Size = New System.Drawing.Size(10, 41)
            Me.pnlBtnSpacer.TabIndex = 1
            '
            ' btnSave
            '
            Me.btnSave.BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
            Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
            Me.btnSave.FlatAppearance.BorderSize = 0
            Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.btnSave.ForeColor = System.Drawing.Color.White
            Me.btnSave.Location = New System.Drawing.Point(264, 12)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(150, 41)
            Me.btnSave.TabIndex = 0
            Me.btnSave.Text = "Register Member"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            ' pnlBody
            '
            Me.pnlBody.AutoScroll = True
            Me.pnlBody.Controls.Add(Me.lblValidationMsg)
            Me.pnlBody.Controls.Add(Me.cboStatus)
            Me.pnlBody.Controls.Add(Me.lblStatus)
            Me.pnlBody.Controls.Add(Me.txtEmergencyContact)
            Me.pnlBody.Controls.Add(Me.lblEmergencyContact)
            Me.pnlBody.Controls.Add(Me.txtEmergencyName)
            Me.pnlBody.Controls.Add(Me.lblEmergencyName)
            Me.pnlBody.Controls.Add(Me.txtAddress)
            Me.pnlBody.Controls.Add(Me.lblAddress)
            Me.pnlBody.Controls.Add(Me.txtEmail)
            Me.pnlBody.Controls.Add(Me.lblEmail)
            Me.pnlBody.Controls.Add(Me.txtContactNumber)
            Me.pnlBody.Controls.Add(Me.lblContactNumber)
            Me.pnlBody.Controls.Add(Me.lblAge)
            Me.pnlBody.Controls.Add(Me.dtpBirthDate)
            Me.pnlBody.Controls.Add(Me.lblBirthDate)
            Me.pnlBody.Controls.Add(Me.cboGender)
            Me.pnlBody.Controls.Add(Me.lblGender)
            Me.pnlBody.Controls.Add(Me.txtLastName)
            Me.pnlBody.Controls.Add(Me.lblLastName)
            Me.pnlBody.Controls.Add(Me.txtMiddleName)
            Me.pnlBody.Controls.Add(Me.lblMiddleName)
            Me.pnlBody.Controls.Add(Me.txtFirstName)
            Me.pnlBody.Controls.Add(Me.lblFirstName)
            Me.pnlBody.Controls.Add(Me.txtMembershipNo)
            Me.pnlBody.Controls.Add(Me.lblMembershipNo)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 55)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(25, 15, 25, 15)
            Me.pnlBody.Size = New System.Drawing.Size(544, 501)
            Me.pnlBody.TabIndex = 1
            '
            ' lblMembershipNo
            '
            Me.lblMembershipNo.AutoSize = True
            Me.lblMembershipNo.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblMembershipNo.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblMembershipNo.Location = New System.Drawing.Point(25, 15)
            Me.lblMembershipNo.Name = "lblMembershipNo"
            Me.lblMembershipNo.Size = New System.Drawing.Size(134, 15)
            Me.lblMembershipNo.TabIndex = 0
            Me.lblMembershipNo.Text = "Membership Number *"
            '
            ' txtMembershipNo
            '
            Me.txtMembershipNo.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
            Me.txtMembershipNo.Location = New System.Drawing.Point(25, 35)
            Me.txtMembershipNo.Name = "txtMembershipNo"
            Me.txtMembershipNo.ReadOnly = True
            Me.txtMembershipNo.Size = New System.Drawing.Size(490, 23)
            Me.txtMembershipNo.TabIndex = 1
            '
            ' lblFirstName
            '
            Me.lblFirstName.AutoSize = True
            Me.lblFirstName.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblFirstName.Location = New System.Drawing.Point(25, 70)
            Me.lblFirstName.Name = "lblFirstName"
            Me.lblFirstName.Size = New System.Drawing.Size(76, 15)
            Me.lblFirstName.TabIndex = 2
            Me.lblFirstName.Text = "First Name *"
            '
            ' txtFirstName
            '
            Me.txtFirstName.Location = New System.Drawing.Point(25, 90)
            Me.txtFirstName.Name = "txtFirstName"
            Me.txtFirstName.Size = New System.Drawing.Size(155, 23)
            Me.txtFirstName.TabIndex = 3
            '
            ' lblMiddleName
            '
            Me.lblMiddleName.AutoSize = True
            Me.lblMiddleName.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblMiddleName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblMiddleName.Location = New System.Drawing.Point(190, 70)
            Me.lblMiddleName.Name = "lblMiddleName"
            Me.lblMiddleName.Size = New System.Drawing.Size(81, 15)
            Me.lblMiddleName.TabIndex = 4
            Me.lblMiddleName.Text = "Middle Name"
            '
            ' txtMiddleName
            '
            Me.txtMiddleName.Location = New System.Drawing.Point(190, 90)
            Me.txtMiddleName.Name = "txtMiddleName"
            Me.txtMiddleName.Size = New System.Drawing.Size(145, 23)
            Me.txtMiddleName.TabIndex = 5
            '
            ' lblLastName
            '
            Me.lblLastName.AutoSize = True
            Me.lblLastName.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblLastName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblLastName.Location = New System.Drawing.Point(345, 70)
            Me.lblLastName.Name = "lblLastName"
            Me.lblLastName.Size = New System.Drawing.Size(74, 15)
            Me.lblLastName.TabIndex = 6
            Me.lblLastName.Text = "Last Name *"
            '
            ' txtLastName
            '
            Me.txtLastName.Location = New System.Drawing.Point(345, 90)
            Me.txtLastName.Name = "txtLastName"
            Me.txtLastName.Size = New System.Drawing.Size(170, 23)
            Me.txtLastName.TabIndex = 7
            '
            ' lblGender
            '
            Me.lblGender.AutoSize = True
            Me.lblGender.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblGender.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblGender.Location = New System.Drawing.Point(25, 125)
            Me.lblGender.Name = "lblGender"
            Me.lblGender.Size = New System.Drawing.Size(57, 15)
            Me.lblGender.TabIndex = 8
            Me.lblGender.Text = "Gender *"
            '
            ' cboGender
            '
            Me.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboGender.FormattingEnabled = True
            Me.cboGender.Items.AddRange(New Object() {"Male", "Female", "Other"})
            Me.cboGender.Location = New System.Drawing.Point(25, 145)
            Me.cboGender.Name = "cboGender"
            Me.cboGender.Size = New System.Drawing.Size(155, 23)
            Me.cboGender.TabIndex = 9
            '
            ' lblBirthDate
            '
            Me.lblBirthDate.AutoSize = True
            Me.lblBirthDate.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblBirthDate.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblBirthDate.Location = New System.Drawing.Point(190, 125)
            Me.lblBirthDate.Name = "lblBirthDate"
            Me.lblBirthDate.Size = New System.Drawing.Size(73, 15)
            Me.lblBirthDate.TabIndex = 10
            Me.lblBirthDate.Text = "Birth Date *"
            '
            ' dtpBirthDate
            '
            Me.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpBirthDate.Location = New System.Drawing.Point(190, 145)
            Me.dtpBirthDate.Name = "dtpBirthDate"
            Me.dtpBirthDate.Size = New System.Drawing.Size(190, 23)
            Me.dtpBirthDate.TabIndex = 11
            '
            ' lblAge
            '
            Me.lblAge.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblAge.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
            Me.lblAge.Location = New System.Drawing.Point(390, 148)
            Me.lblAge.Name = "lblAge"
            Me.lblAge.Size = New System.Drawing.Size(125, 20)
            Me.lblAge.TabIndex = 12
            Me.lblAge.Text = "Age: -- yrs"
            '
            ' lblContactNumber
            '
            Me.lblContactNumber.AutoSize = True
            Me.lblContactNumber.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblContactNumber.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblContactNumber.Location = New System.Drawing.Point(25, 180)
            Me.lblContactNumber.Name = "lblContactNumber"
            Me.lblContactNumber.Size = New System.Drawing.Size(105, 15)
            Me.lblContactNumber.TabIndex = 13
            Me.lblContactNumber.Text = "Contact Number *"
            '
            ' txtContactNumber
            '
            Me.txtContactNumber.Location = New System.Drawing.Point(25, 200)
            Me.txtContactNumber.Name = "txtContactNumber"
            Me.txtContactNumber.Size = New System.Drawing.Size(235, 23)
            Me.txtContactNumber.TabIndex = 14
            '
            ' lblEmail
            '
            Me.lblEmail.AutoSize = True
            Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblEmail.Location = New System.Drawing.Point(280, 180)
            Me.lblEmail.Name = "lblEmail"
            Me.lblEmail.Size = New System.Drawing.Size(83, 15)
            Me.lblEmail.TabIndex = 15
            Me.lblEmail.Text = "Email Address"
            '
            ' txtEmail
            '
            Me.txtEmail.Location = New System.Drawing.Point(280, 200)
            Me.txtEmail.Name = "txtEmail"
            Me.txtEmail.Size = New System.Drawing.Size(235, 23)
            Me.txtEmail.TabIndex = 16
            '
            ' lblAddress
            '
            Me.lblAddress.AutoSize = True
            Me.lblAddress.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblAddress.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblAddress.Location = New System.Drawing.Point(25, 235)
            Me.lblAddress.Name = "lblAddress"
            Me.lblAddress.Size = New System.Drawing.Size(117, 15)
            Me.lblAddress.TabIndex = 17
            Me.lblAddress.Text = "Residential Address"
            '
            ' txtAddress
            '
            Me.txtAddress.Location = New System.Drawing.Point(25, 255)
            Me.txtAddress.Name = "txtAddress"
            Me.txtAddress.Size = New System.Drawing.Size(490, 23)
            Me.txtAddress.TabIndex = 18
            '
            ' lblEmergencyName
            '
            Me.lblEmergencyName.AutoSize = True
            Me.lblEmergencyName.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblEmergencyName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblEmergencyName.Location = New System.Drawing.Point(25, 290)
            Me.lblEmergencyName.Name = "lblEmergencyName"
            Me.lblEmergencyName.Size = New System.Drawing.Size(149, 15)
            Me.lblEmergencyName.TabIndex = 19
            Me.lblEmergencyName.Text = "Emergency Contact Name"
            '
            ' txtEmergencyName
            '
            Me.txtEmergencyName.Location = New System.Drawing.Point(25, 310)
            Me.txtEmergencyName.Name = "txtEmergencyName"
            Me.txtEmergencyName.Size = New System.Drawing.Size(235, 23)
            Me.txtEmergencyName.TabIndex = 20
            '
            ' lblEmergencyContact
            '
            Me.lblEmergencyContact.AutoSize = True
            Me.lblEmergencyContact.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblEmergencyContact.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblEmergencyContact.Location = New System.Drawing.Point(280, 290)
            Me.lblEmergencyContact.Name = "lblEmergencyContact"
            Me.lblEmergencyContact.Size = New System.Drawing.Size(162, 15)
            Me.lblEmergencyContact.TabIndex = 21
            Me.lblEmergencyContact.Text = "Emergency Contact Number"
            '
            ' txtEmergencyContact
            '
            Me.txtEmergencyContact.Location = New System.Drawing.Point(280, 310)
            Me.txtEmergencyContact.Name = "txtEmergencyContact"
            Me.txtEmergencyContact.Size = New System.Drawing.Size(235, 23)
            Me.txtEmergencyContact.TabIndex = 22
            '
            ' lblStatus
            '
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblStatus.Location = New System.Drawing.Point(25, 345)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(92, 15)
            Me.lblStatus.TabIndex = 23
            Me.lblStatus.Text = "Member Status"
            '
            ' cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.FormattingEnabled = True
            Me.cboStatus.Items.AddRange(New Object() {"Active", "Inactive", "Suspended"})
            Me.cboStatus.Location = New System.Drawing.Point(25, 365)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(235, 23)
            Me.cboStatus.TabIndex = 24
            '
            ' lblValidationMsg
            '
            Me.lblValidationMsg.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblValidationMsg.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38)
            Me.lblValidationMsg.Location = New System.Drawing.Point(25, 405)
            Me.lblValidationMsg.Name = "lblValidationMsg"
            Me.lblValidationMsg.Size = New System.Drawing.Size(490, 30)
            Me.lblValidationMsg.TabIndex = 25
            '
            ' MemberForm
            '
            Me.AcceptButton = Me.btnSave
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(544, 621)
            Me.Controls.Add(Me.pnlBody)
            Me.Controls.Add(Me.pnlFooter)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "MemberForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Register New Member"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlFooter.ResumeLayout(False)
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnSave As System.Windows.Forms.Button
        Friend WithEvents pnlBtnSpacer As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents lblMembershipNo As System.Windows.Forms.Label
        Friend WithEvents txtMembershipNo As System.Windows.Forms.TextBox
        Friend WithEvents lblFirstName As System.Windows.Forms.Label
        Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
        Friend WithEvents lblMiddleName As System.Windows.Forms.Label
        Friend WithEvents txtMiddleName As System.Windows.Forms.TextBox
        Friend WithEvents lblLastName As System.Windows.Forms.Label
        Friend WithEvents txtLastName As System.Windows.Forms.TextBox
        Friend WithEvents lblGender As System.Windows.Forms.Label
        Friend WithEvents cboGender As System.Windows.Forms.ComboBox
        Friend WithEvents lblBirthDate As System.Windows.Forms.Label
        Friend WithEvents dtpBirthDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblAge As System.Windows.Forms.Label
        Friend WithEvents lblContactNumber As System.Windows.Forms.Label
        Friend WithEvents txtContactNumber As System.Windows.Forms.TextBox
        Friend WithEvents lblEmail As System.Windows.Forms.Label
        Friend WithEvents txtEmail As System.Windows.Forms.TextBox
        Friend WithEvents lblAddress As System.Windows.Forms.Label
        Friend WithEvents txtAddress As System.Windows.Forms.TextBox
        Friend WithEvents lblEmergencyName As System.Windows.Forms.Label
        Friend WithEvents txtEmergencyName As System.Windows.Forms.TextBox
        Friend WithEvents lblEmergencyContact As System.Windows.Forms.Label
        Friend WithEvents txtEmergencyContact As System.Windows.Forms.TextBox
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblValidationMsg As System.Windows.Forms.Label
    End Class
End Namespace
