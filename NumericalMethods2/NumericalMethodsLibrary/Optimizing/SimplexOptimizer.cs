#region Using directives

using System;
using System.Text;

using NumericalMethods.Interfaces;
using NumericalMethods.MatrixAlgebra;

#endregion

namespace NumericalMethods.Optimization
{
    /// Simplex optimizer of many-variable functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class SimplexOptimizer : MultiVariableOptimizer
    {
	    /// Best value found so far.
        private OptimizingVector[] _simplex;

        /// Constructor method.
        /// @param func IManyVariableFunction
        /// @param pointCreator DhbOptimizing.OptimizingPointFactory
        /// @param initialValue double[]
        public SimplexOptimizer(IManyVariableFunction func,
                OptimizingPointFactory pointCreator, double[] initialValue)
                        : base(func, pointCreator, initialValue)
        {
        }

        /// Add a new best point to the simplex
        /// @param v DhbOptimizing.OptimizingVector
        private void AddBestPoint(OptimizingVector v)
        {
            int n = _simplex.Length;
            while (--n > 0)
                _simplex[n] = _simplex[n - 1];
            _simplex[0] = v;
        }

        /// @return boolean	true if a better point was found
        /// @param g DhbVector	summit whose median is contracted 
        /// @exception DhbIllegalDimension if dimension of initial value is 0.
        private bool AddContraction(DhbVector g)
        {
            g.Accumulate(_simplex[_result.Length].Position);
            g.ScaledBy(0.5);
            OptimizingVector contractedPoint = _pointFactory.CreateVector(g, _f);
            if (contractedPoint.BetterThan(_simplex[0]))
            {
                AddBestPoint(contractedPoint);
                return true;
            }
            else
                return false;
        }

        /// @return boolean	true if a better point was found
        /// @exception DhbIllegalDimension if dimension of initial value is 0.
        private bool AddReflection(DhbVector centerOfgravity)
        {
            DhbVector reflectedVector = centerOfgravity * 2;
            reflectedVector.AccumulateNegated(
                                        _simplex[_result.Length].Position);
            OptimizingVector reflectedPoint =
                            _pointFactory.CreateVector(reflectedVector, _f);
            if (reflectedPoint.BetterThan(_simplex[0]))
            {
                reflectedVector.ScaledBy(2);
                reflectedVector.AccumulateNegated(centerOfgravity);
                OptimizingVector expandedPoint =
                            _pointFactory.CreateVector(reflectedVector, _f);
                if (expandedPoint.BetterThan(reflectedPoint))
                    AddBestPoint(expandedPoint);
                else
                    AddBestPoint(reflectedPoint);
                return true;
            }
            else
                return false;
        }

        /// @return DhbVector	center of gravity of best points of simplex,
        ///													except worst one
        private DhbVector CenterOfGravity()
        {
            DhbVector g = new DhbVector(_result.Length);
            for (int i = 0; i < _result.Length; i++)
                g.Accumulate(_simplex[i].Position);
            g.ScaledBy(1.0 / _result.Length);
            return g;
        }

        /// @return double	maximum simplex extent in each direction
        private double ComputePrecision()
        {
            int i, j;
            double[] position = _simplex[0].Position;
            double[] min = new double[position.Length];
            double[] max = new double[position.Length];
//            for (i = 0; i < position.Length; i++)
//            {
//                min[i] = position[i];
//                max[i] = position[i];
//            }
            position.CopyTo(min, 0);
            position.CopyTo(max, 0);
            for (j = 1; j < _simplex.Length; j++)
            {
                position = _simplex[j].Position;
                for (i = 0; i < position.Length; i++)
                {
                    min[i] = Math.Min(min[i], position[i]);
                    max[i] = Math.Max(max[i], position[i]);
                }
            }
            double eps = 0;
            for (i = 1; i < position.Length; i++)
                eps = Math.Max(eps, this.RelativePrecision(max[i] - min[i], _result[i]));
            return eps;
        }
/**
 * Reduce the simplex from the best point.
 */
        private void contractSimplex()
        {
            double[] bestPoint = _simplex[0].Position;
            for (int i = 1; i < _simplex.Length; i++)
                _simplex[i].ContractFrom(bestPoint);
            SortPoints(_simplex);
        }
/**
 * Here precision is the largest extent of the simplex.
 */
        public override double EvaluateIteration()
        {
            try
            {
                double bestValue = _simplex[0].Value;
                DhbVector g = CenterOfGravity();
                if (!AddReflection(g))
                {
                    if (!AddContraction(g))
                        contractSimplex();
                }
                _result = _simplex[0].Position;
                return ComputePrecision();
            }
            catch (DhbIllegalDimension) { return 1; } ;
        }

        /// Create a Simplex by finding the optimum in each direction
        /// starting from the initial value..
        public override void InitializeIterations()
        {
            double[] v = new double[_result.Length];
            for (int i = 0; i < _result.Length; i++)
                v[i] = 0;
            VectorProjectedFunction projection = new VectorProjectedFunction(_f, _result, v);
            OneVariableFunctionOptimizer unidimensionalFinder =
                new OneVariableFunctionOptimizer(projection, _pointFactory);
            unidimensionalFinder.DesiredPrecision = this.DesiredPrecision;
            _simplex = new OptimizingVector[_result.Length + 1];
            try
            {
                for (int i = 0; i < _result.Length; i++)
                {
                    v[i] = 1;
                    projection.SetDirection(v);
                    v[i] = 0;
                    unidimensionalFinder.InitialValue = 0;
                    unidimensionalFinder.Evaluate();
                    _simplex[i] = _pointFactory.CreateVector(
                                    projection.ArgumentAt(
                                    unidimensionalFinder.Result), _f);
                }
            }
            catch (DhbIllegalDimension) { } ;
            _simplex[_result.Length] = _pointFactory.CreateVector(_result, _f);
            SortPoints(_simplex);
        }

        /// Returns a string that represents the value of this object.
        /// @return a string representation of the receiver
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_simplex[0]);
            for (int i = 1; i < _simplex.Length; i++)
            {
                sb.Append('\n');
                sb.Append(_simplex[i]);
            }
            return sb.ToString();
        }
    }
}
