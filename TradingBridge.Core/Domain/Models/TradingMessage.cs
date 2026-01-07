// <copyright file="TradingMessage.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using TradingBridge.Core.Common.Attributes;
using TradingBridge.Core.Domain.Enums;

namespace TradingBridge.Core.Domain.Models;

/// <summary>
/// Represents a trading message exchanged between platforms.
/// </summary>
[Tag("Created: NamedPipe Server Implementation", Version = 1.00, Date = "07.01.2026")]
public class TradingMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TradingMessage"/> class.
    /// </summary>
    public TradingMessage()
    {
        this.MessageId = Guid.NewGuid();
        this.Timestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets or sets the unique message identifier.
    /// </summary>
    public Guid MessageId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the message was created.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the source platform.
    /// </summary>
    public TradingPlatform Source { get; set; }

    /// <summary>
    /// Gets or sets the target platform.
    /// </summary>
    public TradingPlatform Target { get; set; }

    /// <summary>
    /// Gets or sets the message type.
    /// </summary>
    public MessageType MessageType { get; set; }

    /// <summary>
    /// Gets or sets the message payload.
    /// </summary>
    public string? Payload { get; set; }

    /// <summary>
    /// Gets or sets the symbol/instrument.
    /// </summary>
    public string? Symbol { get; set; }

    /// <summary>
    /// Gets or sets the price.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the volume.
    /// </summary>
    public decimal? Volume { get; set; }
}
