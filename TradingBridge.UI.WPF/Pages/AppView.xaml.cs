// <copyright file="AppView.xaml.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using System.Windows;
using TradingBridge.Core.Common.Attributes;

namespace TradingBridge.UI.WPF.Pages;

/// <summary>
/// Interaktionslogik für AppView.xaml.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "04.01.2026")]
public partial class AppView : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppView"/> class.
    /// </summary>
    /// <param name="viewModel">The application view model.</param>
    public AppView(AppViewModel viewModel)
    {
        this.InitializeComponent();
        this.DataContext = viewModel;
        viewModel.Initialize();
    }
}
