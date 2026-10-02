Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class MembershipPlanDialog
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
            Me.nudPrice = New System.Windows.Forms.NumericUpDown()
            Me.lblPrice = New System.Windows.Forms.Label()
            Me.cboDurationUnit = New System.Windows.Forms.ComboBox()
            Me.lblDurationUnit = New System.Windows.Forms.Label()
            Me.nudDuration = New System.Windows.Forms.NumericUpDown()
            Me.lblDuration = New System.Windows.Forms.Label()
            Me.txtDescription = New System.Windows.Forms.TextBox()
            Me.lblDescription = New System.Windows.Forms.Label()
            Me.txtName = New System.Windows.Forms.TextBox()
            Me.lblName = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlFooter.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            CType(Me.nudPrice, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.nudDuration, System.ComponentModel.ISupportInitialize).BeginInit()
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
            Me.pnlHeader.Size = New System.Drawing.Size(474, 55)
            Me.pnlHeader.TabIndex = 0
            '
            ' lblHeaderTitle
            '
            Me.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblHeaderTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblHeaderTitle.ForeColor = System.Drawing.Color.White
            Me.lblHeaderTitle.Location = New System.Drawing.Point(20, 14)
            Me.lblHeaderTitle.Name = "lblHeaderTitle"
            Me.lblHeaderTitle.Size = New System.Drawing.Size(434, 31)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "🏷️ New Membership Plan"
            Me.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            ' pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
            Me.pnlFooter.Controls.Add(Me.btnSave)
            Me.pnlFooter.Controls.Add(Me.pnlSpacer)
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 416)
            Me.pnlFooter.Name = "pnlFooter"
            Me.pnlFooter.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
            Me.pnlFooter.Size = New System.Drawing.Size(474, 65)
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
            Me.btnCancel.Location = New System.Drawing.Point(354, 12)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(100, 41)
            Me.btnCancel.TabIndex = 2
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            ' pnlSpacer
            '
            Me.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlSpacer.Location = New System.Drawing.Point(344, 12)
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
            Me.btnSave.Location = New System.Drawing.Point(204, 12)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(140, 41)
            Me.btnSave.TabIndex = 0
            Me.btnSave.Text = "Create Plan"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            ' pnlBody
            '
            Me.pnlBody.AutoScroll = True
            Me.pnlBody.Controls.Add(Me.lblError)
            Me.pnlBody.Controls.Add(Me.cboStatus)
            Me.pnlBody.Controls.Add(Me.lblStatus)
            Me.pnlBody.Controls.Add(Me.nudPrice)
            Me.pnlBody.Controls.Add(Me.lblPrice)
            Me.pnlBody.Controls.Add(Me.cboDurationUnit)
            Me.pnlBody.Controls.Add(Me.lblDurationUnit)
            Me.pnlBody.Controls.Add(Me.nudDuration)
            Me.pnlBody.Controls.Add(Me.lblDuration)
            Me.pnlBody.Controls.Add(Me.txtDescription)
            Me.pnlBody.Controls.Add(Me.lblDescription)
            Me.pnlBody.Controls.Add(Me.txtName)
            Me.pnlBody.Controls.Add(Me.lblName)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 55)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(25, 15, 25, 15)
            Me.pnlBody.Size = New System.Drawing.Size(474, 361)
            Me.pnlBody.TabIndex = 1
            '
            ' lblName
            '
            Me.lblName.AutoSize = True
            Me.lblName.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblName.Location = New System.Drawing.Point(25, 15)
            Me.lblName.Name = "lblName"
            Me.lblName.Size = New System.Drawing.Size(74, 15)
            Me.lblName.TabIndex = 0
            Me.lblName.Text = "Plan Name *"
            '
            ' txtName
            '
            Me.txtName.Location = New System.Drawing.Point(25, 35)
            Me.txtName.Name = "txtName"
            Me.txtName.Size = New System.Drawing.Size(420, 23)
            Me.txtName.TabIndex = 1
            '
            ' lblDescription
            '
            Me.lblDescription.AutoSize = True
            Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblDescription.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblDescription.Location = New System.Drawing.Point(25, 70)
            Me.lblDescription.Name = "lblDescription"
            Me.lblDescription.Size = New System.Drawing.Size(71, 15)
            Me.lblDescription.TabIndex = 2
            Me.lblDescription.Text = "Description"
            '
            ' txtDescription
            '
            Me.txtDescription.Location = New System.Drawing.Point(25, 90)
            Me.txtDescription.Multiline = True
            Me.txtDescription.Name = "txtDescription"
            Me.txtDescription.Size = New System.Drawing.Size(420, 50)
            Me.txtDescription.TabIndex = 3
            '
            ' lblDuration
            '
            Me.lblDuration.AutoSize = True
            Me.lblDuration.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblDuration.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblDuration.Location = New System.Drawing.Point(25, 150)
            Me.lblDuration.Name = "lblDuration"
            Me.lblDuration.Size = New System.Drawing.Size(63, 15)
            Me.lblDuration.TabIndex = 4
            Me.lblDuration.Text = "Duration *"
            '
            ' nudDuration
            '
            Me.nudDuration.Location = New System.Drawing.Point(25, 170)
            Me.nudDuration.Maximum = New Decimal(New Integer() {365, 0, 0, 0})
            Me.nudDuration.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.nudDuration.Name = "nudDuration"
            Me.nudDuration.Size = New System.Drawing.Size(195, 23)
            Me.nudDuration.TabIndex = 5
            Me.nudDuration.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            ' lblDurationUnit
            '
            Me.lblDurationUnit.AutoSize = True
            Me.lblDurationUnit.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblDurationUnit.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblDurationUnit.Location = New System.Drawing.Point(240, 150)
            Me.lblDurationUnit.Name = "lblDurationUnit"
            Me.lblDurationUnit.Size = New System.Drawing.Size(38, 15)
            Me.lblDurationUnit.TabIndex = 6
            Me.lblDurationUnit.Text = "Unit *"
            '
            ' cboDurationUnit
            '
            Me.cboDurationUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboDurationUnit.FormattingEnabled = True
            Me.cboDurationUnit.Items.AddRange(New Object() {"Days", "Months", "Years"})
            Me.cboDurationUnit.Location = New System.Drawing.Point(240, 170)
            Me.cboDurationUnit.Name = "cboDurationUnit"
            Me.cboDurationUnit.Size = New System.Drawing.Size(205, 23)
            Me.cboDurationUnit.TabIndex = 7
            '
            ' lblPrice
            '
            Me.lblPrice.AutoSize = True
            Me.lblPrice.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblPrice.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblPrice.Location = New System.Drawing.Point(25, 205)
            Me.lblPrice.Name = "lblPrice"
            Me.lblPrice.Size = New System.Drawing.Size(96, 15)
            Me.lblPrice.TabIndex = 8
            Me.lblPrice.Text = "Price (Amount) *"
            '
            ' nudPrice
            '
            Me.nudPrice.DecimalPlaces = 2
            Me.nudPrice.Location = New System.Drawing.Point(25, 225)
            Me.nudPrice.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
            Me.nudPrice.Name = "nudPrice"
            Me.nudPrice.Size = New System.Drawing.Size(195, 23)
            Me.nudPrice.TabIndex = 9
            Me.nudPrice.Value = New Decimal(New Integer() {1000, 0, 0, 0})
            '
            ' lblStatus
            '
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81)
            Me.lblStatus.Location = New System.Drawing.Point(240, 205)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(42, 15)
            Me.lblStatus.TabIndex = 10
            Me.lblStatus.Text = "Status"
            '
            ' cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.FormattingEnabled = True
            Me.cboStatus.Items.AddRange(New Object() {"Active", "Inactive"})
            Me.cboStatus.Location = New System.Drawing.Point(240, 225)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(205, 23)
            Me.cboStatus.TabIndex = 11
            '
            ' lblError
            '
            Me.lblError.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38)
            Me.lblError.Location = New System.Drawing.Point(25, 260)
            Me.lblError.Name = "lblError"
            Me.lblError.Size = New System.Drawing.Size(420, 25)
            Me.lblError.TabIndex = 12
            '
            ' MembershipPlanDialog
            '
            Me.AcceptButton = Me.btnSave
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(474, 481)
            Me.Controls.Add(Me.pnlBody)
            Me.Controls.Add(Me.pnlFooter)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "MembershipPlanDialog"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Create Membership Plan"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlFooter.ResumeLayout(False)
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            CType(Me.nudPrice, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.nudDuration, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnSave As System.Windows.Forms.Button
        Friend WithEvents pnlSpacer As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents lblName As System.Windows.Forms.Label
        Friend WithEvents txtName As System.Windows.Forms.TextBox
        Friend WithEvents lblDescription As System.Windows.Forms.Label
        Friend WithEvents txtDescription As System.Windows.Forms.TextBox
        Friend WithEvents lblDuration As System.Windows.Forms.Label
        Friend WithEvents nudDuration As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblDurationUnit As System.Windows.Forms.Label
        Friend WithEvents cboDurationUnit As System.Windows.Forms.ComboBox
        Friend WithEvents lblPrice As System.Windows.Forms.Label
        Friend WithEvents nudPrice As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblError As System.Windows.Forms.Label
    End Class
End Namespace
