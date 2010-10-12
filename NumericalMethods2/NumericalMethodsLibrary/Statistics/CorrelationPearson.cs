using System;

namespace NumericalMethods.Statistics
{
    public class CorrelationPearson
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result = 0;
        /*************************************************************************
        Pearson product-moment correlation coefficient

        Input parameters:
            X       -   sample 1 (array indexes: [0..N-1])
            Y       -   sample 2 (array indexes: [0..N-1])
            N       -   sample size.

        Result:
            Pearson product-moment correlation coefficient

        *************************************************************************/
        public CorrelationPearson(double[] x, double[] y, int n)
        {
            int i = 0;
            double xmean = 0;
            double ymean = 0;
            double s = 0;
            double xv = 0;
            double yv = 0;
            double t1 = 0;
            double t2 = 0;

            xv = 0;
            yv = 0;
            if (n <= 1)
            {
                result = 0;
            }

            //
            // Mean
            //
            xmean = 0;
            ymean = 0;
            for (i = 0; i <= n - 1; i++)
            {
                xmean = xmean + x[i];
                ymean = ymean + y[i];
            }
            xmean = xmean / n;
            ymean = ymean / n;

            //
            // numerator and denominator
            //
            s = 0;
            xv = 0;
            yv = 0;
            for (i = 0; i <= n - 1; i++)
            {
                t1 = x[i] - xmean;
                t2 = y[i] - ymean;
                xv = xv + AP.Math.Sqr(t1);
                yv = yv + AP.Math.Sqr(t2);
                s = s + t1 * t2;
            }
            if (xv == 0 | yv == 0)
            {
                result = 0;
            }
            else
            {
                result = s / (Math.Sqrt(xv) * Math.Sqrt(yv));
            }
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
