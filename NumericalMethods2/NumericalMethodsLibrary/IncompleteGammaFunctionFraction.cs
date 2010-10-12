#region Using directives

#endregion

namespace NumericalMethods.Iterations
{
    /// Continued fraction for the incompleteGamma function
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class IncompleteGammaFunctionFraction : ContinuedFraction
    {
	    /// Series parameter.
        private double _alpha;
	    /// Auxiliary sum.
        private double _sum;

        /// Constructor method.
        /// @param a double
        public IncompleteGammaFunctionFraction(double a)
        {
            _alpha = a;
        }

        /// Compute the pair numerator/denominator for iteration n.
        /// @param n int
        protected override void ComputeFactorsAt(int n)
        {
            _sum += 2;
            _factors[0] = (_alpha - n) * n;
            _factors[1] = _sum;
        }

        protected override double InitialValue()
        {
            _sum = _x - _alpha + 1;
            return _sum;
        }
    }
}
