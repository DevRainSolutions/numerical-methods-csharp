using System;
namespace NumericalMethods.Statistics
{
    public class RandomGeneratorsMethod4 : RandomGeneratorsMethod3
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result = 0;
          /*************************************************************************
        Генератор нормально распределенных случайных чисел.

        Генерирует одно случайное  число, имеющее  стандартное  распределение.  По
        затратам времени равен подпрограмме RndNormal2, генерирующей два случайных
        числа.

        *************************************************************************/
        public RandomGeneratorsMethod4()
        {
            RandomGeneratorsMethod3 rand3 = new RandomGeneratorsMethod3();
            result = rand3.GetSolution1();
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