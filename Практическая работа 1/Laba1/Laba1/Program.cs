using System;

namespace Laba1
{
    class Program
    {
        static int IntEnter(string name)
        {
            var buf = "";
            int value;
            while (!int.TryParse(buf, out value))
            {
                Console.Write("Введите " + name + ": ");
                buf = Console.ReadLine();
            }
            return value;
        }

        static double DoubleEnter(string name)
        {
            var buf = "";
            double value;
            while (!double.TryParse(buf, out value))
            {
                Console.Write("Введите значение " + name + ": ");
                buf = Console.ReadLine();
            }
            return value;
        }

        static void Task1()
        {
            int n = IntEnter("n");
            int m = IntEnter("m");

            Console.WriteLine("n++ * m = {0}, m={1}, n={2}", n++ * m, m, n);
            --n;
            Console.WriteLine("n++ < m = {0}, m={1}, n={2}", n++ < m, m, n);
            --n;
            Console.WriteLine("--m > n = {0}, m={1}, n={2}", --m > n, m, n);
            ++m;

            int x = IntEnter("x");
            Console.WriteLine("x = " + x + ", sqrt3(x - x^2 + x^5) = " + Math.Cbrt(x - Math.Pow(x, 2) + Math.Pow(x, 5)));
        }

        static void Task2()
        {
            double x1 = DoubleEnter("x1");
            double y1 = DoubleEnter("y1");

            bool isInArea = Math.Pow(x1, 2) + Math.Pow(y1, 2) <= 1;
            Console.WriteLine("Принадлежность точки к области: " + isInArea);
        }

        static void Task3()
        {
            double a_double = 1000;
            double b_double = 0.0001;

            float a_float = (float)a_double;
            float b_float = (float)b_double;

            float numerator_f = ((float)Math.Pow((a_float - b_float), 3) - (float)Math.Pow(a_float, 3));
            float num1_f = 3 * a_float * (float)Math.Pow(b_float, 2);
            float num2_f = (float)Math.Pow(b_float, 2);
            float num3_f = 3 * (float)Math.Pow(a_float, 2) * b_float;
            float denominator_f = num1_f - num2_f - num3_f;
            Console.WriteLine("Float: ((a - b)^3 - (a^3)) / (3ab^2 - b^3 - 3a^2b) = " + numerator_f / denominator_f);

            double numerator_d = (Math.Pow((a_double - b_double), 3) - Math.Pow(a_double, 3));
            double num1_d = 3 * a_double * Math.Pow(b_double, 2);
            double num2_d = Math.Pow(b_double, 2);
            double num3_d = 3 * Math.Pow(a_double, 2) * b_double;
            double denominator_d = num1_d - num2_d - num3_d;
            Console.WriteLine("Double: ((a - b)^3 - (a^3)) / (3ab^2 - b^3 - 3a^2b) = " + numerator_d / denominator_d);
        }

        static void Main(string[] args)
        {
            int choice = 0;

            while (choice != 4)
            {
                Console.WriteLine("\nВыберите задание \n 1 - Задание 1\n 2 - Задание 2\n " +
                    "3 - Задание 3\n 4 - Выход\n");
                choice = IntEnter("номер команды, которую вы выбираете");

                switch (choice)
                {
                    case 1:
                        Task1();
                        break;

                    case 2:
                        Task2();
                        break;

                    case 3:
                        Task3();
                        break;

                    case 4:
                        Console.WriteLine("Завершение выполнения программы");
                        break;

                    default:
                        break;
                }
            }
        }
    }
}