using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LinalLib.Tests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void MatrixIntBoolConstructorTest()
        {
            Matrix real = new Matrix(2);
            Matrix actual = new Matrix(new double[,]{{0, 0}, {0, 0}});
            Assert.AreEqual(real.CompareTo(actual), 0);
            actual = new Matrix(new double[,] { { 0, 0 }, { 1, 0 } });
            Assert.AreNotEqual(real.CompareTo(actual), 0);
            real = new Matrix(2, true);
            actual = new Matrix(new double[,] { { 1, 0 }, { 0, 1 } });
            Assert.AreEqual(real.CompareTo(actual), 0);
        }

        [TestMethod]
        public void MatrixIntIntRandomTest()
        {
            Matrix real = new Matrix(2, 3);
            Matrix actual = new Matrix(new double[,] { { 0, 0, 0}, { 0, 0, 0} });
            Assert.AreEqual(real.CompareTo(actual), 0);
            Random r = new Random();
            real = new Matrix(2, 3, r);
            Assert.AreNotEqual(real.CompareTo(actual), 0);
        }

        [TestMethod]
        public void MatrixDouble2DTest()
        {
            double[,] d = { { 0, 1 }, { 2, 3 } };
            Matrix real = new Matrix(d);
            for (int i = 0; i < real.N; i++)
            {
                for (int j = 0; j < real.M; j++)
                {
                    Assert.AreEqual(d[i,j], real[i, j]);
                }
            }
        }

        [TestMethod]
        public void MatrixMultiplicationSquareTest()
        {
            double[,] a = { { 1, 2 }, { 3, 4 } };
            double[,] b = { { 2, 0 }, { 1, 2 } };
            double[,] c = { { 4, 4 }, { 10, 8 } };
            Matrix realC = new Matrix(a) * new Matrix(b);
            Matrix actualC = new Matrix(c);
            Assert.AreEqual(realC.CompareTo(actualC), 0);
            c = new double[,] { { 2, 4 }, { 7, 10 } };
            realC = new Matrix(b) * new Matrix(a);
            actualC = new Matrix(c);
            Assert.AreEqual(realC.CompareTo(actualC), 0);
        }

        [TestMethod]
        public void MatrixMultiplicationNotSquareTest()
        {
            double[,] a = { { 6, 0 }, { 0, 2 }, { 1, 1 } };
            double[,] b = { { 6, 4, 1 }, { 0, 3, 5 } };
            double[,] c = { { 36, 24, 6 }, { 0, 6, 10 }, { 6, 7, 6 } };
            Matrix realC = new Matrix(a) * new Matrix(b);
            Matrix actualC = new Matrix(c);
            Assert.AreEqual(realC.CompareTo(actualC), 0);
            c = new double[,] { { 37, 9 }, { 5, 11 } };
            realC = new Matrix(b) * new Matrix(a);
            actualC = new Matrix(c);
            Assert.AreEqual(realC.CompareTo(actualC), 0);
            realC = new Matrix(a) * new Matrix(a);
            Assert.AreEqual(realC, null);
        }

        [TestMethod]
        public void MatrixSumTest()
        {
            double[,] a = { { 1, 2 }, { 3, 4 } };
            double[,] b = { { 2, 0 }, { 1, 2 } };
            double[,] c = { { 3, 2 }, { 4, 6 } };
            Matrix realC = new Matrix(a) + new Matrix(b);
            Matrix actualC = new Matrix(c);
            Assert.AreEqual(realC.CompareTo(actualC), 0);
            b = new double[,] { { 6, 4, 1 }, { 0, 3, 5 } };
            realC = new Matrix(a) + new Matrix(b);
            Assert.AreEqual(realC, null);
        }

        [TestMethod]
        public void MatrixTransposeTest()
        {
            Random r = new Random();
            Matrix real = new Matrix(10, 10, r);
            Matrix actual = (Matrix)real.Clone();
            real.Transpose();
            real.Transpose();
            Assert.AreEqual(real.CompareTo(actual), 0);
        }


        [TestMethod]
        public void MatrixTransposeOutMatrixTest()
        {
            Random r = new Random();
            Matrix real = new Matrix(10, 10, r);
            Matrix actual = (Matrix)real.Clone();
            real.Transpose(out Matrix realTransposed);
            realTransposed.Transpose(out Matrix realTransposedTwice);
            Assert.AreEqual(realTransposedTwice.CompareTo(actual), 0);
        }
    }
}