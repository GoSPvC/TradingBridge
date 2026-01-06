// <copyright file="NavigationService.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using TradingBridge.Core.Common.Attributes;
using TradingBridge.UI.WPF.Common.Interfaces;

namespace TradingBridge.UI.WPF.Common.Services;

/// <summary>
/// Service for handling navigation between view models.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "04.01.2026")]
public class NavigationService : INavigationService
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationService"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public NavigationService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public event EventHandler? CurrentViewModelChanged;

    /// <inheritdoc/>
    public object? CurrentViewModel
    {
        get;
        private set
        {
            field = value;
            this.CurrentViewModelChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <inheritdoc/>
    public void NavigateTo<TViewModel>()
        where TViewModel : class
    {
        var viewModel = this.serviceProvider.GetRequiredService<TViewModel>();
        this.CurrentViewModel = viewModel;
    }
}
