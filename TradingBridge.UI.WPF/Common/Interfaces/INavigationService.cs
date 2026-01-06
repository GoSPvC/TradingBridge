// <copyright file="INavigationService.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using TradingBridge.Core.Common.Attributes;

namespace TradingBridge.UI.WPF.Common.Interfaces;

/// <summary>
/// Interface for navigation service.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "04.01.2026")]
public interface INavigationService
{
    /// <summary>
    /// Occurs when the current view model changes.
    /// </summary>
    event EventHandler? CurrentViewModelChanged;

    /// <summary>
    /// Gets the current view model.
    /// </summary>
    object? CurrentViewModel { get; }

    /// <summary>
    /// Navigates to the specified view model.
    /// </summary>
    /// <typeparam name="TViewModel">The type of view model to navigate to.</typeparam>
    void NavigateTo<TViewModel>()
        where TViewModel : class;
}
