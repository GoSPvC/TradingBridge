// <copyright file="NamedPipeServer.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using System.Collections.Concurrent;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using TradingBridge.Core.Common.Attributes;
using TradingBridge.Core.Domain.Enums;
using TradingBridge.Core.Domain.Interfaces;
using TradingBridge.Core.Domain.Models;

namespace TradingBridge.Infrastructure.Services;

/// <summary>
/// Named Pipe server implementation for inter-process communication.
/// </summary>
[Tag("Created: NamedPipe Server Implementation", Version = 1.00, Date = "07.01.2026")]
public class NamedPipeServer : INamedPipeServer
{
    private readonly ILogger<NamedPipeServer> logger;
    private readonly ConcurrentDictionary<string, NamedPipeServerStream> connectedPipes;
    private readonly ConcurrentDictionary<string, ConnectionStatus> clientStatuses;
    private readonly CancellationTokenSource cancellationTokenSource;
    private bool isRunning;

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedPipeServer"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public NamedPipeServer(ILogger<NamedPipeServer> logger)
    {
        this.logger = logger;
        this.connectedPipes = new ConcurrentDictionary<string, NamedPipeServerStream>();
        this.clientStatuses = new ConcurrentDictionary<string, ConnectionStatus>();
        this.cancellationTokenSource = new CancellationTokenSource();
        this.PipeName = "TradingBridge";
    }

    /// <inheritdoc/>
    public event EventHandler<TradingMessage>? MessageReceived;

    /// <inheritdoc/>
    public event EventHandler<ConnectionStatus>? ClientConnected;

    /// <inheritdoc/>
    public event EventHandler<ConnectionStatus>? ClientDisconnected;

    /// <inheritdoc/>
    public event EventHandler<string>? ErrorOccurred;

    /// <inheritdoc/>
    public bool IsRunning => this.isRunning;

    /// <inheritdoc/>
    public string PipeName { get; }

    /// <inheritdoc/>
    public IReadOnlyList<ConnectionStatus> ConnectedClients =>
        this.clientStatuses.Values.ToList();

    /// <inheritdoc/>
    public async Task StartAsync()
    {
        if (this.isRunning)
        {
            this.logger.LogWarning("Server is already running");
            return;
        }

        this.isRunning = true;
        this.logger.LogInformation("Starting Named Pipe server on pipe: {PipeName}", this.PipeName);

        await Task.Run(() => this.ListenForClientsAsync(), this.cancellationTokenSource.Token);
    }

    /// <inheritdoc/>
    public async Task StopAsync()
    {
        if (!this.isRunning)
        {
            this.logger.LogWarning("Server is not running");
            return;
        }

        this.logger.LogInformation("Stopping Named Pipe server");
        this.isRunning = false;
        this.cancellationTokenSource.Cancel();

        foreach (var pipe in this.connectedPipes.Values)
        {
            try
            {
                pipe.Close();
                pipe.Dispose();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error closing pipe");
            }
        }

        this.connectedPipes.Clear();
        this.clientStatuses.Clear();

        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async Task SendMessageAsync(TradingMessage message)
    {
        var tasks = this.connectedPipes.Select(kvp =>
            this.SendMessageToPipeAsync(kvp.Value, message, kvp.Key));

        await Task.WhenAll(tasks);
    }

    /// <inheritdoc/>
    public async Task SendMessageToClientAsync(TradingMessage message, string clientName)
    {
        if (this.connectedPipes.TryGetValue(clientName, out var pipe))
        {
            await this.SendMessageToPipeAsync(pipe, message, clientName);
        }
        else
        {
            this.logger.LogWarning("Client {ClientName} not found", clientName);
        }
    }

    private async Task ListenForClientsAsync()
    {
        while (this.isRunning && !this.cancellationTokenSource.Token.IsCancellationRequested)
        {
            try
            {
                var serverStream = new NamedPipeServerStream(
                    this.PipeName,
                    PipeDirection.InOut,
                    NamedPipeServerStream.MaxAllowedServerInstances,
                    PipeTransmissionMode.Message,
                    PipeOptions.Asynchronous);

                this.logger.LogInformation("Waiting for client connection...");

                await serverStream.WaitForConnectionAsync(this.cancellationTokenSource.Token);

                var clientId = Guid.NewGuid().ToString();
                this.logger.LogInformation("Client connected: {ClientId}", clientId);

                _ = Task.Run(() => this.HandleClientAsync(serverStream, clientId), this.cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                this.logger.LogInformation("Server stopped");
                break;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error accepting client connection");
                this.ErrorOccurred?.Invoke(this, $"Connection error: {ex.Message}");
            }
        }
    }

    private async Task HandleClientAsync(NamedPipeServerStream pipeStream, string clientId)
    {
        var connectionStatus = new ConnectionStatus
        {
            Platform = TradingPlatform.Unknown,
            IsConnected = true,
            ConnectedSince = DateTime.UtcNow,
            ClientName = clientId,
            LastActivity = DateTime.UtcNow,
        };

        this.connectedPipes.TryAdd(clientId, pipeStream);
        this.clientStatuses.TryAdd(clientId, connectionStatus);

        try
        {
            var buffer = new byte[4096];

            while (this.isRunning && pipeStream.IsConnected)
            {
                var bytesRead = await pipeStream.ReadAsync(buffer, this.cancellationTokenSource.Token);

                if (bytesRead > 0)
                {
                    var json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    this.logger.LogDebug("Received message from {ClientId}: {Json}", clientId, json);

                    var message = JsonSerializer.Deserialize<TradingMessage>(json);

                    if (message != null)
                    {
                        connectionStatus.LastActivity = DateTime.UtcNow;

                        if (message.MessageType == MessageType.Handshake)
                        {
                            connectionStatus.Platform = message.Source;
                            connectionStatus.ClientName = message.Payload ?? clientId;
                            this.ClientConnected?.Invoke(this, connectionStatus);
                        }

                        this.MessageReceived?.Invoke(this, message);
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
            this.logger.LogInformation("Client handler cancelled: {ClientId}", clientId);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error handling client {ClientId}", clientId);
            this.ErrorOccurred?.Invoke(this, $"Client error: {ex.Message}");
        }
        finally
        {
            this.DisconnectClient(clientId, pipeStream, connectionStatus);
        }
    }

    private void DisconnectClient(string clientId, NamedPipeServerStream pipeStream, ConnectionStatus connectionStatus)
    {
        this.logger.LogInformation("Client disconnected: {ClientId}", clientId);

        connectionStatus.IsConnected = false;
        this.ClientDisconnected?.Invoke(this, connectionStatus);

        this.connectedPipes.TryRemove(clientId, out _);
        this.clientStatuses.TryRemove(clientId, out _);

        try
        {
            pipeStream.Close();
            pipeStream.Dispose();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error closing pipe for client {ClientId}", clientId);
        }
    }

    private async Task SendMessageToPipeAsync(NamedPipeServerStream pipe, TradingMessage message, string clientName)
    {
        try
        {
            if (pipe.IsConnected)
            {
                var json = JsonSerializer.Serialize(message);
                var bytes = Encoding.UTF8.GetBytes(json);

                await pipe.WriteAsync(bytes, this.cancellationTokenSource.Token);
                await pipe.FlushAsync(this.cancellationTokenSource.Token);

                this.logger.LogDebug("Sent message to {ClientName}", clientName);
            }
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error sending message to {ClientName}", clientName);
            this.ErrorOccurred?.Invoke(this, $"Send error: {ex.Message}");
        }
    }
}
