using System;
using LinalLib;

namespace PlayGround
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix a = new Matrix(new double[,] {{1, 1, 1}, {2, 2, 5}, {4, 6, 8}});
            Console.WriteLine(a);
            a.Reverse(out var aRev);
            Console.WriteLine((a * aRev).CompareTo(aRev * a) == 0);
            Console.WriteLine((a * aRev).CompareTo(new Matrix(a.N, true)) == 0);
            a.PALU_factorization(out var l0, out var p0, out var u0);
            a.EPAU_factorization(out var el, out var pl, out var ul);
            Console.WriteLine(p0.CompareTo(pl) == 0);
            Console.WriteLine(u0.CompareTo(ul) == 0);
            l0.Reverse(out var lRev);
            Console.WriteLine(lRev.CompareTo(el) == 0);
            a.Transpose(out var aTra);
            aTra.Transpose();
            Console.WriteLine(a.CompareTo(aTra) == 0);
            Matrix b = new Matrix(new double[,] {{3}, {9}, {18}});
            a.GaussElimination(b, out var x);
            Console.WriteLine((a * x).CompareTo(b) == 0);
            Console.ReadLine();
        }
    }
}
