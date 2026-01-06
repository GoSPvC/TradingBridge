// <copyright file="HomeViewModel.Properties.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;
using TradingBridge.Core.Common.Attributes;

namespace TradingBridge.UI.WPF.Pages.Home;

/// <summary>
/// Properties part of the HomeViewModel class.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "01.01.2026")]
public partial class HomeViewModel
{
    /// <summary>
    /// Gets or sets the welcome message.
    /// </summary>
    [ObservableProperty]
    private string welcomeMessage = "Welcome to ExampleApp!";

    /// <summary>
    /// Gets or sets the current date and time.
    /// </summary>
    [ObservableProperty]
    private string currentDateTime = DateTime.Now.ToString("F");
}
