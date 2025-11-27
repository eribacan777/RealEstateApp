using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using AgentApp.Core;

namespace AgentApp.Forms
{
    public class ViewingsForm : Form
    {
        private ListView listView;
        private Button btnConfirm;
        private Button btnReject;
        private Button btnMarkMissed;
        private Button btnReassign;
        private ComboBox cmbAgents;
        private string agentUsername;
        private string viewingsPath = Path.Combine("Core", "Data", "Viewings");

        public ViewingsForm(string username)
        {
            agentUsername = username;

            this.Text = "Manage Viewings - " + username;
            this.ClientSize = new System.Drawing.Size(700, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            listView = new ListView()
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(650, 250)
            };

            listView.Columns.Add("ViewingId", 100);
            listView.Columns.Add("PropertyId", 100);
            listView.Columns.Add("Client", 150);
            listView.Columns.Add("Date/Time", 150);
            listView.Columns.Add("Status", 100);

            btnConfirm = new Button() { Text = "Confirm", Location = new System.Drawing.Point(20, 300), Size = new System.Drawing.Size(150, 40) };
            btnReject = new Button() { Text = "Reject", Location = new System.Drawing.Point(200, 300), Size = new System.Drawing.Size(150, 40) };
            btnMarkMissed = new Button() { Text = "Mark Missed", Location = new System.Drawing.Point(380, 300), Size = new System.Drawing.Size(150, 40) };

            btnConfirm.Click += (s, e) => UpdateStatus("Confirmed");
            btnReject.Click += (s, e) => UpdateStatus("Rejected");
            btnMarkMissed.Click += (s, e) => UpdateStatus("Missed");

            // --- New Controls for Reassignment ---
            cmbAgents = new ComboBox()
            {
                Location = new System.Drawing.Point(20, 360),
                Width = 200
            };

            btnReassign = new Button()
            {
                Text = "Reassign",
                Location = new System.Drawing.Point(240, 360),
                Size = new System.Drawing.Size(150, 40)
            };
            btnReassign.Click += (s, e) => ReassignViewing();

            Controls.Add(listView);
            Controls.Add(btnConfirm);
            Controls.Add(btnReject);
            Controls.Add(btnMarkMissed);
            Controls.Add(cmbAgents);
            Controls.Add(btnReassign);

            LoadAgents();
            LoadViewings();
        }

        private void LoadViewings()
        {
            listView.Items.Clear();

            if (!Directory.Exists(viewingsPath))
                Directory.CreateDirectory(viewingsPath);

            foreach (var file in Directory.GetFiles(viewingsPath, "*.json"))
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
                        item.SubItems.Add(viewing.DateTime.ToString("g"));
                        item.SubItems.Add(viewing.Status);
                        item.Tag = file; // store file path for updates
                        listView.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading viewing: " + ex.Message);
                }
            }
        }

        private void UpdateStatus(string newStatus)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a viewing first.");
                return;
            }

            var selected = listView.SelectedItems[0];
            if (selected.Tag is not string filePath)
            {
                MessageBox.Show("No file path associated with this viewing.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Viewing file not found.");
                return;
            }

            try
            {
                var json = File.ReadAllText(filePath);
                var viewing = JsonSerializer.Deserialize<Viewing>(json);

                if (viewing != null)
                {
                    viewing.Status = newStatus;
                    json = JsonSerializer.Serialize(viewing, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(filePath, json);

                    MessageBox.Show($"Viewing marked as {newStatus}.");
                    LoadViewings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating status: " + ex.Message);
            }
        }

        private void LoadAgents()
        {
            string agentsFile = Path.Combine("Core", "Data", "Agents.json");
            if (!File.Exists(agentsFile)) return;

            try
            {
                var agents = DataHandler.Load<Agent>(agentsFile);
                cmbAgents.Items.Clear();
                foreach (var agent in agents)
                {
                    if (!string.IsNullOrEmpty(agent.Username))
                        cmbAgents.Items.Add(agent.Username);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading agents: " + ex.Message);
            }
        }

        private void ReassignViewing()
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a viewing first.");
                return;
            }
            if (cmbAgents.SelectedItem == null)
            {
                MessageBox.Show("Select an agent to reassign.");
                return;
            }

            var selected = listView.SelectedItems[0];
            if (selected.Tag is not string filePath)
            {
                MessageBox.Show("No file path associated with this viewing.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Viewing file not found.");
                return;
            }

            try
            {
                var json = File.ReadAllText(filePath);
                var viewing = JsonSerializer.Deserialize<Viewing>(json);

                if (viewing != null)
                {
                    viewing.AgentUsername = cmbAgents.SelectedItem?.ToString() ?? string.Empty;
                    json = JsonSerializer.Serialize(viewing, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(filePath, json);

                    MessageBox.Show($"Viewing reassigned to {viewing.AgentUsername}!");
                    LoadViewings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reassigning viewing: " + ex.Message);
            }
        }
    }
}
