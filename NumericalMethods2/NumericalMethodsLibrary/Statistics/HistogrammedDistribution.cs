#region Using directives

using System;
using NumericalMethods.Curves;
using NumericalMethods.Statistics;

#endregion

namespace NumericalMethods.Statistics
{
    /// Distribution constructed on a histogram.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class HistogrammedDistribution : ProbabilityDensityFunction
    {
        Histogram _histogram;

        //TODO: this constructor is mentioned in section 9.8.3 but it's not part of Listing 9.24 code
        public HistogrammedDistribution(Histogram histogram)
        {
            _histogram = histogram;
        }

        /// @return double average of the histogram.
        public override double Average
        {
            get { return _histogram.Average; }
            set { throw new InvalidOperationException("Can not set the Average of a HistogramedDistribution"); }
        }

        /// Returns the probability of finding a random variable smaller
        /// than or equal to x.
        /// @return integral of the probability density function from -infinity to x.
        /// @param x double upper limit of integral.
        public override double DistributionValue(double x)
        {
            if (x < _histogram.Minimum)
                return 0;
            else if (x < _histogram.Maximum)
                return _histogram.CountsUpTo(x) / _histogram.TotalCount;
            else
                return 1;
        }

        /// @return double
        /// @param x1 double
        /// @param x2 double
        public override double DistributionValue(double x1, double x2)
        {
            return _histogram.GetCountsBetween(Math.Max(x1,
                                                    _histogram.Minimum),
                                                Math.Min(x2,
                                                    _histogram.Maximum))
                                        / _histogram.TotalCount;
        }

        /// @return double kurtosis of the histogram.
        public override double Kurtosis
        {
            get { return _histogram.Kurtosis; }
        }

        /// @return string name of the distribution.
        public override string Name
        {
            get { return "Experimental distribution"; }
        }

        /// NOTE: this method is a dummy because the distribution
        /// cannot be fitted.
        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public override double[] Parameters
        {
            get { return new double[0]; }
            /// This method is a dummy method, needed for the compiler because
            /// the superclass requires implementation of the
            /// 						interface ParametrizedOneVariableFunction.
            /// Histogrammed distributions cannot be fitted.
            set { throw new InvalidOperationException("Can not set the Parameters of a HistogrammedDistribution"); }
        }

        /// @return double skewness of the histogram.
        public override double Skewness
        {
            get { return _histogram.Skewness; }
        }

        /// @return double probability density function
        /// @param x double random variable
        public override double Value(double x)
        {
            return (x >= _histogram.Minimum
                                            || x < _histogram.Maximum)
                        ? _histogram.GetBinContent(x)
                                        / (_histogram.TotalCount
                                                    + _histogram.BinWidth)
                        : 0;
        }

        /// @return double variance of the histogram.
        public override double Variance
        {
            get { return _histogram.Variance; }
        }
    }
}
