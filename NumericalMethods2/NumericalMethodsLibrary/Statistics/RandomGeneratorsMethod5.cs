using System;
namespace NumericalMethods.Statistics
{
    public class RandomGeneratorsMethod5 : RandomGeneratorsMethod1
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result = 0;
        /*************************************************************************
        Генератор экспоненциально распределенных случайных чисел

        *************************************************************************/
        public RandomGeneratorsMethod5(double lambda)
        {
            System.Diagnostics.Debug.Assert(lambda > 0, "RndExponential: Lambda<=0!");
            RandomGeneratorsMethod1 rand1 = new RandomGeneratorsMethod1();
            result = -(Math.Log(rand1.GetSolution()) / lambda);
        }
        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public new double GetSolution()
        {
            return result;
        }
    }
}