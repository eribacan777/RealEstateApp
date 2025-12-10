using System;
using System.Windows.Forms;
using ClientApp.Core;
using ClientApp.Forms;

namespace ClientApp 
{
    public partial class ClientDashboardForm : Form
    {
        private Client client;

        public ClientDashboardForm(Client client)
        {
            InitializeComponent();
            this.client = client;
            welcomeLabel.Text = $"Welcome, {client.FullName}!";
        }

        private void viewListingsButton_Click(object sender, EventArgs e)
        {
            var listingsForm = new ViewListingsForm(this.client);
            listingsForm.ShowDialog();

        }

        private void favoritesButton_Click(object sender, EventArgs e)
        {
            var favForm = new FavoritesForm(this.client);
            favForm.ShowDialog();
        }

      private void requestMeetingButton_Click(object sender, EventArgs e)
        {
            var meetingForm = new RequestMeetingForm(client);
            meetingForm.ShowDialog();
        }


        private void logoutButton_Click(object sender, EventArgs e)
        {
            var loginForm = new ClientLoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void viewAccountButton_Click(object sender, EventArgs e)
{
    var accountForm = new AccountDetailsForm(this.client);
    accountForm.ShowDialog();
}

    }
}
