#region Using directives

using System;

using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Statistics
{
    /// Computes the integral of a multi-variable function using Monte Carlo approximation.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class RandomIntegrator
    {
	    /// Function to integrate.
        private IManyVariableFunction _f;
	    /// Low integral bound.
        private double[] _a;
	    /// Integral range.
        private double[] _range;

	    /// Constructor method. from and to must have the same dimension,
	    /// otherwise an ArrayStoreException is thrown by the system
	    /// @param func a function of one variable.
	    /// @param from low limit of integral (array is copied).
	    /// @param to high limit of integral (array is copied).
        public RandomIntegrator(IManyVariableFunction func, double[] from, double[] to)
        {
            _f = func;
            _a = from.Clone() as double[];
            int n = from.Length;
            _range = new double[n];
            for (int i = 0; i < n; i++)
            {
                _range[i] = to[i] - from[i];
            }
        }

        /// Compute the integral.
        /// @param totalCount number of Monte Carlo trials.
        /// @param upperLimit value larger than the maximum of the function over the integral range.
        /// @return integral value
        public double Result(int totalCount, double upperLimit)
        {
            int n = _a.Length;
            double[] x = new double[n];
            int hits = 0;
            Random random = new Random();
            for (int count = 0; count < totalCount; count++)
            {
                for (int i = 0; i < n; i++)
                    x[i] = _a[i] + random.NextDouble() * _range[i];
                if (random.NextDouble() * upperLimit <= _f.Value(x))
                    hits++;
            }
            double answer = upperLimit * (hits / (double)totalCount);
            for (int i = 0; i < n; i++)
                answer *= _range[i];
            return answer;
        }
    }
}
