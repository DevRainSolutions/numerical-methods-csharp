using System;

namespace NumericalMethods.Interpolation
{
    public class NevilleInterpolator 
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result=0;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        /// 
        /*************************************************************************
        Полиномиальная интерполяция.

        Используется алгоритм Невилля, не  требующий  предварительного  вычисления
        таблицы коэффициентов, но имеющий трудоемкость O(N^2)

        Входные параметры:
            X   -   абсциссы точек.
                    массив с нумерацией элементов [0..N-1]
            F   -   значения функций в этих точках.
                    массив с нумерацией элементов [0..N-1]
            N   -   число точек
            T   -   точка, в которой осуществляется интерполяция

        Результат:
            значение интерполяционного полинома в точке T

        *************************************************************************/
        public NevilleInterpolator( double[] x,double[] f,int n,double t)
        {
            int m = 0;
            int i = 0;

            f = (double[])f.Clone();

            n = n - 1;
            for (m = 1; m <= n; m++)
            {
                for (i = 0; i <= n - m; i++)
                {
                    f[i] = ((t - x[i + m]) * f[i] + (x[i] - t) * f[i + 1]) / (x[i] - x[i + m]);
                }
            }
            result = f[0];
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
