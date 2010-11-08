namespace System.NumericalMethods
{
    using System;

    /// <summary>
    /// <see>http://en.wikipedia.org/wiki/Bellman-Ford_algorithm</see>
    /// </summary>
    public class BellmanFord
    {
        public double PathLength { get; protected set; }

        public BellmanFord(double[,] matrix, int start, int end, int size)
        {
            double[] shortestWay = new double[size];
            int i;

            for (i = 0; i < size; i++)
            {
                shortestWay[i] = int.MaxValue;
            }
            for (int v = 0; v < size; v++)
            {
                shortestWay[v] = matrix[start, v];
                shortestWay[start] = 0;

                for (i = 1; i < size - 2; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (j == start)
                        {
                            continue;
                        }
                        for (int a = 0; a < size; a++)
                        {
                            shortestWay[j] = Math.Min(shortestWay[j], (shortestWay[a] + matrix[a, j]));
                        }
                    }
                }

            }
            
            this.PathLength = shortestWay[end];
        }
    }
}
