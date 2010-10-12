#region Using directives

using System;
using System.Text;

#endregion

namespace NumericalMethods.MatrixAlgebra
{
    /// Class representing a system of linear equations.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class LinearEquations
    {
        /// components is a matrix build from the system's matrix and
        /// the constant vector
        private double[,] _rows;
        /// Array containing the solution vectors.
        private DhbVector[] _solutions;

        /// Construct a system of linear equation Ax = y1, y2,....
        /// @param m double[,]
        /// @param c double[,]
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension
        ///								if the system's matrix is not square
        ///								if constant dimension does not match
        ///											that of the matrix
        public LinearEquations(double[,] m, double[,] c)
        {
            int n = m.GetLength(0);
            if (m.GetLength(1) != n)
                throw new DhbIllegalDimension("Illegal system: a" + n + " by "
                            + m.GetLength(1) + " matrix is not a square matrix");
            if (c.GetLength(1) != n)
                throw new DhbIllegalDimension("Illegal system: a " + n + " by " + n
                                    + " matrix cannot build a system with a "
                                        + c.GetLength(1) + "-dimensional vector");
            _rows = new double[n, n + c.GetLength(0)];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    _rows[i, j] = m[i, j];
                for (int j = 0; j < c.GetLength(0); j++)
                    _rows[i, n + j] = c[j, i];
            }
        }

        /// Construct a system of linear equation Ax = y.
        /// @param m double[,]		components of the system's matrix
        /// @param c double[]	components of the constant vector
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension
        ///								if the system's matrix is not square
        ///								if constant dimension does not match
        ///											that of the matrix
        public LinearEquations(double[,] m, double[] c)
        {
            int n = m.GetLength(0);
            if (m.GetLength(1) != n)
                throw new DhbIllegalDimension("Illegal system: a" + n + " by "
                            + m.GetLength(1) + " matrix is not a square matrix");
            if (c.Length != n)
                throw new DhbIllegalDimension("Illegal system: a " + n + " by " + n
                                    + " matrix cannot build a system with a "
                                            + c.Length + "-dimensional vector");
            _rows = new double[n, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    _rows[i, j] = m[i, j];
                _rows[i, n] = c[i];
            }
        }

        /// Construct a system of linear equation Ax = y.
        /// @param a MatrixAlgebra.Matrix	matrix A
        /// @param y MatrixAlgebra.DhbVector	vector y
        /// @exception MatrixAlgebra.DhbIllegalDimension
        ///								if the system's matrix is not square
        ///								if vector dimension does not match
        ///											that of the matrix
        public LinearEquations(Matrix a, DhbVector y) : this( a.Components, y.Components)
        {
        }

        /// Computes the solution for constant vector p applying
        /// backsubstitution.
        /// @param p int
        /// @exception ArithmeticException if one diagonal element
        ///									of the triangle matrix is zero.
        private void BackSubstitution(int p)
        {
            int n = _rows.GetLength(0);
            double[] answer = new double[n];
            double x;
            for (int i = n - 1; i >= 0; i--)
            {
                x = _rows[i, n + p];
                for (int j = i + 1; j < n; j++)
                    x -= answer[j] * _rows[i, j];
                answer[i] = x / _rows[i, i];
            }
            _solutions[p] = new DhbVector(answer);
        }

        /// Finds the position of the largest pivot at step p.
        /// @return int
        /// @param p int	step of pivoting.
        private int LargestPivot(int p)
        {
            double pivot = Math.Abs(_rows[p, p]);
            int answer = p;
            double x;
            for (int i = p + 1; i < _rows.GetLength(0); i++)
            {
                x = Math.Abs(_rows[i, p]);
                if (x > pivot)
                {
                    answer = i;
                    pivot = x;
                }
            }
            return answer;
        }

        /// Perform pivot operation at location p.
        /// @param p int
        /// @exception ArithmeticException if the pivot element
        ///															is zero.
        private void Pivot(int p)
        {
            double inversePivot = 1 / _rows[p, p];
            double r;
            int n = _rows.GetLength(0);
            int m = _rows.GetLength(1);
            for (int i = p + 1; i < n; i++)
            {
                r = inversePivot * _rows[i, p];
                for (int j = p; j < m; j++)
                    _rows[i, j] -= _rows[p, j] * r;
            }
        }

        /// Perform optimum pivot operation at location p.
        /// @param p int
        private void PivotingStep(int p)
        {
            SwapRows(p, LargestPivot(p));
            Pivot(p);
        }

        /// @return DhbVector		solution for the 1st constant vector
        public DhbVector Solution()
        {
            return Solution(0);
        }

        /// Return the vector solution of constants indexed by p.
        /// @return DHBmatrixAlgebra.DhbVector
        /// @param p int	index of the constant vector fed into the system.
        /// @exception ArithmeticException
        ///									if the system cannot be solved.
        public DhbVector Solution(int p)
        {
            if (_solutions == null)
                Solve();
            if (_solutions[p] == null)
                BackSubstitution(p);
            return _solutions[p];
        }

        /// @exception ArithmeticException
        ///									if the system cannot be solved.
        private void Solve()
        {
            int n = _rows.GetLength(1);
            for (int i = 0; i < n; i++)
                PivotingStep(i);
            _solutions = new DhbVector[_rows.GetLength(1) - n];
        }

        /// Swaps rows p and q.
        /// @param p int
        /// @param q int
        private void SwapRows(int p, int q)
        {
            if (p != q)
            {
                double temp;
                int m = _rows.GetLength(1);
                for (int j = 0; j < m; j++)
                {
                    temp = _rows[p, j];
                    _rows[p, j] = _rows[q, j];
                    _rows[q, j] = temp;
                }
            }
        }

        /// Returns a string representation of the system.
        /// @return string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            char[] separator = { '[', ' ' };
            int n = _rows.GetLength(0);
            int m = _rows.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                separator[0] = '(';
                for (int j = 0; j < n; j++)
                {
                    sb.Append(separator);
                    sb.Append(_rows[i, j]);
                    if (j == 0) separator[0] = ',';
                }
                separator[0] = ':';
                for (int j = n; j < m; j++)
                {
                    sb.Append(separator);
                    sb.Append(_rows[i, j]);
                    if (j == n) separator[0] = ',';
                }
                sb.Append(')');
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}
