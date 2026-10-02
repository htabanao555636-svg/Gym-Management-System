Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class AttendanceForm
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
            Me.components = New System.ComponentModel.Container()
            Dim dataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim dataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim dataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim dataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.pnlTop = New System.Windows.Forms.Panel()
            Me.pnlStatusRow = New System.Windows.Forms.Panel()
            Me.lblInsideCount = New System.Windows.Forms.Label()
            Me.lblMemberStatusInfo = New System.Windows.Forms.Label()
            Me.pnlControlRow = New System.Windows.Forms.Panel()
            Me.flpControls = New System.Windows.Forms.FlowLayoutPanel()
            Me.lblCardPrompt = New System.Windows.Forms.Label()
            Me.txtMemberSearch = New System.Windows.Forms.TextBox()
            Me.btnCheckIn = New System.Windows.Forms.Button()
            Me.btnCheckOut = New System.Windows.Forms.Button()
            Me.pnlHeaderRow = New System.Windows.Forms.Panel()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.tabControl = New System.Windows.Forms.TabControl()
            Me.tabInside = New System.Windows.Forms.TabPage()
            Me.dgvInside = New System.Windows.Forms.DataGridView()
            Me.pnlInsideToolbar = New System.Windows.Forms.FlowLayoutPanel()
            Me.btnRefreshInside = New System.Windows.Forms.Button()
            Me.lblLiveClock = New System.Windows.Forms.Label()
            Me.lblLiveNotice = New System.Windows.Forms.Label()
            Me.tabHistory = New System.Windows.Forms.TabPage()
            Me.dgvHistory = New System.Windows.Forms.DataGridView()
            Me.pnlHistFilter = New System.Windows.Forms.FlowLayoutPanel()
            Me.lblHDate = New System.Windows.Forms.Label()
            Me.dtpHistoryDate = New System.Windows.Forms.DateTimePicker()
            Me.btnFilterHistory = New System.Windows.Forms.Button()
            Me.lstSuggestions = New System.Windows.Forms.ListBox()
            Me._searchDebounceTimer = New System.Windows.Forms.Timer(Me.components)
            Me._liveClockTimer = New System.Windows.Forms.Timer(Me.components)
            Me._liveOccupancyTimer = New System.Windows.Forms.Timer(Me.components)
            Me.pnlTop.SuspendLayout()
            Me.pnlStatusRow.SuspendLayout()
            Me.pnlControlRow.SuspendLayout()
            Me.flpControls.SuspendLayout()
            Me.pnlHeaderRow.SuspendLayout()
            Me.tabControl.SuspendLayout()
            Me.tabInside.SuspendLayout()
            CType(Me.dgvInside, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlInsideToolbar.SuspendLayout()
            Me.tabHistory.SuspendLayout()
            CType(Me.dgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlHistFilter.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlTop
            '
            Me.pnlTop.BackColor = System.Drawing.Color.White
            Me.pnlTop.Controls.Add(Me.pnlStatusRow)
            Me.pnlTop.Controls.Add(Me.pnlControlRow)
            Me.pnlTop.Controls.Add(Me.pnlHeaderRow)
            Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTop.Location = New System.Drawing.Point(0, 0)
            Me.pnlTop.Name = "pnlTop"
            Me.pnlTop.Padding = New System.Windows.Forms.Padding(20, 10, 20, 8)
            Me.pnlTop.Size = New System.Drawing.Size(1000, 135)
            Me.pnlTop.TabIndex = 0
            '
            ' pnlStatusRow
            '
            Me.pnlStatusRow.BackColor = System.Drawing.Color.White
            Me.pnlStatusRow.Controls.Add(Me.lblInsideCount)
            Me.pnlStatusRow.Controls.Add(Me.lblMemberStatusInfo)
            Me.pnlStatusRow.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlStatusRow.Location = New System.Drawing.Point(20, 91)
            Me.pnlStatusRow.Name = "pnlStatusRow"
            Me.pnlStatusRow.Size = New System.Drawing.Size(960, 36)
            Me.pnlStatusRow.TabIndex = 2
            '
            ' lblInsideCount
            '
            Me.lblInsideCount.AutoSize = True
            Me.lblInsideCount.Dock = System.Windows.Forms.DockStyle.Right
            Me.lblInsideCount.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblInsideCount.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
            Me.lblInsideCount.Location = New System.Drawing.Point(836, 0)
            Me.lblInsideCount.Name = "lblInsideCount"
            Me.lblInsideCount.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
            Me.lblInsideCount.Size = New System.Drawing.Size(124, 23)
            Me.lblInsideCount.TabIndex = 1
            Me.lblInsideCount.Text = "Currently Inside: 0"
            Me.lblInsideCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' lblMemberStatusInfo
            '
            Me.lblMemberStatusInfo.AutoSize = True
            Me.lblMemberStatusInfo.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblMemberStatusInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
            Me.lblMemberStatusInfo.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128)
            Me.lblMemberStatusInfo.Location = New System.Drawing.Point(0, 0)
            Me.lblMemberStatusInfo.Name = "lblMemberStatusInfo"
            Me.lblMemberStatusInfo.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
            Me.lblMemberStatusInfo.Size = New System.Drawing.Size(298, 21)
            Me.lblMemberStatusInfo.TabIndex = 0
            Me.lblMemberStatusInfo.Text = "Type member name or scan card number to check in/out."
            Me.lblMemberStatusInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            ' pnlControlRow
            '
            Me.pnlControlRow.BackColor = System.Drawing.Color.White
            Me.pnlControlRow.Controls.Add(Me.flpControls)
            Me.pnlControlRow.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlControlRow.Location = New System.Drawing.Point(20, 42)
            Me.pnlControlRow.Name = "pnlControlRow"
            Me.pnlControlRow.Size = New System.Drawing.Size(960, 44)
            Me.pnlControlRow.TabIndex = 1
            '
            ' flpControls
            '
            Me.flpControls.AutoSize = True
            Me.flpControls.Controls.Add(Me.lblCardPrompt)
            Me.flpControls.Controls.Add(Me.txtMemberSearch)
            Me.flpControls.Controls.Add(Me.btnCheckIn)
            Me.flpControls.Controls.Add(Me.btnCheckOut)
            Me.flpControls.Dock = System.Windows.Forms.DockStyle.Fill
            Me.flpControls.Location = New System.Drawing.Point(0, 0)
            Me.flpControls.Name = "flpControls"
            Me.flpControls.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
            Me.flpControls.Size = New System.Drawing.Size(960, 44)
            Me.flpControls.TabIndex = 0
            Me.flpControls.WrapContents = False
            '
            ' lblCardPrompt
            '
            Me.lblCardPrompt.AutoSize = True
            Me.lblCardPrompt.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblCardPrompt.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99)
            Me.lblCardPrompt.Location = New System.Drawing.Point(0, 10)
            Me.lblCardPrompt.Margin = New System.Windows.Forms.Padding(0, 6, 6, 0)
            Me.lblCardPrompt.Name = "lblCardPrompt"
            Me.lblCardPrompt.Size = New System.Drawing.Size(103, 15)
            Me.lblCardPrompt.TabIndex = 0
            Me.lblCardPrompt.Text = "Card No / Name:"
            '
            ' txtMemberSearch
            '
            Me.txtMemberSearch.Font = New System.Drawing.Font("Segoe UI", 10.0!)
            Me.txtMemberSearch.Location = New System.Drawing.Point(109, 6)
            Me.txtMemberSearch.Margin = New System.Windows.Forms.Padding(0, 2, 10, 0)
            Me.txtMemberSearch.Name = "txtMemberSearch"
            Me.txtMemberSearch.PlaceholderText = "Type member name or scan card no..."
            Me.txtMemberSearch.Size = New System.Drawing.Size(280, 25)
            Me.txtMemberSearch.TabIndex = 1
            '
            ' btnCheckIn
            '
            Me.btnCheckIn.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
            Me.btnCheckIn.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCheckIn.Enabled = False
            Me.btnCheckIn.FlatAppearance.BorderSize = 0
            Me.btnCheckIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCheckIn.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.btnCheckIn.ForeColor = System.Drawing.Color.White
            Me.btnCheckIn.Location = New System.Drawing.Point(399, 5)
            Me.btnCheckIn.Margin = New System.Windows.Forms.Padding(0, 1, 8, 0)
            Me.btnCheckIn.Name = "btnCheckIn"
            Me.btnCheckIn.Size = New System.Drawing.Size(130, 30)
            Me.btnCheckIn.TabIndex = 2
            Me.btnCheckIn.Text = "➡️ CHECK IN"
            Me.btnCheckIn.UseVisualStyleBackColor = False
            '
            ' btnCheckOut
            '
            Me.btnCheckOut.BackColor = System.Drawing.Color.FromArgb(239, 68, 68)
            Me.btnCheckOut.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCheckOut.Enabled = False
            Me.btnCheckOut.FlatAppearance.BorderSize = 0
            Me.btnCheckOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCheckOut.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.btnCheckOut.ForeColor = System.Drawing.Color.White
            Me.btnCheckOut.Location = New System.Drawing.Point(537, 5)
            Me.btnCheckOut.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
            Me.btnCheckOut.Name = "btnCheckOut"
            Me.btnCheckOut.Size = New System.Drawing.Size(130, 30)
            Me.btnCheckOut.TabIndex = 3
            Me.btnCheckOut.Text = "⬅️ CHECK OUT"
            Me.btnCheckOut.UseVisualStyleBackColor = False
            '
            ' pnlHeaderRow
            '
            Me.pnlHeaderRow.BackColor = System.Drawing.Color.White
            Me.pnlHeaderRow.Controls.Add(Me.lblTitle)
            Me.pnlHeaderRow.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeaderRow.Location = New System.Drawing.Point(20, 10)
            Me.pnlHeaderRow.Name = "pnlHeaderRow"
            Me.pnlHeaderRow.Size = New System.Drawing.Size(960, 32)
            Me.pnlHeaderRow.TabIndex = 0
            '
            ' lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39)
            Me.lblTitle.Location = New System.Drawing.Point(0, 0)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(434, 21)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "🏋 Member Attendance Check-In / Check-Out Station"
            Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            ' tabControl
            '
            Me.tabControl.Controls.Add(Me.tabInside)
            Me.tabControl.Controls.Add(Me.tabHistory)
            Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabControl.Font = New System.Drawing.Font("Segoe UI", 9.5!)
            Me.tabControl.Location = New System.Drawing.Point(0, 135)
            Me.tabControl.Name = "tabControl"
            Me.tabControl.SelectedIndex = 0
            Me.tabControl.Size = New System.Drawing.Size(1000, 565)
            Me.tabControl.TabIndex = 1
            '
            ' tabInside
            '
            Me.tabInside.Controls.Add(Me.dgvInside)
            Me.tabInside.Controls.Add(Me.pnlInsideToolbar)
            Me.tabInside.Location = New System.Drawing.Point(4, 26)
            Me.tabInside.Name = "tabInside"
            Me.tabInside.Padding = New System.Windows.Forms.Padding(3)
            Me.tabInside.Size = New System.Drawing.Size(992, 535)
            Me.tabInside.TabIndex = 0
            Me.tabInside.Text = "📍 Members Currently Inside Gym"
            Me.tabInside.UseVisualStyleBackColor = True
            '
            ' dgvInside
            '
            Me.dgvInside.AllowUserToAddRows = False
            Me.dgvInside.AllowUserToDeleteRows = False
            Me.dgvInside.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvInside.BackgroundColor = System.Drawing.Color.White
            Me.dgvInside.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.dgvInside.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(31, 41, 55)
            dataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.5!)
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvInside.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1
            Me.dgvInside.ColumnHeadersHeight = 35
            Me.dgvInside.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvInside.EnableHeadersVisualStyles = False
            Me.dgvInside.GridColor = System.Drawing.Color.FromArgb(243, 244, 246)
            Me.dgvInside.Location = New System.Drawing.Point(3, 47)
            Me.dgvInside.MultiSelect = False
            Me.dgvInside.Name = "dgvInside"
            Me.dgvInside.ReadOnly = True
            Me.dgvInside.RowHeadersVisible = False
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55)
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254)
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(30, 58, 138)
            Me.dgvInside.RowsDefaultCellStyle = dataGridViewCellStyle2
            Me.dgvInside.RowTemplate.Height = 34
            Me.dgvInside.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvInside.Size = New System.Drawing.Size(986, 485)
            Me.dgvInside.TabIndex = 1
            '
            ' pnlInsideToolbar
            '
            Me.pnlInsideToolbar.BackColor = System.Drawing.Color.White
            Me.pnlInsideToolbar.Controls.Add(Me.btnRefreshInside)
            Me.pnlInsideToolbar.Controls.Add(Me.lblLiveClock)
            Me.pnlInsideToolbar.Controls.Add(Me.lblLiveNotice)
            Me.pnlInsideToolbar.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlInsideToolbar.Location = New System.Drawing.Point(3, 3)
            Me.pnlInsideToolbar.Name = "pnlInsideToolbar"
            Me.pnlInsideToolbar.Padding = New System.Windows.Forms.Padding(15, 8, 15, 6)
            Me.pnlInsideToolbar.Size = New System.Drawing.Size(986, 44)
            Me.pnlInsideToolbar.TabIndex = 0
            Me.pnlInsideToolbar.WrapContents = False
            '
            ' btnRefreshInside
            '
            Me.btnRefreshInside.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnRefreshInside.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219)
            Me.btnRefreshInside.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRefreshInside.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.btnRefreshInside.Location = New System.Drawing.Point(15, 8)
            Me.btnRefreshInside.Margin = New System.Windows.Forms.Padding(0, 0, 15, 0)
            Me.btnRefreshInside.Name = "btnRefreshInside"
            Me.btnRefreshInside.Size = New System.Drawing.Size(160, 28)
            Me.btnRefreshInside.TabIndex = 0
            Me.btnRefreshInside.Text = "🔄 Refresh Occupancy"
            Me.btnRefreshInside.UseVisualStyleBackColor = True
            '
            ' lblLiveClock
            '
            Me.lblLiveClock.AutoSize = True
            Me.lblLiveClock.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblLiveClock.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55)
            Me.lblLiveClock.Location = New System.Drawing.Point(190, 12)
            Me.lblLiveClock.Margin = New System.Windows.Forms.Padding(0, 4, 15, 0)
            Me.lblLiveClock.Name = "lblLiveClock"
            Me.lblLiveClock.Size = New System.Drawing.Size(166, 17)
            Me.lblLiveClock.TabIndex = 1
            Me.lblLiveClock.Text = "🕒 Current Time: --:--:-- --"
            '
            ' lblLiveNotice
            '
            Me.lblLiveNotice.AutoSize = True
            Me.lblLiveNotice.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Italic)
            Me.lblLiveNotice.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128)
            Me.lblLiveNotice.Location = New System.Drawing.Point(371, 13)
            Me.lblLiveNotice.Margin = New System.Windows.Forms.Padding(0, 5, 0, 0)
            Me.lblLiveNotice.Name = "lblLiveNotice"
            Me.lblLiveNotice.Size = New System.Drawing.Size(228, 15)
            Me.lblLiveNotice.TabIndex = 2
            Me.lblLiveNotice.Text = "ℹ️ Time Inside updates live automatically"
            '
            ' tabHistory
            '
            Me.tabHistory.Controls.Add(Me.dgvHistory)
            Me.tabHistory.Controls.Add(Me.pnlHistFilter)
            Me.tabHistory.Location = New System.Drawing.Point(4, 26)
            Me.tabHistory.Name = "tabHistory"
            Me.tabHistory.Padding = New System.Windows.Forms.Padding(3)
            Me.tabHistory.Size = New System.Drawing.Size(992, 535)
            Me.tabHistory.TabIndex = 1
            Me.tabHistory.Text = "📅 Attendance History"
            Me.tabHistory.UseVisualStyleBackColor = True
            '
            ' dgvHistory
            '
            Me.dgvHistory.AllowUserToAddRows = False
            Me.dgvHistory.AllowUserToDeleteRows = False
            Me.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvHistory.BackgroundColor = System.Drawing.Color.White
            Me.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.dgvHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(31, 41, 55)
            dataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.5!)
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3
            Me.dgvHistory.ColumnHeadersHeight = 35
            Me.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvHistory.EnableHeadersVisualStyles = False
            Me.dgvHistory.GridColor = System.Drawing.Color.FromArgb(243, 244, 246)
            Me.dgvHistory.Location = New System.Drawing.Point(3, 51)
            Me.dgvHistory.MultiSelect = False
            Me.dgvHistory.Name = "dgvHistory"
            Me.dgvHistory.ReadOnly = True
            Me.dgvHistory.RowHeadersVisible = False
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55)
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254)
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(30, 58, 138)
            Me.dgvHistory.RowsDefaultCellStyle = dataGridViewCellStyle4
            Me.dgvHistory.RowTemplate.Height = 32
            Me.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvHistory.Size = New System.Drawing.Size(986, 481)
            Me.dgvHistory.TabIndex = 1
            '
            ' pnlHistFilter
            '
            Me.pnlHistFilter.BackColor = System.Drawing.Color.White
            Me.pnlHistFilter.Controls.Add(Me.lblHDate)
            Me.pnlHistFilter.Controls.Add(Me.dtpHistoryDate)
            Me.pnlHistFilter.Controls.Add(Me.btnFilterHistory)
            Me.pnlHistFilter.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHistFilter.Location = New System.Drawing.Point(3, 3)
            Me.pnlHistFilter.Name = "pnlHistFilter"
            Me.pnlHistFilter.Padding = New System.Windows.Forms.Padding(15, 10, 15, 10)
            Me.pnlHistFilter.Size = New System.Drawing.Size(986, 48)
            Me.pnlHistFilter.TabIndex = 0
            Me.pnlHistFilter.WrapContents = False
            '
            ' lblHDate
            '
            Me.lblHDate.AutoSize = True
            Me.lblHDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblHDate.Location = New System.Drawing.Point(15, 15)
            Me.lblHDate.Margin = New System.Windows.Forms.Padding(0, 5, 8, 0)
            Me.lblHDate.Name = "lblHDate"
            Me.lblHDate.Size = New System.Drawing.Size(73, 15)
            Me.lblHDate.TabIndex = 0
            Me.lblHDate.Text = "Select Date:"
            '
            ' dtpHistoryDate
            '
            Me.dtpHistoryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpHistoryDate.Location = New System.Drawing.Point(96, 11)
            Me.dtpHistoryDate.Margin = New System.Windows.Forms.Padding(0, 1, 10, 0)
            Me.dtpHistoryDate.Name = "dtpHistoryDate"
            Me.dtpHistoryDate.Size = New System.Drawing.Size(130, 24)
            Me.dtpHistoryDate.TabIndex = 1
            '
            ' btnFilterHistory
            '
            Me.btnFilterHistory.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnFilterHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219)
            Me.btnFilterHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnFilterHistory.Location = New System.Drawing.Point(236, 10)
            Me.btnFilterHistory.Margin = New System.Windows.Forms.Padding(0)
            Me.btnFilterHistory.Name = "btnFilterHistory"
            Me.btnFilterHistory.Size = New System.Drawing.Size(110, 28)
            Me.btnFilterHistory.TabIndex = 2
            Me.btnFilterHistory.Text = "View History"
            Me.btnFilterHistory.UseVisualStyleBackColor = True
            '
            ' lstSuggestions
            '
            Me.lstSuggestions.BackColor = System.Drawing.Color.White
            Me.lstSuggestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lstSuggestions.Cursor = System.Windows.Forms.Cursors.Hand
            Me.lstSuggestions.Font = New System.Drawing.Font("Segoe UI", 9.5!)
            Me.lstSuggestions.ItemHeight = 26
            Me.lstSuggestions.Location = New System.Drawing.Point(129, 90)
            Me.lstSuggestions.Name = "lstSuggestions"
            Me.lstSuggestions.Size = New System.Drawing.Size(280, 80)
            Me.lstSuggestions.TabIndex = 2
            Me.lstSuggestions.Visible = False
            '
            ' _searchDebounceTimer
            '
            Me._searchDebounceTimer.Interval = 160
            '
            ' _liveClockTimer
            '
            Me._liveClockTimer.Interval = 1000
            '
            ' _liveOccupancyTimer
            '
            Me._liveOccupancyTimer.Interval = 10000
            '
            ' AttendanceForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
            Me.ClientSize = New System.Drawing.Size(1000, 700)
            Me.Controls.Add(Me.lstSuggestions)
            Me.Controls.Add(Me.tabControl)
            Me.Controls.Add(Me.pnlTop)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "AttendanceForm"
            Me.Text = "Attendance & Occupancy"
            Me.pnlTop.ResumeLayout(False)
            Me.pnlStatusRow.ResumeLayout(False)
            Me.pnlStatusRow.PerformLayout()
            Me.pnlControlRow.ResumeLayout(False)
            Me.pnlControlRow.PerformLayout()
            Me.flpControls.ResumeLayout(False)
            Me.flpControls.PerformLayout()
            Me.pnlHeaderRow.ResumeLayout(False)
            Me.pnlHeaderRow.PerformLayout()
            Me.tabControl.ResumeLayout(False)
            Me.tabInside.ResumeLayout(False)
            CType(Me.dgvInside, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlInsideToolbar.ResumeLayout(False)
            Me.pnlInsideToolbar.PerformLayout()
            Me.tabHistory.ResumeLayout(False)
            CType(Me.dgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlHistFilter.ResumeLayout(False)
            Me.pnlHistFilter.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTop As System.Windows.Forms.Panel
        Friend WithEvents pnlHeaderRow As System.Windows.Forms.Panel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents pnlControlRow As System.Windows.Forms.Panel
        Friend WithEvents flpControls As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents lblCardPrompt As System.Windows.Forms.Label
        Friend WithEvents txtMemberSearch As System.Windows.Forms.TextBox
        Friend WithEvents btnCheckIn As System.Windows.Forms.Button
        Friend WithEvents btnCheckOut As System.Windows.Forms.Button
        Friend WithEvents pnlStatusRow As System.Windows.Forms.Panel
        Friend WithEvents lblMemberStatusInfo As System.Windows.Forms.Label
        Friend WithEvents lblInsideCount As System.Windows.Forms.Label
        Friend WithEvents tabControl As System.Windows.Forms.TabControl
        Friend WithEvents tabInside As System.Windows.Forms.TabPage
        Friend WithEvents pnlInsideToolbar As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents btnRefreshInside As System.Windows.Forms.Button
        Friend WithEvents lblLiveClock As System.Windows.Forms.Label
        Friend WithEvents lblLiveNotice As System.Windows.Forms.Label
        Friend WithEvents dgvInside As System.Windows.Forms.DataGridView
        Friend WithEvents tabHistory As System.Windows.Forms.TabPage
        Friend WithEvents pnlHistFilter As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents lblHDate As System.Windows.Forms.Label
        Friend WithEvents dtpHistoryDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents btnFilterHistory As System.Windows.Forms.Button
        Friend WithEvents dgvHistory As System.Windows.Forms.DataGridView
        Friend WithEvents lstSuggestions As System.Windows.Forms.ListBox
        Friend WithEvents _searchDebounceTimer As System.Windows.Forms.Timer
        Friend WithEvents _liveClockTimer As System.Windows.Forms.Timer
        Friend WithEvents _liveOccupancyTimer As System.Windows.Forms.Timer
    End Class
End Namespace
