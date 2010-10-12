#region Using directives

using System;

using NumericalMethods.DhbFunctionEvaluation;
using NumericalMethods.Curves;
using NumericalMethods.Statistics;

#endregion

namespace NumericalMethods.Statistics
{
    /// Fisher-Tippett distribution
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class FisherTippettDistribution : ProbabilityDensityFunction
    {
	    /// Center of the distribution.
        private double _alpha;
	    /// Scale parameter of the distribution.
        private double _beta;

        /// Constructor method
        /// @param center double
        /// @param scale double
        /// @exception ArgumentOutOfRangeException if the scale parameter is non-positive.
        public FisherTippettDistribution(double center, double scale)
        {
            if (scale <= 0)
                throw new ArgumentOutOfRangeException("Scale parameter must be positive");
            _alpha = center;
            _beta = scale;
        }

        /// Create an instance of the receiver with parameters estimated from the
        /// given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h DhbScientificCurves.Histogram
        /// @exception ArgumentOutOfRangeException when no suitable parameter can be found.
        public FisherTippettDistribution(Histogram h)
        {
            double beta = h.StandardDeviation;
            if (beta < 0)
                throw new ArgumentOutOfRangeException("Histogram has vanishing standard deviation");
            beta *= Math.Sqrt(6) / Math.PI;
            DefineParameters(h.Average - 0.5772156649 * beta, beta);
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return 0.5772156649 * _beta + _alpha; }

            set
            {
                throw new InvalidOperationException(
                                        "Can not set average on Fisher-Tippett distribution");
            }
        }

        /// @param center double
        /// @param scale double
        public void DefineParameters(double center, double scale)
        {
            _alpha = center;
            _beta = scale;
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from -infinity to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            double y = (x - _alpha) / _beta;
            if (y < -DhbMath.LargestExponentialArgument)
                return 0;
            y = Math.Exp(-y);
            if (y > DhbMath.LargestExponentialArgument)
                return 1;
            return Math.Exp(-y);
        }

        /// @return double kurtosis of the distribution.
        public override double Kurtosis
        {
            get { return 2.4; }
        }

        /// @return string	name of the distribution
        public override string Name
        {
            get { return "Fisher-Tippett distribution"; }
        }

        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public override double[] Parameters
        {
            get { return new double[] { _alpha, _beta }; }

            set { DefineParameters(value[0], value[1]); }
        }

        /// @return double a random number distributed according to the receiver.
        public override double Random()
        {
            double t;
            while ((t = -Math.Log(this.Generator.NextDouble())) == 0) ;
            return _alpha - _beta * Math.Log(t);
        }

        /// @return double skewness of the distribution.
        public override double Skewness
        {
            get { return 1.3; }
        }

        /// @return double standard deviation of the distribution
        public override double StandardDeviation
        {
            get { return Math.PI * _beta / Math.Sqrt(6); }
        }

        /// @return string
        public override string ToString()
        {
            return string.Format(
                "Fisher-Tippett distribution({0:####0.00000},{1:####0.00000})",
                _alpha, _beta );
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            double y = (x - _alpha) / _beta;
            if (y > DhbMath.LargestExponentialArgument)
                return 0;
            y += Math.Exp(-y);
            if (y > DhbMath.LargestExponentialArgument)
                return 0;
            return Math.Exp(-y) / _beta;
        }

        /// Evaluate the distribution and the gradient of the distribution with respect
        /// to the parameters.
        /// @return double[]	0: distribution's value, 1,2,...,n distribution's gradient
        /// @param x double
        public override double[] ValueAndGradient(double x)
        {
            double[] answer = new double[3];
            answer[0] = this.Value(x);
            double y = (x - _alpha) / _beta;
            double dy = Math.Exp(-y) - 1;
            double r = -1 / _beta;
            answer[1] = dy * answer[0] * r;
            answer[2] = answer[0] * (y * dy + 1) * r;
            return answer;
        }
    }
}
