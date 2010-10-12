namespace NumericalMethods.Integration
{
    using System;

    public class Chebishev
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[,] result;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>

        public Chebishev(FunctionOne f, double a, double b, int pointsNum)
        {
            double h;
            double j;
            double rez;
            int she = 4;
            //int[] she = { 4, 5, 10, 20, 30,40,50,60,70,80,90,100,110,120 };
            result = new double[2, pointsNum+1];
            for (int i = 0; i <= pointsNum; i++)
            {
                h = (b - a) / (double)she;
                she += 4;
                rez = 0;
                for (j = a; j <= b; j = j + h)
                {
                    rez = rez + ChebushevMethod(j, j + h, f);
                }
                result[0, i] = rez;
                result[1, i] = h;
            }
        }
        double ChebushevMethod(double A, double B, FunctionOne f)
        {
            double[] t = { 0, -0.832498, -0.374513f, 0, 0.374513, 0.832498 };
            int k;
            double x, reten;
            reten = 0;
            for (k = 1; k <= 5; k++)
            {
                x = (double)(A + B) / 2 + (double)(B - A) * (double)t[k] / 2;
                reten += f(x);
            }
            reten = reten * (B - A) / 5;
            return reten;
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
