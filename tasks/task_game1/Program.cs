using System;
using static System.Console;
using System.Threading;
using static System.Random;


namespace task_game1
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter username: ");
            string username = ReadLine();

            Random rand = new Random();
            int counter = 0, max_counter = -1;
            int x = rand.Next(1, 200), y = rand.Next(1, 50);
            int user_x = rand.Next(1, 200), user_y = rand.Next(1, 50);y = rand.Next(1, 50);
            do {
                x = rand.Next(1, 200);
                y = rand.Next(1, 50);
            } while(x == user_x || y == user_y);
            int height = 50, width = 200;
            CursorVisible = true;
            int vector = 1;
            while (true)
            {
                CursorVisible = false;
                Clear();
                
                ForegroundColor = ConsoleColor.Green;
                DrawRectangle(width, height);
                DrawScoreboard(width, height, username, counter, max_counter);

                ForegroundColor = ConsoleColor.Red;
                DrawBall(x, y, "Enemy");
                // changing vectors
                vector = ChangeVector(vector, x, y, width, height);

                // running
                x = xChange(vector, x);
                y = yChange(vector, y);
                //user ball
                ForegroundColor = ConsoleColor.Blue;
                DrawBall(user_x, user_y, username);
                ConsoleKeyInfo keyInfo = ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (user_y - 1 != 0)
                    {
                        user_y -= 1;
                    }
                }
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (user_y + 1 != height)
                    {
                        user_y += 1;
                    }
                }
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (user_x + 1 != width)
                    {
                        user_x += 1;
                    }
                }
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (user_x - 1 != 0)
                    {
                        user_x -= 1;
                    }
                }
                Thread.Sleep(100);
                if (x == user_x && y == user_y)
                {
                    SetCursorPosition(width/2, height/2);
                    Write("You win!");
                    if (counter < max_counter || max_counter == -1)
                    {
                        max_counter = counter; 
                    }
                    counter = 0;
                    Thread.Sleep(5000);
                }
                counter ++;
            }
        }


        static void DrawBall(int x, int y, string username)
        {
            SetCursorPosition(x, y);
            Write("@");
            SetCursorPosition(x, y-1);
            Write(username);
        }


        static void DrawRectangle(int left, int top)
        {
            SetCursorPosition(1, 1);
            for (int i = 0; i <= left; i += 2)
            {
                SetCursorPosition(i, 0);
                Write("+  ");
                SetCursorPosition(i, top);
                Write("+  ");
            }
            for (int i = 0; i < top; i ++)
            {
                SetCursorPosition(0, i);
                Write("+");
                SetCursorPosition(left, i);
                Write("+");
            }
        }

        static void DrawScoreboard(int width, int height, string username, int counter, int max_counter)
        {
            for (int i = height; i <= height + 4; i++)
            {
                SetCursorPosition(0, i);
                Write("+");
                SetCursorPosition(width, i);
                Write("+");
                SetCursorPosition(width/2, i);
                Write("+");
            }
            for (int i = 0; i < width; i += 2)
            {
                SetCursorPosition(i, height + 4);
                Write("+ ");
            }
            SetCursorPosition(4, height +2);
            Write("Points: " + counter);
            SetCursorPosition(4, height + 3);
            Write("Player: " + username);
            SetCursorPosition(width/2 + 4, height + 2);
            Write("Record: " + max_counter);
        }

        static int ChangeVector(int vector, int x, int y, int width, int height)
        {
            if (x + 1 == width && (vector == 1 || vector == 2))
            {
                if (vector == 1)
                {
                    return 4;
                }
                else
                {
                    return 3;
                }
            }
            else if (x - 1 == 0 && (vector == 3 || vector == 4))
            {
                if (vector == 3)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            if (y + 1 == height && (vector == 1 || vector == 4))
            {
                if (vector == 1)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else if (y - 1 == 0 && (vector == 2 || vector == 3))
            {
                if(vector == 2)
                {
                    return 1;
                }
                else
                {
                    return 4;
                }
            }
            return vector;
        }

        static int xChange(int vector, int x)
        {
            if (vector == 1 || vector == 2)
            {
                return x + 1;
            }
            else
            {
                return x - 1;
            }
        }

        static int yChange(int vector, int y)
        {
            if (vector == 1 || vector == 4)
            {
                return y + 1;
            }
            else
            {
                return y - 1;
            }
        }
    }
}
