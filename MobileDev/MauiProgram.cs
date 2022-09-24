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
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<Page1>();
        builder.Services.AddTransient<Page1ViewModel>();
        builder.Services.AddTransient<Page2>();
        builder.Services.AddTransient<Page3>();
        builder.Services.AddTransient<View1>();
        builder.Services.AddTransient<View2>();

        return builder.Build();
	}
}
