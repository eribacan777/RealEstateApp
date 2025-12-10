using System;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AgentApp.Forms
{
    public class ManageRequestsForm : Form
    {
        private ListView listView;
        private Button btnAccept;
        private Button btnDecline;
        private Button btnClose;
        private string agentUsername;

        public ManageRequestsForm(string username)
        {
            agentUsername = username;

            this.Text = "Manage Meeting Requests - " + username;
            this.ClientSize = new Size(700, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            listView = new ListView()
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new Point(20, 20),
                Size = new Size(650, 250),
                BackColor = Color.White
            };

            listView.Columns.Add("Id", 60);
            listView.Columns.Add("PropertyId", 100);
            listView.Columns.Add("ClientName", 150);
            listView.Columns.Add("RequestedDate", 150);
            listView.Columns.Add("Status", 100);
            listView.Columns.Add("Message", 200);

            btnAccept = new Button()
            {
                Text = "✅ Accept",
                Location = new Point(20, 300),
                Size = new Size(100, 40),
                BackColor = Color.LightGreen
            };
            btnAccept.Click += BtnAccept_Click;

            btnDecline = new Button()
            {
                Text = "❌ Decline",
                Location = new Point(140, 300),
                Size = new Size(100, 40),
                BackColor = Color.LightCoral
            };
            btnDecline.Click += BtnDecline_Click;

            btnClose = new Button()
            {
                Text = "Close",
                Location = new Point(260, 300),
                Size = new Size(100, 40),
                BackColor = Color.LightGray
            };
            btnClose.Click += (s, e) => this.Close();

            Controls.Add(listView);
            Controls.Add(btnAccept);
            Controls.Add(btnDecline);
            Controls.Add(btnClose);

            // ✅ Ensure Status column exists before loading
            EnsureStatusColumnExists();
            LoadRequests();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rect = this.ClientRectangle;
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                rect, Color.MediumPurple, Color.DeepPink, 45F))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            base.OnPaint(e);
        }

        // ✅ Migration helper: adds Status column if missing
        private void EnsureStatusColumnExists()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string repoRoot = Path.GetFullPath(Path.Combine(baseDir, "..\\..\\..\\.."));
            string dbPath = Path.Combine(repoRoot, "Database", "MeetingRequests.db");

            using var conn = new SQLiteConnection($"Data Source={dbPath}");
            conn.Open();

            using var checkCmd = new SQLiteCommand("PRAGMA table_info(MeetingRequests);", conn);
            using var reader = checkCmd.ExecuteReader();
            bool hasStatus = false;
            while (reader.Read())
            {
                if (reader["name"].ToString() == "Status")
                {
                    hasStatus = true;
                    break;
                }
            }

            if (!hasStatus)
            {
                using var alterCmd = new SQLiteCommand(
                    "ALTER TABLE MeetingRequests ADD COLUMN Status TEXT DEFAULT 'Requested';", conn);
                alterCmd.ExecuteNonQuery();
            }
        }

        private void LoadRequests()
        {
            listView.Items.Clear();

            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string repoRoot = Path.GetFullPath(Path.Combine(baseDir, "..\\..\\..\\.."));
                string dbPath = Path.Combine(repoRoot, "Database", "MeetingRequests.db");

                using var conn = new SQLiteConnection($"Data Source={dbPath}");
                conn.Open();

                string query = @"
                    SELECT Id, PropertyId, ClientName, RequestedDate, Status, Message
                    FROM MeetingRequests
                    WHERE AgentUsername = @agent";

                using var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@agent", agentUsername);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ListViewItem(reader["Id"].ToString());
                    item.SubItems.Add(reader["PropertyId"].ToString());
                    item.SubItems.Add(reader["ClientName"].ToString());

                    if (DateTime.TryParse(reader["RequestedDate"].ToString(), out DateTime dt))
                        item.SubItems.Add(dt.ToString("dd MMM yyyy - HH:mm"));
                    else
                        item.SubItems.Add("Invalid Date");

                    item.SubItems.Add(reader["Status"].ToString());
                    item.SubItems.Add(reader["Message"].ToString());

                    listView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading requests:\n" + ex.Message);
            }
        }

        private void BtnAccept_Click(object? sender, EventArgs e)
        {
            UpdateStatus("Accepted");
        }

        private void BtnDecline_Click(object? sender, EventArgs e)
        {
            UpdateStatus("Declined");
        }

        private void UpdateStatus(string newStatus)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a request first.");
                return;
            }

            string requestId = listView.SelectedItems[0].Text;

            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string repoRoot = Path.GetFullPath(Path.Combine(baseDir, "..\\..\\..\\.."));
                string dbPath = Path.Combine(repoRoot, "Database", "MeetingRequests.db");

                using var conn = new SQLiteConnection($"Data Source={dbPath}");
                conn.Open();

                string updateQuery = "UPDATE MeetingRequests SET Status = @status WHERE Id = @id";
                using var cmd = new SQLiteCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@status", newStatus);
                cmd.Parameters.AddWithValue("@id", requestId);
                cmd.ExecuteNonQuery();

                MessageBox.Show($"Request {newStatus}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating request:\n" + ex.Message);
            }
        }
    }
}
