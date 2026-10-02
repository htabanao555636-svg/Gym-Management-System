Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Data
Imports Gym_Management_System.Models
Imports Gym_Management_System.Utilities

Namespace Services
    ''' <summary>
    ''' Cashiering, payment processing, and transaction reference generation.
    ''' </summary>
    Public Class PaymentService
        ''' <summary>
        ''' Generates a unique transaction reference number: PAY-YYYYMMDD-XXXX
        ''' </summary>
        Public Function GenerateReferenceNumber() As String
            Dim todayStr As String = DateTime.Now.ToString("yyyyMMdd")
            Dim prefix As String = $"PAY-{todayStr}-"
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT reference_number 
                    FROM payments 
                    WHERE reference_number LIKE @pattern 
                    ORDER BY payment_id DESC LIMIT 1;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@pattern", $"{prefix}%")
                    Dim result As Object = cmd.ExecuteScalar()
                    Dim seq As Integer = 1
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        Dim parts As String() = result.ToString().Split("-"c)
                        If parts.Length >= 3 AndAlso Integer.TryParse(parts(2), seq) Then
                            seq += 1
                        End If
                    End If
                    Return $"{prefix}{seq.ToString("D4")}"
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Processes a payment and returns the newly recorded Payment entity.
        ''' Uses a database transaction for consistency.
        ''' </summary>
        Public Function ProcessPayment(payment As Payment) As Payment
            If payment Is Nothing Then Throw New ArgumentNullException(NameOf(payment))
            If payment.MemberID <= 0 Then Throw New ArgumentException("Valid Member ID is required.")
            If Not ValidationHelper.IsPositiveAmount(payment.Amount) Then Throw New ArgumentException("Payment amount must be greater than zero.")
            If String.IsNullOrWhiteSpace(payment.PaymentMethod) Then Throw New ArgumentException("Payment method is required.")

            If String.IsNullOrWhiteSpace(payment.ReferenceNumber) Then
                payment.ReferenceNumber = GenerateReferenceNumber()
            End If

            payment.ProcessedBy = If(SessionManager.CurrentUser IsNot Nothing, SessionManager.CurrentUser.UserID, 1)
            payment.PaymentDate = DateTime.Now

            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Using trans As MySqlTransaction = conn.BeginTransaction()
                    Try
                        Dim sql As String = "
                            INSERT INTO payments (reference_number, member_id, membership_id, amount,
                                                 payment_method, payment_date, payment_status, processed_by, remarks)
                            VALUES (@ref, @memberId, @msId, @amt, @method, @date, @status, @by, @remarks);
                            SELECT LAST_INSERT_ID();"

                        Using cmd As New MySqlCommand(sql, conn, trans)
                            cmd.Parameters.AddWithValue("@ref", payment.ReferenceNumber)
                            cmd.Parameters.AddWithValue("@memberId", payment.MemberID)
                            cmd.Parameters.AddWithValue("@msId", If(payment.MembershipID.HasValue, payment.MembershipID.Value, DBNull.Value))
                            cmd.Parameters.AddWithValue("@amt", payment.Amount)
                            cmd.Parameters.AddWithValue("@method", payment.PaymentMethod)
                            cmd.Parameters.AddWithValue("@date", payment.PaymentDate)
                            cmd.Parameters.AddWithValue("@status", payment.PaymentStatus)
                            cmd.Parameters.AddWithValue("@by", payment.ProcessedBy)
                            cmd.Parameters.AddWithValue("@remarks", If(String.IsNullOrWhiteSpace(payment.Remarks), DBNull.Value, payment.Remarks.Trim()))

                            payment.PaymentID = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' If this is tied to a membership, activate the membership upon payment confirmation
                        If payment.MembershipID.HasValue Then
                            Dim updateMsSql As String = "UPDATE memberships SET status = 'Active' WHERE membership_id = @msId AND status IN ('Pending Payment', 'Pending');"
                            Using cmdMs As New MySqlCommand(updateMsSql, conn, trans)
                                cmdMs.Parameters.AddWithValue("@msId", payment.MembershipID.Value)
                                cmdMs.ExecuteNonQuery()
                            End Using
                        End If

                        trans.Commit()

                        ActivityLogService.Log("Process Payment", $"Processed payment {payment.ReferenceNumber} of {payment.Amount:C} ({payment.PaymentMethod}) for member ID {payment.MemberID}. Linked membership activated.")
                        Return payment
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New ApplicationException($"Payment processing failed: {ex.Message}", ex)
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Retrieves payments with search, method, and date range filters.
        ''' </summary>
        Public Function GetPayments(Optional search As String = Nothing,
                                    Optional method As String = "All",
                                    Optional startDate As Date? = Nothing,
                                    Optional endDate As Date? = Nothing) As List(Of Payment)
            Dim list As New List(Of Payment)()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT p.payment_id, p.reference_number, p.member_id, p.membership_id, p.amount,
                           p.payment_method, p.payment_date, p.payment_status, p.processed_by, p.remarks,
                           m.first_name, m.middle_name, m.last_name, m.membership_number,
                           pl.plan_name, u.username as processed_by_username
                    FROM payments p
                    INNER JOIN members m ON p.member_id = m.member_id
                    LEFT JOIN memberships ms ON p.membership_id = ms.membership_id
                    LEFT JOIN membership_plans pl ON ms.plan_id = pl.plan_id
                    INNER JOIN users u ON p.processed_by = u.user_id
                    WHERE 1=1 "

                If Not String.IsNullOrWhiteSpace(method) AndAlso Not String.Equals(method, "All", StringComparison.OrdinalIgnoreCase) Then
                    sql &= " AND p.payment_method = @method"
                End If
                If startDate.HasValue Then
                    sql &= " AND DATE(p.payment_date) >= @startDate"
                End If
                If endDate.HasValue Then
                    sql &= " AND DATE(p.payment_date) <= @endDate"
                End If
                If Not String.IsNullOrWhiteSpace(search) Then
                    sql &= " AND (p.reference_number LIKE @search OR m.membership_number LIKE @search OR m.first_name LIKE @search OR m.last_name LIKE @search)"
                End If

                sql &= " ORDER BY p.payment_id DESC;"

                Using cmd As New MySqlCommand(sql, conn)
                    If Not String.IsNullOrWhiteSpace(method) AndAlso Not String.Equals(method, "All", StringComparison.OrdinalIgnoreCase) Then
                        cmd.Parameters.AddWithValue("@method", method)
                    End If
                    If startDate.HasValue Then cmd.Parameters.AddWithValue("@startDate", startDate.Value.Date)
                    If endDate.HasValue Then cmd.Parameters.AddWithValue("@endDate", endDate.Value.Date)
                    If Not String.IsNullOrWhiteSpace(search) Then cmd.Parameters.AddWithValue("@search", $"%{search.Trim()}%")

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapReaderToPayment(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Gets a payment by reference number or ID for receipt generation.
        ''' </summary>
        Public Function GetPaymentById(paymentId As Integer) As Payment
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim sql As String = "
                    SELECT p.payment_id, p.reference_number, p.member_id, p.membership_id, p.amount,
                           p.payment_method, p.payment_date, p.payment_status, p.processed_by, p.remarks,
                           m.first_name, m.middle_name, m.last_name, m.membership_number,
                           pl.plan_name, u.username as processed_by_username
                    FROM payments p
                    INNER JOIN members m ON p.member_id = m.member_id
                    LEFT JOIN memberships ms ON p.membership_id = ms.membership_id
                    LEFT JOIN membership_plans pl ON ms.plan_id = pl.plan_id
                    INNER JOIN users u ON p.processed_by = u.user_id
                    WHERE p.payment_id = @id;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", paymentId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapReaderToPayment(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Private Function MapReaderToPayment(reader As MySqlDataReader) As Payment
            Dim fn As String = reader("first_name").ToString()
            Dim mn As String = If(reader.IsDBNull(reader.GetOrdinal("middle_name")), "", reader("middle_name").ToString())
            Dim ln As String = reader("last_name").ToString()
            Dim mName As String = If(String.IsNullOrWhiteSpace(mn), $"{fn} {ln}", $"{fn} {mn.Substring(0, 1)}. {ln}")

            Dim msId As Integer? = Nothing
            Dim msOrd As Integer = reader.GetOrdinal("membership_id")
            If Not reader.IsDBNull(msOrd) Then
                msId = Convert.ToInt32(reader.GetValue(msOrd))
            End If

            Return New Payment() With {
                .PaymentID = Convert.ToInt32(reader("payment_id")),
                .ReferenceNumber = reader("reference_number").ToString(),
                .MemberID = Convert.ToInt32(reader("member_id")),
                .MembershipID = msId,
                .Amount = Convert.ToDecimal(reader("amount")),
                .PaymentMethod = reader("payment_method").ToString(),
                .PaymentDate = Convert.ToDateTime(reader("payment_date")),
                .PaymentStatus = reader("payment_status").ToString(),
                .ProcessedBy = Convert.ToInt32(reader("processed_by")),
                .Remarks = If(reader.IsDBNull(reader.GetOrdinal("remarks")), "", reader("remarks").ToString()),
                .MemberName = mName,
                .MembershipNumber = reader("membership_number").ToString(),
                .PlanName = If(reader.IsDBNull(reader.GetOrdinal("plan_name")), "N/A", reader("plan_name").ToString()),
                .ProcessedByName = reader("processed_by_username").ToString()
            }
        End Function
    End Class
End Namespace
