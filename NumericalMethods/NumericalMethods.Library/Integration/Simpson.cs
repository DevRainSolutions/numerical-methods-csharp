namespace NumericalMethods.Integration
{
    using System;

    public class Simpson 
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[,] result;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>

        public Simpson(FunctionOne f, double a, double b, int pointsNum)
        {
            double fi, I=0;
            double h;
            int j=0;
            h = 0.015f;
            result = new double[2, pointsNum+1];
            for (int i = 0; i < pointsNum; i++)
            {
                fi = a + h;
                do
                {
                    if (j % 2 == 0)
                    {
                        I = I + 2 * f(fi);
                    }
                    else
                    {
                        I = I + 4 * f(fi);
                    }
                    fi = fi + h;
                    j = j + 1;
                } while (fi < b);
                I = I + f(a) + f(b);
                I = I * (h / 3);
                result[0, i] = I;
                result[1, i] = h;
                I = 0;
                h += 0.001f;
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
