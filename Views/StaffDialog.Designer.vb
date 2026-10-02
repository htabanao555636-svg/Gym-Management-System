Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class StaffDialog
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
            Me.pnlSpacer = New System.Windows.Forms.Panel()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.pnlBody = New System.Windows.Forms.Panel()
            Me.lblError = New System.Windows.Forms.Label()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.cboRole = New System.Windows.Forms.ComboBox()
            Me.lblRole = New System.Windows.Forms.Label()
            Me.txtEmail = New System.Windows.Forms.TextBox()
            Me.lblEmail = New System.Windows.Forms.Label()
            Me.txtContactNumber = New System.Windows.Forms.TextBox()
            Me.lblContactNumber = New System.Windows.Forms.Label()
            Me.txtMiddleName = New System.Windows.Forms.TextBox()
            Me.lblMiddleName = New System.Windows.Forms.Label()
            Me.txtLastName = New System.Windows.Forms.TextBox()
            Me.lblLastName = New System.Windows.Forms.Label()
            Me.txtFirstName = New System.Windows.Forms.TextBox()
            Me.lblFirstName = New System.Windows.Forms.Label()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.lblPassword = New System.Windows.Forms.Label()
            Me.txtUsername = New System.Windows.Forms.TextBox()
            Me.lblUsername = New System.Windows.Forms.Label()
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
            Me.pnlHeader.Size = New System.Drawing.Size(504, 55)
            Me.pnlHeader.TabIndex = 0
            '
            ' lblHeaderTitle
            '
            Me.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblHeaderTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblHeaderTitle.ForeColor = System.Drawing.Color.White
            Me.lblHeaderTitle.Location = New System.Drawing.Point(20, 14)
            Me.lblHeaderTitle.Name = "lblHeaderTitle"
            Me.lblHeaderTitle.Size = New System.Drawing.Size(464, 31)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "👤 Add Staff / User Account"
            Me.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            ' pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
            Me.pnlFooter.Controls.Add(Me.btnSave)
            Me.pnlFooter.Controls.Add(Me.pnlSpacer)
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 456)
            Me.pnlFooter.Name = "pnlFooter"
            Me.pnlFooter.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
            Me.pnlFooter.Size = New System.Drawing.Size(504, 65)
            Me.pnlFooter.TabIndex = 2
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
            Me.btnSave.Location = New System.Drawing.Point(224, 12)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(150, 41)
            Me.btnSave.TabIndex = 0
            Me.btnSave.Text = "Create Account"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            ' pnlSpacer
            '
            Me.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlSpacer.Location = New System.Drawing.Point(374, 12)
            Me.pnlSpacer.Name = "pnlSpacer"
            Me.pnlSpacer.Size = New System.Drawing.Size(10, 41)
            Me.pnlSpacer.TabIndex = 1
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
            Me.btnCancel.Location = New System.Drawing.Point(384, 12)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(100, 41)
            Me.btnCancel.TabIndex = 2
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            ' pnlBody
            '
            Me.pnlBody.AutoScroll = True
            Me.pnlBody.Controls.Add(Me.lblError)
            Me.pnlBody.Controls.Add(Me.cboStatus)
            Me.pnlBody.Controls.Add(Me.lblStatus)
            Me.pnlBody.Controls.Add(Me.cboRole)
            Me.pnlBody.Controls.Add(Me.lblRole)
            Me.pnlBody.Controls.Add(Me.txtEmail)
            Me.pnlBody.Controls.Add(Me.lblEmail)
            Me.pnlBody.Controls.Add(Me.txtContactNumber)
            Me.pnlBody.Controls.Add(Me.lblContactNumber)
            Me.pnlBody.Controls.Add(Me.txtMiddleName)
            Me.pnlBody.Controls.Add(Me.lblMiddleName)
            Me.pnlBody.Controls.Add(Me.txtLastName)
            Me.pnlBody.Controls.Add(Me.lblLastName)
            Me.pnlBody.Controls.Add(Me.txtFirstName)
            Me.pnlBody.Controls.Add(Me.lblFirstName)
            Me.pnlBody.Controls.Add(Me.txtPassword)
            Me.pnlBody.Controls.Add(Me.lblPassword)
            Me.pnlBody.Controls.Add(Me.txtUsername)
            Me.pnlBody.Controls.Add(Me.lblUsername)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 55)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(25, 15, 25, 15)
            Me.pnlBody.Size = New System.Drawing.Size(504, 401)
            Me.pnlBody.TabIndex = 1
            '
            ' lblUsername
            '
            Me.lblUsername.AutoSize = True
            Me.lblUsername.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblUsername.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblUsername.Location = New System.Drawing.Point(25, 15)
            Me.lblUsername.Name = "lblUsername"
            Me.lblUsername.Size = New System.Drawing.Size(73, 15)
            Me.lblUsername.TabIndex = 0
            Me.lblUsername.Text = "Username *"
            '
            ' txtUsername
            '
            Me.txtUsername.Location = New System.Drawing.Point(25, 35)
            Me.txtUsername.Name = "txtUsername"
            Me.txtUsername.Size = New System.Drawing.Size(210, 23)
            Me.txtUsername.TabIndex = 1
            '
            ' lblPassword
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblPassword.Location = New System.Drawing.Point(255, 15)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(102, 15)
            Me.lblPassword.TabIndex = 2
            Me.lblPassword.Text = "Initial Password *"
            '
            ' txtPassword
            '
            Me.txtPassword.Location = New System.Drawing.Point(255, 35)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
            Me.txtPassword.Size = New System.Drawing.Size(215, 23)
            Me.txtPassword.TabIndex = 3
            '
            ' lblFirstName
            '
            Me.lblFirstName.AutoSize = True
            Me.lblFirstName.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblFirstName.Location = New System.Drawing.Point(25, 70)
            Me.lblFirstName.Name = "lblFirstName"
            Me.lblFirstName.Size = New System.Drawing.Size(76, 15)
            Me.lblFirstName.TabIndex = 4
            Me.lblFirstName.Text = "First Name *"
            '
            ' txtFirstName
            '
            Me.txtFirstName.Location = New System.Drawing.Point(25, 90)
            Me.txtFirstName.Name = "txtFirstName"
            Me.txtFirstName.Size = New System.Drawing.Size(210, 23)
            Me.txtFirstName.TabIndex = 5
            '
            ' lblLastName
            '
            Me.lblLastName.AutoSize = True
            Me.lblLastName.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblLastName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblLastName.Location = New System.Drawing.Point(255, 70)
            Me.lblLastName.Name = "lblLastName"
            Me.lblLastName.Size = New System.Drawing.Size(74, 15)
            Me.lblLastName.TabIndex = 6
            Me.lblLastName.Text = "Last Name *"
            '
            ' txtLastName
            '
            Me.txtLastName.Location = New System.Drawing.Point(255, 90)
            Me.txtLastName.Name = "txtLastName"
            Me.txtLastName.Size = New System.Drawing.Size(215, 23)
            Me.txtLastName.TabIndex = 7
            '
            ' lblMiddleName
            '
            Me.lblMiddleName.AutoSize = True
            Me.lblMiddleName.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblMiddleName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblMiddleName.Location = New System.Drawing.Point(25, 125)
            Me.lblMiddleName.Name = "lblMiddleName"
            Me.lblMiddleName.Size = New System.Drawing.Size(81, 15)
            Me.lblMiddleName.TabIndex = 8
            Me.lblMiddleName.Text = "Middle Name"
            '
            ' txtMiddleName
            '
            Me.txtMiddleName.Location = New System.Drawing.Point(25, 145)
            Me.txtMiddleName.Name = "txtMiddleName"
            Me.txtMiddleName.Size = New System.Drawing.Size(210, 23)
            Me.txtMiddleName.TabIndex = 9
            '
            ' lblContactNumber
            '
            Me.lblContactNumber.AutoSize = True
            Me.lblContactNumber.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblContactNumber.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblContactNumber.Location = New System.Drawing.Point(255, 125)
            Me.lblContactNumber.Name = "lblContactNumber"
            Me.lblContactNumber.Size = New System.Drawing.Size(96, 15)
            Me.lblContactNumber.TabIndex = 10
            Me.lblContactNumber.Text = "Contact Number"
            '
            ' txtContactNumber
            '
            Me.txtContactNumber.Location = New System.Drawing.Point(255, 145)
            Me.txtContactNumber.Name = "txtContactNumber"
            Me.txtContactNumber.Size = New System.Drawing.Size(215, 23)
            Me.txtContactNumber.TabIndex = 11
            '
            ' lblEmail
            '
            Me.lblEmail.AutoSize = True
            Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblEmail.Location = New System.Drawing.Point(25, 180)
            Me.lblEmail.Name = "lblEmail"
            Me.lblEmail.Size = New System.Drawing.Size(91, 15)
            Me.lblEmail.TabIndex = 12
            Me.lblEmail.Text = "Email Address *"
            '
            ' txtEmail
            '
            Me.txtEmail.Location = New System.Drawing.Point(25, 200)
            Me.txtEmail.Name = "txtEmail"
            Me.txtEmail.Size = New System.Drawing.Size(445, 23)
            Me.txtEmail.TabIndex = 13
            '
            ' lblRole
            '
            Me.lblRole.AutoSize = True
            Me.lblRole.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblRole.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblRole.Location = New System.Drawing.Point(25, 235)
            Me.lblRole.Name = "lblRole"
            Me.lblRole.Size = New System.Drawing.Size(40, 15)
            Me.lblRole.TabIndex = 14
            Me.lblRole.Text = "Role *"
            '
            ' cboRole
            '
            Me.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboRole.FormattingEnabled = True
            Me.cboRole.Items.AddRange(New Object() {"STAFF", "ADMIN"})
            Me.cboRole.Location = New System.Drawing.Point(25, 255)
            Me.cboRole.Name = "cboRole"
            Me.cboRole.Size = New System.Drawing.Size(210, 23)
            Me.cboRole.TabIndex = 15
            '
            ' lblStatus
            '
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblStatus.Location = New System.Drawing.Point(255, 235)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(42, 15)
            Me.lblStatus.TabIndex = 16
            Me.lblStatus.Text = "Status"
            '
            ' cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.FormattingEnabled = True
            Me.cboStatus.Items.AddRange(New Object() {"Active", "Inactive"})
            Me.cboStatus.Location = New System.Drawing.Point(255, 255)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(215, 23)
            Me.cboStatus.TabIndex = 17
            '
            ' lblError
            '
            Me.lblError.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38)
            Me.lblError.Location = New System.Drawing.Point(25, 290)
            Me.lblError.Name = "lblError"
            Me.lblError.Size = New System.Drawing.Size(445, 28)
            Me.lblError.TabIndex = 18
            '
            ' StaffDialog
            '
            Me.AcceptButton = Me.btnSave
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(504, 521)
            Me.Controls.Add(Me.pnlBody)
            Me.Controls.Add(Me.pnlFooter)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "StaffDialog"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Create User / Staff Account"
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
        Friend WithEvents pnlSpacer As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents lblUsername As System.Windows.Forms.Label
        Friend WithEvents txtUsername As System.Windows.Forms.TextBox
        Friend WithEvents lblPassword As System.Windows.Forms.Label
        Friend WithEvents txtPassword As System.Windows.Forms.TextBox
        Friend WithEvents lblFirstName As System.Windows.Forms.Label
        Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
        Friend WithEvents lblLastName As System.Windows.Forms.Label
        Friend WithEvents txtLastName As System.Windows.Forms.TextBox
        Friend WithEvents lblMiddleName As System.Windows.Forms.Label
        Friend WithEvents txtMiddleName As System.Windows.Forms.TextBox
        Friend WithEvents lblContactNumber As System.Windows.Forms.Label
        Friend WithEvents txtContactNumber As System.Windows.Forms.TextBox
        Friend WithEvents lblEmail As System.Windows.Forms.Label
        Friend WithEvents txtEmail As System.Windows.Forms.TextBox
        Friend WithEvents lblRole As System.Windows.Forms.Label
        Friend WithEvents cboRole As System.Windows.Forms.ComboBox
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblError As System.Windows.Forms.Label
    End Class
End Namespace
