using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.ViewModels
{
    public partial class Page1ViewModel : ObservableObject
    {
        // Properties//
        private bool searchSeperation = true;

        // Methods //
        public bool SearchSeperation
        {
            get { return searchSeperation; }
            set
            {
                searchSeperation = value;
                OnPropertyChanged();
            }
        }

        // Constructors //
        public Page1ViewModel()
        {
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            CheckOrientation();
        }

        // Functions //
        private void CheckOrientation()
        {
            DisplayOrientation orientation = DeviceDisplay.MainDisplayInfo.Orientation;

            if (orientation == DisplayOrientation.Portrait)
            {
                SearchSeperation = false;
            }
            else if(orientation == DisplayOrientation.Landscape)
            {
                SearchSeperation = true;
            }

        }

        // Events //
        private void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            CheckOrientation();
        }
    }
}
