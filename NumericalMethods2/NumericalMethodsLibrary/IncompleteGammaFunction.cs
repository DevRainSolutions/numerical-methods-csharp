#region Using directives

using System;
using NumericalMethods.Interfaces;
using NumericalMethods.DhbFunctionEvaluation;


#endregion

namespace NumericalMethods.Iterations
{
    /// IncompleteGamma function
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class IncompleteGammaFunction : IOneVariableFunction
    {
	    /// Function parameter.
        private double _alpha;
	    /// Constant to be computed once only.
        private double _alphaLogGamma;
	    /// Infinite series.
        private IncompleteGammaFunctionSeries _series;
	    /// Continued fraction.
        private IncompleteGammaFunctionFraction _fraction;

        /// Constructor method.
        public IncompleteGammaFunction(double a)
        {
            _alpha = a;
            _alphaLogGamma = GammaFunction.LogGamma(_alpha);
        }

        /// @return double
        /// @param x double
        private double EvaluateFraction(double x)
        {
            if (_fraction == null)
            {
                _fraction = new IncompleteGammaFunctionFraction(_alpha);
                _fraction.DesiredPrecision = DhbMath.DefaultNumericalPrecision;
            }
            _fraction.Argument = x;
            _fraction.Evaluate();
            return _fraction.Result;
        }

        /// @return double		evaluate the series of the incomplete gamma function.
        /// @param x double
        private double EvaluateSeries(double x)
        {
            if (_series == null)
            {
                _series = new IncompleteGammaFunctionSeries(_alpha);
                _series.DesiredPrecision = DhbMath.DefaultNumericalPrecision;
            }
            _series.Argument = x;
            _series.Evaluate();
            return _series.Result;
        }

	    /// Returns the value of the function for the specified variable value.
        public double Value(double x)
        {
            if (x == 0)
                return 0;
            double norm = Math.Exp(Math.Log(x) * _alpha - x - _alphaLogGamma);
            return x - 1 < _alpha
                            ? EvaluateSeries(x) * norm
                            : 1 - norm / EvaluateFraction(x);
        }
    }
}
