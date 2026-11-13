using System;

namespace Laba9
{
    public class Triangle
    {
        // Задание 1
        private double a, b, c;
        public Triangle()
        {
            a = 1; b = 1; c = 1;
            Count++;
        }

        public static int Count { get; private set; } = 0;
        public Triangle(double a, double b, double c)
        {
            Sides = new double[] { a, b, c };
            Count++;
        }

        public double[] Sides
        {
            get 
            { 
                return new double[] { a, b, c }; 
            }
            set
            {
                a = value[0];
                b = value[1];
                c = value[2];
            }
        }

        public double Area() {
            double p = (a + b + c) / 2;
            return Math.Pow(p * (p - a) * (p - b) * (p - c), 0.5);
        }

        // Задание 2
        public static Triangle operator ++(Triangle t)
        {
            t.a++; t.b++; t.c++;
            return t;
        }

        public static Triangle operator --(Triangle t)
        {
            t.a--; t.b--; t.c--;
            return t;
        }

        public static explicit operator double(Triangle t)
        {
            if (t)
                return t.Area();
            return - 1;
        }

        public static implicit operator bool(Triangle t)
        {
            return (t.a + t.b > t.c) && (t.a + t.c > t.b) && (t.b + t.c > t.a);
        }

        public static bool operator <=(Triangle t1, Triangle t2)
        {
            return t1.Area() <= t2.Area();
        }

        public static bool operator >=(Triangle t1, Triangle t2)
        {
            return t1.Area() >= t2.Area();
        }

    }

}
