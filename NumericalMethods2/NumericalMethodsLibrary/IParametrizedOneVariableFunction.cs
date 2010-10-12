#region Using directives

#endregion

namespace NumericalMethods.Interfaces
{
    /// IParametrizedOneVariableFunction is an interface for mathematical
    /// functions of one variable depending on several parameters,
    /// that is functions of the form f(x;p), where p is a vector.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public interface IParametrizedOneVariableFunction
                                            : IOneVariableFunction
    {
        /// array containing the parameters
        double[] Parameters { get; set; }

        /// Evaluate the function and the gradient of the function with respect
        /// to the parameters.
        /// @return double[]	0: function's value, 1,2,...,n function's gradient
        /// @param x double
        double[] ValueAndGradient(double x);
    }
}
