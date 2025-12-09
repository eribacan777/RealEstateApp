using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Text.Json;

namespace AgentApp.Forms
{
    public class ManageRequestsForm : Form
    {
        private ListView listView;
        private Button btnAccept;
        private Button btnDecline;
        private Button btnClose;
        private string agentUsername;
        private string viewingsFolder;

        public ManageRequestsForm(string username)
        {
            agentUsername = username;

            this.Text = "Manage Meeting Requests - " + username;
            this.ClientSize = new Size(700, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string repoRoot = Path.GetFullPath(Path.Combine(baseDir, "..\\..\\..\\.."));
            viewingsFolder = Path.Combine(repoRoot, "AgentApp", "Core", "Data", "Viewings");

            listView = new ListView()
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new Point(20, 20),
                Size = new Size(650, 250),
                BackColor = Color.White // keep list readable
            };

            listView.Columns.Add("ViewingId", 120);
            listView.Columns.Add("PropertyId", 120);
            listView.Columns.Add("Client", 150);
            listView.Columns.Add("DateTime", 150);
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

            LoadRequests();
        }

        // 🎨 Gradient background
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

        private void LoadRequests()
        {
            listView.Items.Clear();

            if (!Directory.Exists(viewingsFolder)) return;

            foreach (var file in Directory.GetFiles(viewingsFolder, "*.json"))
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var viewing = JsonSerializer.Deserialize<Viewing>(json);

                    if (viewing != null && viewing.AgentUsername == agentUsername)
                    {
                        var item = new ListViewItem(viewing.ViewingId);
                        item.SubItems.Add(viewing.PropertyId);
                        item.SubItems.Add(viewing.ClientUsername);
                        item.SubItems.Add(viewing.DateTime.ToString("dd MMM yyyy - HH:mm"));
                        item.SubItems.Add(viewing.Status);
                        item.SubItems.Add(viewing.Feedback);
                        listView.Items.Add(item);
                    }
                }
                catch
                {
                    // skip malformed files
                }
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

            string viewingId = listView.SelectedItems[0].Text;
            string filePath = Path.Combine(viewingsFolder, viewingId + ".json");

            if (!File.Exists(filePath)) return;

            try
            {
                var json = File.ReadAllText(filePath);
                var viewing = JsonSerializer.Deserialize<Viewing>(json);

                if (viewing != null)
                {
                    viewing.Status = newStatus;
                    var updatedJson = JsonSerializer.Serialize(viewing, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(filePath, updatedJson);

                    MessageBox.Show($"Request {newStatus}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRequests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating request:\n" + ex.Message);
            }
        }

        private class Viewing
        {
            public string ViewingId { get; set; } = "";
            public string PropertyId { get; set; } = "";
            public string AgentUsername { get; set; } = "";
            public string ClientUsername { get; set; } = "";
            public DateTime DateTime { get; set; }
            public string Status { get; set; } = "";
            public string Feedback { get; set; } = "";
        }
    }
}
