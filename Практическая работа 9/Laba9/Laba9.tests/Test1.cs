using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Laba9.Tests
{
    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        public void DefaultConstructor_CreatesUnitTriangle()
        {
            var t = new Triangle();
            CollectionAssert.AreEqual(new double[] { 1, 1, 1 }, t.Sides);
        }

        [TestMethod]
        public void Constructor_SetsSides()
        {
            var t = new Triangle(3, 4, 5);
            CollectionAssert.AreEqual(new double[] { 3, 4, 5 }, t.Sides);
        }

        [TestMethod]
        public void Sides_Setter_ChangesSides()
        {
            var t = new Triangle();
            t.Sides = new double[] { 6, 8, 10 };
            CollectionAssert.AreEqual(new double[] { 6, 8, 10 }, t.Sides);
        }

        [TestMethod]
        public void Count_IncreasesWithEachNewTriangle()
        {
            int before = Triangle.Count;
            new Triangle();
            new Triangle(3, 4, 5);
            Assert.AreEqual(before + 2, Triangle.Count);
        }

        [TestMethod]
        public void Area_RightTriangle()
        {
            Assert.AreEqual(6.0, new Triangle(3, 4, 5).Area(), 1e-9);
        }

        [TestMethod]
        public void Area_InvalidTriangle_IsNaN()
        {
            Assert.IsTrue(double.IsNaN(new Triangle(1, 1, 5).Area()));
        }

        [TestMethod]
        public void Increment_AddsOneToEachSide()
        {
            var t = new Triangle(3, 4, 5);
            t++;
            CollectionAssert.AreEqual(new double[] { 4, 5, 6 }, t.Sides);
        }

        [TestMethod]
        public void Decrement_SubtractsOneFromEachSide()
        {
            var t = new Triangle(3, 4, 5);
            t--;
            CollectionAssert.AreEqual(new double[] { 2, 3, 4 }, t.Sides);
        }

        [TestMethod]
        public void BoolOperator_ValidTriangle_True()
        {
            Assert.IsTrue((bool)new Triangle(3, 4, 5));
        }

        [TestMethod]
        public void BoolOperator_InvalidTriangle_False()
        {
            Assert.IsFalse((bool)new Triangle(1, 1, 5));
        }

        [TestMethod]
        public void DoubleOperator_ValidTriangle_ReturnsArea()
        {
            Assert.AreEqual(6.0, (double)new Triangle(3, 4, 5), 1e-9);
        }

        [TestMethod]
        public void DoubleOperator_InvalidTriangle_ReturnsMinusOne()
        {
            Assert.AreEqual(-1.0, (double)new Triangle(1, 1, 5));
        }

        [TestMethod]
        public void CompareOperators_SmallerAndBigger()
        {
            var small = new Triangle(3, 4, 5);
            var big = new Triangle(10, 10, 10);

            Assert.IsTrue(small <= big);
            Assert.IsFalse(small >= big);
            Assert.IsTrue(big >= small);
        }

        [TestMethod]
        public void CompareOperators_EqualAreas()
        {
            var a = new Triangle(3, 4, 5);
            var b = new Triangle(3, 4, 5);

            Assert.IsTrue(a <= b);
            Assert.IsTrue(a >= b);
        }
    }

    [TestClass]
    public class TriangleArrayTests
    {
        private TextReader oldIn;

        [TestInitialize]
        public void Init() => oldIn = Console.In;

        [TestCleanup]
        public void Cleanup() => Console.SetIn(oldIn);

        [TestMethod]
        public void EmptyConstructor_CreatesEmptyArray()
        {
            Assert.AreEqual(0, new TriangleArray().Length);
        }

        [TestMethod]
        public void RandomConstructor_FillsAllElements()
        {
            var arr = new TriangleArray(5);

            Assert.AreEqual(5, arr.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                Assert.IsNotNull(arr[i]);
                foreach (double side in arr[i].Sides)
                    Assert.IsTrue(side >= 1 && side < 10);
            }
        }

        [TestMethod]
        public void KeyboardConstructor_ReadsTrianglesFromInput()
        {
            Console.SetIn(new StringReader("3\n4\n5\n6\n8\n10\n"));

            var arr = new TriangleArray(2, true);

            CollectionAssert.AreEqual(new double[] { 3, 4, 5 }, arr[0].Sides);
            CollectionAssert.AreEqual(new double[] { 6, 8, 10 }, arr[1].Sides);
        }

        [TestMethod]
        public void ObjectsCount_IncreasesWithEachArray()
        {
            int before = TriangleArray.ObjectsCount;
            new TriangleArray();
            new TriangleArray(1);
            Assert.AreEqual(before + 2, TriangleArray.ObjectsCount);
        }

        [TestMethod]
        public void Indexer_SetAndGet()
        {
            var arr = new TriangleArray(2);
            var t = new Triangle(3, 4, 5);

            arr[1] = t;

            Assert.AreSame(t, arr[1]);
        }

      

        [TestMethod]
        public void PrintArray_PrintsElementsAndNull()
        {
            var arr = new TriangleArray(2);
            arr[0] = new Triangle(3, 4, 5);
            arr[1] = null;

            string output = ConsoleHelper.CaptureOutput(() => arr.PrintArray());

            StringAssert.Contains(output, "Стороны: 3, 4, 5");
            StringAssert.Contains(output, "null");
        }
    }

    [TestClass]
    public class UITests
    {
        private TextReader oldIn;

        [TestInitialize]
        public void Init() => oldIn = Console.In;

        [TestCleanup]
        public void Cleanup() => Console.SetIn(oldIn);

        [TestMethod]
        public void Print_WritesSides()
        {
            string output = ConsoleHelper.CaptureOutput(() => UI.Print(new Triangle(3, 4, 5)));
            StringAssert.Contains(output, "Стороны: 3, 4, 5");
        }

        [TestMethod]
        public void Input_ReadsThreeNumbers()
        {
            Console.SetIn(new StringReader("3\n4\n5\n"));

            Triangle t = UI.Input();

            CollectionAssert.AreEqual(new double[] { 3, 4, 5 }, t.Sides);
        }

        [TestMethod]
        public void Input_RepeatsPromptOnInvalidText()
        {
            Console.SetIn(new StringReader("abc\n3\n4\n5\n"));

            Triangle t = UI.Input();

            CollectionAssert.AreEqual(new double[] { 3, 4, 5 }, t.Sides);
        }
    }

    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void FindMinAreaTriangle_EmptyArray_PrintsMessage()
        {
            string output = ConsoleHelper.CaptureOutput(
                () => Program.FindMinAreaTriangle(new TriangleArray()));

            StringAssert.Contains(output, "Массив пуст");
        }

        [TestMethod]
        public void FindMinAreaTriangle_FindsTriangleWithMaxArea()
        {
            var arr = new TriangleArray(3);
            arr[0] = new Triangle(3, 4, 5);
            arr[1] = new Triangle(10, 10, 10);
            arr[2] = new Triangle(1, 1, 5);

            string output = ConsoleHelper.CaptureOutput(() => Program.FindMinAreaTriangle(arr));

            StringAssert.Contains(output, "Стороны: 10, 10, 10");
        }

        [TestMethod]
        public void FindMinAreaTriangle_NoValidTriangles_PrintsMessage()
        {
            var arr = new TriangleArray(2);
            arr[0] = new Triangle(1, 1, 5);
            arr[1] = new Triangle(1, 2, 10);

            string output = ConsoleHelper.CaptureOutput(() => Program.FindMinAreaTriangle(arr));

            StringAssert.Contains(output, "нет валидных");
        }
    }

    // Перехватывает вывод консоли на время выполнения действия
    internal static class ConsoleHelper
    {
        public static string CaptureOutput(Action action)
        {
            TextWriter oldOut = Console.Out;
            var writer = new StringWriter();
            Console.SetOut(writer);
            try
            {
                action();
            }
            finally
            {
                Console.SetOut(oldOut);
            }
            return writer.ToString();
        }
    }
}