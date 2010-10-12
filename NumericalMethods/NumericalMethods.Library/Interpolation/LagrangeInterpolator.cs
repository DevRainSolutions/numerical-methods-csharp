using System;

namespace NumericalMethods.Interpolation
{
    public class LagrangeInterpolator 
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
   Полиномиальная интерполяция.

   Входные параметры:
       X   -   абсциссы точек.
               массив с нумерацией элементов [0..N-1]
       F   -   значения функций в этих точках.
               массив с нумерацией элементов [0..N-1]
       N   -   число точек
       T   -   точка, в которой осуществляется интерполяция

   Выходные параметры:
       P   -   значение интерполяционного полинома в точке T
       DP  -   первая производная интерполяционного полинома в точке T
       D2P -   вторая производная интерполяционного полинома в точке T

   *************************************************************************/

        public LagrangeInterpolator( double[] x, double[] f,int n, double t)
        {

            f = (double[])f.Clone();

            NevilleInterpolator neville = new NevilleInterpolator(x, f, n, t);
            result = neville.GetSolution();
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
