#region Using directives

using System;
using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Iterations
{
    /// Zero finding by bisection.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class BisectionZeroFinder : FunctionalIterator
    {
	    /// Value at which the function's value is negative.
        private double _xNeg;
	    /// Value at which the function's value is positive.
        private double _xPos;

        /// @param func DhbInterfaces.OneVariableFunction
        public BisectionZeroFinder(IOneVariableFunction func) : base(func)
        {
        }

        /// @param func DhbInterfaces.OneVariableFunction
        /// @param x1 location at which the function yields a negative value
        /// @param x2 location at which the function yields a positive value
        public BisectionZeroFinder(IOneVariableFunction func, double x1, double x2)
											: this(func)
        {
            this.NegativeX = x1;
            this.PositiveX = x2;
        }

        /// @return double
        public override double EvaluateIteration()
        {
            _result = (_xPos + _xNeg) * 0.5;
            if (_f.Value(_result) > 0)
                _xPos = _result;
            else
                _xNeg = _result;
            return RelativePrecision(Math.Abs(_xPos - _xNeg));
        }

        /// @param x double
        /// @exception ArgumentException
        /// 					if the function's value is not negative
        public double NegativeX
        {
            set
            {
                if (_f.Value(value) > 0)
                    throw new ArgumentException("f(" + value +
                                            ") is positive instead of negative");
                _xNeg = value;
            }
        }

        /// (c) Copyrights Didier BESSET, 1999, all rights reserved.
        /// @param x double
        /// @exception ArgumentException
        /// 					if the function's value is not positive
        public double PositiveX
        {
            set
            {
                if (_f.Value(value) < 0)
                    throw new ArgumentException("f(" + value +
                                            ") is negative instead of positive");
                _xPos = value;
            }
        }
    }
}
