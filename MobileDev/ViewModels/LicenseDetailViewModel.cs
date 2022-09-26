using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.ViewModels
{
    [QueryProperty("Number", "Number")]
    public partial class LicenseDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        private int number;
    }
}
