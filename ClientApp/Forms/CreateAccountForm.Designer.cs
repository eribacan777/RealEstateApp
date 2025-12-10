namespace ClientApp
{
    partial class CreateAccountForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label fullnameLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label phoneLabel;

        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox fullnameTextBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Button backButton;
        private void InitializeComponent()
        {
            this.titleLabel = new Label();
            this.usernameLabel = new Label();
            this.passwordLabel = new Label();
            this.fullnameLabel = new Label();
            this.emailLabel = new Label();
            this.phoneLabel = new Label();

            this.usernameTextBox = new TextBox();
            this.passwordTextBox = new TextBox();
            this.fullnameTextBox = new TextBox();
            this.emailTextBox = new TextBox();
            this.phoneTextBox = new TextBox();

            this.registerButton = new Button();
            this.backButton = new Button();

            this.SuspendLayout();

            // 🌸 Form background
            this.BackColor = Color.FromArgb(255, 240, 245); // baby pastel pink

            // 🌸 Title
            this.titleLabel.Text = "Create a New Account";
            this.titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.titleLabel.ForeColor = Color.FromArgb(180, 60, 120);
            this.titleLabel.Location = new Point(70, 20);
            this.titleLabel.AutoSize = true;

            // 🌸 Label styling function
            void StyleLabel(Label lbl)
            {
                lbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                lbl.ForeColor = Color.FromArgb(150, 70, 110);
            }

            // Username
            this.usernameLabel.Text = "Username:";
            this.usernameLabel.Location = new Point(40, 70);
            StyleLabel(this.usernameLabel);

            this.usernameTextBox.Location = new Point(140, 70);
            this.usernameTextBox.Width = 180;
            this.usernameTextBox.BackColor = Color.FromArgb(255, 228, 236);

            // Password
            this.passwordLabel.Text = "Password:";
            this.passwordLabel.Location = new Point(40, 110);
            StyleLabel(this.passwordLabel);

            this.passwordTextBox.Location = new Point(140, 110);
            this.passwordTextBox.Width = 180;
            this.passwordTextBox.BackColor = Color.FromArgb(255, 228, 236);
            this.passwordTextBox.PasswordChar = '*';

            // Full Name
            this.fullnameLabel.Text = "Full Name:";
            this.fullnameLabel.Location = new Point(40, 150);
            StyleLabel(this.fullnameLabel);

            this.fullnameTextBox.Location = new Point(140, 150);
            this.fullnameTextBox.Width = 180;
            this.fullnameTextBox.BackColor = Color.FromArgb(255, 228, 236);

            // Email
            this.emailLabel.Text = "Email:";
            this.emailLabel.Location = new Point(40, 190);
            StyleLabel(this.emailLabel);

            this.emailTextBox.Location = new Point(140, 190);
            this.emailTextBox.Width = 180;
            this.emailTextBox.BackColor = Color.FromArgb(255, 228, 236);

            // Phone
            this.phoneLabel.Text = "Phone:";
            this.phoneLabel.Location = new Point(40, 230);
            StyleLabel(this.phoneLabel);

            this.phoneTextBox.Location = new Point(140, 230);
            this.phoneTextBox.Width = 180;
            this.phoneTextBox.BackColor = Color.FromArgb(255, 228, 236);

            // 🌸 Buttons styling
            void StyleButton(Button btn)
            {
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                btn.Size = new Size(100, 32);
                btn.BackColor = Color.FromArgb(255, 182, 193);
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Cursor = Cursors.Hand;
            }

            // Register button
            this.registerButton.Text = "Register";
            this.registerButton.Location = new Point(70, 280);
            StyleButton(this.registerButton);
            this.registerButton.Click += new EventHandler(this.registerButton_Click);

            // Back button
            this.backButton.Text = "Back";
            this.backButton.Location = new Point(190, 280);
            StyleButton(this.backButton);
            this.backButton.Click += new EventHandler(this.backButton_Click);

            // Form
            this.ClientSize = new Size(380, 350);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.fullnameLabel);
            this.Controls.Add(this.fullnameTextBox);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.backButton);

            this.Text = "Create Account";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
