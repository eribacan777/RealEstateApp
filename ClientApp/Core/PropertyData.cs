using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ClientApp.Core
{
    public static class PropertyData
    {
        public static List<Property> GetAllProperties()
{
    var list = new List<Property>();
    using var conn = RealEstateApp.Core.DatabaseHelper.GetConnection("Listings.db");
    conn.Open();

    var cmd = new SQLiteCommand("SELECT * FROM Listings;", conn);
    using var reader = cmd.ExecuteReader();

    while (reader.Read())
    {
        list.Add(new Property
        {
            Id = Convert.ToInt32(reader["Id"]),
            Name = reader["Title"].ToString()!,
            Address = reader["Description"].ToString()!, // or another field
            Price = (double)Convert.ToDecimal(reader["Price"]),
            Description = reader["Description"].ToString()!
        });
    }

    return list;
}

        public static void SeedProperties()
{
    using var conn = RealEstateApp.Core.DatabaseHelper.GetConnection("Listings.db");
    conn.Open();

    var cmd = new SQLiteCommand(@"
        INSERT INTO Listings (Title, Description, Price, AgentId)
        VALUES 
        ('Cozy Apartment', '2 bed, 1 bath, central location', 85000, 1),
        ('Luxury Villa', '5 bed, 4 bath, pool and garden', 450000, 1);
    ", conn);

    cmd.ExecuteNonQuery();
}

    }
}
