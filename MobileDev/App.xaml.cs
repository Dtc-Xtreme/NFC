﻿using MobileDev.Views;

namespace MobileDev;

public partial class App : Application
{
    private IServiceProvider serviceProvider;

    public App(IServiceProvider services)
	{
        this.serviceProvider = services;
        InitializeComponent();

		//MainPage = new NavigationPage(serviceProvider.GetRequiredService<AppShell>());
		MainPage = serviceProvider.GetRequiredService<AppShell>();
	}
}
