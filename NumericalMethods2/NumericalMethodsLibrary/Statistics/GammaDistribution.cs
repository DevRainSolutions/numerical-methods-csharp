
using System;
using NumericalMethods.DhbFunctionEvaluation;
using NumericalMethods.Interfaces;
using NumericalMethods.Iterations;


namespace NumericalMethods.Statistics
{
    /// Gamma distribution.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class GammaDistribution : ProbabilityDensityFunction
    {
	    /// Shape parameter of the distribution.
        protected double _alpha;
	    /// Scale parameter of the distribution.
        private double _beta;
	    /// Norm of the distribution (cached for efficiency).
        private double _norm;
	    /// Constants used in random number generator (cached for efficiency).
        private double _a;
        private double _b;
        private double _q;
        private double _d;
	    /// Incomplete gamma function for the distribution (cached for efficiency).
        private IncompleteGammaFunction _incompleteGammaFunction;

        /// Constructor method (for internal use only).
        protected GammaDistribution()
        {
        }

        /// Create a new instance of the Gamma distribution with given shape and scale.
        /// @param shape double shape parameter of the distribution (alpha).
        /// @param scale double scale parameter of the distribution (beta).
        /// @exception ArgumentOutOfRangeException The exception description.
        public GammaDistribution(double shape, double scale)
        {
            if (shape <= 0)
                throw new ArgumentOutOfRangeException("Shape parameter must be positive");
            if (scale <= 0)
                throw new ArgumentOutOfRangeException("Scale parameter must be positive");
            DefineParameters(shape, scale);
        }

        /// Create an instance of the receiver with parameters estimated from the
        /// given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h DhbScientificCurves.Histogram
        /// @exception ArgumentOutOfRangeException when no suitable parameter can be found.
        public GammaDistribution(IHistogram h)
        {
            if (h.Minimum < 0)
                throw new ArgumentOutOfRangeException("Gamma distribution is only defined for non-negative values");
            double shape = h.Average;
            if (shape <= 0)
                throw new ArgumentOutOfRangeException("Gamma distribution must have a non-negative shape parameter");
            double scale = h.Variance / shape;
            if (scale <= 0)
                throw new ArgumentOutOfRangeException("Gamma distribution must have a non-negative scale parameter");
            DefineParameters(shape / scale, scale);
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return _alpha * _beta; }
            set { throw new InvalidOperationException("Set not defined for GammaDistribution.Average"); }
        }

        /// Assigns new values to the parameters.
        /// This method assumes that the parameters have been already checked.
        public void DefineParameters(double shape, double scale)
        {
            _alpha = shape;
            _beta = scale;
            _norm = Math.Log(_beta) * _alpha + GammaFunction.LogGamma(_alpha);
            if (_alpha < 1)
                _b = (Math.E + _alpha) / Math.E;
            else if (_alpha > 1)
            {
                _a = Math.Sqrt(2 * _alpha - 1);
                _b = _alpha - Math.Log(4.0);
                _q = _alpha + 1 / _a;
                _d = 1 + Math.Log(4.5);
            }
            _incompleteGammaFunction = null;
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from 0 to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            return IncompleteGammaFunction.Value(x / _beta);
        }

        /// @return DhbIterations.IncompleteGammaFunction
        private IncompleteGammaFunction IncompleteGammaFunction
        {
            get
            {
                if (_incompleteGammaFunction == null)
                    _incompleteGammaFunction = new IncompleteGammaFunction(_alpha);
                return _incompleteGammaFunction;
            }
        }

        /// @return double kurtosis of the distribution.
        public override double Kurtosis
        {
            get { return 6 / _alpha; }
        }

        /// @return string name of the distribution.
        public override string Name
        {
            get { return "Gamma distribution"; }
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
            double r = _alpha > 1
                        ? RandomForAlphaGreaterThan1()
                        : (_alpha < 1 ? RandomForAlphaLessThan1() : RandomForAlphaEqual1());
            return r * _beta;
        }

        /// @return double
        private double RandomForAlphaEqual1()
        {
            return -Math.Log(1 - Generator.NextDouble());
        }

        /// @return double
        private double RandomForAlphaGreaterThan1()
        {
            double u1, u2, v, y, z, w;
            while (true)
            {
                u1 = Generator.NextDouble();
                u2 = Generator.NextDouble();
                v = _a * Math.Log(u1 / (1 - u1));
                y = _alpha * Math.Exp(v);
                z = u1 * u1 * u2;
                w = _b + _q * v - y;
                if (w + _d - 4.5 * z >= 0 || w >= Math.Log(z))
                    return y;
            }
        }

        /// @return double
        private double RandomForAlphaLessThan1()
        {
            double p, y;
            while (true)
            {
                p = Generator.NextDouble() * _b;
                if (p > 1)
                {
                    y = -Math.Log((_b - p) / _alpha);
                    if (Generator.NextDouble() <= Math.Pow(y, _alpha - 1))
                        return y;
                }
                y = Math.Pow(p, 1 / _alpha);
                if (Generator.NextDouble() <= Math.Exp(-y))
                    return y;
            }
        }

        /// @return double skewness of the distribution.
        public override double Skewness
        {
            get { return 2 / Math.Sqrt(_alpha); }
        }

        /// @return string
        public override string ToString()
        {
            return string.Format(
                            "{0} ({1:####0.00000},{2:####0.00000}",
                            Name, _alpha, _beta);
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            return x > 0 ? Math.Exp(Math.Log(x) * (_alpha - 1) - x / _beta - _norm) : 0;
        }

        /// @return double variance of the distribution.
        public override double Variance
        {
            get { return _alpha * _beta * _beta; }
        }
    }
}
