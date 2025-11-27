using System.Data.SQLite;
using ClientApp.Core; 

namespace ClientApp.Core
{
    public static class Database
    {
        private static string connectionString = @"Data Source=..\..\..\Database\RealEstateDatabase.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
