using System;

namespace NumericalMethods.Interpolation
{

  
    public class BarycentricInterpolation
    {


        /// <summary>
        /// Equation solution
        /// </summary>
        double result = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        /*************************************************************************
    Интерполяция функции с использованием барицентрической формулы:

    F(t) = SUM(i=0,n-1,w[i]*f[i]/(t-x[i])) / SUM(i=0,n-1,w[i]/(t-x[i]))

    Входные параметры:
        X   -   абсциссы точек.
                массив с нумерацией элементов [0..N-1]
        F   -   значения функций в этих точках.
                массив с нумерацией элементов [0..N-1]
        W   -   весовые коэффициенты
                массив с нумерацией элементов [0..N-1]
        N   -   число точек
        T   -   точка, в которой осуществляется интерполяция
        
    Результат:
        значение барицентрического интерполянта в точке T

    *************************************************************************/
        public BarycentricInterpolation(double[] x,double[] f,double[] w,int n,double t)
        {
            double s1 = 0;
            double s2 = 0;
            double v = 0;
            double threshold = 0;
            double s = 0;
            int i = 0;
            int j = 0;

            threshold = Math.Sqrt(AP.Math.MinRealNumber);
            //
            // First, decide: should we use "safe" formula (guarded
            // against overflow) or fast one?
            //
            j = 0;
            s = t - x[0];
            for (i = 1; i <= n - 1; i++)
            {
                if (Math.Abs(t - x[i]) < Math.Abs(s))
                {
                    s = t - x[i];
                    j = i;
                }
            }
            if (s == 0)
            {
                result = f[j];
            }
            if (Math.Abs(s) > threshold)
            {

                //
                // use fast formula
                //
                j = -1;
                s = 1.0;
            }

            //
            // Calculate using safe or fast barycentric formula
            //
            s1 = 0;
            s2 = 0;
            for (i = 0; i <= n - 1; i++)
            {
                if (i != j)
                {
                    v = s * w[i] / (t - x[i]);
                    s1 = s1 + v * f[i];
                    s2 = s2 + v;
                }
                else
                {
                    v = w[i];
                    s1 = s1 + v * f[i];
                    s2 = s2 + v;
                }
            }
            result = s1 / s2;
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
