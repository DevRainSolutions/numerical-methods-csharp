using System;

namespace NumericalMethods.Optimizing
{
    public class Pijavsky
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double result;

        /*************************************************************************
        Процедура минимизации значения функции методом Пиявского (ломаных).

        Параметры:
            A,B   - отрезок [A,B], на котором ведётся поиск.
            N     - число шагов поиска, >0;
            L     - константа Липшица для функции F, >0
        
        Результат:
            Абсцисса лучшей точки из найденных.
        
        Алгоритм вычисляет значение функции N+2 раза.
        *************************************************************************/
        public Pijavsky(FunctionOne f, double a, double b, double l, int n)
        {
            double[] points = new double[0];
            double[] values = new double[0];
            double[] ratings = new double[0];
            int i = 0;
            int j = 0;
            double t = 0;
            double maxrating = 0;
            int maxratingpos = 0;
            double minpoint = 0;
            double minvalue = 0;

            points = new double[n + 1 + 1];
            values = new double[n + 1 + 1];
            ratings = new double[n + 1 + 1];
            points[0] = a;
            points[1] = b;
            values[0] = f(a);
            values[1] = f(b);
            for (i = 2; i <= n + 1; i++)
            {
                for (j = 1; j <= i - 1; j++)
                {
                    ratings[j] = l / 2 * (points[j] - points[j - 1]) - (double)(1) / (double)(2) * (values[j] + values[j - 1]);
                }
                maxrating = ratings[1];
                maxratingpos = 1;
                for (j = 2; j <= i - 1; j++)
                {
                    if (ratings[j] > maxrating)
                    {
                        maxratingpos = j;
                        maxrating = ratings[j];
                    }
                }
                points[i] = (double)(1) / (double)(2) * (points[maxratingpos] + points[maxratingpos - 1]) - (double)(1) / (double)(2) / l * (values[maxratingpos] - values[maxratingpos - 1]);
                values[i] = f(points[i]);
                for (j = i; j >= 2; j--)
                {
                    if (points[j] < points[j - 1])
                    {
                        t = points[j];
                        points[j] = points[j - 1];
                        points[j - 1] = t;
                        t = values[j];
                        values[j] = values[j - 1];
                        values[j - 1] = t;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            minpoint = points[0];
            minvalue = values[0];
            for (i = 1; i <= n + 1; i++)
            {
                if (values[i] < minvalue)
                {
                    minvalue = values[i];
                    minpoint = points[i];
                }
            }
            result = minpoint;
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