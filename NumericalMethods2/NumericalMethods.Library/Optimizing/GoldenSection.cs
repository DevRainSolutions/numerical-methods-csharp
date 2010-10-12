using System;

namespace NumericalMethods.Optimizing
{
    public class GoldenSection
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double resultA;
        double resultB;

        /*************************************************************************
        Процедура минимизации значения функции методом золотого сечения.

        Оптимизирует функцию одного  переменного F.

        Параметры:
            A,B      - отрезок [A,B], на котором ищется минимум функции F.
            N        - число шагов метода

        После выхода переменные A и B содержат границы   отрезка,  на  котором
        находится решение задачи.

        Алгоритм проводит 2+N вычислений функции F.
        *************************************************************************/
        public GoldenSection(FunctionOne f,double a,double b, int n)
        {
            int i = 0;
            double s1 = 0;
            double s2 = 0;
            double u1 = 0;
            double u2 = 0;
            double fu1 = 0;
            double fu2 = 0;

            s1 = (3 - Math.Sqrt(5)) / 2;
            s2 = (Math.Sqrt(5) - 1) / 2;
            u1 = a + s1 * (b - a);
            u2 = a + s2 * (b - a);
            fu1 = f(u1);
            fu2 = f(u2);
            for (i = 1; i <= n; i++)
            {
                if (fu1 <= fu2)
                {
                    b = u2;
                    u2 = u1;
                    fu2 = fu1;
                    u1 = a + s1 * (b - a);
                    fu1 = f(u1);
                }
                else
                {
                    a = u1;
                    u1 = u2;
                    fu1 = fu2;
                    u2 = a + s2 * (b - a);
                    fu2 = f(u2);
                }
            }
            resultA = a;
            resultB = b;
        }
        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public double GetSolutionA()
        {
            return resultA;
        }
        public double GetSolutionB()
        {
            return resultB;
        }
    }
}
