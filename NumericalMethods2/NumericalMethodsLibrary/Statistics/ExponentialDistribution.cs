#region Using directives

using System;

using NumericalMethods.Curves;
using NumericalMethods.Statistics;

#endregion

namespace NumericalMethods.Statistics
{
    /// Exponential distribution.
    /// 
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class ExponentialDistribution : ProbabilityDensityFunction
    {
	    /// Exponential term.
        private double _beta;

        /// General constructor method.
        /// @param exponential fall-off
        /// @exception ArgumentOutOfRangeException
        ///									if the fall-off is non-positive.
        public ExponentialDistribution(double fallOff)
        {
            if (fallOff <= 0)
                throw new ArgumentOutOfRangeException(
                                    "Exponential fall-off must be positive");
            _beta = fallOff;
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h DhbScientificCurves.Histogram
        /// @exception ArgumentOutOfRangeException
        ///							when no suitable parameter can be found.
        public ExponentialDistribution(Histogram h)
        {
            if (h.Minimum < 0)
                throw new ArgumentOutOfRangeException(
                       "Exponential distribution is only defined for non-negative values");
            double average = h.Average;
            if (average < 0)
                throw new ArgumentOutOfRangeException(
                        "Exponential distribution is only defined for positive scale");
            this.Scale = average;
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return _beta; }
            set
            {
                throw new InvalidOperationException(
                                        "Can not set average on Exponential distribution");
            }
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from 0 to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            return 1 - Math.Exp(-x / _beta);
        }

        /// @return double kurtosis of the distribution.
        public override double Kurtosis
        {
            get { return 6; }
        }

        /// @return string	 name of the distribution
        public override string Name
        {
            get { return "Exponential distribution"; }
        }

        /// @return double[] an array containing the parameters of
        ///													the distribution.
        public override double[] Parameters
        {
            get { return new double[] { _beta }; }

            set { this.Scale = value[0]; }
        }

        /// This method assumes that the range of the argument has been checked.
        /// @return double the value for which the distribution function
        ///													is equal to x.
        /// @param x double value of the distribution function.
        protected override double PrivateInverseDistributionValue(double x)
        {
            return -Math.Log(1 - x) * _beta;
        }

        /// @return double a random number distributed according to the receiver.
        public override double Random()
        {
            return -_beta * Math.Log(this.Generator.NextDouble());
        }

        /// @param falloff double
        public double Scale
        {
            set { _beta = value; }
        }

        /// @return double skewness of the distribution.
        public override double Skewness
        {
            get { return 2; }
        }

        /// @return double standard deviation of the distribution
        public override double StandardDeviation
        {
            get { return _beta; }
        }

        /// @return java.lang.String
        public override string ToString()
        {
            return string.Format("Exponential distribution ({0:###0.00000})", _beta);
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            return Math.Exp(-x / _beta) / _beta;
        }

        /// Evaluate the distribution and the gradient of the distribution with respect
        /// to the parameters.
        /// @return double[]	0: distribution's value, 1,2,...,n distribution's gradient
        /// @param x double
        public override double[] ValueAndGradient(double x)
        {
            double[] answer = new double[2];
            answer[0] = this.Value(x);
            answer[1] = (x / _beta - 1) * answer[0] / _beta;
            return answer;
        }
    }
}
