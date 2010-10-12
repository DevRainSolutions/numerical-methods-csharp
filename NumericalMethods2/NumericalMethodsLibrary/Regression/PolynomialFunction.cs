#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using NumericalMethods.Interfaces;
using NumericalMethods.Iterations;

#endregion

namespace NumericalMethods.DhbFunctionEvaluation
{
    /// Mathematical polynomial:
    /// c[0] + c[1] * x + c[2] * x^2 + ....
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class PolynomialFunction : IOneVariableFunction
    {
        /// Polynomial coefficients.
        private double[] _coefficients;

        /// Constructor method.
        /// @param coeffs polynomial coefficients.
        public PolynomialFunction(params double[] coeffs)
        {
            _coefficients = coeffs;
        }

        /// @param r double	number added to the polynomial.
        /// @return DhbFunctionEvaluation.PolynomialFunction
        public static PolynomialFunction operator +(PolynomialFunction poly, double r)
        {
            double[] coef = new double[poly._coefficients.Length];
            poly._coefficients.CopyTo(coef, 0);
            coef[0] += r;
            return new PolynomialFunction(coef);
        }

        /// <summary>
        /// Adds to polynomials
        /// </summary>
        /// <param name="poly1">First polynomial to add</param>
        /// <param name="poly2">Second polynomial to add</param>
        /// <returns>A new polynomial which values poly1 + poly1</returns>
        public static PolynomialFunction operator +(PolynomialFunction poly1, PolynomialFunction poly2)
        {
            int n = Math.Max(poly1.Degree, poly2.Degree) + 1;
            double[] coef = new double[n];
            for (int i = 0; i < n; i++)
                coef[i] = poly1[i] + poly2[i];
            return new PolynomialFunction(coef);
        }

        /// Returns the coefficient value at the desired position
        /// @param n int	 the position of the coefficient to be returned
        /// @return double the coefficient value
        /// @version 1.2
        public double this[int n]
        {
            get { return n < _coefficients.Length ? _coefficients[n] : 0; }
        }

        /// @param r double	 a root of the polynomial (no check made).
        /// @return PolynomialFunction the receiver divided by polynomial (x - r).
        public PolynomialFunction Deflate(double r)
        {
            int n = this.Degree;
            double remainder = _coefficients[n];
            double[] coef = new double[n];
            for (int k = n - 1; k >= 0; k--)
            {
                coef[k] = remainder;
                remainder = remainder * r + _coefficients[k];
            }
            return new PolynomialFunction(coef);
        }

        /// Returns degree of this polynomial function
        /// @return int degree of this polynomial function
        public int Degree
        {
            get { return _coefficients.Length - 1; }
        }

        /// Returns the derivative of the receiver.
        /// @return PolynomialFunction derivative of the receiver.
        public PolynomialFunction Derivative()
        {
            int n = this.Degree;
            if (n == 0)
            {
                return new PolynomialFunction(new double[] { 0 });
            }
            double[] coef = new double[n];
            for (int i = 1; i <= n; i++)
                coef[i - 1] = _coefficients[i] * i;
            return new PolynomialFunction(coef);
        }

        /// @param r double
        /// @return DhbFunctionEvaluation.PolynomialFunction
        public static PolynomialFunction operator /(PolynomialFunction poly, double r)
        {
            return poly * (1 / r);
        }

        /// @param r double
        /// @return DhbFunctionEvaluation.PolynomialFunction
        public static PolynomialFunction operator /(PolynomialFunction dividend, PolynomialFunction divisor)
        {
            return dividend.DivideWithRemainder(divisor)[0];
        }

        /// @param r double
        /// @return DhbFunctionEvaluation.PolynomialFunction
        public PolynomialFunction[] DivideWithRemainder(PolynomialFunction p)
        {
            PolynomialFunction[] answer = new PolynomialFunction[2];
            int m = this.Degree;
            int n = p.Degree;
            if (m < n)
            {
                answer[0] = new PolynomialFunction(new double[] { 0 });
                answer[1] = p;
                return answer;
            }
            double[] quotient = new double[m - n + 1];
            double[] coef = new double[m + 1];
            for (int k = 0; k <= m; k++)
                coef[k] = _coefficients[k];
            double norm = 1 / p[n];
            for (int k = m - n; k >= 0; k--)
            {
                quotient[k] = coef[n + k] * norm;
                for (int j = n + k - 1; j >= k; j--)
                    coef[j] -= quotient[k] * p[j - k];
            }
            double[] remainder = new double[n];
            for (int k = 0; k < n; k++)
                remainder[k] = coef[k];
            answer[0] = new PolynomialFunction(quotient);
            answer[1] = new PolynomialFunction(remainder);
            return answer;
        }

        /// Returns the integral of the receiver having the value 0 for X = 0.
        /// @return PolynomialFunction integral of the receiver.
        public PolynomialFunction Integral()
        {
            return Integral(0);
        }

        /// Returns the integral of the receiver having the specified value for X = 0.
        /// @param value double	value of the integral at x=0
        /// @return PolynomialFunction integral of the receiver.
        public PolynomialFunction Integral(double value)
        {
            int n = _coefficients.Length + 1;
            double[] coef = new double[n];
            coef[0] = value;
            for (int i = 1; i < n; i++)
                coef[i] = _coefficients[i - 1] / i;
            return new PolynomialFunction(coef);
        }

        /// @param r double
        /// @return DhbFunctionEvaluation.PolynomialFunction
        public static PolynomialFunction operator *(PolynomialFunction poly, double r)
        {
            int n = poly._coefficients.Length;
            double[] coef = new double[n];
            for (int i = 0; i < n; i++)
                coef[i] = poly._coefficients[i] * r;
            return new PolynomialFunction(coef);
        }

        /// @param p DhbFunctionEvaluation.PolynomialFunction
        /// @return DhbFunctionEvaluation.PolynomialFunction
        public static PolynomialFunction operator *(PolynomialFunction poly1, PolynomialFunction poly2)
        {
            int n = poly1.Degree + poly2.Degree;
            double[] coef = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                for (int k = 0; k <= i; k++)
                    coef[i] += poly2[k] * poly1[i - k];
            }
            return new PolynomialFunction(coef);
        }

