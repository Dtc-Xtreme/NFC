using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetanqueCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.ViewModels
{
    [QueryProperty(nameof(SelectedLicense), "SelectedLicense")]
    public partial class LicenseDetailViewModel : BaseViewModel
    {
        private License selectedlicense;

        public License SelectedLicense
        {
            get { return selectedlicense; }
            set
            {
                selectedlicense = value;
                OnPropertyChanged();
            }
        }
    }
}
