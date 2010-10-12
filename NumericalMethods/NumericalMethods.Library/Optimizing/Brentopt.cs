using System;

namespace NumericalMethods.Optimizing
{
    public class Brentopt
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double resultMin;
        double result;


        /*************************************************************************
        Минимизация функции методом Брента

        Входные параметры:
            a           -   левая граница отрезка, на котором ищется минимум
            b           -   правая граница отрезка, на котором ищется минимум
            Epsilon     -   абсолютная точность, с которой находится расположение
                            минимума

        Выходные параметры:
            XMin        -   точка найденного минимума
        
        Результат:
            значение функции в найденном минимуме
        *************************************************************************/
        public Brentopt(FunctionOne f, double a, double b, double epsilon)
        {
            double ia = 0;
            double ib = 0;
            double bx = 0;
            double d = 0;
            double e = 0;
            double etemp = 0;
            double fu = 0;
            double fv = 0;
            double fw = 0;
            double fx = 0;
            int iter = 0;
            double p = 0;
            double q = 0;
            double r = 0;
            double u = 0;
            double v = 0;
            double w = 0;
            double x = 0;
            double xm = 0;
            double cgold = 0;

            cgold = 0.3819660;
            bx = 0.5 * (a + b);
            if (a < b)
            {
                ia = a;
            }
            else
            {
                ia = b;
            }
            if (a > b)
            {
                ib = a;
            }
            else
            {
                ib = b;
            }
            v = bx;
            w = v;
            x = v;
            e = 0.0;
            fx = f(x);
            fv = fx;
            fw = fx;
            for (iter = 1; iter <= 100; iter++)
            {
                xm = 0.5 * (ia + ib);
                if (Math.Abs(x - xm) <= epsilon * 2 - 0.5 * (ib - ia))
                {
                    break;
                }
                if (Math.Abs(e) > epsilon)
                {
                    r = (x - w) * (fx - fv);
                    q = (x - v) * (fx - fw);
                    p = (x - v) * q - (x - w) * r;
                    q = 2 * (q - r);
                    if (q > 0)
                    {
                        p = -p;
                    }
                    q = Math.Abs(q);
                    etemp = e;
                    e = d;
                    if (!(Math.Abs(p) >= Math.Abs(0.5 * q * etemp) | p <= q * (ia - x) | p >= q * (ib - x)))
                    {
                        d = p / q;
                        u = x + d;
                        if (u - ia < epsilon * 2 | ib - u < epsilon * 2)
                        {
                            d = mysign(epsilon, xm - x);
                        }
                    }
                    else
                    {
                        if (x >= xm)
                        {
                            e = ia - x;
                        }
                        else
                        {
                            e = ib - x;
                        }
                        d = cgold * e;
                    }
                }
                else
                {
                    if (x >= xm)
                    {
                        e = ia - x;
                    }
                    else
                    {
                        e = ib - x;
                    }
                    d = cgold * e;
                }
                if (Math.Abs(d) >= epsilon)
                {
                    u = x + d;
                }
                else
                {
                    u = x + mysign(epsilon, d);
                }
                fu = f(u);
                if (fu <= fx)
                {
                    if (u >= x)
                    {
                        ia = x;
                    }
                    else
                    {
                        ib = x;
                    }
                    v = w;
                    fv = fw;
                    w = x;
                    fw = fx;
                    x = u;
                    fx = fu;
                }
                else
                {
                    if (u < x)
                    {
                        ia = u;
                    }
                    else
                    {
                        ib = u;
                    }
                    if (fu <= fw | w == x)
                    {
                        v = w;
                        fv = fw;
                        w = u;
                        fw = fu;
                    }
                    else
                    {
                        if (fu <= fv | v == x | v == 2)
                        {
                            v = u;
                            fv = fu;
                        }
                    }
                }
            }
            resultMin = x;
            result = fx;
        }


        private double mysign(double a,
            double b)
        {
            double result = 0;

            if (b > 0)
            {
                result = Math.Abs(a);
            }
            else
            {
                result = -Math.Abs(a);
            }
            return result;
        }

        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public double GetSolution()
        {
            return resultMin;
        }
        public double GetSolutionFunction()
        {
            return result;
        }
    }
}
