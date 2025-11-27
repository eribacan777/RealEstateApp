using System;
using System.Windows.Forms;
using ClientApp.Core;

namespace ClientApp 
{
    public partial class RequestMeetingForm : Form
    {
        private Client client;

        public RequestMeetingForm(Client client)
        {
            InitializeComponent();
            this.client = client;
            nameLabel.Text = $"Client: {client.FullName}";
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = meetingDatePicker.Value;
            string message = messageTextBox.Text.Trim();

            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Please enter a short message or reason for the meeting.");
                return;
            }

            // Later: save to database or send to API
            MessageBox.Show($"Meeting requested for {selectedDate:G}.\nMessage: {message}", 
                "Request Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
