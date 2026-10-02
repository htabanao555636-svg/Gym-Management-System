Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' System Activity Audit Log View (Admin Only).
    ''' </summary>
    Public Class ActivityLogsForm
        Inherits Form

        Private ReadOnly _logController As New ActivityLogController()

        Private pnlTop As Panel
        Private pnlHeaderRow As Panel
        Private pnlFilterRow As Panel
        Private flpFilters As FlowLayoutPanel
        Private txtSearch As TextBox
        Private cboActionFilter As ComboBox
        Private dtpFrom As DateTimePicker
        Private dtpTo As DateTimePicker
        Private btnFilter As Button
        Private btnReset As Button
        Private btnRefresh As Button
        Private btnExport As Button
        Private dgvLogs As DataGridView
        Private lblCount As Label

        Public Sub New()
            InitializeComponents()
        End Sub

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            LoadLogs()
        End Sub

        Private Sub InitializeComponents()
            Me.Text = "System Activity Logs"
            Me.BackColor = Color.FromArgb(249, 250, 251)
            Me.Font = New Font("Segoe UI", 9.0F)
            Me.Dock = DockStyle.Fill
            Me.TopLevel = False
            Me.FormBorderStyle = FormBorderStyle.None

            ' Top action panel container
            pnlTop = New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 112,
                .BackColor = Color.White,
                .Padding = New Padding(20, 10, 20, 8)
            }

            ' Row 1: Title and Header Actions
            pnlHeaderRow = New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 38,
                .BackColor = Color.White
            }

            Dim lblTitle As New Label() With {
                .Text = "📜 System Audit Trail & Activity Logs (Admin Only)",
                .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(17, 24, 39),
                .Dock = DockStyle.Left,
                .AutoSize = True,
                .TextAlign = ContentAlignment.MiddleLeft
            }

            Dim flpHeaderButtons As New FlowLayoutPanel() With {
                .Dock = DockStyle.Right,
                .FlowDirection = FlowDirection.RightToLeft,
                .WrapContents = False,
                .AutoSize = True
            }

            btnExport = New Button() With {
                .Text = "📥 Export CSV",
                .Size = New Size(115, 30),
                .BackColor = Color.FromArgb(243, 244, 246),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(4, 0, 0, 0)
            }
            btnExport.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnExport.Click, AddressOf HandleExportCsv

            btnRefresh = New Button() With {
                .Text = "🔄 Refresh",
                .Size = New Size(95, 30),
                .BackColor = Color.FromArgb(243, 244, 246),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(4, 0, 4, 0)
            }
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnRefresh.Click, Sub() LoadLogs()

            flpHeaderButtons.Controls.AddRange(New Control() {btnExport, btnRefresh})
            pnlHeaderRow.Controls.Add(lblTitle)
            pnlHeaderRow.Controls.Add(flpHeaderButtons)

            ' Row 2: Search, Category, Date Range & Counters
            pnlFilterRow = New Panel() With {
                .Dock = DockStyle.Bottom,
                .Height = 44,
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
            txtSearch = New TextBox() With {.Size = New Size(170, 26), .PlaceholderText = "Keywords, member...", .Margin = New Padding(0, 3, 8, 0)}
            AddHandler txtSearch.KeyDown, Sub(s, e)
                                              If e.KeyCode = Keys.Enter Then
                                                  LoadLogs()
                                                  e.SuppressKeyPress = True
                                              End If
                                          End Sub

            Dim lblAct As New Label() With {.Text = "Action:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            cboActionFilter = New ComboBox() With {
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Size = New Size(165, 26),
                .Margin = New Padding(0, 3, 8, 0)
            }
            cboActionFilter.Items.AddRange(New Object() {
                "All Activities",
                "Attendance Check-In",
                "Attendance Check-Out",
                "Payment Processed",
                "Payment Cancelled",
                "Member Added",
                "Member Updated",
                "Membership Enrolled",
                "Membership Cancelled",
                "User Login",
                "User Logout",
                "Staff Management",
                "Settings Updated"
            })
            cboActionFilter.SelectedIndex = 0
            AddHandler cboActionFilter.SelectedIndexChanged, Sub() LoadLogs()

            Dim lblFrom As New Label() With {.Text = "From:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            dtpFrom = New DateTimePicker() With {.Size = New Size(110, 26), .Format = DateTimePickerFormat.Short, .Value = Date.Today.AddDays(-30), .Margin = New Padding(0, 3, 8, 0)}

            Dim lblTo As New Label() With {.Text = "To:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            dtpTo = New DateTimePicker() With {.Size = New Size(110, 26), .Format = DateTimePickerFormat.Short, .Value = Date.Today, .Margin = New Padding(0, 3, 8, 0)}

            btnFilter = New Button() With {.Text = "Filter", .Size = New Size(65, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 6, 0)}
            btnFilter.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnFilter.Click, Sub() LoadLogs()

            btnReset = New Button() With {.Text = "Reset", .Size = New Size(60, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 0, 0)}
            btnReset.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnReset.Click, Sub()
                                           txtSearch.Clear()
                                           cboActionFilter.SelectedIndex = 0
                                           dtpFrom.Value = Date.Today.AddDays(-30)
                                           dtpTo.Value = Date.Today
                                           LoadLogs()
                                       End Sub

            flpFilters.Controls.AddRange(New Control() {lblS, txtSearch, lblAct, cboActionFilter, lblFrom, dtpFrom, lblTo, dtpTo, btnFilter, btnReset})

            lblCount = New Label() With {
                .Text = "Total: 0 logs",
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
            dgvLogs = New DataGridView() With {
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
            dgvLogs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55)
            dgvLogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvLogs.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            dgvLogs.ColumnHeadersHeight = 35
            dgvLogs.EnableHeadersVisualStyles = False
            AddHandler dgvLogs.CellFormatting, AddressOf HandleCellFormatting

            Me.Controls.Add(dgvLogs)
            Me.Controls.Add(pnlTop)
        End Sub

        Private Sub HandleCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            Try
                If e.RowIndex < 0 OrElse e.ColumnIndex < 0 OrElse dgvLogs.Columns Is Nothing OrElse e.ColumnIndex >= dgvLogs.Columns.Count Then Return
                If e.Value Is Nothing OrElse Convert.IsDBNull(e.Value) OrElse e.CellStyle Is Nothing Then Return

                Dim col = dgvLogs.Columns(e.ColumnIndex)
                If col Is Nothing Then Return

                If String.Equals(col.Name, "Role", StringComparison.OrdinalIgnoreCase) Then
                    Dim roleStr = e.Value.ToString()
                    If String.Equals(roleStr, "ADMIN", StringComparison.OrdinalIgnoreCase) Then
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38)
                        If dgvLogs.Font IsNot Nothing Then
                            e.CellStyle.Font = New Font(dgvLogs.Font, FontStyle.Bold)
                        End If
                    ElseIf String.Equals(roleStr, "STAFF", StringComparison.OrdinalIgnoreCase) Then
                        e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235)
                    Else
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128)
                    End If
                ElseIf String.Equals(col.Name, "Action", StringComparison.OrdinalIgnoreCase) Then
                    Dim act = e.Value.ToString()
                    If act.Contains("Check-In") OrElse act.Contains("Paid") OrElse act.Contains("Processed") Then
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129)
                        If dgvLogs.Font IsNot Nothing Then
                            e.CellStyle.Font = New Font(dgvLogs.Font, FontStyle.Bold)
                        End If
                    ElseIf act.Contains("Check-Out") Then
                        e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6)
                        If dgvLogs.Font IsNot Nothing Then
                            e.CellStyle.Font = New Font(dgvLogs.Font, FontStyle.Bold)
                        End If
                    ElseIf act.Contains("Cancel") OrElse act.Contains("Deactivat") Then
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38)
                    End If
                End If
            Catch
                ' Safely ignore formatting exceptions during layout/resizing
            End Try
        End Sub

        Public Sub LoadLogs()
            Try
                Dim search = If(txtSearch IsNot Nothing, txtSearch.Text.Trim(), "")
                Dim startD = If(dtpFrom IsNot Nothing, dtpFrom.Value.Date, Date.Today.AddDays(-30))
                Dim endD = If(dtpTo IsNot Nothing, dtpTo.Value.Date, Date.Today)
                Dim actionSel = If(cboActionFilter IsNot Nothing AndAlso cboActionFilter.SelectedItem IsNot Nothing, cboActionFilter.SelectedItem.ToString(), "All Activities")
                Dim actionFilter As String = If(String.Equals(actionSel, "All Activities", StringComparison.OrdinalIgnoreCase), "All", actionSel)

                Dim logs = _logController.GetLogs(startD, endD, search, actionFilter)
                Dim dt As New DataTable()
                dt.Columns.Add("Log ID", GetType(Integer))
                dt.Columns.Add("Timestamp", GetType(String))
                dt.Columns.Add("Action", GetType(String))
                dt.Columns.Add("Description", GetType(String))
                dt.Columns.Add("User", GetType(String))
                dt.Columns.Add("Role", GetType(String))

                For Each l In logs
                    dt.Rows.Add(l.LogID, l.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss tt"), l.Action, l.Description, $"{l.Username} ({l.FullName})", l.Role)
                Next

                dgvLogs.DataSource = dt
                If dgvLogs.Columns.Contains("Log ID") Then
                    Dim col = dgvLogs.Columns("Log ID")
                    col.FillWeight = 8
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
                If dgvLogs.Columns.Contains("Timestamp") Then
                    Dim col = dgvLogs.Columns("Timestamp")
                    col.FillWeight = 16
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
                If dgvLogs.Columns.Contains("Action") Then
                    Dim col = dgvLogs.Columns("Action")
                    col.FillWeight = 18
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
                If dgvLogs.Columns.Contains("Description") Then
                    Dim col = dgvLogs.Columns("Description")
                    col.FillWeight = 36
                End If
                If dgvLogs.Columns.Contains("User") Then
                    Dim col = dgvLogs.Columns("User")
                    col.FillWeight = 14
                End If
                If dgvLogs.Columns.Contains("Role") Then
                    Dim col = dgvLogs.Columns("Role")
                    col.FillWeight = 8
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
                If lblCount IsNot Nothing Then
                    lblCount.Text = $"Total: {logs.Count} entries"
                End If
            Catch ex As Exception
                MessageBox.Show($"Unable to load activity logs: {ex.Message}", "Activity Logs Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub InitializeComponent()
            SuspendLayout()
            ' 
            ' ActivityLogsForm
            ' 
            ClientSize = New Size(769, 524)
            Name = "ActivityLogsForm"
            ResumeLayout(False)

        End Sub

        Private Sub HandleExportCsv(sender As Object, e As EventArgs)
            If dgvLogs.Rows.Count = 0 Then
                MessageBox.Show("No log entries to export.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Filter = "CSV Files (*.csv)|*.csv"
                sfd.FileName = $"ActivityLogs_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                If sfd.ShowDialog() = DialogResult.OK Then
                    Try
                        Dim sb As New StringBuilder()
                        ' Header row
                        Dim headers As New List(Of String)()
                        For Each col As DataGridViewColumn In dgvLogs.Columns
                            If col.Visible Then headers.Add($"""{col.HeaderText}""")
                        Next
                        sb.AppendLine(String.Join(",", headers))

                        ' Data rows
                        For Each row As DataGridViewRow In dgvLogs.Rows
                            If Not row.IsNewRow Then
                                Dim cells As New List(Of String)()
                                For Each col As DataGridViewColumn In dgvLogs.Columns
                                    If col.Visible Then
                                        Dim val = If(row.Cells(col.Index).Value, "").ToString().Replace("""", """""")
                                        cells.Add($"""{val}""")
                                    End If
                                Next
                                sb.AppendLine(String.Join(",", cells))
                            End If
                        Next

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8)
                        MessageBox.Show($"Activity logs successfully exported to:{Environment.NewLine}{sfd.FileName}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show($"Failed to export logs: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using
        End Sub

        Private Sub ActivityLogsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        End Sub
    End Class
End Namespace
