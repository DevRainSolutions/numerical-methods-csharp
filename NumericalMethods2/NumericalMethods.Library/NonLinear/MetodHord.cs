using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.NonLinearEquations
{
    public class HordMetod 
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[,] result = new double[2, 1];

        // <summary>
        /// Description constructor
        /// </summary>
        /// <param name="Fr">Function to be solved delegate</param>
        /// <param name="xa">Value of low bound</param>
        /// <param name="xb">Value of high bound</param>
        /// <param name="x">Intermediate value</param>
        /// <param name="iter">Amount divisions of segment</param>
      //  public HordMetod(FunctionOne Fr, double xa, double xb, ref double x, ref int iter)
        public HordMetod(FunctionOne Fr, double xa, double xb,  int iter)
        {
            double xlast;
            double x = 0;
            double e = 1f;
            if (Fr(xa) * Fr(xb) >= 0)
            {
                result[0, 0] = 0;
                result[1, 0] = 0;
                // return 0;
            }
            iter = 0;
            do
            {
                xlast = x;
                x = xb - Fr(xb) * (xb - xa) / (Fr(xb) - Fr(xa));
                if (Fr(x) * Fr(xa) > 0)
                    xa = x;
                else
                    xb = x;
                iter++;
            }
            while (Math.Abs(x - xlast) > e);
            result[0, 0] = x;
            result[1, 0] = iter;
           // return 1;
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
