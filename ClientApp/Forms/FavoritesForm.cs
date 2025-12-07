using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ClientApp.Core;

namespace ClientApp.Forms
{
    public partial class FavoritesForm : Form
    {
        private Client client;
        private Panel favoritesPanel;
        private Button backButton;

        public FavoritesForm(Client client)
        {
            InitializeComponent();
            this.client = client;
            LoadFavorites();
        }

        private void InitializeComponent()
        {
            this.favoritesPanel = new Panel();
            this.backButton = new Button();

            // favoritesPanel
            this.favoritesPanel.Name = "favoritesPanel";
            this.favoritesPanel.Dock = DockStyle.Fill;
            this.favoritesPanel.AutoScroll = true;

            // backButton
            this.backButton.Text = "Back";
            this.backButton.Size = new Size(80, 30);
            this.backButton.Location = new Point(10, 10);
            this.backButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.backButton.Click += new EventHandler(this.backButton_Click);

            // Form
            this.ClientSize = new Size(500, 450);
            this.Text = "Favorite Properties";
            this.StartPosition = FormStartPosition.CenterParent;

            // Add controls
            this.Controls.Add(this.favoritesPanel);
            this.Controls.Add(this.backButton);
        }

        private void LoadFavorites()
        {
            // Basic layout: a Panel named `favoritesPanel` should exist in Designer
            // If not, create it dynamically
            Panel panel = null;
            try
            {
                panel = (Panel)this.Controls["favoritesPanel"];
            }
            catch { }

            if (panel == null)
            {
                panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
                this.Controls.Add(panel);
            }

            panel.Controls.Clear();

            var all = PropertyData.GetAllProperties();
            var favIds = client.FavoritePropertyIds ?? new System.Collections.Generic.List<string>();
            var favProperties = all.Where(p => favIds.Contains(p.Id.ToString())).ToList();

            int y = 20;
            foreach (var prop in favProperties)
            {
                Panel card = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(10, y),
                    Size = new Size(460, 120)
                };

                Label nameLabel = new Label
                {
                    Text = prop.Name,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                Label addressLabel = new Label
                {
                    Text = "📍 " + prop.Address,
                    Location = new Point(10, 40),
                    AutoSize = true
                };

                Label priceLabel = new Label
                {
                    Text = $"💰 {prop.Price:C0}",
                    Location = new Point(10, 65),
                    AutoSize = true
                };

                Button removeButton = new Button
                {
                    Text = "🗑️ Remove",
                    Location = new Point(360, 40),
                    Size = new Size(80, 30)
                };
                removeButton.Click += (s, e) => RemoveFavorite(prop);

                card.Controls.Add(nameLabel);
                card.Controls.Add(addressLabel);
                card.Controls.Add(priceLabel);
                card.Controls.Add(removeButton);

                panel.Controls.Add(card);
                y += 140;
            }

            if (favProperties.Count == 0)
            {
                Label none = new Label
                {
                    Text = "No favorite properties yet.",
                    Location = new Point(10, 20),
                    AutoSize = true
                };
                panel.Controls.Add(none);
            }
        }

        private void RemoveFavorite(Property prop)
        {
            client.RemoveFavorite(prop.Id.ToString());
            var manager = new ClientProfileManager();
            var clients = manager.LoadClients();
            var idx = clients.FindIndex(c => c.Id == client.Id);
            if (idx >= 0)
            {
                clients[idx] = client;
                manager.SaveClients(clients);
            }

            LoadFavorites();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
