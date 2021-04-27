using System.Drawing;

namespace ProgbaseLab.ImageEditor.Common
{
    public interface IImageEditor
    {
        Bitmap Crop(Bitmap bmp, int left, int top, int width, int height);
        Bitmap Rotate180(Bitmap scr);
        Bitmap RemoveRed(Bitmap bmp);
        Bitmap Grayscale(Bitmap bmp);
        Bitmap ChangeBrightness(Bitmap bmp, int brightnessValue);
    }
}
