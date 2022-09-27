using banditoth.MAUI.Multilanguage.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using IntelliJ.Lang.Annotations;
using Microsoft.Extensions.Configuration;
using Plugin.NFC;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private IConfiguration configuration;
        private ITranslator translator;
        private bool darkTheme = false;
        private string language;
        private bool nfcIsEnabled;

        public SettingsViewModel(IConfiguration conf, ITranslator trans)
        {
            this.configuration = conf;
            this.translator = trans;
            Language = this.translator.CurrentCulture.IetfLanguageTag;
            var deviceTheme = Application.Current.PlatformAppTheme;
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
                    Application.Current.UserAppTheme = AppTheme.Dark;
                }
                else
                {
                    Application.Current.UserAppTheme = AppTheme.Light;
                }
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
