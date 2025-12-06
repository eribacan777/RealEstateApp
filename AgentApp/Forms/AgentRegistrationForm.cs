using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using RealEstateApp.Core;

namespace AgentApp.Forms
{
    public class AgentRegistrationForm : Form
    {
        private TextBox txtUsername, txtPassword, txtFullName, txtEmail, txtPhone;
        private Button btnRegister;

        public AgentRegistrationForm()
        {
            this.Text = "Agent Registration";
            this.ClientSize = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblUsername = new Label() { Text = "Username", Location = new Point(20, 30) };
            txtUsername = new TextBox() { Location = new Point(120, 30), Width = 200 };

            Label lblPassword = new Label() { Text = "Password", Location = new Point(20, 70) };
            txtPassword = new TextBox() { Location = new Point(120, 70), Width = 200 };

            Label lblFullName = new Label() { Text = "Full Name", Location = new Point(20, 110) };
            txtFullName = new TextBox() { Location = new Point(120, 110), Width = 200 };

            Label lblEmail = new Label() { Text = "Email", Location = new Point(20, 150) };
            txtEmail = new TextBox() { Location = new Point(120, 150), Width = 200 };

            Label lblPhone = new Label() { Text = "Phone", Location = new Point(20, 190) };
            txtPhone = new TextBox() { Location = new Point(120, 190), Width = 200 };

            btnRegister = new Button() { Text = "Register", Location = new Point(120, 230), Width = 200 };
            btnRegister.Click += BtnRegister_Click;

            Controls.AddRange(new Control[] {
                lblUsername, txtUsername,
                lblPassword, txtPassword,
                lblFullName, txtFullName,
                lblEmail, txtEmail,
                lblPhone, txtPhone,
                btnRegister
            });
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            try
            {
                using var conn = DatabaseHelper.GetConnection("AgentAccounts.db");
                conn.Open();

                var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM Agents WHERE Username=@u", conn);
                checkCmd.Parameters.AddWithValue("@u", txtUsername.Text);
                long count = (long)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Username already exists!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var insertCmd = new SQLiteCommand(
                    "INSERT INTO Agents (Username, Password, FullName, Email, Phone) VALUES (@u, @p, @f, @e, @ph)", conn);
                insertCmd.Parameters.AddWithValue("@u", txtUsername.Text);
                insertCmd.Parameters.AddWithValue("@p", txtPassword.Text);
                insertCmd.Parameters.AddWithValue("@f", txtFullName.Text);
                insertCmd.Parameters.AddWithValue("@e", txtEmail.Text);
                insertCmd.Parameters.AddWithValue("@ph", txtPhone.Text);

                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Agent registered successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering agent:\n" + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
