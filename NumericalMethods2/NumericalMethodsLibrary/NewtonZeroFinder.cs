#region Using directives

using System;
using NumericalMethods.Interfaces;
using NumericalMethods.DhbFunctionEvaluation;

#endregion

namespace NumericalMethods.Iterations
{
    /// Finds the zeroes of a function using Newton approximation.
    /// Note: the zero of a function if the value at which the function's
    /// value is zero.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class NewtonZeroFinder : FunctionalIterator
    {
        /// Derivative of the function for which the zero will be found.
        private IOneVariableFunction _df;

        /// Constructor method.
        /// @param func the function for which the zero will be found.
        /// @param start the initial value for the search.
        public NewtonZeroFinder(IOneVariableFunction func, double start) : base(func)
        {
            StartingValue = start;
        }

        /// Constructor method.
        /// @param func the function for which the zero will be found.
        /// @param dFunc derivative of func.
        /// @param start the initial value for the search.
        public NewtonZeroFinder(IOneVariableFunction func,
                                    IOneVariableFunction dFunc, double start) : this(func, start)
        {
            this.Derivative = dFunc;
        }

        /// Evaluate the _result of the current interation.
        /// @return the estimated precision of the _result.
        public override double EvaluateIteration()
        {
            double delta = _f.Value(_result) / _df.Value(_result);
            _result -= delta;
            return RelativePrecision(Math.Abs(delta));
        }

        /// Initializes internal parameters to start the iterative process.
        /// Assigns default derivative if necessary.
        public override void InitializeIterations()
        {
            if (_df == null)
                _df = new FunctionDerivative(_f);
            if (double.IsNaN(_result))
                _result = 0;
            int n = 0;
            Random random = new Random();
            while (DhbMath.Equal(_df.Value(_result), 0))
            {
                if (++n > MaximumIterations)
                    break;
                _result += random.NextDouble();
            }
        }

        /// (c) Copyrights Didier BESSET, 1999, all rights reserved.
        /// @param dFunc DhbInterfaces.OneVariableFunction
        /// @exception ArgumentException
        ///							if the derivative is not accurate.
        public IOneVariableFunction Derivative
        {
            set
            {
                _df = new FunctionDerivative(_f);
                if (!DhbMath.Equal(_df.Value(_result), value.Value(_result), 0.001))
                    throw new ArgumentException
                                    ("Supplied derative function is inaccurate");
                _df = value;
            }
        }

        /// (c) Copyrights Didier BESSET, 1999, all rights reserved.
        public override IOneVariableFunction Function
        {
            set
            {
                base.Function = value;
                _df = null;
            }
        }

        /// Defines the initial value for the search.
        public double StartingValue
        {
            set { _result = value; }
        }
    }
}
