#region Using directives

using System;
using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Statistics
{
     /// This class is used to find the inverse distribution function of
    /// a probability density function.
    /// 
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class OffsetDistributionFunction : IOneVariableFunction
    {
    	/// Probability density function.
        private ProbabilityDensityFunction _probabilityDensity;

    	/// Value for which the inverse value is desired.
        private double _offset;

        /// Create a new instance with given parameters.
        /// @param p statistics.ProbabilityDensityFunction
        /// @param x double
        public OffsetDistributionFunction(ProbabilityDensityFunction p,
                                                                    double x)
        {
            _probabilityDensity = p;
            _offset = x;
        }

        /// @return distribution function minus the offset.
        public double Value(double x)
        {
            return _probabilityDensity.DistributionValue(x) - _offset;
        }
    }
}
