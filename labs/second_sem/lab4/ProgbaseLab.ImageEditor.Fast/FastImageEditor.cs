using System;
using System.Drawing;
using System.Drawing.Imaging;
using ProgbaseLab.ImageEditor.Common;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ProgbaseLab.ImageEditor.Fast
{
    public class FastImageEditor : IImageEditor
    {
        public FastImageEditor()
        {

        }
        public Bitmap ChangeBrightness(Bitmap bmp, int brightnessValue)
        {
            Bitmap newBitmap = new Bitmap(bmp.Width, bmp.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            ColorMatrix colorMatrix = new ColorMatrix(CreateBrightnessMatrix(brightnessValue));

            ImageAttributes attributes = new ImageAttributes();

            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes);

            attributes.Dispose();
            g.Dispose();
            return newBitmap;
        }
        private float[][] CreateBrightnessMatrix(int brightness)
        {
            float scale = (float)(brightness / 100.0);  // 0..1
            float darkness = 1;  // 0..1
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

        public Bitmap Crop(Bitmap bmp, int left, int top, int width, int height)
        {
            Mat source = BitmapConverter.ToMat(bmp);
            Rect rectCrop = new Rect(left, top, width, height);

            Mat cropedImage = new Mat(source, rectCrop);
            return BitmapConverter.ToBitmap(cropedImage);
        }

        public Bitmap Grayscale(Bitmap bmp)
        {
            Mat source = BitmapConverter.ToMat(bmp);
            Mat result = new Mat();

            Cv2.CvtColor(source, result, ColorConversionCodes.RGB2GRAY);

            return BitmapConverter.ToBitmap(result);
        }

        public Bitmap RemoveRed(Bitmap bmp)
        {
            Mat source = BitmapConverter.ToMat(bmp);
            Mat[] channels = Cv2.Split(source);
            channels[2].SetTo(0);
            Mat result = new Mat();
            Cv2.Merge(channels, result);
            return BitmapConverter.ToBitmap(result);
        }

        public Bitmap Rotate180(Bitmap scr)
        {
            Mat source = BitmapConverter.ToMat(scr);
            Mat result = new Mat();

            Point2f center = new Point2f(source.Width / 2, source.Height / 2);
            Mat matrix = Cv2.GetRotationMatrix2D(center, 180, 1);
            Cv2.WarpAffine(source, result, matrix, source.Size());
            return BitmapConverter.ToBitmap(result);
        }
    }
}
