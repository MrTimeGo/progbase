using System;
using System.Drawing;
using ProgbaseLab.ImageEditor.Pixel;
using ProgbaseLab.ImageEditor.Fast;
using ProgbaseLab.ImageEditor.Common;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ArgumentProcessor.Run(args);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    static class ArgumentProcessor
    {
        public static void Run(string[] args)
        {
            ProgramArguments programArguments = ParseArguments(args);

            switch (programArguments.operation)
            {
                case "crop":
                {
                    ProcessCrop(programArguments);
                    break;
                }
                case "rotate180":
                {
                    ProcessRotate180(programArguments);
                    break;
                }
                case "removeRed":
                {
                    ProcessRemoveRed(programArguments);
                    break;
                }
                case "grayscale":
                {
                    ProcessGrayscale(programArguments);
                    break;
                }
                case "changeBrightness":
                {
                    ProcessChangeBrighness(programArguments);
                    break;
                }
            }
        }
        private static void ProcessCrop(ProgramArguments args)
        {
            if (args.otherArguments.Length != 1)
            {
                throw new ArgumentException("Invalid number of parameters in crop command");
            }

            IImageEditor editor = ChooseEditor(args.module);

            Rectangle rectangle = ParseRectangle(args.otherArguments[0]);

            Stopwatch stopwatch = new Stopwatch();

            Bitmap bmp = new Bitmap(args.inputFile);

            stopwatch.Start();
            Bitmap result = editor.Crop(bmp, rectangle.left, rectangle.top, rectangle.width, rectangle.height);
            stopwatch.Stop();

            Console.WriteLine($"Operation {args.operation} done in {stopwatch.ElapsedMilliseconds} ms");
            result.Save(args.outputFile);
        }
        struct Rectangle
        {
            public int width;
            public int height;
            public int left;
            public int top;
        }
        private static Rectangle ParseRectangle(string source)
        {
            string[] widthAndOther = source.Split('x');
            if (widthAndOther.Length != 2)
            {
                throw new ArgumentException("Invalid rectangle format. Should be: \"{width}x{height}+{left}+{top}\"");
            }
            if (!int.TryParse(widthAndOther[0], out int width))
            {
                throw new ArgumentException("Width should be integer");
            }
            string[] heightAndPosition = widthAndOther[1].Split("+");
            if (heightAndPosition.Length != 3)
            {
                throw new ArgumentException("Invalid rectangle format. Should be: \"{width}x{height}+{left}+{top}\"");
            }
            if (!int.TryParse(heightAndPosition[0], out int height))
            {
                throw new ArgumentException("Height should be integer");
            }
            if (!int.TryParse(heightAndPosition[1], out int left))
            {
                throw new ArgumentException("Left cordinate should be integer");
            }
            if (!int.TryParse(heightAndPosition[2], out int top))
            {
                throw new ArgumentException("Top cordinate should be integer");
            }
            return new Rectangle()
            {
                width = width,
                height = height,
                left = left,
                top = top
            };
        }
        private static void ProcessRotate180(ProgramArguments args)
        {
            if (args.otherArguments.Length != 0)
            {
                throw new ArgumentException($"Operation should not have any arguments. Got: {args.otherArguments.Length}");
            }

            IImageEditor editor = ChooseEditor(args.module);

            Stopwatch stopwatch = new Stopwatch();

            Bitmap bmp = new Bitmap(args.inputFile);

            stopwatch.Start();
            Bitmap result = editor.Rotate180(bmp);
            stopwatch.Stop();

            Console.WriteLine($"Operation {args.operation} done in {stopwatch.ElapsedMilliseconds} ms");
            result.Save(args.outputFile);
        }
        private static void ProcessRemoveRed(ProgramArguments args)
        {
            if (args.otherArguments.Length != 0)
            {
                throw new ArgumentException($"Operation should not have any arguments. Got: {args.otherArguments.Length}");
            }

            IImageEditor editor = ChooseEditor(args.module);

            Stopwatch stopwatch = new Stopwatch();

            Bitmap bmp = new Bitmap(args.inputFile);

            stopwatch.Start();
            Bitmap result = editor.RemoveRed(bmp);
            stopwatch.Stop();

            Console.WriteLine($"Operation {args.operation} done in {stopwatch.ElapsedMilliseconds} ms");
            result.Save(args.outputFile);
        }
        private static void ProcessGrayscale(ProgramArguments args)
        {
            if (args.otherArguments.Length != 0)
            {
                throw new ArgumentException($"Operation should not have any arguments. Got: {args.otherArguments.Length}");
            }

            IImageEditor editor = ChooseEditor(args.module);

            Stopwatch stopwatch = new Stopwatch();

            Bitmap bmp = new Bitmap(args.inputFile);

            stopwatch.Start();
            Bitmap result = editor.Grayscale(bmp);
            stopwatch.Stop();

            Console.WriteLine($"Operation {args.operation} done in {stopwatch.ElapsedMilliseconds} ms");
            result.Save(args.outputFile);
        }
        private static void ProcessChangeBrighness(ProgramArguments args)
        {
            if (args.otherArguments.Length != 1)
            {
                throw new ArgumentException($"Operation should  have 1 argument. Got: {args.otherArguments.Length}");
            }
            int brightnessValue = ParseBrightness(args.otherArguments[0]);

            IImageEditor editor = ChooseEditor(args.module);

            Stopwatch stopwatch = new Stopwatch();

            Bitmap bmp = new Bitmap(args.inputFile);

            stopwatch.Start();
            Bitmap result = editor.ChangeBrightness(bmp, brightnessValue);
            stopwatch.Stop();

            Console.WriteLine($"Operation {args.operation} done in {stopwatch.ElapsedMilliseconds} ms");
            result.Save(args.outputFile);
        }
        private static int ParseBrightness(string source)
        {
            if (!int.TryParse(source, out int brightnessValue))
            {
                throw new ArgumentException("Brightness value must be integer");
            }
            if (brightnessValue < -100 || brightnessValue > 100)
            {
                throw new ArgumentException($"Brightness value should be in range [-100; 100]. Got {brightnessValue}");
            }
            return brightnessValue;
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

            ValidateModule(args[0]);
            string module = args[0];

            ValidateInputFile(args[1]);
            string inputFile = args[1];

            string outputFile = args[2];

            ValidateOperation(args[3]);
            string operation = args[3];

            string[] otherArguments = new string[args.Length - 4];
            for (int i = 0; i < otherArguments.Length; i++)
            {
                otherArguments[i] = args[i + 4];
            }
            ProgramArguments programArguments = new ProgramArguments
            {
                module = module,
                inputFile = inputFile,
                outputFile = outputFile,
                operation = operation,
                otherArguments = otherArguments
            };
            return programArguments;
        }
        private static void ValidateArgumentLength(int length)
        {
            if (length < 4)
            {
                throw new ArgumentException($"Number of arguments must be more than 3. Number of entered arguments: {length}");
            }
        }
        private static void ValidateModule(string module)
        {
            string[] modules = new string[] { "pixel", "fast" };
            for (int i = 0; i < module.Length; i++)
            {
                if (modules[i] == module)
                {
                    return;
                }
            }
            throw new ArgumentException($"There is no such module: {module}");
        }
        private static void ValidateInputFile(string inputFile)
        {
            if (!System.IO.File.Exists(inputFile))
            {
                throw new ArgumentException($"Such file does not exist: {inputFile}");
            }
        }
        private static void ValidateOperation(string operation)
        {
            string[] operations = new string[] { "crop", "rotate180", "removeRed", "grayscale", "changeBrightness"};
            for (int i = 0; i < operations.Length; i++)
            {
                if (operations[i] == operation)
                {
                    return;
                }
            }
            throw new ArgumentException($"There is no such operation: {operation}");
        }
        private static IImageEditor ChooseEditor(string module)
        {
            return module == "pixel" ? new PixelImageEditor() : new FastImageEditor(); 
        }
    }
}
