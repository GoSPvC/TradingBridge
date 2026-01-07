// <copyright file="TradingPlatform.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using TradingBridge.Core.Common.Attributes;

namespace TradingBridge.Core.Domain.Enums;

/// <summary>
/// Represents supported trading platforms.
/// </summary>
[Tag("Created: NamedPipe Server Implementation", Version = 1.00, Date = "07.01.2026")]
public enum TradingPlatform
{
    /// <summary>
    /// Unknown platform.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// MetaTrader 4 platform.
    /// </summary>
    MetaTrader4 = 1,

    /// <summary>
    /// MetaTrader 5 platform.
    /// </summary>
    MetaTrader5 = 2,

    /// <summary>
    /// NinjaTrader platform.
    /// </summary>
    NinjaTrader = 3,

    /// <summary>
    /// TradingBridge server.
    /// </summary>
    TradingBridge = 4,
}
