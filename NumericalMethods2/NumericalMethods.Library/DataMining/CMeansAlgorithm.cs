using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Clustering
{
    public sealed class CMeansAlgorithmNDim
    {
        private double Accuracy = Math.Pow(10, -5);

        /// <summary>
        /// Array containing all points used by the algorithm
        /// </summary>
        private List<ClusterPoint> Points;

        /// <summary>
        /// Array containing all clusters handled by the algorithm
        /// </summary>
        private List<ClusterPoint> Clusters;

        /// <summary>
        /// Gets or sets membership matrix
        /// </summary>
        public double[,] U;

        /// <summary>
        /// Gets or sets the current fuzzyness factor
        /// </summary>
        private double Fuzzyness;

        /// <summary>
        /// Algorithm precision
        /// </summary>
        private double Eps = Math.Pow(10, -5);

        /// <summary>
        /// Gets or sets objective function
        /// </summary>
        private double J { get; set; }

        /// <summary>
        /// Gets or sets log message
        /// </summary>
        public string Log { get; set; }

        public CMeansAlgorithmNDim(List<ClusterPoint> points, List<ClusterPoint> clusters) : this(points, clusters, 2.0) 
        { 
        
        }

        /// <summary>
        /// Initialize the algorithm with points and initial clusters
        /// </summary>
        /// <param name="points">Points</param>
        /// <param name="clusters">Clusters</param>
        /// <param name="fuzzy">The fuzzyness factor to be used</param>
        public CMeansAlgorithmNDim(List<ClusterPoint> points, List<ClusterPoint> clusters, double fuzzy)
        {
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }

            if (clusters == null)
            {
                throw new ArgumentNullException("clusters");
            }

            this.Points = points;
            this.Clusters = clusters;

            U = new double[this.Points.Count, this.Clusters.Count];

            this.Fuzzyness = fuzzy;

            double diff;

            // Iterate through all points to create initial U matrix
            for (int i = 0; i < this.Points.Count; i++)
            {
                ClusterPoint p = this.Points[i];
                double sum = 0.0;

                for (int j = 0; j < this.Clusters.Count; j++)
                {
                    ClusterPoint c = this.Clusters[j];
                    diff = this.CalculateEulerDistance(p, c);
                    U[i, j] = (diff == 0) ? Eps : diff;
                    sum += U[i, j];
                }

                double sum2 = 0.0;
                for (int j = 0; j < this.Clusters.Count; j++)
                {
                    U[i, j] = 1.0 / Math.Pow(U[i, j] / sum, 2.0 / (Fuzzyness - 1.0));
                    sum2 += U[i, j];
                }

                for (int j = 0; j < this.Clusters.Count; j++)
                {
                    U[i, j] = U[i, j] / sum2;
                }
            }

            this.RecalculateClusterIndexes();
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        private CMeansAlgorithmNDim()
        {
        }

        /// <summary>
        /// Recalculates cluster indexes
        /// </summary>
        private void RecalculateClusterIndexes() 
        {
            for (int i = 0; i < this.Points.Count; i++)
            {
                double max = -1.0;
                var p = this.Points[i];

                for (int j = 0; j < this.Clusters.Count; j++)
                {
                    if (max < U[i, j])
                    {
                        max = U[i, j];
                        p.ClusterIndex = (max == 0.5) ? 0.5 : j;
                    }
                }
            }
        }

        /// <summary>
        /// Perform one step of the algorithm
        /// </summary>
        public void Step()
        {
            for (int c = 0; c < Clusters.Count; c++)
            {
                for (int h = 0; h < Points.Count; h++)
                {
                    double top = CalculateEulerDistance(Points[h], Clusters[c]);
                    if (top < 1.0) top = Eps;

                    // Bottom is the sum of distances from this data point to all clusters.
                    double sumTerms = 0.0;
                    for (int ck = 0; ck < Clusters.Count; ck++)
                    {
                        double thisDistance = CalculateEulerDistance(Points[h], Clusters[ck]);
                        if (thisDistance < 1.0) thisDistance = Eps;
                        sumTerms += Math.Pow(top / thisDistance, 2.0 / (this.Fuzzyness - 1.0));
                    }
                    
                    // Then the MF can be calculated as...
                    U[h, c] = (double)(1.0 / sumTerms);                    
                }
            }

            this.RecalculateClusterIndexes();
        }

        /// <summary>
        /// Calculates Euler's distance between point and centroid
        /// </summary>
        /// <param name="p">Point</param>
        /// <param name="c">Centroid</param>
        /// <returns>Calculated distance</returns>
        private double CalculateEulerDistance(ClusterPoint point, ClusterPoint centroid) 
        {
            //return Math.Sqrt(Math.Pow(p.X - c.X, 2) + Math.Pow(p.Y - c.Y, 2));

            double sum = 0.0;

            for (int i = 0; i < point.Dimention; i++ )
            {
                sum += Math.Pow(point.Coords[i] - centroid.Coords[i], 2);
            }

            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Calculate the objective function
        /// </summary>
        /// <returns>The objective function as double value</returns>
        private double CalculateObjectiveFunction()
        {
            double Jk = 0;

            for (int i = 0; i < this.Points.Count;i++)
            {
                for (int j = 0; j < this.Clusters.Count; j++)
                {
                    Jk += Math.Pow(U[i, j], this.Fuzzyness) * Math.Pow(this.CalculateEulerDistance(Points[i], Clusters[j]), 2);
                }
            }
            return Jk;
        }

        /// <summary>
        /// Calculates the centroids of the clusters 
        /// </summary>
        private void CalculateClusterCenters()
        {
            for (int j = 0; j < this.Clusters.Count; j++)
            {
                ClusterPoint c = this.Clusters[j];
                //double uX = 0.0;
                //double uY = 0.0;
                double[] uC = new double[c.Dimention];
                double l = 0.0;

                for (int i = 0; i < this.Points.Count; i++)
                {
                    ClusterPoint p = this.Points[i];

                    double uu = Math.Pow(U[i, j], this.Fuzzyness);

                    for (int k = 0; k < c.Dimention; k++) 
                    {
                        uC[k] += uu * c.Coords[k];
                    }

                    //uX += uu * c.X;
                    //uY += uu * c.Y;
                    l += uu;
                }

                for (int k = 0; k < c.Dimention; k++) 
                {
                    c.Coords[k] = ((int)(uC[k] / l));
                    //c.X = ((int)(uX / l));
                    //c.Y = ((int)(uY / l));
                }

                this.Log += string.Format("Cluster Centroid: ({0}; {1})" + System.Environment.NewLine, c.Coords[0], c.Coords[1]);
            }
        }

        public int Run() 
        {
            return this.Run(this.Accuracy);
        }

        /// <summary>
        /// Perform a complete run of the algorithm until the desired accuracy is achieved.
        /// For demonstration issues, the maximum Iteration counter is set to 20.
        /// </summary>
        /// <param name="accuracy">Algorithm accuracy</param>
        /// <returns>The number of steps the algorithm needed to complete</returns>
        public int Run(double accuracy)
        {
            int i = 0;
            int maxIterations = 20;
            do
            {
                i++;
                this.J = this.CalculateObjectiveFunction();
                this.CalculateClusterCenters();
                this.Step();
                double Jnew = this.CalculateObjectiveFunction();
                if (Math.Abs(this.J - Jnew) < accuracy) break;  
            }
            while (maxIterations > i);
            return i;
        }
    }

}
