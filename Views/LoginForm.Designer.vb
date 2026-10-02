Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class LoginForm
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
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.pnlCard = New System.Windows.Forms.Panel()
            Me.btnExit = New System.Windows.Forms.Button()
            Me.btnLogin = New System.Windows.Forms.Button()
            Me.lblError = New System.Windows.Forms.Label()
            Me.chkShowPassword = New System.Windows.Forms.CheckBox()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.lblPass = New System.Windows.Forms.Label()
            Me.txtUsername = New System.Windows.Forms.TextBox()
            Me.lblUser = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlCard.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(17, 24, 39)
            Me.pnlHeader.Controls.Add(Me.lblSubtitle)
            Me.pnlHeader.Controls.Add(Me.lblTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Padding = New System.Windows.Forms.Padding(15, 20, 15, 10)
            Me.pnlHeader.Size = New System.Drawing.Size(464, 115)
            Me.pnlHeader.TabIndex = 0
            '
            ' lblSubtitle
            '
            Me.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175)
            Me.lblSubtitle.Location = New System.Drawing.Point(15, 65)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(434, 40)
            Me.lblSubtitle.TabIndex = 1
            Me.lblSubtitle.Text = "GYM MANAGEMENT SYSTEM • AUTHENTICATION"
            Me.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            ' lblTitle
            '
            Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Bold)
            Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(249, 115, 22)
            Me.lblTitle.Location = New System.Drawing.Point(15, 20)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(434, 45)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "🏋 TITAN FITNESS"
            Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' pnlCard
            '
            Me.pnlCard.BackColor = System.Drawing.Color.White
            Me.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCard.Controls.Add(Me.btnExit)
            Me.pnlCard.Controls.Add(Me.btnLogin)
            Me.pnlCard.Controls.Add(Me.lblError)
            Me.pnlCard.Controls.Add(Me.chkShowPassword)
            Me.pnlCard.Controls.Add(Me.txtPassword)
            Me.pnlCard.Controls.Add(Me.lblPass)
            Me.pnlCard.Controls.Add(Me.txtUsername)
            Me.pnlCard.Controls.Add(Me.lblUser)
            Me.pnlCard.Location = New System.Drawing.Point(45, 140)
            Me.pnlCard.Name = "pnlCard"
            Me.pnlCard.Padding = New System.Windows.Forms.Padding(25)
            Me.pnlCard.Size = New System.Drawing.Size(375, 340)
            Me.pnlCard.TabIndex = 1
            '
            ' btnExit
            '
            Me.btnExit.BackColor = System.Drawing.Color.FromArgb(229, 231, 235)
            Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnExit.FlatAppearance.BorderSize = 0
            Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.btnExit.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.btnExit.Location = New System.Drawing.Point(240, 260)
            Me.btnExit.Name = "btnExit"
            Me.btnExit.Size = New System.Drawing.Size(107, 40)
            Me.btnExit.TabIndex = 7
            Me.btnExit.Text = "EXIT"
            Me.btnExit.UseVisualStyleBackColor = False
            '
            ' btnLogin
            '
            Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(249, 115, 22)
            Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnLogin.FlatAppearance.BorderSize = 0
            Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnLogin.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
            Me.btnLogin.ForeColor = System.Drawing.Color.White
            Me.btnLogin.Location = New System.Drawing.Point(25, 260)
            Me.btnLogin.Name = "btnLogin"
            Me.btnLogin.Size = New System.Drawing.Size(205, 40)
            Me.btnLogin.TabIndex = 6
            Me.btnLogin.Text = "LOGIN"
            Me.btnLogin.UseVisualStyleBackColor = False
            '
            ' lblError
            '
            Me.lblError.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38)
            Me.lblError.Location = New System.Drawing.Point(25, 220)
            Me.lblError.Name = "lblError"
            Me.lblError.Size = New System.Drawing.Size(322, 34)
            Me.lblError.TabIndex = 5
            '
            ' chkShowPassword
            '
            Me.chkShowPassword.AutoSize = True
            Me.chkShowPassword.Cursor = System.Windows.Forms.Cursors.Hand
            Me.chkShowPassword.Font = New System.Drawing.Font("Segoe UI", 8.5!)
            Me.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128)
            Me.chkShowPassword.Location = New System.Drawing.Point(25, 185)
            Me.chkShowPassword.Name = "chkShowPassword"
            Me.chkShowPassword.Size = New System.Drawing.Size(108, 19)
            Me.chkShowPassword.TabIndex = 4
            Me.chkShowPassword.Text = "Show Password"
            Me.chkShowPassword.UseVisualStyleBackColor = True
            '
            ' txtPassword
            '
            Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 10.5!)
            Me.txtPassword.Location = New System.Drawing.Point(25, 140)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
            Me.txtPassword.Size = New System.Drawing.Size(322, 26)
            Me.txtPassword.TabIndex = 3
            Me.txtPassword.Text = "admin123"
            '
            ' lblPass
            '
            Me.lblPass.AutoSize = True
            Me.lblPass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblPass.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblPass.Location = New System.Drawing.Point(25, 115)
            Me.lblPass.Name = "lblPass"
            Me.lblPass.Size = New System.Drawing.Size(59, 15)
            Me.lblPass.TabIndex = 2
            Me.lblPass.Text = "Password"
            '
            ' txtUsername
            '
            Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 10.5!)
            Me.txtUsername.Location = New System.Drawing.Point(25, 65)
            Me.txtUsername.Name = "txtUsername"
            Me.txtUsername.Size = New System.Drawing.Size(322, 26)
            Me.txtUsername.TabIndex = 1
            Me.txtUsername.Text = "admin"
            '
            ' lblUser
            '
            Me.lblUser.AutoSize = True
            Me.lblUser.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblUser.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblUser.Location = New System.Drawing.Point(25, 40)
            Me.lblUser.Name = "lblUser"
            Me.lblUser.Size = New System.Drawing.Size(64, 15)
            Me.lblUser.TabIndex = 0
            Me.lblUser.Text = "Username"
            '
            ' LoginForm
            '
            Me.AcceptButton = Me.btnLogin
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
            Me.ClientSize = New System.Drawing.Size(464, 521)
            Me.Controls.Add(Me.pnlCard)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.5!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "LoginForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Titan Fitness - System Login"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlCard.ResumeLayout(False)
            Me.pnlCard.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubtitle As System.Windows.Forms.Label
        Friend WithEvents pnlCard As System.Windows.Forms.Panel
        Friend WithEvents lblUser As System.Windows.Forms.Label
        Friend WithEvents txtUsername As System.Windows.Forms.TextBox
        Friend WithEvents lblPass As System.Windows.Forms.Label
        Friend WithEvents txtPassword As System.Windows.Forms.TextBox
        Friend WithEvents chkShowPassword As System.Windows.Forms.CheckBox
        Friend WithEvents lblError As System.Windows.Forms.Label
        Friend WithEvents btnLogin As System.Windows.Forms.Button
        Friend WithEvents btnExit As System.Windows.Forms.Button
    End Class
End Namespace
