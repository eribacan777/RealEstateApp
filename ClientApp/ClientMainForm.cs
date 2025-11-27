using System;
using System.Windows.Forms;

namespace ClientApp.Forms
{
    public class ClientMainForm : Form
    {
        public ClientMainForm()
        {
            this.Text = "Client - Main Page";
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.Beige;
        }
    }
}
