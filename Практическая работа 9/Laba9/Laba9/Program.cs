using System;

namespace Laba9
{
    public class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Тестирование класса Triangle \n");

            Triangle t1 = new Triangle(3, 4, 5);
            Console.Write("t1 (3,4,5): ");
            UI.Print(t1);

            Triangle t2 = new Triangle();
            Console.Write("t2 (по умолчанию): ");
            UI.Print(t2);

            Console.WriteLine("t1.Sides[0] (сторона a) = " + t1.Sides[0]);
            double[] newSides = { 6, 8, 10 };
            t2.Sides = newSides;
            Console.Write("t2 после изменения через свойство Sides: ");
            UI.Print(t2);

            Triangle badTri = new Triangle(1, 1, 5);
            Console.WriteLine("Существует ли t1? " + (bool)t1);
            Console.WriteLine("Существует ли badTri(1,1,5)? " + (bool)badTri);

            Console.WriteLine("Площадь t1 (через метод): " + t1.Area());
            Console.WriteLine("Площадь t1 (через (double)t1): " + (double)t1);
            Console.WriteLine("Площадь badTri (через (double)badTri): " + (double)badTri);
            Console.WriteLine("Площадь t1 (через функцию): " + GetAreaStatic(t1));

            Console.Write("t1 до ++: ");
            UI.Print(t1);
            t1++;
            Console.Write("t1 после ++: ");
            UI.Print(t1);
            t1--;
            Console.Write("t1 после --: ");
            UI.Print(t1);

            Triangle t3 = new Triangle(10, 10, 10);
            Console.WriteLine($"Площадь t1 = {t1.Area():F3}, Площадь t3 = {t3.Area():F3}");
            Console.WriteLine("t1 <= t3 ? " + (t1 <= t3));
            Console.WriteLine("t1 >= t3 ? " + (t1 >= t3));

            Console.WriteLine("1. Конструктор без параметров");
            TriangleArray emptyArr = new TriangleArray();
            emptyArr.PrintArray();

            Console.WriteLine("\n2. Конструктор со случайными значениями (размер 3) ");
            TriangleArray rndArr = new TriangleArray(3);
            rndArr.PrintArray();

            Console.WriteLine("\n3. Конструктор с вводом с клавиатуры (размер 2) ");
            TriangleArray keyArr = new TriangleArray(2, true);
            keyArr.PrintArray();

            Console.WriteLine("\nВсего создано объектов Triangle:" + Triangle.Count);

            Console.WriteLine("\nРабота индексатора ");
            Console.WriteLine("Читаем элемент с индексом 0 из rndArr:");
            UI.Print(rndArr[0]);

            Console.WriteLine("\nПроверка исключения (индекс 10) ");
            try
            {
                var test = rndArr[10];
                UI.Print(test);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Поймано исключение: " + ex.Message);
            }

            Console.WriteLine("\nПоиск треугольника с минимальной площадью ");
            FindMinAreaTriangle(keyArr);

            Console.ReadLine();
        }

        public static void FindMinAreaTriangle(TriangleArray collection)
        {
            if (collection.Length == 0)
            {
                Console.WriteLine("Массив пуст");
                return;
            }

            Triangle minTri = null;
            for (int i = 0; i < collection.Length; i++)
            {
                Triangle current = collection[i];

                if (!current) 
                    continue;

                if (minTri == null || current <= minTri)
                {
                    minTri = current;
                }
            }

            if (minTri != null)
            {
                Console.WriteLine("Найден треугольник с минимальной площадью:");
                UI.Print(minTri);
                Console.WriteLine("Его площадь: " + minTri.Area());
            }
            else
            {
                Console.WriteLine("В массиве нет валидных треугольников.");
            }
        }

        static double GetAreaStatic(Triangle t)
        {
            if (t)
                return t.Area();
            else
                return 0;
        }
    }
}