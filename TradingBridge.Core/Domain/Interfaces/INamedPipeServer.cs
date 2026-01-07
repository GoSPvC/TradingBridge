// <copyright file="INamedPipeServer.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using TradingBridge.Core.Common.Attributes;
using TradingBridge.Core.Domain.Models;

namespace TradingBridge.Core.Domain.Interfaces;

/// <summary>
/// Interface for the Named Pipe server.
/// </summary>
[Tag("Created: NamedPipe Server Implementation", Version = 1.00, Date = "07.01.2026")]
public interface INamedPipeServer
{
    /// <summary>
    /// Occurs when a message is received.
    /// </summary>
    event EventHandler<TradingMessage>? MessageReceived;

    /// <summary>
    /// Occurs when a client connects.
    /// </summary>
    event EventHandler<ConnectionStatus>? ClientConnected;

    /// <summary>
    /// Occurs when a client disconnects.
    /// </summary>
    event EventHandler<ConnectionStatus>? ClientDisconnected;

    /// <summary>
    /// Occurs when an error occurs.
    /// </summary>
    event EventHandler<string>? ErrorOccurred;

    /// <summary>
    /// Gets a value indicating whether the server is running.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Gets the pipe name.
    /// </summary>
    string PipeName { get; }

    /// <summary>
    /// Gets the list of connected clients.
    /// </summary>
    IReadOnlyList<ConnectionStatus> ConnectedClients { get; }

    /// <summary>
    /// Starts the Named Pipe server.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task StartAsync();

    /// <summary>
    /// Stops the Named Pipe server.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task StopAsync();

    /// <summary>
    /// Sends a message to all connected clients.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendMessageAsync(TradingMessage message);

    /// <summary>
    /// Sends a message to a specific client.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="clientName">The client name.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendMessageToClientAsync(TradingMessage message, string clientName);
}
