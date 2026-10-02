Namespace Models
    ''' <summary>
    ''' Represents an audit log entry for system activities.
    ''' </summary>
    Public Class ActivityLog
        Public Property LogID As Integer
        Public Property UserID As Integer?
        Public Property Action As String
        Public Property Description As String
        Public Property CreatedAt As DateTime = DateTime.Now

        ' Joined display property
        Public Property Username As String
        Public Property FullName As String
        Public Property Role As String
    End Class
End Namespace
