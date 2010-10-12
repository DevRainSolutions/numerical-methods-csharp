#region Using directives

using System;

using NumericalMethods.Statistics;
using NumericalMethods.Curves;

#endregion

namespace NumericalMethods.Statistics
{
    /// Laplace distribution.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class LaplaceDistribution : ProbabilityDensityFunction
    {
	    /// Average of the distribution.
        private double _mu;
	    /// Scale of the distribution.
        private double _beta;

        /// Constructor method.
        /// @param center double
        /// @param scale double
        /// @exception ArgumentOutOfRangeException
        ///							when the scale parameter is non-positive
        public LaplaceDistribution(double center, double scale)
        {
            if (scale <= 0)
                throw new ArgumentOutOfRangeException("Scale parameter must be positive");
            _mu = center;
            _beta = scale;
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h DhbScientificCurves.Histogram
        public LaplaceDistribution(Histogram h)
                        : this( h.Average, Math.Sqrt( 0.5 * h.Variance))
        {
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return _mu; }

            set
            {
                throw new InvalidOperationException(
                                        "Can not set average on Laplace distribution");
            }
        }

        /// @param center double
        /// @param scale double
        public void DefineParameters(double center, double scale)
        {
            _mu = center;
            _beta = scale;
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from -infinity to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            return x > _mu
                    ? 1 - Math.Exp(-(x - _mu) / _beta) / 2
                    : Math.Exp(-(x - _mu) / _beta) / 2;
        }

        /// @return double kurtosis of the distribution.
        public override double Kurtosis
        {
            get { return 3; }
        }

        /// @return string name of the distribution.
        public override string Name
        {
            get { return null; }
        }

        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public override double[] Parameters
        {
            get { return new double[] { _mu, _beta }; }

            set { DefineParameters(value[0], value[1]); }
        }

        /// This method assumes that the range of the argument has been checked.
        /// @return double the value for which the distribution function
        ///													is equal to x.
        /// @param x double value of the distribution function.
        protected override double PrivateInverseDistributionValue(double x)
        {
            return x < 0.5
                    ? _mu + _beta * Math.Log(2 * x)
                    : _mu - _beta * Math.Log(2 - 2 * x);
        }

        /// @return double a random number distributed according to the receiver.
        public override double Random()
        {
            double r = -_beta * Math.Log(this.Generator.NextDouble());
            return this.Generator.NextDouble() > 0.5 ? _mu + r : _mu - r;
        }

        /// @return double skewness of the distribution.
        public override double Skewness
        {
            get { return 0; }
        }

        /// @return double standard deviation of the distribution
        public override double StandardDeviation
        {
            get { return _beta / Math.Sqrt(2); }
        }

        /// @return string
        public override string ToString()
        {
            return string.Format(
                "Laplace distribution ({0:####0.00000},{1:####0.00000})", _mu, _beta);
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            return Math.Exp(-Math.Abs(x - _mu) / _beta) / (2 * _beta);
        }

        /// Evaluate the distribution and the gradient of the distribution with respect
        /// to the parameters.
        /// @return double[]	0: distribution's value, 1,2,...,n distribution's gradient
        /// @param x double
        public override double[] ValueAndGradient(double x)
        {
            double[] answer = new double[3];
            answer[0] = this.Value(x);
            double y = x - _mu;
            if (y >= 0)
            {
                answer[1] = answer[0] / _beta;
                answer[2] = (y / _beta - 1) * answer[0] / _beta;
            }
            else
            {
                answer[1] = -answer[0] / _beta;
                answer[2] = -(y / _beta + 1) * answer[0] / _beta;
            }
            return answer;
        }
    }
}
