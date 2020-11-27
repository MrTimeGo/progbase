using System;
using static System.Math;
using static System.Console;
using Progbase.Procedural;

namespace lab4
{
    class Program
    {
        struct Point
        {
            public int x;
            public int y;
        }
        struct Circle
        {
            public Point center;
            public int radius;
        }
        struct Ray
        {
            public double angle;
            public Point start;
        }
        static void Main(string[] args)
        {
            const int size = 100;
            Clear();

            int cx = size/2;
            int cy = size/2;
            
            int R1 = 5;
            int R2 = 10;
            
            int alpha = 120;
            int beta = 60;
            int gamma = 30;

            Clear();
            Canvas.SetOrigin(1,1);
            Canvas.SetSize(size, size);

            Canvas.InvertYOrientation();

            bool exit = false;
            do {
                Point C = new Point {x = cx, y = cy};

                Circle circle1 = new Circle {center = C, radius = R1};
                Circle circle2 = new Circle {center = C, radius = R2};

                Ray A = new Ray {angle = ConvertDegToRad(alpha), start = C};
                Ray G = new Ray {angle = ConvertDegToRad(gamma), start = C};

                Point D = new Point {x = (int)(circle1.center.x + circle1.radius * Cos(G.angle)), y = (int)(circle1.center.y + circle1.radius * Sin(G.angle))};

                Ray A1 = new Ray {angle = A.angle - ConvertDegToRad(beta)/2.0, start = A.start};
                Ray A2 = new Ray {angle = A.angle + ConvertDegToRad(beta)/2.0, start = A.start};

                Point D1 = new Point {x = (int)(circle2.center.x + circle2.radius * Cos(A1.angle)), y = (int)(circle2.center.y + circle2.radius * Sin(A1.angle))};
                Point D2 = new Point {x = (int)(circle2.center.x + circle2.radius * Cos(A2.angle)), y = (int)(circle2.center.y + circle2.radius * Sin(A2.angle))};

                DrawObjects(C, D, D1, D2, circle1, circle2);
                
                ConsoleKeyInfo keyInfo = ReadKey();
                if (keyInfo.Key == ConsoleKey.W && circle2.radius + cy < size) cy++;
                else if (keyInfo.Key == ConsoleKey.S && cy - circle2.radius > 0) cy--;
                else if (keyInfo.Key == ConsoleKey.A && cx - circle2.radius > 0) cx--;
                else if (keyInfo.Key == ConsoleKey.D && circle2.radius + cx < size) cx++;
                else if (keyInfo.Key == ConsoleKey.UpArrow) alpha++;
                else if (keyInfo.Key == ConsoleKey.DownArrow) alpha--; 
                else if (keyInfo.Key == ConsoleKey.LeftArrow) gamma++;
                else if (keyInfo.Key == ConsoleKey.RightArrow) gamma--;
                else if (keyInfo.Key == ConsoleKey.T && beta > 0) beta--;
                else if (keyInfo.Key == ConsoleKey.Y && beta <= 120) beta++;
                else if (keyInfo.Key == ConsoleKey.G && R1 > 0) R1--;
                else if (keyInfo.Key == ConsoleKey.H && R1 < R2) R1++;
                else if (keyInfo.Key == ConsoleKey.B && R2 > R1) R2--;
                else if (keyInfo.Key == ConsoleKey.N && cy + R2 + 1 < size && cx + R2 + 1 < size && cy - R2 - 1 > 0 && cx - R2 - 1 > 0) R2++;
                else if (keyInfo.Key == ConsoleKey.Escape) exit = true;
            } while (exit == false);
        }
        static void DrawObjects(Point C, Point D, Point D1, Point D2, Circle circle1, Circle circle2)
        {
            Canvas.BeginDraw();

            Canvas.SetColor("#1E90FF");
            Canvas.PutPixel(C.x, C.y);

            Canvas.SetColor("#008000");
            Canvas.StrokeLine(D1.x, D1.y, D2.x, D2.y);

            Canvas.SetColor("#0000FF");
            Canvas.StrokeLine(D1.x, D1.y, D.x, D.y);
            Canvas.StrokeLine(D2.x, D2.y, D.x, D.y);


            Canvas.EndDraw();
        }
        static double ConvertDegToRad(int degree)
        {
            double rad = PI * degree / 180.0;
            return rad;
        }
    }
}