Imports Gym_Management_System.Models
Imports Gym_Management_System.Services

Namespace Controllers
    ''' <summary>
    ''' Controller coordinating Membership Plan operations.
    ''' </summary>
    Public Class MembershipPlanController
        Private ReadOnly _planService As New MembershipPlanService()

        Public Function GetPlans(Optional activeOnly As Boolean = False) As List(Of MembershipPlan)
            Try
                Return _planService.GetPlans(activeOnly)
            Catch ex As Exception
                Return New List(Of MembershipPlan)()
            End Try
        End Function

        Public Function GetPlanById(id As Integer) As MembershipPlan
            Try
                Return _planService.GetPlanById(id)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function SavePlan(plan As MembershipPlan, isNew As Boolean) As (Success As Boolean, ErrorMessage As String)
            Try
                If isNew Then
                    _planService.AddPlan(plan)
                Else
                    _planService.UpdatePlan(plan)
                End If
                Return (True, String.Empty)
            Catch ex As UnauthorizedAccessException
                Return (False, ex.Message)
            Catch ex As ArgumentException
                Return (False, ex.Message)
            Catch ex As Exception
                Return (False, "Unable to save membership plan.")
            End Try
        End Function

        Public Function SetStatus(planId As Integer, status As String) As (Success As Boolean, ErrorMessage As String)
            Try
                _planService.SetPlanStatus(planId, status)
                Return (True, String.Empty)
            Catch ex As UnauthorizedAccessException
                Return (False, ex.Message)
            Catch ex As Exception
                Return (False, "Unable to update plan status.")
            End Try
        End Function

        Public Function CalculateEndDate(startDate As Date, plan As MembershipPlan) As Date
            Return _planService.CalculateEndDate(startDate, plan)
        End Function
    End Class
End Namespace
