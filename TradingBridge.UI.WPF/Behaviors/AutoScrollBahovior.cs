// <copyright file="AutoScrollBahovior.cs" company="MichaelWernerPT">
// Copyright (c) MichaelWernerPT. All rights reserved.
// </copyright>

using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using TradingBridge.Core.Common.Attributes;

namespace TradingBridge.UI.WPF.Behaviors;

/// <summary>
/// Attached behavior for automatic scrolling to the bottom of a ScrollViewer.
/// </summary>
[Tag("Created: AutoScroll Behavior", Version = 1.00, Date = "08.01.2026")]
public static class AutoScrollBehavior
{
    /// <summary>
    /// Attached property for enabling auto-scroll.
    /// </summary>
    public static readonly DependencyProperty AutoScrollProperty =
        DependencyProperty.RegisterAttached(
            "AutoScroll",
            typeof(bool),
            typeof(AutoScrollBehavior),
            new PropertyMetadata(false, OnAutoScrollChanged));

    /// <summary>
    /// Gets the AutoScroll property value.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>True if auto-scroll is enabled.</returns>
    public static bool GetAutoScroll(DependencyObject obj)
    {
        return (bool)obj.GetValue(AutoScrollProperty);
    }

    /// <summary>
    /// Sets the AutoScroll property value.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="value">True to enable auto-scroll.</param>
    public static void SetAutoScroll(DependencyObject obj, bool value)
    {
        obj.SetValue(AutoScrollProperty, value);
    }

    private static void OnAutoScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ScrollViewer scrollViewer && e.NewValue is bool enableAutoScroll)
        {
            if (enableAutoScroll)
            {
                scrollViewer.Loaded += ScrollViewer_Loaded;
            }
            else
            {
                scrollViewer.Loaded -= ScrollViewer_Loaded;
            }
        }
    }

    private static void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is ScrollViewer scrollViewer)
        {
            // Scroll to end on layout update (ensures items are rendered)
            void OnLayoutUpdated(object? s, EventArgs args)
            {
                scrollViewer.ScrollToEnd();
                scrollViewer.LayoutUpdated -= OnLayoutUpdated;
            }

            scrollViewer.LayoutUpdated += OnLayoutUpdated;

            // Find the ItemsControl inside the ScrollViewer
            if (scrollViewer.Content is ItemsControl itemsControl)
            {
                if (itemsControl.ItemsSource is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged += (s, args) =>
                    {
                        if (args.Action == NotifyCollectionChangedAction.Add)
                        {
                            // Scroll to bottom when items are added
                            scrollViewer.ScrollToEnd();
                        }
                    };
                }
            }
        }
    }
}
