using System;
using System.IO;
using System.Diagnostics;
using static System.Console;
using System.Drawing;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentProcessor.Run(args);
        }
    }
    static class ArgumentProcessor
    {
        private static void ValidateModule(string module)
        {
            string[] supportedModules = new string[] { "pixel", "fast" };
            for (int i = 0; i < supportedModules.Length; i++)
            {
                if (supportedModules[i] == module)
                {
                    return;
                }
            }
            throw new ArgumentException($"Not supported module: {module}");
        }
        private static void ValidateArgumentLength(int length)
        {
            if (length < 4)
            {
                throw new ArgumentException($"Not enough command line arguments. Expected more than 3, got {length}");
            }
        }
        private static void ValidateInputFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new ArgumentException($"File does not exist: {file}");
            }
        }
        private static void ValidateOperation(string operation)
        {
            string[] supportedOperations = new string[] { "crop", "rotate180" };
            for (int i = 0; i < supportedOperations.Length; i++)
            {
                if (supportedOperations[i] == operation)
                {
                    return;
                }
            }
            throw new ArgumentException($"Not supported module: {operation}");
        }
        public static Rectangle ParseRectangle(string rectFormat)
        {
            //TO DO
            return new Rectangle
            {
                Location = new Point(10, 45),
                Width = 200,
                Height = 100,
            };
        }
        struct ProgramArguments
        {
            public string module;
            public string inputFile;
            public string outputFile;
            public string operation;
            public string[] otherArguments;
        }
        private static ProgramArguments ParseArguments(string[] args)
        {
            ValidateArgumentLength(args.Length);

            string module = args[0];
            ValidateModule(module);

            string inputFile = args[1];
            ValidateInputFile(inputFile);
            string outputFile = args[2];
            string operation = args[3];
            ValidateOperation(operation);

            ProgramArguments programArguments = new ProgramArguments();
            programArguments.module = module;
            programArguments.inputFile = inputFile;
            programArguments.outputFile = outputFile;
            programArguments.operation = operation;
            string[] otherArguments = new string[args.Length - 4];
            for(int i =0; i < otherArguments.Length; i++)
            {
                otherArguments[i] = args[i + 4];
            }
            programArguments.otherArguments = otherArguments;
            return programArguments;
        }

        public static void Run(string[] args)
        {
            ProgramArguments programArguments = ParseArguments(args);
            switch (programArguments.operation)
            {
                case "crop":
                    {
                        ProcessCrop(args, new Bitmap(programArguments.inputFile), programArguments.outputFile);
                        break;
                    }
                case "rotate180":
                    {
                        ImageEditor.Rotate180(new Bitmap(programArguments.inputFile)).Save(programArguments.outputFile);
                        break;
                    }
            }
        }
        private static void ProcessCrop(string[] args, Bitmap inputBitmap, string outputFile)
        {
            if (args.Length != 5)
            {
                throw new ArgumentException();
            }
            string cropArguments = args[4];
            Rectangle cropRect = ParseRectangle(cropArguments);
            Stopwatch operationStopwatch = new Stopwatch();
            operationStopwatch.Start();
            Bitmap outputBitmap = ImageEditor.Crop(inputBitmap, cropRect);
            operationStopwatch.Stop();
            WriteLine($"Operation crop finished in {operationStopwatch.ElapsedMilliseconds} ms");
            outputBitmap.Save(outputFile);
        }
    }


    public static class ImageEditor
    {
        public static Bitmap Crop(Bitmap bmp, Rectangle rect)
        {
            ValidateCropRectagle(bmp, rect);
            Bitmap croppedImage = new Bitmap(rect.Width, rect.Height);

            for(int y = 0; y < croppedImage.Height; y++)
            {
                for (int x = 0; x< croppedImage.Width; x++)
                {
                    Color color = bmp.GetPixel(x + rect.Left, y + rect.Top);
                    croppedImage.SetPixel(x, y, color);
                }
            }
            return croppedImage;
        }
        private static void ValidateCropRectagle(Bitmap bmp, Rectangle rect)
        {
            if (bmp.Width < rect.Right)
            {
                throw new Exception();
            }
            if (bmp.Height <  rect.Bottom)
            {
                throw new Exception();
            }
            if (rect.Top < 0 || rect.Left < 0)
            {
                throw new Exception();
            }

        }


        public static Bitmap Rotate180(Bitmap scr)
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
    }

}
