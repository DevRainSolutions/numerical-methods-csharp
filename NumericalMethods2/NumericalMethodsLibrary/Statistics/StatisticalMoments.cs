#region Using directives

using System;

#endregion

namespace NumericalMethods.Statistics
{
    /// A StatisticalMoments accumulates statistical moments of a random variable.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class StatisticalMoments
    {
        /// Vector containing the points.
        protected double[] _moments;

        /// Default constructor methods: declare space for 5 moments.
        public StatisticalMoments() : this( 5)
        {
        }

        /// General constructor methods.
        /// @param n number of moments to accumulate.
        public StatisticalMoments(int n)
        {
            _moments = new double[n];
            Reset();
        }

        /// @param x double	value to accumulate
        public virtual void Accumulate(double x)
        {
            int n = (int)_moments[0];
            int n1 = n + 1;
            double delta = (_moments[1] - x) / n1;
            double[] sums = new double[_moments.Length];
            sums[0] = _moments[0];
            _moments[0] = n1;
            sums[1] = _moments[1];
            _moments[1] -= delta;
            int[] pascal = new int[_moments.Length];
            pascal[0] = 1;
            pascal[1] = 1;
            double r1 = (double)n / n1;
            int nk = -1;
            n = -n;
            double cterm = delta;
            for (int k = 2; k < _moments.Length; k++)
            {
                sums[k] = _moments[k];
                nk *= n;
                cterm *= delta;
                double term = (1 + nk) * cterm;
                for (int l = k; l >= 2; l--)
                {
                    pascal[l] += pascal[l - 1];
                    term += pascal[l] * sums[l];
                    sums[l] *= delta;
                }
                pascal[1] += pascal[0];
                _moments[k] = term * r1;
            }
        }

        /// @return double average.
        public virtual double Average
        {
            get { return _moments[1]; }
        }

        /// Returns the number of accumulated counts.
        /// @return number of counts.
        public int Count
        {
            get { return (int)_moments[0]; }
        }

        /// Returns the error on average. May throw divide by zero exception.
        /// @return error on average.
        public double ErrorOnAverage
        {
            get { return Math.Sqrt(Variance / _moments[0]); }
        }

        /// @return double	F-test confidence level with data accumulated
        ///											in the supplied moments.
        /// @param m DhbStatistics.StatisticalMoments
        public double FConfidenceLevel(StatisticalMoments m)
        {
            FisherSnedecorDistribution fDistr = new FisherSnedecorDistribution((int)Count, (int)m.Count);
            return fDistr.ConfidenceLevel(Variance / m.Variance);
        }

        /// The kurtosis measures the sharpness of the distribution near
        ///														the maximum.
        /// Note: The kurtosis of the Normal distribution is 0 by definition.
        /// @return double kurtosis or NaN.
        public virtual double Kurtosis
        {
            get
            {
                if (_moments[0] < 4)
                    return double.NaN;
                double kFact = (_moments[0] - 2) * (_moments[0] - 3);
                double n1 = _moments[0] - 1;
                double v = Variance;
                return (_moments[4] * _moments[0] * _moments[0] * (_moments[0] + 1)
                                / (v * v * n1) - n1 * n1 * 3) / kFact;
            }
        }

        /// Reset all counters.
        public void Reset()
        {
            Array.Clear(_moments, 0, _moments.Length);
        }

        /// @return double skewness.
        public virtual double Skewness
        {
            get
            {
                if (_moments[0] < 3)
                    return double.NaN;
                double v = Variance;
                return _moments[3] * _moments[0] * _moments[0]
                                / (Math.Sqrt(v) * v * (_moments[0] - 1)
                                                            * (_moments[0] - 2));
            }
        }

        /// Returns the standard deviation. May throw divide by zero exception.
        /// @return double standard deviation.
        public double StandardDeviation
        {
            get { return Math.Sqrt(Variance); }
        }

        /// @return double	t-test confidence level with data accumulated
        ///											in the supplied moments.
        /// Approximation for the case where the variance of both sets may
        ///															differ.
        /// @param m DhbStatistics.StatisticalMoments
        public double TApproximateConfidenceLevel(StatisticalMoments m)
        {
            StudentDistribution tDistr = new StudentDistribution(
                                                (int)(Count + m.Count - 2));
            return tDistr.ConfidenceLevel((Average / StandardDeviation
                                                - m.Average
                                                / m.StandardDeviation)
                                                / Math.Sqrt(1 / Count
                                                            + 1 / m.Count));
        }

        /// @return double	t-test confidence level with data accumulated
        ///											in the supplied moments.
        /// The variance of both sets is assumed to be the same.
        /// @param m DhbStatistics.StatisticalMoments
        public double TConfidenceLevel(StatisticalMoments m)
        {
            int dof = (int)(Count + m.Count - 2);
            double sbar = Math.Sqrt((UnnormalizedVariance
                                        + m.UnnormalizedVariance) / dof);
            StudentDistribution tDistr = new StudentDistribution(dof);
            return tDistr.ConfidenceLevel((Average - m.Average)
                                            / (sbar * Math.Sqrt(1.0 / Count
                                                            + 1.0 / m.Count)));
        }

        /// @return double
        public virtual double UnnormalizedVariance
        {
            get { return _moments[2] * _moments[0]; }
        }

        /// Note: the variance includes the Bessel correction factor.
        /// @return double variance.
        public virtual double Variance
        {
            get
            {
                return _moments[0] < 2
                        ? double.NaN
                        : UnnormalizedVariance / (_moments[0] - 1);
            }
        }
    }
}
