using MobileDev.ViewModels;
using Plugin.Fingerprint.Abstractions;

namespace MobileDev.Views;

public partial class HomePage : ContentPage
{

    public HomePage(HomeViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
	}
}