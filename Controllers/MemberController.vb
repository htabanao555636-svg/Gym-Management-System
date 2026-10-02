Imports Gym_Management_System.Models
Imports Gym_Management_System.Services
Imports Gym_Management_System.Utilities

Namespace Controllers
    ''' <summary>
    ''' Controller managing Member data, validation, and actions for Views.
    ''' </summary>
    Public Class MemberController
        Private ReadOnly _memberService As New MemberService()

        Public Function GetMembers(Optional search As String = Nothing, Optional statusFilter As String = "All") As List(Of Member)
            Try
                Return _memberService.GetMembers(search, statusFilter)
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Error retrieving members: {ex.Message}")
                Return New List(Of Member)()
            End Try
        End Function

        Public Function GetMemberById(id As Integer) As Member
            Try
                Return _memberService.GetMemberById(id)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GetMemberByNumber(number As String) As Member
            Try
                Return _memberService.GetMemberByNumber(number)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function SaveMember(member As Member, isNew As Boolean) As (Success As Boolean, ErrorMessage As String)
            Try
                If isNew Then
                    _memberService.AddMember(member)
                Else
                    _memberService.UpdateMember(member)
                End If
                Return (True, String.Empty)
            Catch ex As ArgumentException
                Return (False, ex.Message)
            Catch ex As Exception
                Return (False, "Unable to save member. Please check input and database connection.")
            End Try
        End Function

        Public Function SetStatus(memberId As Integer, status As String) As (Success As Boolean, ErrorMessage As String)
            Try
                _memberService.SetMemberStatus(memberId, status)
                Return (True, String.Empty)
            Catch ex As Exception
                Return (False, "Unable to update member status.")
            End Try
        End Function

        Public Function GetNextMembershipNumber() As String
            Try
                Return _memberService.GenerateMembershipNumber()
            Catch ex As Exception
                Return $"MEM-{DateTime.Now.Year}-0001"
            End Try
        End Function
    End Class
End Namespace
