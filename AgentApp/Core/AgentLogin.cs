using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AgentApp.Core
{
     public class AgentAccount
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
    }
    public class AgentLogin
    {
      private static readonly string DataPath = @"C:\Ana\RealEstateApp\AgentApp\Core\Data\Agents.json";

     public static List<AgentAccount> LoadAgents()
    {
      
        if (!File.Exists(DataPath))
            return new List<AgentAccount>();
        
        string jsonContent = File.ReadAllText(DataPath);

        var agents = JsonSerializer.Deserialize<List<AgentAccount>>(jsonContent);

            if (agents == null)
                return new List<AgentAccount>();
            
        return agents;
    }


        public static AgentAccount? Validate(string username, string password)

        {
            List<AgentAccount> agents = LoadAgents();

            foreach (AgentAccount agent in agents)
                if (agent.Username == username && agent.Password == password)
                    return agent;
      
            return null;
        }

    }

   
}
