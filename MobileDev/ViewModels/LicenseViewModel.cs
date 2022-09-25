﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.ViewModels
{
    public partial class LicenseViewModel : BaseViewModel
    {
        private DisplayOrientation orientation;
        private int pageSize = 40;

        private bool split;
        public bool Split
        {
            get { return split; }
            set
            {
                split = value;
                OnPropertyChanged();
            }
        }

        private int? number = null;
        public int? Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged();
                if(orientation == DisplayOrientation.Portrait)
                {
                    Navigate();
                }
            }
        }

        private bool nfcAvailable = true;
        public bool NFCAvailable
        {
            get { return nfcAvailable; }
            set
            {
                nfcAvailable = value;
                OnPropertyChanged();
            }
        }

        private GridLength columnWidth;
        public GridLength ColumnWidth
        {
            get { return columnWidth; }
            set
            {
                columnWidth = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> numberList;
        public ObservableCollection<int> NumberList
        {
            get
            {
                return numberList;
            }
            set
            {
                numberList = value;
                OnPropertyChanged();
            }
        }

        private List<int> ints = new List<int>();

        public LicenseViewModel()
        {
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            CheckOrientation();

            ints = new List<int>();
            for (int x = 0; x < 1000; x++)
            {
                ints.Add(x);
            }
            NumberList = new ObservableCollection<int>(ints.Take(pageSize));
        }

        [RelayCommand]
        private void Nfc()
        {
            Number = 1;
        }

        [RelayCommand]
        private void LoadMoreData()
        {
            int counter = NumberList.Count();
            var a = new ObservableCollection<int>(ints.Skip(counter).Take(pageSize));
            foreach (var x in a)
            {
                NumberList.Add(x);
            }
        }

        private async Task Navigate()
        {
            await Shell.Current.GoToAsync($"Detail?Number={Number}"); 
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
    }
}
