using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;
using RealEstateApp.Core;


namespace AgentApp.Forms
{
    public class LoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnCreateAccount;
        private Button btnClose;
        private Button btnBack;

        public LoginForm(Form? previousForm = null)
        {
            this.Text = "Agent Login";
            this.ClientSize = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            Label lblUsername = new Label()
            {
                Text = "Username",
                Location = new Point(40, 60),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtUsername = new TextBox()
            {
                Location = new Point(150, 60),
                Width = 200,
                BackColor = Color.LavenderBlush
            };

            Label lblPassword = new Label()
            {
                Text = "Password",
                Location = new Point(40, 100),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtPassword = new TextBox()
            {
                Location = new Point(150, 100),
                Width = 200,
                PasswordChar = '*',
                BackColor = Color.LavenderBlush
            };

            btnLogin = new Button()
            {
                Text = "Login",
                Location = new Point(150, 150),
                Width = 200,
                Height = 40,
                BackColor = Color.DeepPink,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            btnCreateAccount = new Button()
            {
                Text = "Create Account",
                Location = new Point(150, 200),
                Width = 200,
                Height = 40,
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnCreateAccount.FlatAppearance.BorderSize = 0;
            btnCreateAccount.Click += (s, e) =>
            {
                var createForm = new CreateAccountForm();
                createForm.ShowDialog();
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
            btnClose.Click += (s, e) => Application.Exit();

            btnBack = new Button()
            {
                Text = "← Back",
                ForeColor = Color.White,
                BackColor = Color.Gray,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(10, 10),
                Size = new Size(60, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) =>
            {
                this.Hide();
                var startupForm = new StartUpForm();
                startupForm.Show();
            };

            Controls.AddRange(new Control[] { lblUsername, txtUsername, lblPassword, txtPassword, btnLogin, btnCreateAccount, btnClose, btnBack });
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

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                using var conn = DatabaseHelper.GetConnection("AgentAccounts.db");
                conn.Open();

                var cmd = new SQLiteCommand("SELECT * FROM Agents WHERE Username=@u AND Password=@p", conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    this.Hide();
                    var dashboard = new AgentDashboardForm(username);
                    dashboard.FormClosed += (s, args) => this.Show();
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show("Invalid agent credentials.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login error:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