        /// @return double[]
        public double[] Roots()
        {
            return Roots(DhbMath.DefaultNumericalPrecision);
        }

        /// @param desiredPrecision double
        /// @return double[]
        public double[] Roots(double desiredPrecision)
        {
            PolynomialFunction dp = this.Derivative();
            double start = 0;
            Random random = new Random();
            while (Math.Abs(dp.Value(start)) < desiredPrecision)
                start = random.NextDouble();
            PolynomialFunction p = this;
            NewtonZeroFinder rootFinder = new NewtonZeroFinder(this, dp, start);
            rootFinder.DesiredPrecision = desiredPrecision;
            List<double> rootCollection = new List<double>(Degree);
            while (true)
            {
                rootFinder.Evaluate();
                if (!rootFinder.HasConverged)
                    break;
                double r = rootFinder.Result;
                rootCollection.Add(r);
                p = p.Deflate(r);
                if (p.Degree == 0)
                    break;
                rootFinder.Function = p;
                try { rootFinder.Derivative = p.Derivative(); }
                catch (ArgumentException) { } ;
            }
            return rootCollection.ToArray();
        }

        /// @param p DhbFunctionEvaluation.PolynomialFunction
        /// @return DhbFunctionEvaluation.PolynomialFunction
        public static PolynomialFunction operator -(PolynomialFunction poly, double r)
        {
            return poly + -r;
        }

        /// @return DhbFunctionEvaluation.PolynomialFunction
        /// @param p DhbFunctionEvaluation.PolynomialFunction
        public static PolynomialFunction operator -(PolynomialFunction minuend, PolynomialFunction substraend)
        {
            int n = Math.Max(minuend.Degree, substraend.Degree) + 1;
            double[] coef = new double[n];
            for (int i = 0; i < n; i++)
                coef[i] = minuend[i] - substraend[i];
            return new PolynomialFunction(coef);
        }

        /// Returns a string representing the receiver
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            bool firstNonZeroCoefficientPrinted = false;
            for (int n = 0; n < _coefficients.Length; n++)
            {
                if (_coefficients[n] != 0)
                {
                    if (firstNonZeroCoefficientPrinted)
                        sb.Append(_coefficients[n] > 0 ? " + " : " ");
                    else
                        firstNonZeroCoefficientPrinted = true;
                    if (n == 0 || _coefficients[n] != 1)
                        sb.Append(_coefficients[n]);
                    if (n > 0)
                        sb.Append(" X^" + n);
                }
            }
            return sb.ToString();
        }

        /// Returns the value of the polynomial for the specified variable value.
        /// @param x double	value at which the polynomial is evaluated
        /// @return double polynomial value.
        public double Value(double x)
        {
            int n = _coefficients.Length;
            double answer = _coefficients[--n];
            while (n > 0)
                answer = answer * x + _coefficients[--n];
            return answer;
        }

        /// Returns the value and the derivative of the polynomial 
        /// for the specified variable value in an array of two elements
        /// @version 1.2
        /// @param x double	value at which the polynomial is evaluated
        /// @return double[0]   the value of the polynomial
        /// @return double[1]   the derivative of the polynomial
        public double[] ValueAndDerivative(double x)
        {
            int n = _coefficients.Length;
            double[] answer = new double[2];
            answer[0] = _coefficients[--n];
            answer[1] = 0;
            while (n > 0)
            {
                answer[1] = answer[1] * x + answer[0];
                answer[0] = answer[0] * x + _coefficients[--n];
            }
            return answer;
        }
    }
}
