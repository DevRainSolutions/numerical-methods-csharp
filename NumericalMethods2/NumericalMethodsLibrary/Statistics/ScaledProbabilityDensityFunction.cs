#region Using directives

using NumericalMethods.Interfaces;
using NumericalMethods.Statistics;
#endregion

namespace NumericalMethods.Statistics
{
    /// Construct a function from a probability density function
    /// for a given norm.
    /// 
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class ScaledProbabilityDensityFunction : IParametrizedOneVariableFunction
    {
	    /// Total count of the histogram.
        private double _count;
	    /// Bin width of the histogram.
        private double _binWidth;
	    /// Probability density function
        private ProbabilityDensityFunction _density;

        /// @param pdf DhbStatistics.ProbabilityDensityFunction
        /// @param n long
        /// @param w double
        public ScaledProbabilityDensityFunction(
                            ProbabilityDensityFunction pdf, long n, double w)
        {
            _density = pdf;
            Count = n;
            _binWidth = w;
        }

        /// @param f statistics.ProbabilityDensity
        /// @param hist curves.Histogram
        public ScaledProbabilityDensityFunction(ProbabilityDensityFunction f, IHistogram hist)
                        	: this( f, hist.Count, hist.BinWidth)
        {
        }

        /// The array contains the parameters of the distribution
        /// and the estimated number of events.
        /// @return double[] an array containing the parameters of 
        ///												the distribution.
        public double[] Parameters
        {
            get
            {
                double[] answer = new double[_density.Parameters.Length + 1];
                _density.Parameters.CopyTo(answer, 0);
                answer[answer.Length - 1] = _count;
                return answer;
            }
            set
            {
                _count = value[value.Length - 1];
                _density.Parameters = value;
            }
        }

        /// @param x double	total count in the receiver
        public double Count
        {
            set { _count = value; }
        }

        /// @return string
        public override string ToString()
        {
            return "Scaled " + _density;
        }

        /// @return double the value of the function.
        /// @param x double
        public double Value(double x)
        {
            return _count * _binWidth * _density.Value(x);
        }

        /// Evaluate the function and the gradient of the function with respect
        /// to the parameters.
        /// @return double[]	0: function's value, 1,2,...,n function's gradient
        /// @param x double
        public double[] ValueAndGradient(double x)
        {
            double[] dpg = _density.ValueAndGradient(x);
            double[] answer = new double[dpg.Length + 1];
            double r = _binWidth * _count;
            for (int i = 0; i < dpg.Length; i++)
                answer[i] = dpg[i] * r;
            answer[dpg.Length] = dpg[0] * _binWidth;
            return answer;
        }
    }
}
