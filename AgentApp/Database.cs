using System.Data.SQLite;
using System.IO;

namespace AgentApp.Core
{
    public static class DatabaseHelper
    {
        public static SQLiteConnection GetConnection(string dbFile)
        {
            string path = Path.Combine("Database", dbFile);
            return new SQLiteConnection($"Data Source={path};Version=3;");
        }
    }
}
