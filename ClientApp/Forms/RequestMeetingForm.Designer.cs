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

    // --- Form Styling ---
    this.BackColor = Color.FromArgb(255, 240, 245); // light pink blush
    this.Font = new Font("Segoe UI", 10F);

    // nameLabel
    this.nameLabel.AutoSize = true;
    this.nameLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
    this.nameLabel.ForeColor = Color.FromArgb(180, 60, 120);
    this.nameLabel.Location = new Point(20, 15);
    this.nameLabel.Text = "Client: Placeholder";

    // agentLabel
    this.agentLabel.AutoSize = true;
    this.agentLabel.Location = new Point(20, 55);
    this.agentLabel.ForeColor = Color.FromArgb(150, 70, 110);
    this.agentLabel.Text = "Agent:";

    // agentComboBox
    this.agentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.agentComboBox.Font = new Font("Segoe UI", 10F);
    this.agentComboBox.Location = new Point(90, 52);
    this.agentComboBox.Size = new Size(200, 25);
    this.agentComboBox.BackColor = Color.FromArgb(255, 230, 240);

    // dateLabel
    this.dateLabel.AutoSize = true;
    this.dateLabel.Location = new Point(20, 100);
    this.dateLabel.ForeColor = Color.FromArgb(150, 70, 110);
    this.dateLabel.Text = "Choose a date:";

    // meetingDatePicker
    this.meetingDatePicker.Font = new Font("Segoe UI", 10F);
    this.meetingDatePicker.Location = new Point(150, 95);
    this.meetingDatePicker.Size = new Size(200, 25);
    this.meetingDatePicker.CalendarMonthBackground = Color.FromArgb(255, 230, 240);

    // messageLabel
    this.messageLabel.AutoSize = true;
    this.messageLabel.Location = new Point(20, 145);
    this.messageLabel.ForeColor = Color.FromArgb(150, 70, 110);
    this.messageLabel.Text = "Message:";

    // messageTextBox
    this.messageTextBox.Font = new Font("Segoe UI", 10F);
    this.messageTextBox.Location = new Point(20, 170);
    this.messageTextBox.Multiline = true;
    this.messageTextBox.Size = new Size(330, 120);
    this.messageTextBox.BackColor = Color.FromArgb(255, 225, 235);
    this.messageTextBox.BorderStyle = BorderStyle.FixedSingle;

    // submitButton
    this.submitButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
    this.submitButton.Location = new Point(190, 310);
    this.submitButton.Size = new Size(75, 30);
    this.submitButton.Text = "Submit";
    this.submitButton.BackColor = Color.FromArgb(255, 182, 193); // baby pink
    this.submitButton.FlatStyle = FlatStyle.Flat;
    this.submitButton.FlatAppearance.BorderSize = 0;
    this.submitButton.ForeColor = Color.White;
    this.submitButton.Cursor = Cursors.Hand;
    this.submitButton.Click += new EventHandler(this.submitButton_Click);

    // cancelButton
    this.cancelButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
    this.cancelButton.Location = new Point(275, 310);
    this.cancelButton.Size = new Size(75, 30);
    this.cancelButton.Text = "Cancel";
    this.cancelButton.BackColor = Color.FromArgb(255, 192, 203);
    this.cancelButton.FlatStyle = FlatStyle.Flat;
    this.cancelButton.FlatAppearance.BorderSize = 0;
    this.cancelButton.ForeColor = Color.White;
    this.cancelButton.Cursor = Cursors.Hand;
    this.cancelButton.Click += new EventHandler(this.cancelButton_Click);

    // RequestMeetingForm
    this.AutoScaleDimensions = new SizeF(7F, 15F);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(380, 360);
    this.Controls.Add(this.nameLabel);
    this.Controls.Add(this.agentLabel);
    this.Controls.Add(this.agentComboBox);
    this.Controls.Add(this.dateLabel);
    this.Controls.Add(this.meetingDatePicker);
    this.Controls.Add(this.messageLabel);
    this.Controls.Add(this.messageTextBox);
    this.Controls.Add(this.submitButton);
    this.Controls.Add(this.cancelButton);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Request Meeting";
    this.ResumeLayout(false);
    this.PerformLayout();
}

    }
}

