Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Operational staff dashboard tailored for front-desk workflows.
    ''' </summary>
    Public Class StaffDashboardForm
        Inherits Form

        Private ReadOnly _reportController As New ReportController()
        Private ReadOnly _membershipController As New MembershipController()
        Private ReadOnly _attendanceController As New AttendanceController()
        Private ReadOnly _paymentController As New PaymentController()

        Private pnlCards As FlowLayoutPanel
        Private dgvInside As DataGridView
        Private dgvRecentPayments As DataGridView
        Private btnRefresh As Button

        Public Sub New()
            InitializeComponents()
            LoadDashboardData()
        End Sub

        Private Sub InitializeComponents()
            Me.Text = "Staff Operational Dashboard"
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
                .Text = "Front Desk Daily Operations",
                .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(17, 24, 39),
                .Dock = DockStyle.Left,
                .AutoSize = True,
                .TextAlign = ContentAlignment.MiddleLeft
            }

            btnRefresh = New Button() With {
                .Text = "↻ Refresh",
                .Size = New Size(100, 32),
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

            pnlCards = New FlowLayoutPanel() With {
                .Dock = DockStyle.Top,
                .Height = 150,
                .AutoScroll = True,
                .Padding = New Padding(15, 12, 15, 8),
                .BackColor = Color.FromArgb(249, 250, 251)
            }

            Dim pnlContent As New TableLayoutPanel() With {
                .Dock = DockStyle.Fill,
                .ColumnCount = 2,
                .RowCount = 1,
                .Padding = New Padding(15, 0, 15, 15),
                .BackColor = Color.FromArgb(249, 250, 251)
            }
            pnlContent.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
            pnlContent.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))

            ' Table 1: Members currently inside
            Dim pnlInside As New Panel() With {.Dock = DockStyle.Fill, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Margin = New Padding(0, 0, 8, 0)}
            Dim pnlInTitle As New Panel() With {.Dock = DockStyle.Top, .Height = 40, .BackColor = Color.FromArgb(249, 250, 251), .Padding = New Padding(12, 10, 12, 5)}
            Dim lblInsideTitle As New Label() With {.Text = "📍 Members Currently Inside Gym", .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold), .ForeColor = Color.FromArgb(31, 41, 55), .Dock = DockStyle.Fill}
            pnlInTitle.Controls.Add(lblInsideTitle)
            dgvInside = CreateStyledGrid()
            pnlInside.Controls.Add(dgvInside)
            pnlInside.Controls.Add(pnlInTitle)
            pnlInTitle.SendToBack()

            ' Table 2: Recent transactions
            Dim pnlPayments As New Panel() With {.Dock = DockStyle.Fill, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Margin = New Padding(8, 0, 0, 0)}
            Dim pnlPayTitle As New Panel() With {.Dock = DockStyle.Top, .Height = 40, .BackColor = Color.FromArgb(249, 250, 251), .Padding = New Padding(12, 10, 12, 5)}
            Dim lblPayTitle As New Label() With {.Text = "🧾 Recent Transactions", .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold), .ForeColor = Color.FromArgb(31, 41, 55), .Dock = DockStyle.Fill}
            pnlPayTitle.Controls.Add(lblPayTitle)
            dgvRecentPayments = CreateStyledGrid()
            pnlPayments.Controls.Add(dgvRecentPayments)
            pnlPayments.Controls.Add(pnlPayTitle)
            pnlPayTitle.SendToBack()

            pnlContent.Controls.Add(pnlInside, 0, 0)
            pnlContent.Controls.Add(pnlPayments, 1, 0)

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

            pnlCards.Controls.Add(CreateStatCard("ACTIVE MEMBERS", stats.ActiveMembers.ToString(), "Registered active members", Color.FromArgb(37, 99, 235)))
            pnlCards.Controls.Add(CreateStatCard("CURRENTLY INSIDE", stats.CurrentlyInGym.ToString(), "Active check-ins in gym", Color.FromArgb(16, 185, 129)))
            pnlCards.Controls.Add(CreateStatCard("TODAY CHECK-INS", stats.TodayAttendance.ToString(), "Total gym visits today", Color.FromArgb(139, 92, 246)))
            pnlCards.Controls.Add(CreateStatCard("ACTIVE PLANS", stats.ActiveMemberships.ToString(), $"{stats.ExpiringMemberships} expiring soon", Color.FromArgb(245, 158, 11)))

            ' Populate Members Currently Inside
            Dim insideList = _attendanceController.GetCurrentlyInside()
            Dim dtInside As New DataTable()
            dtInside.Columns.Add("Member No")
            dtInside.Columns.Add("Name")
            dtInside.Columns.Add("Check-In Time")
            dtInside.Columns.Add("Duration")
            For Each item In insideList
                Dim checkInStr As String = If(item.CheckIn.Date = Date.Today, item.CheckIn.ToString("hh:mm:ss tt"), item.CheckIn.ToString("yyyy-MM-dd hh:mm:ss tt"))
                dtInside.Rows.Add(item.MembershipNumber, item.MemberName, checkInStr, item.DurationText)
            Next
            dgvInside.DataSource = dtInside

            ' Populate Recent Transactions
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
        End Sub

        Private Function CreateStatCard(title As String, value As String, subtext As String, accentColor As Color) As Panel
            Dim card As New Panel() With {
                .Size = New Size(220, 120),
                .BackColor = Color.White,
                .Margin = New Padding(0, 0, 15, 10),
                .BorderStyle = BorderStyle.FixedSingle
            }

            Dim pnlBar As New Panel() With {.Dock = DockStyle.Top, .Height = 4, .BackColor = accentColor}
            Dim lblT As New Label() With {.Text = title, .Font = New Font("Segoe UI", 8.0F, FontStyle.Bold), .ForeColor = Color.FromArgb(107, 114, 128), .Location = New Point(14, 14), .AutoSize = True}
            Dim lblV As New Label() With {.Text = value, .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold), .ForeColor = Color.FromArgb(17, 24, 39), .Location = New Point(14, 36), .AutoSize = True}
            Dim lblS As New Label() With {.Text = subtext, .Font = New Font("Segoe UI", 8.5F, FontStyle.Regular), .ForeColor = Color.FromArgb(75, 85, 99), .Location = New Point(14, 80), .AutoSize = True}

            card.Controls.AddRange(New Control() {pnlBar, lblT, lblV, lblS})
            Return card
        End Function
    End Class
End Namespace
