namespace NumericalMethods.DifferentialEquations
{
    using System;
    public class EulerSimple :DifferentialEquationsBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        /// <param name="begin">Interval start point value</param>
        /// <param name="end">Interval end point value</param>
        /// <param name="y0">Starting condition</param>
        /// <param name="pointsNum">Points number</param>

        public EulerSimple(Function function, double begin, double end, double y0, int pointsNum)
            : base(function, begin, end, y0, pointsNum)
        {
            
        }

        protected override void Calculate()
        {
            double y;
            double y1;
            double x = 0;
            double h;
            double[,] result = new double[pointsNum, 2];
            h = (end - begin) / pointsNum;
            y1 = y0;
            y = 0;
            for (int i = 0; i < pointsNum; i++)
            {
                y = y1 + h * function(x, y);
                y1 = y;
                x = x + h;
                result[i,0] = x;
                result[i,1] = y;
            }
            this.Result = result;
        }
    }
}
