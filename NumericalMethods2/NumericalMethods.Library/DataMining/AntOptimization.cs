namespace System.NumericalMethods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    //http://www.codeproject.com/KB/recipes/Ant_Colony_Optimisation.aspx

    public class AntOptimization
    {
        public const int INFINITE = int.MaxValue;
        public const int M = 1000;

        public const double TO = 0.2;

        public const double ALPHA = 0.8;
        public const double BETA = 1;
        public const double ROW = 0.2;

        public double PathLength { get; protected set; }

        static void IntiNode(ref double[,] edge, int n, ref int[] start, ref double[,] g)
        {
            int i, j;

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (i > j)
                    {
                        edge[i, j] = edge[j, i];
                    }
                }
            }


            for (i = 0; i < M; i++)
            {
                start[i] = i % n;
            }


            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        g[i, j] = (double)1 / edge[i, j];
                    }
                }
            }
        }

        static void IntiPh(int n, ref double[,] ph)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        ph[i, j] = TO;
                    }
                }
            }
        }

        static void IntiAnt(int n, ref int[,] Just, ref double[] Len, ref int[] start, ref int[] Locate)
        {
            int i, j;
            for (i = 0; i < M; i++)
                for (j = 0; j < n; j++)
                    Just[i, j] = 1;

            for (i = 0; i < M; i++)
                Len[i] = 0;

            for (int k = 0; k < M; k++)
            {
                Just[k, start[k]] = 0;
                Locate[k] = start[k];
            }
        }

        static int getsk(int k, ref double[,] ph, ref double[,] g, ref int[,] Just, ref int[] Locate, int n)
        {
            double temp = 0.0;
            int sk = 0;
            int lk = Locate[k];
            for (int i = 0; i < n; i++)
            {

                if (Just[k, i] == 1 && lk != i && temp < Math.Pow(ph[lk, i], ALPHA) * Math.Pow(g[lk, i], BETA))
                {
                    temp = Math.Pow(ph[lk, i], ALPHA) * Math.Pow(g[lk, i], BETA);
                    sk = i;
                }
            }
            return sk;
        }


        static int getSK(int k, ref int[] Locate, int n, ref int[,] Just, ref double[,] ph, ref double[,] g)
        {
            double sum = 0;
            int lk = Locate[k];
            int count = 0;
            Random rand = new Random();
            double r = ((double)rand.Next(0, INFINITE)) / ((double)INFINITE);
            int i;
            for (i = 0; i < n; i++)
            {

                if (Just[k, i] == 1 && lk != i)
                {
                    sum = sum + Math.Pow(ph[lk, i], ALPHA) * Math.Pow(g[lk, i], BETA);
                    count++;
                }
            }
            double x = 0;
            double y = 0;
            for (i = 0; i < n; i++)
            {

                if (Just[k, i] == 1 && lk != i)
                {
                    y = y + Math.Pow(ph[lk, i], ALPHA) * Math.Pow(g[lk, i], BETA);
                    if (r >= x / sum && r < y / sum)
                        break;
                    else
                        x = y;
                }
            }
            return i;
        }

        public AntOptimization(double[,] temp, int size)
        {
            int i, j;
            int n = size;
            double[,] edge = new double[n, n];
            edge = temp;

            double[,] ph = new double[n, n];
            int[,] Just = new int[M, n];

            int[] start = new int[M];
            int[] Locate = new int[M];

            int[,] Tour = new int[M, n];
            double[,] g = new double[n, n];
            double[] Len = new double[M];
            Random rand1 = new Random();
            double q;
            double q0 = 0.8;
            int[] sk = new int[M];

            int b = 0;

            double Lbest = 100000000;
            double Lbest0 = 100000000;

            IntiNode(ref edge, n, ref start, ref g);
            IntiPh(n, ref ph);

        found:

            IntiAnt(n, ref Just, ref Len, ref start, ref Locate);
            for (i = 0; i < n; i++)
            {
                if (i < n - 1)
                {
                    for (int k = 0; k < M; k++)
                    {
                        q = ((double)rand1.Next(0, INFINITE)) / ((double)INFINITE);

                        if (q <= q0)
                        {
                            sk[k] = getsk(k, ref ph, ref g, ref Just, ref Locate, n);
                        }
                        else
                        {
                            sk[k] = getSK(k, ref Locate, n, ref Just, ref ph, ref g);
                        }
                        Just[k, sk[k]] = 0;
                        Tour[k, i] = sk[k];
                        Len[k] = Len[k] + edge[Locate[k], sk[k]];
                    }
                }
                else
                {
                    for (int k = 0; k < M; k++)
                    {
                        sk[k] = start[k];
                        Tour[k, i] = sk[k];
                        Len[k] = Len[k] + edge[Locate[k], sk[k]];
                    }
                }

                for (int k = 0; k < M; k++)
                {
                    int x, y;
                    x = Locate[k];
                    y = sk[k];
                    ph[x, y] = (1 - ROW) * ph[x, y] + ROW * TO;
                    Locate[k] = sk[k];
                }
            }

            for (int k = 0; k < M; k++)
            {
                if (Lbest >= Len[k])
                {
                    Lbest = Len[k];
                    b = k;
                }
            }
            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                {
                    if (edge[i, j] != INFINITE)
                    {
                        ph[i, j] = (1 - ROW) * ph[i, j] + ROW / (double)Lbest;
                    }
                }

            if (Lbest0 > Lbest)
            {
                Lbest0 = Lbest;
                goto found;
            }

            //for (i = 0; i < size; i++)
            //{
            //    for (j = 0; j < size; j++)
            //        Console.Write("{0:N}  ", temp[i, j]);

            //    Console.WriteLine();
            //}



            if (Lbest < INFINITE)
            {
                Console.Write("\n\nНайкращий шлях ==>>  ");
                for (i = 0; i < n; i++)
                    Console.Write("<{0}>", Tour[b, i] + 1);
                Console.Write("\n\nДовжина шляху ==>>  ");
                Console.Write("{0:N}", Lbest);

                this.PathLength = Lbest;
            }
            else
            {
                Console.Write("no solution\n");
            }
        }

    }
}
