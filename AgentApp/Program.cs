using System;
using System.Windows.Forms;
using AgentApp.Forms;
using RealEstateApp.Core;
using System.Data.SQLite;

namespace AgentApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                SQLitePCL.Batteries_V2.Init();

               
                DatabaseInitializer.Initialize();            

                ApplicationConfiguration.Initialize();
                Application.Run(new StartUpForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal error:\n" + ex.Message, "Startup Crash");
            }
        }
    }
}
