using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using RealEstateApp.Core;

namespace AgentApp.Forms
{
    public class ManageListingsForm : Form
    {
        private DataGridView dgvListings;
        private Button btnSaveChanges;
        private string agentId;

        public ManageListingsForm(string agentId)
        {
            this.agentId = agentId;

            this.Text = "Manage Listings - Agent " + agentId;
            this.ClientSize = new Size(720, 420);
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvListings = new DataGridView()
            {
                Dock = DockStyle.Top,
                Height = 330,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                BackgroundColor = Color.LavenderBlush,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvListings.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 50,
                ReadOnly = true
            });
            dgvListings.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Title",
                HeaderText = "Title",
                DataPropertyName = "Title",
                Width = 200,
                ReadOnly = true
            });
            dgvListings.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Location",
                HeaderText = "Location",
                DataPropertyName = "Location",
                Width = 150
            });
            dgvListings.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Price",
                HeaderText = "Price (€)",
                DataPropertyName = "Price",
                Width = 100
            });
            dgvListings.Columns.Add(new DataGridViewComboBoxColumn()
            {
                Name = "Type",
                HeaderText = "Type",
                DataPropertyName = "PropertyType",
                Width = 100,
                DataSource = new string[] { "Apartment", "House", "Duplex" }
            });

            btnSaveChanges = new Button()
            {
                Text = "Save Changes",
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.DeepPink,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnSaveChanges.FlatAppearance.BorderSize = 0;
            btnSaveChanges.Click += BtnSaveChanges_Click;

            Controls.Add(dgvListings);
            Controls.Add(btnSaveChanges);

            LoadListings();
        }

        private void LoadListings()
        {
            try
            {
                using var conn = DatabaseHelper.GetConnection("Listings.db");
                conn.Open();

                var cmd = new SQLiteCommand(@"
                    SELECT Id, Title, Location, Price, PropertyType
                    FROM Listings
                    WHERE AgentId = @id
                    ORDER BY Id ASC;", conn);

                cmd.Parameters.AddWithValue("@id", agentId);

                var adapter = new SQLiteDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);

                // Add a display-only column for formatted price
                table.Columns.Add("FormattedPrice", typeof(string));
                foreach (DataRow row in table.Rows)
                {
                    if (decimal.TryParse(row["Price"].ToString(), out decimal price))
                    {
                        row["FormattedPrice"] = price.ToString("N3", CultureInfo.InvariantCulture) + " €";
                    }
                }

                dgvListings.DataSource = table;

                // Hide raw Price column
                dgvListings.Columns["Price"].Visible = false;

                // Update grid to show FormattedPrice instead
                dgvListings.Columns["Price"].HeaderText = "Price (€)";
                dgvListings.Columns["Price"].DataPropertyName = "FormattedPrice";
                dgvListings.Columns["Price"].Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading listings:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveChanges_Click(object? sender, EventArgs e)
        {
            try
            {
                using var conn = DatabaseHelper.GetConnection("Listings.db");
                conn.Open();

                foreach (DataGridViewRow row in dgvListings.Rows)
                {
                    if (row.IsNewRow) continue;

                    int id = Convert.ToInt32(row.Cells["Id"].Value);
                    string location = row.Cells["Location"].Value?.ToString() ?? "";
                    string type = row.Cells["Type"].Value?.ToString() ?? "";
                    string priceText = row.Cells["Price"].Value?.ToString()?.Replace("€", "").Trim() ?? "0";

                    if (!decimal.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
                    {
                        MessageBox.Show($"Invalid price in row {row.Index + 1}. Skipping update.");
                        continue;
                    }

                    var updateCmd = new SQLiteCommand(@"
                        UPDATE Listings
                        SET Location = @l, Price = @p, PropertyType = @t
                        WHERE Id = @id;", conn);
                    updateCmd.Parameters.AddWithValue("@l", location);
                    updateCmd.Parameters.AddWithValue("@p", price);
                    updateCmd.Parameters.AddWithValue("@t", type);
                    updateCmd.Parameters.AddWithValue("@id", id);

                    updateCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
