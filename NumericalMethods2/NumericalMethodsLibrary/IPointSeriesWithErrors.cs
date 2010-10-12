#region Using directives

#endregion

namespace NumericalMethods.Interfaces
{
    /// This is a point series where each point has an error in the y direction.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public interface PointSeriesWithErrors : IPointSeries
    {
        /// @return double	weight of the point
        /// @param n int
        double WeightAt(int n);
    }
}
