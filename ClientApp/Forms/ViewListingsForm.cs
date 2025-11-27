using System;
using System.Drawing;
using System.Windows.Forms;
using ClientApp.Core;

namespace ClientApp.Forms
{
    public partial class ViewListingsForm : Form
    {
        public ViewListingsForm()
        {
            InitializeComponent(); // Designer setup
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

                card.Controls.Add(nameLabel);
                card.Controls.Add(addressLabel);
                card.Controls.Add(priceLabel);
                card.Controls.Add(descLabel);
                card.Controls.Add(favButton);

                listingsPanel.Controls.Add(card);
                y += 140;
            }
        }

        private void AddToFavorites(Property property)
        {
            MessageBox.Show($"{property.Name} added to favorites!");
            // TODO: Add to favorites in database or client account
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
