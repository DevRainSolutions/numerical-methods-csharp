#region Using directives

#endregion

namespace NumericalMethods.Interfaces
{
    /// IPointSeries is an interface used by many classes of the package numericalMethods.
    ///
    /// An IPointSeries has the responsibility of handling mathematical
    /// points in 2-dimensional space.
    /// It is a BRIDGE to a vector containing the points.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public interface IPointSeries
    {

        /// Returns the number of points in the series.
        int Count { get; }

        /// Returns the x coordinate of the point at the given index.
        /// @param index the index of the point.
        /// @return x coordinate
        double XValueAt(int index);

        /// Returns the y coordinate of the point at the given index.
        /// @param index the index of the point.
        /// @return y coordinate
        double YValueAt(int index);
    }
}
