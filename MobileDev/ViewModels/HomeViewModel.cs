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
using PetanqueCL.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using Repositories.SeedData;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

namespace MobileDev.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private IConfiguration configuration;
        private ITranslator translator;
        private IServiceProvider serviceProvider;
        private List<CalendarItem> items;
        private bool isRefreshing = false;

        public HomeViewModel(IConfiguration conf, ITranslator trans, IServiceProvider serv)
        {
            // Check  theme and languages setting when started.
            this.configuration = conf;
            this.translator = trans;
            this.serviceProvider = serv;

            if(DeviceInfo.Current.Platform == DevicePlatform.WinUI)
            {
                // fill database with data
                SeedData.EnsurePopulated(serviceProvider.GetService<PetanqueDbContext>());
            }

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
            GetAPI();
        }

        public List<CalendarItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        [RelayCommand]
        private async void GetAPI()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.dtc-xtreme.net/Calendar");
                response.EnsureSuccessStatusCode();
                Items = await response.Content.ReadFromJsonAsync<List<CalendarItem>>();
                IsRefreshing = false;
            }catch(Exception ex)
            {
                //
                Console.WriteLine("Errors: " + ex.Message);
            }
        }
    }
}
