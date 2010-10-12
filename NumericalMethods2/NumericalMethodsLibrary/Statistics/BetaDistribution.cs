namespace NumericalMethods.Statistics
{
    using System;
    using NumericalMethods.DhbFunctionEvaluation;
    using NumericalMethods.Curves;
    using NumericalMethods.Statistics;
    using NumericalMethods.Iterations;

    /// Beta distribution
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class BetaDistribution : ProbabilityDensityFunction
    {
        /// First shape parameter of the distribution.
        private double _alpha1;
        /// Second shape parameter of the distribution.
        private double _alpha2;
        /// Norm of the distribution (cached for efficiency).
        private double _norm;
        /// Gamma distribution for alpha1 used for random generation (cached for efficiency).
        private GammaDistribution _gamma1;
        /// Gamma distribution for alpha2 used for random generation (cached for efficiency).
        private GammaDistribution _gamma2;
        /// Incomplete beta function for the distribution (cached for efficiency).
        private IncompleteBetaFunction _incompleteBetaFunction;

        /// Create a new instance of the Beta distribution with given shape and scale.
        /// @param shape1 double first shape parameter of the distribution (alpha1).
        /// @param shape2 double second shape parameter of the distribution (alpha2).
        /// @exception ArgumentOutOfRangeException
        ///						if the parameters of the distribution are illegal.
        public BetaDistribution(double shape1, double shape2)
        {
            if (shape1 <= 0)
                throw new ArgumentOutOfRangeException("First shape parameter must be positive");
            if (shape2 <= 0)
                throw new ArgumentOutOfRangeException("Second shape parameter must be positive");
            DefineParameters(shape1, shape2);
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h DhbScientificCurves.Histogram
        /// @exception ArgumentOutOfRangeException
        ///							when no suitable parameter can be found.
        public BetaDistribution(Histogram h)
        {
            if (h.Minimum < 0 || h.Maximum > 1)
                throw new ArgumentOutOfRangeException("Beta distribution is only defined over [0,1]");
            double average = h.Average;
            double variance = h.Variance;
            double a = ((1 - average) / variance - 1) * average;
            if (a <= 0)
                throw new ArgumentOutOfRangeException("Negative shape parameter");
            double b = (1 / average - 1) * a;
            if (b <= 0)
                throw new ArgumentOutOfRangeException("Negative shape parameter");
            DefineParameters(a, b);
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return _alpha1 / (_alpha1 + _alpha2); }

            set { throw new InvalidOperationException("Can not set average on Beta distribution"); }
        }

        /// Assigns new values to the parameters.
        /// This method assumes that the parameters have been already checked.
        private void DefineParameters(double shape1, double shape2)
        {
            _alpha1 = shape1;
            _alpha2 = shape2;
            _norm = GammaFunction.LogBeta(_alpha1, _alpha2);
            _gamma1 = null;
            _gamma2 = null;
            _incompleteBetaFunction = null;
        }

        private void DefineRandomGenerator()
        {
            _gamma1 = new GammaDistribution(_alpha1, 1.0);
            _gamma2 = new GammaDistribution(_alpha2, 1.0);
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from 0 to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            return IncompleteBetaFunction.Value(x);
        }

        /// This method was created in VisualAge.
        /// @return DhbIterations.IncompleteBetaFunction
        private IncompleteBetaFunction IncompleteBetaFunction
        {
            get
            {
                if (_incompleteBetaFunction == null)
                    _incompleteBetaFunction = new IncompleteBetaFunction(_alpha1, _alpha2);
                return _incompleteBetaFunction;
            }
        }

        /// @return double kurtosis of the distribution.
        public override double Kurtosis
        {
            get
            {
                double s = _alpha1 + _alpha2;
                return 3 * (_alpha1 + _alpha2 + 1) * (2 * s * s +
                                        (_alpha1 + _alpha2 - 6) * _alpha1 * _alpha2)
                                / ((_alpha1 + _alpha2 + 2) *
                                        (_alpha1 + _alpha2 + 3) * _alpha1 * _alpha2)
                                    - 3;
            }
        }

        /// @return java.lang.String name of the distribution.
        public override string Name
        {
            get { return "Beta distribution"; }
        }

        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public override double[] Parameters
        {
            get { return new double[] { _alpha1, _alpha2 }; }

            set { DefineParameters(value[0], value[1]); }
        }



        /// @return double a random number distributed according to the receiver.
        public override double Random()
        {
                if (_gamma1 == null)
                    DefineRandomGenerator();
                double y1 = _gamma1.Random();
                return y1 / (y1 + _gamma2.Random());
        }

        /// @param a1 double
        public double Alpha1
        {
            set { DefineParameters(value, _alpha2); }
        }

        /// @param a2 double
        public double Alpha2
        {
            set { DefineParameters(_alpha1, value); }
        }

        /// @return double skewness of the distribution.
        public override double Skewness
        {
            get
            {
                return 2 * Math.Sqrt(_alpha1 + _alpha2 + 1) * (_alpha2 - _alpha1)
                                / (Math.Sqrt(_alpha1 * _alpha2)
                                                        * (_alpha1 + _alpha2 + 2));
            }
        }

        /// @return string
        public override string ToString()
        {
            return string.Format(
                "Beta distribution({0:0.00000},{1:0.00000})", _alpha1, _alpha2 );
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            return Math.Exp(Math.Log(x) * (_alpha1 - 1)
                                + Math.Log(1 - x) * (_alpha2 - 1) - _norm);
        }

        /// @return double variance of the distribution.
        public override double Variance
        {
            get
            {
                double s = _alpha1 + _alpha2;
                return _alpha1 * _alpha2 / (s * s * (_alpha1 + _alpha2 + 1));
            }
        }
    }
}
