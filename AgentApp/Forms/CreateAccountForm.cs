using System;
using System.Drawing;
using System.Windows.Forms;
using RealEstateApp.Core;
using System.Data.SQLite;

namespace AgentApp.Forms
{
    public class CreateAccountForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirm;
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private Button btnCreate;
        private Button btnCancel;
        private Button btnClose;

        public CreateAccountForm()
        {
            this.Text = "Create Account";
            this.ClientSize = new Size(400, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            // Username
            Label lblUsername = new Label() { Text = "Username", Location = new Point(40, 60), ForeColor = Color.White, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtUsername = new TextBox() { Location = new Point(180, 60), Width = 180, BackColor = Color.LavenderBlush };

            // Password
            Label lblPassword = new Label() { Text = "Password", Location = new Point(40, 100), ForeColor = Color.White, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtPassword = new TextBox() { Location = new Point(180, 100), Width = 180, PasswordChar = '*', BackColor = Color.LavenderBlush };

            // Confirm Password
            Label lblConfirm = new Label() { Text = "Confirm Password", Location = new Point(40, 140), ForeColor = Color.White, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtConfirm = new TextBox() { Location = new Point(180, 140), Width = 180, PasswordChar = '*', BackColor = Color.LavenderBlush };

            // Full Name
            Label lblFullName = new Label() { Text = "Full Name", Location = new Point(40, 180), ForeColor = Color.White, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtFullName = new TextBox() { Location = new Point(180, 180), Width = 180, BackColor = Color.LavenderBlush };

            // Email
            Label lblEmail = new Label() { Text = "Email", Location = new Point(40, 220), ForeColor = Color.White, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtEmail = new TextBox() { Location = new Point(180, 220), Width = 180, BackColor = Color.LavenderBlush };

            // Phone
            Label lblPhone = new Label() { Text = "Phone", Location = new Point(40, 260), ForeColor = Color.White, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtPhone = new TextBox() { Location = new Point(180, 260), Width = 180, BackColor = Color.LavenderBlush };

            // Create button
            btnCreate = new Button() { Text = "Create", Location = new Point(80, 320), Width = 120, Height = 40, BackColor = Color.DeepPink, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.Click += BtnCreate_Click;

            // Cancel button
            btnCancel = new Button() { Text = "Cancel", Location = new Point(220, 320), Width = 120, Height = 40, BackColor = Color.MediumPurple, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            // Close button
            btnClose = new Button() { Text = "X", ForeColor = Color.White, BackColor = Color.Transparent, FlatStyle = FlatStyle.Flat, Location = new Point(this.ClientSize.Width - 40, 10), Size = new Size(30, 30), Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            Controls.AddRange(new Control[] { lblUsername, txtUsername, lblPassword, txtPassword, lblConfirm, txtConfirm, lblFullName, txtFullName, lblEmail, txtEmail, lblPhone, txtPhone, btnCreate, btnCancel, btnClose });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rect = this.ClientRectangle;
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, Color.MediumPurple, Color.DeepPink, 45F))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            base.OnPaint(e);
        }

        private void BtnCreate_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirm = txtConfirm.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = DatabaseHelper.GetConnection("AgentAccounts.db");
                conn.Open();

                var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM Agents WHERE Username=@u", conn);
                checkCmd.Parameters.AddWithValue("@u", username);
                long count = (long)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var insertCmd = new SQLiteCommand(
                    "INSERT INTO Agents (Username, Password, FullName, Email, PhoneNumber) VALUES (@u, @p, @n, @e, @ph)", conn);
                insertCmd.Parameters.AddWithValue("@u", username);
                insertCmd.Parameters.AddWithValue("@p", password);
                insertCmd.Parameters.AddWithValue("@n", fullName);
                insertCmd.Parameters.AddWithValue("@e", email);
                insertCmd.Parameters.AddWithValue("@ph", phone);

                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Agent account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating account:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
