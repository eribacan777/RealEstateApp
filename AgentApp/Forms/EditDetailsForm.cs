using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using RealEstateApp.Core;

namespace AgentApp.Forms
{
    public class EditDetailsForm : Form
    {
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private Button btnSave;
        private Button btnClose;
        private string agentUsername;

        public EditDetailsForm(string username)
        {
            agentUsername = username;

            this.Text = "Edit Profile Details";
            this.ClientSize = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            // Full Name
            Label lblFullName = new Label()
            {
                Text = "Full Name",
                Location = new Point(40, 60),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtFullName = new TextBox()
            {
                Location = new Point(180, 60),
                Width = 180,
                BackColor = Color.LavenderBlush
            };

            // Email
            Label lblEmail = new Label()
            {
                Text = "Email",
                Location = new Point(40, 100),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtEmail = new TextBox()
            {
                Location = new Point(180, 100),
                Width = 180,
                BackColor = Color.LavenderBlush
            };

            // Phone
            Label lblPhone = new Label()
            {
                Text = "Phone",
                Location = new Point(40, 140),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtPhone = new TextBox()
            {
                Location = new Point(180, 140),
                Width = 180,
                BackColor = Color.LavenderBlush
            };

            // Save button
            btnSave = new Button()
            {
                Text = "Save",
                Location = new Point(140, 200),
                Width = 120,
                Height = 40,
                BackColor = Color.DeepPink,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            // Close button (X)
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

            Controls.AddRange(new Control[] { lblFullName, txtFullName, lblEmail, txtEmail, lblPhone, txtPhone, btnSave, btnClose });

            LoadAgentData();
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

        private void LoadAgentData()
        {
            using var conn = DatabaseHelper.GetConnection("AgentAccounts.db");
            conn.Open();

            var cmd = new SQLiteCommand("SELECT FullName, Email, PhoneNumber FROM Agents WHERE Username=@u", conn);
            cmd.Parameters.AddWithValue("@u", agentUsername);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtFullName.Text = reader["FullName"]?.ToString() ?? "";
                txtEmail.Text = reader["Email"]?.ToString() ?? "";
                txtPhone.Text = reader["PhoneNumber"]?.ToString() ?? "";
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            using var conn = DatabaseHelper.GetConnection("AgentAccounts.db");
            conn.Open();

            var cmd = new SQLiteCommand(@"
                UPDATE Agents 
                SET FullName=@f, Email=@e, PhoneNumber=@p 
                WHERE Username=@u;", conn);
            cmd.Parameters.AddWithValue("@f", txtFullName.Text);
            cmd.Parameters.AddWithValue("@e", txtEmail.Text);
            cmd.Parameters.AddWithValue("@p", txtPhone.Text);
            cmd.Parameters.AddWithValue("@u", agentUsername);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
