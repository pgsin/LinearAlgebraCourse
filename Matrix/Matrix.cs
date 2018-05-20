using System;
using System.Text;

namespace LinLib
{
    public class Matrix : ICloneable, IComparable<Matrix>
    {
        private readonly double[,] _data;
        public int N => _data.GetUpperBound(0) + 1;
        public int M => _data.GetUpperBound(1) + 1;

        public Matrix(int n, bool diagonal = false)
        {
            _data = new double[n,n];
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
            if (a.M != b.N)
            {
                return null;
            }
            Matrix c = new Matrix(a.N, b.M);
            for (int i = 0; i < c.N; i++)
            {
                for (int j = 0; j < c.M; j++)
                {
                    double s = 0.0;
                    for (int m = 0; m < a.M; m++)
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
            if (a.M != b.M || a.N != b.N)
            {
                return null;
            }
            Matrix c = new Matrix(a.N, b.M);
            for (int i = 0; i < c.N; i++)
            {
                for (int j = 0; j < c.M; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            return c;
        }

        public void Transpose()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < M; j++)
                {
                    double tmp = _data[i, j];
                    _data[i, j] = _data[j, i];
                    _data[j, i] = tmp;
                }
            }
        }

        public void Transpose(out Matrix m)
        {
            m = new Matrix(N, M);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    m[i, j] = _data[j, i];
                }
            }
        }

        /// <summary>
        /// PA = LU factorization
        /// </summary>
        /// <param name="l">low-triangular matrix</param>
        /// <param name="p">permutation matrix</param>
        /// <param name="u">upper-triangular matrix</param>
        /// <returns></returns>
        public int PALU_factorization(out Matrix l, out Matrix p, out Matrix u)
        {
            return L_GaussEliminationForward(this, out l, out p, out u);
        }

        /// <summary>
        /// EPA = U factorization
        /// </summary>
        /// <param name="e">elimination matrix</param>
        /// <param name="p">permutation matrix</param>
        /// <param name="u">upper-triangular matrix</param>
        /// <returns></returns>
        public int EPAU_factorization(out Matrix e, out Matrix p, out Matrix u)
        {
            return E_GaussEliminationForward(this, out e, out p, out u);
        }

        /// <summary>
        /// Find out reverse matrix using Gauss-Jordan elimination
        /// </summary>
        /// <param name="reverseMatrix">reverse matrix to self</param>
        /// <returns>0 if success</returns>
        public int Reverse(out Matrix reverseMatrix)
        {
            if (N != M)
            {
                reverseMatrix = null;
                return -1;
            }
            int stdout = E_GaussEliminationForward(this, out var e, out var p, out var u);
            if (stdout != 0)
            {
                reverseMatrix = null;
                return stdout;
            }
            GaussEliminationBackward(u, e * p, out reverseMatrix);
            return 0;
        }

        /// <summary>
        /// Solve set of linear equations Ax=b
        /// </summary>
        /// <param name="b">right side matrix</param>
        /// <param name="x">matrix of variables</param>
        /// <returns>0 if success</returns>
        public int GaussElimination(Matrix b, out Matrix x)
        {
            if (N != b.N)
            {
                x = null;
                return -1;
            }
            int stdout = E_GaussEliminationForward(this, out var e, out var p, out var u);
            if (stdout != 0)
            {
                x = null;
                return stdout;
            }
            GaussEliminationBackward(u, e*p*b, out x);
            return 0;
        }

        /// <summary>
        /// Forward Gaussian Elimination to find E and P matrices from EPA = U equation
        /// </summary>
        /// <param name="a">coefficient matrix</param>
        /// <param name="e">eliminitaion matrix</param>
        /// <param name="p">permutation matrix</param>
        /// <param name="u">upper-triangular matrix</param>
        /// <returns>0 if success</returns>
        private static int E_GaussEliminationForward(Matrix a, out Matrix e, out Matrix p, out Matrix u)
        {
            e = new Matrix(a.N, true);
            p = new Matrix(a.N, true);
            u = (Matrix)a.Clone();
            //TODO
            return 0;
        }

        /// <summary>
        /// Forward Gaussian Elimination to find L and P matrices from PA = LU equation
        /// </summary>
        /// <param name="a">coefficient matrix (n*m)</param>
        /// <param name="l">low-triangular matrix (n*n)</param>
        /// <param name="p">permutation matrix (n*n)</param>
        /// <param name="u">upper-triangular matrix (n*m)</param>
        /// <returns>0 if success</returns>
        private static int L_GaussEliminationForward(Matrix a, out Matrix l, out Matrix p, out Matrix u)
        {
            l = new Matrix(a.N, true);
            p = new Matrix(a.N, true);
            u = (Matrix)a.Clone();
            //TODO
            return 0;
        }

        /// <summary>
        /// Transforming augmented matrix [U|B] => [I|mB]
        /// </summary>
        /// <param name="u">upper-triangular matrix (n*m)</param>
        /// <param name="b">right-side matrix</param>
        /// <param name="mb">right-side matrix after transformation</param>
        private void GaussEliminationBackward(Matrix u, Matrix b, out Matrix mb)
        {
            mb = (Matrix)b.Clone();
            //TODO
        }

        /// <summary>
        /// Exchange i, j rows of this.matrix until j-th column
        /// </summary>
        /// <param name="i">first row</param>
        /// <param name="j">second row</param>
        /// <param name="until">last column</param>
        public void ExchangeRows(int i, int j, int until = Int32.MaxValue)
        {
            until = Math.Min(M, until);
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
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    sb.Append($"{_data[i, j]:0.000}\t");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public int CompareTo(Matrix other)
        {
            if (N != other.N || M != other.M)
            {
                return -1;
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
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
