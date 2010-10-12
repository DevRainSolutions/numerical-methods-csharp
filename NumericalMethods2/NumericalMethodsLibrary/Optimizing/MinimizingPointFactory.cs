#region Using directives

using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Optimization
{
    /// Factory of point/vector & function holders for minimizing functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class MinimizingPointFactory : OptimizingPointFactory
    {
        /// Constructor method.
        public MinimizingPointFactory() : base()
        {
        }

        /// @return OptimizingPoint	an minimizing point strategy.
        public override OptimizingPoint CreatePoint(double x, IOneVariableFunction f)
        {
            return new MinimizingPoint(x, f);
        }

        /// @return OptimizingVector	an minimizing vector strategy.
        public override OptimizingVector CreateVector(double[] v, IManyVariableFunction f)
        {
            return new MinimizingVector(v, f);
        }
    }
}
