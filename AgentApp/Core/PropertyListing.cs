using System;
using System.IO;
using System.Text.Json;
using AgentApp.Core.Shared;

namespace AgentApp.Core
{
    public class PropertyListing
    {
        public string Title { get; set; } = string.Empty;
        public PropertySize Size { get; set; } = new PropertySize(); // ensure struct/class is initialized
        public decimal Price { get; set; } = 0;

        public string Type { get; set; } = string.Empty;
        public string PropertyId { get; set; } = Guid.NewGuid().ToString();
        public string Location { get; set; } = string.Empty;
        public string Availability { get; set; } = "Available";
        public string AgentUsername { get; set; } = string.Empty;

        /// <summary>
        /// Save a single listing to its own JSON file.
        /// </summary>
        public static void Save(PropertyListing listing)
        {
            string folder = Path.Combine("Core", "Data", "Listings");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, listing.PropertyId + ".json");
            string json = JsonSerializer.Serialize(listing, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Load all listings from the Listings folder.
        /// </summary>
        public static PropertyListing[] LoadAll()
        {
            string folder = Path.Combine("Core", "Data", "Listings");
            if (!Directory.Exists(folder))
                return Array.Empty<PropertyListing>();

            var files = Directory.GetFiles(folder, "*.json");
            var listings = new System.Collections.Generic.List<PropertyListing>();

            foreach (var file in files)
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var listing = JsonSerializer.Deserialize<PropertyListing>(json);
                    if (listing != null)
                        listings.Add(listing);
                }
                catch
                {
                    // skip corrupted files
                }
            }

            return listings.ToArray();
        }
    }
}
