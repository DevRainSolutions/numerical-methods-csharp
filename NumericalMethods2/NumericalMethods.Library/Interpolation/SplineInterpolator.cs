using System;

namespace NumericalMethods.Interpolation
{
    /// A SplineInterpolator can be used to interpolate values between
    /// a series of 2-dimensional points. The interpolation function is
    /// a cubic spline with first derivatives defined at the end points.
    /// If the first derivatives are not defined for the end points,
    /// a so-called natural spline is used with second derivatives at
    /// the end points set to zero.


    public class SplineInterpolator 
    {

        /// <summary>
        /// Equation solution
        /// </summary>
        double result =0;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        /*************************************************************************
    Вычисление интерполирующего сплайна

    Входные параметры:
        C   -   массив коэффициентов, вычисленный подпрограммой для
                построения сплайна.
        X   -   точка, в которой вычисляется значение сплайна

    Результат:
        значение сплайна в точке X

    *************************************************************************/
        public SplineInterpolator(double[] x, double[] y, int N, double t)
        {
            double[] c=new double[1000];
            BuildLinearSpline( x, y, N,ref c);
            int n = 0;
            int l = 0;
            int r = 0;
            int m = 0;
            n = (int)Math.Round(c[2]);
            //
            // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
            //
            l = 3;
            r = 3 + n - 2 + 1;
            while (l != r - 1)
            {
                m = (l + r) / 2;
                if (c[m] >= t)
                {
                    r = m;
                }
                else
                {
                    l = m;
                }
            }

            //
            // Interpolation
            //
            t = t - c[l];
            m = 3 + n + 4 * (l - 3);
            result = c[m] + t * (c[m + 1] + t * (c[m + 2] + t * c[m + 3]));
        }

        /*************************************************************************
   Построение таблицы коэффициентов кусочно-линейного сплайна

   Входные параметры:
       X           -   абсциссы, массив с нумерацией элементов [0..N-1]
       Y           -   значения функции,
                       массив с нумерацией элементов [0..N-1]
       N           -   число точек, N>=2

   Выходные параметры:
       C           -   таблица коэффициентов сплайна для использования в
                       подпрограмме SplineInterpolation
   *************************************************************************/
        public void BuildLinearSpline(double[] x,double[] y, int n,ref double[] c)
        {
            int i = 0;
            int tblsize = 0;
            x = (double[])x.Clone();
            y = (double[])y.Clone();
            //
            // Sort points
            //
            SortPoints(ref x, ref y, n);
            tblsize = 3 + n + (n - 1) * 4;
            c = new double[tblsize - 1 + 1];
            c[0] = tblsize;
            c[1] = 3;
            c[2] = n;
            for (i = 0; i <= n - 1; i++)
            {
                c[3 + i] = x[i];
            }
            for (i = 0; i <= n - 2; i++)
            {
                c[3 + n + 4 * i + 0] = y[i];
                c[3 + n + 4 * i + 1] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
                c[3 + n + 4 * i + 2] = 0;
                c[3 + n + 4 * i + 3] = 0;
            }
        }

        /*************************************************************************
        Sort  points
        *************************************************************************/
        private void SortPoints(ref double[] x,ref double[] y,int n)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int t = 0;
            double tmp = 0;
            bool isascending = new bool();
            bool isdescending = new bool();
            isascending = true;
            isdescending = true;
            for (i = 1; i <= n - 1; i++)
            {
                isascending = isascending & x[i] > x[i - 1];
                isdescending = isdescending & x[i] < x[i - 1];
            }
            if (isascending)
            {
                return;
            }
            if (isdescending)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    j = n - 1 - i;
                    if (j <= i)
                    {
                        break;
                    }
                    tmp = x[i];
                    x[i] = x[j];
                    x[j] = tmp;
                    tmp = y[i];
                    y[i] = y[j];
                    y[j] = tmp;
                }
                return;
            }
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
                    if (x[k - 1] >= x[t - 1])
                    {
                        t = 1;
                    }
                    else
                    {
                        tmp = x[k - 1];
                        x[k - 1] = x[t - 1];
                        x[t - 1] = tmp;
                        tmp = y[k - 1];
                        y[k - 1] = y[t - 1];
                        y[t - 1] = tmp;
                        t = k;
                    }
                }
                i = i + 1;
            }
            while (i <= n);
            i = n - 1;
            do
            {
                tmp = x[i];
                x[i] = x[0];
                x[0] = tmp;
                tmp = y[i];
                y[i] = y[0];
                y[0] = tmp;
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
                            if (x[k] > x[k - 1])
                            {
                                k = k + 1;
                            }
                        }
                        if (x[t - 1] >= x[k - 1])
                        {
                            t = 0;
                        }
                        else
                        {
                            tmp = x[k - 1];
                            x[k - 1] = x[t - 1];
                            x[t - 1] = tmp;
                            tmp = y[k - 1];
                            y[k - 1] = y[t - 1];
                            y[t - 1] = tmp;
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
