#region Using directives

using System;
using NumericalMethods.Interfaces;
using NumericalMethods.Statistics;
using NumericalMethods.DhbFunctionEvaluation;
using NumericalMethods.Approximation;


#endregion

namespace NumericalMethods.Curves
{
    /// A Histogram stores the frequency of hits into bins of equal width.
    ///
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class Histogram : IPointSeries, IHistogram
    {
	    /// Lower limit of first histogram bin.
        private double _minimum;

        /// Width of a bin.
        private double _binWidth;

	    /// Histogram contents.
        private int[] _contents;
        
	    /// Flag to allow automatical growth.
        private bool _growthAllowed = false;

	    /// Flag to enforce integer bin width.
        private bool _integerBinWidth = false;

	    /// Counts of values located below first bin.
	    /// Note: also used to count cached values when caching is in effect.
        private int _underflow;

	    /// Counts of values located above last bin.
	    /// Note: also used to hold desired number of bins when caching is in effect.
        private int _overflow;

	    /// Statistical moments of values accumulated within the histogram limits.
        private FixedStatisticalMoments _moments;

	    /// Flag indicating the histogram is caching values to compute adequate range.
        private bool _cached = false;

	    /// Cache for accumulated values.
        private double[] _cache;

        /// <summary>
        /// Constructor method with unknown limits and a desired number
        /// of 50 bins. The first 100 accumulated values are cached.
        /// Then, a suitable range is computed.
        /// </summary>
        public Histogram() : 	this( 100)
        {
        }

        /// <summary>
        /// Constructor method for approximate range for a desired number
        /// of 50 bins.
        /// All parameters are adjusted so that the bin width is a round number.
        /// @param from approximate lower limit of first histogram bin.
        /// @param to approximate upper limit of last histogram bin.
        /// </summary>
        public Histogram(double from, double to) : this( from, to, 50)
        {
        }

        /// <summary>
        /// Constructor method for approximate range and desired number of bins.
        /// All parameters are adjusted so that the bin width is a round number.
        /// @param from approximate lower limit of first histogram bin.
        /// @param to approximate upper limit of last histogram bin.
        /// @param bins desired number of bins.
        /// </summary>
        public Histogram(double from, double to, int bins)
        {
            DefineParameters(from, to, bins);
        }

        /// <summary>
        /// Constructor method with unknown limits and a desired number of
        /// 50 bins.
        /// Accumulated values are first cached. When the cache is full,
        /// a suitable range is computed.
        /// @param n size of cache.
        /// </summary>
        public Histogram(int n) : this( n, 50)
        {
        }

        /// <summary>
        /// General constructor method.
        /// @param n number of bins.
        /// @param min lower limit of first histogram bin.
        /// @param width bin width (must be positive).
        /// @exception ArgumentOutOfRangeException
        ///							if the number of bins is non-positive,
        ///							if the limits are inversed.
        /// </summary>
        public Histogram(int n, double min, double width)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(
                                            "Non-positive bin width: " + width);
            _contents = new int[n];
            _minimum = min;
            _binWidth = width;
            Reset();
        }

        /// <summary>
        /// Constructor method with unknown limits.
        /// Accumulated values are first cached. When the cache is full,
        /// a suitable range is computed.
        /// @param n size of cache.
        /// @param m desired number of bins
        /// </summary>
        public Histogram(int n, int m)
        {
            _cached = true;
            _cache = new double[n];
            _underflow = 0;
            _overflow = m;
        }

        /// Accumulate a random variable.
        /// @param x value of the random variable.
        public void Accumulate(double x)
        {
            if (_cached)
            {
                _cache[_underflow++] = x;
                if (_underflow == _cache.Length)
                    FlushCache();
            }
            else if (x < _minimum)
            {
                if (_growthAllowed)
                {
                    ExpandDown(x);
                    _moments.Accumulate(x);
                }
                else
                    _underflow++;
            }
            else
            {
                int index = BinIndex(x);
                if (index < _contents.Length)
                {
                    _contents[index]++;
                    _moments.Accumulate(x);
                }
                else if (_growthAllowed)
                {
                    ExpandUp(x);
                    _moments.Accumulate(x);
                }
                else
                    _overflow++;
            }
        }

