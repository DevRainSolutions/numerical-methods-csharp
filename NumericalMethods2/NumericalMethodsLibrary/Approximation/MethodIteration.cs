namespace NumericalMethods.Approximation
{
    using System;

    public class IterationMethod 
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

        public IterationMethod(FunctionOne function, double Left, double Right, double x0, double epsilon)
        {
            double xk;
            do
            {
                xk = g(x0, function);
                if (Math.Abs(xk - x0) < epsilon) break;
                else x0 = xk;
            }
            while (Math.Abs(Left - x0) > epsilon && Math.Abs(Right - x0) > epsilon);
            result = xk;
        }
        double g(double x, FunctionOne function)
        {
            return 0.1 * function(x) + x;
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
