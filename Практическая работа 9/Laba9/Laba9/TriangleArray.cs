using System;

namespace Laba9
{
    public class TriangleArray
    {
        private Triangle[] arr;

        public static int ObjectsCount { get; private set; } = 0;

        public TriangleArray()
        {
            arr = new Triangle[0];
            ObjectsCount++;
        }

        public TriangleArray(int size)
        {
            arr = new Triangle[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                double a = rnd.Next(1, 10);
                double b = rnd.Next(1, 10);
                double c = rnd.Next(1, 10);
                arr[i] = new Triangle(a, b, c);
            }
            ObjectsCount++;
        }

        public TriangleArray(int size, bool fromKeyboard)
        {
            arr = new Triangle[size];
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Ввод треугольника " + (i + 1));
                arr[i] = UI.Input();
            }
            ObjectsCount++;
        }

        public void PrintArray()
        {
            Console.WriteLine("Массив треугольников (всего элементов: " + arr.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(i + ") ");
                if (arr[i] != null)
                    UI.Print(arr[i]);
                else
                    Console.WriteLine("null");
            }
        }

        // Индексатор с проверкой выхода за границы
        public Triangle this[int index]
        {
            get
            {
                if (index < 0 || index >= arr.Length)
                {
                    throw new ArgumentException($"Индекс {index} выходит за границы массива (размер: {arr.Length}).");
                }
                return arr[index];
            }
            set
            {
                if (index < 0 || index >= arr.Length)
                {
                    throw new ArgumentException($"Индекс {index} выходит за границы массива (размер: {arr.Length}).");
                }
                arr[index] = value;
            }
        }

        public int Length => arr.Length;
    }
}