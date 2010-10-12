#region Using directives

using System.Collections.Generic;
using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Curves
{
    /// A Curve is a series of points. A point is implemented as an array
    /// of two doubles. The points are stored in a vector so that points
    /// can be added or removed.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class Curve : IPointSeries, ICurve
    {
        /// Vector containing the points.
        protected List<double[]> points;

        /// Constructor method. Initializes the vector.
        public Curve()
        {
            points = new List<double[]>();
        }

        /// Adds a point to the curve defined by its 2-dimensional coordinates.
        /// @param x double x-coordinate of the point
        /// @param y double y-coordinate of the point
        public void AddPoint(double x, double y)
        {
            points.Add(new double[] { x, y });
        }

        /// Removes the point at the specified index.
        /// @param int index of the point to remove
        public void RemovePointAt(int index)
        {
            points.RemoveAt(index);
        }

        /// @return int the number of points in the curve.
        public int Count
        {
            get { return points.Count; }
        }

        /// @return double the x coordinate of the point at the given index.
        /// @param int index the index of the point.
        public double XValueAt(int index)
        {
            return points[index][0];
        }

        /// @return double the y coordinate of the point at the given index.
        /// @param int index the index of the point.
        public double YValueAt(int index)
        {
            return points[index][1];
        }
    }
}
