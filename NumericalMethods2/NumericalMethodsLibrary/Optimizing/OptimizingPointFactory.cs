#region Using directives

using NumericalMethods.Interfaces;
using NumericalMethods.MatrixAlgebra;

#endregion

namespace NumericalMethods.Optimization
{
    /// Factory of point/vector & function holders for optimizing functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public abstract class OptimizingPointFactory
    {
        /// Constructor method.
        public OptimizingPointFactory() : base()
        {
        }

        /// @return DhbOptimizing.OptimizingPoint
        /// @param x double
        /// @param f IOneVariableFunction
        public abstract OptimizingPoint CreatePoint(double x, IOneVariableFunction f);

        /// @return DhbOptimizing.OptimizingVector
        /// @param v double[]
        /// @param f IManyVariableFunction
        public abstract OptimizingVector CreateVector(double[] v, IManyVariableFunction f);

        /// @return DhbOptimizing.OptimizingVector
        /// @param v DhbVector
        /// @param f IManyVariableFunction
        public OptimizingVector CreateVector(DhbVector v, IManyVariableFunction f)
        {
            return CreateVector(v.ToComponents(), f);
        }
    }
}
