namespace NumericalMethods.Approximation
{
    using System;

    public class Сhord  
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        /// <param name="df">Function to be solved delegate</param>
        /// <param name="Left">Left border of the set interval</param>
        /// <param name="Right">Right border of the set interval</param>
        /// <param name="x0">Starting condition</param>
        /// <param name="epsilon">Exactness conducting of calculations</param>

        public Сhord(FunctionOne function,FunctionOne df, double Left, double Right, double x0, double epsilon)
        {
            double xl;
            const double m = 2.41064f;
            const double M = 20.0828f;
            double xk;
            xk = x0 - (function(x0) / df(x0));
            do
            {
                xl = xk - function(xk) * (xk - x0) / (function(xk) - function(x0));
                x0 = xk;
                xk = xl;
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
