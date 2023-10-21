using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;

namespace RenderTargetBitmapWithPath;
public static class VisualCursorFactory
{
    public static Cursor Create(Visual visual, PixelSize size, PixelPoint hotSpot, string? outputFileName = null)
    {
        using var rtb = new RenderTargetBitmap(size, new(96, 96));

        rtb.Render(visual);

        if (outputFileName is not null)
        {
            rtb.Save(outputFileName); // Only for testing
        }

        return new Cursor(rtb, hotSpot);
    }
}