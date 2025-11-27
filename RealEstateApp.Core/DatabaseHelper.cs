using System;
using System.Data.SQLite;
using System.IO;

namespace RealEstateApp.Core
{
    public static class DatabaseHelper
    {
        public static SQLiteConnection GetConnection(string dbName)
        {
            // Base directory of the running executable
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Navigate back to repo root from bin/Debug/netX.X/
            string repoRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\.."));

            // All databases now live in RealEstateApp/Database
            string dbPath = Path.Combine(repoRoot, "Database", dbName);

            if (!File.Exists(dbPath))
                throw new FileNotFoundException($"Database file not found at: {dbPath}");

            Console.WriteLine($"[DatabaseHelper] Using DB: {dbPath}");

            return new SQLiteConnection($"Data Source={dbPath};Version=3;");
        }
    }
}
