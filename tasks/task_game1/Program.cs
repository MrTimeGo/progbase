using System;
using static System.Console;
using System.Threading;


namespace task_game1
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter username: ");
            string username = ReadLine();
            int counter = 0, max_counter = -1;
            int x = 6, y = 2;
            int user_x = 4, user_y = 5;
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
                if (x + 1 == width && (vector == 1 || vector == 2))
                {
                    if (vector == 1)
                    {
                        vector = 4;
                    }
                    else
                    {
                        vector = 3;
                    }
                }
                else if (x - 1 == 0 && (vector == 3 || vector == 4))
                {
                    if (vector == 3)
                    {
                        vector = 2;
                    }
                    else
                    {
                        vector = 1;
                    }
                }
                if (y + 1 == height && (vector == 1 || vector == 4))
                {
                    if (vector == 1)
                    {
                        vector = 2;
                    }
                    else
                    {
                        vector = 3;
                    }
                }
                else if (y - 1 == 0 && (vector == 2 || vector == 3))
                {
                    if(vector == 2)
                    {
                        vector = 1;
                    }
                    else
                    {
                        vector = 4;
                    }
                }

                // running
                if (vector == 1)
                {
                    x += 1;
                    y += 1;
                }
                else if (vector == 2)
                {
                    x += 1;
                    y -= 1; 
                }
                else if (vector == 3)
                {
                    x -= 1;
                    y -= 1;
                }
                else
                {
                    x -= 1;
                    y += 1;
                }


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
    }
}
