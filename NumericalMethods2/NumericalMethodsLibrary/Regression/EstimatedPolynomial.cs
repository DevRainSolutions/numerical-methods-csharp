#region Using directives

using System;
using NumericalMethods.DhbFunctionEvaluation;
using NumericalMethods.MatrixAlgebra;

#endregion

namespace NumericalMethods.Approximation
{
    /// Polynomial with error estimation
    /// 
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class EstimatedPolynomial : PolynomialFunction
    {
	    /// Error matrix.
        SymmetricMatrix _errorMatrix;

        /// Constructor method.
        /// @param coeffs double[]
        /// @param e double[]	error matrix
        public EstimatedPolynomial(double[] coeffs, SymmetricMatrix e) : base(coeffs)
        {
            _errorMatrix = e;
        }

        /// @return double
        /// @param x double
        public double Error(double x)
        {
            int n = this.Degree + 1;
            double[] errors = new double[n];
            errors[0] = 1;
            for (int i = 1; i < n; i++)
                errors[i] = errors[i - 1] * x;
            DhbVector errorVector = new DhbVector(errors);
            double answer;
            try
            {
                answer = errorVector * (_errorMatrix * errorVector);
            }
            catch (DhbIllegalDimension) { answer = double.NaN; } ;
            return Math.Sqrt(answer);
        }
    }
}
