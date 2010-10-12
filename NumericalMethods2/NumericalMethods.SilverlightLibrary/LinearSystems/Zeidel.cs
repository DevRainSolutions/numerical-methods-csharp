using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.SystemLinearEqualizations
{
    public class Zeidel
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[] result;

        /// <summary>
        /// Constants
        /// </summary>
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="n">An amount of equalizations in the system</param>
        /// <param name="A">Entrance array A</param>
        /// <param name="B">Entrance array B</param>
        public Zeidel(int n, double[,] A,double[] B)
        {
            result = new double[n];
           // this.n = n;
            //A = new double[n, n];
           // B = new double[n];
            int[] otv = new int[n];
            double[] X = new double[n];

            double eps = 0.0001f;
            for (int i = 0; i < n; i++)
            {
                X[i] = 0;
            }
            if (ZeidelMethod(A, B, X, eps, n) == 1)
            {
                for (int i = 0; i < n; i++)
                {
                    result[i] = X[i];
                }
            }
        }
        /// <summary>
        /// Description Zeidel Method
        /// </summary>
        /// <param name="a">Entrance array a</param>
        /// <param name="b">Entrance array a</param>
        /// <param name="x">Entrance array x</param>
        /// <param name="e">Precision of calculations</param>
        /// <param name="n">An amount of equalizations in the system</param>
        public int ZeidelMethod(double[,] a, double[] b, double[] x, double e, int n)
        {
            int i, j;
            double s1, s2, v, m;
            
            do
            {
                m = 0;
                for (i = 0; i < n; i++)
                {
                    s1 = 0;
                    s2 = 0;
                    for (j = 0; j < i; j++) s1 += a[i, j] * x[j];
                    for (j = i; j < n; j++) s2 += a[i, j] * x[j];
                    v = x[i];
                    x[i] -= (1 / a[i, i]) * (s1 + s2 - b[i]);
                    if ((v - x[i]) > m)
                        m = (v - x[i]);
                }
            }
            while (m >= e);
            return 1;
        }

        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public double[] GetSolution()
        {
            return result;
        }
    }
}
