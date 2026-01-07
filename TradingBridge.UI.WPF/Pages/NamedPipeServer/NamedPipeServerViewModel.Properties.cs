// <copyright file="NamedPipeServerViewModel.Properties.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TradingBridge.Core.Common.Attributes;
using TradingBridge.Core.Domain.Models;

namespace TradingBridge.UI.WPF.Pages.NamedPipeServer;

/// <summary>
/// Properties part of the NamedPipeServerViewModel class.
/// </summary>
[Tag("Created: NamedPipe Server Implementation", Version = 1.00, Date = "07.01.2026")]
public partial class NamedPipeServerViewModel
{
    /// <summary>
    /// Gets or sets a value indicating whether the server is running.
    /// </summary>
    [ObservableProperty]
    private bool isServerRunning;

    /// <summary>
    /// Gets or sets the pipe name.
    /// </summary>
    [ObservableProperty]
    private string pipeName = "TradingBridge";

    /// <summary>
    /// Gets or sets the message to send.
    /// </summary>
    [ObservableProperty]
    private string messageToSend = string.Empty;

    /// <summary>
    /// Gets the message log.
    /// </summary>
    public ObservableCollection<string> MessageLog { get; } = [];

    /// <summary>
    /// Gets the connected clients.
    /// </summary>
    public ObservableCollection<ConnectionStatus> ConnectedClients { get; } = [];
}
