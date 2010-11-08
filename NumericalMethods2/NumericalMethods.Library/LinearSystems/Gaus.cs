using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.LinearSystems
{
    public class Gaus
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[] result;

        /// <summary>
        /// Constants
        /// </summary>
        const int N = 50;

        //double[,] mas = new double[N, N];

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="n">An amount of equalizations in the system</param>
        public Gaus(int n, double[,] mas)
        {
           // mas = new double[n, n+1];
            //this.mas = mas;
            double[] x = new double[n];
            int[] otv = new int[2*n];
            result = new double[n];
            //At first all of roots one after another
            for (int i = 0; i < n + 1; i++)
                otv[i] = i;
            //Direct motion of method of Gausse
            for (int k = 0; k < n; k++)
            { //What position a staple must stand on
                glavelem(k, mas, n, otv);//Setting of staple
                for (int j = n; j >= k; j--)
                    mas[k, j] /= mas[k, k];
                for (int i = k + 1; i < n; i++)
                    for (int j = n; j >= k; j--)
                        mas[i, j] -= mas[k, j] * mas[i, k];
            }
            //Countermove
            for (int i = 0; i < n; i++)
                x[i] = mas[i, n];
            for (int i = n - 2; i >= 0; i--)
                for (int j = i + 1; j < n; j++)
                    x[i] -= x[j] * mas[i, j];
            //Conclusion of result
            int p = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (i == otv[j])
                    { //We place roots one after another
                        result[p] = x[j];
                        p++;
                        break;
                    }
        }
        /// <summary>
        /// Description glavelem
        /// </summary>

        void glavelem(int k, double[,] mas, int n, int[] otv)
        {
            int i = 0;
            int j, i_max = k, j_max = k;
            double temp;
            //We search a maximal on the module element.    
            for (i = k; i < n; i++)
                for (j = k; j < n; j++)
                    if (Math.Abs(mas[i_max, j_max]) < Math.Abs(mas[i, j]))
                    {
                        i_max = i;
                        j_max = j;
                    }
            //We move lines.        
            for (j = k; j < n + 1; j++)
            {
                temp = mas[k, j];
                mas[k, j] = mas[i_max, j];
                mas[i_max, j] = temp;
            }
            //We move columns.   
            for (i = 0; i < n; i++)
            {
                temp = mas[i, k];
                mas[i, k] = mas[i, j_max];
                mas[i, j_max] = temp;
            }
            //We take into account the change of order of roots.  
            i = otv[k];
            otv[k] = otv[j_max];
            otv[j_max] = i;
        }
        /// <summary>
        /// Input massive
        /// </summary>
        /// <param name="mas">Entrance array</param>
       /* public double SetMassiv(double[,] mas)
        {
             this.mas = mas;
             return 0;
        }*/

        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public double[] GetSolution()
        {
            return result;
        }
    }
}
