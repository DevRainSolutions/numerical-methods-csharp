using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.Approximation
{
    public class Newton 
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        /// <param name="Left">Left border of the set interval</param>
        /// <param name="Right">Right border of the set interval</param>
        /// <param name="x0">Starting condition</param>
        /// <param name="epsilon">Exactness conducting of calculations</param>

        public Newton(FunctionOne function, FunctionOne df, double Left, double Right, double x0, double epsilon)
        {
            const double m = 2.41064f;
            const double M = 20.0828f;
            double xk;
            xk = x0;
            do
            {
                x0 = xk;
                xk = x0 - (function(x0) / df(x0));
            }
            while (Math.Abs(xk - x0) > Math.Sqrt(Math.Abs(2f * epsilon * m / M)));
            result = x0;
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
