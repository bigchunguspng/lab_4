using System;
using System.Threading;

namespace Task_1
{
    internal class Program
    {
        //висота (у), ширина(х) та рівень деталізації
        private static int _height = 20, _width = 20, _loD = 10, _number = 0;


        private delegate double Function(double x);

        private static double F1(double x) => Math.Sin(x);
        private static double F2(double x) => 4 * x * x - 2 * x - 22;
        private static double F3(double x) => Math.Log(x * x) / (x * x * x);
        
        private static double Y(double x, Function function) => Math.Round(function(x) * _loD) / _loD;

        private static readonly Function[] Functions = new Function[] {F1, F2, F3};
        
        private static readonly double[,] Values = new double[Functions.Length, _width * _loD];

        private static readonly int[,] Matrix = new int[_height * _loD, _width * _loD];


        private static readonly Mutex Mutex = new Mutex();
        

        public static void Main(string[] args)
        {
            for (int i = 0; i < Functions.Length; i++)
            {
                Thread thread = new Thread(CalculateY);
                thread.Start();
            }
            
            FillMatrix();
            Draw();
            Console.ReadKey();
        }

        private static void CalculateY()
        {
            Mutex.WaitOne();

            int n = _number;
            double step = 1 / (double)_loD;
            int halfwidth = _width / 2;
            for (double x = -halfwidth; x < halfwidth; x += step)
            {
                Values[n, (int)((x + halfwidth) * _loD)] = Y(x, Functions[n]);
            }

            _number++;
            
            Mutex.ReleaseMutex();
        }

        private static void FillMatrix()
        {
            for (int i = 0; i < Values.GetLength(0); i++) //func num
            {
                for (int j = 0; j < Values.GetLength(1); j++) //x value
                {
                    int y = (int) ((Values[i, j] + _height / 2d) * _loD);
                    if (y >= 0 && y < Matrix.GetLength(0))
                    {
                        Matrix[y, j] = 1; //function
                    }
                }
            }
        }
        
        private static void Draw()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Console.Write(Matrix[i, j] > 0 ? "*" : " ");
                }
                Console.WriteLine("");
            }
        }

    }
}