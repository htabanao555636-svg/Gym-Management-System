Imports System.Text.RegularExpressions

Namespace Utilities
    ''' <summary>
    ''' Provides input validation rules for common data formats and constraints.
    ''' </summary>
    Public Class ValidationHelper
        Private Shared ReadOnly EmailRegex As New Regex("^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
        Private Shared ReadOnly PhoneRegex As New Regex("^[0-9\+\-\s\(\)]{7,25}$", RegexOptions.Compiled)

        ''' <summary>
        ''' Validates if a string is not null, empty, or whitespace.
        ''' </summary>
        Public Shared Function IsNotEmpty(value As String) As Boolean
            Return Not String.IsNullOrWhiteSpace(value)
        End Function

        ''' <summary>
        ''' Validates email format. Returns true if empty (when optional) or matches valid pattern.
        ''' </summary>
        Public Shared Function IsValidEmail(email As String, Optional required As Boolean = True) As Boolean
            If String.IsNullOrWhiteSpace(email) Then
                Return Not required
            End If
            Return EmailRegex.IsMatch(email.Trim())
        End Function

        ''' <summary>
        ''' Validates phone number format.
        ''' </summary>
        Public Shared Function IsValidPhone(phone As String, Optional required As Boolean = True) As Boolean
            If String.IsNullOrWhiteSpace(phone) Then
                Return Not required
            End If
            Return PhoneRegex.IsMatch(phone.Trim())
        End Function

        ''' <summary>
        ''' Validates that an amount is strictly positive.
        ''' </summary>
        Public Shared Function IsPositiveAmount(amount As Decimal) As Boolean
            Return amount > 0D
        End Function

        ''' <summary>
        ''' Validates age given birth date (must be between 10 and 100 years).
        ''' </summary>
        Public Shared Function IsValidAge(birthDate As Date, Optional minAge As Integer = 10, Optional maxAge As Integer = 100) As Boolean
            Dim age As Integer = CalculateAge(birthDate)
            Return age >= minAge AndAlso age <= maxAge
        End Function

        ''' <summary>
        ''' Calculates integer age from a birth date.
        ''' </summary>
        Public Shared Function CalculateAge(birthDate As Date) As Integer
            Dim today As Date = Date.Today
            Dim age As Integer = today.Year - birthDate.Year
            If birthDate.Date > today.AddYears(-age) Then
                age -= 1
            End If
            Return Math.Max(0, age)
        End Function
    End Class
End Namespace
