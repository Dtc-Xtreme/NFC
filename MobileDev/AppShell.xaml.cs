using MobileDev.Views;

namespace MobileDev;

public partial class AppShell : Shell
{
    private IServiceProvider serviceProvider;

    public AppShell(IServiceProvider services)
	{
        this.serviceProvider = services;
        InitializeComponent();

		Routing.RegisterRoute("Home", typeof(MainPage));
		Routing.RegisterRoute("Page1", typeof(Page1));
		Routing.RegisterRoute("Page2", typeof(Page2));
		Routing.RegisterRoute("Page3", typeof(Page3));
        Routing.RegisterRoute("View1", typeof(View1));
        Routing.RegisterRoute("View2", typeof(View2));
    }
}
