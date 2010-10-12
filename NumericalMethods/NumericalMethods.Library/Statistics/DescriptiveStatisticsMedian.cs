using System;

namespace NumericalMethods.Statistics
{
    public class DescriptiveStatisticsADevMedian
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result = 0;

        /*************************************************************************
         Median calculation.

         Input parameters:
             X   -   sample (array indexes: [0..N-1])
             N   -   sample size

         Output parameters:
             Median
         *************************************************************************/
        public DescriptiveStatisticsADevMedian(double[] x,int n)
        {
            int i = 0;
            int ir = 0;
            int j = 0;
            int l = 0;
            int midp = 0;
            int k = 0;
            double a = 0;
            double tval = 0;
            double median = 0;
            x = (double[])x.Clone();
            //
            // Some degenerate cases
            //
            if (n <= 0)
            {
                return;
            }
            if (n == 1)
            {
                median = x[0];
                return;
            }
            if (n == 2)
            {
                median = 0.5 * (x[0] + x[1]);
                result = median;
            }
            //
            // Common case, N>=3.
            // Choose X[(N-1)/2]
            //
            l = 0;
            ir = n - 1;
            k = (n - 1) / 2;
            while (true)
            {
                if (ir <= l + 1)
                {
                    //
                    // 1 or 2 elements in partition
                    //
                    if (ir == l + 1 & x[ir] < x[l])
                    {
                        tval = x[l];
                        x[l] = x[ir];
                        x[ir] = tval;
                    }
                    break;
                }
                else
                {
                    midp = (l + ir) / 2;
                    tval = x[midp];
                    x[midp] = x[l + 1];
                    x[l + 1] = tval;
                    if (x[l] > x[ir])
                    {
                        tval = x[l];
                        x[l] = x[ir];
                        x[ir] = tval;
                    }
                    if (x[l + 1] > x[ir])
                    {
                        tval = x[l + 1];
                        x[l + 1] = x[ir];
                        x[ir] = tval;
                    }
                    if (x[l] > x[l + 1])
                    {
                        tval = x[l];
                        x[l] = x[l + 1];
                        x[l + 1] = tval;
                    }
                    i = l + 1;
                    j = ir;
                    a = x[l + 1];
                    while (true)
                    {
                        do
                        {
                            i = i + 1;
                        }
                        while (x[i] < a);
                        do
                        {
                            j = j - 1;
                        }
                        while (x[j] > a);
                        if (j < i)
                        {
                            break;
                        }
                        tval = x[i];
                        x[i] = x[j];
                        x[j] = tval;
                    }
                    x[l + 1] = x[j];
                    x[j] = a;
                    if (j >= k)
                    {
                        ir = j - 1;
                    }
                    if (j <= k)
                    {
                        l = i;
                    }
                }
            }
            //
            // If N is odd, return result
            //
            if (n % 2 == 1)
            {
                median = x[k];
                result = median;
            }
            a = x[n - 1];
            for (i = k + 1; i <= n - 1; i++)
            {
                if (x[i] < a)
                {
                    a = x[i];
                }
            }
            median = 0.5 * (x[k] + a);
            result = median;
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

