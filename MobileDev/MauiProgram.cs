using MobileDev.ViewModels;
using MobileDev.Views;

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
		builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<LicensePage>();
        builder.Services.AddTransient<LicenseViewModel>();
        builder.Services.AddTransient<ScoreTrackerPage>();
        builder.Services.AddTransient<ScoreTrackerViewModel>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<View1>();
        builder.Services.AddTransient<View2>();

        return builder.Build();
	}
}
