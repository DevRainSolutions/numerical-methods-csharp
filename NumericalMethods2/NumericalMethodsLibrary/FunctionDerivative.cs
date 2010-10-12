namespace NumericalMethods.DhbFunctionEvaluation
{
    using NumericalMethods.Interfaces;

    /// Evaluate an approximation of the derivative of a given function.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class FunctionDerivative : IOneVariableFunction
    {
        /// Function for which the derivative is computed.
        private IOneVariableFunction _f;
        /// Relative interval variation to compute derivative.
        private double _relativePrecision = 0.0001;

        /// Constructor method.
        /// @param func the function for which the derivative is computed.
        public FunctionDerivative(IOneVariableFunction func) : this( func, 0.000001)
        {
        }

        /// Constructor method.
        /// @param func the function for which the derivative is computed.
        /// @param precision the relative step used to compute the derivative.
        public FunctionDerivative(IOneVariableFunction func, double precision)
        {
            _f = func;
            _relativePrecision = precision;
        }

        /// Returns the value of the function's derivative
        /// for the specified variable value.
        public double Value(double x)
        {
            double x1 = x == 0 ? _relativePrecision
                               : x * (1 + _relativePrecision);
            double x2 = 2 * x - x1;
            return (_f.Value(x1) - _f.Value(x2)) / (x1 - x2);
        }
    }
}
