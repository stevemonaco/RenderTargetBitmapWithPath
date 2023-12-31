﻿using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;

namespace RenderTargetBitmapWithPath.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public Cursor OriginalPathCursor { get; }
    public Cursor OriginalPathTooSmallCursor { get; }
    public Cursor OriginalPathBarelyShowingCursor { get; }
    public Cursor ManuallyScaledPathCursor { get; }
    public Cursor PathIconPathCursor { get; }
    public Cursor ViewboxPathCursor { get; }

    public MainWindowViewModel()
    {
        int length = 32;

        // Build cursor from Path obtained from IconPacks
        var originalPath = new Path
        {
            Data = StreamGeometry.Parse("M256 124l68 68l30 -30l-98 -98l-98 98l30 30zM256 388l-68 -68l-30 30l98 98l98 -98l-30 -30z"),
            StrokeThickness = 2,
            Fill = Brushes.Orange,
            Stroke = Brushes.Red,
        };
        OriginalPathCursor = VisualCursorFactory.Create(originalPath, new Avalonia.PixelSize(512, 512), new Avalonia.PixelPoint(256, 256), "cursororiginal.png");

        // Try to shrink the original Path by rendering to a smaller RenderTargetBitmap
        OriginalPathBarelyShowingCursor = VisualCursorFactory.Create(originalPath, new Avalonia.PixelSize(180, 150), new Avalonia.PixelPoint(90, 75), "cursororiginalbarelyshowing.png");

        // Try to shrink the original Path by rendering to a smaller RenderTargetBitmap
        OriginalPathTooSmallCursor = VisualCursorFactory.Create(originalPath, new Avalonia.PixelSize(length, length), new Avalonia.PixelPoint(length / 2, length / 2), "cursororiginaltoosmall.png");

        // Manually shrunk in Inkscape, contains some modifications like moving the arrows closer together
        var manuallyScaledPath = new Path
        {
            Data = StreamGeometry.Parse("M 8.4666667,5.3001399 14.341494,11.306953 16.933333,8.6568826 8.4666667,0 0,8.6568826 2.5918254,11.306953 Z m 0,15.9122451 L 2.5918254,15.205573 0,17.855641 8.4666667,26.512525 16.933333,17.855641 14.341494,15.205573 Z"),
            StrokeThickness = 2,
            Fill = Brushes.Orange,
            Stroke = Brushes.Red
        };

        ManuallyScaledPathCursor = VisualCursorFactory.Create(manuallyScaledPath, new Avalonia.PixelSize(length, length), new Avalonia.PixelPoint(length / 2, length / 2), "cursormanuallyscaled.png");

        // Try to put the original Path into a PathIcon and force layout
        length = 32;
        var pathIcon = new PathIcon
        {
            Width = length,
            Height = length,
            Data = StreamGeometry.Parse("M256 124l68 68l30 -30l-98 -98l-98 98l30 30zM256 388l-68 -68l-30 30l98 98l98 -98l-30 -30z"),
            Foreground = Brushes.Orange,
            Background = Brushes.Red,
        };

        PathIconPathCursor = VisualCursorFactory.Create(pathIcon, new Avalonia.PixelSize(length, length), new Avalonia.PixelPoint(length / 2, length / 2), "cursorpathicon.png");

        length = 32;
        var vb = new Viewbox
        {
            Width = length,
            Height = length,
            Child = originalPath,
        };

        vb.Measure(new(length, length));
        vb.Arrange(new Avalonia.Rect(0, 0, length, length));

        ViewboxPathCursor = VisualCursorFactory.Create(vb, new Avalonia.PixelSize(length, length), new Avalonia.PixelPoint(length / 2, length / 2), "cursorviewbox.png");
    }
}
