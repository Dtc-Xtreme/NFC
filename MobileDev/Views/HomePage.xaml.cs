using MobileDev.ViewModels;

namespace MobileDev.Views;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}