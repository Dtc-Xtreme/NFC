using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel.Communication;
using MobileDev.Services;
using MobileDev.Views;
using PetanqueCL.Models;
using Plugin.NFC;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using PetanqueCL.Models;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Windows.Input;
using Plugin.LocalNotification;

namespace MobileDev.ViewModels
{
    public partial class LicenseViewModel : BaseViewModel
    {
        private IAlertService alertService;
        private ILicenseRepository<License> licenseRepository;
        private DisplayOrientation orientation;
        private int pageSize = 10;

        private bool split;
        private bool nfcAvailable = true;
        private GridLength columnWidth;
        private License? selectedLicense = null;
        private string searchText;
        private ObservableCollection<License> searchResults = new ObservableCollection<License>();
        private ObservableCollection<License> showResults = new ObservableCollection<License>();


        public ICommand LoadExtraCommand { get; set; }
        public LicenseViewModel(IAlertService alert, ILicenseRepository<License> licenseRepository)
        {
            this.alertService = alert;
            this.licenseRepository = licenseRepository;
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            LoadExtraCommand = new Command(LoadMoreData, LoadMoreDate_CanExecute);
            CheckOrientation();
            GetAPI();
        }

        public bool Split
        {
            get { return split; }
            set
            {
                split = value;
                OnPropertyChanged();
            }
        }
        public bool NFCAvailable
        {
            get { return nfcAvailable; }
            set
            {
                nfcAvailable = value;
                OnPropertyChanged();
            }
        }
        public GridLength ColumnWidth
        {
            get { return columnWidth; }
            set
            {
                columnWidth = value;
                OnPropertyChanged();
            }
        }
        public License? SelectedLicense
        {
            get { return selectedLicense; }
            set
            {
                if (selectedLicense != value)
                {
                    SetProperty(ref selectedLicense, value);
                    if (orientation == DisplayOrientation.Portrait)
                    {
                        Navigate();
                    }
                }
            }
        }
        public string SearchText
        {
            get { return this.searchText; }
            set
            {
                this.searchText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<License> SearchResults
        {
            get { return this.searchResults; }
            set
            {
                this.searchResults = value;
                OnPropertyChanged();
                LoadMoreData();
            }
        }
        public ObservableCollection<License> ShowResults
        {
            get { return this.showResults; }
            set
            {
                this.showResults = value;
                OnPropertyChanged();
            }
        }

        [RelayCommand]
        private async void Nfc()
        {
            if (!CrossNFC.Current.IsEnabled)
            {
                NotificationRequest request = new NotificationRequest
                {
                    Title = "NFC",
                    Description = "Your device does not support NFC or it may be disabled!",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now
                    }
                };
                LocalNotificationCenter.Current.Show(request);
            }
            else
            {
                await StartListeningIfNotiOS();
            }
        }

        //[RelayCommand]
        private void LoadMoreData()
        {
            int counter = ShowResults.Count();
            if (counter < SearchResults.Count())
            {
                var a = new ObservableCollection<License>(SearchResults.Skip(counter).Take(pageSize));
                foreach (var x in a)
                {
                    ShowResults.Add(x);
                }
            }
        }

        private bool LoadMoreDate_CanExecute()
        {
            return ShowResults.Count() < SearchResults.Count();
        }

        [RelayCommand]
        private async void Search()
        {
            SearchResults.Clear();
            ShowResults.Clear();
            SearchResults = new ObservableCollection<License>(await licenseRepository.Find(SearchText));
        }

        private async void Navigate()
        {
            var args = new Dictionary<string, object>
            {
                {"SelectedLicense", SelectedLicense}
            };
            await Shell.Current.GoToAsync(nameof(LicenseDetailPage), args); 
        }

        private void CheckOrientation()
        {
            orientation = DeviceDisplay.MainDisplayInfo.Orientation;

            if (orientation == DisplayOrientation.Portrait)
            {
                Split = false;
                ColumnWidth = GridLength.Auto;
            }
            else if (orientation == DisplayOrientation.Landscape)
            {
                Split = true;
                ColumnWidth = new GridLength(2, GridUnitType.Star);
            }

        }

        private async void GetAPI()
        {
            SearchResults = new ObservableCollection<License>(await licenseRepository.GetAll());
        }

        // Events //
        private void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            CheckOrientation();
        }
        
        //#####//
        // NFC //
        //#####//
        public const string ALERT_TITLE = "NFC";
        public const string MIME_TYPE = "application/com.companyname.nfcsample";

        NFCNdefTypeFormat _type;
        bool _makeReadOnly = false;
        bool _eventsAlreadySubscribed = false;
        bool _isDeviceiOS = false;

        /// <summary>
        /// Property that tracks whether the Android device is still listening,
        /// so it can indicate that to the user.
        /// </summary>
        public bool DeviceIsListening
        {
            get => _deviceIsListening;
            set
            {
                _deviceIsListening = value;
                OnPropertyChanged(nameof(DeviceIsListening));
            }
        }
        private bool _deviceIsListening;

