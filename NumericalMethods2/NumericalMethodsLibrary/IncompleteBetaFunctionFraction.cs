#region Using directives

#endregion

namespace NumericalMethods.Iterations
{
    /// Incomplete Beta function fraction
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class IncompleteBetaFunctionFraction : ContinuedFraction
    {
	    /// Fraction's parameters.
        private double _alpha1;
        private double _alpha2;

        /// Constructor method.
        /// @param a1 double
        /// @param a2 double
        public IncompleteBetaFunctionFraction(double a1, double a2)
        {
            _alpha1 = a1;
            _alpha2 = a2;
        }

        /// Compute the pair numerator/denominator for iteration n.
        /// @param n int
        protected override void ComputeFactorsAt(int n)
        {
            int m = n / 2;
            int m2 = 2 * m;
            _factors[0] = m2 == n
                            ? _x * m * (_alpha2 - m)
                                        / ((_alpha1 + m2) * (_alpha1 + m2 - 1))
                            : -_x * (_alpha1 + m) * (_alpha1 + _alpha2 + m)
                                        / ((_alpha1 + m2) * (_alpha1 + m2 + 1));
        }

        protected override double InitialValue()
        {
            _factors[1] = 1;
            return 1;
        }
    }
}
