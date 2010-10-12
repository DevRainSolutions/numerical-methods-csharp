using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.NonLinearEquations
{
    public class NewtonMethod 
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[,] result = new double[2, 10];

        // <summary>
        /// Description constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>

        public NewtonMethod(FunctionOne Fr, double x, double d)
        {
            int t = 0;
            double x1, y;
            do
            {
                t++;
                x1 = x - Fr(x) / Fr1(x, d,Fr);
                x = x1;
                y = Fr(x);
            }
            while (Math.Abs(y) >= d);
            result[0,0] = x;
            result[1, 0] = t;
        }
        /// <summary>
        /// Description Fr1
        /// </summary>
        /// <param name="x">Initial value</param>
        /// <param name="d">Amount divisions of segment</param>
        /// <param name="Fr">Function to be solved delegate</param>
        public double Fr1(double x, double d, FunctionOne Fr)
        {
            return (Fr(x + d) - Fr(x)) / d;
        }

        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public double[,] GetSolution()
        {
            return result;
        }
    }
}
