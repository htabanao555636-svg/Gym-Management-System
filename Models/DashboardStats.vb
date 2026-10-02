Namespace Models
    ''' <summary>
    ''' Data Transfer Object for Dashboard metric counters and aggregations.
    ''' </summary>
    Public Class DashboardStats
        Public Property TotalMembers As Integer
        Public Property ActiveMembers As Integer
        Public Property InactiveMembers As Integer
        Public Property SuspendedMembers As Integer

        Public Property ActiveMemberships As Integer
        Public Property ExpiringMemberships As Integer
        Public Property ExpiredMemberships As Integer

        Public Property TodayAttendance As Integer
        Public Property CurrentlyInGym As Integer

        Public Property TodayRevenue As Decimal
        Public Property MonthlyRevenue As Decimal
        Public Property YearlyRevenue As Decimal

        Public Property TotalStaff As Integer
    End Class
End Namespace
