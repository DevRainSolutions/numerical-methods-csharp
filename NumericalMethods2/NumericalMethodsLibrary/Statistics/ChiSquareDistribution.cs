#region Using directives

using System;
using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Statistics
{
    /// Chi square distribution.
    /// (as special case of the gamma distribution)
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class ChiSquareDistribution : GammaDistribution
    {

        /// Create a new instance as Gamma( n/2, 2).
        /// @param n int degrees of freedom of the receiver.
        public ChiSquareDistribution(int n) : base( 0.5 * n, 2.0)
        {
        }

        /// Create an instance of the receiver with parameters estimated from
        /// the given histogram using best guesses. This method can be used to
        /// find the initial values for a fit.
        /// @param h Histogram
        /// @exception ArgumentOutOfRangeException
        ///							when no suitable parameter can be found.
        public ChiSquareDistribution(IHistogram h)
        {
            if (h.Minimum < 0)
                throw new ArgumentOutOfRangeException(
                    "Chi square distribution is only defined for non-negative values");
            int dof = (int)Math.Round(h.Average);
            if (dof <= 0)
                throw new ArgumentOutOfRangeException(
                    "Chi square distribution is only defined for positive degrees of freedom");
            DegreesOfFreedom = dof;
        }

        /// @return double
        /// @param x double
        /// @exception ArgumentOutOfRangeException
        ///						if the argument is outside the expected range.
        public double ConfidenceLevel(double x)
        {
            return x < 0 ? double.NaN : (1 - DistributionValue(x)) * 100;
        }

        /// @return string name of the distribution.
        public override string Name
        {
            get { return "Chi square distribution"; }
        }

        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        /// Note: for fitting, non-integer degree of dreedom is allowed
        /// @param params double[]	assigns the parameters
        public override double[] Parameters
        {
            get { return new double[] { _alpha * 2 }; }
            set { DefineParameters(value[0] * 0.5, 2); }
        }

        /// @param n int
        public int DegreesOfFreedom
        {
            set { base.DefineParameters(0.5 * value, 2.0); }
        }

        /// @return string
        public override string ToString()
        {
            return string.Format("{0} ({1:0.00})", Name, _alpha * 2);
        }
    }
}
