Imports System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Services

Namespace Controllers
    ''' <summary>
    ''' Controller managing report generations and metrics.
    ''' </summary>
    Public Class ReportController
        Private ReadOnly _reportService As New ReportService()

        Public Function GetDashboardStats() As DashboardStats
            Try
                Return _reportService.GetDashboardStats()
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Failed to retrieve stats: {ex.Message}")
                Return New DashboardStats()
            End Try
        End Function

        Public Function GetRevenueReport(startDate As Date, endDate As Date, Optional method As String = "All") As DataTable
            Try
                Return _reportService.GetRevenueReport(startDate, endDate, method)
            Catch ex As Exception
                Return New DataTable()
            End Try
        End Function

        Public Function GetAttendanceReport(startDate As Date, endDate As Date) As DataTable
            Try
                Return _reportService.GetAttendanceReport(startDate, endDate)
            Catch ex As Exception
                Return New DataTable()
            End Try
        End Function

        Public Function GetMembershipReport(Optional statusFilter As String = "All") As DataTable
            Try
                Return _reportService.GetMembershipReport(statusFilter)
            Catch ex As Exception
                Return New DataTable()
            End Try
        End Function

        Public Function GetMonthlyRevenueTrend() As DataTable
            Try
                Return _reportService.GetMonthlyRevenueTrend()
            Catch ex As Exception
                Return New DataTable()
            End Try
        End Function
    End Class
End Namespace
