namespace ClientApp
{
    partial class ClientDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.Button viewListingsButton;
        private System.Windows.Forms.Button favoritesButton;
        private System.Windows.Forms.Button requestMeetingButton;
        private System.Windows.Forms.Button logoutButton;

        private void InitializeComponent()
        {
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.viewListingsButton = new System.Windows.Forms.Button();
            this.favoritesButton = new System.Windows.Forms.Button();
            this.requestMeetingButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 🌸 Background (soft pastel gradient)
            this.BackColor = System.Drawing.Color.FromArgb(255, 240, 245); // soft pink

            // 🌟 Welcome label (cute aesthetic)
            this.welcomeLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.welcomeLabel.ForeColor = System.Drawing.Color.FromArgb(255, 120, 140);
            this.welcomeLabel.Text = "Welcome Back ✨";
            this.welcomeLabel.Location = new System.Drawing.Point(80, 25);
            this.welcomeLabel.AutoSize = true;

            // 🌸 Buttons (cute rounded pastel)
            StyleCuteButton(this.viewListingsButton, "🏡 View Listings", 90);
            StyleCuteButton(this.favoritesButton, "💖 Favorite Properties", 150);
            StyleCuteButton(this.requestMeetingButton, "📅 Request Meeting", 210);

            // Logout (smaller, still cute)
            this.logoutButton.Text = "🚪 Logout";
            this.logoutButton.Location = new System.Drawing.Point(110, 270);
            this.logoutButton.Size = new System.Drawing.Size(180, 40);
            this.logoutButton.BackColor = System.Drawing.Color.FromArgb(255, 200, 210);
            this.logoutButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.logoutButton.ForeColor = System.Drawing.Color.White;
            this.logoutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logoutButton.FlatAppearance.BorderSize = 0;
            this.logoutButton.Region = new System.Drawing.Region(
                CreateRoundedPath(this.logoutButton.Width, this.logoutButton.Height, 25)
            );
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);

            // Form setup
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this.welcomeLabel);
            this.Controls.Add(this.viewListingsButton);
            this.Controls.Add(this.favoritesButton);
            this.Controls.Add(this.requestMeetingButton);
            this.Controls.Add(this.logoutButton);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client Dashboard";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // 🎀 Helper method to style cute buttons
        private void StyleCuteButton(System.Windows.Forms.Button btn, string text, int y)
        {
            btn.Text = text;
            btn.Location = new System.Drawing.Point(110, y);
            btn.Size = new System.Drawing.Size(180, 45);
            btn.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btn.BackColor = System.Drawing.Color.FromArgb(255, 210, 230);
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.Region = new System.Drawing.Region(
                CreateRoundedPath(btn.Width, btn.Height, 25)
            );
        }

        // 🎀 Rounded corner path
        private System.Drawing.Drawing2D.GraphicsPath CreateRoundedPath(int width, int height, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(width - radius, 0, radius, radius, 270, 90);
            path.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            path.AddArc(0, height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
