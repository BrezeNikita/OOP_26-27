using System;

namespace Laba9
{
    public static class UI
    {
        static double DoubleEnter(string name)
        {
            var buf = "";
            double value;
            while (!double.TryParse(buf, out value))
            {
                Console.Write("Введите " + name + ": ");
                buf = Console.ReadLine();
            }
            return value;
        }
        public static Triangle Input()
        {
            double a = DoubleEnter("a");
            double b = DoubleEnter("b");
            double c = DoubleEnter("c");
            return new Triangle(a, b, c);
        }

        public static void Print(Triangle t)
        {
            Console.WriteLine($"Стороны: {t.Sides[0]}, {t.Sides[1]}, {t.Sides[2]}");
        }
    }
}