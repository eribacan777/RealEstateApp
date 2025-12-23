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
        private string agentId;
        private Button btnAdd;
        private Button btnSave;

        public ManageListingsForm(string agentId)
        {
            this.agentId = agentId;

            this.Text = "Manage Listings";
            this.ClientSize = new Size(800, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            dgvListings = new DataGridView()
            {
                Dock = DockStyle.Top,
                Height = 360,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                ReadOnly = false,
                BackgroundColor = Color.LavenderBlush
            };

            dgvListings.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Id",
                DataPropertyName = "Id",
                Name = "Id",
                Visible = false
            });

            dgvListings.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Title",
                DataPropertyName = "Title",
                Name = "Title",
                Width = 150
            });

            dgvListings.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Description",
                DataPropertyName = "Description",
                Name = "Description",
                Width = 200
            });

            dgvListings.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Price",
                DataPropertyName = "Price",
                Name = "Price",
                Width = 100,
                ValueType = typeof(string),
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    ForeColor = Color.DarkSlateBlue
                }
            });

            dgvListings.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Location",
                DataPropertyName = "Location",
                Name = "Location",
                Width = 120
            });

            dgvListings.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Type",
                DataPropertyName = "PropertyType",
                Name = "PropertyType",
                Width = 100
            });

            dgvListings.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 80,
                Name = "DeleteAction"
            });

            Controls.Add(dgvListings);

            Panel bottomPanel = new Panel()
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.WhiteSmoke
            };

            btnAdd = new Button()
            {
                Text = "+",
                Dock = DockStyle.Left,
                Width = this.ClientSize.Width / 2,
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;

            btnSave = new Button()
            {
                Text = "Save Changes",
                Dock = DockStyle.Right,
                Width = this.ClientSize.Width / 2,
                BackColor = Color.DeepPink,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            bottomPanel.Controls.Add(btnAdd);
            bottomPanel.Controls.Add(btnSave);
            Controls.Add(bottomPanel);

            dgvListings.CellClick += DgvListings_CellClick;

            LoadListings();
        }

  private void LoadListings()
{
    using var conn = DatabaseHelper.GetConnection("Listings.db");
    conn.Open();

    var cmd = new SQLiteCommand(@"
        SELECT Id, Title, Description, Price, Location, PropertyType
        FROM Listings
        WHERE AgentId = @id
        ORDER BY Id ASC;", conn);
    cmd.Parameters.AddWithValue("@id", agentId);

    var adapter = new SQLiteDataAdapter(cmd);
    var originalTable = new DataTable();
    adapter.Fill(originalTable);

    // ✅ Clone schema and change Price column to string
    var table = originalTable.Clone();
    table.Columns["Price"].DataType = typeof(string);

    // ✅ Copy rows and format Price
    foreach (DataRow row in originalTable.Rows)
    {
        var newRow = table.NewRow();
        newRow.ItemArray = row.ItemArray;

        if (decimal.TryParse(row["Price"].ToString(), out decimal price))
        {
            newRow["Price"] = price.ToString("C2", new CultureInfo("fr-FR"));
        }

        table.Rows.Add(newRow);
    }

    dgvListings.DataSource = table;
}

        private void DgvListings_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvListings.Columns[e.ColumnIndex].Name == "DeleteAction")
            {
                var row = ((DataTable)dgvListings.DataSource).Rows[e.RowIndex];
                int listingId = Convert.ToInt32(row["Id"]);

                if (MessageBox.Show("Delete this listing?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DeleteListing(listingId);
                    LoadListings();
                }
            }
        }

        private void DeleteListing(int id)
        {
            using var conn = DatabaseHelper.GetConnection("Listings.db");
            conn.Open();

            var cmd = new SQLiteCommand("DELETE FROM Listings WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var createForm = new CreateListingForm(agentId);
            createForm.ShowDialog();
            LoadListings();
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            using var conn = DatabaseHelper.GetConnection("Listings.db");
            conn.Open();

            foreach (DataGridViewRow row in dgvListings.Rows)
            {
                if (row.IsNewRow) continue;

                string priceText = row.Cells["Price"].Value?.ToString() ?? "0";

                priceText = priceText
                    .Replace("€", "")
                    .Replace(" ", "") // non-breaking space
                    .Replace(" ", "")
                    .Replace(".", "")
                    .Replace(",", ".");

                if (!decimal.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
                    price = 0;

                var cmd = new SQLiteCommand(@"
                    UPDATE Listings SET
                        Title = @t,
                        Description = @d,
                        Price = @p,
                        Location = @l,
                        PropertyType = @pt
                    WHERE Id = @id;", conn);

                cmd.Parameters.AddWithValue("@t", row.Cells["Title"].Value);
                cmd.Parameters.AddWithValue("@d", row.Cells["Description"].Value);
                cmd.Parameters.AddWithValue("@p", price);
                cmd.Parameters.AddWithValue("@l", row.Cells["Location"].Value);
                cmd.Parameters.AddWithValue("@pt", row.Cells["PropertyType"].Value);
                cmd.Parameters.AddWithValue("@id", row.Cells["Id"].Value);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Changes saved successfully!");
        }
    }
}
