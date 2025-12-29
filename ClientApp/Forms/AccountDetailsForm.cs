using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ClientApp.Core;

namespace ClientApp.Forms
{
    public class AccountDetailsForm : Form
    {
        private Client client;

        // UI elements
        private Label titleLabel;
        private Label fullNameLabel;
        private Label usernameLabel;
        private Label emailLabel;
        private Label phoneLabel;
        private Label locationLabel;
        private Label idLabel;
        private Label favoritesCountLabel;

        private Button backButton;
        private Button settingsButton;

        public AccountDetailsForm(Client client)
        {
            this.client = client;

            InitializeComponent();
            LoadClientData();
        }

        private void InitializeComponent()
        {
            this.titleLabel = new Label();
            this.fullNameLabel = new Label();
            this.usernameLabel = new Label();
            this.emailLabel = new Label();
            this.phoneLabel = new Label();
            this.locationLabel = new Label();
            this.idLabel = new Label();
            this.favoritesCountLabel = new Label();

            this.backButton = new Button();
            this.settingsButton = new Button();

            this.SuspendLayout();

            // Form setup
            this.ClientSize = new Size(420, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Account Details";
            this.BackColor = Color.FromArgb(255, 250, 240, 245); // pastel background

            // Title
            this.titleLabel.Text = "👤 Account Details";
            this.titleLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.titleLabel.Location = new Point(20, 20);
            this.titleLabel.AutoSize = true;
            this.titleLabel.ForeColor = Color.FromArgb(255, 255, 105, 180);

            int x = 40;
            int y = 80;

            // Labels
            this.fullNameLabel.Location = new Point(x, y);
            this.usernameLabel.Location = new Point(x, y + 30);
            this.emailLabel.Location = new Point(x, y + 60);
            this.phoneLabel.Location = new Point(x, y + 90);
            this.locationLabel.Location = new Point(x, y + 120);
            this.idLabel.Location = new Point(x, y + 150);
            this.favoritesCountLabel.Location = new Point(x, y + 180);

            foreach (var lbl in new[] { fullNameLabel, usernameLabel, emailLabel, phoneLabel, locationLabel, idLabel, favoritesCountLabel })
            {
                lbl.Font = new Font("Segoe UI", 10);
                lbl.AutoSize = true;
                lbl.ForeColor = Color.FromArgb(120, 0, 0, 0);
            }

            // Back button
            this.backButton.Text = "⬅ Back";
            this.backButton.Size = new Size(100, 35);
            this.backButton.Location = new Point(60, 260);
            this.backButton.BackColor = Color.Gray;
            this.backButton.ForeColor = Color.White;
            this.backButton.FlatStyle = FlatStyle.Flat;
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.backButton.Click += BackButton_Click;
            MakeCornersRounded(this.backButton, 15);

            // Settings button
            this.settingsButton.Text = "⚙ Settings";
            this.settingsButton.Size = new Size(100, 35);
            this.settingsButton.Location = new Point(220, 260);
            this.settingsButton.BackColor = Color.MediumPurple;
            this.settingsButton.ForeColor = Color.White;
            this.settingsButton.FlatStyle = FlatStyle.Flat;
            this.settingsButton.FlatAppearance.BorderSize = 0;
            this.settingsButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.settingsButton.Click += SettingsButton_Click;
            MakeCornersRounded(this.settingsButton, 15);

            // Add controls
            this.Controls.AddRange(new Control[]
            {
                titleLabel, fullNameLabel, usernameLabel, emailLabel, phoneLabel,
                locationLabel, idLabel, favoritesCountLabel, backButton, settingsButton
            });

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadClientData()
        {
            if (client == null)
            {
                MessageBox.Show("Client information not found.");
                return;
            }

            fullNameLabel.Text = $"Full Name: {client.FullName}";
            usernameLabel.Text = $"Username: {client.Username}";
            emailLabel.Text = $"Email: {client.Email}";
            phoneLabel.Text = $"Phone: {client.PhoneNumber ?? "Not set"}";
            locationLabel.Text = $"Preferred Location: {client.PreferredLocation ?? "Not set"}";
            idLabel.Text = $"Client ID: {client.Id}";
            favoritesCountLabel.Text = $"Favorites: {client.FavoritePropertyIds.Count}";
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            if (client == null) return;

            var profileForm = new ProfileManagementForm(client.Username);
            profileForm.ShowDialog();
        }

        // Helper to round button corners
        private void MakeCornersRounded(Button btn, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int w = btn.Width;
            int h = btn.Height;

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(w - radius, 0, radius, radius, 270, 90);
            path.AddArc(w - radius, h - radius, radius, radius, 0, 90);
            path.AddArc(0, h - radius, radius, radius, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
        }
    }
}


/*using System;
using System.Drawing;
using System.Windows.Forms;
using ClientApp.Core;

namespace ClientApp.Forms
{
    public class AccountDetailsForm : Form
    {
        private Client client;

        // UI elements
        private Label titleLabel;
        private Label fullNameLabel;
        private Label usernameLabel;
        private Label emailLabel;
        private Label phoneLabel;
        private Label locationLabel;
        private Label idLabel;
        private Label favoritesCountLabel;

        private Button backButton;

        public AccountDetailsForm(Client client)
        {
            this.client = client;

            InitializeComponent();
            LoadClientData();
        }

        private void InitializeComponent()
        {
            this.titleLabel = new Label();
            this.fullNameLabel = new Label();
            this.usernameLabel = new Label();
            this.emailLabel = new Label();
            this.phoneLabel = new Label();
            this.locationLabel = new Label();
            this.idLabel = new Label();
            this.favoritesCountLabel = new Label();

            this.backButton = new Button();

            this.SuspendLayout();

            // Title
            this.titleLabel.Text = "👤 Account Details";
            this.titleLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.titleLabel.Location = new Point(20, 20);
            this.titleLabel.AutoSize = true;

            int x = 40;
            int y = 80;

            // Full Name
            this.fullNameLabel.Location = new Point(x, y);
            this.fullNameLabel.Font = new Font("Segoe UI", 10);
            this.fullNameLabel.AutoSize = true;

            // Username
            this.usernameLabel.Location = new Point(x, y + 30);
            this.usernameLabel.Font = new Font("Segoe UI", 10);
            this.usernameLabel.AutoSize = true;

            // Email
            this.emailLabel.Location = new Point(x, y + 60);
            this.emailLabel.Font = new Font("Segoe UI", 10);
            this.emailLabel.AutoSize = true;

            // Phone
            this.phoneLabel.Location = new Point(x, y + 90);
            this.phoneLabel.Font = new Font("Segoe UI", 10);
            this.phoneLabel.AutoSize = true;

            // Preferred Location
            this.locationLabel.Location = new Point(x, y + 120);
            this.locationLabel.Font = new Font("Segoe UI", 10);
            this.locationLabel.AutoSize = true;

            // Client ID
            this.idLabel.Location = new Point(x, y + 150);
            this.idLabel.Font = new Font("Segoe UI", 10);
            this.idLabel.AutoSize = true;

            // Favorites count
            this.favoritesCountLabel.Location = new Point(x, y + 180);
            this.favoritesCountLabel.Font = new Font("Segoe UI", 10);
            this.favoritesCountLabel.AutoSize = true;

            // Back button
            this.backButton.Text = "⬅ Back";
            this.backButton.Size = new Size(80, 30);
            this.backButton.Location = new Point(300, 260);
            this.backButton.Click += backButton_Click;

            // Form setup
            this.ClientSize = new Size(420, 320);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.fullNameLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.favoritesCountLabel);
            this.Controls.Add(this.backButton);

            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Account Details";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadClientData()
        {
            if (client == null)
            {
                MessageBox.Show("Client information not found.");
                return;
            }

            fullNameLabel.Text = $"Full Name: {client.FullName}";
            usernameLabel.Text = $"Username: {client.Username}";
            emailLabel.Text = $"Email: {client.Email}";
            phoneLabel.Text = $"Phone: {client.PhoneNumber ?? "Not set"}";
            locationLabel.Text = $"Preferred Location: {client.PreferredLocation ?? "Not set"}";
            idLabel.Text = $"Client ID: {client.Id}";
            favoritesCountLabel.Text = $"Favorites: {client.FavoritePropertyIds.Count}";
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}*/
