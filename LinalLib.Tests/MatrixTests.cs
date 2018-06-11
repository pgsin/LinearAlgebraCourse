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
            Matrix actual = new Matrix(new double[,] { { 0, 0 }, { 0, 0 } });
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
            Matrix actual = new Matrix(new double[,] { { 0, 0, 0 }, { 0, 0, 0 } });
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
            for (int i = 0; i < real.M; i++)
            {
                for (int j = 0; j < real.N; j++)
                {
                    Assert.AreEqual(d[i, j], real[i, j]);
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
            Random rand = new Random();
            Matrix r = new Matrix(10, 10, rand);
            Matrix a = (Matrix)r.Clone();
            r.Transpose(out Matrix rTransposed);
            rTransposed.Transpose(out Matrix rTransposedTwice);
            Assert.AreEqual(rTransposedTwice.CompareTo(a), 0);
        }

        [TestMethod]
        public void PALU_factorization0Test()
        {
            Matrix a = new Matrix(new double[,] { { 2, 1 }, { 8, 7 } });
            Matrix la = new Matrix(new double[,] { { 1, 0 }, { 4, 1 } });
            Matrix pa = new Matrix(new double[,] { { 1, 0 }, { 0, 1 } });
            Matrix ua = new Matrix(new double[,] { { 2, 1 }, { 0, 3 } });
            a.EchelonForm(out Matrix pr, out Matrix lr, out Matrix ur);
            Assert.AreEqual(la.CompareTo(lr), 0);
            Assert.AreEqual(pa.CompareTo(pr), 0);
            Assert.AreEqual(ua.CompareTo(ur), 0);
        }

        [TestMethod]
        public void PALU_factorization1Test()
        {
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 2 }, { 1, 2, 3 } });
            Matrix la = new Matrix(new double[,] { { 1, 0, 0 }, { 1, 1, 0 }, { 1, 1, 1 } });
            Matrix pa = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix ua = new Matrix(new double[,] { { 1, 1, 1 }, { 0, 1, 1 }, { 0, 0, 1 } });
            a.EchelonForm(out Matrix pr, out Matrix lr, out Matrix ur);
            Assert.AreEqual(la.CompareTo(lr), 0);
            Assert.AreEqual(pa.CompareTo(pr), 0);
            Assert.AreEqual(ua.CompareTo(ur), 0);
        }


        [TestMethod]
        public void PALU_factorization2Test()
        {
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 2, 5 }, { 4, 6, 8 } });
            Matrix la = new Matrix(new double[,] { { 1, 0, 0 }, { 4, 1, 0 }, { 2, 0, 1 } });
            Matrix pa = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 0, 1 }, { 0, 1, 0 } });
            Matrix ua = new Matrix(new double[,] { { 1, 1, 1 }, { 0, 2, 4 }, { 0, 0, 3 } });
            a.EchelonForm(out Matrix pr, out Matrix lr, out Matrix ur);
            Assert.AreEqual(la.CompareTo(lr), 0);
            Assert.AreEqual(pa.CompareTo(pr), 0);
            Assert.AreEqual(ua.CompareTo(ur), 0);
        }

        [TestMethod]
        public void Reverse0Test()
        {
            Random rand = new Random();
            Matrix r = new Matrix(10, 10, rand);
            Matrix a = (Matrix)r.Clone();
            Assert.AreEqual(r.Reverse(out Matrix rReverse), 0);
            Assert.AreEqual((a * rReverse).CompareTo(new Matrix(a.M, true)), 0);
            Assert.AreEqual(rReverse.Reverse(out Matrix rReverseTwice), 0);
            Assert.AreEqual(a.CompareTo(rReverseTwice), 0);
        }

        [TestMethod]
        public void Reverse1Test()
        {
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 2, 5 }, { 4, 4, 8 } });
            Assert.AreNotEqual(a.Reverse(out Matrix _), 0);
        }

        [TestMethod]
        public void GaussElimination0Test()
        {
            Matrix a = new Matrix(new double[,] { { 2, -1 }, { -1, 2 } });
            Matrix b = new Matrix(new double[,] { { 0 }, { 3 } });
            Matrix xa = new Matrix(new double[,] { { 1 }, { 2 } });
            Assert.AreEqual(a.CompleteSolution(b, out Matrix pr, out Matrix xpr, out Matrix xnr), 0);
            Assert.AreEqual(xa.CompareTo(xpr), 0);
        }

        [TestMethod]
        public void GaussElimination1Test()
        {
            Matrix a = new Matrix(new double[,] { { 2, -1, 0 }, { -1, 2, -1 }, { 0, -3, 4 } });
            Matrix b = new Matrix(new double[,] { { 0 }, { -1 }, { 4 } });
            Matrix xa = new Matrix(new double[,] { { 0 }, { 0 }, { 1 } });
            Assert.AreEqual(a.CompleteSolution(b, out Matrix pr, out Matrix xpr, out Matrix xnr), 0);
            Assert.AreEqual(xa.CompareTo(xpr), 0);
        }

        [TestMethod]
        public void ExchangeRowsTest()
        {
            Random rand = new Random();
            Matrix r = new Matrix(10, 10, rand);
            Matrix a = (Matrix)r.Clone();
            int i = rand.Next(0, 10);
            int j = rand.Next(0, 10);
            a.ExchangeRows(i, j);
            a.ExchangeRows(i, j);
            Assert.AreEqual(a.CompareTo(r), 0);
            int until = rand.Next(0, 10);
            a.ExchangeRows(i, j, until);
            a.ExchangeRows(i, j, until);
            Assert.AreEqual(a.CompareTo(r), 0);
            a.ExchangeRows(i, j);
            a.ExchangeRows(j, i);
            Assert.AreEqual(a.CompareTo(r), 0);
        }
    }
}