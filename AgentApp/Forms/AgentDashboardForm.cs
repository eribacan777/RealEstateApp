using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using RealEstateApp.Core;

namespace AgentApp.Forms
{
    public class AgentDashboardForm : Form
    {
        private Button btnManageListings;
        private Button btnManageMeetings;
        private Button btnProfile;
        private Button btnLogout;
        private Button btnClose;
        private string agentUsername;
        private Label lblWelcome;

        public AgentDashboardForm(string username)
        {
            agentUsername = username;

            this.Text = "Agent Dashboard - " + username;
            this.ClientSize = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            string firstName = GetFirstName(username);

            lblWelcome = new Label()
            {
                Text = $"Welcome, {firstName}!",
                AutoSize = true,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblWelcome);
            CenterLabelHorizontally(lblWelcome, 40);

            // Wide buttons
            int wideX = 180;
            int wideY = 120;
            int wideWidth = 240;
            int wideHeight = 45;
            int spacing = 60;

            // Smaller buttons (Profile + Logout)
            int smallWidth = 160;
            int smallHeight = 40;
            int smallX = 220;

            btnManageListings = new Button()
            {
                Text = "Manage Listings",
                Location = new Point(wideX, wideY),
                Size = new Size(wideWidth, wideHeight),
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            btnManageListings.FlatAppearance.BorderSize = 0;
            btnManageListings.Click += (s, e) =>
            {
                var listingsForm = new ManageListingsForm(agentUsername);
                listingsForm.ShowDialog();
            };

            btnManageMeetings = new Button()
            {
                Text = "Manage Meetings",
                Location = new Point(wideX, wideY + spacing),
                Size = new Size(wideWidth, wideHeight),
                BackColor = Color.MediumOrchid,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            btnManageMeetings.FlatAppearance.BorderSize = 0;
            btnManageMeetings.Click += (s, e) =>
            {
                var meetingsForm = new ManageRequestsForm(agentUsername);
                meetingsForm.ShowDialog();
            };

            // EXTRA SPACE after Manage Meetings
            int afterMeetingsOffset = spacing + 20;

            btnProfile = new Button()
            {
                Text = "Profile",
                Location = new Point(smallX, wideY + afterMeetingsOffset * 2),
                Size = new Size(smallWidth, smallHeight),
                BackColor = Color.DeepPink,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            btnProfile.FlatAppearance.BorderSize = 0;
            btnProfile.Click += (s, e) =>
            {
                var profileForm = new ProfileManagementForm(agentUsername);
                profileForm.ShowDialog();
            };

            // LOGOUT DIRECTLY UNDER PROFILE (NO EXTRA SPACE)
            btnLogout = new Button()
            {
                Text = "Logout",
                Location = new Point(smallX, wideY + afterMeetingsOffset * 2 + smallHeight + 5),
                Size = new Size(smallWidth, smallHeight),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) =>
            {
                this.Close();
            };

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

            Controls.Add(btnManageListings);
            Controls.Add(btnManageMeetings);
            Controls.Add(btnProfile);
            Controls.Add(btnLogout);
            Controls.Add(btnClose);

            CenterLabelHorizontally(lblWelcome, 40);
        }

        private string GetFirstName(string username)
        {
            try
            {
                using var conn = DatabaseHelper.GetConnection("AgentAccounts.db");
                conn.Open();

                var cmd = new SQLiteCommand("SELECT FullName FROM Agents WHERE Username=@u", conn);
                cmd.Parameters.AddWithValue("@u", username);

                var fullName = cmd.ExecuteScalar()?.ToString();
                if (!string.IsNullOrEmpty(fullName))
                {
                    var parts = fullName.Split(' ');
                    return parts[0];
                }
            }
            catch
            {
            }
            return username;
        }

        private void CenterLabelHorizontally(Label lbl, int y)
        {
            lbl.Location = new Point((this.ClientSize.Width - lbl.Width) / 2, y);
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
    }
}
