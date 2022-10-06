using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.Models
{
    public class Settings
    {
        private bool darkTheme;
        private string language;

        public bool DarkTheme {
            get { return darkTheme; }
            set { 
                darkTheme = value;
                Save();
            }
        }
        public string Language {
            get { return language; }
            set
            {
                language = value;
                Save();
            }
        }

        public Settings()
        {
            this.darkTheme = false;
            this.language = "en";
        }

        public Settings(bool darkTheme, string language)
        {
            DarkTheme = darkTheme;
            Language = language;
        }

        private async void Save()
        {
            string path = FileSystem.Current.AppDataDirectory;
            string fullPath = Path.Combine(path, "settings.json");
            string jsonString = System.Text.Json.JsonSerializer.Serialize(this);
            await File.WriteAllTextAsync(fullPath, jsonString);
        }
    }
}
