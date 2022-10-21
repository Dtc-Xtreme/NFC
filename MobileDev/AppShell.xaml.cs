using MobileDev.Views;
using Repositories.SeedData;
using Repositories;

namespace MobileDev;

public partial class AppShell : Shell
{
    private IServiceProvider serviceProvider;

    public AppShell(IServiceProvider services)
	{
        this.serviceProvider = services;

        InitializeComponent();

		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(LicensePage), typeof(LicensePage));
        Routing.RegisterRoute(nameof(ScoreTrackerPage), typeof(ScoreTrackerPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(LicenseDetailPage), typeof(LicenseDetailPage));
    }
}
