#region Using directives

#endregion

namespace NumericalMethods.Interfaces
{
    /// IOneVariableFunction is an interface for mathematical functions of
    /// a single variable, that is functions of the form f(x).
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public interface IOneVariableFunction
    {
        /// Returns the value of the function for the specified variable value.
        double Value(double x);
    }
}
