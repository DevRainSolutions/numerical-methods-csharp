#region Using directives

using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Optimization
{
    /// Point & function holder used in minimizing one-variable functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class MinimizingPoint : OptimizingPoint
    {
        /// Constructor method.
        /// @param x double
        /// @param f DhbInterfaces.IOneVariableFunction
        public MinimizingPoint(double x, IOneVariableFunction f) : base(x, f)
        {
        }

        /// @return boolean	true if the receiver is "better" than
        ///												the supplied point
        /// @param point OptimizingPoint
        public override bool BetterThan(OptimizingPoint point)
        {
            return this.Value < point.Value;
        }

        /// (used by method ToString).
        /// @return string
        protected override sealed string PrintedKey()
        {
            return " min@";
        }
    }
}
