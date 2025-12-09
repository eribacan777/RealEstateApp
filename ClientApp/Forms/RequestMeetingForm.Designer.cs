namespace ClientApp.Forms
{
    partial class RequestMeetingForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label agentLabel;
        private System.Windows.Forms.ComboBox agentComboBox;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.DateTimePicker meetingDatePicker;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.agentLabel = new System.Windows.Forms.Label();
            this.agentComboBox = new System.Windows.Forms.ComboBox();
            this.dateLabel = new System.Windows.Forms.Label();
            this.meetingDatePicker = new System.Windows.Forms.DateTimePicker();
            this.messageLabel = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // nameLabel
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.nameLabel.Location = new System.Drawing.Point(20, 15);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(135, 19);
            this.nameLabel.Text = "Client: Placeholder";

            // agentLabel
            this.agentLabel.AutoSize = true;
            this.agentLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.agentLabel.Location = new System.Drawing.Point(20, 55);
            this.agentLabel.Name = "agentLabel";
            this.agentLabel.Size = new System.Drawing.Size(49, 19);
            this.agentLabel.Text = "Agent:";

            // agentComboBox
            this.agentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.agentComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.agentComboBox.Location = new System.Drawing.Point(90, 52);
            this.agentComboBox.Name = "agentComboBox";
            this.agentComboBox.Size = new System.Drawing.Size(200, 25);

            // dateLabel
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dateLabel.Location = new System.Drawing.Point(20, 100);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(113, 19);
            this.dateLabel.Text = "Choose a date:";

            // meetingDatePicker
            this.meetingDatePicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.meetingDatePicker.Location = new System.Drawing.Point(150, 95);
            this.meetingDatePicker.Name = "meetingDatePicker";
            this.meetingDatePicker.Size = new System.Drawing.Size(200, 25);

            // messageLabel
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.messageLabel.Location = new System.Drawing.Point(20, 145);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(70, 19);
            this.messageLabel.Text = "Message:";

            // messageTextBox
            this.messageTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.messageTextBox.Location = new System.Drawing.Point(20, 170);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(330, 120);

            // submitButton
            this.submitButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.submitButton.Location = new System.Drawing.Point(190, 310);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 30);
            this.submitButton.Text = "Submit";
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);

            // cancelButton
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cancelButton.Location = new System.Drawing.Point(275, 310);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 30);
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);

            // RequestMeetingForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 360);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.agentLabel);
            this.Controls.Add(this.agentComboBox);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.meetingDatePicker);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RequestMeetingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Request Meeting";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

