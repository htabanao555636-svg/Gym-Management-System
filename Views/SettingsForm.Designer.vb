Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class SettingsForm
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
            Me.pnlTop = New System.Windows.Forms.Panel()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.pnlBody = New System.Windows.Forms.Panel()
            Me.pnlCard = New System.Windows.Forms.Panel()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.chkAllowPartial = New System.Windows.Forms.CheckBox()
            Me.txtCurrency = New System.Windows.Forms.TextBox()
            Me.lblCurrency = New System.Windows.Forms.Label()
            Me.txtReceiptFooter = New System.Windows.Forms.TextBox()
            Me.lblReceiptFooter = New System.Windows.Forms.Label()
            Me.txtGymContact = New System.Windows.Forms.TextBox()
            Me.lblGymContact = New System.Windows.Forms.Label()
            Me.txtGymAddress = New System.Windows.Forms.TextBox()
            Me.lblGymAddress = New System.Windows.Forms.Label()
            Me.txtGymName = New System.Windows.Forms.TextBox()
            Me.lblGymName = New System.Windows.Forms.Label()
            Me.pnlTop.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            Me.pnlCard.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlTop
            '
            Me.pnlTop.BackColor = System.Drawing.Color.White
            Me.pnlTop.Controls.Add(Me.lblTitle)
            Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTop.Location = New System.Drawing.Point(0, 0)
            Me.pnlTop.Name = "pnlTop"
            Me.pnlTop.Padding = New System.Windows.Forms.Padding(20, 14, 20, 14)
            Me.pnlTop.Size = New System.Drawing.Size(784, 60)
            Me.pnlTop.TabIndex = 0
            '
            ' lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39)
            Me.lblTitle.Location = New System.Drawing.Point(20, 14)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(376, 21)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "⚙️ System & Gym Configuration (Admin Only)"
            Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            ' pnlBody
            '
            Me.pnlBody.AutoScroll = True
            Me.pnlBody.Controls.Add(Me.pnlCard)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 60)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(30)
            Me.pnlBody.Size = New System.Drawing.Size(784, 561)
            Me.pnlBody.TabIndex = 1
            '
            ' pnlCard
            '
            Me.pnlCard.BackColor = System.Drawing.Color.White
            Me.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCard.Controls.Add(Me.lblStatus)
            Me.pnlCard.Controls.Add(Me.btnSave)
            Me.pnlCard.Controls.Add(Me.chkAllowPartial)
            Me.pnlCard.Controls.Add(Me.txtCurrency)
            Me.pnlCard.Controls.Add(Me.lblCurrency)
            Me.pnlCard.Controls.Add(Me.txtReceiptFooter)
            Me.pnlCard.Controls.Add(Me.lblReceiptFooter)
            Me.pnlCard.Controls.Add(Me.txtGymContact)
            Me.pnlCard.Controls.Add(Me.lblGymContact)
            Me.pnlCard.Controls.Add(Me.txtGymAddress)
            Me.pnlCard.Controls.Add(Me.lblGymAddress)
            Me.pnlCard.Controls.Add(Me.txtGymName)
            Me.pnlCard.Controls.Add(Me.lblGymName)
            Me.pnlCard.Location = New System.Drawing.Point(30, 20)
            Me.pnlCard.Name = "pnlCard"
            Me.pnlCard.Padding = New System.Windows.Forms.Padding(25)
            Me.pnlCard.Size = New System.Drawing.Size(620, 520)
            Me.pnlCard.TabIndex = 0
            '
            ' lblStatus
            '
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129)
            Me.lblStatus.Location = New System.Drawing.Point(200, 458)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(380, 24)
            Me.lblStatus.TabIndex = 12
            '
            ' btnSave
            '
            Me.btnSave.BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
            Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnSave.FlatAppearance.BorderSize = 0
            Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.btnSave.ForeColor = System.Drawing.Color.White
            Me.btnSave.Location = New System.Drawing.Point(25, 450)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(160, 38)
            Me.btnSave.TabIndex = 11
            Me.btnSave.Text = "💾 Save Settings"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            ' chkAllowPartial
            '
            Me.chkAllowPartial.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.chkAllowPartial.Location = New System.Drawing.Point(210, 385)
            Me.chkAllowPartial.Name = "chkAllowPartial"
            Me.chkAllowPartial.Size = New System.Drawing.Size(300, 26)
            Me.chkAllowPartial.TabIndex = 10
            Me.chkAllowPartial.Text = "Allow Partial / Installment Cashiering"
            Me.chkAllowPartial.UseVisualStyleBackColor = True
            '
            ' txtCurrency
            '
            Me.txtCurrency.Location = New System.Drawing.Point(25, 385)
            Me.txtCurrency.Name = "txtCurrency"
            Me.txtCurrency.Size = New System.Drawing.Size(150, 23)
            Me.txtCurrency.TabIndex = 9
            '
            ' lblCurrency
            '
            Me.lblCurrency.AutoSize = True
            Me.lblCurrency.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblCurrency.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblCurrency.Location = New System.Drawing.Point(25, 365)
            Me.lblCurrency.Name = "lblCurrency"
            Me.lblCurrency.Size = New System.Drawing.Size(98, 15)
            Me.lblCurrency.TabIndex = 8
            Me.lblCurrency.Text = "Currency Symbol"
            '
            ' txtReceiptFooter
            '
            Me.txtReceiptFooter.Location = New System.Drawing.Point(25, 285)
            Me.txtReceiptFooter.Multiline = True
            Me.txtReceiptFooter.Name = "txtReceiptFooter"
            Me.txtReceiptFooter.Size = New System.Drawing.Size(560, 50)
            Me.txtReceiptFooter.TabIndex = 7
            '
            ' lblReceiptFooter
            '
            Me.lblReceiptFooter.AutoSize = True
            Me.lblReceiptFooter.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblReceiptFooter.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblReceiptFooter.Location = New System.Drawing.Point(25, 265)
            Me.lblReceiptFooter.Name = "lblReceiptFooter"
            Me.lblReceiptFooter.Size = New System.Drawing.Size(134, 15)
            Me.lblReceiptFooter.TabIndex = 6
            Me.lblReceiptFooter.Text = "Receipt Footer Message"
            '
            ' txtGymContact
            '
            Me.txtGymContact.Location = New System.Drawing.Point(25, 205)
            Me.txtGymContact.Name = "txtGymContact"
            Me.txtGymContact.Size = New System.Drawing.Size(560, 23)
            Me.txtGymContact.TabIndex = 5
            '
            ' lblGymContact
            '
            Me.lblGymContact.AutoSize = True
            Me.lblGymContact.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblGymContact.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblGymContact.Location = New System.Drawing.Point(25, 185)
            Me.lblGymContact.Name = "lblGymContact"
            Me.lblGymContact.Size = New System.Drawing.Size(209, 15)
            Me.lblGymContact.TabIndex = 4
            Me.lblGymContact.Text = "Contact Information (Phone / Email)"
            '
            ' txtGymAddress
            '
            Me.txtGymAddress.Location = New System.Drawing.Point(25, 125)
            Me.txtGymAddress.Name = "txtGymAddress"
            Me.txtGymAddress.Size = New System.Drawing.Size(560, 23)
            Me.txtGymAddress.TabIndex = 3
            '
            ' lblGymAddress
            '
            Me.lblGymAddress.AutoSize = True
            Me.lblGymAddress.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblGymAddress.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblGymAddress.Location = New System.Drawing.Point(25, 105)
            Me.lblGymAddress.Name = "lblGymAddress"
            Me.lblGymAddress.Size = New System.Drawing.Size(240, 15)
            Me.lblGymAddress.TabIndex = 2
            Me.lblGymAddress.Text = "Gym Street Address (Printed on Receipts)"
            '
            ' txtGymName
            '
            Me.txtGymName.Location = New System.Drawing.Point(25, 45)
            Me.txtGymName.Name = "txtGymName"
            Me.txtGymName.Size = New System.Drawing.Size(560, 23)
            Me.txtGymName.TabIndex = 1
            '
            ' lblGymName
            '
            Me.lblGymName.AutoSize = True
            Me.lblGymName.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblGymName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblGymName.Location = New System.Drawing.Point(25, 25)
            Me.lblGymName.Name = "lblGymName"
            Me.lblGymName.Size = New System.Drawing.Size(149, 15)
            Me.lblGymName.TabIndex = 0
            Me.lblGymName.Text = "Gym Organization Name *"
            '
            ' SettingsForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
            Me.ClientSize = New System.Drawing.Size(784, 621)
            Me.Controls.Add(Me.pnlBody)
            Me.Controls.Add(Me.pnlTop)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "SettingsForm"
            Me.Text = "System Settings"
            Me.pnlTop.ResumeLayout(False)
            Me.pnlTop.PerformLayout()
            Me.pnlBody.ResumeLayout(False)
            Me.pnlCard.ResumeLayout(False)
            Me.pnlCard.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTop As System.Windows.Forms.Panel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents pnlCard As System.Windows.Forms.Panel
        Friend WithEvents lblGymName As System.Windows.Forms.Label
        Friend WithEvents txtGymName As System.Windows.Forms.TextBox
        Friend WithEvents lblGymAddress As System.Windows.Forms.Label
        Friend WithEvents txtGymAddress As System.Windows.Forms.TextBox
        Friend WithEvents lblGymContact As System.Windows.Forms.Label
        Friend WithEvents txtGymContact As System.Windows.Forms.TextBox
        Friend WithEvents lblReceiptFooter As System.Windows.Forms.Label
        Friend WithEvents txtReceiptFooter As System.Windows.Forms.TextBox
        Friend WithEvents lblCurrency As System.Windows.Forms.Label
        Friend WithEvents txtCurrency As System.Windows.Forms.TextBox
        Friend WithEvents chkAllowPartial As System.Windows.Forms.CheckBox
        Friend WithEvents btnSave As System.Windows.Forms.Button
        Friend WithEvents lblStatus As System.Windows.Forms.Label
    End Class
End Namespace
