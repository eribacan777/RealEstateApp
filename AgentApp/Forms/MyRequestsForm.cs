using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using AgentApp.Core; // reuse Viewing class

namespace ClientApp.Forms
{
    public class MyRequestsForm : Form
    {
        private ListView listView;
        private string clientUsername;
        private string viewingsPath = Path.Combine("Core", "Data", "Viewings");

        public MyRequestsForm(string username)
        {
            clientUsername = username;

            this.Text = "My Viewing Requests - " + username;
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            listView = new ListView()
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(650, 300)
            };

            listView.Columns.Add("ViewingId", 120);
            listView.Columns.Add("PropertyId", 120);
            listView.Columns.Add("Date/Time", 150);
            listView.Columns.Add("Status", 100);
            listView.Columns.Add("Agent", 120);

            Controls.Add(listView);

            LoadRequests();
        }

        private void LoadRequests()
        {
            listView.Items.Clear();

            if (!Directory.Exists(viewingsPath))
                Directory.CreateDirectory(viewingsPath);

            foreach (var file in Directory.GetFiles(viewingsPath, "*.json"))
            {
                var json = File.ReadAllText(file);
                var viewing = JsonSerializer.Deserialize<Viewing>(json);

                if (viewing != null && viewing.ClientUsername == clientUsername)
                {
                    var item = new ListViewItem(viewing.ViewingId);
                    item.SubItems.Add(viewing.PropertyId);
                    item.SubItems.Add(viewing.DateTime.ToString("g"));
                    item.SubItems.Add(viewing.Status);
                    item.SubItems.Add(viewing.AgentUsername);
                    listView.Items.Add(item);
                }
            }
        }
    }
}
