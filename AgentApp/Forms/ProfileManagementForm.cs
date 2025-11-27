using System;
using System.IO;
using System.Windows.Forms;
using AgentApp.Core;
using System.Drawing;

namespace AgentApp.Forms
{
    public class ProfileManagementForm : Form
    {
        private Button btnEditDetails;
        private Button btnChangePassword;
        private Button btnDelete;
        private string agentUsername;

        public ProfileManagementForm(string username)
        {
            agentUsername = username;

            this.Text = "Profile Management - " + username;
            this.ClientSize = new Size(420, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Edit Details button
            btnEditDetails = new Button()
            {
                Text = "Edit Details",
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.DeepPink,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnEditDetails.FlatAppearance.BorderSize = 0;
            btnEditDetails.Click += BtnEditDetails_Click;

            // Change Password button
            btnChangePassword = new Button()
            {
                Text = "Change Password",
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.Click += BtnChangePassword_Click;

            // Delete Account button
            btnDelete = new Button()
            {
                Text = "Delete Account",
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;

            Controls.AddRange(new Control[] { btnDelete, btnChangePassword, btnEditDetails });
        }

        private void BtnEditDetails_Click(object? sender, EventArgs e)
        {
            var form = new EditDetailsForm(agentUsername);
            form.ShowDialog();
        }

        private void BtnChangePassword_Click(object? sender, EventArgs e)
        {
            var form = new ChangePasswordForm(agentUsername);
            form.ShowDialog();
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            var form = new DeleteAccountForm(agentUsername);
            form.ShowDialog();
        }
    }
}
