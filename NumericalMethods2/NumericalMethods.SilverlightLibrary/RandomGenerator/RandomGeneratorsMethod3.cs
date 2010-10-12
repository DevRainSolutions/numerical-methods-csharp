using System;
namespace NumericalMethods.Statistics
{
    public class RandomGeneratorsMethod3 : RandomGeneratorsMethod1
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result1 = 0;
        double result2 = 0;
         /*************************************************************************
        Генератор нормально распределенных случайных чисел.

        Генерирует   два   независимых   случайных   числа,   имеющих  стандартное
        распределение.   По   затратам   времени  равен   подпрограмме  RndNormal,
        генерирующей одно случайное число.

        *************************************************************************/
        public RandomGeneratorsMethod3()
        {
            double x1=0;
            double x2=0;
            double u = 0;
            double v = 0;
            double s = 0;
            bool execute = true;
            while (execute==true)
            {
                RandomGeneratorsMethod1 rand1 = new RandomGeneratorsMethod1();
                u = 2 * rand1.GetSolution() - 1;
                RandomGeneratorsMethod1 rand11 = new RandomGeneratorsMethod1();
                v = 2 * rand11.GetSolution() - 1;
                s = AP.Math.Sqr(u) + AP.Math.Sqr(v);
                if (s > 0 & s < 1)
                {
                    s = Math.Sqrt(-(2 * Math.Log(s) / s));
                    x1 = u * s;
                    x2 = v * s;
                    execute = false;
                }
                result1 = x1;
                result2 = x2;
            }
            
        }
        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public double GetSolution1()
        {
            return result1;
        }
        public double GetSolution2()
        {
            return result2;
        }
    }
}