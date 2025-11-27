using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClientApp
{
    partial class ClientLoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label usernameLabel;
        private Label passwordLabel;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Button registerButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.usernameLabel = new Label();
            this.passwordLabel = new Label();
            this.usernameTextBox = new TextBox();
            this.passwordTextBox = new TextBox();
            this.loginButton = new Button();
            this.registerButton = new Button();
            this.SuspendLayout();

            // Form settings
            this.ClientSize = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Client Login";
            this.BackColor = Color.FromArgb(255, 250, 240, 245); // pastel pink

            // usernameLabel
            this.usernameLabel.Text = "Username:";
            this.usernameLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.usernameLabel.ForeColor = Color.FromArgb(255, 255, 105, 180); // hot pink
            this.usernameLabel.Location = new Point(50, 50);
            this.usernameLabel.AutoSize = true;

            // passwordLabel
            this.passwordLabel.Text = "Password:";
            this.passwordLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.passwordLabel.ForeColor = Color.FromArgb(255, 255, 105, 180);
            this.passwordLabel.Location = new Point(50, 110);
            this.passwordLabel.AutoSize = true;

            // usernameTextBox
            this.usernameTextBox.Location = new Point(160, 50);
            this.usernameTextBox.Width = 160;
            this.usernameTextBox.Font = new Font("Segoe UI", 11);

            // passwordTextBox
            this.passwordTextBox.Location = new Point(160, 110);
            this.passwordTextBox.Width = 160;
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Font = new Font("Segoe UI", 11);

            // loginButton
            this.loginButton.Text = "Login";
            this.loginButton.Location = new Point(160, 170);
            this.loginButton.Size = new Size(160, 45);
            this.loginButton.BackColor = Color.FromArgb(255, 210, 230); // pastel pink
            this.loginButton.ForeColor = Color.White;
            this.loginButton.FlatStyle = FlatStyle.Flat;
            this.loginButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.loginButton.FlatAppearance.BorderSize = 0;
            this.loginButton.Click += new EventHandler(this.loginButton_Click);
            MakeCornersRounded(this.loginButton, 20);

            // registerButton
            this.registerButton.Text = "📝 Create Account";
            this.registerButton.Location = new Point(160, 230);
            this.registerButton.Size = new Size(160, 45);
            this.registerButton.BackColor = Color.FromArgb(255, 255, 182); // pastel yellow
            this.registerButton.ForeColor = Color.White;
            this.registerButton.FlatStyle = FlatStyle.Flat;
            this.registerButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.registerButton.FlatAppearance.BorderSize = 0;
            this.registerButton.Click += new EventHandler(this.registerButton_Click);
            MakeCornersRounded(this.registerButton, 20);

            // Add controls
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.registerButton);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

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
