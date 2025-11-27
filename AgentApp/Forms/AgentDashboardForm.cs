using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using RealEstateApp.Core;

namespace AgentApp.Forms
{
    public class AgentDashboardForm : Form
    {
        private Button btnListings;
        private Button btnProfile;
        private Button btnLogout;
        private Button btnClose;
        private string agentUsername;
        private Label lblWelcome;

        public AgentDashboardForm(string username)
        {
            agentUsername = username;

            this.Text = "Agent Dashboard - " + username;
            this.ClientSize = new Size(600, 400); // wider window
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            // Get first name from DB
            string firstName = GetFirstName(username);

           // Welcome label (centered horizontally, no purple background)
            lblWelcome = new Label()
            {
                Text = $"Welcome, {firstName}!",
                AutoSize = true,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent // transparent instead of purple
            };

            // Position will be adjusted after adding controls
            this.Controls.Add(lblWelcome);
            CenterLabelHorizontally(lblWelcome, 30);

            // View Listings button
            btnListings = new Button()
            {
                Text = "View Listings",
                Location = new Point(220, 80),
                Size = new Size(160, 40),
                BackColor = Color.DeepPink,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            btnListings.FlatAppearance.BorderSize = 0;
            btnListings.Click += (s, e) =>
            {
                var listingsForm = new ManageListingsForm(username);
                listingsForm.ShowDialog();
            };

            // Create Listing button
            Button btnCreateListing = new Button()
            {
                Text = "Create Listing",
                Location = new Point(220, 130),
                Size = new Size(160, 40),
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            btnCreateListing.FlatAppearance.BorderSize = 0;
            btnCreateListing.Click += (s, e) =>
            {
                var createListingForm = new CreateListingForm(agentUsername);
                createListingForm.ShowDialog();
            };

            // Delete Listings button
            Button btnDeleteListings = new Button()
            {
                Text = "Delete Listings",
                Location = new Point(220, 180),
                Size = new Size(160, 40),
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            btnDeleteListings.FlatAppearance.BorderSize = 0;
            btnDeleteListings.Click += (s, e) =>
            {
                var deleteForm = new DeleteListingsForm(agentUsername);
                deleteForm.ShowDialog();
            };

            // Profile Management button
            btnProfile = new Button()
            {
                Text = "Profile Management",
                Location = new Point(220, 230),
                Size = new Size(160, 40),
                BackColor = Color.DeepPink,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            btnProfile.FlatAppearance.BorderSize = 0;
            btnProfile.Click += (s, e) =>
            {
                var profileForm = new ProfileManagementForm(username);
                profileForm.ShowDialog();
            };

            // Logout button
            btnLogout = new Button()
            {
                Text = "Logout",
                Location = new Point(220, 280),
                Size = new Size(160, 40),
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

            // Close button
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

            // Add controls
            Controls.Add(btnListings);
            Controls.Add(btnCreateListing);
            Controls.Add(btnDeleteListings);
            Controls.Add(btnProfile);
            Controls.Add(btnLogout);
            Controls.Add(btnClose);

            // Re-center welcome label after all controls are added
            CenterLabelHorizontally(lblWelcome, 30);
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
                    return parts[0]; // first word = first name
                }
            }
            catch
            {
                // fallback to username if DB fails
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
