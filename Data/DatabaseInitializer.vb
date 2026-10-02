Imports System.Data
Imports MySql.Data.MySqlClient
Imports Gym_Management_System.Utilities

Namespace Data
    ''' <summary>
    ''' Initializes the MySQL database, tables, relationships, and default seed records.
    ''' </summary>
    Public Class DatabaseInitializer
        ''' <summary>
        ''' Runs database creation, table creation, and initial seeding.
        ''' </summary>
        Public Shared Sub InitializeDatabase()
            CreateDatabaseIfNotExists()
            CreateTables()
            SeedDefaultAdmin()
            SeedDefaultPlans()
            SeedDefaultSettings()
        End Sub

        Private Shared Sub CreateDatabaseIfNotExists()
            Using conn As New MySqlConnection(DatabaseConnection.ServerConnectionString)
                conn.Open()
                Dim sql As String = $"CREATE DATABASE IF NOT EXISTS `{DatabaseConnection.DatabaseName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Private Shared Sub CreateTables()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                ' 1. Users table
                Dim sqlUsers As String = "
                    CREATE TABLE IF NOT EXISTS users (
                        user_id INT AUTO_INCREMENT PRIMARY KEY,
                        username VARCHAR(50) NOT NULL UNIQUE,
                        password_hash VARCHAR(255) NOT NULL,
                        password_salt VARCHAR(255) NOT NULL,
                        first_name VARCHAR(50) NOT NULL,
                        middle_name VARCHAR(50) NULL,
                        last_name VARCHAR(50) NOT NULL,
                        email VARCHAR(100) NOT NULL UNIQUE,
                        contact_number VARCHAR(25) NULL,
                        role ENUM('ADMIN', 'STAFF') NOT NULL DEFAULT 'STAFF',
                        status ENUM('Active', 'Inactive') NOT NULL DEFAULT 'Active',
                        created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"

                ' 2. Members table
                Dim sqlMembers As String = "
                    CREATE TABLE IF NOT EXISTS members (
                        member_id INT AUTO_INCREMENT PRIMARY KEY,
                        membership_number VARCHAR(30) NOT NULL UNIQUE,
                        first_name VARCHAR(50) NOT NULL,
                        middle_name VARCHAR(50) NULL,
                        last_name VARCHAR(50) NOT NULL,
                        gender ENUM('Male', 'Female', 'Other') NOT NULL,
                        birth_date DATE NOT NULL,
                        contact_number VARCHAR(25) NOT NULL,
                        email VARCHAR(100) NULL,
                        address TEXT NULL,
                        emergency_contact_name VARCHAR(100) NULL,
                        emergency_contact_number VARCHAR(25) NULL,
                        registration_date DATE NOT NULL,
                        status ENUM('Active', 'Inactive', 'Suspended') NOT NULL DEFAULT 'Active',
                        photo_path VARCHAR(255) NULL,
                        created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                        INDEX idx_member_name (last_name, first_name),
                        INDEX idx_member_status (status)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"

                ' 3. Membership plans table
                Dim sqlPlans As String = "
                    CREATE TABLE IF NOT EXISTS membership_plans (
                        plan_id INT AUTO_INCREMENT PRIMARY KEY,
                        plan_name VARCHAR(100) NOT NULL,
                        description TEXT NULL,
                        duration INT NOT NULL,
                        duration_unit ENUM('Days', 'Months', 'Years') NOT NULL DEFAULT 'Months',
                        price DECIMAL(10,2) NOT NULL,
                        status ENUM('Active', 'Inactive') NOT NULL DEFAULT 'Active',
                        created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"

                ' 4. Memberships table
                Dim sqlMemberships As String = "
                    CREATE TABLE IF NOT EXISTS memberships (
                        membership_id INT AUTO_INCREMENT PRIMARY KEY,
                        member_id INT NOT NULL,
                        plan_id INT NOT NULL,
                        start_date DATE NOT NULL,
                        end_date DATE NOT NULL,
                        amount DECIMAL(10,2) NOT NULL,
                        status ENUM('Pending Payment', 'Active', 'Expiring Soon', 'Expired', 'Cancelled') NOT NULL DEFAULT 'Pending Payment',
                        created_by INT NOT NULL,
                        created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                        FOREIGN KEY (member_id) REFERENCES members(member_id) ON DELETE RESTRICT,
                        FOREIGN KEY (plan_id) REFERENCES membership_plans(plan_id) ON DELETE RESTRICT,
                        FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE RESTRICT,
                        INDEX idx_membership_dates (start_date, end_date),
                        INDEX idx_membership_status (status)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"

                ' 5. Payments table
                Dim sqlPayments As String = "
                    CREATE TABLE IF NOT EXISTS payments (
                        payment_id INT AUTO_INCREMENT PRIMARY KEY,
                        reference_number VARCHAR(50) NOT NULL UNIQUE,
                        member_id INT NOT NULL,
                        membership_id INT NULL,
                        amount DECIMAL(10,2) NOT NULL,
                        payment_method ENUM('Cash', 'GCash', 'Bank Transfer') NOT NULL DEFAULT 'Cash',
                        payment_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        payment_status ENUM('Paid', 'Cancelled', 'Refunded') NOT NULL DEFAULT 'Paid',
                        processed_by INT NOT NULL,
                        remarks TEXT NULL,
                        FOREIGN KEY (member_id) REFERENCES members(member_id) ON DELETE RESTRICT,
                        FOREIGN KEY (membership_id) REFERENCES memberships(membership_id) ON DELETE SET NULL,
                        FOREIGN KEY (processed_by) REFERENCES users(user_id) ON DELETE RESTRICT,
                        INDEX idx_payment_date (payment_date),
                        INDEX idx_payment_status (payment_status)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"

                ' 6. Attendance table
                Dim sqlAttendance As String = "
                    CREATE TABLE IF NOT EXISTS attendance (
                        attendance_id INT AUTO_INCREMENT PRIMARY KEY,
                        member_id INT NOT NULL,
                        check_in DATETIME NOT NULL,
                        check_out DATETIME NULL,
                        recorded_by INT NOT NULL,
                        FOREIGN KEY (member_id) REFERENCES members(member_id) ON DELETE RESTRICT,
                        FOREIGN KEY (recorded_by) REFERENCES users(user_id) ON DELETE RESTRICT,
                        INDEX idx_attendance_checkin (check_in),
                        INDEX idx_attendance_checkout (check_out)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"

                ' 7. Activity logs table
                Dim sqlActivityLogs As String = "
                    CREATE TABLE IF NOT EXISTS activity_logs (
                        log_id INT AUTO_INCREMENT PRIMARY KEY,
                        user_id INT NULL,
                        action VARCHAR(100) NOT NULL,
                        description TEXT NOT NULL,
                        created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE SET NULL,
                        INDEX idx_log_created_at (created_at)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"

                ' 8. System settings table
                Dim sqlSettings As String = "
                    CREATE TABLE IF NOT EXISTS system_settings (
                        setting_key VARCHAR(50) PRIMARY KEY,
                        setting_value TEXT NOT NULL,
                        description VARCHAR(255) NULL,
                        updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"

                Dim scripts As String() = {sqlUsers, sqlMembers, sqlPlans, sqlMemberships, sqlPayments, sqlAttendance, sqlActivityLogs, sqlSettings}
                For Each script In scripts
                    Using cmd As New MySqlCommand(script, conn)
                        cmd.ExecuteNonQuery()
                    End Using
                Next

                ' Migration: Ensure memberships.status includes 'Pending Payment'
                Try
                    Dim alterStatusSql As String = "ALTER TABLE memberships MODIFY COLUMN status ENUM('Pending Payment', 'Active', 'Expiring Soon', 'Expired', 'Cancelled') NOT NULL DEFAULT 'Pending Payment';"
                    Using alterCmd As New MySqlCommand(alterStatusSql, conn)
                        alterCmd.ExecuteNonQuery()
                    End Using
                Catch
                    ' Ignore if alter is not supported or already modified
                End Try
            End Using
        End Sub

        Private Shared Sub SeedDefaultAdmin()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim checkSql As String = "SELECT COUNT(*) FROM users WHERE username = 'admin';"
                Using checkCmd As New MySqlCommand(checkSql, conn)
                    Dim count As Long = Convert.ToInt64(checkCmd.ExecuteScalar())
                    If count = 0 Then
                        Dim hash As String = ""
                        Dim salt As String = ""
                        PasswordHasher.HashPassword("admin123", hash, salt)

                        Dim insertSql As String = "
                            INSERT INTO users (username, password_hash, password_salt, first_name, middle_name, last_name, email, contact_number, role, status)
                            VALUES (@user, @hash, @salt, @first, @mid, @last, @email, @contact, 'ADMIN', 'Active');"
                        Using insertCmd As New MySqlCommand(insertSql, conn)
                            insertCmd.Parameters.AddWithValue("@user", "admin")
                            insertCmd.Parameters.AddWithValue("@hash", hash)
                            insertCmd.Parameters.AddWithValue("@salt", salt)
                            insertCmd.Parameters.AddWithValue("@first", "System")
                            insertCmd.Parameters.AddWithValue("@mid", "Gym")
                            insertCmd.Parameters.AddWithValue("@last", "Administrator")
                            insertCmd.Parameters.AddWithValue("@email", "admin@gymsystem.local")
                            insertCmd.Parameters.AddWithValue("@contact", "09123456789")
                            insertCmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using

                ' Also seed a default staff account for testing
                Dim checkStaffSql As String = "SELECT COUNT(*) FROM users WHERE username = 'staff';"
                Using checkCmd As New MySqlCommand(checkStaffSql, conn)
                    Dim count As Long = Convert.ToInt64(checkCmd.ExecuteScalar())
                    If count = 0 Then
                        Dim hash As String = ""
                        Dim salt As String = ""
                        PasswordHasher.HashPassword("staff123", hash, salt)

                        Dim insertSql As String = "
                            INSERT INTO users (username, password_hash, password_salt, first_name, middle_name, last_name, email, contact_number, role, status)
                            VALUES (@user, @hash, @salt, @first, @mid, @last, @email, @contact, 'STAFF', 'Active');"
                        Using insertCmd As New MySqlCommand(insertSql, conn)
                            insertCmd.Parameters.AddWithValue("@user", "staff")
                            insertCmd.Parameters.AddWithValue("@hash", hash)
                            insertCmd.Parameters.AddWithValue("@salt", salt)
                            insertCmd.Parameters.AddWithValue("@first", "Front")
                            insertCmd.Parameters.AddWithValue("@mid", "Desk")
                            insertCmd.Parameters.AddWithValue("@last", "Staff")
                            insertCmd.Parameters.AddWithValue("@email", "staff@gymsystem.local")
                            insertCmd.Parameters.AddWithValue("@contact", "09987654321")
                            insertCmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End Using
        End Sub

        Private Shared Sub SeedDefaultPlans()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim checkSql As String = "SELECT COUNT(*) FROM membership_plans;"
                Using checkCmd As New MySqlCommand(checkSql, conn)
                    Dim count As Long = Convert.ToInt64(checkCmd.ExecuteScalar())
                    If count = 0 Then
                        Dim insertSql As String = "
                            INSERT INTO membership_plans (plan_name, description, duration, duration_unit, price, status) VALUES
                            ('1 Month Basic', 'Standard access to gym workout equipment for 1 month.', 1, 'Months', 1000.00, 'Active'),
                            ('3 Months Standard', 'Workout equipment access and free locker for 3 months.', 3, 'Months', 2700.00, 'Active'),
                            ('6 Months Pro', 'Full gym access, locker, and 1 fitness assessment session.', 6, 'Months', 5000.00, 'Active'),
                            ('1 Year VIP', 'Unlimited 24/7 gym access, locker, shower, and 3 trainer consultations.', 1, 'Years', 9500.00, 'Active');"
                        Using insertCmd As New MySqlCommand(insertSql, conn)
                            insertCmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End Using
        End Sub

        Private Shared Sub SeedDefaultSettings()
            Using conn As MySqlConnection = DatabaseConnection.GetOpenConnection()
                Dim checkSql As String = "SELECT COUNT(*) FROM system_settings;"
                Using checkCmd As New MySqlCommand(checkSql, conn)
                    Dim count As Long = Convert.ToInt64(checkCmd.ExecuteScalar())
                    If count = 0 Then
                        Dim insertSql As String = "
                            INSERT INTO system_settings (setting_key, setting_value, description) VALUES
                            ('gym_name', 'TITAN FITNESS & GYM CENTER', 'Official gym name on displays and receipts'),
                            ('gym_address', '123 Fitness Boulevard, Metro City', 'Gym address printed on receipts'),
                            ('gym_contact', '+63 912 345 6789 / info@titanfitness.local', 'Gym contact number and email'),
                            ('receipt_footer', 'Thank you for training with us! Stay fit and stay strong.', 'Receipt footer text'),
                            ('currency_symbol', '₱', 'Currency symbol used in cashiering'),
                            ('allow_partial_payments', 'false', 'Flag to enable or disable installment/partial payments');"
                        Using insertCmd As New MySqlCommand(insertSql, conn)
                            insertCmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End Using
        End Sub
    End Class
End Namespace
