Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Views
    ''' <summary>
    ''' Member Management View for searching, filtering, adding, editing, and managing member statuses.
    ''' </summary>
    Public Class MembersForm
        Inherits Form

        Private ReadOnly _memberController As New MemberController()

        Private pnlTop As Panel
        Private pnlHeaderRow As Panel
        Private pnlFilterRow As Panel
        Private flpActions As FlowLayoutPanel
        Private flpFilters As FlowLayoutPanel
        Private txtSearch As TextBox
        Private cboStatusFilter As ComboBox
        Private btnSearch As Button
        Private btnAddMember As Button
        Private btnEditMember As Button
        Private btnToggleStatus As Button
        Private btnViewDetails As Button
        Private btnRefresh As Button
        Private dgvMembers As DataGridView
        Private lblCount As Label

        Public Sub New()
            InitializeComponents()
            LoadMembers()
        End Sub

        Private Sub InitializeComponents()
            Me.Text = "Member Management"
            Me.BackColor = Color.FromArgb(249, 250, 251)
            Me.Font = New Font("Segoe UI", 9.0F)
            Me.Dock = DockStyle.Fill
            Me.TopLevel = False
            Me.FormBorderStyle = FormBorderStyle.None

            ' Top action panel container
            pnlTop = New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 105,
                .BackColor = Color.White,
                .Padding = New Padding(20, 10, 20, 10)
            }

            ' Row 1: Title & Action Buttons
            pnlHeaderRow = New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 40,
                .BackColor = Color.White
            }

            Dim lblTitle As New Label() With {
                .Text = "🏋 Member Management Directory",
                .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(17, 24, 39),
                .Dock = DockStyle.Left,
                .AutoSize = True,
                .TextAlign = ContentAlignment.MiddleLeft
            }

            flpActions = New FlowLayoutPanel() With {
                .Dock = DockStyle.Right,
                .FlowDirection = FlowDirection.RightToLeft,
                .WrapContents = False,
                .AutoSize = True
            }

            btnToggleStatus = New Button() With {
                .Text = "🔄 Status Toggle",
                .Size = New Size(130, 32),
                .BackColor = Color.FromArgb(243, 244, 246),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 2, 0, 2)
            }
            btnToggleStatus.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnToggleStatus.Click, AddressOf HandleToggleStatus

            btnEditMember = New Button() With {
                .Text = "✏️ Edit Member",
                .Size = New Size(120, 32),
                .BackColor = Color.FromArgb(243, 244, 246),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 2, 0, 2)
            }
            btnEditMember.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnEditMember.Click, AddressOf HandleEditMember

            btnAddMember = New Button() With {
                .Text = "➕ Add Member",
                .Size = New Size(130, 32),
                .BackColor = Color.FromArgb(37, 99, 235),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 2, 0, 2)
            }
            btnAddMember.FlatAppearance.BorderSize = 0
            AddHandler btnAddMember.Click, AddressOf HandleAddMember

            flpActions.Controls.Add(btnToggleStatus)
            flpActions.Controls.Add(btnEditMember)
            flpActions.Controls.Add(btnAddMember)

            pnlHeaderRow.Controls.Add(lblTitle)
            pnlHeaderRow.Controls.Add(flpActions)

            ' Row 2: Search, Filters & Counters
            pnlFilterRow = New Panel() With {
                .Dock = DockStyle.Bottom,
                .Height = 42,
                .BackColor = Color.White
            }

            flpFilters = New FlowLayoutPanel() With {
                .Dock = DockStyle.Left,
                .FlowDirection = FlowDirection.LeftToRight,
                .WrapContents = False,
                .AutoSize = True,
                .Padding = New Padding(0, 4, 0, 0)
            }

            Dim lblS As New Label() With {.Text = "Search:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            txtSearch = New TextBox() With {.Size = New Size(220, 26), .PlaceholderText = "Name, Card No, Phone...", .Margin = New Padding(0, 3, 6, 0)}
            AddHandler txtSearch.KeyDown, Sub(s, e)
                                              If e.KeyCode = Keys.Enter Then
                                                  LoadMembers()
                                                  e.SuppressKeyPress = True
                                              End If
                                          End Sub

            btnSearch = New Button() With {.Text = "Search", .Size = New Size(75, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 12, 0)}
            btnSearch.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnSearch.Click, Sub() LoadMembers()

            Dim lblF As New Label() With {.Text = "Status:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            cboStatusFilter = New ComboBox() With {.Size = New Size(125, 26), .DropDownStyle = ComboBoxStyle.DropDownList, .Margin = New Padding(0, 3, 10, 0)}
            cboStatusFilter.Items.AddRange(New Object() {"All", "Active", "Inactive", "Suspended"})
            cboStatusFilter.SelectedIndex = 0
            AddHandler cboStatusFilter.SelectedIndexChanged, Sub() LoadMembers()

            btnViewDetails = New Button() With {.Text = "👁️ Details", .Size = New Size(90, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 6, 0)}
            btnViewDetails.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnViewDetails.Click, AddressOf HandleViewDetails

            btnRefresh = New Button() With {.Text = "↻ Reset", .Size = New Size(75, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 0, 0)}
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnRefresh.Click, Sub()
                                             txtSearch.Clear()
                                             cboStatusFilter.SelectedIndex = 0
                                             LoadMembers()
                                         End Sub

            flpFilters.Controls.AddRange(New Control() {lblS, txtSearch, btnSearch, lblF, cboStatusFilter, btnViewDetails, btnRefresh})

            lblCount = New Label() With {
                .Text = "Total: 0 members",
                .Dock = DockStyle.Right,
                .TextAlign = ContentAlignment.MiddleRight,
                .AutoSize = True,
                .ForeColor = Color.FromArgb(107, 114, 128),
                .Font = New Font("Segoe UI", 8.5F, FontStyle.Italic),
                .Padding = New Padding(0, 8, 0, 0)
            }

            pnlFilterRow.Controls.Add(flpFilters)
            pnlFilterRow.Controls.Add(lblCount)

            pnlTop.Controls.Add(pnlFilterRow)
            pnlTop.Controls.Add(pnlHeaderRow)

            ' Main DataGridView
            dgvMembers = New DataGridView() With {
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
            dgvMembers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55)
            dgvMembers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvMembers.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            dgvMembers.ColumnHeadersHeight = 35
            dgvMembers.EnableHeadersVisualStyles = False
            AddHandler dgvMembers.CellFormatting, AddressOf HandleCellFormatting

            Me.Controls.Add(dgvMembers)
            Me.Controls.Add(pnlTop)
        End Sub

        Private Sub HandleCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If dgvMembers.Columns(e.ColumnIndex).Name = "Status" AndAlso e.Value IsNot Nothing Then
                Dim status As String = e.Value.ToString()
                If String.Equals(status, "Active", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129)
                    e.CellStyle.Font = New Font(dgvMembers.Font, FontStyle.Bold)
                ElseIf String.Equals(status, "Suspended", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(245, 158, 11)
                    e.CellStyle.Font = New Font(dgvMembers.Font, FontStyle.Bold)
                Else
                    e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68)
                End If
            End If
        End Sub

        Public Sub LoadMembers()
            Dim search As String = txtSearch.Text.Trim()
            Dim filter As String = If(cboStatusFilter.SelectedItem IsNot Nothing, cboStatusFilter.SelectedItem.ToString(), "All")
            Dim members As List(Of Member) = _memberController.GetMembers(search, filter)

            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Membership No", GetType(String))
            dt.Columns.Add("Full Name", GetType(String))
            dt.Columns.Add("Gender", GetType(String))
            dt.Columns.Add("Age", GetType(Integer))
            dt.Columns.Add("Contact Number", GetType(String))
            dt.Columns.Add("Email", GetType(String))
            dt.Columns.Add("Registered Date", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            For Each m In members
                dt.Rows.Add(m.MemberID, m.MembershipNumber, m.FullName, m.Gender, m.Age, m.ContactNumber, m.Email, m.RegistrationDate.ToString("yyyy-MM-dd"), m.Status)
            Next

            dgvMembers.DataSource = dt
            If dgvMembers.Columns.Contains("ID") Then
                dgvMembers.Columns("ID").Visible = False
            End If

            ' Align columns
            If dgvMembers.Columns.Contains("Membership No") Then
                dgvMembers.Columns("Membership No").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvMembers.Columns("Membership No").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvMembers.Columns.Contains("Gender") Then
                dgvMembers.Columns("Gender").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvMembers.Columns("Gender").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvMembers.Columns.Contains("Age") Then
                dgvMembers.Columns("Age").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvMembers.Columns("Age").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvMembers.Columns.Contains("Registered Date") Then
                dgvMembers.Columns("Registered Date").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvMembers.Columns("Registered Date").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvMembers.Columns.Contains("Status") Then
                dgvMembers.Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvMembers.Columns("Status").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            lblCount.Text = $"Total: {members.Count} members"
        End Sub

        Private Function GetSelectedMemberId() As Integer?
            If dgvMembers.SelectedRows.Count > 0 Then
                Dim row = dgvMembers.SelectedRows(0)
                Return Convert.ToInt32(row.Cells("ID").Value)
            End If
            Return Nothing
        End Function

        Private Sub HandleAddMember(sender As Object, e As EventArgs)
            Using dlg As New MemberForm()
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    LoadMembers()
                End If
            End Using
        End Sub

        Private Sub HandleEditMember(sender As Object, e As EventArgs)
            Dim memberId = GetSelectedMemberId()
            If Not memberId.HasValue Then
                MessageBox.Show("Please select a member to edit.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim member = _memberController.GetMemberById(memberId.Value)
            If member IsNot Nothing Then
                Using dlg As New MemberForm(member)
                    If dlg.ShowDialog(Me) = DialogResult.OK Then
                        LoadMembers()
                    End If
                End Using
            End If
        End Sub

        Private Sub HandleToggleStatus(sender As Object, e As EventArgs)
            Dim memberId = GetSelectedMemberId()
            If Not memberId.HasValue Then
                MessageBox.Show("Please select a member to change status.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim member = _memberController.GetMemberById(memberId.Value)
            If member IsNot Nothing Then
                Dim newStatus As String = If(member.IsActive, "Inactive", "Active")
                Dim prompt As String = $"Are you sure you want to change {member.FullName}'s status to '{newStatus}'?"
                If MessageBox.Show(prompt, "Confirm Status Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim res = _memberController.SetStatus(member.MemberID, newStatus)
                    If res.Success Then
                        LoadMembers()
                    Else
                        MessageBox.Show(res.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        End Sub

        Private Sub HandleViewDetails(sender As Object, e As EventArgs)
            Dim memberId = GetSelectedMemberId()
            If Not memberId.HasValue Then
                MessageBox.Show("Please select a member to view details.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim member = _memberController.GetMemberById(memberId.Value)
            If member IsNot Nothing Then
                Dim info As String = $"MEMBER DETAILS:{Environment.NewLine}" &
                                    $"-----------------------------------{Environment.NewLine}" &
                                    $"Card No: {member.MembershipNumber}{Environment.NewLine}" &
                                    $"Name: {member.FullName}{Environment.NewLine}" &
                                    $"Gender: {member.Gender} | Age: {member.Age} yrs{Environment.NewLine}" &
                                    $"Birth Date: {member.BirthDate:yyyy-MM-dd}{Environment.NewLine}" &
                                    $"Contact: {member.ContactNumber}{Environment.NewLine}" &
                                    $"Email: {member.Email}{Environment.NewLine}" &
                                    $"Address: {member.Address}{Environment.NewLine}" &
                                    $"Emergency Contact: {member.EmergencyContactName} ({member.EmergencyContactNumber}){Environment.NewLine}" &
                                    $"Registration Date: {member.RegistrationDate:yyyy-MM-dd}{Environment.NewLine}" &
                                    $"Status: {member.Status}"

                MessageBox.Show(info, $"Member: {member.FullName}", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub
    End Class
End Namespace
