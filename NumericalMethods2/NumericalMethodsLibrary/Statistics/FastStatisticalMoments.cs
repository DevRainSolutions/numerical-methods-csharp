#region Using directives
using NumericalMethods.Statistics;

#endregion

namespace NumericalMethods.Statistics
{
    ///
    /// Fast StatisticalMonents (at the cost of accuracy)
    /// 
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class FastStatisticalMoments : StatisticalMoments
    {
        /// Default constructor method.
        public FastStatisticalMoments() : base()
        {
        }

        /// Constructor method.
        /// @param n int
        public FastStatisticalMoments(int n) : base(n)
        {
        }

        /// Accumulate a random variable.
        /// @param x value of the random variable.
        public override void Accumulate(double x)
        {
            double value = 1.0;
            for (int n = 0; n < _moments.Length; n++)
            {
                _moments[n] += value;
                value *= x;
            }
        }

        /// @return double average.
        public override double Average
        {
            get { return _moments[1] / _moments[0]; }
        }

        /// The kurtosis measures the sharpness of the distribution near the maximum.
        /// Note: The kurtosis of the Normal distribution is 0 by definition.
        /// @return double kurtosis.
        public override double Kurtosis
        {
            get
            {
                if (_moments[0] < 4)
                    return double.NaN;
                double x1 = this.Average;
                double x2 = _moments[2] / _moments[0];
                double x3 = _moments[3] / _moments[0];
                double x4 = _moments[4] / _moments[0];
                double xSquared = x1 * x1;
                double m4 = x4 - (4 * x1 * x3) + 3 * xSquared
                                                            * (2 * x2 - xSquared);
                double kFact = _moments[0] * (_moments[0] + 1)
                                        / ((_moments[0] - 1) * (_moments[0] - 2)
                                                            * (_moments[0] - 3));
                double kConst = 3 * (_moments[0] - 1) * (_moments[0] - 1)
                                        / ((_moments[0] - 2) * (_moments[0] - 3));
                x4 = this.Variance;
                x4 *= x4;
                return kFact * (m4 * _moments[0] / x4) - kConst;
            }
        }

        /// @return double skewness.
        public override double Skewness
        {
            get
            {
                if (_moments[0] < 3)
                    return double.NaN;
                double x1 = this.Average;
                double x2 = _moments[2] / _moments[0];
                double x3 = _moments[3] / _moments[0];
                double m3 = x3 + x1 * (2 * x1 * x1 - 3 * x2);
                x1 = this.StandardDeviation;
                x2 = x1 * x1;
                x2 *= x1;
                return m3 * _moments[0] * _moments[0] / (x2 * (_moments[0] - 1)
                                                            * (_moments[0] - 2));
            }
        }

        /// Unnormalized central moment of 2nd order
        /// (needed to compute the t-test).
        /// @return double
        public override double UnnormalizedVariance
        {
            get
            {
                double average = this.Average;
                return _moments[2] - average * average * _moments[0];
            }
        }

        /// Note: the variance includes the Bessel correction factor.
        /// @return double variance.
        public override double Variance
        {
            get
            {
                if (_moments[0] < 2)
                    return double.NaN;
                double average = this.Average;
                return (_moments[0] / (_moments[0] - 1))
                                * (_moments[2] / _moments[0] - average * average);
            }
        }
    }
}
