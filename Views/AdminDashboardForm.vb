Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Administrator Dashboard displaying comprehensive gym metrics, revenue, and active subscriptions.
    ''' </summary>
    Public Class AdminDashboardForm
        Inherits Form

        Private ReadOnly _reportController As New ReportController()
        Private ReadOnly _membershipController As New MembershipController()
        Private ReadOnly _attendanceController As New AttendanceController()
        Private ReadOnly _paymentController As New PaymentController()

        Private pnlCards As FlowLayoutPanel
        Private dgvExpiring As DataGridView
        Private dgvRecentPayments As DataGridView
        Private dgvRecentAttendance As DataGridView
        Private btnRefresh As Button

        Public Sub New()
            InitializeComponents()
            LoadDashboardData()
        End Sub

        Private Sub InitializeComponents()
            Me.Text = "Admin Dashboard"
            Me.BackColor = Color.FromArgb(249, 250, 251)
            Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            Me.Dock = DockStyle.Fill
            Me.TopLevel = False
            Me.FormBorderStyle = FormBorderStyle.None

            ' Top Header
            Dim pnlTop As New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 60,
                .BackColor = Color.White,
                .Padding = New Padding(20, 14, 20, 14)
            }

            Dim lblTitle As New Label() With {
                .Text = "Executive Overview & Analytics",
                .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(17, 24, 39),
                .Dock = DockStyle.Left,
                .AutoSize = True,
                .TextAlign = ContentAlignment.MiddleLeft
            }

            btnRefresh = New Button() With {
                .Text = "↻ Refresh Data",
                .Size = New Size(125, 32),
                .Dock = DockStyle.Right,
                .BackColor = Color.FromArgb(243, 244, 246),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand
            }
            btnRefresh.FlatAppearance.BorderSize = 1
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnRefresh.Click, Sub() LoadDashboardData()

            pnlTop.Controls.Add(lblTitle)
            pnlTop.Controls.Add(btnRefresh)

            ' Metric cards container
            pnlCards = New FlowLayoutPanel() With {
                .Dock = DockStyle.Top,
                .Height = 155,
                .AutoScroll = True,
                .Padding = New Padding(15, 12, 15, 8),
                .BackColor = Color.FromArgb(249, 250, 251)
            }

            ' Main content panels for tables
            Dim pnlContent As New TableLayoutPanel() With {
                .Dock = DockStyle.Fill,
                .ColumnCount = 3,
                .RowCount = 1,
                .Padding = New Padding(15, 0, 15, 15),
                .BackColor = Color.FromArgb(249, 250, 251)
            }
            pnlContent.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 34.0F))
            pnlContent.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.0F))
            pnlContent.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.0F))

            ' Card 1: Expiring Memberships
            Dim pnlExpiring As New Panel() With {.Dock = DockStyle.Fill, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Margin = New Padding(0, 0, 8, 0)}
            Dim pnlExpTitle As New Panel() With {.Dock = DockStyle.Top, .Height = 40, .BackColor = Color.FromArgb(249, 250, 251), .Padding = New Padding(12, 10, 12, 5)}
            Dim lblExpiringTitle As New Label() With {.Text = "⚠️ Expiring Memberships (Next 7 Days)", .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold), .ForeColor = Color.FromArgb(31, 41, 55), .Dock = DockStyle.Fill}
            pnlExpTitle.Controls.Add(lblExpiringTitle)
            dgvExpiring = CreateStyledGrid()
            pnlExpiring.Controls.Add(dgvExpiring)
            pnlExpiring.Controls.Add(pnlExpTitle)
            pnlExpTitle.SendToBack()

            ' Card 2: Recent Payments
            Dim pnlPayments As New Panel() With {.Dock = DockStyle.Fill, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Margin = New Padding(4, 0, 4, 0)}
            Dim pnlPayTitle As New Panel() With {.Dock = DockStyle.Top, .Height = 40, .BackColor = Color.FromArgb(249, 250, 251), .Padding = New Padding(12, 10, 12, 5)}
            Dim lblPaymentsTitle As New Label() With {.Text = "💳 Recent Payments", .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold), .ForeColor = Color.FromArgb(31, 41, 55), .Dock = DockStyle.Fill}
            pnlPayTitle.Controls.Add(lblPaymentsTitle)
            dgvRecentPayments = CreateStyledGrid()
            pnlPayments.Controls.Add(dgvRecentPayments)
            pnlPayments.Controls.Add(pnlPayTitle)
            pnlPayTitle.SendToBack()

            ' Card 3: Today's Check-ins
            Dim pnlAttendance As New Panel() With {.Dock = DockStyle.Fill, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Margin = New Padding(8, 0, 0, 0)}
            Dim pnlAttTitle As New Panel() With {.Dock = DockStyle.Top, .Height = 40, .BackColor = Color.FromArgb(249, 250, 251), .Padding = New Padding(12, 10, 12, 5)}
            Dim lblAttendanceTitle As New Label() With {.Text = "🏋 Today's Attendance", .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold), .ForeColor = Color.FromArgb(31, 41, 55), .Dock = DockStyle.Fill}
            pnlAttTitle.Controls.Add(lblAttendanceTitle)
            dgvRecentAttendance = CreateStyledGrid()
            pnlAttendance.Controls.Add(dgvRecentAttendance)
            pnlAttendance.Controls.Add(pnlAttTitle)
            pnlAttTitle.SendToBack()

            pnlContent.Controls.Add(pnlExpiring, 0, 0)
            pnlContent.Controls.Add(pnlPayments, 1, 0)
            pnlContent.Controls.Add(pnlAttendance, 2, 0)

            Me.Controls.Add(pnlContent)
            Me.Controls.Add(pnlCards)
            Me.Controls.Add(pnlTop)
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
                .Font = New Font("Segoe UI", 8.5F)
            }
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55)
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            dgv.ColumnHeadersHeight = 32
            dgv.EnableHeadersVisualStyles = False
            Return dgv
        End Function

        Public Sub LoadDashboardData()
            pnlCards.Controls.Clear()
            Dim stats As DashboardStats = _reportController.GetDashboardStats()

            ' Create stat metric cards
            pnlCards.Controls.Add(CreateStatCard("TOTAL MEMBERS", stats.TotalMembers.ToString(), $"{stats.ActiveMembers} active", Color.FromArgb(37, 99, 235)))
            pnlCards.Controls.Add(CreateStatCard("ACTIVE PLANS", stats.ActiveMemberships.ToString(), $"{stats.ExpiringMemberships} expiring soon", Color.FromArgb(16, 185, 129)))
            pnlCards.Controls.Add(CreateStatCard("TODAY CHECK-INS", stats.TodayAttendance.ToString(), $"{stats.CurrentlyInGym} currently inside", Color.FromArgb(139, 92, 246)))
            pnlCards.Controls.Add(CreateStatCard("TODAY REVENUE", stats.TodayRevenue.ToString("C"), $"Month: {stats.MonthlyRevenue:C}", Color.FromArgb(245, 158, 11)))
            pnlCards.Controls.Add(CreateStatCard("MONTHLY REVENUE", stats.MonthlyRevenue.ToString("C"), $"Year: {stats.YearlyRevenue:C}", Color.FromArgb(239, 68, 68)))

            ' Populate Expiring Memberships
            Dim expiring = _membershipController.GetMemberships(statusFilter:="Expiring Soon")
            Dim dtExp As New DataTable()
            dtExp.Columns.Add("Member")
            dtExp.Columns.Add("Plan")
            dtExp.Columns.Add("End Date")
            dtExp.Columns.Add("Days Left")
            For Each ms In expiring.Take(15)
                dtExp.Rows.Add(ms.MemberName, ms.PlanName, ms.EndDate.ToString("yyyy-MM-dd"), ms.DaysRemaining)
            Next
            dgvExpiring.DataSource = dtExp

            ' Populate Recent Payments
            Dim payments = _paymentController.GetPayments().Take(15).ToList()
            Dim dtPay As New DataTable()
            dtPay.Columns.Add("Ref No")
            dtPay.Columns.Add("Member")
            dtPay.Columns.Add("Amount")
            dtPay.Columns.Add("Method")
            For Each p In payments
                dtPay.Rows.Add(p.ReferenceNumber, p.MemberName, p.Amount.ToString("C"), p.PaymentMethod)
            Next
            dgvRecentPayments.DataSource = dtPay

            ' Populate Recent Attendance
            Dim attendance = _attendanceController.GetHistory(targetDate:=Date.Today).Take(15).ToList()
            Dim dtAtt As New DataTable()
            dtAtt.Columns.Add("Member")
            dtAtt.Columns.Add("In")
            dtAtt.Columns.Add("Status")
            For Each a In attendance
                dtAtt.Rows.Add(a.MemberName, a.CheckIn.ToString("HH:mm"), If(a.CheckOut.HasValue, a.CheckOut.Value.ToString("HH:mm"), "Inside"))
            Next
            dgvRecentAttendance.DataSource = dtAtt
        End Sub

        Private Function CreateStatCard(title As String, value As String, subtext As String, accentColor As Color) As Panel
            Dim card As New Panel() With {
                .Size = New Size(220, 125),
                .BackColor = Color.White,
                .Margin = New Padding(0, 0, 15, 10),
                .BorderStyle = BorderStyle.FixedSingle
            }

            Dim pnlBar As New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 4,
                .BackColor = accentColor
            }

            Dim lblT As New Label() With {
                .Text = title,
                .Font = New Font("Segoe UI", 8.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(107, 114, 128),
                .Location = New Point(14, 14),
                .AutoSize = True
            }

            Dim lblV As New Label() With {
                .Text = value,
                .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(17, 24, 39),
                .Location = New Point(14, 36),
                .AutoSize = True
            }

            Dim lblS As New Label() With {
                .Text = subtext,
                .Font = New Font("Segoe UI", 8.5F, FontStyle.Regular),
                .ForeColor = Color.FromArgb(75, 85, 99),
                .Location = New Point(14, 82),
                .AutoSize = True
            }

            card.Controls.AddRange(New Control() {pnlBar, lblT, lblV, lblS})
            Return card
        End Function
    End Class
End Namespace
