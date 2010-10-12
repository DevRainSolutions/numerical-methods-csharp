namespace NumericalMethods.DifferentialEquations
{
    using System;

    public class EulerCorrected
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

        public EulerCorrected(Function function, double begin, double end, double y0, int pointsNum)
        {

            double y = 0;
            double y1;
            double f1;
            double x = 0;
            double h;
            result = new double[2, pointsNum + 1];
            h = (end - begin) / pointsNum;
            y1 = y0;
            for (int i = 0; i <= pointsNum; i++)
            {
                f1 = function(x, y);
                y = y1 +(h/2)*(function(x,y)+function(x + h ,y + h * f1));
                result[0, i] = x;
                result[1, i] = y1; 
                y1 = y;
                x = x + h;
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
