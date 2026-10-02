Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Views
    ''' <summary>
    ''' Membership Plans View: Admin full management, Staff view-only.
    ''' </summary>
    Public Class MembershipPlansForm
        Inherits Form

        Private ReadOnly _planController As New MembershipPlanController()

        Private pnlTop As Panel
        Private pnlHeaderRow As Panel
        Private flpActions As FlowLayoutPanel
        Private btnAddPlan As Button
        Private btnEditPlan As Button
        Private btnToggleStatus As Button
        Private dgvPlans As DataGridView
        Private lblNotice As Label

        Public Sub New()
            InitializeComponents()
            ApplyRolePermissions()
            LoadPlans()
        End Sub

        Private Sub InitializeComponents()
            Me.Text = "Membership Plans"
            Me.BackColor = Color.FromArgb(249, 250, 251)
            Me.Font = New Font("Segoe UI", 9.0F)
            Me.Dock = DockStyle.Fill
            Me.TopLevel = False
            Me.FormBorderStyle = FormBorderStyle.None

            pnlTop = New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 65,
                .BackColor = Color.White,
                .Padding = New Padding(20, 14, 20, 14)
            }

            pnlHeaderRow = New Panel() With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.White
            }

            Dim flpTitle As New FlowLayoutPanel() With {
                .Dock = DockStyle.Left,
                .FlowDirection = FlowDirection.LeftToRight,
                .WrapContents = False,
                .AutoSize = True,
                .Padding = New Padding(0, 4, 0, 0)
            }

            Dim lblTitle As New Label() With {
                .Text = "📋 Membership Packages & Plans",
                .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(17, 24, 39),
                .AutoSize = True,
                .Margin = New Padding(0, 0, 10, 0)
            }

            lblNotice = New Label() With {
                .Text = "",
                .ForeColor = Color.FromArgb(107, 114, 128),
                .Font = New Font("Segoe UI", 8.5F, FontStyle.Italic),
                .AutoSize = True,
                .Margin = New Padding(0, 3, 0, 0)
            }

            flpTitle.Controls.Add(lblTitle)
            flpTitle.Controls.Add(lblNotice)

            flpActions = New FlowLayoutPanel() With {
                .Dock = DockStyle.Right,
                .FlowDirection = FlowDirection.RightToLeft,
                .WrapContents = False,
                .AutoSize = True
            }

            btnToggleStatus = New Button() With {
                .Text = "🔄 Status",
                .Size = New Size(100, 32),
                .BackColor = Color.FromArgb(243, 244, 246),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 0, 0, 0)
            }
            btnToggleStatus.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnToggleStatus.Click, AddressOf HandleToggleStatus

            btnEditPlan = New Button() With {
                .Text = "✏️ Edit Plan",
                .Size = New Size(110, 32),
                .BackColor = Color.FromArgb(243, 244, 246),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 0, 0, 0)
            }
            btnEditPlan.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            AddHandler btnEditPlan.Click, AddressOf HandleEditPlan

            btnAddPlan = New Button() With {
                .Text = "➕ Add Plan",
                .Size = New Size(110, 32),
                .BackColor = Color.FromArgb(37, 99, 235),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat,
                .Cursor = Cursors.Hand,
                .Margin = New Padding(6, 0, 0, 0)
            }
            btnAddPlan.FlatAppearance.BorderSize = 0
            AddHandler btnAddPlan.Click, AddressOf HandleAddPlan

            flpActions.Controls.Add(btnToggleStatus)
            flpActions.Controls.Add(btnEditPlan)
            flpActions.Controls.Add(btnAddPlan)

            pnlHeaderRow.Controls.Add(flpTitle)
            pnlHeaderRow.Controls.Add(flpActions)
            pnlTop.Controls.Add(pnlHeaderRow)

            dgvPlans = New DataGridView() With {
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
                .RowTemplate = New DataGridViewRow() With {.Height = 35}
            }
            dgvPlans.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55)
            dgvPlans.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvPlans.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            dgvPlans.ColumnHeadersHeight = 35
            dgvPlans.EnableHeadersVisualStyles = False
            AddHandler dgvPlans.CellFormatting, AddressOf HandleCellFormatting

            Me.Controls.Add(dgvPlans)
            Me.Controls.Add(pnlTop)
        End Sub

        Private Sub ApplyRolePermissions()
            If Not PermissionHelper.CanAccess(PermissionHelper.AppFeature.MembershipPlansManage) Then
                ' Staff: View only mode
                btnAddPlan.Visible = False
                btnEditPlan.Visible = False
                btnToggleStatus.Visible = False
                lblNotice.Text = "(View Only Mode - Admin permissions required to modify)"
            End If
        End Sub

        Public Sub LoadPlans()
            Dim plans = _planController.GetPlans()
            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Plan Name", GetType(String))
            dt.Columns.Add("Description", GetType(String))
            dt.Columns.Add("Duration", GetType(String))
            dt.Columns.Add("Price", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            For Each p In plans
                dt.Rows.Add(p.PlanID, p.PlanName, p.Description, p.DurationDisplay, p.Price.ToString("C"), p.Status)
            Next

            dgvPlans.DataSource = dt
            If dgvPlans.Columns.Contains("ID") Then
                dgvPlans.Columns("ID").Visible = False
            End If

            ' Format column alignments and headers
            If dgvPlans.Columns.Contains("Duration") Then
                dgvPlans.Columns("Duration").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvPlans.Columns("Duration").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If dgvPlans.Columns.Contains("Price") Then
                dgvPlans.Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                dgvPlans.Columns("Price").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            If dgvPlans.Columns.Contains("Status") Then
                dgvPlans.Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvPlans.Columns("Status").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
        End Sub

        Private Sub HandleCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If dgvPlans.Columns(e.ColumnIndex).Name = "Status" AndAlso e.Value IsNot Nothing Then
                Dim status = e.Value.ToString()
                If String.Equals(status, "Active", StringComparison.OrdinalIgnoreCase) Then
                    e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129)
                    e.CellStyle.Font = New Font(dgvPlans.Font, FontStyle.Bold)
                Else
                    e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68)
                End If
            End If
        End Sub

        Private Function GetSelectedPlanId() As Integer?
            If dgvPlans.SelectedRows.Count > 0 Then
                Return Convert.ToInt32(dgvPlans.SelectedRows(0).Cells("ID").Value)
            End If
            Return Nothing
        End Function

        Private Sub HandleAddPlan(sender As Object, e As EventArgs)
            Using dlg As New MembershipPlanDialog()
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    LoadPlans()
                End If
            End Using
        End Sub

        Private Sub HandleEditPlan(sender As Object, e As EventArgs)
            Dim planId = GetSelectedPlanId()
            If Not planId.HasValue Then
                MessageBox.Show("Please select a plan to edit.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim plan = _planController.GetPlanById(planId.Value)
            If plan IsNot Nothing Then
                Using dlg As New MembershipPlanDialog(plan)
                    If dlg.ShowDialog(Me) = DialogResult.OK Then
                        LoadPlans()
                    End If
                End Using
            End If
        End Sub

        Private Sub HandleToggleStatus(sender As Object, e As EventArgs)
            Dim planId = GetSelectedPlanId()
            If Not planId.HasValue Then
                MessageBox.Show("Please select a plan to toggle status.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim plan = _planController.GetPlanById(planId.Value)
            If plan IsNot Nothing Then
                Dim newStatus = If(plan.IsActive, "Inactive", "Active")
                If MessageBox.Show($"Change plan '{plan.PlanName}' status to '{newStatus}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim res = _planController.SetStatus(plan.PlanID, newStatus)
                    If res.Success Then
                        LoadPlans()
                    Else
                        MessageBox.Show(res.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        End Sub
    End Class
End Namespace
