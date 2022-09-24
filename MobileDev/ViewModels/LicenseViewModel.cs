using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Util.LocaleData;

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

        //private int gridRow = 0;
        //public int GridRow
        //{
        //    get { return gridRow; }
        //    set
        //    {
        //        gridRow = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private int gridCol = 0;
        //public int GridCol
        //{
        //    get { return gridCol; }
        //    set
        //    {
        //        gridCol = value;
        //        OnPropertyChanged();
        //    }
        //}


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
                //GridRow = 1;
                //GridCol = 0;
            }
            else if (orientation == DisplayOrientation.Landscape)
            {
                Split = true;
                //GridRow = 0;
                //GridCol = 1;
            }

        }

        // Events //
        private void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            CheckOrientation();
        }
    }
}
