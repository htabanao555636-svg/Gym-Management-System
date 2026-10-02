Imports System.Windows.Forms
Imports Gym_Management_System.Data
Imports Gym_Management_System.Views

Namespace Main
    ''' <summary>
    ''' Main application entry point for Gym Management System.
    ''' </summary>
    Public Module Program
        <STAThread>
        Public Sub Main()
            Application.SetHighDpiMode(HighDpiMode.SystemAware)
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)

            ' Initialize MySQL database schema and seed defaults
            Try
                DatabaseInitializer.InitializeDatabase()
            Catch ex As Exception
                Dim msg As String = $"Unable to connect to MySQL database at localhost:3306.{Environment.NewLine}{Environment.NewLine}" &
                                   $"Details: {ex.Message}{Environment.NewLine}{Environment.NewLine}" &
                                   $"Please ensure MySQL Server / XAMPP is running and port 3306 is open."
                MessageBox.Show(msg, "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            ' Launch application with LoginForm
            Application.Run(New LoginForm())
        End Sub
    End Module
End Namespace
