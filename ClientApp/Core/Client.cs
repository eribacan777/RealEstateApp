using System;
using System.Collections.Generic;

namespace ClientApp.Core
{
    [Serializable]
    public class Client
    {
        // Unique identifier
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Required info
        public string Username { get; set; } = "";

        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        // Optional info
        public string? PhoneNumber { get; set; }
        public string? PreferredLocation { get; set; }

        // Favorite properties (optional)
        public List<string> FavoritePropertyIds { get; set; } = new List<string>();

        // Parameterless constructor (needed for serialization)
        public Client() { }

        // Constructor with required fields
        public Client(string fullName, string email, string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
        }

        // Favorites management
        public void AddFavorite(string propertyId)
        {
            if (!FavoritePropertyIds.Contains(propertyId))
                FavoritePropertyIds.Add(propertyId);
        }

        public void RemoveFavorite(string propertyId)
        {
            FavoritePropertyIds.Remove(propertyId);
        }

        public bool IsFavorite(string propertyId)
        {
            return FavoritePropertyIds.Contains(propertyId);
        }
    }
}
