using System;
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
}
