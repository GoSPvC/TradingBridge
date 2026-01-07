// <copyright file="AppViewModel.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TradingBridge.Core.Common.Attributes;
using TradingBridge.UI.WPF.Abstractions.ViewModels;
using TradingBridge.UI.WPF.Common.Interfaces;
using TradingBridge.UI.WPF.Pages.Home;
using TradingBridge.UI.WPF.Pages.NamedPipeServer;

namespace TradingBridge.UI.WPF.Pages;

/// <summary>
/// Main application view model that manages navigation.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "04.01.2026")]
public partial class AppViewModel : BaseViewModel
{
    private readonly INavigationService navigationService;

    /// <summary>
    /// Gets or sets the current page view model.
    /// </summary>
    [ObservableProperty]
    private object? currentPageViewModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppViewModel"/> class.
    /// </summary>
    /// <param name="navigationService">The navigation service.</param>
    public AppViewModel(INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    /// <summary>
    /// Initializes the application view model.
    /// </summary>
    public void Initialize()
    {
        this.navigationService.CurrentViewModelChanged += this.OnCurrentViewModelChanged;
        this.navigationService.NavigateTo<HomeViewModel>();
    }

    /// <summary>
    /// Gets the command to navigate to the home page.
    /// </summary>
    [RelayCommand]
    private void NavigateToHome()
    {
        this.navigationService.NavigateTo<HomeViewModel>();
    }

    /// <summary>
    /// Gets the command to navigate to the Named Pipe server page.
    /// </summary>
    [RelayCommand]
    private void NavigateToNamedPipeServer()
    {
        this.navigationService.NavigateTo<NamedPipeServerViewModel>();
    }

    // /// <summary>
    // /// Gets the command to navigate to the products page.
    // /// </summary>
    // [RelayCommand]
    // private void NavigateToProducts()
    // {
    //     navigationService.NavigateTo<ProductsViewModel>();
    // }

    // /// <summary>
    // /// Gets the command to navigate to the settings page.
    // /// </summary>
    // [RelayCommand]
    // private void NavigateToSettings()
    // {
    //     navigationService.NavigateTo<SettingsViewModel>();
    // }
    private void OnCurrentViewModelChanged(object? sender, EventArgs e)
    {
        this.CurrentPageViewModel = this.navigationService.CurrentViewModel;
    }
}
