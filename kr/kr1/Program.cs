using System;
using static System.Console;
using static System.Math;

namespace kr1
{
    class Program
    {
        struct Point
        {
            public double x;
            public double y;
        }
        struct Circle
        {
            public double radius;
            public Point center;
        }
        static void Main(string[] args)
        {
            Write("Choose task: ");
            string choise = ReadLine();
            if (choise == "1")
            {
                WriteLine("Part 1");
                Write("Enter N: ");
                int N = int.Parse(ReadLine());
                if (N <= 100 && N > 1)
                {
                    int[] A = new int[N];
                    FillRandom(A);
                    int counter = 0;
                    for (int i = 0; i < A.Length; i++)
                    {
                        if (A[i] < 0 && A[i] % 2 == 0)
                        {
                            counter++;
                        }
                    }
                    WriteLine("Number of even negative elements: " + counter);
                    WriteLine("Part 2");
                    Write("Enter M: ");
                    int M = int.Parse(ReadLine());
                    if (M > 0 && N > M)
                    {
                        int[] B = new int[M];
                        for (int i = 0; i < B.Length; i++)
                        {
                            Write("Element {0}: ", i + 1);
                            B[i] = int.Parse(ReadLine());
                            if (B[i] >= 99 || B[i] <= -99)
                            {
                                WriteLine("Entered incorrect value");
                                return;
                            }
                        }
                        bool flag = false;
                        for (int i = 0; i < B.Length; i++)
                        {
                            for (int j = 0; j < A.Length; j++)
                            {
                                if (A[j] == B[i])
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                break;
                            }
                        }
                        if (flag)
                        {
                            WriteLine("One of elements of B contains in A");
                        }
                        else
                        {
                            WriteLine("No elements of B contains in A");
                        }

                    }
                    else
                    {
                        WriteLine("Error: M is out of range");
                    }
                }
                else
                {
                    WriteLine("Error: N is out of range");
                }
            }
            else if (choise == "2")
            {
                WriteLine("Part 1");
                WriteLine("Max divisor of {0} and {1}: {2}", 5, 10, MaxDivisor(5, 10));
                WriteLine("Max divisor of {0} and {1}: {2}", 54, 81, MaxDivisor(54, 81));
                WriteLine("Max divisor of {0} and {1}: {2}", 100, 101, MaxDivisor(100, 101));

                WriteLine("Part 2");
                Write("Enter N: ");
                int N = int.Parse(ReadLine());
                Write("Enter a: ");
                double a = double.Parse(ReadLine());
                Write("Enter b: ");
                double b = double.Parse(ReadLine());

                Point[] points = new Point[N];
                FillRandom(points, a, b);
                WritePoints(points);
                Circle circle = FindSmallestCircle(points);
                WriteLine("The smallest circle: radius - {0}, center - ({1}, {2})", Round(circle.radius, 4), Round(circle.center.x, 4), Round(circle.center.y, 4));
            }
        }
        static void FillRandom(int[] array)
        {
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(-99, 100);
            }
        }
        static void FillRandom(Point[] points, double a, double b)
        {
            Random rand = new Random();
            for (int i = 0; i < points.Length; i++)
            {
                points[i].x = rand.NextDouble() * (b - a) + a;
                points[i].y = rand.NextDouble() * (b - a) + a;
            }
        }
        static int MaxDivisor(int num1, int num2)
        {
            int min = Min(num1, num2);
            int max_divisor = 0;
            for (int i = min; i > 0; i--)
            {
                if (num1 % i == 0 && num2 % i == 0)
                {
                    max_divisor = i;
                    break;
                }
            }
            return max_divisor;
        }
        static void WritePoints(Point[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                WriteLine("Point {0}: ({1}, {2})", i+1, Round(points[i].x, 4), Round(points[i].y, 4));
            }
        }
        static Point FindCenter(Point[] points)
        {
            double sum_x = 0;
            double sum_y = 0;
            double counter = 0;
            for(int i = 0; i < points.Length; i++)
            {
                sum_x += points[i].x;
                sum_y += points[i].y;
                counter++;
            }
            Point center = new Point {x = sum_x/counter, y = sum_y/counter};
            return center;
        }
        static Circle FindSmallestCircle(Point[] points)
        {
            Point center = FindCenter(points);
            Point max = center;
            for (int i = 0; i < points.Length; i++)
            {
                Point point = points[i];
                double radius_max = Sqrt(Pow(max.x - center.x, 2) + Pow(max.y - center.y, 2));
                double point_radius = Sqrt(Pow(point.x - center.x, 2) + Pow(point.y - center.y, 2));
                if (radius_max < point_radius)
                {
                    max = point;
                }
            }
            double radius = Sqrt(Pow(max.x - center.x, 2) + Pow(max.y - center.y, 2));
            Circle circle = new Circle {center = center, radius = radius};
            return circle;
        }
    }
}
