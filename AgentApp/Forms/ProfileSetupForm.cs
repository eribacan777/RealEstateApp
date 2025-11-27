using System.Windows.Forms;
using AgentApp.Core;

namespace AgentApp.Forms
{
    public class ProfileSetupForm : Form

    {
        private TextBox txtFullName;
        private TextBox txtPhone;
        private Button btnSave;

        public ProfileSetupForm(string username)
        {
            this.Text = "Profile Setup";
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            var lblFullName = new Label() { Text = "Full Name", Location = new System.Drawing.Point(50, 30), AutoSize = true };
            txtFullName = new TextBox() { Location = new System.Drawing.Point(150, 30), Width = 180 };

            var lblPhone = new Label() { Text = "Phone", Location = new System.Drawing.Point(50, 70), AutoSize = true };
            txtPhone = new TextBox() { Location = new System.Drawing.Point(150, 70), Width = 180 };

            btnSave = new Button()
            {
                Text = "Save",
                Location = new System.Drawing.Point(150, 130),
                Size = new System.Drawing.Size(120, 40)
            };

            btnSave.Click += (s, e) =>
            {
                btnSave.Click += (s, e) =>
                {
                    ProfileManager.SaveProfile(username, txtFullName.Text.Trim(), txtPhone.Text.Trim());
                    MessageBox.Show("Profile saved successfully.");
                    this.Close();
                };

            };

            Controls.AddRange(new Control[] {
                lblFullName, txtFullName,
                lblPhone, txtPhone,
                btnSave
            });
        }
    }
}
