using System;
using System.Collections.Generic;
using System.Text;

namespace Laba56
{
    class Program
    {
        static Random rnd = new Random();
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

        // Перегрузка - ввод целого числа в заданном диапазоне
        static int IntEnter(string name, int min, int max)
        {
            int value;
            while (true)
            {
                value = IntEnter(name + " (от " + min + " до " + max + ")");
                if (value >= min && value <= max)
                    return value;
                ShowError("число вне допустимого диапазона!");
            }
        }

        static void ShowError(string message, params object[] args)
        {
            Console.WriteLine("Ошибка: " + string.Format(message, args));
        }

        // Вывод массивов (перегрузка)
        static void PrintArray(int[,] a)
        {
            if (a == null || a.Length == 0)
            {
                Console.WriteLine("Массив пустой");
                return;
            }
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                    Console.Write(a[i, j] + "\t");
                Console.WriteLine();
            }
        }

        static void PrintArray(int[][] a)
        {
            if (a == null || a.Length == 0)
            {
                Console.WriteLine("Массив пустой");
                return;
            }
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[i].Length; j++)
                    Console.Write(a[i][j] + "\t");
                Console.WriteLine();
            }
        }

        static void PrintString(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                Console.WriteLine("Строка пустая");
            else
                Console.WriteLine(s);
        }

        // Случайное заполнение (перегрузка + параметры по умолчанию)
        static void FillRandom(int[,] a, int min = -10, int max = 10)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    a[i, j] = rnd.Next(min, max + 1);
        }

        static void FillRandom(int[] a, int min = -10, int max = 10)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] = rnd.Next(min, max + 1);
        }

        // Двумерный массив 
        static int[,] CreateMatrixManual()
        {
            int rows = IntEnter("количество строк", 1, 10);
            int cols = IntEnter("количество столбцов", 1, 10);
            int[,] a = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    a[i, j] = IntEnter("элемент [" + i + "," + j + "]");
            return a;
        }

        static int[,] CreateMatrixRandom()
        {
            int rows = IntEnter("количество строк", 1, 10);
            int cols = IntEnter("количество столбцов", 1, 10);
            int[,] a = new int[rows, cols];
            FillRandom(a);
            return a;
        }

        // Добавить столбец в конец (массив передаётся по ссылке)
        static void AddColumn(ref int[,] a)
        {
            if (a == null || a.Length == 0)
            {
                ShowError("Массив не создан. Сначала заполните его значениями");
                return;
            }

            int rows = a.GetLength(0);
            int cols = a.GetLength(1);
            int[,] result = new int[rows, cols + 1];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = a[i, j];

            for (int i = 0; i < rows; i++)
                result[i, cols] = IntEnter("элемент нового столбца [" + i + "," + cols + "]");

            a = result;
        }

        // Рваный массив
        static int[][] CreateJaggedManual()
        {
            int rows = IntEnter("количество строк", 1, 10);
            int[][] a = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                int len = IntEnter("длину строки " + i, 1, 10);
                a[i] = new int[len];
                for (int j = 0; j < len; j++)
                    a[i][j] = IntEnter("элемент [" + i + "][" + j + "]");
            }
            return a;
        }

        static int[][] CreateJaggedRandom()
        {
            int rows = IntEnter("количество строк", 1, 10);
            int[][] a = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                a[i] = new int[rnd.Next(1, 8)];
                FillRandom(a[i]);
            }
            return a;
        }

        // Есть ли число k в строке 
        static bool RowContains(int[] row, int k, int index = 0)
        {
            if (index >= row.Length)
                return false;
            if (row[index] == k)
                return true;
            return RowContains(row, k, index + 1);
        }

        // Удалить все строки, в которых встречается число k
        static int[][] DeleteRows(int[][] a, int k)
        {
            if (a == null || a.Length == 0)
            {
                ShowError("нельзя удалить строки, массив пустой.");
                return a;
            }

            // сколько строк останется
            int keep = 0;
            for (int i = 0; i < a.Length; i++)
                if (!RowContains(a[i], k))
                    keep++;

            if (keep == a.Length)
            {
                ShowError("в массиве нет строк с числом {0}.", k);
                return a;
            }

            int[][] result = new int[keep][];
            int pos = 0;
            for (int i = 0; i < a.Length; i++)
                if (!RowContains(a[i], k))
                {
                    result[pos] = a[i];
                    pos++;
                }
            return result;
        }

        // Строки

        // Поменять местами первое и последнее предложение
        static string SwapSentences(string s)
        {
            List<string> sentences = new List<string>();
            int start = 0;
            while (start < s.Length)
            {
                int end = s.IndexOfAny(new char[] { '.', '!', '?' }, start);
                if (end == -1)
                    end = s.Length - 1;
                string sentence = s.Substring(start, end - start + 1).Trim();
                if (sentence != "")
                    sentences.Add(sentence);
                start = end + 1;
            }

            if (sentences.Count < 2)
            {
                ShowError("в строке меньше двух предложений, менять нечего.");
                return s;
            }

            string temp = sentences[0];
            sentences[0] = sentences[sentences.Count - 1];
            sentences[sentences.Count - 1] = temp;

            return string.Join(" ", sentences);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            int choice = 0, choice2 = 0;

            int[,] matrix = null;
            int[][] jagged = null;
            string text = "";

            // тестовый массив строк
            string[] tests = {
                "В лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была. ",
                "Первое предложение.   Второе;  с знаками: препинания!  Третье?   Последнее.",
                "Одно предложение."
            };

            do
            {
                Console.WriteLine("\nВыберите задание \n 1 - Двумерный массив\n 2 - Рваный массив\n " +
                    "3 - Строки\n 4 - Выход\n");
                choice = IntEnter("номер команды, которую вы выбираете");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            do
                            {
                                Console.WriteLine("\nВыберите действие \n 1 - Ручное заполнение\n 2 - Автоматическое заполнение\n " +
                                                "3 - Добавить столбец в конец\n 4 - Назад\n");
                                choice2 = IntEnter("номер команды, которую вы выбираете");

                                switch (choice2)
                                {
                                    case 1:
                                        matrix = CreateMatrixManual();
                                        Console.WriteLine("\nМассив:");
                                        PrintArray(matrix);
                                        break;
                                    case 2:
                                        matrix = CreateMatrixRandom();
                                        Console.WriteLine("\nМассив:");
                                        PrintArray(matrix);
                                        break;
                                    case 3:
                                        Console.WriteLine("\nИсходный массив:");
                                        PrintArray(matrix);
                                        AddColumn(ref matrix);
                                        Console.WriteLine("\nРезультат:");
                                        PrintArray(matrix);
                                        break;
                                    case 4:
                                        break;
                                    default:
                                        Console.WriteLine("Такой команды нет");
                                        break;
                                }
                            } while (choice2 != 4);

                            break;

                        case 2:
                            do
                            {
                                Console.WriteLine("\nВыберите действие \n 1 - Ручное заполнение\n 2 - Автоматическое заполнение\n " +
                                                "3 - Удалить строки с числом k\n 4 - Назад\n");
                                choice2 = IntEnter("номер команды, которую вы выбираете");

                                switch (choice2)
                                {
                                    case 1:
                                        jagged = CreateJaggedManual();
                                        Console.WriteLine("\nМассив:");
                                        PrintArray(jagged);
                                        break;
                                    case 2:
                                        jagged = CreateJaggedRandom();
                                        Console.WriteLine("\nМассив:");
                                        PrintArray(jagged);
                                        break;
                                    case 3:
                                        Console.WriteLine("\nИсходный массив:");
                                        PrintArray(jagged);
                                        if (jagged == null || jagged.Length == 0)
                                        {
                                            ShowError("нельзя удалить строки, массив пустой.");
                                            break;
                                        }
                                        int k = IntEnter("число k");
                                        jagged = DeleteRows(jagged, k);
                                        Console.WriteLine("\nРезультат:");
                                        PrintArray(jagged);
                                        break;
                                    case 4:
                                        break;
                                    default:
                                        Console.WriteLine("Такой команды нет");
                                        break;
                                }
                            } while (choice2 != 4);

                            break;

                        case 3:
                            do
                            {
                                Console.WriteLine("\nВыберите действие \n 1 - Ввод строки с клавиатуры\n 2 - Выбор строки из тестового массива\n " +
                                                "3 - Поменять местами первое и последнее предложение\n 4 - Назад\n");
                                choice2 = IntEnter("номер команды, которую вы выбираете");

                                switch (choice2)
                                {
                                    case 1:
                                        Console.Write("Введите строку: ");
                                        text = Console.ReadLine();
                                        Console.WriteLine("\nСтрока:");
                                        PrintString(text);
                                        break;
                                    case 2:
                                        Console.WriteLine();
                                        for (int i = 0; i < tests.Length; i++)
                                            Console.WriteLine((i + 1) + ") " + tests[i]);
                                        int num = IntEnter("номер строки", 1, tests.Length);
                                        text = tests[num - 1];
                                        Console.WriteLine("\nСтрока:");
                                        PrintString(text);
                                        break;
                                    case 3:
                                        Console.WriteLine("\nИсходная строка:");
                                        PrintString(text);
                                        if (string.IsNullOrWhiteSpace(text))
                                            break;
                                        text = SwapSentences(text);
                                        Console.WriteLine("\nРезультат:");
                                        PrintString(text);
                                        break;
                                    case 4:
                                        break;
                                    default:
                                        Console.WriteLine("Такой команды нет");
                                        break;
                                }
                            } while (choice2 != 4);

                            break;

                        case 4:
                            Console.WriteLine("Завершение выполнения программы");
                            break;

                        default:
                            Console.WriteLine("Такой команды нет");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Непредвиденная ошибка: " + ex.Message);
                }
            } while (choice != 4);
        }
    }
}