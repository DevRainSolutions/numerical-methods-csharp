#region Using directives

using System;
using NumericalMethods.DhbFunctionEvaluation;
using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Statistics
{
    /// Normal distribution, a.k.a. Gaussian distribution.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class NormalDistribution : ProbabilityDensityFunction
    {
        /// Average of the distribution.
        private double _mu;

        /// Standard deviation of the distribution.
        private double _sigma;

        /// Constant needed to compute the norm.
        private static double _baseNorm = Math.Sqrt(2 * Math.PI);

        /// Series to compute the error function.
        private static PolynomialFunction _errorFunctionSeries;

        static NormalDistribution()
        {
            double[] coeffs = { 0.31938153, -0.356563782, 1.781477937,
										-1.821255978, 1.330274429};
            _errorFunctionSeries = new PolynomialFunction(coeffs);
        }

        /// Constant needed to compute the argument to the error function series.
        private const double _errorFunctionConstant = 0.2316419;

        /// Defines a normalized Normal distribution with average 0
        ///										and standard deviation 1.
        public NormalDistribution() : this(0, 1)
        {
        }

        /// Defines a Normal distribution with known average
        ///											and standard deviation.
        /// @param average of the distribution
        /// @param standard deviation of the distribution
        /// @exception ArgumentOutOfRangeException
        ///						if the standard deviation is non-positive
        public NormalDistribution(double average, double standardDeviation)
        {
            if (standardDeviation <= 0)
                throw new ArgumentOutOfRangeException(
                                    "Standard deviation must be positive");
            _mu = average;
            _sigma = standardDeviation;
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h DhbScientificCurves.Histogram
        public NormalDistribution(IHistogram h) : this(h.Average, h.StandardDeviation)
        {
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return _mu; }
            set { _mu = value; }
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from -infinity to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            return ErrorFunction((x - _mu) / _sigma);
        }

        /// @return error function for the argument.
        /// @param x double
        public static double ErrorFunction(double x)
        {
            if (x == 0)
                return 0.5;
            else if (x > 0)
                return 1 - ErrorFunction(-x);
            double t = 1 / (1 - _errorFunctionConstant * x);
            return t * _errorFunctionSeries.Value(t) * Normal(x);
        }

        /// @return double kurtosis of the distribution.
        public override double Kurtosis
        {
            get { return 0; }
        }

        /// @return string	 name of the distribution
        public override string Name
        {
            get { return "Normal distribution"; }
        }

        /// @return the density probability function for a (0,1) normal distribution.
        /// @param x double	value for which the probability is evaluated.
        static public double Normal(double x)
        {
            return Math.Exp(-0.5 * x * x) / _baseNorm;
        }

        /// @return double[]	array containing mu and sigma
        public override double[] Parameters
        {
            get { return new double[] { _mu, _sigma }; }
            set
            {
                this.Average = value[0];
                this.StandardDeviation = value[1];
            }
        }

        /// @return double a random number distributed according to the receiver.
        public override double Random()
        {
            return base.Generator.NextGaussian() * _sigma + _mu;
        }

        /// @return double skewness of the distribution.
        public override double Skewness
        {
            get { return 0; }
        }

        /// @return double standard deviation of the distribution
        public override double StandardDeviation
        {
            get { return _sigma; }
            set { _sigma = value; }
        }

        /// @return string
        public override string ToString()
        {
            return string.Format( 
                        "{0} ({1:0.00000},{2:0.00000})",
                        this.Name, _mu, _sigma );
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            return Normal((x - _mu) / _sigma) / _sigma;
        }

        /// Evaluate the distribution and the gradient of the distribution with respect
        /// to the parameters.
        /// @return double[]	0: distribution's value, 1,2,...,n distribution's gradient
        /// @param x double
        public override double[] ValueAndGradient(double x)
        {
            double[] answer = new double[3];
            double y = (x - _mu) / _sigma;
            answer[0] = Normal(y) / _sigma;
            answer[1] = answer[0] * y / _sigma;
            answer[2] = answer[0] * (y * y - 1) / _sigma;
            return answer;
        }
    }
}
