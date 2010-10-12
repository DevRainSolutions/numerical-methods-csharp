#region Using directives

using System;
using NumericalMethods.DhbFunctionEvaluation;

#endregion

namespace NumericalMethods.Iterations
{
    /// Continued fraction
    /// 
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public abstract class ContinuedFraction : IterativeProcess
    {
	    /// Best approximation of the fraction.
        private double _result;

	    /// Fraction's argument.
        protected double _x;

	    /// Fraction's accumulated numerator.
        private double _numerator;

	    /// Fraction's accumulated denominator.
        private double _denominator;

	    /// Fraction's next factors.
        protected double[] _factors = new double[2];

        /// Compute the pair numerator/denominator for iteration n.
        /// @param n int
        protected abstract void ComputeFactorsAt(int n);

        /// @return double
        public override double EvaluateIteration()
        {
            ComputeFactorsAt(Iterations);
            _denominator = 1 / LimitedSmallValue(_factors[0] * _denominator
                                                                + _factors[1]);
            _numerator = LimitedSmallValue(_factors[0] / _numerator + _factors[1]);
            double delta = _numerator * _denominator;
            _result *= delta;
            return Math.Abs(delta - 1);
        }

        /// @return double
        public double Result
        {
            get { return _result; }
        }

        public override void InitializeIterations()
        {
            _numerator = LimitedSmallValue(InitialValue());
            _denominator = 0;
            _result = _numerator;
        }

        /// @return double
        protected abstract double InitialValue();

        /// Protection against small factors.
        /// @return double
        /// @param r double
        private double LimitedSmallValue(double r)
        {
            return Math.Abs(r) < DhbMath.SmallNumber
                                                ? DhbMath.SmallNumber : r;
        }

        /// @param r double	the value of the series argument.
        public double Argument
        {
            set { _x = value; }
        }
    }
}
