namespace NumericalMethods.DifferentialEquations
{
    using System;

    public class EulerModified : DifferentialEquationsBase
    {
        public EulerModified(Function function, double begin, double end, double y0, int pointsNum) : base(function, begin,end, y0,pointsNum)
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
            x = 0;
            result[0, 0] = x;
            result[0, 1] = y1;
            for (int i = 1; i <= pointsNum; i++)
            {
                f1 = function(x, y);
                x = x + h;
                y = y + f1 * h;
                y = y1 + h * (f1 + function(x, y)) / 2;
                y1 = y;
                result[i, 0] = x;
                result[i, 1] = y1;
            }
            this.Result = result;
        }
    }
}
