#region Using directives

using System;

using NumericalMethods.Statistics;
using NumericalMethods.Curves;

#endregion

namespace NumericalMethods.Statistics
{
    /// Cauchy distribution
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class CauchyDistribution : ProbabilityDensityFunction
    {
	    /// Center of the distribution.
        private double _mu;
        /// Scale of the distribution.
        private double _beta;

        /// Create an instance centered at 0 with width 1.
        public CauchyDistribution() : this( 0, 1)
        {
        }

        /// @param middle double middle point of the distribution.
        /// @param width double width of the distribution.
        public CauchyDistribution(double middle, double width)
        {
            _mu = middle;
            _beta = width;
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h Histogram
        public CauchyDistribution(Histogram h)
            : this( h.Average,
		                4 * h.Variance /Math.Sqrt(Math.PI *( h.Minimum
			  		        * h.Minimum +h.Maximum * h.Maximum ) ) )
        {
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return _mu; }

            set
            {
                throw new InvalidOperationException(
                                        "Can not set average on Cauchy distribution"); 
            }
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from -infinity to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            return Math.Atan((x - _mu) / _beta) / Math.PI + 0.5;
        }

        /// @return string	 name of the distribution
        public override string Name
        {
            get { return "Cauchy distribution"; }
        }

        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public override double[] Parameters
        {
            get { return new double[] { _mu, _beta }; }

            set
            {
                this.Mu = value[0];
                this.Beta = value[1];
            }
        }

        /// This method assumes that the range of the argument has been checked.
        /// @return double the value for which the distribution function
        ///													is equal to x.
        /// @param x double value of the distribution function.
        protected override double PrivateInverseDistributionValue(double x)
        {
            return Math.Tan((x - 0.5) * Math.PI) * _beta + _mu;
        }

        /// @param center double
        public double Beta
        {
            set { _beta = value; }
        }

        /// @param center double
        public double Mu
        {
            set { _mu = value; }
        }

        /// @return NaN since the standard deviation of the distribution is
        ///														not defined.
        public override double StandardDeviation
        {
            get { return double.NaN; }
        }

        /// @return string
        public override string ToString()
        {
            return string.Format(
                "Cauchy distribution ({0:####0.00000},{1:####0.00000})", _mu, _beta );
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            double dev = x - _mu;
            return _beta / (Math.PI * (_beta * _beta + dev * dev));
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
            double r = 1 / (y * y + _beta * _beta);
            answer[1] = 2 * answer[0] * y * r;
            answer[2] = answer[0] * (1 / _beta - 2 * _beta * r);
            return answer;
        }
    }
}
