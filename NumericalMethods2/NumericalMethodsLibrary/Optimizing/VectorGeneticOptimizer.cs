#region Using directives

using System;
using System.Text;

using NumericalMethods.Interfaces;
using NumericalMethods.MatrixAlgebra;

#endregion

namespace NumericalMethods.Optimization
{
    /// Genetic optimizer of many-variable functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class VectorGeneticOptimizer : GeneticOptimizer
    {
	    /// Best values found so far.
        private OptimizingVector[] _bestPoints;
	    /// Number of points filled so far.
        private int _fillIndex;

        /// Constructor method.
        /// @param func IManyVariableFunction
        /// @param pointCreator DhbOptimizing.OptimizingPointFactory
        /// @param chrManager DhbOptimizing.ChromosomeManager
        public VectorGeneticOptimizer(IManyVariableFunction func,
                                        OptimizingPointFactory pointCreator,
                                        ChromosomeManager chrManager)
                    : base(func, pointCreator, chrManager)
        {
        }

        /// @param x DhbVector
        public override void CollectPoint(object x)
        {
            OptimizingVector v = _pointFactory.CreateVector((DhbVector)x, _f);
            if (_fillIndex == 0 || _bestPoints[_fillIndex - 1].BetterThan(v))
            {
                _bestPoints[_fillIndex++] = v;
                return;
            }
            int n = 0;
            int m = _fillIndex - 1;
            if (_bestPoints[0].BetterThan(v))
            {
                int k;
                while (m - n > 1)
                {
                    k = (n + m) / 2;
                    if (v.BetterThan(_bestPoints[k]))
                        m = k;
                    else
                        n = k;
                }
                n = m;
            }
            for (m = _fillIndex; m > n; m--)
                _bestPoints[m] = _bestPoints[m - 1];
            _bestPoints[n] = v;
            _fillIndex += 1;
        }

        /// @return double[]		best point found so far
        public override double[] Result
        {
            get { return _bestPoints[0].Position; }
        }

        /// @return DhbVector	vector at given index
        /// @param n int
        public override object IndividualAt(int n)
        {
            try { return new DhbVector(_bestPoints[n].Position); }
            catch (OverflowException) { return null; } ;
        }

        /// @param n int	size of the initial population
        public override void InitializeIterations(int n)
        {
            _bestPoints = new OptimizingVector[n];
        }

        /// @return double[]		fitness scale for random generation
        public override double[] RandomScale()
        {
            double[] f = new double[_bestPoints.Length];
            double sum = 0;
            for (int i = 0; i < _bestPoints.Length; i++)
            {
                f[i] = _bestPoints[i].Value + sum;
                sum += _bestPoints[i].Value;
            }
            sum = 1 / sum;
            for (int i = 0; i < _bestPoints.Length; i++)
                f[i] *= sum;
            return f;
        }

        public override void Reset()
        {
            _fillIndex = 0;
        }

        /// Returns a String that represents the value of this object.
        /// @return a string representation of the receiver
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_bestPoints[0]);
            for (int i = 1; i < Math.Min(_bestPoints.Length, 30); i++)
            {
                sb.Append('\n');
                sb.Append(_bestPoints[i]);
            }
            return sb.ToString();
        }
    }
}
