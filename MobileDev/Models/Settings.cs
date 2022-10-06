using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.Models
{
    public class Settings
    {
        public bool DarkTheme { get; set; }
        public string Language { get; set; }

        public async void Save()
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(this);
            var path = FileSystem.Current.CacheDirectory;
            var fullPath = Path.Combine(path, "test1.json");
            File.WriteAllText(fullPath, jsonString);
            string test1 = File.ReadAllText(fullPath);

            var xpath = FileSystem.Current.AppDataDirectory;
            var xfullPath = Path.Combine(xpath, "test2.json");
            File.WriteAllText(xfullPath, jsonString);
            string test2 = File.ReadAllText(fullPath);

            //var stream = await FileSystem.Current.OpenAppPackageFileAsync("appsettings.json");
            //using StreamReader reader = new StreamReader(stream);
            //var contents = reader.ReadToEnd();
        }
    }
}
