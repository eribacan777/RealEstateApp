using System;
using System.IO;
using System.Text.Json;

namespace AgentApp.Core
{
    public static class DataHandler
    {
        // Generic load method with error handling
        public static T[] Load<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return Array.Empty<T>();

                var json = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<T[]>(json);

                return data ?? Array.Empty<T>();
            }
            catch
            {
                return Array.Empty<T>();
            }
        }

        // Generic save method with safe directory creation
        public static void Save<T>(string filePath, T[] items)
        {
            var folder = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        // Append a new item
        public static void Add<T>(string filePath, T newItem)
        {
            var items = Load<T>(filePath);
            var updated = new System.Collections.Generic.List<T>(items) { newItem };
            Save(filePath, updated.ToArray());
        }

        // Remove item by predicate
        public static void Remove<T>(string filePath, Predicate<T> match)
        {
            var items = Load<T>(filePath);
            var updated = Array.FindAll(items, i => !match(i));
            Save(filePath, updated);
        }

        // Update item by predicate
        public static void Update<T>(string filePath, Predicate<T> match, Action<T> updateAction)
        {
            var items = Load<T>(filePath);
            foreach (var item in items)
            {
                if (match(item))
                {
                    updateAction(item);
                }
            }
            Save(filePath, items);
        }
    }
}
