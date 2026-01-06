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
/// <param name="serviceProvider">The service provider.</param>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "04.01.2026")]
#pragma warning disable SA1009
public class NavigationService(IServiceProvider serviceProvider) : INavigationService
#pragma warning restore SA1009
{
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
        var viewModel = serviceProvider.GetRequiredService<TViewModel>();
        this.CurrentViewModel = viewModel;
    }
}
