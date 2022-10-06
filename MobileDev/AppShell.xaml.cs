using MobileDev.Views;

namespace MobileDev;

public partial class AppShell : Shell
{
    private IServiceProvider serviceProvider;

    public AppShell(IServiceProvider services)
	{
        this.serviceProvider = services;
        InitializeComponent();

		Routing.RegisterRoute("Home", typeof(HomePage));
        Routing.RegisterRoute("License", typeof(LicensePage));
        Routing.RegisterRoute("Score", typeof(ScoreTrackerPage));
        Routing.RegisterRoute("Settings", typeof(SettingsPage));
        Routing.RegisterRoute("Detail", typeof(LicenseDetailPage));
    }
}
