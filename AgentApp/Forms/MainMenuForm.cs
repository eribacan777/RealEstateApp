using System;
using System.Windows.Forms;

namespace AgentApp.Forms
{
    public class MainMenuForm : Form
    {
        private Button btnLogin;
        private Button btnCreateAccount;
        private Button btnBack;
        private Form previousForm;

        public MainMenuForm(Form previousForm)
        {
            this.previousForm = previousForm;

            this.Text = "AgentApp - Main Menu";
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(250, 134, 208);

            btnLogin = new Button()
            {
                Text = "Login",
                Location = new System.Drawing.Point(120, 100),
                Size = new System.Drawing.Size(150, 40),
                BackColor = System.Drawing.Color.FromArgb(217, 184, 128, 1),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold)
            };
            btnLogin.Click += (s, e) =>
            {
                this.Hide();
                var loginForm = new LoginForm();
                loginForm.FormClosed += (sender, args) => this.Show();
                loginForm.Show();
            };

            btnCreateAccount = new Button()
            {
                Text = "Create Account",
                Location = new System.Drawing.Point(120, 150),
                Size = new System.Drawing.Size(150, 40),
                BackColor = System.Drawing.Color.FromArgb(217, 184, 128, 1),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold)
            };
            btnCreateAccount.Click += (s, e) =>
            {
                var createForm = new CreateAccountForm();
                createForm.ShowDialog();
            };

            btnBack = new Button()
            {
                Text = "Back",
                Location = new System.Drawing.Point(120, 200),
                Size = new System.Drawing.Size(150, 40),
                BackColor = System.Drawing.Color.FromArgb(217, 184, 128, 1),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold)
            };
            btnBack.Click += (s, e) =>
            {
                this.Hide();
                previousForm.Show();
            };

            Controls.AddRange(new Control[] {
                btnLogin, btnCreateAccount, btnBack
            });
        }
    }
}
