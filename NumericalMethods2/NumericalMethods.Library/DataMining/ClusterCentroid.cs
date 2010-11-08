using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Clustering
{
	public class ClusterCentroid : ClusterPoint
	{
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="x">Centroid x-coord</param>
        /// <param name="y">Centroid y-coord</param>
        public ClusterCentroid(double x, double y)
            : base(x, y)
        {
        }
	}


}