        private bool _nfcIsEnabled;
        public bool NfcIsEnabled
        {
            get => _nfcIsEnabled;
            set
            {
                _nfcIsEnabled = value;
                OnPropertyChanged(nameof(NfcIsEnabled));
                OnPropertyChanged(nameof(NfcIsDisabled));
            }
        }

        public bool NfcIsDisabled => !NfcIsEnabled;

        /// <summary>
        /// Subscribe to the NFC events
        /// </summary>
        void SubscribeEvents()
        {
            if (_eventsAlreadySubscribed)
                UnsubscribeEvents();

            _eventsAlreadySubscribed = true;

            CrossNFC.Current.OnMessageReceived += Current_OnMessageReceived;
            CrossNFC.Current.OnMessagePublished += Current_OnMessagePublished;
            CrossNFC.Current.OnTagDiscovered += Current_OnTagDiscovered;
            CrossNFC.Current.OnNfcStatusChanged += Current_OnNfcStatusChanged;
            CrossNFC.Current.OnTagListeningStatusChanged += Current_OnTagListeningStatusChanged;

            if (_isDeviceiOS)
                CrossNFC.Current.OniOSReadingSessionCancelled += Current_OniOSReadingSessionCancelled;
        }

        /// <summary>
        /// Unsubscribe from the NFC events
        /// </summary>
        void UnsubscribeEvents()
        {
            CrossNFC.Current.OnMessageReceived -= Current_OnMessageReceived;
            CrossNFC.Current.OnMessagePublished -= Current_OnMessagePublished;
            CrossNFC.Current.OnTagDiscovered -= Current_OnTagDiscovered;
            CrossNFC.Current.OnNfcStatusChanged -= Current_OnNfcStatusChanged;
            CrossNFC.Current.OnTagListeningStatusChanged -= Current_OnTagListeningStatusChanged;

            if (_isDeviceiOS)
                CrossNFC.Current.OniOSReadingSessionCancelled -= Current_OniOSReadingSessionCancelled;

            _eventsAlreadySubscribed = false;
        }

        /// <summary>
        /// Event raised when a NDEF message is received
        /// </summary>
        /// <param name="tagInfo">Received <see cref="ITagInfo"/></param>
        async void Current_OnMessageReceived(ITagInfo tagInfo)
        {
            if (tagInfo == null)
            {
                await alertService.ShowAlertAsync("X","No tag found");
                //await ShowAlert("No tag found");
                return;
            }

            // Customized serial number
            var identifier = tagInfo.Identifier;
            var serialNumber = NFCUtils.ByteArrayToHexString(identifier, ":");
            var title = !string.IsNullOrWhiteSpace(serialNumber) ? $"Tag [{serialNumber}]" : "Tag Info";

            if (!tagInfo.IsSupported)
            {
                await alertService.ShowAlertAsync(title, "Unsupported tag (app)");
                //await ShowAlert("Unsupported tag (app)", title);
            }
            else if (tagInfo.IsEmpty)
            {
                await alertService.ShowAlertAsync(title, "Empty tag");
                //await ShowAlert("Empty tag", title);
            }
            else
            {
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                jsonSerializerOptions.PropertyNameCaseInsensitive = true;

                string json = tagInfo.Records[0].Message;
                License lic = JsonSerializer.Deserialize<License>(json, jsonSerializerOptions);

                NetworkAccess accessType = Connectivity.Current.NetworkAccess;
                if (accessType == NetworkAccess.Internet)
                {
                    License? result = await licenseRepository.FindById(lic.Id);
                    if(result != null) lic = result;
                }
                SelectedLicense = lic;
                //SearchResults.Clear();
                //ShowResults.Clear();
                //SearchResults = new ObservableCollection<License> { lic };
            }
        }

        /// <summary>
        /// Event raised when data has been published on the tag
        /// </summary>
        /// <param name="tagInfo">Published <see cref="ITagInfo"/></param>
        async void Current_OnMessagePublished(ITagInfo tagInfo)
        {
            try
            {
                // #UI ChkReadOnly.IsChecked = false;
                CrossNFC.Current.StopPublishing();
                if (tagInfo.IsEmpty)
                    await alertService.ShowAlertAsync("X", "Formatting tag operation successful");
                    //await ShowAlert("Formatting tag operation successful");
                else
                    await alertService.ShowAlertAsync("X", "Writing tag operation successful");
                    //await ShowAlert("Writing tag operation successful");
            }
            catch (Exception ex)
            {
                await alertService.ShowAlertAsync("X", ex.Message);
                //await ShowAlert(ex.Message);
            }
        }

