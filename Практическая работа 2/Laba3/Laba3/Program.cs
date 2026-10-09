using System;

namespace Laba3
{
    class Program
    {
        static double Function(double x)
        {
            double y = x * Math.Atan(x) - Math.Log(Math.Sqrt(1 + x*x));
            return y;
        }

        static double ElementOfRow(double x, int currentN)
        {
            double el = Math.Pow(-1, currentN + 1) * Math.Pow(x, 2 * currentN) /
                (2 * currentN * (2 * currentN - 1));
            return el;
        }

        static void Show(double x, double y, double se, double sn)
        {
            Console.WriteLine("X = " + x.ToString("F4") + "\t SN = " + sn.ToString("F8") + 
                "\t SE = " + se.ToString("F8") + "\t Y = " + y.ToString("F8"));
        }

        static void SumOfRow(double a, double b, int n, double k, double eps)
        {
            Console.WriteLine("\nВычисление функции\n");
            int number = 0;
            for (double x = a; x <= b; x += (b - a)/k)
            {
                number++;
                double sn = 0, se2 = 0;
                double y = Function(x);
                for (int i = 1; i <= n; i++)
                {
                    sn += ElementOfRow(x, i);
                }
                double se1 = ElementOfRow(x, 1);
                for (int i = 2; Math.Abs(se1 - se2) >= eps; i++)
                {
                    se2 = se1;
                    se1 += ElementOfRow(x, i);
                }
                Console.Write(number + ")\t");
                Show(x, y, se1, sn);
            }

        }

        static void Main(string[] args)
        {
            double a = 0.1;
            double b = 0.8;
            int n = 10;
            int k = 10;
            double eps = 0.0001;

            SumOfRow(a, b, n, k, eps);
        }
    }
}