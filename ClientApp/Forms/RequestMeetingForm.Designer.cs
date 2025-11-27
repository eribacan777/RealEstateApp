namespace ClientApp
{
    partial class RequestMeetingForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.DateTimePicker meetingDatePicker;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button cancelButton;

        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.meetingDatePicker = new System.Windows.Forms.DateTimePicker();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // nameLabel
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.nameLabel.Location = new System.Drawing.Point(30, 20);

            // dateLabel
            this.dateLabel.Text = "Choose date & time:";
            this.dateLabel.Location = new System.Drawing.Point(30, 60);
            this.dateLabel.AutoSize = true;

            // meetingDatePicker
            this.meetingDatePicker.Location = new System.Drawing.Point(30, 80);
            this.meetingDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.meetingDatePicker.CustomFormat = "dd MMM yyyy - HH:mm";
            this.meetingDatePicker.Width = 250;

            // messageLabel
            this.messageLabel.Text = "Message to agent:";
            this.messageLabel.Location = new System.Drawing.Point(30, 120);
            this.messageLabel.AutoSize = true;

            // messageTextBox
            this.messageTextBox.Location = new System.Drawing.Point(30, 140);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Size = new System.Drawing.Size(300, 80);

            // submitButton
            this.submitButton.Text = "✅ Submit";
            this.submitButton.Location = new System.Drawing.Point(70, 240);
            this.submitButton.Size = new System.Drawing.Size(100, 35);
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);

            // cancelButton
            this.cancelButton.Text = "❌ Cancel";
            this.cancelButton.Location = new System.Drawing.Point(190, 240);
            this.cancelButton.Size = new System.Drawing.Size(100, 35);
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);

            // Form setup
            this.ClientSize = new System.Drawing.Size(370, 310);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.meetingDatePicker);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.cancelButton);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Request a Meeting";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
