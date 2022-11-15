﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel.Communication;
using MobileDev.Services;
using MobileDev.Views;
using PetanqueCL.Models;
using PetanqueCL.Repositories;
using Plugin.NFC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Encoding = System.Text.Encoding;

namespace MobileDev.ViewModels
{
    public partial class LicenseViewModel : BaseViewModel
    {
        private IAlertService alertService;
        private ILicenseRepository repository;
        private DisplayOrientation orientation;
        private int pageSize = 40;

        private bool split;
        private bool nfcAvailable = true;
        private GridLength columnWidth;
        private License? selectedLicense = null;
        private ObservableCollection<License> licensesList = new ObservableCollection<License>();

        public LicenseViewModel(IAlertService alert, ILicenseRepository repo)
        {
            this.alertService = alert;
            this.repository = repo;
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            CheckOrientation();
            LicensesList.Add(new License());
            GetAPI();
            //licenses = repository.Licenses.ToList();
            //LicensesList = new ObservableCollection<License>(licenses.Take(pageSize));
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
                selectedLicense = value;
                OnPropertyChanged();
                if (orientation == DisplayOrientation.Portrait)
                {
                    Navigate();
                }
            }
        }
        public ObservableCollection<License> LicensesList
        {
            get
            {
                return licensesList;
            }
            set
            {
                licensesList = value;
                OnPropertyChanged();
            }
        }

        [RelayCommand]
        private async void Nfc()
        {
            await StartListeningIfNotiOS();
            //Number = 1;
        }

        [RelayCommand]
        private void LoadMoreData()
        {
            //int counter = NumberList.Count();
            //var a = new ObservableCollection<int>(ints.Skip(counter).Take(pageSize));
            //foreach (var x in a)
            //{
            //    NumberList.Add(x);
            //}
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
                //var first = tagInfo.Records[0];
                //await alertService.ShowAlertAsync(title, GetMessage(first));
                //await ShowAlert(GetMessage(first), title);

                License lic = new License();
                lic.Nr = int.Parse(tagInfo.Records[0].Message.Substring(7));
                string[] name = tagInfo.Records[1].Message.Split(" ");
                lic.FirstName = name[0];
                lic.LastName = name[1];
                lic.BirthDate = DateTime.ParseExact(tagInfo.Records[2].Message, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Club club = new Club();
                club.Id = int.Parse(tagInfo.Records[0].Message.Substring(3, 3));
                lic.Club = club;
                SelectedLicense = lic;
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

        private async void GetAPI()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.dtc-xtreme.net/License");
                response.EnsureSuccessStatusCode();
                LicensesList = await response.Content.ReadFromJsonAsync<ObservableCollection<License>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errors: " + ex.Message);
            }
        }
    }
}
