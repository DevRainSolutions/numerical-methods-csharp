using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Clustering
{
    public class ClusterPoint
    {
        /// <summary>
        /// Gets or sets X-coord of the point
        /// </summary>
        public double X { get { return this.Coords[0]; } set { this.Coords[0] = value; } }

        /// <summary>
        /// Gets or sets Y-coord of the point
        /// </summary>
        public double Y { get { return this.Coords[1]; } set { this.Coords[1] = value; } }

        /// <summary>
        /// Gets or sets some additional data for point
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Gets or sets cluster index
        /// </summary>
        public double ClusterIndex { get; set; }

        public List<double> Coords { get; set; }

        public int Dimention { get { return this.Coords.Count; } }

        public ClusterPoint(List<double> coords) : this(coords, null)
        {
        }

        public ClusterPoint(List<double> coords, object tag)
        {
            this.Coords = coords;
            this.Tag = tag;
            this.ClusterIndex = -1;
        }

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="x">X-coord</param>
        /// <param name="y">Y-coord</param>
        public ClusterPoint(double x, double y)
            : this(x, y, null)
        {
        }

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="x">X-coord</param>
        /// <param name="y">Y-coord</param>
        public ClusterPoint(double x, double y, object tag)
        {
            this.Coords = new List<double>();
            this.Coords.Add(x);
            this.Coords.Add(y);
            this.Tag = tag;
            this.ClusterIndex = -1;
        }
    }

}
