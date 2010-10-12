#region Using directives

using System;

using NumericalMethods.DhbFunctionEvaluation;
using NumericalMethods.Curves;
using NumericalMethods.Statistics;


#endregion

namespace NumericalMethods.Statistics
{
    /// Weibull distribution.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class WeibullDistribution : ProbabilityDensityFunction
    {
    	 /// Shape parameter of the distribution.
        private double _alpha;
	    /// Scale parameter of the distribution.
        private double _beta;
	    /// Norm of the distribution (cached for efficiency).
        private double _norm;

        /// Create a new instance of the Weibull distribution with given shape and scale.
        /// @param shape double shape parameter of the distribution (alpha).
        /// @param scale double scale parameter of the distribution (beta).
        /// @exception ArgumentOutOfRangeException
        ///							if any of the parameters is non-positive.
        public WeibullDistribution(double shape, double scale)
        {
            if (shape <= 0)
                throw new ArgumentOutOfRangeException(
                                        "Shape parameter must be positive");
            if (scale <= 0)
                throw new ArgumentOutOfRangeException(
                                        "Scale parameter must be positive");
            DefineParameters(shape, scale);
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h DhbScientificCurves.Histogram
        /// @exception ArgumentOutOfRangeException
        ///							when no suitable parameter can be found.
        public WeibullDistribution(Histogram h)
        {
            if (h.Minimum < 0)
                throw new ArgumentOutOfRangeException(
                        "Weibull distribution is only defined for non-negative values");
            double average = h.Average;
            if (average <= 0)
                throw new ArgumentOutOfRangeException(
                        "Weibull distribution must have a non-negative average");
            double xMin = (h.Minimum + average) * 0.5;
            double accMin = Math.Log(-Math.Log(1 - h.CountsUpTo(xMin) / h.TotalCount));
            double xMax = (h.Maximum + average) * 0.5;
            double accMax = Math.Log(-Math.Log(1 - h.CountsUpTo(xMax) / h.TotalCount));
            double delta = accMax - accMin;
            xMin = Math.Log(xMin);
            xMax = Math.Log(xMax);
            DefineParameters(delta / (xMax - xMin),
                                Math.Exp((accMax * xMin - accMin * xMax) / delta));
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return GammaFunction.Gamma(1 / _alpha) * _beta / _alpha; }

            set
            {
                throw new InvalidOperationException(
                                        "Can not set average on Weibull distribution");
            }
        }

        /// Assigns new values to the parameters.
        /// This method assumes that the parameters have been already checked.
        public void DefineParameters(double shape, double scale)
        {
            _alpha = shape;
            _beta = scale;
            _norm = _alpha / Math.Pow(_beta, _alpha);
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from 0 to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            return 1.0 - Math.Exp(-Math.Pow(x / _beta, _alpha));
        }

        /// @return string the name of the distribution.
        public override string Name
        {
            get { return "Weibull distribution"; }
        }

        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public override double[] Parameters
        {
            get { return new double[] { _alpha, _beta }; }
            set { DefineParameters(value[0], value[1]); }
        }

        /// This method assumes that the range of the argument has been checked.
        /// @return double the value for which the distribution function
        ///													is equal to x.
        /// @param x double value of the distribution function.
        protected override double PrivateInverseDistributionValue(double x)
        {
            return Math.Pow(-Math.Log(1 - x), 1.0 / _alpha) * _beta;
        }

        /// This method was created in VisualAge.
        /// @return string
        public override string ToString()
        {
            return string.Format("Weibull distribution ({0:####0.00000},{1:####0.00000})", _alpha, _beta);
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            return _norm * Math.Pow(x, _alpha - 1) * Math.Exp(-Math.Pow(x / _beta, _alpha));
        }

        /// @return double variance of the distribution.
        public override double Variance
        {
            get
            {
                double s = GammaFunction.Gamma(1 / _alpha);
                return _beta * _beta * (2 * GammaFunction.Gamma(2 / _alpha)
                                                        - s * s / _alpha) / _alpha;
            }
        }
    }
}
