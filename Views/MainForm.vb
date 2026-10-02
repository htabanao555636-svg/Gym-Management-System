Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Views
    ''' <summary>
    ''' Main application navigation shell providing sidebar navigation, top status bar, and dynamic view hosting.
    ''' </summary>
    Public Class MainForm
        Inherits Form

        Private ReadOnly _authController As New AuthController()

        Private pnlSidebar As Panel
        Private pnlHeader As Panel
        Private pnlContent As Panel
        Private lblAppBrand As Label
        Private lblCurrentViewTitle As Label
        Private lblUserBadge As Label
        Private lblClock As Label
        Private tmrClock As Timer
        Private pnlNavItems As FlowLayoutPanel
        Private flpHeaderRight As FlowLayoutPanel

        Private _currentActiveButton As Button
        Private _currentHostedForm As Form

        Public Sub New()
            InitializeComponents()
            CheckAuthentication()
            BuildSidebarMenu()
            StartClock()
            NavigateToDefaultDashboard()
        End Sub

        Private Sub InitializeComponents()
            Me.Text = "Titan Fitness - Gym Management System"
            Me.Size = New Size(1280, 800)
            Me.MinimumSize = New Size(1024, 700)
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.BackColor = Color.FromArgb(243, 244, 246)
            Me.Font = New Font("Segoe UI", 9.0F)

            ' 1. Left Sidebar
            pnlSidebar = New Panel() With {
                .Dock = DockStyle.Left,
                .Width = 230,
                .BackColor = Color.FromArgb(17, 24, 39)
            }

            Dim pnlBrand As New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 70,
                .BackColor = Color.FromArgb(15, 23, 42),
                .Padding = New Padding(18, 12, 10, 10)
            }

            lblAppBrand = New Label() With {
                .Text = "🏋 TITAN FITNESS",
                .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(249, 115, 22),
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleLeft
            }
            pnlBrand.Controls.Add(lblAppBrand)

            pnlNavItems = New FlowLayoutPanel() With {
                .Dock = DockStyle.Fill,
                .FlowDirection = FlowDirection.TopDown,
                .WrapContents = False,
                .AutoScroll = True,
                .Padding = New Padding(10, 12, 10, 12)
            }

            pnlSidebar.Controls.Add(pnlNavItems)
            pnlSidebar.Controls.Add(pnlBrand)

            ' 2. Top Header
            pnlHeader = New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 60,
                .BackColor = Color.White,
                .Padding = New Padding(20, 0, 20, 0)
            }

            lblCurrentViewTitle = New Label() With {
                .Text = "Dashboard",
                .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(17, 24, 39),
                .Dock = DockStyle.Left,
                .AutoSize = True,
                .TextAlign = ContentAlignment.MiddleLeft
            }

            ' Header right panel for Clock and User badge
            flpHeaderRight = New FlowLayoutPanel() With {
                .Dock = DockStyle.Right,
                .FlowDirection = FlowDirection.RightToLeft,
                .WrapContents = False,
                .AutoSize = True,
                .Padding = New Padding(0, 15, 0, 10)
            }

            lblUserBadge = New Label() With {
                .Text = "👤 User: Admin",
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(37, 99, 235),
                .AutoSize = True,
                .Margin = New Padding(15, 5, 0, 0)
            }

            lblClock = New Label() With {
                .Text = DateTime.Now.ToString("dddd, MMM dd, yyyy  HH:mm:ss"),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
                .ForeColor = Color.FromArgb(107, 114, 128),
                .AutoSize = True,
                .Margin = New Padding(0, 6, 0, 0)
            }

            flpHeaderRight.Controls.Add(lblUserBadge)
            flpHeaderRight.Controls.Add(lblClock)

            pnlHeader.Controls.Add(lblCurrentViewTitle)
            pnlHeader.Controls.Add(flpHeaderRight)

            ' 3. Main Content Container
            pnlContent = New Panel() With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.FromArgb(249, 250, 251)
            }

            Me.Controls.Add(pnlContent)
            Me.Controls.Add(pnlHeader)
            Me.Controls.Add(pnlSidebar)
        End Sub

        Private Sub CheckAuthentication()
            If Not SessionManager.IsLoggedIn Then
                MessageBox.Show("Please log in to continue.", "Access Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Hide()
                Dim login As New LoginForm()
                login.Show()
                Me.Close()
                Return
            End If

            Dim user = SessionManager.CurrentUser
            lblUserBadge.Text = $"👤 {user.FullName}  [{user.Role}]"
            If user.IsAdmin Then
                lblUserBadge.ForeColor = Color.FromArgb(220, 38, 38)
            Else
                lblUserBadge.ForeColor = Color.FromArgb(37, 99, 235)
            End If
        End Sub

        Private Sub StartClock()
            tmrClock = New Timer() With {.Interval = 1000}
            AddHandler tmrClock.Tick, Sub()
                                          lblClock.Text = DateTime.Now.ToString("dddd, MMM dd, yyyy  HH:mm:ss")
                                      End Sub
            tmrClock.Start()
        End Sub

        Private Sub BuildSidebarMenu()
            pnlNavItems.Controls.Clear()

            If SessionManager.IsAdmin Then
                ' ADMIN SIDEBAR NAVIGATION
                AddNavButton("📊 Dashboard", AddressOf LoadAdminDashboard)
                AddNavButton("🏋 Members", AddressOf LoadMembersView)
                AddNavButton("⭐ Memberships", AddressOf LoadMembershipsView)
                AddNavButton("📋 Membership Plans", AddressOf LoadMembershipPlansView)
                AddNavButton("💳 Payments", AddressOf LoadPaymentsView)
                AddNavButton("⏱️ Attendance", AddressOf LoadAttendanceView)
                AddNavButton("👥 Staff Management", AddressOf LoadStaffManagementView)
                AddNavButton("📈 Reports", AddressOf LoadReportsView)
                AddNavButton("📜 Activity Logs", AddressOf LoadActivityLogsView)
                AddNavButton("⚙️ Settings", AddressOf LoadSettingsView)
            Else
                ' STAFF SIDEBAR NAVIGATION
                AddNavButton("📊 Dashboard", AddressOf LoadStaffDashboard)
                AddNavButton("🏋 Members", AddressOf LoadMembersView)
                AddNavButton("⭐ Memberships", AddressOf LoadMembershipsView)
                AddNavButton("💳 Payments", AddressOf LoadPaymentsView)
                AddNavButton("⏱️ Attendance", AddressOf LoadAttendanceView)
                AddNavButton("📋 Membership Plans", AddressOf LoadMembershipPlansView)
            End If

            ' Logout button at the bottom of navigation
            Dim btnLogout = CreateNavButton("🚪 Logout", AddressOf HandleLogout)
            btnLogout.ForeColor = Color.FromArgb(239, 68, 68)
            pnlNavItems.Controls.Add(btnLogout)
        End Sub

        Private Function CreateNavButton(text As String, handler As EventHandler) As Button
            Dim btn As New Button() With {
                .Text = $"  {text}",
                .Size = New Size(208, 42),
                .FlatStyle = FlatStyle.Flat,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular),
                .ForeColor = Color.FromArgb(209, 213, 219),
                .BackColor = Color.Transparent,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(0, 2, 0, 2)
            }
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(31, 41, 55)
            AddHandler btn.Click, handler
            Return btn
        End Function

        Private Sub AddNavButton(text As String, handler As EventHandler)
            Dim btn = CreateNavButton(text, handler)
            pnlNavItems.Controls.Add(btn)
        End Sub

        Private Sub SetActiveButton(btn As Button)
            If _currentActiveButton IsNot Nothing Then
                _currentActiveButton.BackColor = Color.Transparent
                _currentActiveButton.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
            End If
            _currentActiveButton = btn
            If _currentActiveButton IsNot Nothing Then
                _currentActiveButton.BackColor = Color.FromArgb(31, 41, 55)
                _currentActiveButton.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            End If
        End Sub

        Public Sub HostView(title As String, form As Form, Optional senderBtn As Button = Nothing)
            If senderBtn IsNot Nothing Then
                SetActiveButton(senderBtn)
            End If

            lblCurrentViewTitle.Text = title

            If _currentHostedForm IsNot Nothing Then
                _currentHostedForm.Hide()
                pnlContent.Controls.Remove(_currentHostedForm)
                _currentHostedForm.Dispose()
                _currentHostedForm = Nothing
            End If

            _currentHostedForm = form
            form.TopLevel = False
            form.FormBorderStyle = FormBorderStyle.None
            form.Dock = DockStyle.Fill
            pnlContent.Controls.Add(form)
            form.Show()
        End Sub

        Private Sub NavigateToDefaultDashboard()
            If pnlNavItems.Controls.Count > 0 AndAlso TypeOf pnlNavItems.Controls(0) Is Button Then
                Dim firstBtn = DirectCast(pnlNavItems.Controls(0), Button)
                If SessionManager.IsAdmin Then
                    LoadAdminDashboard(firstBtn, EventArgs.Empty)
                Else
                    LoadStaffDashboard(firstBtn, EventArgs.Empty)
                End If
            End If
        End Sub

        ' Navigation Handlers
        Private Sub LoadAdminDashboard(sender As Object, e As EventArgs)
            HostView("Executive Dashboard", New AdminDashboardForm(), TryCast(sender, Button))
        End Sub

        Private Sub LoadStaffDashboard(sender As Object, e As EventArgs)
            HostView("Front Desk Dashboard", New StaffDashboardForm(), TryCast(sender, Button))
        End Sub

        Private Sub LoadMembersView(sender As Object, e As EventArgs)
            HostView("Member Management Directory", New MembersForm(), TryCast(sender, Button))
        End Sub

        Private Sub LoadMembershipsView(sender As Object, e As EventArgs)
            HostView("Member Subscriptions & Renewals", New MembershipsListForm(), TryCast(sender, Button))
        End Sub

        Private Sub LoadMembershipPlansView(sender As Object, e As EventArgs)
            HostView("Membership Plans & Packages", New MembershipPlansForm(), TryCast(sender, Button))
        End Sub

        Private Sub LoadPaymentsView(sender As Object, e As EventArgs)
            HostView("Cashiering & Payments Register", New PaymentsForm(), TryCast(sender, Button))
        End Sub

        Private Sub LoadAttendanceView(sender As Object, e As EventArgs)
            HostView("Attendance Check-In / Check-Out Station", New AttendanceForm(), TryCast(sender, Button))
        End Sub

        Private Sub LoadStaffManagementView(sender As Object, e As EventArgs)
            Try
                PermissionHelper.RequirePermission(PermissionHelper.AppFeature.StaffManagement)
                HostView("Staff & User Accounts", New StaffManagementForm(), TryCast(sender, Button))
            Catch ex As UnauthorizedAccessException
                MessageBox.Show(ex.Message, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Sub LoadReportsView(sender As Object, e As EventArgs)
            HostView("Reports & Analytics", New ReportsForm(), TryCast(sender, Button))
        End Sub

        Private Sub LoadActivityLogsView(sender As Object, e As EventArgs)
            Try
                PermissionHelper.RequirePermission(PermissionHelper.AppFeature.ActivityLogs)
                HostView("System Activity Audit Trail", New ActivityLogsForm(), TryCast(sender, Button))
            Catch ex As UnauthorizedAccessException
                MessageBox.Show(ex.Message, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Sub LoadSettingsView(sender As Object, e As EventArgs)
            Try
                PermissionHelper.RequirePermission(PermissionHelper.AppFeature.Settings)
                HostView("Gym & System Settings", New SettingsForm(), TryCast(sender, Button))
            Catch ex As UnauthorizedAccessException
                MessageBox.Show(ex.Message, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Sub HandleLogout(sender As Object, e As EventArgs)
            If MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                tmrClock?.Stop()
                _authController.Logout()
                Me.Hide()
                Dim login As New LoginForm()
                login.Show()
                Me.Close()
            End If
        End Sub
    End Class
End Namespace
