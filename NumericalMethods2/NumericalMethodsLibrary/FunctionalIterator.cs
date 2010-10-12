#region Using directives

using System;
using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Iterations
{
    /// Iterative process based on a one-variable function,
    /// having a single numerical _result.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public abstract class FunctionalIterator : IterativeProcess
    {
        /// Best approximation of the zero.
        protected double _result = double.NaN;
        /// Function for which the zero will be found.
        protected IOneVariableFunction _f;

        /// Generic constructor.
        /// @param func OneVariableFunction
        /// @param start double
        public FunctionalIterator(IOneVariableFunction func)
        {
            this.Function = func;
        }

/// Returns the _result (assuming convergence has been attained).
        public double Result
        {
            get { return _result; }
        }

        /// @return double
        /// @param epsilon double
        public double RelativePrecision(double epsilon)
        {
            return RelativePrecision(epsilon, Math.Abs(_result));
        }

        /// @param func DhbInterfaces.OneVariableFunction
        public virtual IOneVariableFunction Function
        {
            set { _f = value; }
        }

        /// @param x double
        public double InitialValue
        {
            set { _result = value; }
        }
    }
}
