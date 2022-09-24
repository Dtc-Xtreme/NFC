﻿using MobileDev.ViewModels;
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
        //Routing.RegisterRoute("View1", typeof(View1));
        //Routing.RegisterRoute("View2", typeof(View2));
    }
}
