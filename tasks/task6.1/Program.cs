using System;
using static System.Console;

namespace task6._1
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Enter string:");
            string str1 = ReadLine();
            int[] a_code = FromStrToInt(str1);
            PrintArray(a_code);
            
            WriteLine();
            WriteLine("-----------");
            WriteLine();

            int[] nums = ReadArray();
            string str2 = FromIntToStr(nums);
            WriteLine(str2);
        }
        static int[] FromStrToInt(string str)
        {
            int[] nums = new int[str.Length];
            for(int i = 0; i < str.Length; i++)
            {
                nums[i] = Convert.ToInt32(str[i]);
            }
            return nums;
        }
        static string FromIntToStr(int[] nums)
        {
            char[] characters = new char[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                characters[i] = Convert.ToChar(nums[i]);
            }
            string str = new string(characters);
            return str;
        }
        static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Write(array[i] + " ");
            }
            WriteLine();
        }
        static int[] ReadArray()
        {
            WriteLine("Enter lenth:");
            int lenth = int.Parse(ReadLine());
            int[] array = new int[lenth];
            for (int i = 0; i < lenth; i++)
            {
                Write(i + ": ");
                array[i] = int.Parse(ReadLine());
            }
            return array;
        }
    }
}
