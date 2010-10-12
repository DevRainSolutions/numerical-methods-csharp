namespace NumericalMethods.Statistics
{
    using System;
    using NumericalMethods.Interfaces;
    using NumericalMethods.DhbFunctionEvaluation;
    using NumericalMethods.Iterations;

    /// Student distribution
    /// used in computing the t-test.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class StudentDistribution : ProbabilityDensityFunction
    {
	    /// Degree of freedom.
        private int _dof;
	    /// Norm (stored for efficiency).
        private double _norm;
	    /// Function used to compute the distribution.
        private IncompleteBetaFunction _incompleteBetaFunction = null;
	    /// Auxiliary distribution for random number generation.
        private ChiSquareDistribution _chiSquareDistribution = null;

        /// Constructor method.
        /// @param n int	degree of freedom
        /// @exception ArgumentOutOfRangeException
        ///				when the specified degree of freedom is non-positive.
        public StudentDistribution(int n)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException(
                                        "Degree of freedom must be positive");
            DefineParameters(n);
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h DhbScientificCurves.Histogram
        /// @exception ArgumentOutOfRangeException
        ///							when no suitable parameter can be found.
        public StudentDistribution(IHistogram h)
        {
            double variance = h.Variance;
            if (variance <= 0)
                throw new ArgumentOutOfRangeException(
                        "Student distribution is only defined for positive variance");
            DefineParameters((int)Math.Max(1,
                                        Math.Round(2 / (1 - 1 / variance))));
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return 0; }
            set { throw new InvalidOperationException("Set not defined for FisherSnedecorDistribution.Average"); }
        }

        /// @return double
        /// @param x double
        /// @exception ArgumentOutOfRangeException
        ///										if the argument is illegal.
        public double ConfidenceLevel(double x)
        {
            return x < 0 ? double.NaN : (1 - SymmetricAcceptance(x)) * 100;
        }

        /// @param n int	degree of freedom
        public void DefineParameters(int n)
        {
            _dof = n;
            _norm = -(Math.Log(_dof) * 0.5
                                    + GammaFunction.LogBeta(_dof * 0.5, 0.5));
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from -infinity to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            if (x == 0)
                return 0.5;
            double acc = SymmetricAcceptance(Math.Abs(x));
            return x > 0 ? 1 + acc : 1 - acc;
        }

        /// @return DhbIterations.IncompleteBetaFunction
        private IncompleteBetaFunction IncompleteBetaFunction
        {
            get
            {
                if (_incompleteBetaFunction == null)
                    _incompleteBetaFunction = new IncompleteBetaFunction(_dof / 2, 0.5);
                return _incompleteBetaFunction;
            }
        }

        /// @return double kurtosis of the distribution.
        public override double Kurtosis
        {
            get { return _dof > 4 ? 6 / (_dof - 4) : double.NaN; }
        }

        /// @return string the name of the distribution.
        public override string Name
        {
            get { return "Student distribution"; }
        }

        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public override double[] Parameters
        {
            get { return new double[] { _dof }; }

            set { DefineParameters((int)Math.Round(value[0])); }
        }

        /// @return double a random number distributed according to the receiver.
        public override double Random()
        {
            if (_chiSquareDistribution == null)
            {
                _chiSquareDistribution = new ChiSquareDistribution(_dof - 1);
            }
            return Generator.NextGaussian() * Math.Sqrt((_dof - 1) / _chiSquareDistribution.Random());
        }

        /// @return double skewness of the distribution.
        public override double Skewness
        {
            get { return 0; }
        }

        /// @return double	integral from -x to x
        /// @param x double
        private double SymmetricAcceptance(double x)
        {
            return IncompleteBetaFunction.Value(_dof / (x * x + _dof));
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            return Math.Exp(_norm - Math.Log(x * x / _dof + 1) * (_dof + 1) / 2);
        }

        /// @return double variance of the distribution.
        public override double Variance
        {
            get { return _dof > 2 ? _dof / (_dof - 2) : double.NaN; }
        }
    }
}
