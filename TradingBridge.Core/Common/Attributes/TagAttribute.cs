// <copyright file="TagAttribute.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

namespace TradingBridge.Core.Common.Attributes;

/// <summary>
/// TagAttribute to display infotext, version and date of the latest version of this file.
/// </summary>
[Tag("Chged: Coding Convention/StyleCop", Version = 2.10, Date = "31.12.2025")]
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class TagAttribute : Attribute
{
    private readonly string? tag;

    /// <summary>
    /// Initializes a new instance of the <see cref="TagAttribute"/> class.
    /// </summary>
    /// <param name="tag">Infotext to describe the latest changes to this file.</param>
    public TagAttribute(string? tag)
    {
        this.tag = tag;
        this.Version = 1.0;
        this.Date = "01.01.1970";
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="TagAttribute"/> class.
    /// </summary>
    ~TagAttribute()
    {
        _ = this.tag;
    }

    /// <summary>
    /// Gets or sets the latest version of this file.
    /// </summary>
    public double Version { get; set; }

    /// <summary>
    /// Gets or sets the date of that latest version of this file.
    /// </summary>
    public string Date { get; set; }
}
