using System;
using System.Drawing;
using System.Windows.Forms;
using ClientApp.Core;

namespace ClientApp.Forms
{
    public partial class ViewListingsForm : Form
    {
        private Client? currentClient;

        public ViewListingsForm(Client? client = null)
        {
            InitializeComponent(); // Designer setup
            this.currentClient = client;
            LoadProperties();
        }

        private void LoadProperties()
        {
            // Clear previous listings
            listingsPanel.Controls.Clear();
            int y = 20;

            // Fetch all properties from database
            var propertyList = PropertyData.GetAllProperties();

            foreach (var property in propertyList)
            {
                // Create a card panel for each property
                Panel card = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(10, y),
                    Size = new Size(460, 120)
                };

                Label nameLabel = new Label
                {
                    Text = property.Name,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                Label addressLabel = new Label
                {
                    Text = "📍 " + property.Address,
                    Location = new Point(10, 40),
                    AutoSize = true
                };

                Label priceLabel = new Label
                {
                    Text = $"💰 {property.Price:C0}",
                    Location = new Point(10, 65),
                    AutoSize = true
                };

                Label descLabel = new Label
                {
                    Text = property.Description,
                    Location = new Point(10, 90),
                    AutoSize = true
                };

                Button favButton = new Button
                {
                    Text = "❤️ Favorite",
                    Location = new Point(360, 40),
                    Size = new Size(80, 30)
                };
                favButton.Click += (s, e) => AddToFavorites(property);

                Button reqButton = new Button
                {
                    Text = "📅 Request",
                    Location = new Point(360, 75),
                    Size = new Size(80, 30)
                };
                reqButton.Click += (s, e) => OpenRequest(property);

                card.Controls.Add(nameLabel);
                card.Controls.Add(addressLabel);
                card.Controls.Add(priceLabel);
                card.Controls.Add(descLabel);
                card.Controls.Add(favButton);
                card.Controls.Add(reqButton);

                listingsPanel.Controls.Add(card);
                y += 140;
            }
        }

        private void AddToFavorites(Property property)
        {
            if (currentClient == null)
            {
                MessageBox.Show("You must be logged in to add favorites.");
                return;
            }

            string propId = property.Id.ToString();
            currentClient.AddFavorite(propId);

            // Persist change
            var manager = new ClientProfileManager();
            var clients = manager.LoadClients();
            var idx = clients.FindIndex(c => c.Id == currentClient.Id);
            if (idx >= 0)
            {
                clients[idx] = currentClient;
                manager.SaveClients(clients);
            }

            MessageBox.Show($"{property.Name} added to favorites!");
        }

        private void OpenRequest(Property property)
        {
            if (currentClient == null)
            {
                MessageBox.Show("You must be logged in to request a meeting.");
                return;
            }

            // Ensure AgentUsername is resolved — try to load from DB if missing
            if (string.IsNullOrEmpty(property.AgentUsername) && property.AgentId != 0)
            {
                try
                {
                    using var conn = RealEstateApp.Core.DatabaseHelper.GetConnection("AgentAccounts.db");
                    conn.Open();
                    using var cmd = new System.Data.SQLite.SQLiteCommand("SELECT Username FROM Agents WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", property.AgentId);
                    var res = cmd.ExecuteScalar();
                    property.AgentUsername = res?.ToString() ?? string.Empty;
                }
                catch { /* ignore */ }
            }

            var reqForm = new ClientApp.Forms.RequestMeetingForm(currentClient, property.Id.ToString(), property.AgentUsername);
            reqForm.ShowDialog();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
