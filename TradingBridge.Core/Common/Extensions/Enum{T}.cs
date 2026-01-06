// <copyright file="Enum{T}.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using TradingBridge.Core.Common.Attributes;

namespace TradingBridge.Core.Common.Extensions;

/// <summary>
/// Enum-Extension.
/// </summary>
/// <typeparam name="T">Type T.</typeparam>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "01.01.2026")]
public class Enum<T>
{
    /// <summary>
    /// Check if a enum is defined by name.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <returns>True or False.</returns>
    public static bool IsDefined(string name)
    {
        return Enum.IsDefined(typeof(T), name);
    }

    /// <summary>
    /// Check if a enum is defined by value.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <returns>True or False.</returns>
    public static bool IsDefined(T value)
    {
        if (value is not null)
        {
            return Enum.IsDefined(typeof(T), value);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Returns all values of an enum.
    /// </summary>
    /// <returns>Values array.</returns>
    public static IEnumerable<T> GetValues()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }
}
