Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Member Attendance check-in/out station and occupancy tracking.
    ''' Compatible with Visual Studio Windows Forms Designer.
    ''' </summary>
    Partial Public Class AttendanceForm
        Inherits Form

        Private ReadOnly _attendanceController As New AttendanceController()
        Private ReadOnly _memberController As New MemberController()
        Private ReadOnly _membershipController As New MembershipController()

        Private _verifiedMember As Member
        Private _verifiedMembership As Membership
        Private _verifiedActiveSession As Attendance
        Private _currentInsideList As New List(Of Attendance)()
        Private _isUpdatingSearchText As Boolean = False

        Public Sub New()
            InitializeComponent()
            SetupRuntimeBehaviors()
            LoadCurrentlyInside()
            LoadHistory()
        End Sub

        Private Sub SetupRuntimeBehaviors()
            AddHandler txtMemberSearch.TextChanged, AddressOf HandleSearchTextChanged
            AddHandler txtMemberSearch.KeyDown, AddressOf HandleSearchKeyDown
            AddHandler btnCheckIn.Click, AddressOf HandleCheckIn
            AddHandler btnCheckOut.Click, AddressOf HandleCheckOut
            AddHandler lstSuggestions.Click, AddressOf HandleSuggestionClicked
            AddHandler lstSuggestions.KeyDown, AddressOf HandleSuggestionKeyDown
            AddHandler _searchDebounceTimer.Tick, AddressOf OnSearchDebounceTick
            AddHandler btnRefreshInside.Click, Sub() LoadCurrentlyInside()
            AddHandler btnFilterHistory.Click, Sub() LoadHistory()

            AddHandler dgvInside.SelectionChanged, Sub()
                                                       If _verifiedMember Is Nothing AndAlso dgvInside.SelectedRows.Count > 0 Then
                                                           btnCheckOut.Enabled = True
                                                       End If
                                                   End Sub

            AddHandler _liveClockTimer.Tick, Sub()
                                                 If lblLiveClock IsNot Nothing Then
                                                     lblLiveClock.Text = $"🕒 Current Time: {DateTime.Now:hh:mm:ss tt}"
                                                 End If

                                                 If dgvInside IsNot Nothing AndAlso dgvInside.Rows.Count > 0 AndAlso _currentInsideList IsNot Nothing AndAlso _currentInsideList.Count > 0 Then
                                                     For Each row As DataGridViewRow In dgvInside.Rows
                                                         If row.Cells("ID").Value IsNot Nothing Then
                                                             Dim attId As Integer = Convert.ToInt32(row.Cells("ID").Value)
                                                             Dim item = _currentInsideList.FirstOrDefault(Function(a) a.AttendanceID = attId)
                                                             If item IsNot Nothing Then
                                                                 row.Cells("Time Inside").Value = item.DurationText
                                                             End If
                                                         End If
                                                     Next
                                                 End If

                                                 If _verifiedMember IsNot Nothing AndAlso _verifiedActiveSession IsNot Nothing Then
                                                     Dim planName As String = If(_verifiedMembership IsNot Nothing, _verifiedMembership.PlanName, "Gym Access")
                                                     lblMemberStatusInfo.Text = $"ℹ️ {_verifiedMember.FullName} ({planName}) is CURRENTLY INSIDE — Checked In: {_verifiedActiveSession.CheckIn:hh:mm:ss tt} | Time Inside: {_verifiedActiveSession.DurationText}. Ready for check-out."
                                                 End If
                                             End Sub
            _liveClockTimer.Start()

            AddHandler _liveOccupancyTimer.Tick, Sub()
                                                     If tabControl.SelectedTab Is tabInside Then
                                                         LoadCurrentlyInside()
                                                     End If
                                                 End Sub
            _liveOccupancyTimer.Start()

            AddHandler pnlTop.MouseDown, Sub() lstSuggestions.Visible = False
            AddHandler tabControl.MouseDown, Sub() lstSuggestions.Visible = False
            AddHandler Me.MouseDown, Sub() lstSuggestions.Visible = False
            AddHandler Me.Resize, Sub() If lstSuggestions.Visible Then PositionSuggestionList()
        End Sub

        Private Sub InitializeComponent()

        End Sub

        Private Function CreateStyledGrid() As DataGridView
            Dim dgv As New DataGridView() With {
                .Dock = DockStyle.Fill,
                .BackgroundColor = Color.White,
                .BorderStyle = BorderStyle.None,
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                .GridColor = Color.FromArgb(243, 244, 246),
                .RowHeadersVisible = False,
                .AllowUserToAddRows = False,
                .AllowUserToDeleteRows = False,
                .ReadOnly = True,
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .MultiSelect = False,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                .Font = New Font("Segoe UI", 9.0F),
                .RowTemplate = New DataGridViewRow() With {.Height = 32}
            }
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55)
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            dgv.ColumnHeadersHeight = 35
            dgv.EnableHeadersVisualStyles = False
            Return dgv
        End Function

        Private Class MemberSuggestionItem
            Public Property Member As Member

            Public Sub New(mem As Member)
                Me.Member = mem
            End Sub

            Public Overrides Function ToString() As String
                If Member Is Nothing Then Return String.Empty
                Return $"👤 {Member.FullName}  [{Member.MembershipNumber}]  •  {Member.Status}"
            End Function
        End Class

        Private Sub PositionSuggestionList()
            If txtMemberSearch Is Nothing OrElse Not txtMemberSearch.IsHandleCreated Then Return
            Dim screenPt = txtMemberSearch.PointToScreen(New Point(0, txtMemberSearch.Height + 2))
            Dim clientPt = Me.PointToClient(screenPt)
            lstSuggestions.Location = clientPt
            lstSuggestions.Width = Math.Max(txtMemberSearch.Width + 120, 420)
            Dim count = Math.Min(lstSuggestions.Items.Count, 6)
            lstSuggestions.Height = Math.Max(35, count * 26 + 6)
            lstSuggestions.BringToFront()
        End Sub

        Private Sub HandleSearchTextChanged(sender As Object, e As EventArgs)
            If _isUpdatingSearchText Then Return
            _searchDebounceTimer.Stop()
            _searchDebounceTimer.Start()
        End Sub

        Private Sub OnSearchDebounceTick(sender As Object, e As EventArgs)
            _searchDebounceTimer.Stop()
            PerformSearchAndAutoVerify()
        End Sub

        Private Sub HandleSearchKeyDown(sender As Object, e As KeyEventArgs)
            If e.KeyCode = Keys.Down Then
                If lstSuggestions.Visible AndAlso lstSuggestions.Items.Count > 0 Then
                    If lstSuggestions.SelectedIndex < lstSuggestions.Items.Count - 1 Then
                        lstSuggestions.SelectedIndex += 1
                    End If
                    e.SuppressKeyPress = True
                End If
            ElseIf e.KeyCode = Keys.Up Then
                If lstSuggestions.Visible AndAlso lstSuggestions.Items.Count > 0 Then
                    If lstSuggestions.SelectedIndex > 0 Then
                        lstSuggestions.SelectedIndex -= 1
                    End If
                    e.SuppressKeyPress = True
                End If
            ElseIf e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                _searchDebounceTimer.Stop()
                If lstSuggestions.Visible AndAlso lstSuggestions.SelectedItem IsNot Nothing Then
                    Dim item = TryCast(lstSuggestions.SelectedItem, MemberSuggestionItem)
                    If item IsNot Nothing AndAlso item.Member IsNot Nothing Then
                        SelectAndVerifyMember(item.Member, updateSearchText:=True)
                        Return
                    End If
                End If
                PerformSearchAndAutoVerify()
            ElseIf e.KeyCode = Keys.Escape Then
                lstSuggestions.Visible = False
                e.SuppressKeyPress = True
            End If
        End Sub

        Private Sub HandleSuggestionClicked(sender As Object, e As EventArgs)
            If lstSuggestions.SelectedItem IsNot Nothing Then
                Dim item = TryCast(lstSuggestions.SelectedItem, MemberSuggestionItem)
                If item IsNot Nothing AndAlso item.Member IsNot Nothing Then
                    SelectAndVerifyMember(item.Member, updateSearchText:=True)
                End If
            End If
        End Sub

        Private Sub HandleSuggestionKeyDown(sender As Object, e As KeyEventArgs)
            If e.KeyCode = Keys.Enter Then
                HandleSuggestionClicked(sender, EventArgs.Empty)
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Escape Then
                lstSuggestions.Visible = False
                e.SuppressKeyPress = True
            End If
        End Sub

        Private Sub PerformSearchAndAutoVerify()
            Dim query As String = txtMemberSearch.Text.Trim()
            If String.IsNullOrWhiteSpace(query) Then
                _verifiedMember = Nothing
                _verifiedMembership = Nothing
                _verifiedActiveSession = Nothing
                lstSuggestions.Visible = False
                btnCheckIn.Enabled = False
                btnCheckOut.Enabled = (dgvInside.SelectedRows.Count > 0)
                lblMemberStatusInfo.ForeColor = Color.FromArgb(107, 114, 128)
                lblMemberStatusInfo.Text = "Type member name or scan card number to check in/out."
                Return
            End If

            ' 1. Check direct exact card number match
            Dim exactMember = _memberController.GetMemberByNumber(query)
            If exactMember IsNot Nothing Then
                lstSuggestions.Visible = False
                SelectAndVerifyMember(exactMember, updateSearchText:=False)
                Return
            End If

            ' 2. Search matches by name / number / phone / email
            Dim matches = _memberController.GetMembers(query)

            If matches.Count = 0 Then
                _verifiedMember = Nothing
                _verifiedMembership = Nothing
                _verifiedActiveSession = Nothing
                lstSuggestions.Visible = False
                btnCheckIn.Enabled = False
                btnCheckOut.Enabled = False
                lblMemberStatusInfo.ForeColor = Color.FromArgb(220, 38, 38)
                lblMemberStatusInfo.Text = "❌ Member: No record found. Please verify member card or name."
                Return
            End If

            If matches.Count = 1 Then
                lstSuggestions.Visible = False
                SelectAndVerifyMember(matches(0), updateSearchText:=False)
                Return
            End If

            ' Multiple matches found (e.g. "Juan" -> Juan Cruz and Juan Santos)
            ' Check if query is an exact full name match
            Dim exactNames = matches.Where(Function(m) String.Equals(m.FullName, query, StringComparison.OrdinalIgnoreCase)).ToList()
            If exactNames.Count = 1 Then
                lstSuggestions.Visible = False
                SelectAndVerifyMember(exactNames(0), updateSearchText:=False)
                Return
            End If

            ' Show suggestion list for disambiguation
            lstSuggestions.BeginUpdate()
            lstSuggestions.Items.Clear()
            For Each m In matches
                lstSuggestions.Items.Add(New MemberSuggestionItem(m))
            Next
            lstSuggestions.SelectedIndex = 0
            lstSuggestions.EndUpdate()

            PositionSuggestionList()
            lstSuggestions.Visible = True

            _verifiedMember = Nothing
            _verifiedMembership = Nothing
            _verifiedActiveSession = Nothing
            btnCheckIn.Enabled = False
            btnCheckOut.Enabled = False
            lblMemberStatusInfo.ForeColor = Color.FromArgb(37, 99, 235)
            lblMemberStatusInfo.Text = $"👥 {matches.Count} members found. Click or use arrow keys + Enter to select a member:"
        End Sub

        Private Sub SelectAndVerifyMember(member As Member, updateSearchText As Boolean)
            If member Is Nothing Then Return

            _verifiedMember = member
            lstSuggestions.Visible = False

            If updateSearchText Then
                _isUpdatingSearchText = True
                txtMemberSearch.Text = $"{member.FullName} ({member.MembershipNumber})"
                txtMemberSearch.SelectionStart = txtMemberSearch.Text.Length
                _isUpdatingSearchText = False
            End If

            _verifiedMembership = _membershipController.GetActiveForMember(_verifiedMember.MemberID)
            Dim isInside As Boolean = _attendanceController.IsCurrentlyInside(_verifiedMember.MemberID)

            If Not _verifiedMember.IsActive Then
                _verifiedActiveSession = Nothing
                lblMemberStatusInfo.ForeColor = Color.FromArgb(220, 38, 38)
                lblMemberStatusInfo.Text = $"❌ Cannot check in: Member account is {_verifiedMember.Status}."
                btnCheckIn.Enabled = False
                btnCheckOut.Enabled = isInside
            ElseIf isInside Then
                lblMemberStatusInfo.ForeColor = Color.FromArgb(37, 99, 235)
                Dim planName As String = If(_verifiedMembership IsNot Nothing, _verifiedMembership.PlanName, "Gym Access")
                _verifiedActiveSession = _attendanceController.GetCurrentSession(_verifiedMember.MemberID)
                If _verifiedActiveSession IsNot Nothing Then
                    lblMemberStatusInfo.Text = $"ℹ️ {_verifiedMember.FullName} ({planName}) is CURRENTLY INSIDE — Checked In: {_verifiedActiveSession.CheckIn:hh:mm:ss tt} | Time Inside: {_verifiedActiveSession.DurationText}. Ready for check-out."
                Else
                    lblMemberStatusInfo.Text = $"ℹ️ {_verifiedMember.FullName} ({planName}) is CURRENTLY INSIDE the gym. Ready for check-out."
                End If
                btnCheckIn.Enabled = False
                btnCheckOut.Enabled = True
            ElseIf _verifiedMembership IsNot Nothing Then
                _verifiedActiveSession = Nothing
                lblMemberStatusInfo.ForeColor = Color.FromArgb(16, 185, 129)
                lblMemberStatusInfo.Text = $"✅ VERIFIED: {_verifiedMember.FullName} | Plan: {_verifiedMembership.PlanName} (Expires: {_verifiedMembership.EndDate:yyyy-MM-dd}). Ready to check in."
                btnCheckIn.Enabled = True
                btnCheckOut.Enabled = False
            Else
                _verifiedActiveSession = Nothing
                Dim pendingMs = _membershipController.GetPendingForMember(_verifiedMember.MemberID)
                If pendingMs IsNot Nothing Then
                    lblMemberStatusInfo.ForeColor = Color.FromArgb(217, 119, 6)
                    lblMemberStatusInfo.Text = $"⚠️ Cannot check in: {_verifiedMember.FullName}'s subscription '{pendingMs.PlanName}' is Pending Payment. Please settle payment at cashier first."
                Else
                    lblMemberStatusInfo.ForeColor = Color.FromArgb(220, 38, 38)
                    lblMemberStatusInfo.Text = $"⚠️ {_verifiedMember.FullName} does NOT have an active membership plan! (Please enroll or renew plan)."
                End If
                btnCheckIn.Enabled = False
                btnCheckOut.Enabled = False
            End If
        End Sub

        Private Sub HandleCheckIn(sender As Object, e As EventArgs)
            If _verifiedMember Is Nothing Then Return
            Dim res = _attendanceController.CheckIn(_verifiedMember.MemberID)
            If res.Success Then
                MessageBox.Show($"Check-in recorded for {_verifiedMember.FullName} at {res.Attendance.CheckIn:hh:mm:ss tt}!", "Check-In Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                _isUpdatingSearchText = True
                txtMemberSearch.Clear()
                _isUpdatingSearchText = False
                _verifiedMember = Nothing
                _verifiedMembership = Nothing
                _verifiedActiveSession = Nothing
                btnCheckIn.Enabled = False
                btnCheckOut.Enabled = False
                lblMemberStatusInfo.ForeColor = Color.FromArgb(16, 185, 129)
                lblMemberStatusInfo.Text = $"✅ Check-in recorded for {res.Attendance.MemberName}! Ready for next member."
                LoadCurrentlyInside()
                LoadHistory()
            Else
                MessageBox.Show(res.ErrorMessage, "Check-In Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        Private Sub HandleCheckOut(sender As Object, e As EventArgs)
            If _verifiedMember Is Nothing Then
                ' Check out selected member from grid
                If dgvInside.SelectedRows.Count > 0 Then
                    Dim attId = Convert.ToInt32(dgvInside.SelectedRows(0).Cells("ID").Value)
                    Dim memName = dgvInside.SelectedRows(0).Cells("Member Name").Value.ToString()
                    Dim res = _attendanceController.CheckOut(attId)
                    If res.Success Then
                        Dim durText = If(res.Attendance IsNot Nothing, res.Attendance.DurationText, "Recorded")
                        MessageBox.Show($"Check-out recorded for {memName}! Total time inside: {durText}.", "Check-Out Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadCurrentlyInside()
                        LoadHistory()
                    Else
                        MessageBox.Show(res.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Please select a member currently inside the gym to check out.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return
            End If

            Dim checkOutRes = _attendanceController.CheckOutByMemberId(_verifiedMember.MemberID)
            If checkOutRes.Success Then
                Dim durText = If(checkOutRes.Attendance IsNot Nothing, checkOutRes.Attendance.DurationText, "Recorded")
                MessageBox.Show($"Check-out recorded for {_verifiedMember.FullName} at {DateTime.Now:hh:mm:ss tt}! Total time inside: {durText}.", "Check-Out Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                _isUpdatingSearchText = True
                txtMemberSearch.Clear()
                _isUpdatingSearchText = False
                _verifiedMember = Nothing
                _verifiedMembership = Nothing
                _verifiedActiveSession = Nothing
                btnCheckIn.Enabled = False
                btnCheckOut.Enabled = False
                lblMemberStatusInfo.ForeColor = Color.FromArgb(16, 185, 129)
                lblMemberStatusInfo.Text = $"✅ Check-out recorded for {checkOutRes.Attendance.MemberName} (Time inside: {durText}). Ready for next member."
                LoadCurrentlyInside()
                LoadHistory()
            Else
                MessageBox.Show(checkOutRes.ErrorMessage, "Check-Out Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        Public Sub LoadCurrentlyInside()
            Dim insideList = _attendanceController.GetCurrentlyInside()
            _currentInsideList = insideList

            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Card No", GetType(String))
            dt.Columns.Add("Member Name", GetType(String))
            dt.Columns.Add("Check-In Time", GetType(String))
            dt.Columns.Add("Time Inside", GetType(String))
            dt.Columns.Add("Recorded By", GetType(String))

            For Each item In insideList
                Dim checkInStr As String = If(item.CheckIn.Date = Date.Today, item.CheckIn.ToString("hh:mm:ss tt"), item.CheckIn.ToString("yyyy-MM-dd hh:mm:ss tt"))
                dt.Rows.Add(item.AttendanceID, item.MembershipNumber, item.MemberName, checkInStr, item.DurationText, item.RecordedByName)
            Next

            ' Preserve selection across timer refreshes
            Dim selectedId As Integer? = Nothing
            If dgvInside.SelectedRows.Count > 0 AndAlso dgvInside.SelectedRows(0).Cells("ID").Value IsNot Nothing Then
                selectedId = Convert.ToInt32(dgvInside.SelectedRows(0).Cells("ID").Value)
            End If

            dgvInside.DataSource = dt
            If dgvInside.Columns.Contains("ID") Then dgvInside.Columns("ID").Visible = False

            If dgvInside.Columns.Contains("Card No") Then
                dgvInside.Columns("Card No").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvInside.Columns("Card No").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvInside.Columns.Contains("Check-In Time") Then
                dgvInside.Columns("Check-In Time").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvInside.Columns("Check-In Time").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvInside.Columns.Contains("Time Inside") Then
                dgvInside.Columns("Time Inside").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvInside.Columns("Time Inside").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            If selectedId.HasValue Then
                For Each row As DataGridViewRow In dgvInside.Rows
                    If Convert.ToInt32(row.Cells("ID").Value) = selectedId.Value Then
                        row.Selected = True
                        Exit For
                    End If
                Next
            End If

            lblInsideCount.Text = $"Currently Inside: {insideList.Count}"
            tabInside.Text = $"📍 Members Currently Inside Gym ({insideList.Count})"
        End Sub

        Public Sub LoadHistory()
            Dim targetDate = dtpHistoryDate.Value.Date
            Dim histList = _attendanceController.GetHistory(targetDate:=targetDate)

            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Card No", GetType(String))
            dt.Columns.Add("Member Name", GetType(String))
            dt.Columns.Add("Check-In", GetType(String))
            dt.Columns.Add("Check-Out", GetType(String))
            dt.Columns.Add("Duration", GetType(String))
            dt.Columns.Add("Recorded By", GetType(String))

            For Each item In histList
                dt.Rows.Add(item.AttendanceID, item.MembershipNumber, item.MemberName, item.CheckIn.ToString("hh:mm:ss tt"), If(item.CheckOut.HasValue, item.CheckOut.Value.ToString("hh:mm:ss tt"), "Inside"), item.DurationText, item.RecordedByName)
            Next

            dgvHistory.DataSource = dt
            If dgvHistory.Columns.Contains("ID") Then dgvHistory.Columns("ID").Visible = False

            If dgvHistory.Columns.Contains("Card No") Then
                dgvHistory.Columns("Card No").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvHistory.Columns("Card No").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvHistory.Columns.Contains("Check-In") Then
                dgvHistory.Columns("Check-In").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvHistory.Columns("Check-In").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvHistory.Columns.Contains("Check-Out") Then
                dgvHistory.Columns("Check-Out").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvHistory.Columns("Check-Out").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvHistory.Columns.Contains("Duration") Then
                dgvHistory.Columns("Duration").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvHistory.Columns("Duration").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
        End Sub
    End Class
End Namespace
