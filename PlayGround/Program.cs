﻿using System;
using System.Runtime.InteropServices;
using LinalLib;

namespace PlayGround
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix a = new Matrix(new double[,] {{1, 1, 1}, {2, 2, 5}, {4, 6, 8}});
            Console.WriteLine(a);
            a.ReverseL(out var aRev);
            Console.WriteLine((a * aRev).CompareTo(aRev * a) == 0);
            Console.WriteLine((a * aRev).CompareTo(new Matrix(a.N, true)) == 0);
            a.PALU_factorization(out var l0, out var p0, out var u0);
            a.EPAU_factorizationL(out var el, out var pl, out var ul);
            a.EPAU_factorizationE(out var ee, out var pe, out var ue);
            Console.WriteLine(el.CompareTo(ee) == 0);
            Console.WriteLine(p0.CompareTo(pl) == 0);
            Console.WriteLine(p0.CompareTo(pe) == 0);
            Console.WriteLine(u0.CompareTo(ul) == 0);
            Console.WriteLine(u0.CompareTo(ue) == 0);
            l0.ReverseL(out var lRev);
            l0.ReverseE(out var lRev0);
            Console.WriteLine(lRev.CompareTo(ee) == 0);
            Console.WriteLine(lRev.CompareTo(lRev0) == 0);
            a.Transpose(out var aTra);
            aTra.Transpose();
            Console.WriteLine(a.CompareTo(aTra) == 0);
            Matrix b = new Matrix(new double[,] {{3}, {9}, {18}});
            a.GaussEliminationL(b, out var x);
            a.GaussEliminationE(b, out var xl);
            Console.WriteLine((a * x).CompareTo(b) == 0);
            Console.WriteLine((xl).CompareTo(x) == 0);
            Console.ReadLine();
        }
    

    //private static void TestFactorization()
    //    {
    //        Console.WriteLine("Test Factorization");
    //        Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 2, 5 }, { 4, 6, 8 } });
    //        Console.WriteLine("A");
    //        Console.WriteLine(a);
    //        Console.WriteLine("__________________________");
    //        Matrix e, p, u, l;
    //        if (a.EPAU_factorization(out e, out p, out u) == 0)
    //        {
    //            Console.WriteLine("EPA=U");
    //            Console.WriteLine("E");
    //            Console.WriteLine(e);
    //            Console.WriteLine("");
    //            Console.WriteLine("P");
    //            Console.WriteLine(p);
    //            Console.WriteLine("");
    //            Console.WriteLine("U");
    //            Console.WriteLine(u);
    //            Console.WriteLine("__________________________");
    //        }
            

    //        a.PALU_factorization(out l, out p, out u);
    //        Console.WriteLine("PA=LU");
    //        Console.WriteLine("P");
    //        Console.WriteLine(p);
    //        Console.WriteLine("");
    //        Console.WriteLine("L");
    //        Console.WriteLine(l);
    //        Console.WriteLine("");
    //        Console.WriteLine("U");
    //        Console.WriteLine(u);
    //        Console.WriteLine("__________________________");
    //    }

    //    private static void TestFactorization1()
    //    {
    //        Console.WriteLine("Test Factorization");
    //        Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 3, 5 }, { 4, 6, 8 } });
    //        Console.WriteLine("A");
    //        Console.WriteLine(a);
    //        Console.WriteLine("__________________________");
    //        Matrix e, p, u, l;
    //        a.EPAU_factorization(out e, out p, out u);
    //        Console.WriteLine("EPA=U");
    //        Console.WriteLine("E");
    //        Console.WriteLine(e);
    //        Console.WriteLine("");
    //        Console.WriteLine("P");
    //        Console.WriteLine(p);
    //        Console.WriteLine("");
    //        Console.WriteLine("U");
    //        Console.WriteLine(u);
    //        Console.WriteLine("__________________________");

    //        a.PALU_factorization(out l, out p, out u);
    //        Console.WriteLine("PA=LU");
    //        Console.WriteLine("P");
    //        Console.WriteLine(p);
    //        Console.WriteLine("");
    //        Console.WriteLine("L");
    //        Console.WriteLine(l);
    //        Console.WriteLine("");
    //        Console.WriteLine("U");
    //        Console.WriteLine(u);
    //        Console.WriteLine("__________________________");
    //    }

    //    private static void TestFactorization3()
    //    {
    //        Random r = new Random(123);
    //        Matrix a = new Matrix(50, 50, r);
    //        Matrix i = new Matrix(50, true);
    //        Matrix e0, p0, u0;
    //        a.EPAU_factorization(out e0, out p0, out u0);
    //        Matrix l1, p1, u1;
    //        a.PALU_factorization(out l1, out p1, out u1);
    //        Console.WriteLine(p0.CompareTo(p1));
    //        Console.WriteLine(u0.CompareTo(u1));
    //        Console.WriteLine((l1*e0).CompareTo(i));
    //    }
    }
}
