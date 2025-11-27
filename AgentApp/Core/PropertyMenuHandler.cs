using System;
using System.IO;
using System.Text.Json;

namespace AgentApp.Core
{
    public class Property
    {
        public string PropertyId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;       // Apartment, House, Land
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public string Availability { get; set; } = string.Empty; // Available, Sold, Rented
        public string Description { get; set; } = string.Empty;
    }

    public static class PropertyMenuHandler
    {
        private static string listingsPath = Path.Combine("Core", "Data", "Listings");

        public static void PropertyMenu(string agentUsername)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Property Management ---");
                Console.WriteLine("1. Add Property");
                Console.WriteLine("2. Edit Property");
                Console.WriteLine("3. Delete Property");
                Console.WriteLine("4. View Properties");
                Console.WriteLine("5. Back");

                Console.Write("Choose an option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProperty(agentUsername);
                        break;
                    case "2":
                        EditProperty();
                        break;
                    case "3":
                        DeleteProperty();
                        break;
                    case "4":
                        ViewProperties();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private static void AddProperty(string agentUsername)
        {
            Property property = new Property
            {
                PropertyId = Guid.NewGuid().ToString(),
                Type = Prompt("Enter property type (Apartment/House/Land): "),
                Location = Prompt("Enter location: "),
                Price = decimal.TryParse(Prompt("Enter price: "), out var price) ? price : 0,
                Rooms = int.TryParse(Prompt("Enter number of rooms: "), out var rooms) ? rooms : 0,
                Availability = "Available",
                Description = Prompt("Enter description: ")
            };

            Directory.CreateDirectory(listingsPath);
            string filePath = Path.Combine(listingsPath, property.PropertyId + ".json");
            string json = JsonSerializer.Serialize(property, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);

            Console.WriteLine($"Property {property.PropertyId} added successfully!");
        }

        private static void EditProperty()
        {
            Console.Write("Enter PropertyId to edit: ");
            string? id = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Invalid PropertyId.");
                return;
            }

            string filePath = Path.Combine(listingsPath, id + ".json");
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Property not found.");
                return;
            }

            string json = File.ReadAllText(filePath);
            Property? property = JsonSerializer.Deserialize<Property>(json);
            if (property is null)
            {
                Console.WriteLine("Error loading property.");
                return;
            }

            property.Price = decimal.TryParse(Prompt($"Enter new price (current {property.Price}): "), out var price) ? price : property.Price;
            property.Availability = Prompt($"Enter availability (current {property.Availability}): ");
            property.Description = Prompt($"Enter new description (current {property.Description}): ");

            json = JsonSerializer.Serialize(property, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);

            Console.WriteLine("Property updated successfully!");
        }

        private static void DeleteProperty()
        {
            Console.Write("Enter PropertyId to delete: ");
            string? id = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Invalid PropertyId.");
                return;
            }

            string filePath = Path.Combine(listingsPath, id + ".json");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("Property deleted successfully!");
            }
            else
            {
                Console.WriteLine("Property not found.");
            }
        }

        private static void ViewProperties()
        {
            Console.WriteLine("\n--- Properties ---");
            if (!Directory.Exists(listingsPath))
            {
                Console.WriteLine("No properties found.");
                return;
            }

            foreach (var file in Directory.GetFiles(listingsPath, "*.json"))
            {
                string json = File.ReadAllText(file);
                Property? property = JsonSerializer.Deserialize<Property>(json);
                if (property != null)
                {
                    Console.WriteLine($"ID: {property.PropertyId}, Type: {property.Type}, Location: {property.Location}, Price: {property.Price}, Rooms: {property.Rooms}, Availability: {property.Availability}");
                }
            }
        }

        private static string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
