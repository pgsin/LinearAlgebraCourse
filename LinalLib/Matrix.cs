using System;
using System.Text;

namespace LinalLib
{
    public class Matrix : ICloneable, IComparable<Matrix>
    {
        private const double Epsilon = 0.0000000001;
        private readonly double[,] _data;
        public int M => _data.GetUpperBound(0) + 1;
        public int N => _data.GetUpperBound(1) + 1;

        public Matrix(int n, bool diagonal = false)
        {
            _data = new double[n, n];
            if (!diagonal) return;
            for (int i = 0; i < n; i++)
            {
                _data[i, i] = 1.0;
            }
        }

        public Matrix(int n, int m, Random r = null)
        {
            _data = new double[n, m];
            if (r == null) return;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    _data[i, j] = r.NextDouble();
                }
            }
        }

        public Matrix(double[,] data)
        {
            _data = data;
        }

        public ref double this[int row, int column] => ref _data[row, column];

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.N != b.M)
            {
                return null;
            }
            Matrix c = new Matrix(a.M, b.N);
            for (int i = 0; i < c.M; i++)
            {
                for (int j = 0; j < c.N; j++)
                {
                    double s = 0.0;
                    for (int m = 0; m < a.N; m++)
                    {
                        s += a[i, m] * b[m, j];
                    }
                    c[i, j] = s;
                }
            }
            return c;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.N != b.N || a.M != b.M)
            {
                return null;
            }
            Matrix c = new Matrix(a.M, b.N);
            for (int i = 0; i < c.M; i++)
            {
                for (int j = 0; j < c.N; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            return c;
        }

        public void Transpose()
        {
            for (int i = 0; i < M; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    double tmp = _data[i, j];
                    _data[i, j] = _data[j, i];
                    _data[j, i] = tmp;
                }
            }
        }

        public void Transpose(out Matrix m)
        {
            m = new Matrix(M, N);
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    m[i, j] = _data[j, i];
                }
            }
        }

        /// <summary>
        /// Solve Ax=b (general case)
        /// </summary>
        /// <param name="b">right side matrix</param>
        /// <param name="p">row-permutation matrix</param>
        /// <param name="xp">particular solution</param>
        /// <param name="xn">nullspace solution</param>
        /// <returns>0 if success</returns>
        public int CompleteSolution(Matrix b, out Matrix p, out Matrix xp, out Matrix xn)
        {
            if (M != b.M)
            {
                p = null;
                xp = null;
                xn = null;
                return -1;
            }
            ForwardElimination(this, out p, out Matrix lre, out Matrix ue, out int[] pivots);
            DiagonalElimination(ue, pivots, out Matrix d, out Matrix uue);
            UpperBackwardElimination(uue, pivots, out Matrix ure, out var rref);
            LowerBackwardElimination(lre, p*b, out var c);
            UpperBackwardElimination(d*ure, c, out var b0);
            if (pivots.Length == M)
            {
                xn = new Matrix(N, 1);
                xp = b0;
            }
            else
            {
                int m = N, n = N - pivots.Length;
                double[,] xntmp = new double[m, n];
                double[,] xptmp = new double[m, 1];
                int[] free = ExcludedSubset(N, pivots);
                int indf = 0, indp = 0;
                for (int i = 0; i < N; i++)
                {
                    if (indp != pivots.Length && i == pivots[indp])
                    {
                        for (int j = 0; j < free.Length; j++)
                        {
                            xntmp[i, j] = -rref[indf, free[j]];
                        }
                        xptmp[i, 0] = b0[indp, 0];
                        indp++;
                    }
                    else
                    {
                        xntmp[i, indf] = 1;
                        indf++;
                    }
                }
                xn = new Matrix(xntmp);
                xp = new Matrix(xptmp);
            }
            return 0;
        }

        /// <summary>
        /// Solve Ax=0 (general case)
        /// </summary>
        /// <param name="p">row-permutation matrix</param>
        /// <param name="re">reverse elimination matrix</param>
        /// <param name="xn">nullspace solution</param>
        /// <returns>0 if success</returns>
        public void NullSpaceSolution(out Matrix p, out Matrix re, out Matrix xn)
        {
            ForwardElimination(this, out p, out Matrix lre, out Matrix ue, out int[] pivots);
            DiagonalElimination(ue, pivots, out Matrix d, out Matrix uue);
            UpperBackwardElimination(uue, pivots, out Matrix ure, out var rref);
            re = lre * d * ure;
            if (pivots.Length == M)
            {
                xn = new Matrix(N, 1);
            }
            else
            {
                int m = N, n = N - pivots.Length;
                double[,] tmp = new double[m, n];
                int[] free = ExcludedSubset(N, pivots);
                int indf = 0, indp = 0;
                for (int i = 0; i < N; i++)
                {
                    if (indp != pivots.Length && i == pivots[indp])
                    {
                        for (int j = 0; j < free.Length; j++)
                        {
                            tmp[i, j] = -rref[indf, free[j]];
                        }
                        indp++;
                    }
                    else
                    {
                        tmp[i, indf] = 1;
                        indf++;
                    }
                }
                xn = new Matrix(tmp);
            }
        }

        private static int[] ExcludedSubset(int n, int[] s)
        {
            int[] r = new int[n - s.Length];
            int ir = 0;
            for (int i = 1; i < s.Length; i++)
            {
                for (int j = s[i - 1] + 1; j < s[i]; j++)
                {
                    r[ir++] = j;
                }
            }
            for (int i = s[s.Length - 1] + 1; i < n; i++)
            {
                r[ir++] = i;
            }
            return r;
        }

        /// <summary>
        /// Find out reverse matrix using Gauss-Jordan elimination
        /// </summary>
        /// <param name="reverseMatrix">reverse matrix to self</param>
        /// <returns>0 if success</returns>
        public int Reverse(out Matrix reverseMatrix)
        {
            if (M != N)
            {
                reverseMatrix = null;
                return -1;
            }
            ForwardElimination(this, out Matrix p, out Matrix lre, out Matrix ue, out int[] pivots);
            if (pivots.Length !=  M)
            {
                reverseMatrix = null;
                return -1;
            }
            LowerBackwardElimination(lre, p, out var c);
            UpperBackwardElimination(ue, c, out reverseMatrix);
            return 0;
        }

        /// <summary>
        /// Produce Echelon Form 
        /// </summary>
        /// <param name="p">row-permutation matrix</param>
        /// <param name="re">reverse elimination matrix</param>
        /// <param name="e">reduced row echelon form</param>
        public void EchelonForm(out Matrix p, out Matrix re, out Matrix e) => ForwardElimination(this, out p, out re, out e, out int[] _);

        /// <summary>
        /// Produce Uni-Echelon Form (Uni-Triangular)  
        /// </summary>
        /// <param name="p">row-permutation matrix</param>
        /// <param name="re">reverse elimination matrix</param>
        /// <param name="d">diagonal matrix</param>
        /// <param name="e">uni-reduced row echelon form</param>
        public void UniEchelonForm(out Matrix p, out Matrix re, out Matrix d, out Matrix e)
        {
            ForwardElimination(this, out p, out re, out var ue, out int[] pivots);
            DiagonalElimination(ue, pivots, out d, out e);
        }

        /// <summary>
        /// Produce Reduced Row Echelon Form (rref) R
        /// </summary>
        /// <param name="p">row-permutation matrix</param>
        /// <param name="re">reverse elimination matrix</param>
        /// <param name="rref">reduced row echelon form</param>
        public void RowReducedEchelonForm(out Matrix p, out Matrix re, out Matrix rref)
        {
            ForwardElimination(this, out p, out Matrix lre, out Matrix ue, out int[] pivots);
            DiagonalElimination(ue, pivots, out Matrix d, out Matrix uue);
            UpperBackwardElimination(uue, pivots, out Matrix ure, out rref);
            re = lre * d * ure;
        }

        /// <summary>
        /// Forward Elimination to find L and P matrices from PA = LU equation
        /// </summary>
        /// <param name="a">the matrix</param>
        /// <param name="p">row-permutation matrix</param>
        /// <param name="lre">lower reverse elimination matrix</param>
        /// <param name="ue">upper echelon matrix</param>
        /// <param name="pivotColumns">indexes of pivot columns</param>
        private static void ForwardElimination(Matrix a, out Matrix p, out Matrix lre, out Matrix ue, out int[] pivotColumns)
        {
            lre = new Matrix(a.M, true);
            p = new Matrix(a.M, true);
            ue = (Matrix)a.Clone();
            pivotColumns = new int[a.M];
            int i = 0, i0 = 0;
            for (; i < a.M; i++, i0++)
            {
                for (; i0 < a.N && Math.Abs(ue[i, i0]) < Epsilon; i0++)
                {
                    int iReverse = i;
                    for (int j = i + 1; j < a.M; j++)
                    {
                        if (Math.Abs(ue[j, i0]) > Epsilon)
                        {
                            iReverse = j;
                            break;
                        }
                    }
                    if (iReverse == i) continue;
                    lre.ExchangeRows(iReverse, i, i);
                    p.ExchangeRows(iReverse, i);
                    ue.ExchangeRows(iReverse, i);
                    break;
                }
                if (i0 == a.N) break;
                pivotColumns[i] = i0;
                for (int j = i + 1; j < a.M; j++)
                {
                    double coeff = ue[j, i0] / ue[i, i0];
                    lre[j, i] = coeff;
                    for (int k = i0; k < a.N; k++)
                    {
                        ue[j, k] -= ue[i, k] * coeff;
                    }
                }
            }
            Array.Resize(ref pivotColumns, i);
        }


        /// <summary>
        /// Ue = D*Uue
        /// </summary>
        /// <param name="ue">upper echelon matrix</param>
        /// <param name="pivotColumns">indexes of pivot columns</param>
        /// <param name="d">diagonal matrix</param>
        /// <param name="uue">uni-upper echelon matrix</param>
        /// <returns></returns>
        private static void DiagonalElimination(Matrix ue, int[] pivotColumns, out Matrix d, out Matrix uue)
        {
            d = new Matrix(ue.M, true);
            uue = (Matrix)ue.Clone();
            for (int i = 0; i < pivotColumns.Length; i++)
            {
                double coeff = ue[i, pivotColumns[i]];
                d[i, i] = coeff;
                uue[i, pivotColumns[i]] = 1.0;
                for (int j = pivotColumns[i] + 1; j < uue.N; j++)
                {
                    uue[i, j] /= coeff;
                }
            }
        }

        /// <summary>
        /// Uue = Ure*Urre
        /// </summary>
        /// <param name="uue">uni-upper echelon matrix</param>
        /// <param name="pivotColumns">indexes of pivot columns</param>
        /// <param name="ure">upper reverse elimination matrix</param>
        /// <param name="urre">upper reduced row echelon matrix</param>
        private static void UpperBackwardElimination(Matrix uue, int[] pivotColumns, out Matrix ure, out Matrix urre)
        {
            ure = new Matrix(uue.M, true);
            urre = (Matrix)uue.Clone();
            for (int i = pivotColumns.Length-1; i >= 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    double coeff = urre[j, pivotColumns[i]];
                    if (Math.Abs(coeff) < Epsilon) continue;
                    ure[j, i] = coeff;
                    for (int k = pivotColumns[i]; k < urre.N; k++)
                    {
                        urre[j, k] -= urre[i, k] * coeff;
                    }
                }
            }
        }

        /// <summary>
        /// Transforming augmented matrix [U|B] => [I|c]
        /// </summary>
        /// <param name="u">upper-triangular matrix</param>
        /// <param name="b">right-side matrix</param>
        /// <param name="c">right-side matrix after transformation</param>
        private static void UpperBackwardElimination(Matrix u, Matrix b, out Matrix c)
        {
            c = (Matrix)b.Clone();
            for (int i = c.M - 1; i >= 0; i--)
            {
                for (int j = 0; j < c.N; j++)
                {
                    c[i, j] /= u[i, i];
                }
                for (int j = 0; j < i; j++)
                {
                    double coeff = u[j, i];
                    for (int k = 0; k < c.N; k++)
                    {
                        c[j, k] -= coeff * c[i, k];
                    }
                }
            }
        }

        /// <summary>
        /// Transforming augmented matrix [L|B] => [I|c]
        /// </summary>
        /// <param name="l">lower-triangular matrix)</param>
        /// <param name="b">right-side matrix</param>
        /// <param name="c">right-side matrix after transformation</param>
        private static void LowerBackwardElimination(Matrix l, Matrix b, out Matrix c)
        {
            c = (Matrix)b.Clone();
            for (int i = 0; i < c.M; i++)
            {
                for (int j = 0; j < c.N; j++)
                {
                    c[i, j] /= l[i, i];
                }
                for (int j = i + 1; j < c.M; j++)
                {
                    double coeff = l[j, i];
                    for (int k = 0; k < c.N; k++)
                    {
                        c[j, k] -= coeff * c[i, k];
                    }
                }
            }
        }

        /// <summary>
        /// Exchange i, j rows of this.matrix until j-th column
        /// </summary>
        /// <param name="i">first row</param>
        /// <param name="j">second row</param>
        /// <param name="until">last column</param>
        public void ExchangeRows(int i, int j, int until = Int32.MaxValue)
        {
            until = Math.Min(N, until);
            for (int k = 0; k < until; k++)
            {
                double tmp = _data[i, k];
                _data[i, k] = _data[j, k];
                _data[j, k] = tmp;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    sb.Append($"{_data[i, j]:0.000}\t");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public int CompareTo(Matrix other)
        {
            if (M != other.M || N != other.N)
            {
                return -1;
            }

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (Math.Abs(_data[i, j] - other[i, j]) > 0.0000000001)
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }

        public object Clone()
        {
            return new Matrix((double[,])_data.Clone());
        }
    }
}