        /// <summary>
        /// Event raised when a NFC Tag is discovered
        /// </summary>
        /// <param name="tagInfo"><see cref="ITagInfo"/> to be published</param>
        /// <param name="format">Format the tag</param>
        async void Current_OnTagDiscovered(ITagInfo tagInfo, bool format)
        {
            if (!CrossNFC.Current.IsWritingTagSupported)
            {
                await alertService.ShowAlertAsync("X", "Writing tag is not supported on this device");
                //await ShowAlert("Writing tag is not supported on this device");
                return;
            }

            try
            {
                NFCNdefRecord record = null;
                switch (_type)
                {
                    case NFCNdefTypeFormat.WellKnown:
                        record = new NFCNdefRecord
                        {
                            TypeFormat = NFCNdefTypeFormat.WellKnown,
                            MimeType = MIME_TYPE,
                            Payload = NFCUtils.EncodeToByteArray("Plugin.NFC is awesome!"),
                            LanguageCode = "en"
                        };
                        break;
                    case NFCNdefTypeFormat.Uri:
                        record = new NFCNdefRecord
                        {
                            TypeFormat = NFCNdefTypeFormat.Uri,
                            Payload = NFCUtils.EncodeToByteArray("https://github.com/franckbour/Plugin.NFC")
                        };
                        break;
                    case NFCNdefTypeFormat.Mime:
                        record = new NFCNdefRecord
                        {
                            TypeFormat = NFCNdefTypeFormat.Mime,
                            MimeType = MIME_TYPE,
                            Payload = NFCUtils.EncodeToByteArray("Plugin.NFC is awesome!")
                        };
                        break;
                    default:
                        break;
                }

                if (!format && record == null)
                    throw new Exception("Record can't be null.");

                tagInfo.Records = new[] { record };

                if (format)
                    CrossNFC.Current.ClearMessage(tagInfo);
                else
                {
                    CrossNFC.Current.PublishMessage(tagInfo, _makeReadOnly);
                }
            }
            catch (Exception ex)
            {
                await alertService.ShowAlertAsync("X", ex.Message);
                //await ShowAlert(ex.Message);
            }
        }

        /// <summary>
        /// Event raised when NFC Status has changed
        /// </summary>
        /// <param name="isEnabled">NFC status</param>
        async void Current_OnNfcStatusChanged(bool isEnabled)
        {
            NfcIsEnabled = isEnabled;
            await alertService.ShowAlertAsync("X", $"NFC has been {(isEnabled ? "enabled" : "disabled")}");
            //await ShowAlert($"NFC has been {(isEnabled ? "enabled" : "disabled")}");
        }

        /// <summary>
        /// Event raised when Listener Status has changed
        /// </summary>
        /// <param name="isListening"></param>
        void Current_OnTagListeningStatusChanged(bool isListening) => DeviceIsListening = isListening;

        /// <summary>
        /// Event raised when user cancelled NFC session on iOS 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Current_OniOSReadingSessionCancelled(object sender, EventArgs e) => Console.WriteLine("iOS NFC Session has been cancelled");

        /// <summary>
        /// Task to start listening for NFC tags if the user's device platform is not iOS
        /// </summary>
        /// <returns>The task to be performed</returns>
        async Task StartListeningIfNotiOS()
        {
            if (_isDeviceiOS)
            {
                SubscribeEvents();
                return;
            }
            await BeginListening();
        }

        /// <summary>
        /// Task to safely start listening for NFC Tags
        /// </summary>
        /// <returns>The task to be performed</returns>
        async Task BeginListening()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    SubscribeEvents();
                    CrossNFC.Current.StartListening();
                });
            }
            catch (Exception ex)
            {
                await alertService.ShowAlertAsync("X", ex.Message);
                //await ShowAlert(ex.Message);
            }
        }

        /// <summary>
        /// Task to safely stop listening for NFC tags
        /// </summary>
        /// <returns>The task to be performed</returns>
        async Task StopListening()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    CrossNFC.Current.StopListening();
                    UnsubscribeEvents();
                });
            }
            catch (Exception ex)
            {
                await alertService.ShowAlertAsync("X", ex.Message);
                //await ShowAlert(ex.Message);
            }
        }

        /// <summary>
        /// Returns the tag information from NDEF record
        /// </summary>
        /// <param name="record"><see cref="NFCNdefRecord"/></param>
        /// <returns>The tag information</returns>
        string GetMessage(NFCNdefRecord record)
        {
            var message = $"Message: {record.Message}";
            message += Environment.NewLine;
            message += $"RawMessage: {Encoding.UTF8.GetString(record.Payload)}";
            message += Environment.NewLine;
            message += $"Type: {record.TypeFormat}";

            if (!string.IsNullOrWhiteSpace(record.MimeType))
            {
                message += Environment.NewLine;
                message += $"MimeType: {record.MimeType}";
            }

            return message;
        }
    }
}
