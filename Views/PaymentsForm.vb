Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Cashiering and Payments Form for managing transactions, verifying payments, and re-printing receipts.
    ''' </summary>
    Public Class PaymentsForm
        Inherits Form

        Private ReadOnly _paymentController As New PaymentController()

        Private pnlTop As Panel
        Private pnlHeaderRow As Panel
        Private pnlFilterRow As Panel
        Private flpActions As FlowLayoutPanel
        Private flpFilters As FlowLayoutPanel
        Private txtSearch As TextBox
        Private cboMethodFilter As ComboBox
        Private dtpFrom As DateTimePicker
        Private dtpTo As DateTimePicker
        Private btnFilter As Button
        Private btnProcessPayment As Button
        Private btnPrintReceipt As Button
        Private btnReset As Button
        Private dgvPayments As DataGridView
        Private lblTotalRevenue As Label

        Public Sub New()
            InitializeComponents()
            LoadPayments()
        End Sub

        Private Sub InitializeComponents()
            Me.Text = "Cashiering & Payments"
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
                .Text = "💳 Cashiering & Transaction Register",
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

            btnPrintReceipt = New Button() With {
                .Text = "🖨️ Print Receipt",
                .Size = New Size(130, 32),
                .BackColor = Color.FromArgb(243, 244, 246),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 2, 0, 2)
            }
            btnPrintReceipt.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnPrintReceipt.Click, AddressOf HandlePrintReceipt

            btnProcessPayment = New Button() With {
                .Text = "➕ Process Payment",
                .Size = New Size(150, 32),
                .BackColor = Color.FromArgb(16, 185, 129),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 2, 0, 2)
            }
            btnProcessPayment.FlatAppearance.BorderSize = 0
            AddHandler btnProcessPayment.Click, AddressOf HandleProcessPayment

            flpActions.Controls.Add(btnPrintReceipt)
            flpActions.Controls.Add(btnProcessPayment)

            pnlHeaderRow.Controls.Add(lblTitle)
            pnlHeaderRow.Controls.Add(flpActions)

            ' Row 2: Search, Filters & Total Revenue
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
            txtSearch = New TextBox() With {.Size = New Size(170, 26), .PlaceholderText = "Ref No, Member...", .Margin = New Padding(0, 3, 6, 0)}
            AddHandler txtSearch.KeyDown, Sub(s, e)
                                              If e.KeyCode = Keys.Enter Then
                                                  LoadPayments()
                                                  e.SuppressKeyPress = True
                                              End If
                                          End Sub

            Dim lblM As New Label() With {.Text = "Method:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            cboMethodFilter = New ComboBox() With {.Size = New Size(110, 26), .DropDownStyle = ComboBoxStyle.DropDownList, .Margin = New Padding(0, 3, 6, 0)}
            cboMethodFilter.Items.AddRange(New Object() {"All", "Cash", "GCash", "Bank Transfer"})
            cboMethodFilter.SelectedIndex = 0

            Dim lblFrom As New Label() With {.Text = "From:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            dtpFrom = New DateTimePicker() With {.Size = New Size(115, 26), .Format = DateTimePickerFormat.Short, .Value = Date.Today.AddMonths(-1), .Margin = New Padding(0, 3, 6, 0)}

            Dim lblTo As New Label() With {.Text = "To:", .AutoSize = True, .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold), .Margin = New Padding(0, 6, 4, 0)}
            dtpTo = New DateTimePicker() With {.Size = New Size(115, 26), .Format = DateTimePickerFormat.Short, .Value = Date.Today, .Margin = New Padding(0, 3, 6, 0)}

            btnFilter = New Button() With {.Text = "Filter", .Size = New Size(65, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 4, 0)}
            btnFilter.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnFilter.Click, Sub() LoadPayments()

            btnReset = New Button() With {.Text = "Reset", .Size = New Size(65, 28), .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 2, 0, 0)}
            btnReset.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnReset.Click, Sub()
                                           txtSearch.Clear()
                                           cboMethodFilter.SelectedIndex = 0
                                           dtpFrom.Value = Date.Today.AddMonths(-1)
                                           dtpTo.Value = Date.Today
                                           LoadPayments()
                                       End Sub

            flpFilters.Controls.AddRange(New Control() {lblS, txtSearch, lblM, cboMethodFilter, lblFrom, dtpFrom, lblTo, dtpTo, btnFilter, btnReset})

            lblTotalRevenue = New Label() With {
                .Text = "Total: ₱0.00",
                .Dock = DockStyle.Right,
                .TextAlign = ContentAlignment.MiddleRight,
                .AutoSize = True,
                .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(16, 185, 129),
                .Padding = New Padding(0, 6, 0, 0)
            }

            pnlFilterRow.Controls.Add(flpFilters)
            pnlFilterRow.Controls.Add(lblTotalRevenue)

            pnlTop.Controls.Add(pnlFilterRow)
            pnlTop.Controls.Add(pnlHeaderRow)

            ' Main DataGridView
            dgvPayments = New DataGridView() With {
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
            dgvPayments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55)
            dgvPayments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvPayments.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            dgvPayments.ColumnHeadersHeight = 35
            dgvPayments.EnableHeadersVisualStyles = False
            AddHandler dgvPayments.CellFormatting, AddressOf HandleCellFormatting

            Me.Controls.Add(dgvPayments)
            Me.Controls.Add(pnlTop)
        End Sub

        Private Sub HandleCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If dgvPayments.Columns(e.ColumnIndex).Name = "Status" AndAlso e.Value IsNot Nothing Then
                Dim status = e.Value.ToString()
                If String.Equals(status, "Paid", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129)
                    e.CellStyle.Font = New Font(dgvPayments.Font, FontStyle.Bold)
                Else
                    e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68)
                End If
            End If
        End Sub

        Public Sub LoadPayments()
            Dim search = txtSearch.Text.Trim()
            Dim method = If(cboMethodFilter.SelectedItem IsNot Nothing, cboMethodFilter.SelectedItem.ToString(), "All")
            Dim startDate = dtpFrom.Value.Date
            Dim endDate = dtpTo.Value.Date
            Dim list = _paymentController.GetPayments(search, method, startDate, endDate)

            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Ref No", GetType(String))
            dt.Columns.Add("Member Card", GetType(String))
            dt.Columns.Add("Member Name", GetType(String))
            dt.Columns.Add("Plan / Item", GetType(String))
            dt.Columns.Add("Amount", GetType(String))
            dt.Columns.Add("Method", GetType(String))
            dt.Columns.Add("Payment Date", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("Cashier", GetType(String))

            Dim total As Decimal = 0D
            For Each p In list
                dt.Rows.Add(p.PaymentID, p.ReferenceNumber, p.MembershipNumber, p.MemberName, p.PlanName, p.Amount.ToString("C"), p.PaymentMethod, p.PaymentDate.ToString("yyyy-MM-dd HH:mm"), p.PaymentStatus, p.ProcessedByName)
                If String.Equals(p.PaymentStatus, "Paid", StringComparison.OrdinalIgnoreCase) Then
                    total += p.Amount
                End If
            Next

            dgvPayments.DataSource = dt
            If dgvPayments.Columns.Contains("ID") Then dgvPayments.Columns("ID").Visible = False

            ' Align columns
            If dgvPayments.Columns.Contains("Ref No") Then
                dgvPayments.Columns("Ref No").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvPayments.Columns("Ref No").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvPayments.Columns.Contains("Member Card") Then
                dgvPayments.Columns("Member Card").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvPayments.Columns("Member Card").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvPayments.Columns.Contains("Amount") Then
                dgvPayments.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                dgvPayments.Columns("Amount").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            If dgvPayments.Columns.Contains("Method") Then
                dgvPayments.Columns("Method").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvPayments.Columns("Method").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvPayments.Columns.Contains("Payment Date") Then
                dgvPayments.Columns("Payment Date").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvPayments.Columns("Payment Date").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvPayments.Columns.Contains("Status") Then
                dgvPayments.Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvPayments.Columns("Status").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            lblTotalRevenue.Text = $"Total: {total:C}"
        End Sub

        Private Function GetSelectedPaymentId() As Integer?
            If dgvPayments.SelectedRows.Count > 0 Then
                Return Convert.ToInt32(dgvPayments.SelectedRows(0).Cells("ID").Value)
            End If
            Return Nothing
        End Function

        Private Sub HandleProcessPayment(sender As Object, e As EventArgs)
            Using dlg As New PaymentDialog()
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    LoadPayments()
                End If
            End Using
        End Sub

        Private Sub HandlePrintReceipt(sender As Object, e As EventArgs)
            Dim payId = GetSelectedPaymentId()
            If Not payId.HasValue Then
                MessageBox.Show("Please select a payment transaction from the table.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim payment = _paymentController.GetPaymentById(payId.Value)
            If payment IsNot Nothing Then
                _paymentController.PrintReceipt(payment, Me)
            End If
        End Sub
    End Class
End Namespace
