#region Using directives

#endregion

namespace NumericalMethods.Interfaces
{
    /// IManyVariableFunction is an interface for mathematical functions
    /// of many variables, that is functions of the form:
    /// 				f(X) where X is a vector.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public interface IManyVariableFunction
    {
        /// Returns the value of the function for the specified vector.
        double Value(params double[] x);
    }
}
