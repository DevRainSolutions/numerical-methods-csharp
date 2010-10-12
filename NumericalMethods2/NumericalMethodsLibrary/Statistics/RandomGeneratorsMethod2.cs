using System;
namespace NumericalMethods.Statistics
{
    public class RandomGeneratorsMethod2 : RandomGeneratorsMethod1
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        int result = 0;

        /*************************************************************************
        Генератор равномерно распределенных случайных целых чисел
        в диапазоне [0, N)
        *************************************************************************/
        public RandomGeneratorsMethod2(int n)
        {
            System.Diagnostics.Debug.Assert(n > 0, "RndUniformI: N<=0!");
            System.Diagnostics.Debug.Assert(n < rndbasemax, "RndUniformI: N>RNDBaseMax!");
            result = rndintegerbase() % n;
        }
        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public new int GetSolution()
        {
            return result;
        }
    }
}