        /// Returns the average of the values accumulated in the histogram bins.
        /// @return average.
        public double Average
        {
            get
            {
                if (_cached)
                    FlushCache();
                return _moments.Average;
            }
        }

        /// @return int	index of the bin where x is located
        /// @param x double
        public int BinIndex(double x)
        {
            return (int)Math.Floor((x - _minimum) / _binWidth);
        }

        /// @param pdf DhbStatistics.ScaledProbabilityDensityFunction
        /// @return double	chi2 of histogram compared to supplied
        /// 											probability distribution.
        public double Chi2Against(ScaledProbabilityDensityFunction pdf)
        {
            double chi2 = 0;
            for (int i = 0; i < _contents.Length; i++)
                chi2 += new WeightedPoint(1, _contents[i]).Chi2Contribution(pdf);
            return chi2;
        }

        /// @param pdf DhbStatistics.ScaledProbabilityDensityFunction
        /// @return double	chi2 of histogram compared to supplied
        ///											probability distribution.
        public double Chi2ConfidenceLevel(
                                        ScaledProbabilityDensityFunction pdf)
        {
            return (new ChiSquareDistribution(_contents.Length -
                                                    pdf.Parameters.Length))
                            .ConfidenceLevel(Chi2Against(pdf));
        }

        /// Returns the number of accumulated counts.
        /// @return number of counts.
        public int Count
        {
            get { return _cached ? _underflow : _moments.Count; }
        }

        /// Compute suitable limits and bin width.
        /// @param from approximate lower limit of first histogram bin.
        /// @param to approximate upper limit of last histogram bin.
        /// @param bins desired number of bins.
        /// @exception ArgumentOutOfRangeException
        ///							if the number of bins is non-positive,
        ///							if the limits are inversed.
        private void DefineParameters(double from, double to, int bins)
        {
            if (from >= to)
                throw new ArgumentOutOfRangeException(
                        "Inverted range: minimum = " + from + ", maximum = " + to);
            if (bins < 1)
                throw new ArgumentOutOfRangeException(
                                        "Non-positive number of bins: " + bins);
            _binWidth = DhbMath.RoundToScale((to - from) / bins,
                                                            _integerBinWidth);
            _minimum = _binWidth * Math.Floor(from / _binWidth);
            int numberOfBins = (int)Math.Ceiling((to - _minimum) / _binWidth);
            if (_minimum + numberOfBins * _binWidth <= to)
                numberOfBins++;
            _contents = new int[numberOfBins];
            _cached = false;
            _cache = null;
            Reset();
        }

        /// Returns the error on average. May throw divide by zero exception.
        /// @return error on average.
        public double ErrorOnAverage
        {
            get
            {
                if (_cached)
                    FlushCache();
                return _moments.ErrorOnAverage;
            }
        }

        /// Expand the contents so that the lowest bin include the specified
        ///																value.
        /// @param x value to be included.
        private void ExpandDown(double x)
        {
            int addSize = (int)Math.Ceiling((_minimum - x) / _binWidth);
            int[] newContents = new int[addSize + _contents.Length];
            _minimum -= addSize * _binWidth;
//            int n;
            newContents[0] = 1;
            Array.Clear(newContents, 1, addSize - 1);
//            for (n = 1; n < addSize; n++) // Replaced by previous line
//                newContents[n] = 0;
            for (int n = 0; n < _contents.Length; n++)
                newContents[n + addSize] = _contents[n];
            _contents = newContents;
        }

