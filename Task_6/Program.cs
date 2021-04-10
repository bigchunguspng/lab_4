using System;
using System.Threading;

namespace Task_6
{
    internal class Program
    {
        private static readonly int NumberOfRunners = 7, Distance = 50;
        private static int[,] _map;
        private static int[] _runnersLocations, _runnerPlaces;

        private static int _counterR, _counterD, _counterF;

        private static readonly Random R = new Random();

        public static void Main(string[] args)
        {
            GenerateMap();
            _runnersLocations = new int[NumberOfRunners];
            _runnerPlaces = new int[NumberOfRunners];

            for (int i = 0; i < NumberOfRunners; i++)
            {
                _counterR = i;
                Thread thread = new Thread(Run);
                thread.Start();
            }

            Console.ReadKey();
        }

        static void GenerateMap()
        {
            _map = new int[NumberOfRunners, Distance];
            
            for (int runner = 0; runner < _map.GetLength(0); runner++)
            {
                for (int x = RandomSmallDistance; x < _map.GetLength(1) - 1;)
                {
                    _map[runner, x] = 1;
                    x += RandomSmallDistance;
                }
            }
        }

        static int RandomSmallDistance => R.Next(3, Distance * 3 / 8);

        static void Draw()
        {
            Console.Clear();
            for (int runner = 0; runner < _map.GetLength(0); runner++)
            {
                for (int x = 0; x < _map.GetLength(1); x++)
                {
                    if (_runnersLocations[runner] == x)
                        Console.Write(_map[runner, x] == 1 ? "^" : "/");
                    else
                        Console.Write(_map[runner, x] == 1 ? "-" : "_");
                }

                if (_runnersLocations[runner] >= Distance - 1)
                    Console.Write(" " + _runnerPlaces[runner]);
                Console.WriteLine();
            }
        }

        static void Run()
        {
            int number = _counterR;
            for (int x = 0; x < Distance; x++)
            {
                _runnersLocations[number] = x;
                if (x == Distance - 1)
                {
                    _counterF++;
                    _runnerPlaces[number] = _counterF;
                    _counterD += NumberOfRunners;
                }
                DrawRequest();
                Thread.Sleep(_map[number, x] == 0 ? 200 : 800);
            }
            
            DrawRequest();
        }

        static void DrawRequest()
        {
            _counterD++;
            if (_counterD >= NumberOfRunners)
            {
                _counterD = 0;
                Draw();
            }
        }
    }
}