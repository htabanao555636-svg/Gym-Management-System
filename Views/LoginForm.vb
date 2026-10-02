Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Models

Namespace Views
    ''' <summary>
    ''' Secure authentication view for Gym Management System.
    ''' Compatible with Visual Studio Windows Forms Designer.
    ''' </summary>
    Partial Public Class LoginForm
        Inherits Form

        Private ReadOnly _authController As New AuthController()

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
            txtPassword.PasswordChar = If(chkShowPassword.Checked, ChrW(0), ChrW(9679))
        End Sub

        Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
            lblError.Text = ""
            Dim username As String = txtUsername.Text.Trim()
            Dim password As String = txtPassword.Text

            If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
                lblError.Text = "Please enter both username and password."
                Return
            End If

            btnLogin.Enabled = False
            btnLogin.Text = "AUTHENTICATING..."
            Application.DoEvents()

            Dim result = _authController.Login(username, password)
            btnLogin.Enabled = True
            btnLogin.Text = "LOGIN"

            If result.Success Then
                Me.Hide()
                Dim main = New MainForm()
                AddHandler main.FormClosed, Sub() Me.Close()
                main.Show()
            Else
                lblError.Text = result.ErrorMessage
            End If
        End Sub

        Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
            Application.Exit()
        End Sub

        Private Sub LoginForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
            If pnlCard IsNot Nothing Then
                pnlCard.Location = New Point((Me.ClientSize.Width - pnlCard.Width) \ 2, 135)
            End If
        End Sub
    End Class
End Namespace
