#region Using directives

using System;
using System.Text;
using NumericalMethods.Iterations;

#endregion

namespace NumericalMethods.MatrixAlgebra
{
    /// JacobiTransformation
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class JacobiTransformation : IterativeProcess
    {
        double[,] _rows;
        double[,] _transform;
        int _p, _q;	//Indices of the largest off-diagonal element

        /// Create a new instance for a given symmetric matrix.
        /// @param m DhbMatrixAlgebra.SymmetricMatrix
        public JacobiTransformation(SymmetricMatrix m)
        {
            int n = m.Rows;
            _rows = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    _rows[i, j] = m[i, j];
            }
        }

        /// @return double[]
        public double[] Eigenvalues()
        {
            int n = _rows.GetLength(0);
            double[] eigenvalues = new double[n];
            for (int i = 0; i < n; i++)
                eigenvalues[i] = _rows[i, i];
            return eigenvalues;
        }

        /// @return DhbMatrixAlgebra.SymmetricMatrix
        public DhbVector[] Eigenvectors()
        {
            int n = _rows.GetLength(0);
            DhbVector[] eigenvectors = new DhbVector[n];
            double[] temp = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    temp[j] = _transform[j, i];
                eigenvectors[i] = new DhbVector(temp);
            }
            return eigenvectors;
        }

        public override double EvaluateIteration()
        {
            double offDiagonal = largestOffDiagonal();
            Transform();
            return offDiagonal;
        }

        /// @param m int
        private void Exchange(int m)
        {
            int m1 = m + 1;
            double temp = _rows[m, m];
            _rows[m, m] = _rows[m1, m1];
            _rows[m1, m1] = temp;
            int n = _rows.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                temp = _transform[i, m];
                _transform[i, m] = _transform[i, m1];
                _transform[i, m1] = temp;
            }
        }

        public override void FinalizeIterations()
        {
            int n = _rows.GetLength(0);
            int bound = n - 1;
            int i, m;
            while (bound >= 0)
            {
                m = -1;
                for (i = 0; i < bound; i++)
                {
                    if (Math.Abs(_rows[i, i]) < Math.Abs(_rows[i + 1, i + 1]))
                    {
                        Exchange(i);
                        m = i;
                    }
                }
                bound = m;
            }
        }

        public override void InitializeIterations()
        {
            _transform = SymmetricMatrix.IdentityMatrix(_rows.GetLength(0)).Components;
        }

        /// @return double	absolute value of the largest off diagonal element
        private double largestOffDiagonal()
        {
            double value = 0;
            double r;
            int n = _rows.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    r = Math.Abs(_rows[i, j]);
                    if (r > value)
                    {
                        value = r;
                        _p = i;
                        _q = j;
                    }
                }
            }
            return value;
        }

        /// Returns a string representation of the system.
        /// @return string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            char[] separator = { '[', ' ' };
            int n = _rows.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                separator[0] = '{';
                for (int j = 0; j <= i; j++)
                {
                    sb.Append(separator);
                    sb.Append(_rows[i, j]);
                    if (j == 0) separator[0] = ' ';
                }
                sb.Append('}');
                sb.Append('\n');
            }
            return sb.ToString();
        }

        /// @return DhbMatrixAlgebra.SymmetricMatrix
        private void Transform()
        {
            double apq = _rows[_p, _q];
            if (apq == 0)
                return;
            double app = _rows[_p, _p];
            double aqq = _rows[_q, _q];
            double arp = (aqq - app) * 0.5 / apq;
            double t = arp > 0 ? 1 / (Math.Sqrt(arp * arp + 1) + arp)
                               : 1 / (arp - Math.Sqrt(arp * arp + 1));
            double c = 1 / Math.Sqrt(t * t + 1);
            double s = t * c;
            double tau = s / (1 + c);
            _rows[_p, _p] = app - t * apq;
            _rows[_q, _q] = aqq + t * apq;
            _rows[_p, _q] = 0;
            _rows[_q, _p] = 0;
            int n = _rows.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                if (i != _p && i != _q)
                {
                    _rows[_p, i] = _rows[i, _p] - s * (_rows[i, _q] + tau * _rows[i, _p]);
                    _rows[_q, i] = _rows[i, _q] + s * (_rows[i, _p] - tau * _rows[i, _q]);
                    _rows[i, _p] = _rows[_p, i];
                    _rows[i, _q] = _rows[_q, i];
                }
                arp = _transform[i, _p];
                aqq = _transform[i, _q];
                _transform[i, _p] = arp - s * (aqq + tau * arp);
                _transform[i, _q] = aqq + s * (arp - tau * aqq);
            }
        }
    }
}
