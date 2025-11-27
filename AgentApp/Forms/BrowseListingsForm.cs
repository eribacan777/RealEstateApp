using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using AgentApp.Core;

namespace ClientApp.Forms
{
    public class BrowseListingsForm : Form
    {
        private ListView listView;
        private Button btnRequestViewing;
        private string clientUsername;
        private string listingsFile = Path.Combine("Core", "Data", "Listings.json");

        public BrowseListingsForm(string username)
        {
            clientUsername = username;

            this.Text = "Browse Listings - " + username;
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            listView = new ListView()
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(650, 250)
            };

            listView.Columns.Add("PropertyId", 120);
            listView.Columns.Add("Type", 120);
            listView.Columns.Add("Location", 150);
            listView.Columns.Add("Price", 100);
            listView.Columns.Add("Availability", 100);

            btnRequestViewing = new Button()
            {
                Text = "Request Viewing",
                Location = new System.Drawing.Point(20, 300),
                Size = new System.Drawing.Size(200, 40)
            };
            btnRequestViewing.Click += BtnRequestViewing_Click;

            Controls.Add(listView);
            Controls.Add(btnRequestViewing);

            LoadListings();
        }

        private void LoadListings()
        {
            listView.Items.Clear();

            if (!File.Exists(listingsFile)) return;

            var json = File.ReadAllText(listingsFile);
            var listings = JsonSerializer.Deserialize<PropertyListing[]>(json);

            if (listings == null) return;

            foreach (var listing in listings)
            {
                var item = new ListViewItem(listing.PropertyId);
                item.SubItems.Add(listing.Type);
                item.SubItems.Add(listing.Location);
                item.SubItems.Add(listing.Price.ToString("C"));
                item.SubItems.Add(listing.Availability);
                listView.Items.Add(item);
            }
        }

        private void BtnRequestViewing_Click(object? sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a property first.");
                return;
            }

            string propertyId = listView.SelectedItems[0].Text; // actual PropertyId
            var requestForm = new RequestViewingForm(clientUsername, propertyId);
            requestForm.ShowDialog();
        }
    }
}
