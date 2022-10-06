using Org.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Java.Util.Jar.Attributes;

namespace MobileDev.Models
{
    public class Settings
    {
        public bool DarkTheme { get; set; }
        public string Language { get; set; }

        public Settings()
        {
            DarkTheme = false;
            Language = "en";
        }

        public async void Save()
        {
            string path = FileSystem.Current.AppDataDirectory;
            string fullPath = Path.Combine(path, "settings.json");
            string jsonString = System.Text.Json.JsonSerializer.Serialize(this);
            File.WriteAllText(fullPath, jsonString);
        }
    }
}
