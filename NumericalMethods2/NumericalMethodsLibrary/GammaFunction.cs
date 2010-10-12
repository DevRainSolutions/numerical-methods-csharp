#region Using directives

using System;

#endregion

namespace NumericalMethods.DhbFunctionEvaluation
{
    /// Gamma function (Euler's integral).
    /// 
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class GammaFunction
    {
        static double _sqrt2Pi = Math.Sqrt(2 * Math.PI);
        static double[] _coefficients = { 76.18009172947146,
								    -86.50532032941677,
								     24.01409824083091,
								     -1.231739572450155,
								      0.1208650973866179e-2,
								     -0.5395239384953e-5};

        /// @return double		beta function of the arguments
        /// @param x double
        /// @param y double
        public static double Beta(double x, double y)
        {
            return Math.Exp(LogGamma(x) + LogGamma(y) - LogGamma(x + y));
        }

        /// @return long	factorial of n
        /// @param n long
        public static long Factorial(long n)
        {
            return n < 2 ? 1 : n * Factorial(n - 1);
        }

        /// @return double		gamma function
        /// @param x double
        public static double Gamma(double x)
        {
            return x > 1
                        ? Math.Exp(LeadingFactor(x)) * Series(x) * _sqrt2Pi / x
                        : (x > 0 ? Gamma(x + 1) / x
                                        : double.NaN);
        }

        /// @return double
        /// @param x double
        private static double LeadingFactor(double x)
        {
            double temp = x + 5.5;
            return Math.Log(temp) * (x + 0.5) - temp;
        }

        /// @return double	logarithm of the beta function of the arguments
        /// @param x double
        /// @param y double
        public static double LogBeta(double x, double y)
        {
            return LogGamma(x) + LogGamma(y) - LogGamma(x + y);
        }

        /// @return double		log of the gamma function
        /// @param x double
        public static double LogGamma(double x)
        {
            return x > 1
                        ? LeadingFactor(x) + Math.Log(Series(x) * _sqrt2Pi / x)
                        : (x > 0 ? LogGamma(x + 1) - Math.Log(x)
                                        : Double.NaN);
        }

        /// @return double		value of the series in Lanczos formula.
        /// @param x double
        private static double Series(double x)
        {
            double answer = 1.000000000190015d;
            double term = x;
            for (int i = 0; i < 6; i++)
            {
                term += 1;
                answer += _coefficients[i] / term;
            }
            return answer;
        }
    }
}
