Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class MembershipForm
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
            Me.pnlNotice = New System.Windows.Forms.Panel()
            Me.lblNoticeText = New System.Windows.Forms.Label()
            Me.lblAmount = New System.Windows.Forms.Label()
            Me.lblAmountTitle = New System.Windows.Forms.Label()
            Me.lblEndDate = New System.Windows.Forms.Label()
            Me.lblEndDateTitle = New System.Windows.Forms.Label()
            Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
            Me.lblStartDate = New System.Windows.Forms.Label()
            Me.cboPlans = New System.Windows.Forms.ComboBox()
            Me.lblPlans = New System.Windows.Forms.Label()
            Me.cboMembers = New System.Windows.Forms.ComboBox()
            Me.lblMembers = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlFooter.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            Me.pnlNotice.SuspendLayout()
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
            Me.pnlHeader.Size = New System.Drawing.Size(524, 55)
            Me.pnlHeader.TabIndex = 0
            '
            ' lblHeaderTitle
            '
            Me.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblHeaderTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblHeaderTitle.ForeColor = System.Drawing.Color.White
            Me.lblHeaderTitle.Location = New System.Drawing.Point(20, 14)
            Me.lblHeaderTitle.Name = "lblHeaderTitle"
            Me.lblHeaderTitle.Size = New System.Drawing.Size(484, 31)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "⭐ Enroll / Renew Subscription"
            Me.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            ' pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
            Me.pnlFooter.Controls.Add(Me.btnSave)
            Me.pnlFooter.Controls.Add(Me.pnlSpacer)
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 396)
            Me.pnlFooter.Name = "pnlFooter"
            Me.pnlFooter.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
            Me.pnlFooter.Size = New System.Drawing.Size(524, 65)
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
            Me.btnCancel.Location = New System.Drawing.Point(404, 12)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(100, 41)
            Me.btnCancel.TabIndex = 2
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            ' pnlSpacer
            '
            Me.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlSpacer.Location = New System.Drawing.Point(394, 12)
            Me.pnlSpacer.Name = "pnlSpacer"
            Me.pnlSpacer.Size = New System.Drawing.Size(10, 41)
            Me.pnlSpacer.TabIndex = 1
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
            Me.btnSave.Size = New System.Drawing.Size(170, 41)
            Me.btnSave.TabIndex = 0
            Me.btnSave.Text = "Enroll Subscription"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            ' pnlBody
            '
            Me.pnlBody.AutoScroll = True
            Me.pnlBody.Controls.Add(Me.lblError)
            Me.pnlBody.Controls.Add(Me.pnlNotice)
            Me.pnlBody.Controls.Add(Me.lblAmount)
            Me.pnlBody.Controls.Add(Me.lblAmountTitle)
            Me.pnlBody.Controls.Add(Me.lblEndDate)
            Me.pnlBody.Controls.Add(Me.lblEndDateTitle)
            Me.pnlBody.Controls.Add(Me.dtpStartDate)
            Me.pnlBody.Controls.Add(Me.lblStartDate)
            Me.pnlBody.Controls.Add(Me.cboPlans)
            Me.pnlBody.Controls.Add(Me.lblPlans)
            Me.pnlBody.Controls.Add(Me.cboMembers)
            Me.pnlBody.Controls.Add(Me.lblMembers)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 55)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(25, 15, 25, 15)
            Me.pnlBody.Size = New System.Drawing.Size(524, 341)
            Me.pnlBody.TabIndex = 1
            '
            ' lblError
            '
            Me.lblError.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38)
            Me.lblError.Location = New System.Drawing.Point(25, 295)
            Me.lblError.Name = "lblError"
            Me.lblError.Size = New System.Drawing.Size(470, 28)
            Me.lblError.TabIndex = 11
            '
            ' pnlNotice
            '
            Me.pnlNotice.BackColor = System.Drawing.Color.FromArgb(239, 246, 255)
            Me.pnlNotice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlNotice.Controls.Add(Me.lblNoticeText)
            Me.pnlNotice.Location = New System.Drawing.Point(25, 235)
            Me.pnlNotice.Name = "pnlNotice"
            Me.pnlNotice.Padding = New System.Windows.Forms.Padding(10, 6, 10, 6)
            Me.pnlNotice.Size = New System.Drawing.Size(470, 48)
            Me.pnlNotice.TabIndex = 10
            '
            ' lblNoticeText
            '
            Me.lblNoticeText.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblNoticeText.Font = New System.Drawing.Font("Segoe UI", 8.5!)
            Me.lblNoticeText.ForeColor = System.Drawing.Color.FromArgb(30, 58, 138)
            Me.lblNoticeText.Location = New System.Drawing.Point(10, 6)
            Me.lblNoticeText.Name = "lblNoticeText"
            Me.lblNoticeText.Size = New System.Drawing.Size(448, 34)
            Me.lblNoticeText.TabIndex = 0
            Me.lblNoticeText.Text = "ℹ️ Note: Enrolling creates the subscription in 'Pending Payment' status. Settle pa" &
        "yment in the Cashiering / Payment Module to activate gym access."
            '
            ' lblAmount
            '
            Me.lblAmount.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblAmount.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129)
            Me.lblAmount.Location = New System.Drawing.Point(135, 195)
            Me.lblAmount.Name = "lblAmount"
            Me.lblAmount.Size = New System.Drawing.Size(220, 24)
            Me.lblAmount.TabIndex = 9
            Me.lblAmount.Text = "₱0.00"
            '
            ' lblAmountTitle
            '
            Me.lblAmountTitle.AutoSize = True
            Me.lblAmountTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblAmountTitle.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblAmountTitle.Location = New System.Drawing.Point(25, 199)
            Me.lblAmountTitle.Name = "lblAmountTitle"
            Me.lblAmountTitle.Size = New System.Drawing.Size(89, 15)
            Me.lblAmountTitle.TabIndex = 8
            Me.lblAmountTitle.Text = "Total Plan Fee:"
            '
            ' lblEndDate
            '
            Me.lblEndDate.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
            Me.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
            Me.lblEndDate.Location = New System.Drawing.Point(265, 147)
            Me.lblEndDate.Name = "lblEndDate"
            Me.lblEndDate.Size = New System.Drawing.Size(230, 24)
            Me.lblEndDate.TabIndex = 7
            Me.lblEndDate.Text = "---"
            '
            ' lblEndDateTitle
            '
            Me.lblEndDateTitle.AutoSize = True
            Me.lblEndDateTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblEndDateTitle.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblEndDateTitle.Location = New System.Drawing.Point(265, 125)
            Me.lblEndDateTitle.Name = "lblEndDateTitle"
            Me.lblEndDateTitle.Size = New System.Drawing.Size(142, 15)
            Me.lblEndDateTitle.TabIndex = 6
            Me.lblEndDateTitle.Text = "Calculated Expiration Date"
            '
            ' dtpStartDate
            '
            Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpStartDate.Location = New System.Drawing.Point(25, 145)
            Me.dtpStartDate.Name = "dtpStartDate"
            Me.dtpStartDate.Size = New System.Drawing.Size(215, 23)
            Me.dtpStartDate.TabIndex = 5
            '
            ' lblStartDate
            '
            Me.lblStartDate.AutoSize = True
            Me.lblStartDate.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblStartDate.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblStartDate.Location = New System.Drawing.Point(25, 125)
            Me.lblStartDate.Name = "lblStartDate"
            Me.lblStartDate.Size = New System.Drawing.Size(73, 15)
            Me.lblStartDate.TabIndex = 4
            Me.lblStartDate.Text = "Start Date *"
            '
            ' cboPlans
            '
            Me.cboPlans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboPlans.FormattingEnabled = True
            Me.cboPlans.Location = New System.Drawing.Point(25, 90)
            Me.cboPlans.Name = "cboPlans"
            Me.cboPlans.Size = New System.Drawing.Size(470, 23)
            Me.cboPlans.TabIndex = 3
            '
            ' lblPlans
            '
            Me.lblPlans.AutoSize = True
            Me.lblPlans.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblPlans.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblPlans.Location = New System.Drawing.Point(25, 70)
            Me.lblPlans.Name = "lblPlans"
            Me.lblPlans.Size = New System.Drawing.Size(155, 15)
            Me.lblPlans.TabIndex = 2
            Me.lblPlans.Text = "Select Membership Plan *"
            '
            ' cboMembers
            '
            Me.cboMembers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboMembers.FormattingEnabled = True
            Me.cboMembers.Location = New System.Drawing.Point(25, 35)
            Me.cboMembers.Name = "cboMembers"
            Me.cboMembers.Size = New System.Drawing.Size(470, 23)
            Me.cboMembers.TabIndex = 1
            '
            ' lblMembers
            '
            Me.lblMembers.AutoSize = True
            Me.lblMembers.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblMembers.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblMembers.Location = New System.Drawing.Point(25, 15)
            Me.lblMembers.Name = "lblMembers"
            Me.lblMembers.Size = New System.Drawing.Size(98, 15)
            Me.lblMembers.TabIndex = 0
            Me.lblMembers.Text = "Select Member *"
            '
            ' MembershipForm
            '
            Me.AcceptButton = Me.btnSave
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(524, 461)
            Me.Controls.Add(Me.pnlBody)
            Me.Controls.Add(Me.pnlFooter)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "MembershipForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Enroll / Renew Membership Plan"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlFooter.ResumeLayout(False)
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            Me.pnlNotice.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnSave As System.Windows.Forms.Button
        Friend WithEvents pnlSpacer As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents lblMembers As System.Windows.Forms.Label
        Friend WithEvents cboMembers As System.Windows.Forms.ComboBox
        Friend WithEvents lblPlans As System.Windows.Forms.Label
        Friend WithEvents cboPlans As System.Windows.Forms.ComboBox
        Friend WithEvents lblStartDate As System.Windows.Forms.Label
        Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblEndDateTitle As System.Windows.Forms.Label
        Friend WithEvents lblEndDate As System.Windows.Forms.Label
        Friend WithEvents lblAmountTitle As System.Windows.Forms.Label
        Friend WithEvents lblAmount As System.Windows.Forms.Label
        Friend WithEvents pnlNotice As System.Windows.Forms.Panel
        Friend WithEvents lblNoticeText As System.Windows.Forms.Label
        Friend WithEvents lblError As System.Windows.Forms.Label
    End Class
End Namespace
