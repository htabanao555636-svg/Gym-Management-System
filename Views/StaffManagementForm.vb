Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Staff and User Account Management View (Admin Only).
    ''' </summary>
    Public Class StaffManagementForm
        Inherits Form

        Private ReadOnly _staffController As New StaffController()

        Private pnlTop As Panel
        Private pnlHeaderRow As Panel
        Private pnlFilterRow As Panel
        Private flpActions As FlowLayoutPanel
        Private flpFilters As FlowLayoutPanel
        Private txtSearch As TextBox
        Private btnSearch As Button
        Private btnAddStaff As Button
        Private btnEditStaff As Button
        Private btnResetPassword As Button
        Private btnToggleStatus As Button
        Private btnRefresh As Button
        Private dgvStaff As DataGridView
        Private lblCount As Label

        Public Sub New()
            InitializeComponents()
            LoadStaffList()
        End Sub

        Private Sub InitializeComponents()
            Me.Text = "Staff & User Account Management"
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
                .Text = "👥 System Staff & User Management (Admin Only)",
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

            btnEditStaff = New Button() With {
                .Text = "✏️ Edit Account",
                .Size = New Size(125, 32),
                .BackColor = Color.FromArgb(243, 244, 246),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 2, 0, 2)
            }
            btnEditStaff.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnEditStaff.Click, AddressOf HandleEditStaff

            btnAddStaff = New Button() With {
                .Text = "➕ Add Account",
                .Size = New Size(130, 32),
                .BackColor = Color.FromArgb(37, 99, 235),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 2, 0, 2)
            }
            btnAddStaff.FlatAppearance.BorderSize = 0
            AddHandler btnAddStaff.Click, AddressOf HandleAddStaff

            flpActions.Controls.Add(btnToggleStatus)
            flpActions.Controls.Add(btnEditStaff)
            flpActions.Controls.Add(btnAddStaff)

            pnlHeaderRow.Controls.Add(lblTitle)
            pnlHeaderRow.Controls.Add(flpActions)

            ' Row 2: Search, Reset Password & Counters
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
            txtSearch = New TextBox() With {.Size = New Size(240, 26), .PlaceholderText = "Username, Name, Email...", .Margin = New Padding(0, 3, 6, 0)}
            AddHandler txtSearch.KeyDown, Sub(s, e)
                                              If e.KeyCode = Keys.Enter Then
                                                  LoadStaffList()
                                                  e.SuppressKeyPress = True
                                              End If
                                          End Sub

            btnSearch = New Button() With {.Text = "Search", .Size = New Size(75, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 10, 0)}
            btnSearch.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnSearch.Click, Sub() LoadStaffList()

            btnResetPassword = New Button() With {.Text = "🔑 Reset Password", .Size = New Size(130, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 8, 0)}
            btnResetPassword.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnResetPassword.Click, AddressOf HandleResetPassword

            btnRefresh = New Button() With {.Text = "↻ Reset", .Size = New Size(75, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 0, 0)}
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnRefresh.Click, Sub()
                                             txtSearch.Clear()
                                             LoadStaffList()
                                         End Sub

            flpFilters.Controls.AddRange(New Control() {lblS, txtSearch, btnSearch, btnResetPassword, btnRefresh})

            lblCount = New Label() With {
                .Text = "Total: 0 accounts",
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
            dgvStaff = New DataGridView() With {
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
            dgvStaff.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55)
            dgvStaff.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvStaff.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            dgvStaff.ColumnHeadersHeight = 35
            dgvStaff.EnableHeadersVisualStyles = False
            AddHandler dgvStaff.CellFormatting, AddressOf HandleCellFormatting

            Me.Controls.Add(dgvStaff)
            Me.Controls.Add(pnlTop)
        End Sub

        Private Sub HandleCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If dgvStaff.Columns(e.ColumnIndex).Name = "Role" AndAlso e.Value IsNot Nothing Then
                If String.Equals(e.Value.ToString(), "ADMIN", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38)
                    e.CellStyle.Font = New Font(dgvStaff.Font, FontStyle.Bold)
                Else
                    e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235)
                End If
            ElseIf dgvStaff.Columns(e.ColumnIndex).Name = "Status" AndAlso e.Value IsNot Nothing Then
                If String.Equals(e.Value.ToString(), "Active", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129)
                    e.CellStyle.Font = New Font(dgvStaff.Font, FontStyle.Bold)
                Else
                    e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128)
                End If
            End If
        End Sub

        Public Sub LoadStaffList()
            Dim search = txtSearch.Text.Trim()
            Dim staffList = _staffController.GetStaffList(search)

            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Emp ID", GetType(String))
            dt.Columns.Add("Username", GetType(String))
            dt.Columns.Add("Full Name", GetType(String))
            dt.Columns.Add("Role", GetType(String))
            dt.Columns.Add("Email", GetType(String))
            dt.Columns.Add("Contact Number", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("Created Date", GetType(String))

            For Each s In staffList
                dt.Rows.Add(s.UserID, s.EmployeeID, s.Username, s.FullName, s.Role, s.Email, s.ContactNumber, s.Status, s.DateCreated.ToString("yyyy-MM-dd"))
            Next

            dgvStaff.DataSource = dt
            If dgvStaff.Columns.Contains("ID") Then dgvStaff.Columns("ID").Visible = False

            If dgvStaff.Columns.Contains("Emp ID") Then
                dgvStaff.Columns("Emp ID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvStaff.Columns("Emp ID").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvStaff.Columns.Contains("Username") Then
                dgvStaff.Columns("Username").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvStaff.Columns("Username").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvStaff.Columns.Contains("Role") Then
                dgvStaff.Columns("Role").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvStaff.Columns("Role").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvStaff.Columns.Contains("Status") Then
                dgvStaff.Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvStaff.Columns("Status").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvStaff.Columns.Contains("Created Date") Then
                dgvStaff.Columns("Created Date").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvStaff.Columns("Created Date").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            lblCount.Text = $"Total: {staffList.Count} accounts"
        End Sub

        Private Function GetSelectedStaffId() As Integer?
            If dgvStaff.SelectedRows.Count > 0 Then
                Return Convert.ToInt32(dgvStaff.SelectedRows(0).Cells("ID").Value)
            End If
            Return Nothing
        End Function

        Private Sub HandleAddStaff(sender As Object, e As EventArgs)
            Using dlg As New StaffDialog()
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    LoadStaffList()
                End If
            End Using
        End Sub

        Private Sub HandleEditStaff(sender As Object, e As EventArgs)
            Dim uid = GetSelectedStaffId()
            If Not uid.HasValue Then
                MessageBox.Show("Please select an account to edit.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim staff = _staffController.GetStaffById(uid.Value)
            If staff IsNot Nothing Then
                Using dlg As New StaffDialog(staff)
                    If dlg.ShowDialog(Me) = DialogResult.OK Then
                        LoadStaffList()
                    End If
                End Using
            End If
        End Sub

        Private Sub HandleResetPassword(sender As Object, e As EventArgs)
            Dim uid = GetSelectedStaffId()
            If Not uid.HasValue Then
                MessageBox.Show("Please select an account to reset password.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim newPass As String = Microsoft.VisualBasic.Interaction.InputBox("Enter new password for this user (minimum 6 characters):", "Reset User Password", "")
            If Not String.IsNullOrWhiteSpace(newPass) Then
                Dim res = _staffController.ResetPassword(uid.Value, newPass)
                If res.Success Then
                    MessageBox.Show("Password reset successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show(res.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End Sub

        Private Sub HandleToggleStatus(sender As Object, e As EventArgs)
            Dim uid = GetSelectedStaffId()
            If Not uid.HasValue Then
                MessageBox.Show("Please select an account to change status.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim staff = _staffController.GetStaffById(uid.Value)
            If staff IsNot Nothing Then
                Dim newStatus = If(staff.IsActive, "Inactive", "Active")
                If MessageBox.Show($"Are you sure you want to change user '{staff.Username}' status to '{newStatus}'?", "Confirm Status Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim res = _staffController.SetStatus(staff.UserID, newStatus)
                    If res.Success Then
                        LoadStaffList()
                    Else
                        MessageBox.Show(res.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        End Sub
    End Class
End Namespace
