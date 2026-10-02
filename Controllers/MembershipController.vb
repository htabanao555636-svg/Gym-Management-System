Imports Gym_Management_System.Models
Imports Gym_Management_System.Services

Namespace Controllers
    ''' <summary>
    ''' Controller managing member subscriptions and renewals.
    ''' </summary>
    Public Class MembershipController
        Private ReadOnly _membershipService As New MembershipService()

        Public Function GetMemberships(Optional search As String = Nothing, Optional statusFilter As String = "All") As List(Of Membership)
            Try
                Return _membershipService.GetMemberships(search, statusFilter)
            Catch ex As Exception
                Return New List(Of Membership)()
            End Try
        End Function

        Public Function GetActiveForMember(memberId As Integer) As Membership
            Try
                Return _membershipService.GetActiveMembershipForMember(memberId)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GetPendingForMember(memberId As Integer) As Membership
            Try
                Return _membershipService.GetPendingMembershipForMember(memberId)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function Enroll(memberId As Integer, planId As Integer, Optional customStartDate As Date? = Nothing) As (Success As Boolean, Membership As Membership, ErrorMessage As String)
            Try
                Dim ms = _membershipService.EnrollMembership(memberId, planId, customStartDate)
                Return (True, ms, String.Empty)
            Catch ex As ArgumentException
                Return (False, Nothing, ex.Message)
            Catch ex As Exception
                Return (False, Nothing, "Unable to enroll membership. Please try again.")
            End Try
        End Function

        Public Function Cancel(membershipId As Integer, reason As String) As (Success As Boolean, ErrorMessage As String)
            Try
                _membershipService.CancelMembership(membershipId, reason)
                Return (True, String.Empty)
            Catch ex As Exception
                Return (False, "Unable to cancel membership.")
            End Try
        End Function
    End Class
End Namespace
