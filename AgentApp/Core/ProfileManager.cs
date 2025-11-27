using System.IO;
using System.Text.Json;

namespace AgentApp.Core
{
    public static class ProfileManager
    {
        public static bool ProfileExists(string username)
        {
            string path = Path.Combine("Core", "Data", "Profiles", $"{username}.json");
            return File.Exists(path);
        }

        public static void SaveProfile(string username, string fullName, string phone)
        {
            string folder = Path.Combine("Core", "Data", "Profiles");
            Directory.CreateDirectory(folder);

            var profile = new { Username = username, FullName = fullName, Phone = phone };
            string json = JsonSerializer.Serialize(profile);
            File.WriteAllText(Path.Combine(folder, $"{username}.json"), json);
        }
    }
}
