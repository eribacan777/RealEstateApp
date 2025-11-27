using System;
using System.Windows.Forms;
using ClientApp.Forms; // Assuming ClientLoginForm is here
using RealEstateApp.Core; // Necessary for DatabaseInitializer and DatabaseHelper
using System.Data.SQLite; // Necessary for SQLite commands

namespace ClientApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                // 1. Initialize SQLite PCL
                SQLitePCL.Batteries_V2.Init();

                // 2. Run Database Initializer from the shared Core project
                DatabaseInitializer.Initialize();
                MessageBox.Show("✅ DatabaseInitializer ran successfully (ClientApp)", "Startup Check");

                // 3. Confirm Clients table exists (Specific to ClientApp)
                using var conn = DatabaseHelper.GetConnection("ClientAccounts.db");
                conn.Open();
                var cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Clients';", conn);
                var result = cmd.ExecuteScalar();

                if (result == null || result.ToString() != "Clients")
                {
                    MessageBox.Show("❌ Clients table NOT found in ClientAccounts.db", "DB Check");
                }
                else
                {
                    MessageBox.Show("✅ Clients table exists in ClientAccounts.db", "DB Check");
                }

                // 4. Launch UI
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ClientLoginForm()); // start with client login
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Fatal error in ClientApp:\n" + ex.Message, "Startup Crash");
            }
        }
    }
}
