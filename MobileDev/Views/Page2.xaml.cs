namespace MobileDev.Views;

public partial class Page2 : ContentPage
{
	public Page2()
	{
		InitializeComponent();
	}

    private async void GoToPage3_Clicked(object sender, EventArgs e)
    {
        var a = Navigation.NavigationStack;
        //Navigation.InsertPageBefore(new Page3(), );
        //await Navigation.PopAsync();
        //await Navigation.PopToRootAsync();
        await Navigation.PushAsync(new Page3());
    }
}