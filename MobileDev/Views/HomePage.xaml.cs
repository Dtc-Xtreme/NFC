using MobileDev.ViewModels;
using Plugin.Fingerprint.Abstractions;

namespace MobileDev.Views;

public partial class HomePage : ContentPage
{
    private readonly IFingerprint fingerprint;

    public HomePage(IFingerprint fingerprint, HomeViewModel vm)
    {
        this.fingerprint = fingerprint;
        BindingContext = vm;
        InitializeComponent();
	}

    private async void OnBiometricClicked(object sender, EventArgs e)
    {
        var request = new AuthenticationRequestConfiguration("Validate that you have fingers", "Because without them you will not be able to access");
        var result = await fingerprint.AuthenticateAsync(request);
        if (result.Authenticated)
        {
            await DisplayAlert("Authenticate!", "Access Granted", "OK");
        }
        else
        {
            await DisplayAlert("Unauthenticated", "Access Denied", "OK");
        }
    }
}