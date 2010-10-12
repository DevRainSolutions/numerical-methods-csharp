#region Using directives

using System;

using NumericalMethods.Curves;
using NumericalMethods.Statistics;

#endregion

namespace NumericalMethods.Statistics
{
    /// Triangular distribution.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class TriangularDistribution : ProbabilityDensityFunction
    {
	    /// Low limit.
        private double _a;
	    /// High limit.
        private double _b;
	    /// peak location.
        private double _c;

        /// Constructor method.
        /// @param low double	low limit
        /// @param high double	high limit
        /// @param peak double	peak of the distribution
        /// @exception ArgumentOutOfRangeException
        ///						if the limits are inverted or
        ///						if the peak is outside the limits.
        public TriangularDistribution(double low, double high, double peak)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(
                            "Limits of distribution are equal or reversed");
            if (peak < low || peak > high)
                throw new ArgumentOutOfRangeException(
                            "Peak of distribution lies outside the limits");
            _a = low;
            _b = high;
            _c = peak;
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h DhbScientificCurves.Histogram
        public TriangularDistribution(Histogram h)
        {
            _b = h.StandardDeviation * 1.73205080756888; // sqrt(12)/2
            _c = h.Average;
            _a = _c - _b;
            _b += _c;
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return (_a + _b + _c) / 3; }

            set
            {
                throw new InvalidOperationException(
                                        "Can not set average on Triangular distribution");
            }
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from a to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            if (x < _a)
                return 0;
            else if (x < _c)
                return (x - _a) * (x - _a) / ((_b - _a) * (_c - _a));
            else if (x < _b)
                return 1 - (_b - x) * (_b - x) / ((_b - _a) * (_b - _c));
            else
                return 1;
        }

        /// @return string	 name of the distribution
        public override string Name
        {
            get { return "Triangular distribution"; }
        }

        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public override double[] Parameters
        {
            get { return new double[] { _a, _b, _c }; }

            set
            {
                _a = value[0];
                _b = value[1];
                _c = value[2];
            }
        }

        /// This method assumes that the range of the argument has been checked.
        /// @return double the value for which the distribution function
        ///													is equal to x.
        /// @param x double value of the distribution function.
        protected override double PrivateInverseDistributionValue(double x)
        {
            return (x < (_c - _a) / (_b - _a))
                            ? Math.Sqrt(x * (_b - _a) * (_c - _a)) + _a
                            : _b - Math.Sqrt((1 - x) * (_b - _a) * (_b - _c));
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            if (x < _a)
                return 0;
            else if (x < _c)
                return 2 * (x - _a) / ((_b - _a) * (_c - _a));
            else if (x < _b)
                return 2 * (_b - x) / ((_b - _a) * (_b - _c));
            else
                return 0;
        }

        /// @return double variance of the distribution
        public override double Variance
        {
            get { return (_a * _a + _b * _b + _c * _c - _a * _b - _a * _c - _b * _c) / 18; }
        }
    }
}
