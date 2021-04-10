using System;
using System.Threading;

namespace Task_4
{
    internal class Program
    {
        private static readonly int Width = 4, Height = 70;
        private static int _halfWidth, _halfHeight;
        private static readonly double Step = 0.01;
        
        private static double[] _values;
        

        public static void Main(string[] args)
        {
            _halfWidth = Width / 2;
            _halfHeight = Height / 2;
            
            Thread t1 = new Thread(Calculate);
            Thread t2 = new Thread(Draw);
            
            t1.Start();
            t2.Start();
        }

        static void Calculate()
        {
            _values = new double[(int) (Width / Step)];
            
            for (double x = -_halfWidth; x < _halfWidth; x += Step)
                _values[(int) ((x + _halfWidth) / Step)] = Function(x);
        }

        static double Function(double x) => Math.Round((23 * x * x - 33) / Step) * Step;

        static void Draw()
        {
            for (var i = 0; i < _values.Length; i++)
            {
                double value = _values[i];
                if (value > _halfHeight || value < -_halfHeight)
                    Console.WriteLine();
                else
                    Console.WriteLine(
                        $"{"*".PadLeft((int) Math.Round(value + _halfHeight))} F({Math.Round(i * Step - _halfWidth, 2)}) = {value}");
            }

            Console.ReadKey();
        }
    }
}