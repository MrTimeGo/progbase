using System;
using static System.Console;
using System.Threading;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Clear();
            Write("Type a number of Part you want to choose: ");
            string choise = ReadLine();
            if (choise == "1")
            {
                int[] massive1 = new int[11];
                for (int i = 0; i < 11; i++)
                {
                    Write("{0} element: ", i+1);
                    massive1[i] = int.Parse(ReadLine());
                }
                Clear();
                WriteLine("Entered levels:");
                for (int i = 0; i < 11; i++)
                {
                    Write(massive1[i]);
                    if (i == 10)
                    {
                        WriteLine(".");
                    }
                    else
                    {
                        Write(", ");
                    }
                }
                int max = int.MinValue;
                int min = int.MaxValue;

                for (int i = 0; i < 11; i++)
                {
                    if (max < massive1[i])
                    {
                        max = massive1[i];
                    }
                    if (min > massive1[i])
                    {
                        min = massive1[i];
                    }
                }
                
                int difference = -min;
                max = difference + max;
                min = difference + min;
                int[] massive2 = new int[11];

                //a
                WriteLine("Moved levels: ");
                for (int i = 0; i < 11; i++)
                {
                    massive2[i] = massive1[i] + difference;
                    Write(massive2[i]);
                    if (i == 10)
                    {
                        WriteLine(".");
                    }
                    else
                    {
                        Write(", ");
                    }
                }

                //b
                double[] massive3 = new double[11];

                WriteLine("Normalized levels:");
                for (int i = 0; i < 11; i++)
                {
                    massive3[i] = massive2[i]/((double)max);
                    Write(massive3[i]);
                    if (i == 10)
                    {
                        WriteLine(".");
                    }
                    else
                    {
                        Write(", ");
                    }
                }
                WriteLine();
                //c
                bool breaker = false;
                int water_level = 0;
                while (breaker == false)
                {
                    Write("Enter water level: ");
                    water_level = int.Parse(ReadLine());
                    if (water_level >= 0 && water_level <= max)
                    {
                        breaker = true;
                    }
                    else
                    {
                        WriteLine("You entered wrong water level, try again");
                    }
                }
                
                int[] massive4 = new int[11];
                //int max_above_water = int.MinValue;
                WriteLine("Water levels:");
                for (int i = 0; i < 11; i++)
                {
                    if (massive2[i] >= water_level)
                    {
                        massive4[i] = 0;
                    }
                    else
                    {
                        massive4[i] = water_level - massive2[i];
                    }
                    Write(massive4[i]);
                    if (i == 10)
                    {
                        WriteLine(".");
                    }
                    else
                    {
                        Write(", ");
                    }
                }
                WriteLine();
                int volume = max - water_level;
                WriteLine("Volume of the heighest mountain above the water is: " + volume);
                WriteLine();

                int[,] graphics = Make_2D_massive(max, water_level, massive2, massive4);

                DrawDiagram(max, graphics, water_level);
            }
            else if (choise == "2")
            {
                //Part2 under construction
            }
        }
        static int[,] Make_2D_massive(int max, int water_level, int[] massive2, int[] massive4)
        {
            int[,] graphics = new int[max, 11];
            for (int j = 0; j < 11; j++)
            {
                for (int i = 0; i < massive2[j]; i++)
                {
                    graphics[i, j] = 1;
                }
                for (int i = 0; i < water_level; i++)
                {
                    if (graphics[i,j] == 0)
                    {
                        graphics[i,j] = 2;
                    }
                }
            }
            return graphics;
        }
        static void DrawDiagram(int max, int[,] graphics, int water_level)
        {
            int top = CursorTop;
            WriteLine("-------------");
            for (int i = 1; i <= max; i++)
            {
                SetCursorPosition(0, top+i);
                Write("|");
                SetCursorPosition(12, top+i);
                Write("|");
                SetCursorPosition(13, top+i);
                Write(max+1-i);
                if (max+1-i == water_level)
                {
                    Write(" (water level)");
                }
            }
            WriteLine();
            WriteLine("-------------");
            WriteLine();

            for (int j = 0; j < 11; j++)
            {
                for(int i = 0; i < max; i++)
                {
                    SetCursorPosition(j+1, top+1+i);
                    if (graphics[max-1-i,j] == 1)
                    {
                        Write("N");
                    }
                    else if(graphics[max-1-i,j] == 2)
                    {
                        Write("~");
                    }
                    else
                    {
                        Write(" ");
                    }
                    Thread.Sleep(200);
                }
            }
            WriteLine();
            WriteLine();
        }
    }
}
