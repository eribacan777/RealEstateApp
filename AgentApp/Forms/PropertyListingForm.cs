using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using AgentApp.Core;
using AgentApp.Core.Shared;


namespace AgentApp.Forms
{
    public class PropertyListingForm : Form
    {
        private TextBox txtTitle;
        private ComboBox cmbSize;
        private TextBox txtPrice;
        private Button btnSave;

        public PropertyListingForm(string agentUsername)
        {
       
            this.Text = "Add Property Listing";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(147, 255, 137);

            var lblTitle = new Label
            {
                Text = "Title",
                Location = new Point(50, 30),
                AutoSize = true
            };

            txtTitle = new TextBox
            {
                Location = new Point(150, 30),
                Width = 180
            };

            var lblSize = new Label
            {
                Text = "Size",
                Location = new Point(50, 70),
                AutoSize = true
            };

            cmbSize = new ComboBox
            {
                Location = new Point(150, 70),
                Width = 180
            };
            cmbSize.Items.AddRange(Enum.GetNames(typeof(PropertySize)));
            cmbSize.SelectedIndex = 0;

            var lblPrice = new Label
            {
                Text = "Price",
                Location = new Point(50, 110),
                AutoSize = true
            };

            txtPrice = new TextBox
            {
                Location = new Point(150, 110),
                Width = 180
            };

            btnSave = new Button
            {
                Text = "Save",
                Size = new Size(120, 40),
                Location = new Point(150, 160)
            };



            btnSave.Click += (sender, e) =>
            {
                
                var listing = new PropertyListing
                {
                    AgentUsername = agentUsername
                };

                
                if (!string.IsNullOrWhiteSpace(txtTitle.Text))
                    listing.Title = txtTitle.Text.Trim();
                else
                    listing.Title = "Untitled";

               
                string selectedSizeText = cmbSize?.SelectedItem?.ToString() ?? "Small";
                if (Enum.TryParse<PropertySize>(selectedSizeText, out var parsedSize))
                    listing.Size = parsedSize;
                else
                    listing.Size = PropertySize.Small;

                string priceText = txtPrice.Text.Replace(" ", "").Replace(",", ".").Trim();
                if (decimal.TryParse(priceText, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var parsedPrice))
                    listing.Price = parsedPrice;
                else
                    listing.Price = 0;

                PropertyListing.Save(listing);
                this.Close();
            };


           


            Controls.AddRange(new Control[]
            {
                lblTitle, txtTitle,
                lblSize, cmbSize,
                lblPrice, txtPrice,
                btnSave
            });
        }
    }
}
