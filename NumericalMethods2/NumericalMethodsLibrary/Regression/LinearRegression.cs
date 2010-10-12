#region Using directives

using System;
using NumericalMethods.MatrixAlgebra;
using NumericalMethods.DhbFunctionEvaluation;

#endregion

namespace NumericalMethods.Approximation
{
    /// linear regression
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class LinearRegression
    {
        /// Number of accumulated points
        private int _sum1;
        /// Sum of X
        private double _sumX;
        /// Sum of Y
        private double _sumY;
        /// Sum of XX
        private double _sumXX;
        /// Sum of XY
        private double _sumXY;
        /// Sum of YY
        private double _sumYY;
        /// Slope
        private double _slope;
        /// Intercept
        private double _intercept;
        /// Correlation coefficient
        private double _correlationCoefficient;

        /// Cnstructor method.
        public LinearRegression() : base()
        {
            Reset();
        }

        /// @param x double
        /// @param y double
        public void Add(double x, double y)
        {
            Add(x, y, 1);
        }

        /// @param x double
        /// @param y double
        /// @param w double
        public void Add(double x, double y, double w)
        {
            double wx = w * x;
            double wy = w * y;
            _sum1 += (int)w;
            _sumX += wx;
            _sumY += wy;
            _sumXX += wx * x;
            _sumYY += wy * y;
            _sumXY += wx * y;
            ResetResults();
        }

        /// @return DhbFunctionEvaluation.PolynomialFunction
        public EstimatedPolynomial AsEstimatedPolynomial()
        {
            return new EstimatedPolynomial(this.Coefficients, ErrorMatrix());
        }

        /// @return DhbFunctionEvaluation.PolynomialFunction
        public PolynomialFunction AsPolynomial()
        {
            return new PolynomialFunction(this.Coefficients);
        }

        /// @return double[]
        private double[] Coefficients
        {
            get { return new double[] { this.Intercept, this.Slope }; }
        }

        private void ComputeResults()
        {
            double xNorm = _sumXX * _sum1 - _sumX * _sumX;
            double xyNorm = _sumXY * _sum1 - _sumX * _sumY;
            _slope = xyNorm / xNorm;
            _intercept = (_sumXX * _sumY - _sumXY * _sumX) / xNorm;
            _correlationCoefficient =
                    xyNorm / Math.Sqrt(xNorm * (_sumYY * _sum1 - _sumY * _sumY));
        }

        /// @return DhbMatrixAlgebra.SymmetricMatrix
        public SymmetricMatrix ErrorMatrix()
        {
            double[,] rows = new double[2, 2];
            rows[1, 1] = 1.0 / (_sumXX * _sum1 - _sumX * _sumX);
            rows[0, 1] = _sumXX * rows[1, 1];
            rows[1, 0] = rows[0, 1];
            rows[0, 0] = _sumXX * rows[1, 1];
            SymmetricMatrix answer = null;
            try
            {
                try { answer = SymmetricMatrix.FromComponents(rows); }
                catch (DhbIllegalDimension) { }
            }
            catch (DhbNonSymmetricComponents) { }
            return answer;
        }

        /// @return double
        public double CorrelationCoefficient
        {
            get
            {
                if (double.IsNaN(_correlationCoefficient))
                    ComputeResults();
                return _correlationCoefficient;
            }
        }

        /// @return double
        public double Intercept
        {
            get
            {
                if (double.IsNaN(_intercept))
                    ComputeResults();
                return _intercept;
            }
        }

        /// @return double
        public double Slope
        {
            get
            {
                if (double.IsNaN(_slope))
                    ComputeResults();
                return _slope;
            }
        }

        /// @param x double
        /// @param y double
        public void Remove(double x, double y)
        {
            _sum1 -= 1;
            _sumX -= x;
            _sumY -= y;
            _sumXX -= x * x;
            _sumYY -= y * y;
            _sumXY -= x * y;
            ResetResults();
        }

        public void Reset()
        {
            _sum1 = 0;
            _sumX = 0;
            _sumY = 0;
            _sumXX = 0;
            _sumYY = 0;
            _sumXY = 0;
            ResetResults();
        }

        private void ResetResults()
        {
            _slope = double.NaN;
            _intercept = double.NaN;
            _correlationCoefficient = double.NaN;
        }

        /// @return double
        /// @param x double
        public double Value(double x)
        {
            return x * this.Slope + this.Intercept;
        }
    }
}
