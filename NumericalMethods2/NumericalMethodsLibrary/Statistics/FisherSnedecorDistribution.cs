#region Using directives

using System;
using NumericalMethods.Interfaces;
using NumericalMethods.DhbFunctionEvaluation;
using NumericalMethods.Iterations;

#endregion

namespace NumericalMethods.Statistics
{
    /// Fisher-Snedecor distribution
    /// (distribution used to perform the F-test).
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class FisherSnedecorDistribution
                                    : ProbabilityDensityFunction
    {
	    /// First degree of freedom.
        private int _dof1;
	    /// Second degree of freedom.
        private int _dof2;
	    /// Norm (stored for efficiency).
        private double _norm;
	    /// Function used to compute the distribution.
        private IncompleteBetaFunction _incompleteBetaFunction = null;
	    /// Auxiliary distributions for random number generation.
        private ChiSquareDistribution _chiSquareDistribution1 = null;
        private ChiSquareDistribution _chiSquareDistribution2 = null;

        /// Create a new instance of the Fisher-Snedecor distribution with
        ///											given degrees of freedom.
        /// @param n1 int	first degree of freedom
        /// @param n2 int	second degree of freedom
        /// @exception ArgumentOutOfRangeException
        ///			one of the specified degrees of freedom is non-positive.
        public FisherSnedecorDistribution(int n1, int n2)
        {
            if (n1 <= 0)
                throw new ArgumentOutOfRangeException(
                                "First degree of freedom must be positive");
            if (n2 <= 0)
                throw new ArgumentOutOfRangeException(
                                "Second degree of freedom must be positive");
            DefineParameters(n1, n2);
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h Histogram
        /// @exception ArgumentOutOfRangeException
        ///							when no suitable parameter can be found.
        public FisherSnedecorDistribution(IHistogram h)
        {
            if (h.Minimum < 0)
                throw new ArgumentOutOfRangeException(
        "Fisher-Snedecor distribution is only defined for non-negative values");
            int n2 = (int)Math.Round(2 / (1 - 1 / h.Average));
            if (n2 <= 0)
                throw new ArgumentOutOfRangeException(
            "Fisher-Snedecor distribution has positive degrees of freedom");
            double a = 1 - (n2 - 2) * (n2 - 4) * h.Variance / (2 * 2 * n2);
            int n1 = (int)Math.Round(0.7 * (n2 - 2) / a);
            if (n1 <= 0)
                throw new ArgumentOutOfRangeException(
            "Fisher-Snedecor distribution has positive degrees of freedom");
            DefineParameters(n1, n2);
        }

        /// @return double average of the distribution.
        public override double Average
        {
            get { return _dof2 > 2 ? _dof2 / (_dof2 - 2) : double.NaN; }
            set { throw new InvalidOperationException("Set not defined for FisherSnedecorDistribution.Average"); }
        }

        /// @return double
        /// @param x double
        /// @exception ArgumentOutOfRangeException
        ///						if the argument is outside the expected range.
        public double ConfidenceLevel(double x)
        {
            if (x <= 0)
                throw new ArgumentOutOfRangeException("F-CL argument must be positive");
            return x < 1 ? (1 - DistributionValue(x) + DistributionValue(1 / x)) * 100
                         : (1 - DistributionValue(1 / x) + DistributionValue(x)) * 100;
        }

        /// Assigns new degrees of freedom to the receiver.
        /// Compute the norm of the distribution after a change of parameters.
        /// @param n1 int	first degree of freedom
        /// @param n2 int	second degree of freedom
        public void DefineParameters(int n1, int n2)
        {
            _dof1 = n1;
            _dof2 = n2;
            double nn1 = 0.5 * n1;
            double nn2 = 0.5 * n2;
            _norm = nn1 * Math.Log(n1) + nn2 * Math.Log(n2)
                                        - GammaFunction.LogBeta(nn1, nn2);
            _incompleteBetaFunction = null;
            _chiSquareDistribution1 = null;
            _chiSquareDistribution2 = null;
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from 0 to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            return IncompleteBetaFunction.Value(_dof2 / (x * _dof1 + _dof2));
        }

        private IncompleteBetaFunction IncompleteBetaFunction
        {
            get
            {
                if (_incompleteBetaFunction == null)
                    _incompleteBetaFunction = new IncompleteBetaFunction(
                                                        0.5 * _dof1, 0.5 * _dof2);
                return _incompleteBetaFunction;
            }
        }

        /// @return string	name of the distribution.
        public override string Name
        {
            get { return "Fisher-Snedecor distribution"; }
        }

        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public override double[] Parameters
        {
            get { return new double[] { _dof1, _dof2 }; }
            set
            {
                DefineParameters((int)Math.Round(value[0]), (int)Math.Round(value[1]));
            }
        }

        /// @return double a random number distributed according to the receiver.
        public override double Random()
        {
            if (_chiSquareDistribution1 == null)
            {
                _chiSquareDistribution1 = new ChiSquareDistribution(_dof1);
                _chiSquareDistribution2 = new ChiSquareDistribution(_dof2);
            }
            return _chiSquareDistribution1.Random() * _dof2
                    / (_chiSquareDistribution2.Random() * _dof1);
        }

        /// @return string
        public override string ToString()
        {
            return string.Format("{0} ({1},{2})", Name, _dof1, _dof2);
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            return x > 0
                    ? Math.Exp(_norm + Math.Log(x) * (_dof1 / 2 - 1)
                                    - Math.Log(x * _dof1 + _dof2)
                                                    * ((_dof1 + _dof2) / 2))
                    : 0;
        }

        /// @return double variance of the distribution.
        public override double Variance
        {
            get
            {
                return _dof2 > 4
                        ? _dof2 * _dof2 * 2 * (_dof1 + _dof2 + 2)
                                            / (_dof1 * (_dof2 - 2) * (_dof2 - 4))
                        : double.NaN;
            }
        }
    }
}
