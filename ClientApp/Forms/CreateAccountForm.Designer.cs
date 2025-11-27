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
            this.titleLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.fullnameLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.phoneLabel = new System.Windows.Forms.Label();

            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.fullnameTextBox = new System.Windows.Forms.TextBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.phoneTextBox = new System.Windows.Forms.TextBox();

            this.registerButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // Title
            this.titleLabel.Text = "Create a New Account";
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(90, 20);
            this.titleLabel.AutoSize = true;

            // Username
            this.usernameLabel.Text = "Username:";
            this.usernameLabel.Location = new System.Drawing.Point(40, 70);
            this.usernameTextBox.Location = new System.Drawing.Point(140, 70);
            this.usernameTextBox.Width = 180;

            // Password
            this.passwordLabel.Text = "Password:";
            this.passwordLabel.Location = new System.Drawing.Point(40, 110);
            this.passwordTextBox.Location = new System.Drawing.Point(140, 110);
            this.passwordTextBox.Width = 180;
            this.passwordTextBox.PasswordChar = '*';

            // Full Name
            this.fullnameLabel.Text = "Full Name:";
            this.fullnameLabel.Location = new System.Drawing.Point(40, 150);
            this.fullnameTextBox.Location = new System.Drawing.Point(140, 150);
            this.fullnameTextBox.Width = 180;

            // Email
            this.emailLabel.Text = "Email:";
            this.emailLabel.Location = new System.Drawing.Point(40, 190);
            this.emailTextBox.Location = new System.Drawing.Point(140, 190);
            this.emailTextBox.Width = 180;

            // Phone
            this.phoneLabel.Text = "Phone:";
            this.phoneLabel.Location = new System.Drawing.Point(40, 230);
            this.phoneTextBox.Location = new System.Drawing.Point(140, 230);
            this.phoneTextBox.Width = 180;

            // Register button
            this.registerButton.Text = "Register";
            this.registerButton.Location = new System.Drawing.Point(90, 280);
            this.registerButton.Width = 100;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);

            // Back button
            this.backButton.Text = "Back";
            this.backButton.Location = new System.Drawing.Point(200, 280);
            this.backButton.Width = 100;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);

            // Form setup
            this.ClientSize = new System.Drawing.Size(380, 340);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
