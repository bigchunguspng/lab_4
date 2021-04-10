using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Task__3
{
    internal class Program
    {
        private static int countF = 93, countP = 200;
        
        private static long[] fibs;
        private static int[] prims;

        private static string path1 = @"c:\temp\fibs.txt";
        private static string path2 = @"c:\temp\prims.txt";

        private static int _count = 0;
        

        public static void Main(string[] args)
        {
            Thread t1 = new Thread(GetFibonacciNumbers);
            Thread t2 = new Thread(GetPrimeNumbers);
            t1.Start();
            t2.Start();
        }

        private static void GetFibonacciNumbers()
        {
            fibs = new long[countF];

            fibs[0] = 0;
            fibs[1] = 1;
            for (int i = 2; i < countF; i++)
            {
                fibs[i] = fibs[i - 1] + fibs[i - 2];
            }
            
            SaveToFile(path1, fibs);
        }

        private static void GetPrimeNumbers()
        {
            List<int> primaries = new List<int>(countP);
            

            for (int i = 2; primaries.Count < countP; i++)
                if (IsPrimary(i))
                    primaries.Add(i);

            prims = primaries.ToArray();
            
            SaveToFile(path2, prims.Select(x=>(long)x).ToArray());
        }

        private static bool IsPrimary(int x)
        {
            if (x < 2)
                return false;

            int upper = x / 2;
            for (int i = 2; i <= upper; i++)
                if (x % i == 0)
                    return false;

            return true;
        }

        static void SaveToFile(string path, long[] array)
        {
            string[] nums = array.Select(x => x.ToString()).ToArray();
            
            if (File.Exists(path)) File.Delete(path);

            File.WriteAllLines(path, nums);

            CheckCount();
        }

        private static void CheckCount()
        {
            _count++;
            if (_count == 2)
            {
                ReadData(path1, "Fibbonacci");
                ReadData(path2, "Prime");
            }
        }

        static void ReadData(string path, string message)
        {
            string[] output = File.ReadAllLines(path);
            Console.WriteLine("Amount of " + message + " numbers: " + output.Length);
            foreach (string s in output)
            {
                Console.WriteLine(s);
            }
        }
    }
}