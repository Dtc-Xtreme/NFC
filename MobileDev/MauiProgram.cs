using MobileDev.ViewModels;
using MobileDev.Views;
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using banditoth.MAUI.Multilanguage;
using MobileDev.Resources.Translations;
using MobileDev.Models;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Layouts;
using MobileDev.Services;
using PetanqueCL.Repositories.EF;
using PetanqueCL.Repositories.Interfaces;
using PetanqueCL.Repositories;
using Repositories;
using Microsoft.Extensions.DependencyInjection;
using Repositories.SeedData;
using System;
using PetanqueCL.Models;
using Plugin.LocalNotification;

namespace MobileDev;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        IConfigurationRoot config;
        string path = FileSystem.Current.AppDataDirectory;
        string fullPath = Path.Combine(path, "settings.json");

        if (File.Exists(fullPath))
        {
            config = new ConfigurationBuilder()
                .AddJsonFile(fullPath)
                .Build();
        }
        else
        {
            Settings settings = new Settings();
            string jsonString = System.Text.Json.JsonSerializer.Serialize<Settings>(settings);
            File.WriteAllText(fullPath, jsonString);

            config = new ConfigurationBuilder()
            .AddJsonFile(fullPath)
            .Build();

            // appsettings.json implementation readonly
            //Assembly a = Assembly.GetExecutingAssembly();
            //using Stream stream = a.GetManifestResourceStream("MobileDev.appsettings.json");

            //config = new ConfigurationBuilder()
            //    .AddJsonStream(stream)
            //    .Build();
        }
        builder.Configuration.AddConfiguration(config);


        builder
            .UseMauiApp<App>()
            .UseLocalNotification()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

            })
            .ConfigureMultilanguage(config =>
            {
            // Set the source of the translations
            // You can use multiple resource managers by calling UseResource multiple times.
            config.UseResource(Translation.ResourceManager);

            // If the app is not storing last used culture, this culture will be used by default
            config.UseDefaultCulture(new System.Globalization.CultureInfo("en-US"));

            // Determines whether the app should store the last used culture
            config.StoreLastUsedCulture(true);

            // Determines whether the app should throw an exception if a translation is not found.
            config.ThrowExceptionIfTranslationNotFound(false);

            // You can set custom translation not found text by calling this method 
            config.SetTranslationNotFoundText("Transl_Not_Found:", appendTranslationKey: true);
            });

        // Add Services
        builder.Services.AddTransient<AppShell>();
        builder.Services.AddSingleton(typeof(IFingerprint), CrossFingerprint.Current);
        builder.Services.AddSingleton<IAlertService, AlertService>();
        builder.Services.AddTransient<ILicenseRepository<License>, LicenseRepository>();
        builder.Services.AddTransient<ICalendarRepository<CalendarItem>, CalendarRepository>();

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

        var serv = builder.Services.BuildServiceProvider();

        return builder.Build();
	}
}
