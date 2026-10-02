Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Services
    ''' <summary>
    ''' Compiles reports for Revenue, Attendance, Memberships, Payments, and Dashboard Statistics.
    ''' </summary>
    Public Class ReportService
        Private ReadOnly _membershipService As New MembershipService()

        ''' <summary>
        ''' Gathers high-level summary counters for Admin & Staff Dashboards.
        ''' </summary>
        Public Function GetDashboardStats() As DashboardStats
            _membershipService.SyncExpiredMemberships()
            Dim stats As New DashboardStats()

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                ' 1. Members counters
                Dim sqlMembers As String = "
                    SELECT 
                        COUNT(*) as total,
                        SUM(CASE WHEN status = 'Active' THEN 1 ELSE 0 END) as active_cnt,
                        SUM(CASE WHEN status = 'Inactive' THEN 1 ELSE 0 END) as inactive_cnt,
                        SUM(CASE WHEN status = 'Suspended' THEN 1 ELSE 0 END) as suspended_cnt
                    FROM members;"
                Using cmd As New MySqlCommand(sqlMembers, conn)
                    Using r = cmd.ExecuteReader()
                        If r.Read() Then
                            stats.TotalMembers = Convert.ToInt32(r("total"))
                            stats.ActiveMembers = If(r.IsDBNull(r.GetOrdinal("active_cnt")), 0, Convert.ToInt32(r("active_cnt")))
                            stats.InactiveMembers = If(r.IsDBNull(r.GetOrdinal("inactive_cnt")), 0, Convert.ToInt32(r("inactive_cnt")))
                            stats.SuspendedMembers = If(r.IsDBNull(r.GetOrdinal("suspended_cnt")), 0, Convert.ToInt32(r("suspended_cnt")))
                        End If
                    End Using
                End Using

                ' 2. Memberships counters
                Dim sqlMemberships As String = "
                    SELECT 
                        SUM(CASE WHEN status = 'Active' THEN 1 ELSE 0 END) as active_ms,
                        SUM(CASE WHEN status = 'Expiring Soon' THEN 1 ELSE 0 END) as expiring_ms,
                        SUM(CASE WHEN status = 'Expired' THEN 1 ELSE 0 END) as expired_ms
                    FROM memberships;"
                Using cmd As New MySqlCommand(sqlMemberships, conn)
                    Using r = cmd.ExecuteReader()
                        If r.Read() Then
                            stats.ActiveMemberships = If(r.IsDBNull(r.GetOrdinal("active_ms")), 0, Convert.ToInt32(r("active_ms")))
                            stats.ExpiringMemberships = If(r.IsDBNull(r.GetOrdinal("expiring_ms")), 0, Convert.ToInt32(r("expiring_ms")))
                            stats.ExpiredMemberships = If(r.IsDBNull(r.GetOrdinal("expired_ms")), 0, Convert.ToInt32(r("expired_ms")))
                        End If
                    End Using
                End Using

                ' 3. Attendance counters
                Dim sqlAttendance As String = "
                    SELECT 
                        COUNT(CASE WHEN DATE(check_in) = CURDATE() THEN 1 END) as today_att,
                        COUNT(CASE WHEN check_out IS NULL THEN 1 END) as inside_cnt
                    FROM attendance;"
                Using cmd As New MySqlCommand(sqlAttendance, conn)
                    Using r = cmd.ExecuteReader()
                        If r.Read() Then
                            stats.TodayAttendance = If(r.IsDBNull(r.GetOrdinal("today_att")), 0, Convert.ToInt32(r("today_att")))
                            stats.CurrentlyInGym = If(r.IsDBNull(r.GetOrdinal("inside_cnt")), 0, Convert.ToInt32(r("inside_cnt")))
                        End If
                    End Using
                End Using

                ' 4. Revenue counters
                Dim sqlRevenue As String = "
                    SELECT 
                        SUM(CASE WHEN DATE(payment_date) = CURDATE() THEN amount ELSE 0 END) as today_rev,
                        SUM(CASE WHEN YEAR(payment_date) = YEAR(CURDATE()) AND MONTH(payment_date) = MONTH(CURDATE()) THEN amount ELSE 0 END) as month_rev,
                        SUM(CASE WHEN YEAR(payment_date) = YEAR(CURDATE()) THEN amount ELSE 0 END) as year_rev
                    FROM payments 
                    WHERE payment_status = 'Paid';"
                Using cmd As New MySqlCommand(sqlRevenue, conn)
                    Using r = cmd.ExecuteReader()
                        If r.Read() Then
                            stats.TodayRevenue = If(r.IsDBNull(r.GetOrdinal("today_rev")), 0D, Convert.ToDecimal(r("today_rev")))
                            stats.MonthlyRevenue = If(r.IsDBNull(r.GetOrdinal("month_rev")), 0D, Convert.ToDecimal(r("month_rev")))
                            stats.YearlyRevenue = If(r.IsDBNull(r.GetOrdinal("year_rev")), 0D, Convert.ToDecimal(r("year_rev")))
                        End If
                    End Using
                End Using

                ' 5. Staff counter
                Dim sqlStaff As String = "SELECT COUNT(*) FROM users WHERE status = 'Active';"
                Using cmd As New MySqlCommand(sqlStaff, conn)
                    stats.TotalStaff = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using

            Return stats
        End Function

        ''' <summary>
        ''' Returns DataTable of revenue grouped by date / method for reports.
        ''' </summary>
        Public Function GetRevenueReport(startDate As Date, endDate As Date, Optional method As String = "All") As DataTable
            Dim dt As New DataTable()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT 
                        DATE(p.payment_date) as `Date`,
                        p.payment_method as `Payment Method`,
                        COUNT(p.payment_id) as `Transactions`,
                        SUM(p.amount) as `Total Revenue`
                    FROM payments p
                    WHERE p.payment_status = 'Paid'
                      AND DATE(p.payment_date) >= @start 
                      AND DATE(p.payment_date) <= @end "

                If Not String.IsNullOrWhiteSpace(method) AndAlso Not String.Equals(method, "All", StringComparison.OrdinalIgnoreCase) Then
                    sql &= " AND p.payment_method = @method"
                End If

                sql &= " GROUP BY DATE(p.payment_date), p.payment_method ORDER BY `Date` DESC, `Total Revenue` DESC;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@start", startDate.Date)
                    cmd.Parameters.AddWithValue("@end", endDate.Date)
                    If Not String.IsNullOrWhiteSpace(method) AndAlso Not String.Equals(method, "All", StringComparison.OrdinalIgnoreCase) Then
                        cmd.Parameters.AddWithValue("@method", method)
                    End If

                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
            Return dt
        End Function

        ''' <summary>
        ''' Returns DataTable of attendance breakdown for report.
        ''' </summary>
        Public Function GetAttendanceReport(startDate As Date, endDate As Date) As DataTable
            Dim dt As New DataTable()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT 
                        DATE(a.check_in) as `Date`,
                        m.membership_number as `Member No`,
                        CONCAT(m.first_name, ' ', m.last_name) as `Member Name`,
                        TIME(a.check_in) as `Check In Time`,
                        CASE WHEN a.check_out IS NOT NULL THEN TIME(a.check_out) ELSE 'Inside' END as `Check Out Time`,
                        TIMESTAMPDIFF(MINUTE, a.check_in, IFNULL(a.check_out, NOW())) as `Minutes Spent`,
                        u.username as `Recorded By`
                    FROM attendance a
                    INNER JOIN members m ON a.member_id = m.member_id
                    INNER JOIN users u ON a.recorded_by = u.user_id
                    WHERE DATE(a.check_in) >= @start AND DATE(a.check_in) <= @end
                    ORDER BY a.check_in DESC;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@start", startDate.Date)
                    cmd.Parameters.AddWithValue("@end", endDate.Date)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
            Return dt
        End Function

        ''' <summary>
        ''' Returns DataTable of membership subscription analytics.
        ''' </summary>
        Public Function GetMembershipReport(Optional statusFilter As String = "All") As DataTable
            _membershipService.SyncExpiredMemberships()
            Dim dt As New DataTable()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT 
                        ms.membership_id as `ID`,
                        m.membership_number as `Member No`,
                        CONCAT(m.first_name, ' ', m.last_name) as `Member Name`,
                        p.plan_name as `Plan`,
                        ms.start_date as `Start Date`,
                        ms.end_date as `End Date`,
                        ms.amount as `Amount`,
                        ms.status as `Status`,
                        DATEDIFF(ms.end_date, CURDATE()) as `Days Remaining`
                    FROM memberships ms
                    INNER JOIN members m ON ms.member_id = m.member_id
                    INNER JOIN membership_plans p ON ms.plan_id = p.plan_id
                    WHERE 1=1 "

                If Not String.IsNullOrWhiteSpace(statusFilter) AndAlso Not String.Equals(statusFilter, "All", StringComparison.OrdinalIgnoreCase) Then
                    sql &= " AND ms.status = @status"
                End If

                sql &= " ORDER BY ms.end_date DESC;"

                Using cmd As New MySqlCommand(sql, conn)
                    If Not String.IsNullOrWhiteSpace(statusFilter) AndAlso Not String.Equals(statusFilter, "All", StringComparison.OrdinalIgnoreCase) Then
                        cmd.Parameters.AddWithValue("@status", statusFilter)
                    End If
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
            Return dt
        End Function

        ''' <summary>
        ''' Returns monthly revenue trend for the last 6 months (for charting).
        ''' </summary>
        Public Function GetMonthlyRevenueTrend() As DataTable
            Dim dt As New DataTable()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT 
                        DATE_FORMAT(payment_date, '%Y-%m') as `MonthKey`,
                        DATE_FORMAT(payment_date, '%b %Y') as `MonthLabel`,
                        SUM(amount) as `TotalRevenue`,
                        COUNT(*) as `TransactionCount`
                    FROM payments
                    WHERE payment_status = 'Paid'
                      AND payment_date >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH)
                    GROUP BY MonthKey, MonthLabel
                    ORDER BY MonthKey ASC;"

                Using cmd As New MySqlCommand(sql, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
            Return dt
        End Function
    End Class
End Namespace
