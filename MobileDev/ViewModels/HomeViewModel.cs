using banditoth.MAUI.Multilanguage.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MobileDev.Models;

namespace MobileDev.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private IConfiguration configuration;
        private ITranslator translator;

        public HomeViewModel(IConfiguration conf, ITranslator trans)
        {
            // Check  theme and languages setting when started.
            this.configuration = conf;
            this.translator = trans;
            Settings settings = configuration.Get<Settings>();

            if (settings.DarkTheme)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }

            translator.SetCurrentCulture(new CultureInfo(settings.Language));
        }

        [RelayCommand]
        private async void GetAPI()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://home.dtc-xtreme.net:7163/Calendar");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
            }catch(Exception ex)
            {
                //
                Console.WriteLine("Errors: " + ex.Message);
            }
        }
    }
}
