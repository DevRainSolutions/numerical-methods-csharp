#region Using directives

#endregion

namespace NumericalMethods.Interfaces
{
    /// IHistogram is an interface for the Histogram class
    /// Mainly created so there's no circular references between
    /// the DhbStatistics and DhbScientificCurves projects.
    ///
    /// @author edgar.sanchez@logicstudio.net
    public interface IHistogram
    {
        double Average { get; }
        int Count { get; }
        double BinWidth { get; }
        double Minimum { get; }
        double Variance { get; }
        double StandardDeviation { get; }
    }
}
