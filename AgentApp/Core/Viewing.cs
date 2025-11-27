using System;
using System.IO;
using System.Text.Json;

namespace AgentApp.Core
{
    public class Viewing
    {
        public string ViewingId { get; set; } = Guid.NewGuid().ToString();
        public string PropertyId { get; set; } = string.Empty;
        public string AgentUsername { get; set; } = string.Empty;
        public string ClientUsername { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Scheduled";
        public string Feedback { get; set; } = string.Empty;

        /// <summary>
        /// Save a single viewing to its own JSON file.
        /// </summary>
        public static void Save(Viewing viewing)
        {
            string folder = Path.Combine("Core", "Data", "Viewings");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, viewing.ViewingId + ".json");
            string json = JsonSerializer.Serialize(viewing, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Load all viewings from the Viewings folder.
        /// </summary>
        public static Viewing[] LoadAll()
        {
            string folder = Path.Combine("Core", "Data", "Viewings");
            if (!Directory.Exists(folder))
                return Array.Empty<Viewing>();

            var files = Directory.GetFiles(folder, "*.json");
            var viewings = new System.Collections.Generic.List<Viewing>();

            foreach (var file in files)
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var viewing = JsonSerializer.Deserialize<Viewing>(json);
                    if (viewing != null)
                        viewings.Add(viewing);
                }
                catch
                {
                    // skip corrupted files
                }
            }

            return viewings.ToArray();
        }
    }
}
