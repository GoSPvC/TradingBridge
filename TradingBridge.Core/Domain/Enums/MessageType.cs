// <copyright file="MessageType.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using TradingBridge.Core.Common.Attributes;

namespace TradingBridge.Core.Domain.Enums;

/// <summary>
/// Represents the type of trading message.
/// </summary>
[Tag("Created: NamedPipe Server Implementation", Version = 1.00, Date = "07.01.2026")]
public enum MessageType
{
    /// <summary>
    /// Unknown message type.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Handshake message to establish connection.
    /// </summary>
    Handshake = 1,

    /// <summary>
    /// Heartbeat message to keep connection alive.
    /// </summary>
    Heartbeat = 2,

    /// <summary>
    /// Market data update.
    /// </summary>
    MarketData = 3,

    /// <summary>
    /// Order placement request.
    /// </summary>
    OrderRequest = 4,

    /// <summary>
    /// Order execution confirmation.
    /// </summary>
    OrderConfirmation = 5,

    /// <summary>
    /// Position update.
    /// </summary>
    PositionUpdate = 6,

    /// <summary>
    /// Account information.
    /// </summary>
    AccountInfo = 7,

    /// <summary>
    /// Error message.
    /// </summary>
    Error = 8,

    /// <summary>
    /// Disconnect message.
    /// </summary>
    Disconnect = 9,

    /// <summary>
    /// Custom message.
    /// </summary>
    Custom = 10,
}
