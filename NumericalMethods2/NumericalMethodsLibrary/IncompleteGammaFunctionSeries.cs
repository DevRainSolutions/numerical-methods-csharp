#region Using directives

#endregion

namespace NumericalMethods.Iterations
{
    /// Series for the incompleteGamma function
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class IncompleteGammaFunctionSeries : InifiniteSeries
    {
	    /// Series parameter.
        private double _alpha;
	    /// Auxiliary sum.
        private double _sum;

        /// Constructor method
        /// @param a double	series parameter
        public IncompleteGammaFunctionSeries(double a)
        {
            _alpha = a;
        }

        /// Computes the n-th term of the series and stores it in lastTerm.
        /// @param n int
        protected override void ComputeTermAt(int n)
        {
            _sum += 1;
            _lastTerm *= _x / _sum;
        }

        /// initializes the series and return the 0-th term.
        protected override double InitialValue()
        {
            _lastTerm = 1 / _alpha;
            _sum = _alpha;
            return _lastTerm;
        }
    }
}
