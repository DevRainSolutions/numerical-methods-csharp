using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.NonLinearEquations
{
    public class Secant 
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[,] result = new double[2, 1];

        /// <summary>
        /// Constants
        /// </summary>
       // const double shag = 0.01f;
        const double dl = 0.05f;
        //const double delta = 0.0001f;

        /// <summary>
        /// Description constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>

        public Secant(FunctionOne Fr,double shag, double delta)
        {
            for (double i = 0; i <= 10; i = i + dl)
            {
                if (Fr(i) * Fr(i + dl) < 0)
                {
                    SecantMethod(i + dl, Fr, shag,delta);
                }
            }
        }
        /// <summary>
        /// Description SecantMethod
        /// </summary>
        /// <param name="x0">Initial value</param>
        /// <param name="Fr">Function to be solved delegate</param>

        void SecantMethod(double x0, FunctionOne Fr, double shag, double delta)
        {
            int j=0;
            double x1;
            x1 = x0 - Fr(x0) / fsh(x0,Fr,shag);
            while (Math.Abs(x1 - x0) <= delta)
            {
                x1 = x0 - Fr(x0) / fsh(x0,Fr,shag);
                if (Math.Abs(x1 - x0) > delta)
                    x0 = x1;
            }
            result[0, 0] = x1;
            result[1, 0] = j;
        }
        /// <summary>
        /// Description fsh
        /// </summary>
        /// <param name="x">Initial value</param>
        /// <param name="Fr">Function to be solved delegate</param>

        double fsh(double x, FunctionOne Fr, double shag)
        {
            return (Fr(x + shag) - Fr(x)) / shag;
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
