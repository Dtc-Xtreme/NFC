using Android.App;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;
using Plugin.Fingerprint;
using Plugin.NFC;
using Android.Content;
using MobileDev.ViewModels;

namespace MobileDev;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
[IntentFilter(new[] { NfcAdapter.ActionNdefDiscovered }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = LicenseViewModel.MIME_TYPE)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        CrossFingerprint.SetCurrentActivityResolver(() => this);
        CrossNFC.Init(this);
        base.OnCreate(savedInstanceState);
    }

    protected override void OnResume()
    {
        base.OnResume();

        // Plugin NFC: Restart NFC listening on resume (needed for Android 10+) 
        CrossNFC.OnResume();
    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);

        // Plugin NFC: Tag Discovery Interception
        CrossNFC.OnNewIntent(intent);
    }
}

