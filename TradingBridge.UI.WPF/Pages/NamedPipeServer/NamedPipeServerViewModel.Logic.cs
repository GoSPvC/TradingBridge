// <copyright file="NamedPipeServerViewModel.Logic.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using TradingBridge.Core.Common.Attributes;
using TradingBridge.Core.Domain.Enums;
using TradingBridge.Core.Domain.Interfaces;
using TradingBridge.Core.Domain.Models;
using TradingBridge.UI.WPF.Abstractions.ViewModels;

namespace TradingBridge.UI.WPF.Pages.NamedPipeServer;

/// <summary>
/// Logic part of the NamedPipeServerViewModel class.
/// </summary>
[Tag("Created: NamedPipe Server Implementation", Version = 1.00, Date = "07.01.2026")]
public partial class NamedPipeServerViewModel : BaseViewModel
{
    private readonly INamedPipeServer namedPipeServer;
    private readonly ILogger<NamedPipeServerViewModel> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedPipeServerViewModel"/> class.
    /// </summary>
    /// <param name="namedPipeServer">The named pipe server.</param>
    /// <param name="logger">The logger.</param>
    public NamedPipeServerViewModel(
        INamedPipeServer namedPipeServer,
        ILogger<NamedPipeServerViewModel> logger)
    {
        this.namedPipeServer = namedPipeServer;
        this.logger = logger;

        this.namedPipeServer.MessageReceived += this.OnMessageReceived;
        this.namedPipeServer.ClientConnected += this.OnClientConnected;
        this.namedPipeServer.ClientDisconnected += this.OnClientDisconnected;
        this.namedPipeServer.ErrorOccurred += this.OnErrorOccurred;

        this.PipeName = this.namedPipeServer.PipeName;

        // Sync with current server state
        this.IsServerRunning = this.namedPipeServer.IsRunning;

        // Sync connected clients
        foreach (var client in this.namedPipeServer.ConnectedClients)
        {
            this.ConnectedClients.Add(client);
        }

        this.MessageLog.Add($"[{DateTime.Now:HH:mm:ss}] Named Pipe Server ViewModel initialized");

        if (this.IsServerRunning)
        {
            this.MessageLog.Add($"[{DateTime.Now:HH:mm:ss}] Server is running on pipe: {this.PipeName}");
        }
    }

    /// <summary>
    /// Gets the command to toggle the server on/off.
    /// </summary>
    [RelayCommand]
    private async Task ToggleServerAsync()
    {
        try
        {
            if (this.IsServerRunning)
            {
                await this.namedPipeServer.StopAsync();
                this.IsServerRunning = false;
                this.MessageLog.Add($"[{DateTime.Now:HH:mm:ss}] Server stopped");
                this.logger.LogInformation("Named Pipe server stopped");
            }
            else
            {
                await this.namedPipeServer.StartAsync();
                this.IsServerRunning = true;
                this.MessageLog.Add($"[{DateTime.Now:HH:mm:ss}] Server started on pipe: {this.PipeName}");
                this.logger.LogInformation("Named Pipe server started");
            }
        }
        catch (Exception ex)
        {
            this.MessageLog.Add($"[{DateTime.Now:HH:mm:ss}] ERROR: {ex.Message}");
            this.logger.LogError(ex, "Error toggling server");
        }
    }

    /// <summary>
    /// Gets the command to send a message.
    /// </summary>
    [RelayCommand]
    private async Task SendMessageAsync()
    {
        if (string.IsNullOrWhiteSpace(this.MessageToSend))
        {
            return;
        }

        if (!this.IsServerRunning)
        {
            this.MessageLog.Add($"[{DateTime.Now:HH:mm:ss}] Cannot send message - server is not running");
            return;
        }

        try
        {
            var message = new TradingMessage
            {
                Source = TradingPlatform.TradingBridge,
                Target = TradingPlatform.Unknown,
                MessageType = MessageType.Custom,
                Payload = this.MessageToSend,
            };

            await this.namedPipeServer.SendMessageAsync(message);

            this.MessageLog.Add($"[{DateTime.Now:HH:mm:ss}] [SENT] {this.MessageToSend}");
            this.MessageToSend = string.Empty;

            this.logger.LogInformation("Message sent to all clients");
        }
        catch (Exception ex)
        {
            this.MessageLog.Add($"[{DateTime.Now:HH:mm:ss}] ERROR sending message: {ex.Message}");
            this.logger.LogError(ex, "Error sending message");
        }
    }

    /// <summary>
    /// Gets the command to clear the message log.
    /// </summary>
    [RelayCommand]
    private void ClearLog()
    {
        this.MessageLog.Clear();
        this.MessageLog.Add($"[{DateTime.Now:HH:mm:ss}] Log cleared");
    }

    private void OnMessageReceived(object? sender, TradingMessage message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            var logEntry = $"[{message.Timestamp:HH:mm:ss}] [{message.Source}] {message.MessageType}: {message.Payload}";
            this.MessageLog.Add(logEntry);

            if (this.MessageLog.Count > 500)
            {
                this.MessageLog.RemoveAt(0);
            }
        });
    }

    private void OnClientConnected(object? sender, ConnectionStatus status)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            this.ConnectedClients.Add(status);
            var logEntry = $"[{DateTime.Now:HH:mm:ss}] Client connected: {status.ClientName} ({status.Platform})";
            this.MessageLog.Add(logEntry);
        });
    }

    private void OnClientDisconnected(object? sender, ConnectionStatus status)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            var client = this.ConnectedClients.FirstOrDefault(c => c.ClientName == status.ClientName);
            if (client != null)
            {
                this.ConnectedClients.Remove(client);
            }

            var logEntry = $"[{DateTime.Now:HH:mm:ss}] Client disconnected: {status.ClientName}";
            this.MessageLog.Add(logEntry);
        });
    }

    private void OnErrorOccurred(object? sender, string error)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            var logEntry = $"[{DateTime.Now:HH:mm:ss}] ERROR: {error}";
            this.MessageLog.Add(logEntry);
        });
    }
}
