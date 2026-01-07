// <copyright file="ConnectionStatus.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using TradingBridge.Core.Common.Attributes;
using TradingBridge.Core.Domain.Enums;

namespace TradingBridge.Core.Domain.Models;

/// <summary>
/// Represents the connection status of a trading platform.
/// </summary>
[Tag("Created: NamedPipe Server Implementation", Version = 1.00, Date = "07.01.2026")]
public class ConnectionStatus
{
    /// <summary>
    /// Gets or sets the trading platform.
    /// </summary>
    public TradingPlatform Platform { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the platform is connected.
    /// </summary>
    public bool IsConnected { get; set; }

    /// <summary>
    /// Gets or sets the connection timestamp.
    /// </summary>
    public DateTime? ConnectedSince { get; set; }

    /// <summary>
    /// Gets or sets the client name.
    /// </summary>
    public string? ClientName { get; set; }

    /// <summary>
    /// Gets or sets the last activity timestamp.
    /// </summary>
    public DateTime? LastActivity { get; set; }
}
