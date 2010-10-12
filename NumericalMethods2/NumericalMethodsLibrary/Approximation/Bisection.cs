using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.Approximation
{
    public class Bisection
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
        /// <param name="epsilon">Exactness conducting of calculations</param>

        public Bisection(FunctionOne f,double Left, double Right, double epsilon)
        {
            bool blnError = false;
            double c;
            do
            {
                c = (Left + Right) / 2;
                if (f(Left) * f(c) < 0)
                    Right = c;
                else
                    Left = c;
                if (f(c) == 0)
                {
                    blnError = (f(c - epsilon / 2) * f(c + epsilon / 2) < 0) ? false : true;
                    break;
                }
            }
            while ((f(c)) > epsilon || (Right - Left) > epsilon);
            if (blnError == false)
            {
                result = c;
            }
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
