using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ClientApp;
using ClientApp.Forms;

namespace AgentApp.Forms
{
    public class StartUpForm : Form
    {
        private Button btnAgent;
        private Button btnClient;
        private Button btnExit;

        public StartUpForm()
        {
            this.Text = "Welcome ✨";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(450, 320);
            this.BackColor = Color.FromArgb(255, 240, 245); // Soft pastel background

            // Buttons
            btnAgent = CreateCuteButton("Agent", new Point(50, 150));
            btnClient = CreateCuteButton("Client", new Point(250, 150));
            btnExit = CreateSmallButton("Exit", new Point(175, 240));

            btnAgent.Click += (s, e) =>
            {
                Console.WriteLine("[StartUpForm] Agent button clicked");
                var agentForm = new LoginForm();
                agentForm.Show();
                this.Hide();
            };

            btnClient.Click += (s, e) =>
            {
                Console.WriteLine("[StartUpForm] Client button clicked");
                var loginForm = new ClientLoginForm();
                loginForm.Show();
                this.Hide();
            };

            btnExit.Click += (s, e) =>
            {
                Console.WriteLine("[StartUpForm] Exit button clicked");
                Application.Exit();
            };

            Controls.Add(btnAgent);
            Controls.Add(btnClient);
            Controls.Add(btnExit);

            // Title
            Label title = new Label()
            {
                Text = "Real Estate App",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(120, 0, 0, 0),
                AutoSize = false,
                Size = new Size(450, 55),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 15)
            };
            Controls.Add(title);

            // Subtitle
            Label subtitle = new Label()
            {
                Text = "Choose your account ✨",
                Font = new Font("Segoe UI", 14, FontStyle.Italic),
                ForeColor = Color.FromArgb(255, 140, 170),
                AutoSize = false,
                Size = new Size(450, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 85)
            };
            Controls.Add(subtitle);

            // ✅ Hook into Load event
            this.Load += (s, e) =>
            {
                Console.WriteLine("[StartUpForm] Load event triggered");
            };
        }

        private Button CreateCuteButton(string text, Point location)
        {
            Button btn = new Button()
            {
                Text = text,
                Size = new Size(140, 50),
                Location = location,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 210, 230),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btn.FlatAppearance.BorderSize = 0;
            MakeCornersRounded(btn, 25);
            return btn;
        }

        private Button CreateSmallButton(string text, Point location)
        {
            Button btn = new Button()
            {
                Text = text,
                Size = new Size(100, 40),
                Location = location,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 170, 190),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btn.FlatAppearance.BorderSize = 0;
            MakeCornersRounded(btn, 18);
            return btn;
        }

        private void MakeCornersRounded(Button btn, int radius)
        {
            var path = new GraphicsPath();
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