        /// Expand the contents so that the highest bin include the specified
        ///																value.
        /// @param x value to be included.
        private void ExpandUp(double x)
        {
            int newSize = (int)Math.Ceiling((x - _minimum) / _binWidth);
            int[] newContents = new int[newSize];
            int n;
            for (n = 0; n < _contents.Length; n++)
                newContents[n] = _contents[n];
            Array.Clear(newContents, _contents.Length, newSize - 1 - _contents.Length);
            newContents[newSize - 1] = 1;
//            for (n = _contents.Length; n < newSize - 1; n++)
//                newContents[n] = 0;
//            newContents[n] = 1;
            _contents = newContents;
        }

        /// @return double	F-test confidence level with data accumulated
        ///											in the supplied histogram.
        /// @param h DhbScientificCurves.Histogram
        public double FConfidenceLevel(Histogram h)
        {
            return _moments.FConfidenceLevel(h._moments);
        }

        /// Flush the values from the cache.
        private void FlushCache()
        {
            double min = _cache[0];
            double max = min;
            int cacheSize = _underflow;
            double[] cachedValues = _cache;
            for (int n = 1; n < cacheSize; n++)
            {
                if (_cache[n] < min)
                    min = _cache[n];
                else if (_cache[n] > max)
                    max = _cache[n];
            }
            DefineParameters(min, max, _overflow);
            for (int n = 0; n < cacheSize; n++)
                Accumulate(cachedValues[n]);
        }

        /// @return int
        /// @param x double
        public double GetBinContent(double x)
        {
            if (x < _minimum)
                return double.NaN;
            int n = BinIndex(x);
            return n < _contents.Length ? YValueAt(n) : double.NaN;
        }

        /// Returns the low and high limits and the content of the bin 
        /// containing the specified number or nul if the specified number
        /// lies outside of the histogram limits.
        /// @return a 3-dimensional array containing the bin limits and
        ///													the bin content.
        public double[] GetBinParameters(double x)
        {
            if (x >= _minimum)
            {
                int index = (int)Math.Floor((x - _minimum) / _binWidth);
                if (index < _contents.Length)
                {
                    double[] answer = new double[3];
                    answer[0] = _minimum + index * _binWidth;
                    answer[1] = answer[0] + _binWidth;
                    answer[2] = _contents[index];
                    return answer;
                }
            }
            return null;
        }

        /// Returns the bin width.
        /// @return bin width.
        public double BinWidth
        {
            get { return _binWidth; }
        }

        /// @return double
        /// @param x double
        /// @param y double
        public double GetCountsBetween(double x, double y)
        {
            int n = BinIndex(x);
            int m = BinIndex(y);
            double sum = _contents[n] * ((_minimum - x) / _binWidth - (n + 1))
                        + _contents[m] * ((y - _minimum) / _binWidth - m);
            while (++n < m)
                sum += _contents[n];
            return sum;
        }

        /// @return double integrated count up to x
        /// @param x double
        public double CountsUpTo(double x)
        {
            int n = BinIndex(x);
            double sum = _contents[n] * ((x - _minimum) / _binWidth - n)
                                                                + _underflow;
            for (int i = 0; i < n; i++)
                sum += _contents[i];
            return sum;
        }

        /// Returns the number of bins of the histogram.
        /// @return number of bins.
        public double Dimension
        {
            get
            {
                if (_cached)
                    FlushCache();
                return _contents.Length;
            }
        }

        /// @return double
        public double Maximum
        {
            get { return _minimum + (_contents.Length - 1) * _binWidth; }
        }

        /// Returns the lower bin limit of the first bin.
        /// @return minimum histogram range.
        public double Minimum
        {
            get { return _minimum; }
        }

        /// Returns the range of values to be plotted.
        /// @return An array of 4 double values as follows
        /// index 0: minimum of X range
        ///       1: maximum of X range
        ///       2: minimum of Y range
        ///       3: maximum of Y range
        public double[] Range
        {
            get
            {
                if (_cached)
                    FlushCache();
                double[] range = new double[] { _minimum, Maximum, 0, 0 };
                for (int n = 0; n < _contents.Length; n++)
                    range[3] = Math.Max(range[3], _contents[n]);
                return range;
            }
        }

