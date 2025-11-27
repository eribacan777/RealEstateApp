using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClientApp.Forms
{
    public class ClientMainMenuForm : Form
    {
        private Button btnLogin;
        private Button btnCreateAccount;
        private Button btnBack;
        private Label titleLabel;
        private Form previousForm;

        public ClientMainMenuForm(Form previousForm)
        {
            this.previousForm = previousForm;

            // 🌸 Form Setup
            this.Text = "Client Menu 💖";
            this.ClientSize = new Size(420, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(255, 235, 245); // soft pink

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

            // 🌸 Rounded Cute Buttons
            btnLogin = CreateCuteButton("💗 Login", new Point(120, 120));
            btnCreateAccount = CreateCuteButton("🌸 Create Account", new Point(120, 190));
            btnBack = CreateSmallCuteButton("⬅ Back", new Point(120, 260));

            // Navigation Logic
            btnLogin.Click += (s, e) =>
            {
                this.Hide();
                var loginForm = new ClientLoginForm();
                loginForm.FormClosed += (sender, args) => this.Show();
                loginForm.Show();
            };

            btnCreateAccount.Click += (s, e) =>
            {
                var createForm = new CreateAccountForm();
                createForm.ShowDialog();
            };

            btnBack.Click += (s, e) =>
            {
                this.Hide();
                previousForm.Show();
            };

            Controls.Add(btnLogin);
            Controls.Add(btnCreateAccount);
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
                BackColor = Color.FromArgb(255, 182, 210), // darker pastel pink
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            btn.FlatAppearance.BorderSize = 0;
            MakeRoundCorners(btn);

            return btn;
        }

        // 🌸 Smaller cute button for "Back"
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
