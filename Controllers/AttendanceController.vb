Imports Gym_Management_System.Models
Imports Gym_Management_System.Services

Namespace Controllers
    ''' <summary>
    ''' Controller managing attendance check-in, check-out, and active occupancy tracking.
    ''' </summary>
    Public Class AttendanceController
        Private ReadOnly _attendanceService As New AttendanceService()

        Public Function CheckIn(memberId As Integer) As (Success As Boolean, Attendance As Attendance, ErrorMessage As String)
            Try
                Dim record = _attendanceService.CheckIn(memberId)
                Return (True, record, String.Empty)
            Catch ex As InvalidOperationException
                Return (False, Nothing, ex.Message)
            Catch ex As ArgumentException
                Return (False, Nothing, ex.Message)
            Catch ex As Exception
                Return (False, Nothing, "Unable to record check-in. Please try again.")
            End Try
        End Function

        Public Function CheckOut(attendanceId As Integer) As (Success As Boolean, Attendance As Attendance, ErrorMessage As String)
            Try
                Dim att = _attendanceService.CheckOut(attendanceId)
                Return (True, att, String.Empty)
            Catch ex As InvalidOperationException
                Return (False, Nothing, ex.Message)
            Catch ex As Exception
                Return (False, Nothing, "Unable to record check-out.")
            End Try
        End Function

        Public Function CheckOutByMemberId(memberId As Integer) As (Success As Boolean, Attendance As Attendance, ErrorMessage As String)
            Try
                Dim att = _attendanceService.CheckOutByMemberId(memberId)
                Return (True, att, String.Empty)
            Catch ex As InvalidOperationException
                Return (False, Nothing, ex.Message)
            Catch ex As Exception
                Return (False, Nothing, "Unable to record check-out.")
            End Try
        End Function

        Public Function GetCurrentSession(memberId As Integer) As Attendance
            Try
                Return _attendanceService.GetCurrentSession(memberId)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GetCurrentlyInside() As List(Of Attendance)
            Try
                Return _attendanceService.GetCurrentlyInsideMembers()
            Catch ex As Exception
                Return New List(Of Attendance)()
            End Try
        End Function

        Public Function GetHistory(Optional targetDate As Date? = Nothing, Optional search As String = Nothing) As List(Of Attendance)
            Try
                Return _attendanceService.GetAttendanceHistory(targetDate, search)
            Catch ex As Exception
                Return New List(Of Attendance)()
            End Try
        End Function

        Public Function IsCurrentlyInside(memberId As Integer) As Boolean
            Try
                Return _attendanceService.IsMemberCurrentlyInside(memberId)
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace
