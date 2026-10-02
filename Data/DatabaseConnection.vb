Imports System.Data
Imports MySql.Data.MySqlClient

Namespace Data
    ''' <summary>
    ''' Centralized database connection manager for MySQL.
    ''' Manages connections, connection strings, and helper methods for safe query execution.
    ''' </summary>
    Public Class DatabaseConnection
        Private Shared _server As String = "localhost"
        Private Shared _port As Integer = 3306
        Private Shared _database As String = "gym_management"
        Private Shared _username As String = "root"
        Private Shared _password As String = ""

        ''' <summary>
        ''' Gets or sets the database server address.
        ''' </summary>
        Public Shared Property Server As String
            Get
                Return _server
            End Get
            Set(value As String)
                _server = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the database port.
        ''' </summary>
        Public Shared Property Port As Integer
            Get
                Return _port
            End Get
            Set(value As Integer)
                _port = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the database name.
        ''' </summary>
        Public Shared Property DatabaseName As String
            Get
                Return _database
            End Get
            Set(value As String)
                _database = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the database user.
        ''' </summary>
        Public Shared Property Username As String
            Get
                Return _username
            End Get
            Set(value As String)
                _username = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the database password.
        ''' </summary>
        Public Shared Property Password As String
            Get
                Return _password
            End Get
            Set(value As String)
                _password = value
            End Set
        End Property

        ''' <summary>
        ''' Returns the formatted connection string for the gym_management database.
        ''' </summary>
        Public Shared ReadOnly Property ConnectionString As String
            Get
                Dim builder As New MySqlConnectionStringBuilder() With {
                    .Server = _server,
                    .Port = CUInt(_port),
                    .Database = _database,
                    .UserID = _username,
                    .Password = _password,
                    .CharacterSet = "utf8mb4",
                    .ConnectionTimeout = 10,
                    .AllowUserVariables = True
                }
                Return builder.ConnectionString
            End Get
        End Property

        ''' <summary>
        ''' Returns the connection string for administrative tasks (without selecting a specific database).
        ''' </summary>
        Public Shared ReadOnly Property ServerConnectionString As String
            Get
                Dim builder As New MySqlConnectionStringBuilder() With {
                    .Server = _server,
                    .Port = CUInt(_port),
                    .UserID = _username,
                    .Password = _password,
                    .CharacterSet = "utf8mb4",
                    .ConnectionTimeout = 10
                }
                Return builder.ConnectionString
            End Get
        End Property

        ''' <summary>
        ''' Creates and opens a new MySQL connection. Caller is responsible for disposing it.
        ''' </summary>
        Public Shared Function GetOpenConnection() As MySqlConnection
            Dim conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Return conn
        End Function

        ''' <summary>
        ''' Tests if connection to the database server is successful.
        ''' </summary>
        Public Shared Function TestConnection(Optional ByRef errorMessage As String = "") As Boolean
            Try
                Using conn As New MySqlConnection(ConnectionString)
                    conn.Open()
                    Return True
                End Using
            Catch ex As Exception
                errorMessage = ex.Message
                Return False
            End Try
        End Function
    End Class
End Namespace
