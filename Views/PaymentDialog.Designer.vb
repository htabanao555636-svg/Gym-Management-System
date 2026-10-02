Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class PaymentDialog
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
            Me.btnProcess = New System.Windows.Forms.Button()
            Me.pnlBody = New System.Windows.Forms.Panel()
            Me.lblError = New System.Windows.Forms.Label()
            Me.txtRemarks = New System.Windows.Forms.TextBox()
            Me.lblRemarks = New System.Windows.Forms.Label()
            Me.cboPaymentMethod = New System.Windows.Forms.ComboBox()
            Me.lblPaymentMethod = New System.Windows.Forms.Label()
            Me.nudAmount = New System.Windows.Forms.NumericUpDown()
            Me.lblAmount = New System.Windows.Forms.Label()
            Me.cboMemberships = New System.Windows.Forms.ComboBox()
            Me.lblMemberships = New System.Windows.Forms.Label()
            Me.cboMembers = New System.Windows.Forms.ComboBox()
            Me.lblMembers = New System.Windows.Forms.Label()
            Me.txtRefNo = New System.Windows.Forms.TextBox()
            Me.lblRefNo = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlFooter.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            CType(Me.nudAmount, System.ComponentModel.ISupportInitialize).BeginInit()
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
            Me.lblHeaderTitle.Text = "💳 Process Payment & Generate Receipt"
            Me.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            ' pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
            Me.pnlFooter.Controls.Add(Me.btnProcess)
            Me.pnlFooter.Controls.Add(Me.pnlSpacer)
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 456)
            Me.pnlFooter.Name = "pnlFooter"
            Me.pnlFooter.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
            Me.pnlFooter.Size = New System.Drawing.Size(504, 65)
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
            Me.btnCancel.Location = New System.Drawing.Point(384, 12)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(100, 41)
            Me.btnCancel.TabIndex = 2
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            ' pnlSpacer
            '
            Me.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlSpacer.Location = New System.Drawing.Point(374, 12)
            Me.pnlSpacer.Name = "pnlSpacer"
            Me.pnlSpacer.Size = New System.Drawing.Size(10, 41)
            Me.pnlSpacer.TabIndex = 1
            '
            ' btnProcess
            '
            Me.btnProcess.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
            Me.btnProcess.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnProcess.Dock = System.Windows.Forms.DockStyle.Right
            Me.btnProcess.FlatAppearance.BorderSize = 0
            Me.btnProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnProcess.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.btnProcess.ForeColor = System.Drawing.Color.White
            Me.btnProcess.Location = New System.Drawing.Point(214, 12)
            Me.btnProcess.Name = "btnProcess"
            Me.btnProcess.Size = New System.Drawing.Size(160, 41)
            Me.btnProcess.TabIndex = 0
            Me.btnProcess.Text = "Process Payment"
            Me.btnProcess.UseVisualStyleBackColor = False
            '
            ' pnlBody
            '
            Me.pnlBody.AutoScroll = True
            Me.pnlBody.Controls.Add(Me.lblError)
            Me.pnlBody.Controls.Add(Me.txtRemarks)
            Me.pnlBody.Controls.Add(Me.lblRemarks)
            Me.pnlBody.Controls.Add(Me.cboPaymentMethod)
            Me.pnlBody.Controls.Add(Me.lblPaymentMethod)
            Me.pnlBody.Controls.Add(Me.nudAmount)
            Me.pnlBody.Controls.Add(Me.lblAmount)
            Me.pnlBody.Controls.Add(Me.cboMemberships)
            Me.pnlBody.Controls.Add(Me.lblMemberships)
            Me.pnlBody.Controls.Add(Me.cboMembers)
            Me.pnlBody.Controls.Add(Me.lblMembers)
            Me.pnlBody.Controls.Add(Me.txtRefNo)
            Me.pnlBody.Controls.Add(Me.lblRefNo)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 55)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(25, 15, 25, 15)
            Me.pnlBody.Size = New System.Drawing.Size(504, 401)
            Me.pnlBody.TabIndex = 1
            '
            ' lblRefNo
            '
            Me.lblRefNo.AutoSize = True
            Me.lblRefNo.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblRefNo.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblRefNo.Location = New System.Drawing.Point(25, 15)
            Me.lblRefNo.Name = "lblRefNo"
            Me.lblRefNo.Size = New System.Drawing.Size(107, 15)
            Me.lblRefNo.TabIndex = 0
            Me.lblRefNo.Text = "Reference Number"
            '
            ' txtRefNo
            '
            Me.txtRefNo.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
            Me.txtRefNo.Location = New System.Drawing.Point(25, 35)
            Me.txtRefNo.Name = "txtRefNo"
            Me.txtRefNo.ReadOnly = True
            Me.txtRefNo.Size = New System.Drawing.Size(450, 23)
            Me.txtRefNo.TabIndex = 1
            '
            ' lblMembers
            '
            Me.lblMembers.AutoSize = True
            Me.lblMembers.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblMembers.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblMembers.Location = New System.Drawing.Point(25, 70)
            Me.lblMembers.Name = "lblMembers"
            Me.lblMembers.Size = New System.Drawing.Size(61, 15)
            Me.lblMembers.TabIndex = 2
            Me.lblMembers.Text = "Member *"
            '
            ' cboMembers
            '
            Me.cboMembers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboMembers.FormattingEnabled = True
            Me.cboMembers.Location = New System.Drawing.Point(25, 90)
            Me.cboMembers.Name = "cboMembers"
            Me.cboMembers.Size = New System.Drawing.Size(450, 23)
            Me.cboMembers.TabIndex = 3
            '
            ' lblMemberships
            '
            Me.lblMemberships.AutoSize = True
            Me.lblMemberships.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblMemberships.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblMemberships.Location = New System.Drawing.Point(25, 125)
            Me.lblMemberships.Name = "lblMemberships"
            Me.lblMemberships.Size = New System.Drawing.Size(256, 15)
            Me.lblMemberships.TabIndex = 4
            Me.lblMemberships.Text = "Link to Membership Subscription (Optional)"
            '
            ' cboMemberships
            '
            Me.cboMemberships.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboMemberships.FormattingEnabled = True
            Me.cboMemberships.Location = New System.Drawing.Point(25, 145)
            Me.cboMemberships.Name = "cboMemberships"
            Me.cboMemberships.Size = New System.Drawing.Size(450, 23)
            Me.cboMemberships.TabIndex = 5
            '
            ' lblAmount
            '
            Me.lblAmount.AutoSize = True
            Me.lblAmount.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblAmount.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblAmount.Location = New System.Drawing.Point(25, 180)
            Me.lblAmount.Name = "lblAmount"
            Me.lblAmount.Size = New System.Drawing.Size(109, 15)
            Me.lblAmount.TabIndex = 6
            Me.lblAmount.Text = "Payment Amount *"
            '
            ' nudAmount
            '
            Me.nudAmount.DecimalPlaces = 2
            Me.nudAmount.Location = New System.Drawing.Point(25, 200)
            Me.nudAmount.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
            Me.nudAmount.Name = "nudAmount"
            Me.nudAmount.Size = New System.Drawing.Size(210, 23)
            Me.nudAmount.TabIndex = 7
            Me.nudAmount.Value = New Decimal(New Integer() {1000, 0, 0, 0})
            '
            ' lblPaymentMethod
            '
            Me.lblPaymentMethod.AutoSize = True
            Me.lblPaymentMethod.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblPaymentMethod.Location = New System.Drawing.Point(255, 180)
            Me.lblPaymentMethod.Name = "lblPaymentMethod"
            Me.lblPaymentMethod.Size = New System.Drawing.Size(109, 15)
            Me.lblPaymentMethod.TabIndex = 8
            Me.lblPaymentMethod.Text = "Payment Method *"
            '
            ' cboPaymentMethod
            '
            Me.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboPaymentMethod.FormattingEnabled = True
            Me.cboPaymentMethod.Items.AddRange(New Object() {"Cash", "GCash", "Bank Transfer"})
            Me.cboPaymentMethod.Location = New System.Drawing.Point(255, 200)
            Me.cboPaymentMethod.Name = "cboPaymentMethod"
            Me.cboPaymentMethod.Size = New System.Drawing.Size(220, 23)
            Me.cboPaymentMethod.TabIndex = 9
            '
            ' lblRemarks
            '
            Me.lblRemarks.AutoSize = True
            Me.lblRemarks.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblRemarks.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblRemarks.Location = New System.Drawing.Point(25, 235)
            Me.lblRemarks.Name = "lblRemarks"
            Me.lblRemarks.Size = New System.Drawing.Size(95, 15)
            Me.lblRemarks.TabIndex = 10
            Me.lblRemarks.Text = "Remarks / Notes"
            '
            ' txtRemarks
            '
            Me.txtRemarks.Location = New System.Drawing.Point(25, 255)
            Me.txtRemarks.Name = "txtRemarks"
            Me.txtRemarks.Size = New System.Drawing.Size(450, 23)
            Me.txtRemarks.TabIndex = 11
            '
            ' lblError
            '
            Me.lblError.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38)
            Me.lblError.Location = New System.Drawing.Point(25, 290)
            Me.lblError.Name = "lblError"
            Me.lblError.Size = New System.Drawing.Size(450, 28)
            Me.lblError.TabIndex = 12
            '
            ' PaymentDialog
            '
            Me.AcceptButton = Me.btnProcess
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
            Me.Name = "PaymentDialog"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Cashiering - Process Payment"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlFooter.ResumeLayout(False)
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            CType(Me.nudAmount, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnProcess As System.Windows.Forms.Button
        Friend WithEvents pnlSpacer As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents lblRefNo As System.Windows.Forms.Label
        Friend WithEvents txtRefNo As System.Windows.Forms.TextBox
        Friend WithEvents lblMembers As System.Windows.Forms.Label
        Friend WithEvents cboMembers As System.Windows.Forms.ComboBox
        Friend WithEvents lblMemberships As System.Windows.Forms.Label
        Friend WithEvents cboMemberships As System.Windows.Forms.ComboBox
        Friend WithEvents lblAmount As System.Windows.Forms.Label
        Friend WithEvents nudAmount As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblPaymentMethod As System.Windows.Forms.Label
        Friend WithEvents cboPaymentMethod As System.Windows.Forms.ComboBox
        Friend WithEvents lblRemarks As System.Windows.Forms.Label
        Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
        Friend WithEvents lblError As System.Windows.Forms.Label
    End Class
End Namespace
