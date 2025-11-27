using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using RealEstateApp.Core;

namespace AgentApp.Forms
{
    public class ChangePasswordForm : Form
    {
        private TextBox txtOldPassword;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private Button btnSave;
        private Button btnClose;
        private string agentUsername;

        public ChangePasswordForm(string username)
        {
            agentUsername = username;

            this.Text = "Change Password";
            this.ClientSize = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            Label lblOld = new Label()
            {
                Text = "Old Password",
                Location = new Point(40, 40),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtOldPassword = new TextBox()
            {
                Location = new Point(160, 40),
                Width = 180,
                UseSystemPasswordChar = true,
                BackColor = Color.LavenderBlush
            };

            Label lblNew = new Label()
            {
                Text = "New Password",
                Location = new Point(40, 80),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtNewPassword = new TextBox()
            {
                Location = new Point(160, 80),
                Width = 180,
                UseSystemPasswordChar = true,
                BackColor = Color.LavenderBlush
            };

            Label lblConfirm = new Label()
            {
                Text = "Confirm Password",
                Location = new Point(40, 120),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtConfirmPassword = new TextBox()
            {
                Location = new Point(160, 120),
                Width = 180,
                UseSystemPasswordChar = true,
                BackColor = Color.LavenderBlush
            };

            // Smaller Save button
            btnSave = new Button()
            {
                Text = "Save",
                Location = new Point(160, 170),
                Width = 100,
                Height = 35,
                BackColor = Color.DeepPink,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            // X button (top-right corner)
            btnClose = new Button()
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(this.ClientSize.Width - 40, 10),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            Controls.AddRange(new Control[] { lblOld, txtOldPassword, lblNew, txtNewPassword, lblConfirm, txtConfirmPassword, btnSave, btnClose });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rect = this.ClientRectangle;
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                rect, Color.MediumPurple, Color.DeepPink, 45F))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            base.OnPaint(e);
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            using var conn = DatabaseHelper.GetConnection("AgentAccounts.db");
            conn.Open();

            // Verify old password
            var checkCmd = new SQLiteCommand("SELECT Password FROM Agents WHERE Username=@u", conn);
            checkCmd.Parameters.AddWithValue("@u", agentUsername);
            var currentPassword = checkCmd.ExecuteScalar()?.ToString();

            if (currentPassword != txtOldPassword.Text)
            {
                MessageBox.Show("Old password is incorrect.");
                return;
            }

            // Update password
            var updateCmd = new SQLiteCommand("UPDATE Agents SET Password=@p WHERE Username=@u", conn);
            updateCmd.Parameters.AddWithValue("@p", txtNewPassword.Text);
            updateCmd.Parameters.AddWithValue("@u", agentUsername);
            updateCmd.ExecuteNonQuery();

            MessageBox.Show("Password changed successfully.");
            this.Close();
        }
    }
}
