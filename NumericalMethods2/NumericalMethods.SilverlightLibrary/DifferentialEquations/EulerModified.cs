namespace NumericalMethods.DifferentialEquations
{
    using System;

    public class EulerModified 
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[,] result;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        /// <param name="begin">Interval start point value</param>
        /// <param name="end">Interval end point value</param>
        /// <param name="y0">Starting condition</param>
        /// <param name="pointsNum">Points number</param>

        public EulerModified(Function function, double begin, double end, double y0, int pointsNum)
        {
            double y=0;
            double y1;
            double f1;
            double x = 0;
            double h;
            result = new double[2, pointsNum+1];
            h = (end - begin) / pointsNum;
            y1 = y0;
            x = 0;
            result[0, 0] = x;
            result[1, 0] = y1;
            for (int i = 1; i <= pointsNum; i++)
            {
                f1 = function(x, y);
                x = x + h;
                y = y + f1 * h;
                y = y1 + h * (f1 + function(x, y)) / 2;
                y1 = y;
                result[0, i] = x;
                result[1, i] = y1;
            }
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
