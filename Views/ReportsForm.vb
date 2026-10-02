Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers

Namespace Views
    ''' <summary>
    ''' Comprehensive reporting module for Revenue, Attendance, and Memberships with CSV export capabilities.
    ''' </summary>
    Public Class ReportsForm
        Inherits Form

        Private ReadOnly _reportController As New ReportController()

        Private pnlTop As Panel
        Private pnlHeaderRow As Panel
        Private pnlFilterRow As Panel
        Private flpFilters As FlowLayoutPanel
        Private cboReportType As ComboBox
        Private dtpFrom As DateTimePicker
        Private dtpTo As DateTimePicker
        Private cboMethodFilter As ComboBox
        Private btnGenerate As Button
        Private btnExportCSV As Button
        Private dgvReport As DataGridView
        Private lblSummary As Label
        Private _currentReportData As DataTable

        Public Sub New()
            InitializeComponents()
            GenerateReport()
        End Sub

        Private Sub InitializeComponents()
            Me.Text = "Gym Analytics & Reports"
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

            ' Row 1: Title & Export Button
            pnlHeaderRow = New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 40,
                .BackColor = Color.White
            }

            Dim lblTitle As New Label() With {
                .Text = "📊 Gym Operational & Financial Reports",
                .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(17, 24, 39),
                .Dock = DockStyle.Left,
                .AutoSize = True,
                .TextAlign = ContentAlignment.MiddleLeft
            }

            btnExportCSV = New Button() With {
                .Text = "📥 Export to CSV",
                .Size = New Size(140, 32),
                .Dock = DockStyle.Right,
                .BackColor = Color.FromArgb(16, 185, 129),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand
            }
            btnExportCSV.FlatAppearance.BorderSize = 0
            AddHandler btnExportCSV.Click, AddressOf HandleExportCSV

            pnlHeaderRow.Controls.Add(lblTitle)
            pnlHeaderRow.Controls.Add(btnExportCSV)

            ' Row 2: Report Type, Date Range, Method & Summary
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

            Dim lblType As New Label() With {.Text = "Report:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            cboReportType = New ComboBox() With {.Size = New Size(175, 26), .DropDownStyle = ComboBoxStyle.DropDownList, .Margin = New Padding(0, 3, 8, 0)}
            cboReportType.Items.AddRange(New Object() {"Revenue Report", "Attendance Report", "Membership Report", "Monthly Revenue Trend"})
            cboReportType.SelectedIndex = 0
            AddHandler cboReportType.SelectedIndexChanged, AddressOf HandleReportTypeChanged

            Dim lblFrom As New Label() With {.Text = "From:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            dtpFrom = New DateTimePicker() With {.Size = New Size(115, 26), .Format = DateTimePickerFormat.Short, .Value = Date.Today.AddMonths(-1), .Margin = New Padding(0, 3, 6, 0)}

            Dim lblTo As New Label() With {.Text = "To:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            dtpTo = New DateTimePicker() With {.Size = New Size(115, 26), .Format = DateTimePickerFormat.Short, .Value = Date.Today, .Margin = New Padding(0, 3, 6, 0)}

            Dim lblMethod As New Label() With {.Text = "Method:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            cboMethodFilter = New ComboBox() With {.Size = New Size(105, 26), .DropDownStyle = ComboBoxStyle.DropDownList, .Margin = New Padding(0, 3, 8, 0)}
            cboMethodFilter.Items.AddRange(New Object() {"All", "Cash", "GCash", "Bank Transfer"})
            cboMethodFilter.SelectedIndex = 0

            btnGenerate = New Button() With {
                .Text = "Generate",
                .Size = New Size(85, 28),
                .BackColor = Color.FromArgb(37, 99, 235),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(0, 2, 0, 0)
            }
            btnGenerate.FlatAppearance.BorderSize = 0
            AddHandler btnGenerate.Click, Sub() GenerateReport()

            flpFilters.Controls.AddRange(New Control() {lblType, cboReportType, lblFrom, dtpFrom, lblTo, dtpTo, lblMethod, cboMethodFilter, btnGenerate})

            lblSummary = New Label() With {
                .Text = "",
                .Dock = DockStyle.Right,
                .TextAlign = ContentAlignment.MiddleRight,
                .AutoSize = True,
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(37, 99, 235),
                .Padding = New Padding(0, 6, 0, 0)
            }

            pnlFilterRow.Controls.Add(flpFilters)
            pnlFilterRow.Controls.Add(lblSummary)

            pnlTop.Controls.Add(pnlFilterRow)
            pnlTop.Controls.Add(pnlHeaderRow)

            ' Main DataGridView
            dgvReport = New DataGridView() With {
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
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55)
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvReport.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            dgvReport.ColumnHeadersHeight = 35
            dgvReport.EnableHeadersVisualStyles = False
            AddHandler dgvReport.CellFormatting, AddressOf HandleCellFormatting

            Me.Controls.Add(dgvReport)
            Me.Controls.Add(pnlTop)
        End Sub

        Private Sub HandleCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If dgvReport.Columns(e.ColumnIndex).Name = "Status" AndAlso e.Value IsNot Nothing Then
                Dim status = e.Value.ToString()
                If String.Equals(status, "Active", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129)
                    e.CellStyle.Font = New Font(dgvReport.Font, FontStyle.Bold)
                ElseIf String.Equals(status, "Expiring Soon", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(245, 158, 11)
                    e.CellStyle.Font = New Font(dgvReport.Font, FontStyle.Bold)
                Else
                    e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68)
                End If
            ElseIf (dgvReport.Columns(e.ColumnIndex).Name = "Total Revenue" OrElse dgvReport.Columns(e.ColumnIndex).Name = "TotalRevenue" OrElse dgvReport.Columns(e.ColumnIndex).Name = "Amount") AndAlso e.Value IsNot Nothing Then
                Dim val As Decimal
                If Decimal.TryParse(e.Value.ToString(), val) Then
                    e.Value = val.ToString("C")
                    e.FormattingApplied = True
                End If
            End If
        End Sub

        Private Sub HandleReportTypeChanged(sender As Object, e As EventArgs)
            Dim isRevenue = (cboReportType.SelectedIndex = 0)
            cboMethodFilter.Enabled = isRevenue
        End Sub

        Public Sub GenerateReport()
            Dim rType = cboReportType.SelectedItem.ToString()
            Dim startDate = dtpFrom.Value.Date
            Dim endDate = dtpTo.Value.Date

            Select Case rType
                Case "Revenue Report"
                    Dim method = cboMethodFilter.SelectedItem.ToString()
                    _currentReportData = _reportController.GetRevenueReport(startDate, endDate, method)
                    Dim totalRev As Decimal = 0D
                    For Each row As DataRow In _currentReportData.Rows
                        totalRev += Convert.ToDecimal(row("Total Revenue"))
                    Next
                    lblSummary.Text = $"Total Revenue: {totalRev:C}"

                Case "Attendance Report"
                    _currentReportData = _reportController.GetAttendanceReport(startDate, endDate)
                    lblSummary.Text = $"Total Visits: {_currentReportData.Rows.Count}"

                Case "Membership Report"
                    _currentReportData = _reportController.GetMembershipReport()
                    lblSummary.Text = $"Total Subscriptions: {_currentReportData.Rows.Count}"

                Case "Monthly Revenue Trend"
                    _currentReportData = _reportController.GetMonthlyRevenueTrend()
                    lblSummary.Text = $"Trend Records: {_currentReportData.Rows.Count} Months"
            End Select

            dgvReport.DataSource = _currentReportData
            If dgvReport.Columns.Contains("ID") Then dgvReport.Columns("ID").Visible = False

            ' Align columns
            For Each col As DataGridViewColumn In dgvReport.Columns
                If col.Name.Contains("Date") OrElse col.Name.Contains("Time") OrElse col.Name = "MonthKey" OrElse col.Name = "MonthLabel" OrElse col.Name = "Member No" OrElse col.Name = "Payment Method" OrElse col.Name = "Status" OrElse col.Name = "Days Remaining" OrElse col.Name = "Minutes Spent" OrElse col.Name = "Transactions" OrElse col.Name = "TransactionCount" Then
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                ElseIf col.Name = "Total Revenue" OrElse col.Name = "TotalRevenue" OrElse col.Name = "Amount" Then
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            Next
        End Sub

        Private Sub HandleExportCSV(sender As Object, e As EventArgs)
            If _currentReportData Is Nothing OrElse _currentReportData.Rows.Count = 0 Then
                MessageBox.Show("There is no data in the current report to export.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
                sfd.FileName = $"{cboReportType.SelectedItem.ToString().Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmm}.csv"
                If sfd.ShowDialog(Me) = DialogResult.OK Then
                    Try
                        Dim sb As New StringBuilder()

                        ' Column headers
                        Dim colNames = _currentReportData.Columns.Cast(Of DataColumn)().Select(Function(c) $"""{c.ColumnName}""")
                        sb.AppendLine(String.Join(",", colNames))

                        ' Data rows
                        For Each row As DataRow In _currentReportData.Rows
                            Dim fields = row.ItemArray.Select(Function(f) $"""{f?.ToString().Replace("""", """""")}""")
                            sb.AppendLine(String.Join(",", fields))
                        Next

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8)
                        MessageBox.Show($"Report successfully exported to:{Environment.NewLine}{sfd.FileName}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show($"Export failed: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using
        End Sub
    End Class
End Namespace
