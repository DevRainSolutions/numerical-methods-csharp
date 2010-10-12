using System;


namespace NumericalMethods.Statistics
{
    public class DescriptiveStatisticsPercentile
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result = 0;

        /*************************************************************************
         Percentile calculation.

         Input parameters:
             X   -   sample (array indexes: [0..N-1])
             N   -   sample size, N>1
             P   -   percentile (0<=P<=1)

         Output parameters:
             V   -   percentile

         *************************************************************************/
        public DescriptiveStatisticsPercentile(double[] x,int n,double p)
        {
            int i1 = 0;
            double t = 0;
            double v = 0;
            x = (double[])x.Clone();
            InternalStatSort(ref x, n);
            if (p == 0)
            {
                v = x[0];
                result = v;
            }
            if (p == 1)
            {
                v = x[n - 1];
                result = v;
            }
            t = p * (n - 1);
            i1 = (int)Math.Floor(t);
            t = t - (int)Math.Floor(t);
            v = x[i1] * (1 - t) + x[i1 + 1] * t;
            result = v;
        }


        private  void InternalStatSort(ref double[] arr,int n)
        {
            int i = 0;
            int k = 0;
            int t = 0;
            double tmp = 0;

            if (n == 1)
            {
                return;
            }
            i = 2;
            do
            {
                t = i;
                while (t != 1)
                {
                    k = t / 2;
                    if (arr[k - 1] >= arr[t - 1])
                    {
                        t = 1;
                    }
                    else
                    {
                        tmp = arr[k - 1];
                        arr[k - 1] = arr[t - 1];
                        arr[t - 1] = tmp;
                        t = k;
                    }
                }
                i = i + 1;
            }
            while (i <= n);
            i = n - 1;
            do
            {
                tmp = arr[i];
                arr[i] = arr[0];
                arr[0] = tmp;
                t = 1;
                while (t != 0)
                {
                    k = 2 * t;
                    if (k > i)
                    {
                        t = 0;
                    }
                    else
                    {
                        if (k < i)
                        {
                            if (arr[k] > arr[k - 1])
                            {
                                k = k + 1;
                            }
                        }
                        if (arr[t - 1] >= arr[k - 1])
                        {
                            t = 0;
                        }
                        else
                        {
                            tmp = arr[k - 1];
                            arr[k - 1] = arr[t - 1];
                            arr[t - 1] = tmp;
                            t = k;
                        }
                    }
                }
                i = i - 1;
            }
            while (i >= 1);
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
