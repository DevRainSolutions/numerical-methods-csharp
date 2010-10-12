#region Using directives

#endregion

using NumericalMethods.Statistics;
namespace NumericalMethods.Statistics
{
    /// Statistical moments for a fixed set (1-4th order)
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class FixedStatisticalMoments : StatisticalMoments
    {
        /// Constructor method.
        public FixedStatisticalMoments() : base()
        {
        }

        /// Quick implementation of statistical moment accumulation up to order 4.
        /// @param x double
        public override void Accumulate(double x)
        {
            double n = _moments[0];
            double n1 = n + 1;
            double n2 = n * n;
            double delta = (_moments[1] - x) / n1;
            double d2 = delta * delta;
            double d3 = delta * d2;
            double r1 = (double)n / (double)n1;
            _moments[4] += 4 * delta * _moments[3] + 6 * d2 * _moments[2]
                                                    + (1 + n * n2) * d2 * d2;
            _moments[4] *= r1;
            _moments[3] += 3 * delta * _moments[2] + (1 - n2) * d3;
            _moments[3] *= r1;
            _moments[2] += (1 + n) * d2;
            _moments[2] *= r1;
            _moments[1] -= delta;
            _moments[0] = n1;
        }
    }
}
