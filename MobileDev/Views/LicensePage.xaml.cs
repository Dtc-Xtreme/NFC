using MobileDev.ViewModels;

namespace MobileDev.Views;

public partial class LicensePage : ContentPage
{
	public LicensePage(LicenseViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}

	//private void CheckPages_Clicked(object sender, EventArgs e)
	//{
 //       var a = Navigation.NavigationStack;
 //   }
}