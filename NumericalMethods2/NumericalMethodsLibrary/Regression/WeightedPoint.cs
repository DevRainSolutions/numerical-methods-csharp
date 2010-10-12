#region Using directives

using System;
using NumericalMethods.Statistics;
using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Approximation
{
    /// Point with error used in chi-square test and least square fits
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class WeightedPoint
    {
        private double _xValue;
        private double _yValue;
        private double _weight;
        private double _error = double.NaN;

        /// Constructor method.
        /// @param x double
        /// @param y double
        public WeightedPoint(double x, double y) : this(x, y, 1)
        {
        }

        /// Constructor method.
        /// @param x double
        /// @param y double
        /// @param w double
        public WeightedPoint(double x, double y, double w)
        {
            _xValue = x;
            _yValue = y;
            _weight = w;
        }

        /// Constructor method.
        /// @param x double
        /// @param n int	a Histogram bin content
        public WeightedPoint(double x, int n) : this(x, n, 1.0 / Math.Max(n, 1))
        {
        }

        /// Constructor method.
        /// @param x double
        /// @param m DhbStatistics.StatisticalMoments
        public WeightedPoint(double x, StatisticalMoments m) : this(x, m.Average)
        {
            Error = m.ErrorOnAverage;
        }

        /// @return double	contribution to chi^2 sum against
        ///												a theoretical function
        /// @param wp WeightedPoint
        public double Chi2Contribution(WeightedPoint wp)
        {
            double residue = _yValue - wp.YValue;
            return residue * residue / (1 / wp.Weight + 1 / _weight);
        }

        /// @return double	contribution to chi^2 sum against
        ///												a theoretical function
        /// @param f DhbInterfaces.OneVariableFunction
        public double Chi2Contribution(IOneVariableFunction f)
        {
            double residue = _yValue - f.Value(_xValue);
            return residue * residue * _weight;
        }

        /// @return double	error of the receiver
        public double Error
        {
            get
            {
                if (double.IsNaN(_error))
                    _error = 1 / Math.Sqrt(_weight);
                return _error;
            }
            set
            {
                _error = value;
                _weight = 1 / (value * value);
            }
        }

        /// @return string
        public override string ToString()
        {
            return string.Format("({0},{1}+-{2})", _xValue, _yValue, Error);
        }

        /// @return double	weight of the receiver
        public double Weight
        {
            get { return _weight; }
        }

        /// @return double	x value of the receiver
        public double XValue
        {
            get { return _xValue; }
        }

        /// @return double	y value of the receiver
        public double YValue
        {
            get { return _yValue; }
        }
    }
}
