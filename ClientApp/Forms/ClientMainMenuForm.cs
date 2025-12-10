using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ClientApp.Core;

namespace ClientApp.Forms
{
    public class ClientMainMenuForm : Form
    {
        private Button btnLogin;
        private Button btnCreateAccount;
        private Button btnAccountDetails;
        private Button btnBack;
        private Label titleLabel;

        private Form previousForm;
        private Client currentClient; // ⭐ Added

        public ClientMainMenuForm(Form previousForm, Client currentClient = null)
        {
            this.previousForm = previousForm;
            this.currentClient = currentClient;

            // 🌸 Form Setup
            this.Text = "Client Menu 💖";
            this.ClientSize = new Size(420, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(255, 235, 245);

            // 🌸 Title Label
            titleLabel = new Label()
            {
                Text = "Welcome, lovely client! ✨",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 50, 90),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(420, 80),
                Location = new Point(0, 20)
            };
            Controls.Add(titleLabel);

            // 🌸 Menu Buttons
            btnLogin = CreateCuteButton("💗 Login", new Point(120, 120));
            btnCreateAccount = CreateCuteButton("🌸 Create Account", new Point(120, 190));
            btnAccountDetails = CreateCuteButton("💎 Account Details", new Point(120, 260));
            btnBack = CreateSmallCuteButton("⬅ Back", new Point(120, 330));

            // 🌸 Navigation Logic

            // Login
            btnLogin.Click += (s, e) =>
            {
                this.Hide();
                var loginForm = new ClientLoginForm();
                loginForm.FormClosed += (sender, args) => this.Show();
                loginForm.Show();
            };

            // Create Account
            btnCreateAccount.Click += (s, e) =>
            {
                var createForm = new CreateAccountForm();
                createForm.ShowDialog();
            };

            // ⭐ NEW — Open Account Details
            btnAccountDetails.Click += (s, e) =>
            {
                if (currentClient == null)
                {
                    MessageBox.Show("You must log in first!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var detailsForm = new AccountDetailsForm(currentClient);
                detailsForm.ShowDialog();
            };

            // Back
            btnBack.Click += (s, e) =>
            {
                this.Hide();
                previousForm.Show();
            };

            Controls.Add(btnLogin);
            Controls.Add(btnCreateAccount);
            Controls.Add(btnAccountDetails);
            Controls.Add(btnBack);
        }

        // 🌸 Helper: Cute Rounded Button
        private Button CreateCuteButton(string text, Point location)
        {
            Button btn = new Button()
            {
                Text = text,
                Location = location,
                Size = new Size(180, 50),
                BackColor = Color.FromArgb(255, 182, 210),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            btn.FlatAppearance.BorderSize = 0;
            MakeRoundCorners(btn);

            return btn;
        }

        // 🌸 Small cute button
        private Button CreateSmallCuteButton(string text, Point location)
        {
            Button btn = new Button()
            {
                Text = text,
                Location = location,
                Size = new Size(180, 45),
                BackColor = Color.FromArgb(255, 160, 195),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            btn.FlatAppearance.BorderSize = 0;
            MakeRoundCorners(btn);

            return btn;
        }

        // 🌸 Rounded Corners
        private void MakeRoundCorners(Button btn)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = 25;
            int w = btn.Width, h = btn.Height;

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(w - radius, 0, radius, radius, 270, 90);
            path.AddArc(w - radius, h - radius, radius, radius, 0, 90);
            path.AddArc(0, h - radius, radius, radius, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
        }
    }
}
