using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
        public void CompleteSolutionErrorTest()
        {
            Matrix a, b, pr, xpr, xnr;
            a = new Matrix(new double[,] { { 0, 1 }, { 1, 0 }, { 0, 0 } });
            b = new Matrix(new double[,] { { 0 }, { 0 }, { 1 } });
            Assert.AreNotEqual(a.CompleteSolution(b, out pr, out xpr, out xnr), 0);
            a = new Matrix(new double[,] { { 0, 1 }, { 1, 0 }, { 0, 0 } });
            b = new Matrix(new double[,] { { 1 }, { 1 } });
            Assert.AreNotEqual(a.CompleteSolution(b, out pr, out xpr, out xnr), 0);
        }

        [TestMethod]
        public void CompleteSolution0Test()
        {
            Matrix p = new Matrix(new double[,] { { 1 } });
            Matrix a = new Matrix(new double[,] { { 0 } });
            Matrix b = new Matrix(new double[,] { { 0 } });
            Matrix xp = new Matrix(new double[,] { { 0 } });
            Matrix xn = new Matrix(new double[,] { { 1 } });
            Assert.AreEqual(a.CompleteSolution(b, out Matrix pr, out Matrix xpr, out Matrix xnr), 0);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(xp.CompareTo(xpr), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * (xpr + xnr * new Matrix(xnr.N, 1, new Random()))).CompareTo(p * b), 0);
        }

        [TestMethod]
        public void CompleteSolution1Test()
        {
            Matrix p = new Matrix(new double[,] { { 1 } });
            Matrix a = new Matrix(new double[,] { { 1 } });
            Matrix b = new Matrix(new double[,] { { 1 } });
            Matrix xp = new Matrix(new double[,] { { 1 } });
            Matrix xn = new Matrix(new double[,] { { 0 } });
            Assert.AreEqual(a.CompleteSolution(b, out Matrix pr, out Matrix xpr, out Matrix xnr), 0);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(xp.CompareTo(xpr), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * (xpr + xnr * new Matrix(xnr.N, 1, new Random()))).CompareTo(p * b), 0);
        }

        [TestMethod]
        public void CompleteSolution2Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0 }, { 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 2, 1 }, { 8, 7 } });
            Matrix b = new Matrix(new double[,] { { 3 }, { 15 } });
            Matrix xp = new Matrix(new double[,] { { 1 }, { 1 } });
            Matrix xn = new Matrix(new double[,] { { 0 }, { 0 } });
            Assert.AreEqual(a.CompleteSolution(b, out Matrix pr, out Matrix xpr, out Matrix xnr), 0);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(xp.CompareTo(xpr), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * (xpr + xnr * new Matrix(xnr.N, 1, new Random()))).CompareTo(p * b), 0);
        }

        [TestMethod]
        public void CompleteSolution3Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 2 }, { 1, 2, 3 } });
            Matrix b = new Matrix(new double[,] { { 3 }, { 5 }, { 6 } });
            Matrix xp = new Matrix(new double[,] { { 1 }, { 1 }, { 1 } });
            Matrix xn = new Matrix(new double[,] { { 0 }, { 0 }, { 0 } });
            Assert.AreEqual(a.CompleteSolution(b, out Matrix pr, out Matrix xpr, out Matrix xnr), 0);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(xp.CompareTo(xpr), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * (xpr + xnr * new Matrix(xnr.N, 1, new Random()))).CompareTo(p * b), 0);
        }

        //TODO
        [TestMethod]
        public void CompleteSolution4Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 0, 1 }, { 0, 1, 0 } });
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 2, 5 }, { 4, 6, 8 } });
            Matrix b = new Matrix(new double[,] { { 3 }, { 9 }, { 18 } });
            Matrix xp = new Matrix(new double[,] { { 1 }, { 1 }, { 1 } });
            Matrix xn = new Matrix(new double[,] { { 0 }, { 0 }, { 0 } });
            Assert.AreEqual(a.CompleteSolution(b, out Matrix pr, out Matrix xpr, out Matrix xnr), 0);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(xp.CompareTo(xpr), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * (xpr + xnr * new Matrix(xnr.N, 1, new Random()))).CompareTo(p * b), 0);
        }

        [TestMethod]
        public void CompleteSolution5Test()
        {
            Matrix xnr, xpr, pr;
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 3, 3, 2 }, { 2, 6, 9, 7 }, { -1, -3, 3, 4 } });
            Matrix xn = new Matrix(new double[,] { { -3, 1 }, { 1, 0 }, { 0, -1 }, { 0, 1 } });
            Matrix b = new Matrix(new double[,] { { 8 }, { 24 }, { 3 } });
            Assert.AreNotEqual(a.CompleteSolution(b, out pr, out xpr, out xnr), 0);
            b = new Matrix(new double[,] { { 9 }, { 24 }, { 3 } });
            Matrix xp = new Matrix(new double[,] { { 3 }, { 0 }, { 2 }, { 0 } });
            Assert.AreEqual(a.CompleteSolution(b, out pr, out xpr, out xnr), 0);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(xp.CompareTo(xpr), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * (xpr + xnr * new Matrix(xnr.N, 1, new Random()))).CompareTo(p * b), 0);
        }

        [TestMethod]
        public void CompleteSolution6Test()
        {
            Matrix xnr, xpr, pr;
            Matrix p = new Matrix(new double[,] { { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, -1 }, { 3, 6, -3 }, { 3, 9, 3 }, { 2, 7, 4 } });
            Matrix xn = new Matrix(new double[,] { { 5 }, { -2 }, { 1 } });
            Matrix b = new Matrix(new double[,] { { 1 }, { 6 }, { 15 }, { 13 } });
            Assert.AreNotEqual(a.CompleteSolution(b, out pr, out xpr, out xnr), 0);
            b = new Matrix(new double[,] { { 2 }, { 6 }, { 15 }, { 13 } });
            Matrix xp = new Matrix(new double[,] { { -4 }, { 3 }, { 0 } });
            Assert.AreEqual(a.CompleteSolution(b, out pr, out xpr, out xnr), 0);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(xp.CompareTo(xpr), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * (xpr + xnr * new Matrix(xnr.N, 1, new Random()))).CompareTo(p * b), 0);
        }

        [TestMethod]
        public void CompleteSolution7Test()
        {
            Matrix xnr, xpr, pr;
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, 4, 3, 5 }, { 2, 4, 7, 9, 12 }, { 3, 6, 13, 6, 13 } });
            Matrix xn = new Matrix(new double[,] { { -2, -15, -13 }, { 1, 0, 0 }, { 0, 3, 2 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix b = new Matrix(new double[,] { { 14 }, { 34 }, { 41 } });
            Assert.AreNotEqual(a.CompleteSolution(b, out pr, out xpr, out xnr), 0);
            b = new Matrix(new double[,] { { 15 }, { 34 }, { 41 } });
            Matrix xp = new Matrix(new double[,] { { 31 }, { 0 }, { -4 }, { 0 }, { 0 } });
            Assert.AreEqual(a.CompleteSolution(b, out pr, out xpr, out xnr), 0);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(xp.CompareTo(xpr), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * (xpr + xnr * new Matrix(xnr.N, 1, new Random()))).CompareTo(p * b), 0);
        }

        [TestMethod]
        public void CompleteSolution8Test()
        {
            Matrix xnr, xpr, pr;
            Matrix p = new Matrix(new double[,] { { 1, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 1, 0, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, 3 }, { 2, 4, 6 }, { 4, 7, 13 }, { 3, 9, 6 }, { 5, 12, 13 } });
            Matrix xn = new Matrix(new double[,] { { -5 }, { 1 }, { 1 } });
            Matrix b = new Matrix(new double[,] { { 5 }, { 12 }, { 24 }, { 18 }, { 30 } });
            Assert.AreNotEqual(a.CompleteSolution(b, out pr, out xpr, out xnr), 0);
            b = new Matrix(new double[,] { { 6 }, { 12 }, { 24 }, { 18 }, { 30 } });
            Matrix xp = new Matrix(new double[,] { { 6 }, { 0 }, { 0 } });
            Assert.AreEqual(a.CompleteSolution(b, out pr, out xpr, out xnr), 0);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(xp.CompareTo(xpr), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * (xpr + xnr * new Matrix(xnr.N, 1, new Random()))).CompareTo(p * b), 0);
        }

        [TestMethod]
        public void NullSolution0Test()
        {
            Matrix p = new Matrix(new double[,] { { 1 } });
            Matrix a = new Matrix(new double[,] { { 0 } });
            Matrix re = new Matrix(new double[,] { { 1 } });
            Matrix xn = new Matrix(new double[,] { { 1 } });
            a.NullSpaceSolution(out Matrix pr, out Matrix rer, out Matrix xnr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(rer.CompareTo(re), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * xnr * new Matrix(xnr.N, 1, new Random())).CompareTo(new Matrix(a.M, 1)), 0);
        }

        [TestMethod]
        public void NullSolution1Test()
        {
            Matrix p = new Matrix(new double[,] { { 1 } });
            Matrix a = new Matrix(new double[,] { { 1 } });
            Matrix re = new Matrix(new double[,] { { 1 } });
            Matrix xn = new Matrix(new double[,] { { 0 } });
            a.NullSpaceSolution(out Matrix pr, out Matrix rer, out Matrix xnr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(rer.CompareTo(re), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * xnr * new Matrix(xnr.N, 1, new Random())).CompareTo(new Matrix(a.M, 1)), 0);
        }

        [TestMethod]
        public void NullSolution2Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0 }, { 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 2, 1 }, { 8, 7 } });
            Matrix re = new Matrix(new double[,] { { 2, 1 }, { 8, 7 } });
            Matrix xn = new Matrix(new double[,] { { 0 }, { 0 } });
            a.NullSpaceSolution(out Matrix pr, out Matrix rer, out Matrix xnr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(rer.CompareTo(re), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * xnr * new Matrix(xnr.N, 1, new Random())).CompareTo(new Matrix(a.M, 1)), 0);
        }
        
        [TestMethod]
        public void NullSolution3Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 2 }, { 1, 2, 3 } });
            Matrix re = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 2 }, { 1, 2, 3 } });
            Matrix xn = new Matrix(new double[,] { { 0 }, { 0 }, { 0 } });
            a.NullSpaceSolution(out Matrix pr, out Matrix rer, out Matrix xnr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(rer.CompareTo(re), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * xnr * new Matrix(xnr.N, 1, new Random())).CompareTo(new Matrix(a.M, 1)), 0);
        }
        
        [TestMethod]
        public void NullSolution4Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 0, 1 }, { 0, 1, 0 } });
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 2, 5 }, { 4, 6, 8 } });
            Matrix re = new Matrix(new double[,] { { 1, 1, 1 }, { 4, 6, 8 }, { 2, 2, 5 } });
            Matrix xn = new Matrix(new double[,] { { 0 }, { 0 }, { 0 } });
            a.NullSpaceSolution(out Matrix pr, out Matrix rer, out Matrix xnr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(rer.CompareTo(re), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * xnr * new Matrix(xnr.N, 1, new Random())).CompareTo(new Matrix(a.M, 1)), 0);
        }

        [TestMethod]
        public void NullSolution5Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 3, 3, 2 }, { 2, 6, 9, 7 }, { -1, -3, 3, 4 } });
            Matrix re = new Matrix(new double[,] { { 1, 3, 0 }, { 2, 9, 0 }, { -1, 3, 1 } });
            Matrix xn = new Matrix(new double[,] { { -3, 1 }, { 1, 0 }, { 0, -1 }, { 0, 1 } });
            a.NullSpaceSolution(out Matrix pr, out Matrix rer, out Matrix xnr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(rer.CompareTo(re), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * xnr * new Matrix(xnr.N, 1, new Random())).CompareTo(new Matrix(a.M, 1)), 0);
        }

        [TestMethod]
        public void NullSolution6Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, -1 }, { 3, 6, -3 }, { 3, 9, 3 }, { 2, 7, 4 } });
            Matrix re = new Matrix(new double[,] { { 1, 2, 0, 0 }, { 3, 9, 0, 0 }, { 3, 6, 1, 0 }, { 2, 7, 0, 1 } });
            Matrix xn = new Matrix(new double[,] { { 5 }, { -2 }, { 1 } });
            a.NullSpaceSolution(out Matrix pr, out Matrix rer, out Matrix xnr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(rer.CompareTo(re), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * xnr * new Matrix(xnr.N, 1, new Random())).CompareTo(new Matrix(a.M, 1)), 0);
        }

        [TestMethod]
        public void NullSolution7Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, 4, 3, 5 }, { 2, 4, 7, 9, 12 }, { 3, 6, 13, 6, 13 } });
            Matrix re = new Matrix(new double[,] { { 1, 4, 0 }, { 2, 7, 0 }, { 3, 13, 1 } });
            Matrix xn = new Matrix(new double[,] { { -2, -15, -13 }, { 1, 0, 0 }, { 0, 3, 2 }, { 0, 1, 0 }, { 0, 0, 1 } });
            a.NullSpaceSolution(out Matrix pr, out Matrix rer, out Matrix xnr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(rer.CompareTo(re), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * xnr * new Matrix(xnr.N, 1, new Random())).CompareTo(new Matrix(a.M, 1)), 0);
        }
        
        [TestMethod]
        public void NullSolution8Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 1, 0, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, 3 }, { 2, 4, 6 }, { 4, 7, 13 }, { 3, 9, 6 }, { 5, 12, 13 } });
            Matrix re = new Matrix(new double[,] { { 1, 2, 0, 0, 0 }, { 4, 7, 0, 0, 0 }, { 2, 4, 1, 0, 0 }, { 3, 9, 0, 1, 0 }, { 5, 12, 0, 0, 1 } });
            Matrix xn = new Matrix(new double[,] { { -5 }, { 1 }, { 1 } });
            a.NullSpaceSolution(out Matrix pr, out Matrix rer, out Matrix xnr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(rer.CompareTo(re), 0);
            Assert.AreEqual(xn.CompareTo(xnr), 0);
            Assert.AreEqual((p * a * xnr * new Matrix(xnr.N, 1, new Random())).CompareTo(new Matrix(a.M, 1)), 0);
        }

        [TestMethod]
        public void Reverse0Test()
        {
            Matrix a = new Matrix(new double[,] { { 0 } });
            Assert.AreNotEqual(a.Reverse(out Matrix _), 0);
        }

        [TestMethod]
        public void Reverse1Test()
        {
            Matrix a = new Matrix(new double[,] { { 1 } });
            Matrix r = new Matrix(new double[,] { { 1 } });
            Assert.AreEqual(a.Reverse(out Matrix rr), 0);
            Assert.AreEqual(r.CompareTo(rr), 0);
            Assert.AreEqual((a*rr).CompareTo(new Matrix(a.N, true)), 0);
        }

        [TestMethod]
        public void Reverse2Test()
        {
            Matrix a = new Matrix(new double[,] { { 1, 0 }, { 0, 1 } });
            Matrix r = new Matrix(new double[,] { { 1, 0 }, { 0, 1 } });
            Assert.AreEqual(a.Reverse(out Matrix rr), 0);
            Assert.AreEqual(r.CompareTo(rr), 0);
            Assert.AreEqual((a * rr).CompareTo(new Matrix(a.N, true)), 0);
        }

        [TestMethod]
        public void Reverse3Test()
        {
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                Matrix r = new Matrix(10, 10, rand);
                Matrix a = (Matrix)r.Clone();
                Assert.AreEqual(r.Reverse(out Matrix rReverse), 0);
                Assert.AreEqual((a * rReverse).CompareTo(new Matrix(a.M, true)), 0);
                Assert.AreEqual(rReverse.Reverse(out Matrix rReverseTwice), 0);
                Assert.AreEqual(a.CompareTo(rReverseTwice), 0);
            }
        }

        [TestMethod]
        public void Reverse4Test()
        {
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 2 }, { 1, 2, 3 } });
            Matrix r = new Matrix(new double[,] { { 2, -1, 0 }, { -1, 2, -1 }, { 0, -1, 1 } });
            Assert.AreEqual(a.Reverse(out Matrix rr), 0);
            Assert.AreEqual(r.CompareTo(rr), 0);
            Assert.AreEqual((a * rr).CompareTo(new Matrix(a.N, true)), 0);
        }

        [TestMethod]
        public void Reverse5Test()
        {
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 2, 5 }, { 4, 6, 8 } });
            Matrix r = new Matrix(new double[,] { { 7.0 / 3, 1.0 / 3, -0.5 }, { -2.0 / 3, -2.0 / 3, 0.5 }, { -2.0 / 3, 1.0 / 3, 0 } });
            Assert.AreEqual(a.Reverse(out Matrix rr), 0);
            Assert.AreEqual(r.CompareTo(rr), 0);
            Assert.AreEqual((a * rr).CompareTo(new Matrix(a.N, true)), 0);
        }

        [TestMethod]
        public void EchelonForm0Test()
        {
            Matrix p = new Matrix(new double[,] { { 1 } });
            Matrix a = new Matrix(new double[,] { { 0 } });
            Matrix re = new Matrix(new double[,] { { 1 } });
            Matrix e = new Matrix(new double[,] { { 0 } });
            int[] pivots = { };
            a.EchelonForm(out Matrix pr, out Matrix rer, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * e).CompareTo(rer * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * e), 0);
        }

        [TestMethod]
        public void EchelonForm1Test()
        {
            Matrix p = new Matrix(new double[,] { { 1 } });
            Matrix a = new Matrix(new double[,] { { 1 } });
            Matrix re = new Matrix(new double[,] { { 1 } });
            Matrix e = new Matrix(new double[,] { { 1 } });
            int[] pivots = { 0 };
            a.EchelonForm(out Matrix pr, out Matrix rer, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * e).CompareTo(rer * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * e), 0);
        }

        [TestMethod]
        public void EchelonForm2Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0 }, { 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 2, 1 }, { 8, 7 } });
            Matrix re = new Matrix(new double[,] { { 1, 0 }, { 4, 1 } });
            Matrix e = new Matrix(new double[,] { { 2, 1 }, { 0, 3 } });
            int[] pivots = { 0, 1 };
            a.EchelonForm(out Matrix pr, out Matrix rer, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * e).CompareTo(rer * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * e), 0);
        }

        [TestMethod]
        public void EchelonForm3Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 2 }, { 1, 2, 3 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0 }, { 1, 1, 0 }, { 1, 1, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 1, 1 }, { 0, 1, 1 }, { 0, 0, 1 } });
            int[] pivots = { 0, 1, 2 };
            a.EchelonForm(out Matrix pr, out Matrix rer, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * e).CompareTo(rer * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * e), 0);
        }

        [TestMethod]
        public void EchelonForm4Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 0, 1 }, { 0, 1, 0 } });
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 2, 5 }, { 4, 6, 8 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0 }, { 4, 1, 0 }, { 2, 0, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 1, 1 }, { 0, 2, 4 }, { 0, 0, 3 } });
            int[] pivots = { 0, 1, 2 };
            a.EchelonForm(out Matrix pr, out Matrix rer, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * e).CompareTo(rer * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * e), 0);
        }

        [TestMethod]
        public void EchelonForm5Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 3, 3, 2 }, { 2, 6, 9, 7 }, { -1, -3, 3, 4 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0 }, { 2, 1, 0 }, { -1, 2, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 3, 3, 2 }, { 0, 0, 3, 3 }, { 0, 0, 0, 0 } });
            int[] pivots = { 0, 2 };
            a.EchelonForm(out Matrix pr, out Matrix rer, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * e).CompareTo(rer * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * e), 0);
        }

        [TestMethod]
        public void EchelonForm6Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, -1 }, { 3, 6, -3 }, { 3, 9, 3 }, { 2, 7, 4 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0, 0 }, { 3, 1, 0, 0 }, { 3, 0, 1, 0 }, { 2, 1, 0, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 2, -1 }, { 0, 3, 6 }, { 0, 0, 0 }, { 0, 0, 0 } });
            int[] pivots = { 0, 1 };
            a.EchelonForm(out Matrix pr, out Matrix rer, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * e).CompareTo(rer * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * e), 0);
        }

        [TestMethod]
        public void EchelonForm7Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, 4, 3, 5 }, { 2, 4, 7, 9, 12 }, { 3, 6, 13, 6, 13 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0 }, { 2, 1, 0 }, { 3, -1, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 2, 4, 3, 5 }, { 0, 0, -1, 3, 2 }, { 0, 0, 0, 0, 0 } });
            int[] pivots = { 0, 2 };
            a.EchelonForm(out Matrix pr, out Matrix rer, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * e).CompareTo(rer * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * e), 0);
        }

        [TestMethod]
        public void EchelonForm8Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 1, 0, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, 3 }, { 2, 4, 6 }, { 4, 7, 13 }, { 3, 9, 6 }, { 5, 12, 13 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0, 0, 0 }, { 4, 1, 0, 0, 0 }, { 2, 0, 1, 0, 0 }, { 3, -3, 0, 1, 0 }, { 5, -2, 0, 0, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 2, 3 }, { 0, -1, 1 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } });
            int[] pivots = { 0, 1 };
            a.EchelonForm(out Matrix pr, out Matrix rer, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * e).CompareTo(rer * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * e), 0);
        }

        [TestMethod]
        public void UniEchelonForm0Test()
        {
            Matrix p = new Matrix(new double[,] { { 1 } });
            Matrix a = new Matrix(new double[,] { { 0 } });
            Matrix re = new Matrix(new double[,] { { 1 } });
            Matrix d = new Matrix(new double[,] { { 1 } });
            Matrix e = new Matrix(new double[,] { { 0 } });
            int[] pivots = { };
            a.UniEchelonForm(out Matrix pr, out Matrix rer, out Matrix dr, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(d.CompareTo(dr), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * d * e).CompareTo(rer * dr * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * d * e), 0);
        }

        [TestMethod]
        public void UniEchelonForm1Test()
        {
            Matrix p = new Matrix(new double[,] { { 1 } });
            Matrix a = new Matrix(new double[,] { { 1 } });
            Matrix re = new Matrix(new double[,] { { 1 } });
            Matrix d = new Matrix(new double[,] { { 1 } });
            Matrix e = new Matrix(new double[,] { { 1 } });
            int[] pivots = { 0 };
            a.UniEchelonForm(out Matrix pr, out Matrix rer, out Matrix dr, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(d.CompareTo(dr), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * d * e).CompareTo(rer * dr * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * d * e), 0);
        }

        [TestMethod]
        public void UniEchelonForm2Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0 }, { 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 2, 1 }, { 8, 7 } });
            Matrix re = new Matrix(new double[,] { { 1, 0 }, { 4, 1 } });
            Matrix d = new Matrix(new double[,] { { 2, 0 }, { 0, 3 } });
            Matrix e = new Matrix(new double[,] { { 1, 0.5 }, { 0, 1 } });
            int[] pivots = { 0, 1 };
            a.UniEchelonForm(out Matrix pr, out Matrix rer, out Matrix dr, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(d.CompareTo(dr), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * d * e).CompareTo(rer * dr * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * d * e), 0);
        }

        [TestMethod]
        public void UniEchelonForm3Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 2 }, { 1, 2, 3 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0 }, { 1, 1, 0 }, { 1, 1, 1 } });
            Matrix d = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 1, 1 }, { 0, 1, 1 }, { 0, 0, 1 } });
            int[] pivots = { 0, 1, 2 };
            a.UniEchelonForm(out Matrix pr, out Matrix rer, out Matrix dr, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(d.CompareTo(dr), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * d * e).CompareTo(rer * dr * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * d * e), 0);
        }

        [TestMethod]
        public void UniEchelonForm4Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 0, 1 }, { 0, 1, 0 } });
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 2, 5 }, { 4, 6, 8 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0 }, { 4, 1, 0 }, { 2, 0, 1 } });
            Matrix d = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 2, 0 }, { 0, 0, 3 } });
            Matrix e = new Matrix(new double[,] { { 1, 1, 1 }, { 0, 1, 2 }, { 0, 0, 1 } });
            int[] pivots = { 0, 1, 2 };
            a.UniEchelonForm(out Matrix pr, out Matrix rer, out Matrix dr, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(d.CompareTo(dr), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * d * e).CompareTo(rer * dr * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * d * e), 0);
        }

        [TestMethod]
        public void UniEchelonForm5Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 3, 3, 2 }, { 2, 6, 9, 7 }, { -1, -3, 3, 4 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0 }, { 2, 1, 0 }, { -1, 2, 1 } });
            Matrix d = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 3, 0 }, { 0, 0, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 3, 3, 2 }, { 0, 0, 1, 1 }, { 0, 0, 0, 0 } });
            int[] pivots = { 0, 2 };
            a.UniEchelonForm(out Matrix pr, out Matrix rer, out Matrix dr, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(d.CompareTo(dr), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * d * e).CompareTo(rer * dr * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * d * e), 0);
        }

        [TestMethod]
        public void UniEchelonForm6Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, -1 }, { 3, 6, -3 }, { 3, 9, 3 }, { 2, 7, 4 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0, 0 }, { 3, 1, 0, 0 }, { 3, 0, 1, 0 }, { 2, 1, 0, 1 } });
            Matrix d = new Matrix(new double[,] { { 1, 0, 0, 0 }, { 0, 3, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 2, -1 }, { 0, 1, 2 }, { 0, 0, 0 }, { 0, 0, 0 } });
            int[] pivots = { 0, 1 };
            a.UniEchelonForm(out Matrix pr, out Matrix rer, out Matrix dr, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(d.CompareTo(dr), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * d * e).CompareTo(rer * dr * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * d * e), 0);
        }

        [TestMethod]
        public void UniEchelonForm7Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, 4, 3, 5 }, { 2, 4, 7, 9, 12 }, { 3, 6, 13, 6, 13 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0 }, { 2, 1, 0 }, { 3, -1, 1 } });
            Matrix d = new Matrix(new double[,] { { 1, 0, 0 }, { 0, -1, 0 }, { 0, 0, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 2, 4, 3, 5 }, { 0, 0, 1, -3, -2 }, { 0, 0, 0, 0, 0 } });
            int[] pivots = { 0, 2 };
            a.UniEchelonForm(out Matrix pr, out Matrix rer, out Matrix dr, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(d.CompareTo(dr), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * d * e).CompareTo(rer * dr * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * d * e), 0);
        }

        [TestMethod]
        public void UniEchelonForm8Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 1, 0, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, 3 }, { 2, 4, 6 }, { 4, 7, 13 }, { 3, 9, 6 }, { 5, 12, 13 } });
            Matrix re = new Matrix(new double[,] { { 1, 0, 0, 0, 0 }, { 4, 1, 0, 0, 0 }, { 2, 0, 1, 0, 0 }, { 3, -3, 0, 1, 0 }, { 5, -2, 0, 0, 1 } });
            Matrix d = new Matrix(new double[,] { { 1, 0, 0, 0, 0 }, { 0, -1, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1 } });
            Matrix e = new Matrix(new double[,] { { 1, 2, 3 }, { 0, 1, -1 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } });
            int[] pivots = { 0, 1 };
            a.UniEchelonForm(out Matrix pr, out Matrix rer, out Matrix dr, out Matrix er, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(d.CompareTo(dr), 0);
            Assert.AreEqual(e.CompareTo(er), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * d * e).CompareTo(rer * dr * er), 0);
            Assert.AreEqual((p * a).CompareTo(re * d * e), 0);
        }

        [TestMethod]
        public void RowReducedEchelonForm0Test()
        {
            Matrix p = new Matrix(new double[,] { { 1 } });
            Matrix a = new Matrix(new double[,] { { 0 } });
            Matrix re = new Matrix(new double[,] { { 1 } });
            Matrix rref = new Matrix(new double[,] { { 0 } });
            int[] pivots = { };
            a.RowReducedEchelonForm(out Matrix pr, out Matrix rer, out Matrix rrefr, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(rref.CompareTo(rrefr), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * rref).CompareTo(rer * rrefr), 0);
            Assert.AreEqual((p * a).CompareTo(re * rref), 0);
        }

        [TestMethod]
        public void RowReducedEchelonForm1Test()
        {
            Matrix p = new Matrix(new double[,] { { 1 } });
            Matrix a = new Matrix(new double[,] { { 1 } });
            Matrix re = new Matrix(new double[,] { { 1 } });
            Matrix rref = new Matrix(new double[,] { { 1 } });
            int[] pivots = { 0 };
            a.RowReducedEchelonForm(out Matrix pr, out Matrix rer, out Matrix rrefr, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(rref.CompareTo(rrefr), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * rref).CompareTo(rer * rrefr), 0);
            Assert.AreEqual((p * a).CompareTo(re * rref), 0);
        }

        [TestMethod]
        public void RowReducedEchelonForm2Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0 }, { 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 2, 1 }, { 8, 7 } });
            Matrix re = new Matrix(new double[,] { { 2, 1 }, { 8, 7 } });
            Matrix rref = new Matrix(new double[,] { { 1, 0 }, { 0, 1 } });
            int[] pivots = { 0, 1 };
            a.RowReducedEchelonForm(out Matrix pr, out Matrix rer, out Matrix rrefr, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(rref.CompareTo(rrefr), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * rref).CompareTo(rer * rrefr), 0);
            Assert.AreEqual((p * a).CompareTo(re * rref), 0);
        }

        [TestMethod]
        public void RowReducedEchelonForm3Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 2 }, { 1, 2, 3 } });
            Matrix re = new Matrix(new double[,] { { 1, 1, 1 }, { 1, 2, 2 }, { 1, 2, 3 } });
            Matrix rref = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            int[] pivots = { 0, 1, 2 };
            a.RowReducedEchelonForm(out Matrix pr, out Matrix rer, out Matrix rrefr, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(rref.CompareTo(rrefr), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * rref).CompareTo(rer * rrefr), 0);
            Assert.AreEqual((p * a).CompareTo(re * rref), 0);
        }

        [TestMethod]
        public void RowReducedEchelonForm4Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 0, 1 }, { 0, 1, 0 } });
            Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 2, 5 }, { 4, 6, 8 } });
            Matrix re = new Matrix(new double[,] { { 1, 1, 1 }, { 4, 6, 8 }, { 2, 2, 5 } });
            Matrix rref = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            int[] pivots = { 0, 1, 2 };
            a.RowReducedEchelonForm(out Matrix pr, out Matrix rer, out Matrix rrefr, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(rref.CompareTo(rrefr), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * rref).CompareTo(rer * rrefr), 0);
            Assert.AreEqual((p * a).CompareTo(re * rref), 0);
        }

        [TestMethod]
        public void RowReducedEchelonForm5Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 3, 3, 2 }, { 2, 6, 9, 7 }, { -1, -3, 3, 4 } });
            Matrix re = new Matrix(new double[,] { { 1, 3, 0 }, { 2, 9, 0 }, { -1, 3, 1} });
            Matrix rref = new Matrix(new double[,] { { 1, 3, 0, -1 }, { 0, 0, 1, 1 }, { 0, 0, 0, 0 } });
            int[] pivots = { 0, 2 };
            a.RowReducedEchelonForm(out Matrix pr, out Matrix rer, out Matrix rrefr, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(rref.CompareTo(rrefr), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * rref).CompareTo(rer * rrefr), 0);
            Assert.AreEqual((p * a).CompareTo(re * rref), 0);
        }

        [TestMethod]
        public void RowReducedEchelonForm6Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, -1 }, { 3, 6, -3 }, { 3, 9, 3 }, { 2, 7, 4 } });
            Matrix re = new Matrix(new double[,] { { 1, 2, 0, 0 }, { 3, 9, 0, 0 }, { 3, 6, 1, 0 }, { 2, 7, 0, 1 } });
            Matrix rref = new Matrix(new double[,] { { 1, 0, -5 }, { 0, 1, 2 }, { 0, 0, 0 }, { 0, 0, 0 } });
            int[] pivots = { 0, 1 };
            a.RowReducedEchelonForm(out Matrix pr, out Matrix rer, out Matrix rrefr, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(rref.CompareTo(rrefr), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * rref).CompareTo(rer * rrefr), 0);
            Assert.AreEqual((p * a).CompareTo(re * rref), 0);
        }

        [TestMethod]
        public void RowReducedEchelonForm7Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, 4, 3, 5 }, { 2, 4, 7, 9, 12 }, { 3, 6, 13, 6, 13 } });
            Matrix re = new Matrix(new double[,] { { 1, 4, 0 }, { 2, 7, 0 }, { 3, 13, 1 } });
            Matrix rref = new Matrix(new double[,] { { 1, 2, 0, 15, 13 }, { 0, 0, 1, -3, -2 }, { 0, 0, 0, 0, 0 } });
            int[] pivots = { 0, 2 };
            a.RowReducedEchelonForm(out Matrix pr, out Matrix rer, out Matrix rrefr, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(rref.CompareTo(rrefr), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * rref).CompareTo(rer * rrefr), 0);
            Assert.AreEqual((p * a).CompareTo(re * rref), 0);
        }

        [TestMethod]
        public void RowReducedEchelonForm8Test()
        {
            Matrix p = new Matrix(new double[,] { { 1, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 1, 0, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1 } });
            Matrix a = new Matrix(new double[,] { { 1, 2, 3 }, { 2, 4, 6 }, { 4, 7, 13 }, { 3, 9, 6 }, { 5, 12, 13 } });
            Matrix re = new Matrix(new double[,] { { 1, 2, 0, 0, 0 }, { 4, 7, 0, 0, 0 }, { 2, 4, 1, 0, 0 }, { 3, 9, 0, 1, 0 }, { 5, 12, 0, 0, 1 } });
            Matrix rref = new Matrix(new double[,] { { 1, 0, 5 }, { 0, 1, -1 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } });
            int[] pivots = {0, 1};
            a.RowReducedEchelonForm(out Matrix pr, out Matrix rer, out Matrix rrefr, out int[] pivotsr);
            Assert.AreEqual(p.CompareTo(pr), 0);
            Assert.AreEqual(re.CompareTo(rer), 0);
            Assert.AreEqual(rref.CompareTo(rrefr), 0);
            Assert.AreEqual(pivots.Length.CompareTo(pivotsr.Length), 0);
            for (int i = 0; i < pivots.Length; i++)
            {
                Assert.AreEqual(pivots[i].Equals(pivotsr[i]), true);
            }
            Assert.AreEqual((p * a).CompareTo(pr * a), 0);
            Assert.AreEqual((re * rref).CompareTo(rer * rrefr), 0);
            Assert.AreEqual((p * a).CompareTo(re * rref), 0);
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