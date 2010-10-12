#region Using directives

using NumericalMethods.Interfaces;
using NumericalMethods.Optimization;

#endregion

namespace NumericalMethods.Optimization
{
    /// Factory of point/vector & function holders for maximizing functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class MaximizingPointFactory : OptimizingPointFactory
    {
        /// Constructor method.
        public MaximizingPointFactory() : base()
        {
        }

        /// @return OptimizingPoint	an maximizing point strategy.
        public override OptimizingPoint CreatePoint(double x, IOneVariableFunction f)
        {
            return new MaximizingPoint(x, f);
        }

        /// @return OptimizingVector	an maximizing vector strategy.
        public override OptimizingVector CreateVector(double[] v, IManyVariableFunction f)
        {
            return new MaximizingVector(v, f);
        }
    }
}
