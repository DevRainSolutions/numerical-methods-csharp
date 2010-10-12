#region Using directives

#endregion

namespace NumericalMethods.Interfaces
{
    public interface ICurve : IPointSeries
    {
        void AddPoint(double x, double y);
        void RemovePointAt(int index);
    }
}
