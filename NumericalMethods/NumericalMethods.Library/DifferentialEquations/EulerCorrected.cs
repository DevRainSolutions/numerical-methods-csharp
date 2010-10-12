namespace NumericalMethods.DifferentialEquations
{
    using System;

    public class EulerCorrected : DifferentialEquationsBase
    {
        

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        /// <param name="begin">Interval start point value</param>
        /// <param name="end">Interval end point value</param>
        /// <param name="y0">Starting condition</param>
        /// <param name="pointsNum">Points number</param>
        public EulerCorrected(Function function, double begin, double end, double y0, int pointsNum)
            : base(function, begin, end, y0, pointsNum)
        {
        }

        protected override void Calculate()
        {
            double y = 0;
            double y1;
            double f1;
            double x = 0;
            double[,] result = new double[pointsNum + 1, 2];
            y1 = y0;
            for (int i = 0; i <= pointsNum; i++)
            {
                f1 = function(x, y);
                y = y1 + (h / 2) * (function(x, y) + function(x + h, y + h * f1));
                result[i, 0] = x;
                result[i, 1] = y1;
                y1 = y;
                x = x + h;
            }
            this.Result = result;
        }
    }
}
