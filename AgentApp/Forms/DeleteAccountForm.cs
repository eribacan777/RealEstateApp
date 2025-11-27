using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using RealEstateApp.Core;

namespace AgentApp.Forms
{
    public class DeleteAccountForm : Form
    {
        private Button btnConfirmDelete;
        private Button btnCancel;
        private string agentUsername;

        public DeleteAccountForm(string username)
        {
            agentUsername = username;

            this.Text = "Delete Account";
            this.ClientSize = new Size(500, 220);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LavenderBlush;

            Label lblWarning = new Label()
            {
                Text = "Are you sure you want to delete your account?",
                Location = new Point(20, 40),
                Width = 460,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Black
            };

            btnConfirmDelete = new Button()
            {
                Text = "Delete",
                Location = new Point(80, 120),
                Width = 150,
                Height = 60,
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnConfirmDelete.FlatAppearance.BorderSize = 0;
            btnConfirmDelete.Click += BtnConfirmDelete_Click;

            btnCancel = new Button()
            {
                Text = "Cancel",
                Location = new Point(270, 120),
                Width = 150,
                Height = 60,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            Controls.AddRange(new Control[] { lblWarning, btnConfirmDelete, btnCancel });
        }

        private void BtnConfirmDelete_Click(object? sender, EventArgs e)
        {
            using var conn = DatabaseHelper.GetConnection("AgentAccounts.db");
            conn.Open();

            var cmd = new SQLiteCommand("DELETE FROM Agents WHERE Username=@u", conn);
            cmd.Parameters.AddWithValue("@u", agentUsername);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Account deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
