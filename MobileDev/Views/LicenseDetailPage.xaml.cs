using MobileDev.ViewModels;

namespace MobileDev.Views;

public partial class LicenseDetailPage : ContentPage
{
	public LicenseDetailPage(LicenseDetailViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}