        /// Returns the kurtosis of the values accumulated in the histogram bins.
        /// The kurtosis measures the sharpness of the distribution near the maximum.
        /// Note: The kurtosis of the Normal distribution is 0 by definition.
        /// @return double kurtosis.
        public double Kurtosis
        {
            get
            {
                if (_cached)
                    FlushCache();
                return _moments.Kurtosis;
            }
        }

        /// @return FixedStatisticalMoments
        protected FixedStatisticalMoments Moments
        {
            get { return _moments; }
        }

        /// Returns the number of counts accumulated below the lowest bin.
        /// @return overflow.
        public long Overflow
        {
            get { return _cached ? 0 : _overflow; }
        }

        /// Reset histogram.
        public void Reset()
        {
            if (_moments == null)
                _moments = new FixedStatisticalMoments();
            else
                _moments.Reset();
            _underflow = 0;
            _overflow = 0;
            Array.Clear(_contents, 0, _contents.Length);
        }

        /// Allows histogram contents to grow in order to contain all
        ///											accumulated values.
        /// Note: Should not be called after counts have been accumulated in 
        /// the underflow and/or overflow of the histogram.
        /// @exception InvalidOperationException
        ///								if the histogram has some contents.
        public void SetGrowthAllowed()
        {
            if (_underflow != 0 || _overflow != 0)
            {
                if (!_cached)
                    throw new InvalidOperationException(
                            "Cannot allow growth to a non-empty histogram");
            }
            _growthAllowed = true;
        }

        /// Forces the bin width of the histogram to be integer.
        /// Note: Can only be called when the histogram is cached.
        /// @exception InvalidOperationException
        ///								if the histogram has some contents.
        public void SetIntegerBinWidth()
        {
            if (!_cached)
                throw new InvalidOperationException(
                        "Cannot change bin width of a non-empty histogram");
            _integerBinWidth = true;
        }

        /// Returns the number of points in the series.
        public int Size
        {
            get
            {
                if (_cached)
                    FlushCache();
                return _contents.Length;
            }
        }

        /// Returns the skewness of the values accumulated in the histogram bins.
        /// @return double skewness.
        public double Skewness
        {
            get
            {
                if (_cached)
                    FlushCache();
                return _moments.Skewness;
            }
        }

        /// Returns the standard deviation of the values accumulated in the histogram bins.
        /// @return double standard deviation.
        public double StandardDeviation
        {
            get
            {
                if (_cached)
                    FlushCache();
                return _moments.StandardDeviation;
            }
        }

        /// @return double	t-test confidence level with data accumulated 
        ///											in the supplied histogram.
        /// @param h DhbScientificCurves.Histogram
        public double TConfidenceLevel(Histogram h)
        {
            return _moments.TConfidenceLevel(h._moments);
        }

        /// @return long
        public long TotalCount
        {
            get
            {
                return _cached ? _underflow
                              : _moments.Count + _overflow + _underflow;
            }
        }

        /// Returns the number of counts accumulated below the lowest bin.
        /// @return underflow.
        public long Underflow
        {
            get { return _cached ? 0 : _underflow; }
        }

        /// Returns the variance of the values accumulated in the histogram bins.
        /// @return double variance.
        public double Variance
        {
            get
            {
                if (_cached)
                    FlushCache();
                return _moments.Variance;
            }
        }

        /// @return DhbEstimation.WeightedPoint	corresponding to bin n.
        /// @param n int
        public WeightedPoint WeightedPointAt(int n)
        {
            return new WeightedPoint(XValueAt(n), _contents[n]);
        }

        /// Returns the middle of the bin at the specified index.
        /// @param index the index of the bin.
        /// @return middle of bin
        public double XValueAt(int index)
        {
            return (index + 0.5) * _binWidth + _minimum;
        }

        /// Returns the content of the bin at the given index.
        /// @param index the index of the bin.
        /// @return bin content
        public double YValueAt(int index)
        {
            if (_cached)
                FlushCache();
            return (index >= 0 && index < _contents.Length) ? _contents[index] : 0;
        }
    }
}
