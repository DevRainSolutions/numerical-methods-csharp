#region Using directives

using System;

using NumericalMethods.Curves;
using NumericalMethods.Statistics;

#endregion

namespace NumericalMethods.Statistics
{
    /// Uniform distribution over a given interval.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class UniformDistribution : ProbabilityDensityFunction
    {
	    /// Low limit.
        private double _a;
	    /// High limit.
        private double _b;

        /// Constructor method.
        /// @param low double	low limit
        /// @param high double	high limit
        /// @exception ArgumentOutOfRangeException
        ///										if the limits are inverted.
        public UniformDistribution(double low, double high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(
                            "Limits of distribution are equal or reversed");
            _a = low;
            _b = high;
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h DhbScientificCurves.Histogram
        public UniformDistribution(Histogram h)
        {
            _b = h.StandardDeviation * 1.73205080756888; // sqrt(12)/2
            double c = h.Average;
            _a = c - _b;
            _b += c;
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return (_a + _b) * 0.5; }

            set
            {
                throw new InvalidOperationException(
                                        "Can not set average on Uniform distribution");
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
            else if (x < _b)
                return (x - _a) / (_b - _a);
            else
                return 1;
        }

        /// @return double kurtosis of the distribution.
        public override double Kurtosis
        {
            get { return -1.2; }
        }

        /// @return string	 name of the distribution
        public override string Name
        {
            get { return "Uniform distribution"; }
        }

        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public override double[] Parameters
        {
            get { return new double[] { _a, _b }; }

            set
            {
                _a = value[0];
                _b = value[1];
            }
        }

        /// This method assumes that the range of the argument has been checked.
        /// @return double the value for which the distribution function
        ///													is equal to x.
        /// @param x double value of the distribution function.
        protected override double PrivateInverseDistributionValue(double x)
        {
            return (_b - _a) * x + _a;
        }

        /// @return double skewness of the distribution.
        public override double Skewness
        {
            get { return 0; }
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            if (x < _a)
                return 0;
            else if (x < _b)
                return 1 / (_b - _a);
            else
                return 0;
        }

        /// @return double variance of the distribution
        public override double Variance
        {
            get
            {
                double range = _b - _a;
                return range * range / 12;
            }
        }
    }
}
