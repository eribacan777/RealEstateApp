using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ClientApp.Core
{
    public static class ClientLogin
    {
        // Add a new client to the database
        public static bool AddClient(Client client)
        {
            using var conn = RealEstateApp.Core.DatabaseHelper.GetConnection("ClientAccounts.db");
            conn.Open();

var cmd = new SQLiteCommand(@"
    INSERT INTO Clients (Username, FullName, Email, Password, PhoneNumber)
    VALUES (@username, @fullname, @email, @password, @phone);", conn);

cmd.Parameters.AddWithValue("@username", client.Username); // ← add this
cmd.Parameters.AddWithValue("@fullname", client.FullName);
cmd.Parameters.AddWithValue("@email", client.Email);
cmd.Parameters.AddWithValue("@password", client.Password);
cmd.Parameters.AddWithValue("@phone", client.PhoneNumber ?? "");


            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"❌ Could not add client: {ex.Message}");
                return false;
            }
        }

        // Validate login credentials
        public static Client? Validate(string email, string password)
        {
            using var conn = RealEstateApp.Core.DatabaseHelper.GetConnection("ClientAccounts.db");
            conn.Open();

            var cmd = new SQLiteCommand(@"
                SELECT Id, FullName, Email, Password, PhoneNumber
                FROM Clients
                WHERE Email=@Email AND Password=@Password;", conn);

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Client
                {
                    Id = reader["Id"].ToString(),
                    FullName = reader["FullName"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Password = reader["Password"].ToString()!,
                    PhoneNumber = reader["PhoneNumber"].ToString()
                };
            }

            return null;
        }
    }
}
