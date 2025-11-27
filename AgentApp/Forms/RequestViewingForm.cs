using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using AgentApp.Core; // reuse Viewing + PropertyListing classes

namespace ClientApp.Forms
{
    public class RequestViewingForm : Form
    {
        private DateTimePicker dateTimePicker;
        private Button btnSubmit;
        private string clientUsername;
        private string propertyId;
        private string viewingsPath = Path.Combine("Core", "Data", "Viewings");

        public RequestViewingForm(string username, string propertyId)
        {
            clientUsername = username;
            this.propertyId = propertyId;

            this.Text = "Request Viewing";
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblDate = new Label()
            {
                Text = "Select Date & Time:",
                Location = new System.Drawing.Point(20, 30),
                AutoSize = true
            };

            dateTimePicker = new DateTimePicker()
            {
                Location = new System.Drawing.Point(150, 25),
                Width = 200,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm"
            };

            btnSubmit = new Button()
            {
                Text = "Submit Request",
                Location = new System.Drawing.Point(150, 80),
                Size = new System.Drawing.Size(200, 40)
            };
            btnSubmit.Click += BtnSubmit_Click;

            Controls.Add(lblDate);
            Controls.Add(dateTimePicker);
            Controls.Add(btnSubmit);
        }

        private void BtnSubmit_Click(object? sender, EventArgs e)
        {
            // Ensure directory exists
            if (!Directory.Exists(viewingsPath))
                Directory.CreateDirectory(viewingsPath);

            // Validate propertyId
            if (string.IsNullOrWhiteSpace(propertyId))
            {
                MessageBox.Show("Invalid property selection.");
                return;
            }

            // Validate date/time
            if (dateTimePicker.Value < DateTime.Now)
            {
                MessageBox.Show("You cannot request a viewing in the past.");
                return;
            }

            // Assign agent based on property listing
            string agentAssigned = "Unassigned";
            try
            {
                var listingsFile = Path.Combine("Core", "Data", "Listings.json");
                if (File.Exists(listingsFile))
                {
                    var listings = DataHandler.Load<PropertyListing>(listingsFile);
                    var property = Array.Find(listings, l => l.PropertyId == propertyId);
                    if (property != null && !string.IsNullOrWhiteSpace(property.AgentUsername))
                        agentAssigned = property.AgentUsername;
                }
            }
            catch
            {
                // fallback to Unassigned if error occurs
            }

            // Create viewing object
            var viewing = new Viewing
            {
                ViewingId = Guid.NewGuid().ToString(),
                PropertyId = propertyId,
                AgentUsername = agentAssigned,
                ClientUsername = clientUsername,
                DateTime = dateTimePicker.Value,
                Status = "Scheduled",
                Feedback = string.Empty
            };

            string filePath = Path.Combine(viewingsPath, viewing.ViewingId + ".json");
            string json = JsonSerializer.Serialize(viewing, new JsonSerializerOptions { WriteIndented = true });

            try
            {
                File.WriteAllText(filePath, json);
                MessageBox.Show($"Viewing requested for {propertyId} on {dateTimePicker.Value:g}");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving request: " + ex.Message);
            }
        }
    }
}
