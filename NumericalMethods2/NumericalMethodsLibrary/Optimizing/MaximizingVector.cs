#region Using directives

using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Optimization
{
    /// Vector & function holder used in maximizing many-variable functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class MaximizingVector : OptimizingVector
    {
        /// Constructor method.
        /// @param v double[]
        /// @param f DhbInterfaces.IManyVariableFunction
        public MaximizingVector(double[] v,
                                        IManyVariableFunction f) : base(v, f)
        {
        }

        /// @return bool	true if the receiver is "better" than
        ///												the supplied point
        /// @param point OptimizingVector
        public override bool BetterThan(OptimizingVector point)
        {
            return this.Value > point.Value;
        }

        /// (used by method ToString).
        /// @return string
        protected override sealed string PrintedKey()
        {
            return " max@";
        }
    }
}
