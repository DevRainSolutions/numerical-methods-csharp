using System;

namespace NumericalMethods.Statistics
{
    public class DescriptiveStatisticsADev
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result = 0;
        /*************************************************************************
        ADev

        Input parameters:
            X   -   sample (array indexes: [0..N-1])
            N   -   sample size
        
        Output parameters:
            ADev-   ADev
        *************************************************************************/
        public DescriptiveStatisticsADev( double[] x,int n)
        {
            int i = 0;
            double mean = 0;
            double adev = 0;
            if (n <= 0)
            {
                return;
            }
            //
            // Mean
            //
            for (i = 0; i <= n - 1; i++)
            {
                mean = mean + x[i];
            }
            mean = mean / n;
            //
            // ADev
            //
            for (i = 0; i <= n - 1; i++)
            {
                adev = adev + Math.Abs(x[i] - mean);
            }
            adev = adev / n;
            result = adev;
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
