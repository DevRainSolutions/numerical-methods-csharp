using System;
namespace NumericalMethods.Statistics
{
    public class RandomGeneratorsMethod1
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result = 0;

        public const int rndbasemax = 2147483563;
        public const double rndbasemaxr = 1.0 / rndbasemax;
        public const int rndbasem1 = 2147483563;
        public const int rndbasem2 = 2147483399;
        public static int rndbases1 = 0;
        public static int rndbases2 = 0;

        /*************************************************************************
        Генератор равномерно распределенных случайных вещественных чисел
        в диапазоне (0, 1)
        *************************************************************************/
        public RandomGeneratorsMethod1()
        {
            result = rndbasemaxr * rndintegerbase();
        }
        /*************************************************************************
        Генерация случайного целого числа  в  диапазоне  (0, RndIntegerMax())  (не
        включая  границы  интервала).  Эта  подпрограмма является основой для всех
        подпрограмм этого модуля и обладает максимальным быстродействием.
       L'Ecuyer, Efficient and portable combined random number generators
        *************************************************************************/
        public  int rndintegerbase()
        {
            int results = 0;
            int k = 0;
            //
            // Initialize S1 and S2 if needed
            //
            if (rndbases1 < 1 | rndbases1 >= rndbasem1)
            {
                rndbases1 = 1 + AP.Math.RandomInteger(32000);
            }
            if (rndbases2 < 1 | rndbases2 >= rndbasem2)
            {
                rndbases2 = 1 + AP.Math.RandomInteger(32000);
            }
            //
            // Process S1, S2
            //
            k = rndbases1 / 53668;
            rndbases1 = 40014 * (rndbases1 - k * 53668) - k * 12211;
            if (rndbases1 < 0)
            {
                rndbases1 = rndbases1 + 2147483563;
            }
            k = rndbases2 / 52774;
            rndbases2 = 40692 * (rndbases2 - k * 52774) - k * 3791;
            if (rndbases2 < 0)
            {
                rndbases2 = rndbases2 + 2147483399;
            }

            //
            // Result
            //
            results = rndbases1 - rndbases2;
            if (results < 1)
            {
                results = results + 2147483562;
                return results;
            }
            return results;
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