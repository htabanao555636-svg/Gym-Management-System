Namespace Utilities
    ''' <summary>
    ''' Enforces Role-Based Access Control (RBAC) permissions.
    ''' </summary>
    Public Class PermissionHelper
        Public Enum AppFeature
            Dashboard
            Members
            MembershipPlansView
            MembershipPlansManage
            Memberships
            Payments
            Attendance
            StaffManagement
            UserManagement
            ReportsBasic
            ReportsFull
            ActivityLogs
            Settings
        End Enum

        ''' <summary>
        ''' Checks whether the current session has permission to access a specific feature.
        ''' </summary>
        Public Shared Function CanAccess(feature As AppFeature) As Boolean
            If Not SessionManager.IsLoggedIn Then
                Return False
            End If

            If SessionManager.IsAdmin Then
                ' Admin has full access to everything
                Return True
            End If

            ' Staff permissions
            Select Case feature
                Case AppFeature.Dashboard,
                     AppFeature.Members,
                     AppFeature.MembershipPlansView,
                     AppFeature.Memberships,
                     AppFeature.Payments,
                     AppFeature.Attendance,
                     AppFeature.ReportsBasic
                    Return True

                Case AppFeature.MembershipPlansManage,
                     AppFeature.StaffManagement,
                     AppFeature.UserManagement,
                     AppFeature.ReportsFull,
                     AppFeature.ActivityLogs,
                     AppFeature.Settings
                    Return False

                Case Else
                    Return False
            End Select
        End Function

        ''' <summary>
        ''' Throws an UnauthorizedAccessException if the current user lacks permission for the feature.
        ''' </summary>
        Public Shared Sub RequirePermission(feature As AppFeature)
            If Not CanAccess(feature) Then
                Throw New UnauthorizedAccessException($"Access Denied: You do not have permission to access {feature}.")
            End If
        End Sub
    End Class
End Namespace
