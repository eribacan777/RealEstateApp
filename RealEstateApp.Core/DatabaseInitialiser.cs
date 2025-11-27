using System;
using System.Data.SQLite;

namespace RealEstateApp.Core
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            InitializeAgents();
            InitializeClients();
            InitializeListings();
        }

        private static void InitializeAgents()
        {
            using var conn = DatabaseHelper.GetConnection("AgentAccounts.db");
            conn.Open();

            var createCmd = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Agents (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    FullName TEXT,
                    Email TEXT,
                    PhoneNumber TEXT
                );", conn);
            createCmd.ExecuteNonQuery();

            var verifyCmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Agents';", conn);
            var result = verifyCmd.ExecuteScalar();
            if (result == null || result.ToString() != "Agents")
            {
                throw new InvalidOperationException("❌ Agents table was not created.");
            }
        }

        private static void InitializeClients()
        {
            using var conn = DatabaseHelper.GetConnection("ClientAccounts.db");
            conn.Open();

            var createCmd = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Clients (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    FullName TEXT,
                    Email TEXT,
                    PhoneNumber TEXT
                );", conn);
            createCmd.ExecuteNonQuery();

            var verifyCmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Clients';", conn);
            var result = verifyCmd.ExecuteScalar();
            if (result == null || result.ToString() != "Clients")
            {
                throw new InvalidOperationException("❌ Clients table was not created.");
            }
        }

        private static void InitializeListings()
        {
            using var conn = DatabaseHelper.GetConnection("Listings.db");
            conn.Open();

            var createCmd = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Listings (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT,
                    Price REAL,
                    AgentId INTEGER,
                    FOREIGN KEY (AgentId) REFERENCES Agents(Id)
                );", conn);
            createCmd.ExecuteNonQuery();

            var verifyCmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Listings';", conn);
            var result = verifyCmd.ExecuteScalar();
            if (result == null || result.ToString() != "Listings")
            {
                throw new InvalidOperationException("❌ Listings table was not created.");
            }
        }
    }
}
