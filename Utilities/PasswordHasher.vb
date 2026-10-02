Imports System.Security.Cryptography
Imports System.Text

Namespace Utilities
    ''' <summary>
    ''' Provides cryptographic hashing and verification for user passwords using PBKDF2 with SHA256.
    ''' </summary>
    Public Class PasswordHasher
        Private Const SaltSize As Integer = 16
        Private Const HashSize As Integer = 32
        Private Const Iterations As Integer = 100000

        ''' <summary>
        ''' Generates a cryptographic salt and hashes the plain text password.
        ''' </summary>
        Public Shared Sub HashPassword(password As String, ByRef hash As String, ByRef salt As String)
            If String.IsNullOrEmpty(password) Then
                Throw New ArgumentException("Password cannot be empty.", NameOf(password))
            End If

            Dim saltBytes(SaltSize - 1) As Byte
            Using rng = RandomNumberGenerator.Create()
                rng.GetBytes(saltBytes)
            End Using

            Dim hashBytes As Byte() = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, HashAlgorithmName.SHA256, HashSize)
            hash = Convert.ToBase64String(hashBytes)
            salt = Convert.ToBase64String(saltBytes)
        End Sub

        ''' <summary>
        ''' Verifies a plain text password against a stored hash and salt.
        ''' </summary>
        Public Shared Function VerifyPassword(password As String, storedHash As String, storedSalt As String) As Boolean
            If String.IsNullOrEmpty(password) OrElse String.IsNullOrEmpty(storedHash) OrElse String.IsNullOrEmpty(storedSalt) Then
                Return False
            End If

            Try
                Dim saltBytes As Byte() = Convert.FromBase64String(storedSalt)
                Dim computedBytes As Byte() = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, HashAlgorithmName.SHA256, HashSize)
                Dim computedHash As String = Convert.ToBase64String(computedBytes)
                Return CryptographicOperations.FixedTimeEquals(
                    Encoding.UTF8.GetBytes(computedHash),
                    Encoding.UTF8.GetBytes(storedHash)
                )
            Catch
                Return False
            End Try
        End Function
    End Class
End Namespace
