using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    static class DataProcessor
    {
        public static int[] GenNum(int n, int a, int b)
        {
            int[] array = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next(a, b + 1);
            }
            return array;
        }
        public static int[] ReadUnique(int[] inputArray)
        {
            SortedSet<int> set = new SortedSet<int>();
            for (int i = 0; i < inputArray.Length; i++)
            {
                set.Add(inputArray[i]);
            }
            int[] outputArray = new int[set.Count];
            set.CopyTo(outputArray);
            return outputArray;
        }
        public static Vector[] GenVec(int a, int b, int n)
        {
            Vector[] array = new Vector[n];
            Random rnd = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Vector
                {
                    x = rnd.Next(a, b + 1),
                    y = rnd.Next(a, b + 1),
                };
            }
            return array;
        }
        public static double GetAverageOfNegative(int[] array)
        {
            double average = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    average += array[i];
                }
            }
            return average / array.Length;
        }
        public static int NumberOfSpecVec(Vector[] array)
        {
            int counter = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (IsPrime(Math.Abs(array[i].x)) || IsPrime(Math.Abs(array[i].y)))
                {
                    counter++;
                }
            }
            return counter;
        }
        private static bool IsPrime(int num)
        {   
            if (num > 1)
            {
                for (int i = 2; i <= Math.Sqrt(num); i++)
                {
                    if (num % i == 0)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
