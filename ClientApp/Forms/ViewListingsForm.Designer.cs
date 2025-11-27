using System.Windows.Forms;
using System.Drawing;

namespace ClientApp.Forms
{
    partial class ViewListingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel listingsPanel;
        private Button backButton;
        private Label titleLabel;

        private void InitializeComponent()
        {
            this.listingsPanel = new Panel();
            this.backButton = new Button();
            this.titleLabel = new Label();

            this.SuspendLayout();

            // titleLabel
            this.titleLabel.Text = "🏡 Available Properties";
            this.titleLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.titleLabel.Location = new Point(20, 10);
            this.titleLabel.AutoSize = true;

            // listingsPanel
            this.listingsPanel.AutoScroll = true;
            this.listingsPanel.Location = new Point(10, 50);
            this.listingsPanel.Size = new Size(480, 380);

            // backButton
            this.backButton.Text = "⬅ Back";
            this.backButton.Location = new Point(390, 440);
            this.backButton.Size = new Size(80, 30);
            this.backButton.Click += new EventHandler(this.backButton_Click);

            // Form setup
            this.ClientSize = new Size(500, 480);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.listingsPanel);
            this.Controls.Add(this.backButton);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "View Listings";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
