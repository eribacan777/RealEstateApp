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

    // --- Form Background ---
    this.BackColor = Color.FromArgb(255, 240, 245); // soft baby pink

    // titleLabel
    this.titleLabel.Text = "🏡 Available Properties";
    this.titleLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
    this.titleLabel.ForeColor = Color.FromArgb(180, 60, 120); // rose-plum
    this.titleLabel.Location = new Point(20, 10);
    this.titleLabel.AutoSize = true;

    // listingsPanel
    this.listingsPanel.AutoScroll = true;
    this.listingsPanel.Location = new Point(10, 50);
    this.listingsPanel.Size = new Size(480, 380);
    this.listingsPanel.BackColor = Color.FromArgb(255, 228, 236); // light pastel box
    this.listingsPanel.BorderStyle = BorderStyle.FixedSingle;

    // backButton
    this.backButton.Text = "⬅ Back";
    this.backButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
    this.backButton.Location = new Point(390, 440);
    this.backButton.Size = new Size(80, 32);
    this.backButton.BackColor = Color.FromArgb(255, 182, 193); // soft pink
    this.backButton.ForeColor = Color.White;
    this.backButton.FlatStyle = FlatStyle.Flat;
    this.backButton.FlatAppearance.BorderSize = 0;
    this.backButton.Cursor = Cursors.Hand;
    this.backButton.Click += new EventHandler(this.backButton_Click);

    // Form setup
    this.ClientSize = new Size(500, 480);
    this.Controls.Add(this.titleLabel);
    this.Controls.Add(this.listingsPanel);
    this.Controls.Add(this.backButton);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "View Listings";
    this.FormBorderStyle = FormBorderStyle.FixedDialog;

    this.ResumeLayout(false);
    this.PerformLayout();
}

    }
}
