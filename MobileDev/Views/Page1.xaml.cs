using MobileDev.ViewModels;

namespace MobileDev.Views;

public partial class Page1 : ContentPage
{
    public Page1(Page1ViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}