// <copyright file="DependencyInjection.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using TradingBridge.Core.Common.Attributes;
using TradingBridge.Core.Domain.Interfaces;
using TradingBridge.Infrastructure.Services;

namespace TradingBridge.UI.WPF.Services;

/// <summary>
/// Provides extension methods for configuring infrastructure services.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "04.01.2026")]
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register repositories
        // services.AddSingleton<IProductRepository, ProductRepository>();
        // services.AddSingleton<IUserRepository, UserRepository>();

        // Register shared services (Singletons for data sharing across ViewModels)
        // services.AddSingleton<ICurrentUserService, CurrentUserService>();
        // services.AddSingleton<ICartService, CartService>();
        // Register Named Pipe server

        // Register UI-specific services here if needed
        // services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<INamedPipeServer, NamedPipeServer>();
        return services;
    }
}
