Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Memberships management and subscriptions overview view with responsive top bar and clean styling.
    ''' </summary>
    Public Class MembershipsListForm
        Inherits Form

        Private ReadOnly _membershipController As New MembershipController()

        Private pnlTop As Panel
        Private pnlHeaderRow As Panel
        Private pnlFilterRow As Panel
        Private flpActions As FlowLayoutPanel
        Private flpFilters As FlowLayoutPanel
        Private txtSearch As TextBox
        Private cboStatusFilter As ComboBox
        Private btnSearch As Button
        Private btnEnroll As Button
        Private btnCancelMembership As Button
        Private btnRefresh As Button
        Private dgvMemberships As DataGridView
        Private lblCount As Label

        Public Sub New()
            InitializeComponents()
            LoadMemberships()
        End Sub

        Private Sub InitializeComponents()
            Me.Text = "Memberships Overview"
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
                .Text = "⭐ Member Subscriptions & Packages",
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

            btnCancelMembership = New Button() With {
                .Text = "❌ Cancel Subscription",
                .Size = New Size(160, 32),
                .BackColor = Color.FromArgb(243, 244, 246),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 2, 0, 2)
            }
            btnCancelMembership.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnCancelMembership.Click, AddressOf HandleCancelMembership

            btnEnroll = New Button() With {
                .Text = "➕ Enroll / Renew",
                .Size = New Size(140, 32),
                .BackColor = Color.FromArgb(37, 99, 235),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 2, 0, 2)
            }
            btnEnroll.FlatAppearance.BorderSize = 0
            AddHandler btnEnroll.Click, AddressOf HandleEnroll

            flpActions.Controls.Add(btnCancelMembership)
            flpActions.Controls.Add(btnEnroll)

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
            txtSearch = New TextBox() With {.Size = New Size(240, 26), .PlaceholderText = "Member Name, Card No, Plan...", .Margin = New Padding(0, 3, 6, 0)}
            AddHandler txtSearch.KeyDown, Sub(s, e)
                                              If e.KeyCode = Keys.Enter Then
                                                  LoadMemberships()
                                                  e.SuppressKeyPress = True
                                              End If
                                          End Sub

            btnSearch = New Button() With {.Text = "Search", .Size = New Size(75, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 12, 0)}
            btnSearch.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnSearch.Click, Sub() LoadMemberships()

            Dim lblF As New Label() With {.Text = "Status:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            cboStatusFilter = New ComboBox() With {.Size = New Size(150, 26), .DropDownStyle = ComboBoxStyle.DropDownList, .Margin = New Padding(0, 3, 10, 0)}
            cboStatusFilter.Items.AddRange(New Object() {"All", "Pending Payment", "Active", "Expiring Soon", "Expired", "Cancelled"})
            cboStatusFilter.SelectedIndex = 0
            AddHandler cboStatusFilter.SelectedIndexChanged, Sub() LoadMemberships()

            btnRefresh = New Button() With {.Text = "↻ Reset", .Size = New Size(75, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 0, 0)}
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnRefresh.Click, Sub()
                                             txtSearch.Clear()
                                             cboStatusFilter.SelectedIndex = 0
                                             LoadMemberships()
                                         End Sub

            flpFilters.Controls.AddRange(New Control() {lblS, txtSearch, btnSearch, lblF, cboStatusFilter, btnRefresh})

            lblCount = New Label() With {
                .Text = "Total: 0 records",
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
            dgvMemberships = New DataGridView() With {
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
            dgvMemberships.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55)
            dgvMemberships.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvMemberships.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            dgvMemberships.ColumnHeadersHeight = 35
            dgvMemberships.EnableHeadersVisualStyles = False
            AddHandler dgvMemberships.CellFormatting, AddressOf HandleCellFormatting

            Me.Controls.Add(dgvMemberships)
            Me.Controls.Add(pnlTop)
        End Sub

        Private Sub HandleCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If dgvMemberships.Columns(e.ColumnIndex).Name = "Status" AndAlso e.Value IsNot Nothing Then
                Dim status = e.Value.ToString()
                If String.Equals(status, "Active", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129)
                    e.CellStyle.Font = New Font(dgvMemberships.Font, FontStyle.Bold)
                ElseIf String.Equals(status, "Pending Payment", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(234, 88, 12)
                    e.CellStyle.Font = New Font(dgvMemberships.Font, FontStyle.Bold)
                ElseIf String.Equals(status, "Expiring Soon", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(245, 158, 11)
                    e.CellStyle.Font = New Font(dgvMemberships.Font, FontStyle.Bold)
                Else
                    e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68)
                End If
            End If
        End Sub

        Public Sub LoadMemberships()
            Dim search = txtSearch.Text.Trim()
            Dim filter = If(cboStatusFilter.SelectedItem IsNot Nothing, cboStatusFilter.SelectedItem.ToString(), "All")
            Dim list = _membershipController.GetMemberships(search, filter)

            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Card No", GetType(String))
            dt.Columns.Add("Member Name", GetType(String))
            dt.Columns.Add("Plan Name", GetType(String))
            dt.Columns.Add("Start Date", GetType(String))
            dt.Columns.Add("End Date", GetType(String))
            dt.Columns.Add("Days Left", GetType(Integer))
            dt.Columns.Add("Amount", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            For Each ms In list
                dt.Rows.Add(ms.MembershipID, ms.MembershipNumber, ms.MemberName, ms.PlanName, ms.StartDate.ToString("yyyy-MM-dd"), ms.EndDate.ToString("yyyy-MM-dd"), ms.DaysRemaining, ms.Amount.ToString("C"), ms.Status)
            Next

            dgvMemberships.DataSource = dt
            If dgvMemberships.Columns.Contains("ID") Then dgvMemberships.Columns("ID").Visible = False

            ' Align columns
            If dgvMemberships.Columns.Contains("Card No") Then
                dgvMemberships.Columns("Card No").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvMemberships.Columns("Card No").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvMemberships.Columns.Contains("Start Date") Then
                dgvMemberships.Columns("Start Date").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvMemberships.Columns("Start Date").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvMemberships.Columns.Contains("End Date") Then
                dgvMemberships.Columns("End Date").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvMemberships.Columns("End Date").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvMemberships.Columns.Contains("Days Left") Then
                dgvMemberships.Columns("Days Left").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvMemberships.Columns("Days Left").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvMemberships.Columns.Contains("Amount") Then
                dgvMemberships.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                dgvMemberships.Columns("Amount").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            If dgvMemberships.Columns.Contains("Status") Then
                dgvMemberships.Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvMemberships.Columns("Status").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            lblCount.Text = $"Total: {list.Count} records"
        End Sub

        Private Function GetSelectedMembershipId() As Integer?
            If dgvMemberships.SelectedRows.Count > 0 Then
                Return Convert.ToInt32(dgvMemberships.SelectedRows(0).Cells("ID").Value)
            End If
            Return Nothing
        End Function

        Private Sub HandleEnroll(sender As Object, e As EventArgs)
            Using dlg As New MembershipForm()
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    LoadMemberships()
                End If
            End Using
        End Sub

        Private Sub HandleCancelMembership(sender As Object, e As EventArgs)
            Dim msId = GetSelectedMembershipId()
            If Not msId.HasValue Then
                MessageBox.Show("Please select a membership to cancel.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If MessageBox.Show("Are you sure you want to cancel this membership?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Dim res = _membershipController.Cancel(msId.Value, "User requested cancellation")
                If res.Success Then
                    LoadMemberships()
                Else
                    MessageBox.Show(res.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End Sub
    End Class
End Namespace
