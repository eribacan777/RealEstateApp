using System;
using System.Windows.Forms;
using ClientApp.Core;
using ClientApp.Forms;

namespace ClientApp
{
    public partial class ClientLoginForm : Form
    {
        public ClientLoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Validate login
            var client = ClientLogin.Validate(username, password);

            if (client != null)
            {
                MessageBox.Show($"Welcome, {client.FullName}!");

                // Open dashboard
                var dashboard = new ClientDashboardForm(client);
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            var registerForm = new CreateAccountForm();
            registerForm.Show();
            this.Hide();
        }
    }
}
