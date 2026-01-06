// <copyright file="DefaultListener.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using TradingBridge.Core.Common.Attributes;

namespace TradingBridge.Core.Common.Helpers;

/// <summary>
/// DefaultListener to default listen to any event for testing.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "31.12.2025")]
public static class DefaultListener
{
    /// <summary>
    /// Listen to some event.
    /// </summary>
    /// <param name="s">Sender.</param>
    /// <param name="e">EventArgs.</param>
    public static void OnEvented(object? s, EventArgs e)
    {
    }
}
