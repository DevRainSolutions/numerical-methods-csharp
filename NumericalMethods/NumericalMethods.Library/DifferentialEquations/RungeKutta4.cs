namespace NumericalMethods.DifferentialEquations
{
    using System;

    /// <summary>
    /// Fourth order Runge-Kutta method for solving first order ODE y'=f(x,y)
    /// in the interval [a, b] with a given initial condition x[0] = y0 and fixed step h = (begin - start) / pointNum.
    /// </summary>
    public class RungeKutta4 : DifferentialEquationsBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        /// <param name="begin">Interval start point value</param>
        /// <param name="end">Interval end point value</param>
        /// <param name="y0">Starting condition</param>
        /// <param name="pointsNum">Points number</param>
        public RungeKutta4(Function function, double begin, double end, double y0, int pointsNum)
            : base(function, begin, end, y0, pointsNum)
        {
            double[,] result = new double[pointsNum+1,2];
            double k1;
            double k2;
            double k3;
            double h = (end - begin) / pointsNum;
            double y1;
            double x=0;
            double y=0;
            y1 = y0;
            result[0, 0] = x;
            result[0, 1] = y1;
            for (int i = 1; i <= pointsNum; i++)
            {
                k1 = h * function(x, y);
                x = x + h / 2;
                y = y1 + k1 / 2;
                k2 = function(x, y) * h;
                y = y1 + k2 / 2;
                k3 = function(x, y) * h;
                x = x + h / 2;
                y = y1 + (k1 + 2 * k2 + 2 * k3 + function(x, y) * h) / 6;
                y1 = y;
                result[i,0] = x;
                result[i,1] = y1; 
            }
            this.Result = result;
            /*
                result = new double[2, pointsNum];
                result[0, 0] = begin;
	            result[1, 0] = y0;
                double h = (begin - end) / pointsNum;
                double x = begin;
                double y = y0;
	            for (int i = 1; i < pointsNum; i++)
	            {
                    double k1 = function(x, y);
                    double k2 = function(x + h / 2, y + h * k1 / 2);
                    double k3 = function(x + h / 2, y + h * k2 / 2);
                    double k4 = function(x + h, y + h * k3);
	                y += h * ((k1 + 2 * k2 + 2 * k3 + k4) / 6);
	                x += h;
	                result[0, i] = x;
                    result[1, i] = y;
                }*/
        }
       
    }
}
