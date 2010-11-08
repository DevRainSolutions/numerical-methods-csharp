namespace System.Extensions
{
    using System;

    public class MathHelper
    {
        public static double Gaussian(double x, double mean, double variance)
        {
            return 1.0 / Math.Sqrt(2.0 * Math.PI * variance) * Math.Exp(-(x - mean) * (x - mean) / (2.0 * variance));
        }

    }
}
