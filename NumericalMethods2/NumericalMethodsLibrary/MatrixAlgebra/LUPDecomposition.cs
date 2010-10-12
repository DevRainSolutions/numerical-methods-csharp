#region Using directives

using System;
using System.Text;

#endregion

namespace NumericalMethods.MatrixAlgebra
{
    /// Lower Upper Permutation (LUP) decomposition
    /// 
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class LUPDecomposition
    {
        /// Rows of the system
        private double[,] _rows;
        /// Permutation
        private int[] _permutation = null;
        /// Permutation's parity
        private int _parity = 1;

        /// Constructor method
        /// @param components double[,]
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension 
        ///									the supplied matrix is not square
        public LUPDecomposition(double[,] components)
        {
            if (components.GetLength(0) != components.GetLength(1))
                throw new DhbIllegalDimension(
                    string.Format("Illegal system: a {0} by {1} matrix is not a square matrix",
                            components.GetLength(0), components.GetLength(1) ) );
            _rows = components;
            Initialize();
        }

        /// Constructor method.
        /// @param m DhbMatrixAlgebra.Matrix
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension 
        ///									the supplied matrix is not square
        public LUPDecomposition(Matrix m)
        {
            if (!m.IsSquare)
                throw new DhbIllegalDimension("Supplied matrix is not a square matrix");
            Initialize(m.Components);
        }

        /// Constructor method.
        /// @param m DhbMatrixAlgebra.DhbSymmetricMatrix
        public LUPDecomposition(SymmetricMatrix m)
        {
            Initialize(m.Components);
        }

        /// @return double[]
        /// @param xTilde double[]
        private double[] BackwardSubstitution(double[] xTilde)
        {
            int n = _rows.GetLength(0);
            double[] answer = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                answer[i] = xTilde[i];
                for (int j = i + 1; j < n; j++)
                    answer[i] -= _rows[i, j] * answer[j];
                answer[i] /= _rows[i, i];
            }
            return answer;
        }

        private void Decompose()
        {
            int n = _rows.GetLength(0);
            _permutation = new int[n];
            for (int i = 0; i < n; i++)
                _permutation[i] = i;
            _parity = 1;
            try
            {
                for (int i = 0; i < n; i++)
                {
                    SwapRows(i, LargestPivot(i));
                    Pivot(i);
                }
            }
            catch (ArithmeticException) { _parity = 0; } ;
        }

        /// @return boolean	true if decomposition was done already
        private bool Decomposed()
        {
            if (_parity == 1 && _permutation == null)
                Decompose();
            return _parity != 0;
        }

        /// @return double[]
        /// @param c double[]
        public double Determinant()
        {
            if (!Decomposed())
                return double.NaN;
            double determinant = _parity;
            for (int i = 0; i < _rows.GetLength(0); i++)
                determinant *= _rows[i, i];
            return determinant;
        }

        /// @return double[]
        /// @param c double[]
        private double[] ForwardSubstitution(double[] c)
        {
            int n = _rows.GetLength(0);
            double[] answer = new double[n];
            for (int i = 0; i < n; i++)
            {
                answer[i] = c[_permutation[i]];
                for (int j = 0; j <= i - 1; j++)
                    answer[i] -= _rows[i, j] * answer[j];
            }
            return answer;
        }

        private void Initialize()
        {
            _permutation = null;
            _parity = 1;
        }

        /// @param components double[,]  components obtained from constructor methods.
        private void Initialize(double[,] components)
        {
            _rows = (double[,])components.Clone();

            // The previous line and the following code are not exact equivalents
            // The loop still works with a non-square components, as long as rows <= columns
            // Is this OK or components should be square
//            int n = components.GetLength(0);
//            _rows = new double[n, n];
//            for (int i = 0; i < n; i++)
//            {
//                for (int j = 0; j < n; j++)
//                    _rows[i, j] = components[i, j];
//            }

            Initialize();
        }

        /// @return double[]
        /// @param c double[]
        public double[,] InverseMatrixComponents()
        {
            if (!Decomposed())
                return null;
            int n = _rows.GetLength(0);
            double[,] inverseRows = new double[n, n];
            double[] column = new double[n];
            for (int i = 0; i < n; i++)
            {
                Array.Clear(column, 0, n);
                column[i] = 1;
                column = Solve(column);
                for (int j = 0; j < n; j++)
                    inverseRows[i, j] = column[j];
            }
            return inverseRows;
        }

        /// @return int
        /// @param k int
        private int LargestPivot(int k)
        {
            double maximum = Math.Abs(_rows[k, k]);
            double abs;
            int index = k;
            for (int i = k + 1; i < _rows.GetLength(0); i++)
            {
                abs = Math.Abs(_rows[i, k]);
                if (abs > maximum)
                {
                    maximum = abs;
                    index = i;
                }
            }
            return index;
        }

        /// @param k int
        private void Pivot(int k)
        {
            double inversePivot = 1 / _rows[k, k];
            int k1 = k + 1;
            int n = _rows.GetLength(0);
            for (int i = k1; i < n; i++)
            {
                _rows[i, k] *= inversePivot;
                for (int j = k1; j < n; j++)
                    _rows[i, j] -= _rows[i, k] * _rows[k, j];
            }
        }

        /// @return double[]
        /// @param c double[]
        public double[] Solve(double[] c)
        {
            return Decomposed()
                            ? BackwardSubstitution(ForwardSubstitution(c))
                            : null;
        }

        /// @return double[]
        /// @param c double[]
        public DhbVector Solve(DhbVector c)
        {
            double[] components = Solve(c.Components);
            return components == null ? null : new DhbVector(components);
        }

        /// @param i int
        /// @param k int
        private void SwapRows(int i, int k)
        {
            if (i != k)
            {
                for (int j = 0; j < _rows.GetLength(1); j++)
                {
                    double temp = _rows[i, j];
                    _rows[i, j] = _rows[k, j];
                    _rows[k, j] = temp;
                }
                int nTemp = _permutation[i];
                _permutation[i] = _permutation[k];
                _permutation[k] = nTemp;
                _parity = -_parity;
            }
        }

        /// Make sure the supplied matrix components are those of
        /// a symmetric matrix
        /// @param components double
        public static void SymmetrizeComponents(double[,] components)
        {
            for (int i = 0; i < components.GetLength(0); i++)
            {
                for (int j = i + 1; j < components.GetLength(1); j++)
                {
                    components[i,j] += components[j,i];
                    components[i,j] *= 0.5;
                    components[j,i] = components[i,j];
                }
            }
        }

        /// Returns a string that represents the value of this object.
        /// @return a string representation of the receiver
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            char[] separator = { '[', ' ' };
            int n = _rows.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                separator[0] = '{';
                for (int j = 0; j < n; j++)
                {
                    sb.Append(separator);
                    sb.Append(_rows[i, j]);
                    if (j == 0) separator[0] = ' ';
                }
                sb.Append('}');
                sb.Append('\n');
            }
            if (_permutation != null)
            {
                sb.Append(_parity == 1 ? '+' : '-');
                sb.Append("( " + _permutation[0]);
                for (int i = 1; i < n; i++)
                    sb.Append(", " + _permutation[i]);
                sb.Append(')');
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}
