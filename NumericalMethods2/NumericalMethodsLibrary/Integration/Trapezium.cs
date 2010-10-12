namespace NumericalMethods.Integration
{
    using System;

    public class Trapezium
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[,] result;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>

        public Trapezium(FunctionOne f, double a, double b, int pointsNum)
        {
            double h;
            double j;
            double rez;
            int she = 4;
            //int[] she = { 4, 5, 10, 20, 30, };
            result = new double[2, pointsNum+1];
            for (int i = 0; i <= pointsNum; i++)
            {
                h = (double)(b - a) / (double)she;
                she += 4;
                rez = f(a) + f(b);
                for (j = a + h; j < b; j = j + h)
                {
                    rez = rez + 2 * f(j);
                }
                rez = rez * h / 2;
                result[0,i] = rez;
                result[1,i] = h;
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
