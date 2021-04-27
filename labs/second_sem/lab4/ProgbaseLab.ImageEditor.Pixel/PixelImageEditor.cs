using System;
using System.Drawing;
using ProgbaseLab.ImageEditor.Common;

namespace ProgbaseLab.ImageEditor.Pixel
{
    public class PixelImageEditor : IImageEditor
    {
        public PixelImageEditor()
        {

        }
        public Bitmap Crop(Bitmap bmp, int left, int top, int width, int height)
        {
            Rectangle rect = new Rectangle(left, top, width, height);
            ValidateImageSize(bmp, rect);

            Bitmap destination = new Bitmap(width, height);
            for (int x = 0; x < rect.Width; x++)
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    Color color = bmp.GetPixel(x + rect.Left, y + rect.Top);
                    destination.SetPixel(x, y, color);
                }
            }
            return destination;
        }
        private void ValidateImageSize(Bitmap bmp, Rectangle rect)
        {
            if (rect.Top < 0 || rect.Bottom > bmp.Height || rect.Left < 0 || rect.Right > bmp.Width)
            {
                throw new Exception("Crop options is out of image bounds");
            }
        }
        public Bitmap Rotate180(Bitmap scr)
        {
            Bitmap targetBitmap = new Bitmap(scr.Width, scr.Height);
            for (int y = 0; y < targetBitmap.Height; y++)
            {
                for (int x = 0; x < targetBitmap.Width; x++)
                {
                    Color color = scr.GetPixel(x, y);
                    targetBitmap.SetPixel(scr.Width - x - 1, scr.Height - y - 1, color);
                }
            }
            return targetBitmap;
        }
        public Bitmap RemoveRed(Bitmap bmp)
        {
            Bitmap targetBitmap = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < targetBitmap.Height; y++)
            {
                for (int x = 0; x < targetBitmap.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    Color newColor = Color.FromArgb(255, 0, color.G, color.B);
                    targetBitmap.SetPixel(x, y, newColor);
                }
            }
            return targetBitmap;
        }
        public Bitmap Grayscale(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    int yLinear = (int)(0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B);
                    Color newColor = Color.FromArgb(255, yLinear, yLinear, yLinear);
                    bmp.SetPixel(x, y, newColor);
                }
            }
            return bmp;
        }
        public Bitmap ChangeBrightness(Bitmap bmp, int brightnessValue)
        {
            Bitmap targeBmp = new Bitmap(bmp.Width, bmp.Height);
            float[][] matrix = CreateBrightnessMatrix(brightnessValue);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color oldColor = bmp.GetPixel(x, y);
                    Color newColor = ApplyFilter(oldColor, matrix);
                    targeBmp.SetPixel(x, y, newColor);
                }
            }
            return targeBmp;
        }
        private Color ApplyFilter(Color oldColor, float[][] filter)
        {
            int red = (int)(oldColor.R * filter[0][0] + oldColor.G * filter[1][0] + oldColor.B * filter[2][0] + 255 * filter[4][0]);
            int green = (int)(oldColor.R * filter[0][1] + oldColor.G * filter[1][1] + oldColor.B * filter[2][1] + 255 * filter[4][1]);
            int blue = (int)(oldColor.R * filter[0][2] + oldColor.G * filter[1][2] + oldColor.B * filter[2][2] + 255 * filter[4][2]);

            int r = Math.Min(Math.Max(red, 0), 255);
            int g = Math.Min(Math.Max(green, 0), 255);
            int b = Math.Min(Math.Max(blue, 0), 255);

            return Color.FromArgb(255, r, g, b);
        }

        private float[][] CreateBrightnessMatrix(int brightness)
        {
            float scale = (float)(brightness / 100.0);
            float darkness = 1;
            if (scale < 0)
            {
                darkness = 1 + scale;
                scale = 0;
            }

            return new float[][]
            {
            new float[] {darkness, 0, 0, 0, 0},
            new float[] {0, darkness, 0, 0, 0},
            new float[] {0, 0, darkness, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {scale, scale, scale, 0, 1}
            };
        }
        
    }
}
