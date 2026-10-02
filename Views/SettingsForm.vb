Imports System.Drawing
Imports System.Windows.Forms
Imports Gym_Management_System.Controllers
Imports Gym_Management_System.Utilities

Namespace Views
    ''' <summary>
    ''' System and Gym Configuration Settings View (Admin Only).
    ''' Compatible with Visual Studio Windows Forms Designer.
    ''' </summary>
    Partial Public Class SettingsForm
        Inherits Form

        Private ReadOnly _settingController As New SettingController()

        Public Sub New()
            InitializeComponent()
            LoadSettings()
        End Sub

        Public Sub LoadSettings()
            txtGymName.Text = _settingController.GetSetting("gym_name", "TITAN FITNESS & GYM CENTER")
            txtGymAddress.Text = _settingController.GetSetting("gym_address", "123 Fitness Boulevard, Metro City")
            txtGymContact.Text = _settingController.GetSetting("gym_contact", "+63 912 345 6789 / info@titanfitness.local")
            txtReceiptFooter.Text = _settingController.GetSetting("receipt_footer", "Thank you for training with us! Stay fit and stay strong.")
            txtCurrency.Text = _settingController.GetSetting("currency_symbol", "₱")
            chkAllowPartial.Checked = String.Equals(_settingController.GetSetting("allow_partial_payments", "false"), "true", StringComparison.OrdinalIgnoreCase)
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            lblStatus.Text = ""
            Try
                _settingController.SaveSetting("gym_name", txtGymName.Text.Trim(), "Official gym name on displays and receipts")
                _settingController.SaveSetting("gym_address", txtGymAddress.Text.Trim(), "Gym address printed on receipts")
                _settingController.SaveSetting("gym_contact", txtGymContact.Text.Trim(), "Gym contact number and email")
                _settingController.SaveSetting("receipt_footer", txtReceiptFooter.Text.Trim(), "Receipt footer text")
                _settingController.SaveSetting("currency_symbol", txtCurrency.Text.Trim(), "Currency symbol used in cashiering")
                _settingController.SaveSetting("allow_partial_payments", If(chkAllowPartial.Checked, "true", "false"), "Flag to enable partial payments")

                lblStatus.ForeColor = Color.FromArgb(16, 185, 129)
                lblStatus.Text = "✓ Settings updated successfully!"
            Catch ex As Exception
                lblStatus.ForeColor = Color.FromArgb(220, 38, 38)
                lblStatus.Text = $"Failed to save: {ex.Message}"
            End Try
        End Sub
    End Class
End Namespace
