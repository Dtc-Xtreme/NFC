using MobileDev.Views;

namespace MobileDev;

public partial class MainPage : ContentPage
{
    private IServiceProvider serviceProvider;

    int count = 0;

	public MainPage(IServiceProvider services)
	{
        this.serviceProvider = services;
        InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

