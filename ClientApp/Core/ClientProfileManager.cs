using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ClientApp.Core
{
    public class ClientProfileManager
    {
        private readonly string filePath = @"Core\Data\Profiles\Clients.json";

        public List<Client> LoadClients()
        {
            if (!File.Exists(filePath)) return new List<Client>();
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Client>>(json);
        }

        public void SaveClients(List<Client> clients)
        {
            string json = JsonSerializer.Serialize(clients, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void AddClient(Client client)
        {
            var clients = LoadClients();
            clients.Add(client);
            SaveClients(clients);
        }

        public Client GetClientByEmail(string email)
        {
            return LoadClients().Find(c => c.Email == email);
        }
    }
}
