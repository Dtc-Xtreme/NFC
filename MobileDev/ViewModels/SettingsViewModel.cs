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
            this.settings = configuration.GetRequiredSection("Settings").Get<Settings>();

            Language = this.translator.CurrentCulture.IetfLanguageTag;
            AppTheme deviceTheme = Application.Current.PlatformAppTheme;

            if (Application.Current.UserAppTheme == AppTheme.Unspecified)
            {

            }
            else if (Application.Current.UserAppTheme == AppTheme.Light)
            {
                DarkTheme = false;
            }
            else if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                DarkTheme = true;
            }
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
                    settings.DarkTheme = true;
                    Application.Current.UserAppTheme = AppTheme.Dark;
                }
                else
                {
                    settings.DarkTheme = false;
                    Application.Current.UserAppTheme = AppTheme.Light;
                }
                settings.Save();
                OnPropertyChanged();
            }
        }
        public string Language
        {
            get => language;
            set
            {
                language = value;
                OnPropertyChanged();
                translator.SetCurrentCulture(new CultureInfo(value));
                settings.Save();
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
