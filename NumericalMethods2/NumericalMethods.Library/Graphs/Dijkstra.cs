namespace System.NumericalMethods
{
    using System;

    public class Dijkstra
    {
        /// <summary>
        /// Gets path string
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// Gets path length
        /// </summary>
        public double PathLength { get; protected set; }

        /// <summary>
        /// 
        /// <see>http://en.wikipedia.org/wiki/Dijkstra%27s_algorithm</see>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="size"></param>
        public Dijkstra(double[,] matrix, int start, int end, int size)
        {
            int p, i, j, n = (ushort)size;

            double[] flag = new double[n];
            double[] l = new double[n];
            double[,] c = new double[n, n];
            
            string s = string.Empty;
            string[] path = new string[n + 1];

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    c[i, j] = matrix[i, j];
                }
            }

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (c[i, j] == 0)
                    {
                        c[i, j] = 65535;
                    }
                }
            }

            if (start == end)
            {
                throw new ArgumentException("Initial and final poits can not be the same.");
            }

            for (i = 0; i < n; i++)
            {
                flag[i] = 0;
                l[i] = 65535;
            }

            l[start] = 0;
            flag[start] = 1;
            p = start;

            s = (start + 1).ToString();

            for (i = 1; i <= n; i++)
            {
                path[i] = "X" + s;
            }

            do
            {
                for (i = 0; i < n; i++)
                    if ((c[p, i] != 65535) && (0 == flag[i]) && (i != p))
                    {
                        if (l[i] > l[p] + c[p, i])
                        {
                            s = (i + 1).ToString();
                            path[i + 1] = String.Copy(path[p + 1]);
                            path[i + 1] += "->X";
                            path[i + 1] += s;

                        }
                        l[i] = Math.Min(l[i], (l[p] + c[p, i]));
                    }
                p = min(n, flag, l);
                flag[p] = 1;
            }
            while (p != end);


            if (l[p] != 65535)
            {
                //for (i = 0; i < size; i++)
                //{
                //    for (j = 0; j < size; j++)
                //    {
                //        Console.Write("{0:N}  ", temp[i, j]);
                //    }

                //    Console.WriteLine();
                //}

                this.Path = path[p + 1];
                this.PathLength = l[p];
                
            }
            else
            {
                throw new Exception("Such path is not exists!");
            }
        }

        public static ushort min(int n, double[] flag, double[] l)
        {
            ushort i, result = 0;

            for (i = 0; i < n; i++)
            {
                if (0 == (flag[i]))
                {
                    result = i;
                }
            }

            for (i = 0; i < n; i++)
            {
                if ((l[result] > l[i]) && (0 == flag[i]))
                {
                    result = i;
                }
            }
            return result;
        }

    }
}
