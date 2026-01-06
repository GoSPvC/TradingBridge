// <copyright file="BaseViewModel.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;
using TradingBridge.Core.Common.Attributes;

namespace TradingBridge.UI.WPF.Abstractions.ViewModels;

/// <summary>
/// Base class for all view models.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "03.01.2026")]
public abstract partial class BaseViewModel : ObservableObject
{
    /// <summary>
    /// Gets or sets a value indicating whether the view model is busy.
    /// </summary>
    [ObservableProperty]
    private bool isBusy;

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    [ObservableProperty]
    private string? errorMessage;
}
