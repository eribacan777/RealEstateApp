using System;
using System.IO;
using System.Text.Json;

namespace AgentApp.Core
{
    public class Agent
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Save all agents to Agents.json
        /// </summary>
        public static void SaveAll(Agent[] agents)
        {
            string filePath = Path.Combine("Core", "Data", "Agents.json");
            string folder = Path.GetDirectoryName(filePath) ?? string.Empty;

            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var json = JsonSerializer.Serialize(agents, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Load all agents from Agents.json
        /// </summary>
        public static Agent[] LoadAll()
        {
            string filePath = Path.Combine("Core", "Data", "Agents.json");
            if (!File.Exists(filePath))
                return Array.Empty<Agent>();

            try
            {
                var json = File.ReadAllText(filePath);
                var agents = JsonSerializer.Deserialize<Agent[]>(json);
                return agents ?? Array.Empty<Agent>();
            }
            catch
            {
                return Array.Empty<Agent>();
            }
        }
    }
}
