// <copyright file="ExcludeAttribute.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

namespace TradingBridge.Core.Common.Attributes;

/// <summary>
/// ExcludeAttribute to exclude "something" from serialization.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "31.12.2025")]
[AttributeUsage(AttributeTargets.All)]
public class ExcludeAttribute : Attribute
{
}
