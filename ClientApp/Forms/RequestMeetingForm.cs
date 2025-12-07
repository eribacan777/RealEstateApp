using System;
using System.Windows.Forms;
using ClientApp.Core;

namespace ClientApp.Forms
{
    public partial class RequestMeetingForm : Form
    {
        private Client client;
        private string propertyId;
        private string agentUsername;

        public RequestMeetingForm(Client client, string propertyId = "", string agentUsername = "")
        {
            InitializeComponent();
            this.client = client;
            this.propertyId = propertyId;
            this.agentUsername = agentUsername ?? string.Empty;
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

            try
            {
                // Build a viewing object compatible with AgentApp.Core.Viewing
                var viewing = new
                {
                    ViewingId = Guid.NewGuid().ToString(),
                    PropertyId = propertyId,
                    AgentUsername = agentUsername ?? string.Empty,
                    ClientUsername = client.Username ?? string.Empty,
                    DateTime = selectedDate,
                    Status = "Requested",
                    Feedback = message
                };

                // Compute AgentApp viewings folder in the repo (same logic as DatabaseHelper uses to find repo root)
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string repoRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, "..\\..\\..\\.."));
                string viewingsFolder = System.IO.Path.Combine(repoRoot, "AgentApp", "Core", "Data", "Viewings");
                if (!System.IO.Directory.Exists(viewingsFolder))
                    System.IO.Directory.CreateDirectory(viewingsFolder);

                string filePath = System.IO.Path.Combine(viewingsFolder, viewing.ViewingId + ".json");
                var json = System.Text.Json.JsonSerializer.Serialize(viewing, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(filePath, json);

                MessageBox.Show($"Meeting requested for {selectedDate:G}.\nMessage: {message}", "Request Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving request: " + ex.Message);
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
