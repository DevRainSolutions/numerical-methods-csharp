#region Using directives

using System;
using NumericalMethods.Statistics;
using NumericalMethods.MatrixAlgebra;

#endregion

namespace NumericalMethods.Approximation
{
    /// Polynomial least square fit
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class PolynomialLeastSquareFit
    {
        double[,] _systemMatrix;
        double[] _systemConstants;

        /// Constructor method.
        public PolynomialLeastSquareFit(int n)
        {
            int n1 = n + 1;
            _systemMatrix = new double[n1, n1];
            _systemConstants = new double[n1];
            Reset();
        }

        /// @param x double
        /// @param m StatisticalMoments
        public void AccumulateAverage(double x, StatisticalMoments m)
        {
            AccumulatePoint(x, m.Average, m.ErrorOnAverage);
        }

        /// @param x double
        /// @param n int	bin content
        public void AccumulateBin(double x, int n)
        {
            AccumulateWeightedPoint(x, n, 1.0 / Math.Max(1, n));
        }

        /// @param x double
        /// @param y double
        public void AccumulatePoint(double x, double y)
        {
            AccumulateWeightedPoint(x, y, 1);
        }

        /// @param x double
        /// @param y double
        /// @param error double	standard deviation on y
        public void AccumulatePoint(double x, double y, double error)
        {
            AccumulateWeightedPoint(x, y, 1.0 / (error * error));
        }

        /// @param x double
        /// @param y double
        /// @param w double	weight of point
        public void AccumulateWeightedPoint(double x, double y, double w)
        {
            double xp1 = w;
            double xp2;
            for (int i = 0; i < _systemConstants.Length; i++)
            {
                _systemConstants[i] += xp1 * y;
                xp2 = xp1;
                for (int j = 0; j <= i; j++)
                {
                    _systemMatrix[i, j] += xp2;
                    xp2 *= x;
                }
                xp1 *= x;
            }
        }

        /// @return DhbEstimation.EstimatedPolynomial
        public EstimatedPolynomial Evaluate()
        {
            for (int i = 0; i < _systemConstants.Length; i++)
            {
                for (int j = i + 1; j < _systemConstants.Length; j++)
                    _systemMatrix[i, j] = _systemMatrix[j, i];
            }
            try
            {
                LUPDecomposition lupSystem = new LUPDecomposition(_systemMatrix);
                double[,] components = lupSystem.InverseMatrixComponents();
                LUPDecomposition.SymmetrizeComponents(components);
                return new EstimatedPolynomial(
                                lupSystem.Solve(_systemConstants),
                                SymmetricMatrix.FromComponents(components) );
            }
            catch (DhbIllegalDimension) { }
            catch (DhbNonSymmetricComponents) { }
            return null;
        }

        public void Reset()
        {
            for (int i = 0; i < _systemConstants.Length; i++)
            {
                _systemConstants[i] = 0;
                for (int j = 0; j < _systemConstants.Length; j++)
                    _systemMatrix[i, j] = 0;
            }
        }
    }
}
