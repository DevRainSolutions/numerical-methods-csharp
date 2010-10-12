
namespace NumericalMethods.Iterations
{
    using System;
    using NumericalMethods.Interfaces;
    using NumericalMethods.DhbFunctionEvaluation;

    /// Incomplete Beta function
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class IncompleteBetaFunction : IOneVariableFunction
    {
	    /// Function parameters.
        private double _alpha1;
        private double _alpha2;
	    /// Constant to be computed once only.
        private double _logNorm;
	    /// Continued fractions.
        private IncompleteBetaFunctionFraction _fraction;
        //private IncompleteBetaFunctionFraction inverseFraction;

        /// Constructor method.
        /// @param a1 double
        /// @param a2 double
        public IncompleteBetaFunction(double a1, double a2)
        {
            _alpha1 = a1;
            _alpha2 = a2;
            _logNorm = GammaFunction.LogGamma(_alpha1 + _alpha2)
                                    - GammaFunction.LogGamma(_alpha1)
                                    - GammaFunction.LogGamma(_alpha2);
        }

        /// @return double
        /// @param x double
        private double EvaluateFraction(double x)
        {
            if (_fraction == null)
            {
                _fraction = new IncompleteBetaFunctionFraction(_alpha1, _alpha2);
                _fraction.DesiredPrecision = DhbMath.DefaultNumericalPrecision;
            }
            _fraction.Argument = x;
            _fraction.Evaluate();
            return _fraction.Result;
        }

        /// @return double
        /// @param x double
        private double EvaluateInverseFraction(double x)
        {
            if (_fraction == null)
            {
                _fraction = new IncompleteBetaFunctionFraction(_alpha2, _alpha1);
                _fraction.DesiredPrecision = DhbMath.DefaultNumericalPrecision;
            }
            _fraction.Argument = x;
            _fraction.Evaluate();
            return _fraction.Result;
        }

        public double Value(double x)
        {
            if (x == 0)
                return 0;
            if (x == 1)
                return 1;
            double norm = Math.Exp(_alpha1 * Math.Log(x)
                                        + _alpha2 * Math.Log(1 - x) + _logNorm);
            return (_alpha1 + _alpha2 + 2) * x < (_alpha1 + 1)
                            ? norm / (EvaluateFraction(x) * _alpha1)
                            : 1 - norm / (EvaluateInverseFraction(1 - x)
                                                                    * _alpha2);
        }
    }
}
