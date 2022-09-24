using MobileDev.ViewModels;

namespace MobileDev.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}