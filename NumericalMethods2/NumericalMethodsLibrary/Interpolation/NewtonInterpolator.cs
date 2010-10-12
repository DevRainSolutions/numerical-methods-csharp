using System;
using NumericalMethods.Interfaces;

namespace NumericalMethods.Interpolation
{
  
    public class NewtonInterpolator 
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        public NewtonInterpolator(double[] x, double[] f, int n, double t)
        {
            double F, LN, XX, X = 1;
            int i, j, k;
            for (i = 1, LN = f[0]; i < n; i++)
            {
                X *= (t - x[i - 1]);
                for (j = 0, F = 0; j <= i; j++)
                {
                    for (k = 0, XX = 1; k <= i; k++)
                    {
                        if (k != j)
                            XX *= x[j] - x[k];
                    }
                    F += f[j] / XX;
                }
                LN += X * F;
            }
            result = LN;
        }
        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public double GetSolution()
        {
            return result;
        }
    }
}
