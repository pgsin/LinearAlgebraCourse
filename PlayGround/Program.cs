﻿using System;
using LinalLib;


namespace PlayGround
{
    class Program
    {
        static void Main(string[] args)
        {
            //Matrix a = new Matrix(new double[,] { { 1, 2, 0}, { 2, 4, 1 }, { 2, 1, 0 } });
            Matrix a = new Matrix(new double[,] { { 1, 3, 3, 2 }, { 2, 6, 9, 7 }, { -1, -3, 3, 4 } });
            Matrix b = new Matrix(new double[,] {{1}, {5}, {5}});
            a.CompleteSolution(b, out var _, out var _, out var _);
            //Matrix shivani = new Matrix(new double[,] { { 2, 3, 3 }, { 0, 5, 7 }, { 6, 9, 8 } });
            //Console.WriteLine(shivani);
            //shivani.Reverse(out var rshivani);
            //Console.WriteLine(rshivani);
            //Console.ReadLine();
            //Matrix peli = new Matrix(new double[,] { { 2, -3, 0 }, { 4, -5, 1 }, { 2, -1, -3 } });
            //Console.WriteLine(peli);
            //shivani.Reverse(out var rpeli);
            //Console.WriteLine(rpeli);
            //Console.ReadLine();
            //Matrix julie = new Matrix(new double[,] { { 2, 1, 1 }, { 4, -6, 0 }, { -2, 7, 2 } });
            //Console.WriteLine(julie);
            //shivani.Reverse(out var rjulie);
            //Console.WriteLine(rjulie);
            //Console.ReadLine();
            //Matrix a = new Matrix(new double[,] { { 1, 1, 1 }, { 2, 2, 5 }, { 4, 6, 8 } });
            //Console.WriteLine(a);
            //a.Reverse(out var aRev);
            //Console.WriteLine((a * aRev).CompareTo(aRev * a) == 0);
            //Console.WriteLine((a * aRev).CompareTo(new Matrix(a.M, true)) == 0);
            //a.PALU_factorization(out var l0, out var p0, out var u0);
            //a.Transpose(out var aTra);
            //aTra.Transpose();
            //Console.WriteLine(a.CompareTo(aTra) == 0);
            //Matrix b = new Matrix(new double[,] { { 3 }, { 9 }, { 18 } });
            //a.GaussElimination(b, out var x);
            //Console.WriteLine((a * x).CompareTo(b) == 0);
            //Console.ReadLine();
        }
    }
}
