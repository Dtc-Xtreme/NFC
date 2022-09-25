using MobileDev.ViewModels;
using MobileDev.Views;
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;

namespace MobileDev;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddTransient<AppShell>();
        builder.Services.AddSingleton(typeof(IFingerprint), CrossFingerprint.Current);
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<LicensePage>();
        builder.Services.AddTransient<LicenseViewModel>();
        builder.Services.AddTransient<ScoreTrackerPage>();
        builder.Services.AddTransient<ScoreTrackerViewModel>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<LicenseSearchView>();
        builder.Services.AddTransient<LicenseContentView>();
        builder.Services.AddTransient<LicenseDetailPage>();
        builder.Services.AddTransient<LicenseDetailViewModel>();

        return builder.Build();
	}
}
