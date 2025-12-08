using System;
using System.Data.SQLite;
using System.IO;
using ClientApp.Core;

namespace RealEstateApp.Core
{
    public static class MeetingRequestRepository
    {
        private static string GetDbPath()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string repoRoot = Path.GetFullPath(Path.Combine(baseDir, "..\\..\\..\\.."));
            string dbFolder = Path.Combine(repoRoot, "Database");

            if (!Directory.Exists(dbFolder))
                Directory.CreateDirectory(dbFolder);

            return Path.Combine(dbFolder, "MeetingRequests.db");
        }

        private static void EnsureTableExists()
        {
            string dbPath = GetDbPath();
            using (var conn = new SQLiteConnection($"Data Source={dbPath}"))
            {
                conn.Open();

                string sql = @"
                CREATE TABLE IF NOT EXISTS MeetingRequests (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ClientId INTEGER NOT NULL,
                    ClientName TEXT NOT NULL,
                    AgentUsername TEXT NOT NULL,
                    PropertyId TEXT,
                    RequestedDate TEXT NOT NULL,
                    Message TEXT
                )";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Save(MeetingRequest request)
        {
            EnsureTableExists();

            string dbPath = GetDbPath();

            using (var conn = new SQLiteConnection($"Data Source={dbPath}"))
            {
                conn.Open();

                string sql = @"
                INSERT INTO MeetingRequests 
                (ClientId, ClientName, AgentUsername, PropertyId, RequestedDate, Message)
                VALUES (@ClientId, @ClientName, @AgentUsername, @PropertyId, @RequestedDate, @Message)";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientId", request.ClientId);
                    cmd.Parameters.AddWithValue("@ClientName", request.ClientName);
                    cmd.Parameters.AddWithValue("@AgentUsername", request.AgentUsername);
                    cmd.Parameters.AddWithValue("@PropertyId", request.PropertyId);
                    cmd.Parameters.AddWithValue("@RequestedDate", request.RequestedDate.ToString("yyyy-MM-dd HH:mm"));
                    cmd.Parameters.AddWithValue("@Message", request.Message);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
