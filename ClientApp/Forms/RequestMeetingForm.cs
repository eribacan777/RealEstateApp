using System;
using System.Data.SQLite;
using System.Windows.Forms;
using RealEstateApp.Core;
using ClientApp.Core;

namespace ClientApp.Forms
{
    public partial class RequestMeetingForm : Form
    {
        private Client client;
        private string propertyId;

        public RequestMeetingForm(Client client, string propertyId = "", string agentUsername = "")
        {
            InitializeComponent();
            this.client = client;
            this.propertyId = propertyId;

            nameLabel.Text = $"Client: {client.FullName}";

            LoadAgents();

            // If developer passed an agent username, pre-select it
            if (!string.IsNullOrEmpty(agentUsername))
            {
                agentComboBox.SelectedItem = agentUsername;
            }
        }

        private void LoadAgents()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string repoRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, "..\\..\\..\\.."));
                string dbPath = System.IO.Path.Combine(repoRoot, "Database", "AgentAccounts.db");

                using (var conn = new SQLiteConnection($"Data Source={dbPath}"))
                {
                    conn.Open();

                    string query = "SELECT Username FROM Agents";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            agentComboBox.Items.Add(reader.GetString(0));
                        }
                    }
                }

                if (agentComboBox.Items.Count > 0)
                    agentComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading agents: " + ex.Message);
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (agentComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select an agent.");
                return;
            }

            var date = meetingDatePicker.Value;
            var message = messageTextBox.Text.Trim();
            var agent = agentComboBox.SelectedItem.ToString();

            var meeting = new MeetingRequest
            {
                ClientId = client.Id,
                ClientName = client.FullName,
                PropertyId = propertyId,
                AgentUsername = agent,
                RequestedDate = date,
                Message = message
            };

            try
            {
                MeetingRequestRepository.Save(meeting);
                MessageBox.Show("Meeting request sent successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving meeting request: " + ex.Message);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
