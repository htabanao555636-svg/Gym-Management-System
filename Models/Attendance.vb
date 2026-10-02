Namespace Models
    ''' <summary>
    ''' Represents a gym attendance check-in / check-out session.
    ''' </summary>
    Public Class Attendance
        Public Property AttendanceID As Integer
        Public Property MemberID As Integer
        Public Property CheckIn As DateTime = DateTime.Now
        Public Property CheckOut As DateTime?
        Public Property RecordedBy As Integer

        ' Joined display properties
        Public Property MemberName As String
        Public Property MembershipNumber As String
        Public Property RecordedByName As String
        Public Property MembershipStatus As String

        ''' <summary>
        ''' Gets whether this member is currently active inside the gym.
        ''' </summary>
        Public ReadOnly Property IsCurrentlyInside As Boolean
            Get
                Return Not CheckOut.HasValue OrElse CheckOut.Value = DateTime.MinValue
            End Get
        End Property

        ''' <summary>
        ''' Formatted duration of the visit if checked out, or elapsed time if still inside in hour format with real-time seconds.
        ''' </summary>
        Public ReadOnly Property DurationText As String
            Get
                Try
                    Dim hasCheckedOut As Boolean = CheckOut.HasValue AndAlso CheckOut.Value > DateTime.MinValue
                    Dim endTime As DateTime = If(hasCheckedOut, CheckOut.Value, DateTime.Now)
                    Dim span As TimeSpan = endTime - CheckIn
                    Dim sec As Double = span.TotalSeconds
                    If Double.IsNaN(sec) OrElse Double.IsInfinity(sec) OrElse sec < 0 Then
                        sec = 0
                    End If
                    Dim totalSeconds As Long = CLng(Math.Floor(sec))
                    Dim hrs As Long = totalSeconds \ 3600
                    Dim mins As Long = (totalSeconds Mod 3600) \ 60
                    Dim secs As Long = totalSeconds Mod 60
                    Dim hrStr As String = If(hrs = 1, "1 hr", $"{hrs} hrs")
                    Return $"{hrStr} {mins:D2} min {secs:D2} sec"
                Catch ex As Exception
                    Return "0 hrs 00 min 00 sec"
                End Try
            End Get
        End Property
    End Class
End Namespace
