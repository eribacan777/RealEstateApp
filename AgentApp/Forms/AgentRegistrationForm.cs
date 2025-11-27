using System;
using System.IO;
using System.Windows.Forms;
using AgentApp.Core;

namespace AgentApp.Forms
{
    public class AgentRegistrationForm : Form
    {
        private TextBox txtUsername, txtPassword, txtFullName, txtEmail, txtPhone;
        private Button btnRegister;
        private string agentsFile = Path.Combine("Core", "Data", "Agents.json");

        public AgentRegistrationForm()
        {
            this.Text = "Agent Registration";
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblUsername = new Label() { Text = "Username", Location = new System.Drawing.Point(20, 30) };
            txtUsername = new TextBox() { Location = new System.Drawing.Point(120, 30), Width = 200 };

            Label lblPassword = new Label() { Text = "Password", Location = new System.Drawing.Point(20, 70) };
            txtPassword = new TextBox() { Location = new System.Drawing.Point(120, 70), Width = 200 };

            Label lblFullName = new Label() { Text = "Full Name", Location = new System.Drawing.Point(20, 110) };
            txtFullName = new TextBox() { Location = new System.Drawing.Point(120, 110), Width = 200 };

            Label lblEmail = new Label() { Text = "Email", Location = new System.Drawing.Point(20, 150) };
            txtEmail = new TextBox() { Location = new System.Drawing.Point(120, 150), Width = 200 };

            Label lblPhone = new Label() { Text = "Phone", Location = new System.Drawing.Point(20, 190) };
            txtPhone = new TextBox() { Location = new System.Drawing.Point(120, 190), Width = 200 };

            btnRegister = new Button() { Text = "Register", Location = new System.Drawing.Point(120, 230), Width = 200 };
            btnRegister.Click += BtnRegister_Click;

            Controls.AddRange(new Control[] { lblUsername, txtUsername, lblPassword, txtPassword, lblFullName, txtFullName, lblEmail, txtEmail, lblPhone, txtPhone, btnRegister });
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            var agents = DataHandler.Load<Agent>(agentsFile);

            if (Array.Exists(agents, a => a.Username == txtUsername.Text))
            {
                MessageBox.Show("Username already exists!");
                return;
            }

            Agent newAgent = new Agent
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                FullName = txtFullName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text
            };

            DataHandler.Add(agentsFile, newAgent);
            MessageBox.Show("Agent registered successfully!");
            this.Close();
        }
    }
}
