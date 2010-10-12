using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.NonLinearEquations
{
    public class HalfDivision 
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[,] result=new double[2,1];

        // <summary>
        /// Description constructor
        /// </summary>
        /// <param name="Fr">Function to be solved delegate</param>
        /// <param name="x0">Interval start point value</param>
        /// <param name="x1">Interval end point value</param>
        /// <param name="d">Amount divisions of segment</param>

        public HalfDivision(FunctionOne Fr,double x0, double x1, double d)
        {
            int t=0;
            int j = 0;
            double x2;
            double y = Math.Abs(x0 - x1);
            while (y > d)
            {
                t++;
                x2 = (x0 + x1) / 2;
                if (Fr(x0) * Fr(x2) > 0)
                    x0 = x2;
                else
                    x1 = x2;
                y = Math.Abs(x0 - x1);
            }
            result[0 , j] = x1;
            result[1 , j] = t;
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
