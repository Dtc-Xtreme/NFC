using banditoth.MAUI.Multilanguage.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;
using Plugin.NFC;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileDev.Models;
using Android.Bluetooth;

namespace MobileDev.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private IConfiguration configuration;
        private ITranslator translator;
        private Settings settings;
        private bool darkTheme = false;
        private string language;
        private bool nfcIsEnabled;

        public SettingsViewModel(IConfiguration conf, ITranslator trans)
        {
            this.configuration = conf;
            this.translator = trans;
            this.settings = configuration.Get<Settings>();
            darkTheme = settings.DarkTheme;
            language = settings.Language;

            Language = this.translator.CurrentCulture.IetfLanguageTag;
            AppTheme deviceTheme = Application.Current.PlatformAppTheme;
            NfcIsEnabled = CrossNFC.Current.IsEnabled;
        }

        public bool DarkTheme
        {
            get { return this.darkTheme; }
            set
            {
                this.darkTheme = value;
                if (value)
                {
                    Application.Current.UserAppTheme = AppTheme.Dark;
                }
                else
                {
                    Application.Current.UserAppTheme = AppTheme.Light;
                }
                OnPropertyChanged();
                settings.DarkTheme = value;
            }
        }
        public string Language
        {
            get => language;
            set
            {
                language = value;
                translator.SetCurrentCulture(new CultureInfo(value));
                OnPropertyChanged();
                settings.Language = language;
            }
        }
        public bool NfcIsEnabled
        {
            get => nfcIsEnabled;
            set
            {
                nfcIsEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}
