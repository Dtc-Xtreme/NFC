using MobileDev.ViewModels;

namespace MobileDev.Views;

public partial class ScoreTrackerPage : ContentPage
{
	public ScoreTrackerPage(ScoreTrackerViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}