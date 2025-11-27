using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using RealEstateApp.Core;

namespace AgentApp.Forms
{
    public class ViewListingsForm : Form
    {
        private ListView listView;
        private Button btnRefresh;
        private string agentId;

        public ViewListingsForm(string agentId)
        {
            this.agentId = agentId;

            this.Text = "View Listings - Agent " + agentId;
            this.ClientSize = new Size(800, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            listView = new ListView()
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new Point(20, 20),
                Size = new Size(750, 300)
            };

            listView.Columns.Add("Id", 60);
            listView.Columns.Add("Title", 120);
            listView.Columns.Add("Description", 180);
            listView.Columns.Add("Price", 80);
            listView.Columns.Add("Location", 120);
            listView.Columns.Add("Type", 100);

            btnRefresh = new Button()
            {
                Text = "Refresh",
                Location = new Point(20, 340),
                Size = new Size(100, 30),
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => LoadListings();

            Controls.Add(listView);
            Controls.Add(btnRefresh);

            LoadListings();
        }

        private void LoadListings()
        {
            listView.Items.Clear();

            try
            {
                using var conn = DatabaseHelper.GetConnection("Listings.db");
                conn.Open();

                var cmd = new SQLiteCommand(@"
                    SELECT Id, Title, Description, Price, Location, PropertyType
                    FROM Listings
                    WHERE AgentId = @id;", conn);
                cmd.Parameters.AddWithValue("@id", agentId);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ListViewItem(reader["Id"].ToString());
                    item.SubItems.Add(reader["Title"].ToString());
                    item.SubItems.Add(reader["Description"].ToString());
                    item.SubItems.Add(reader["Price"].ToString());
                    item.SubItems.Add(reader["Location"].ToString());
                    item.SubItems.Add(reader["PropertyType"].ToString());
                    listView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading listings:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
