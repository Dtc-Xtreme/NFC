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
using MobileDev.Services;

namespace MobileDev.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private IConfiguration configuration;
        private ITranslator translator;
        private IServiceProvider serviceProvider;
        private IAlertService alertService;
        private ICalendarRepository<CalendarItem> calendarRepository;
        private readonly IFingerprint fingerprint;
        private List<CalendarItem> items;
        private bool isRefreshing = false;
        private bool auth = false;

        public HomeViewModel(IConfiguration conf, ITranslator trans, IServiceProvider serv, IFingerprint fingerprint, IAlertService alert, ICalendarRepository<CalendarItem> calendarRepository)
        {
            // Check  theme and languages setting when started.
            this.configuration = conf;
            this.translator = trans;
            this.serviceProvider = serv;
            this.fingerprint = fingerprint;
            this.alertService = alert;
            this.calendarRepository = calendarRepository;

            if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
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

        public bool Auth
        {
            get { return auth; }
            set
            {
                auth = value;
                OnPropertyChanged();
            }
        }

        [RelayCommand]
        private async void GetAPI()
        {
            Items = await this.calendarRepository.GetAll();
            IsRefreshing = false;
        }

        [RelayCommand]
        private async void Biometric()
        {
            var request = new AuthenticationRequestConfiguration("Validate that you have fingers", "Because without them you will not be able to access");
            var result = await fingerprint.AuthenticateAsync(request);
            if (result.Authenticated)
            {
                Auth = true;
                //await alertService.ShowAlertAsync("Authenticate!", "Access Granted", "OK");
            }
            else
            {
                await alertService.ShowAlertAsync("Unauthenticated", "Access Denied", "OK");
            }
        }
    }
}
