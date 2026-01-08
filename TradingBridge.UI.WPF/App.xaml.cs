// <copyright file="App.xaml.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TradingBridge.Core.Common.Attributes;
using TradingBridge.UI.WPF.Common.Interfaces;
using TradingBridge.UI.WPF.Common.Services;
using TradingBridge.UI.WPF.Pages;
using TradingBridge.UI.WPF.Pages.Home;
using TradingBridge.UI.WPF.Pages.NamedPipeServer;
using TradingBridge.UI.WPF.Services;

namespace TradingBridge.UI.WPF;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.11, Date = "07.01.2026")]
public partial class App : Application
{
    private ServiceProvider? serviceProvider;

    /// <summary>
    /// Raises the <see cref="Application.Startup"/> event.
    /// </summary>
    /// <param name="e">The event data.</param>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        ConfigureServices(services);
        this.serviceProvider = services.BuildServiceProvider();

        var appView = this.serviceProvider.GetRequiredService<AppView>();
        appView.Show();
    }

    /// <summary>
    /// Raises the <see cref="Application.Exit"/> event.
    /// </summary>
    /// <param name="e">The event data.</param>
    protected override void OnExit(ExitEventArgs e)
    {
        this.serviceProvider?.Dispose();
        base.OnExit(e);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Add logging
        services.AddLogging(configure =>
        {
            configure.AddConsole();
            configure.SetMinimumLevel(LogLevel.Information);
        });

        // Add infrastructure services (repositories, Named Pipe server, etc.)
        services.AddInfrastructure();

        // Add navigation service
        services.AddSingleton<INavigationService, NavigationService>();

        // Add ViewModels
        services.AddSingleton<AppViewModel>();
        services.AddTransient<HomeViewModel>();
        services.AddSingleton<NamedPipeServerViewModel>();

        // Add Views (Windows)
        services.AddSingleton<AppView>();
    }
}
