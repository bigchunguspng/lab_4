using System;
using System.Threading;

namespace Task_5
{
    internal class Program
    {
        private static int[] _array1, _array2;

        private static int _count;

        private static readonly Random R = new Random();

        static readonly Mutex Mutex = new Mutex();

        public static void Main(string[] args)
        {
            _array1 = new int[28];
            _array2 = new int[28];

            PopulateArray(_array1);
            Array.Copy(_array1, _array2, 28);

            Thread t1 = new Thread(SortUp);
            Thread t2 = new Thread(SortDown);

            t1.Start();
            t2.Start();
        }


        static void SortUp()
        {
            int length = _array1.Length;
            for (int i = 1; i < length;)
            {
                Mutex.WaitOne();

                if (i == 0) i++;
                if (_array1[i - 1] <= _array1[i]) i++;
                else
                {
                    int temp = _array1[i];
                    _array1[i] = _array1[i - 1];
                    _array1[i - 1] = temp;
                    i--;
                }

                /*Illustrate(_array1);
                Console.WriteLine();*/

                TryIllustrateAll();
                Mutex.ReleaseMutex();
            }

            _count++;
        }

        static void SortDown()
        {
            int length = _array2.Length;
            for (int i = 1; i < length;)
            {
                Mutex.WaitOne();

                if (i == 0) i++;
                if (_array2[i - 1] >= _array2[i]) i++;
                else
                {
                    int temp = _array2[i];
                    _array2[i] = _array2[i - 1];
                    _array2[i - 1] = temp;
                    i--;
                }

                /*Illustrate(_array2);

                Thread.Sleep(33);
                Console.Clear();*/

                TryIllustrateAll();
                Mutex.ReleaseMutex();
            }

            _count++;
        }

        static void PopulateArray(int[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                array[i] = R.Next(length * 4);
                Thread.Sleep(50);
            }
        }

        static void Illustrate(int[] array)
        {
            foreach (int i in array)
            {
                Console.WriteLine($"[{"".PadRight(i, '_')}]");
            }
        }

        static void TryIllustrateAll()
        {
            _count++;

            if (_count > 1)
            {
                Console.Clear();
                Illustrate(_array1);
                Console.WriteLine();
                Illustrate(_array2);
                Thread.Sleep(33);
                _count -= 2;
            }
        }
    }
}