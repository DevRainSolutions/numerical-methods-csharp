using System;
namespace NumericalMethods.Statistics
{
    public class DescriptiveStatisticsMoments
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result = 0;
        public double variance = 0;
        public double kurtosis = 0;
        public double skewness = 0;
        /*************************************************************************
        Calculation of the distribution moments: mean, variance, slewness, kurtosis.

        Input parameters:
            X       -   sample. Array with whose indexes range within [0..N-1]
            N       -   sample size.
        
        Output parameters:
            Mean    -   mean.
            Variance-   variance.
            Skewness-   skewness (if variance<>0; zero otherwise).
            Kurtosis-   kurtosis (if variance<>0; zero otherwise).


        *************************************************************************/
        public DescriptiveStatisticsMoments(double[] x,int n)
        {
            int i = 0;
            double v = 0;
            double v1 = 0;
            double v2 = 0;
            double stddev = 0;
            double mean = 0;
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
            // Variance (using corrected two-pass algorithm)
            //
            if (n != 1)
            {
                v1 = 0;
                for (i = 0; i <= n - 1; i++)
                {
                    v1 = v1 + AP.Math.Sqr(x[i] - mean);
                }
                v2 = 0;
                for (i = 0; i <= n - 1; i++)
                {
                    v2 = v2 + (x[i] - mean);
                }
                v2 = AP.Math.Sqr(v2) / n;
                variance = (v1 - v2) / (n - 1);
                if (variance < 0)
                {
                    variance = 0;
                }
                stddev = Math.Sqrt(variance);
            }
            //
            // Skewness and kurtosis
            //
            if (stddev != 0)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    v = (x[i] - mean) / stddev;
                    v2 = AP.Math.Sqr(v);
                    skewness = skewness + v2 * v;
                    kurtosis = kurtosis + AP.Math.Sqr(v2);
                }
                skewness = skewness / n;
                kurtosis = kurtosis / n - 3;
            }
            result = mean;
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
