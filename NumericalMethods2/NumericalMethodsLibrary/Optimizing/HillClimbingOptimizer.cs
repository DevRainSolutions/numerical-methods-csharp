#region Using directives

using System;
using System.Text;

using NumericalMethods.Interfaces;
using NumericalMethods.MatrixAlgebra;

#endregion

namespace NumericalMethods.Optimization
{
    /// Hill climbing optimizer using Powell's algorithm.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class HillClimbingOptimizer : MultiVariableOptimizer
    {
	    /// One dimensional optimizer used in each direction.
        private OneVariableFunctionOptimizer _unidimensionalFinder;
	    /// Projected goal function on independent directions.
        private VectorProjectedFunction[] _projections;

        /// Constructor method.
        /// @param func IManyVariableFunction
        /// @param pointCreator DhbOptimizing.OptimizingPointFactory
        public HillClimbingOptimizer(IManyVariableFunction func,
                            OptimizingPointFactory pointCreator, double[] v)
                    : base(func, pointCreator, v)
        {
        }

        private void AdjustLastDirection(DhbVector start)
        {
            try
            {
                int n = _projections.Length - 1;
                _projections[n].SetOrigin(_result);
                DhbVector newDirection = _projections[n].Origin - start;
                double norm = newDirection.Norm;
                if (norm > this.DesiredPrecision)
                {
                    newDirection.ScaledBy(1 / norm);
                    _projections[n].Direction = newDirection;
                    _unidimensionalFinder.Function = _projections[n];
                    _unidimensionalFinder.InitialValue = 0;
                    _unidimensionalFinder.Evaluate();
                    _result = _projections[n].ArgumentAt(
                            _unidimensionalFinder.Result).ToComponents();
                }
            }
            catch (DhbIllegalDimension) { } ;
        }

        /// @return double	relative precision of current result
        /// @param x double[]	result at previous iteration
        private double ComputePrecision(double[] x)
        {
            double eps = 0;
            for (int i = 0; i < _result.Length; i++)
                eps = Math.Max(eps, RelativePrecision(
                                    Math.Abs(_result[i] - x[i]), _result[i]));
            return eps;
        }

        public override double EvaluateIteration()
        {
            try
            {
                DhbVector start;
                start = new DhbVector(_result);
                int n = _projections.Length;
                for (int i = 0; i < n; i++)
                {
                    _projections[i].SetOrigin(_result);
                    _unidimensionalFinder.Function = _projections[i];
                    _unidimensionalFinder.InitialValue = 0;
                    _unidimensionalFinder.Evaluate();
                    _result = _projections[i].ArgumentAt(
                            _unidimensionalFinder.Result).ToComponents();
                }
                RotateDirections();
                AdjustLastDirection(start);
                return ComputePrecision(start.ToComponents());
            }
            catch (OverflowException) { return double.NaN; }
            catch (DhbIllegalDimension) { return double.NaN; } ;
        }

        public override void InitializeIterations()
        {
            _projections = new VectorProjectedFunction[_result.Length];
            double[] v = new double[_result.Length];
//            for (int i = 0; i < _projections.Length; i++)
//                v[i] = 0;
            for (int i = 0; i < _projections.Length; i++)
            {
                v[i] = 1;
                _projections[i] = new VectorProjectedFunction(_f, _result, v);
                v[i] = 0;
            }
            _unidimensionalFinder = new OneVariableFunctionOptimizer(
                                                _projections[0], _pointFactory);
            _unidimensionalFinder.DesiredPrecision = this.DesiredPrecision;
        }

        private void RotateDirections()
        {
            DhbVector firstDirection = _projections[0].Direction;
            int n = _projections.Length;
            for (int i = 1; i < n; i++)
                _projections[i - 1].Direction = _projections[i].Direction;
            _projections[n - 1].Direction = firstDirection;
        }

        /// Returns a string that represents the value of this object.
        /// @return a string representation of the receiver
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                string.Format("{0} iterations, precision = {1}\nResult:",
                                this.Iterations, this.Precision ) );
            for (int i = 0; i < _result.Length; i++)
            {
                sb.Append(' ');
                sb.Append(_result[i]);
            }
            for (int i = 0; i < _projections.Length; i++)
            {
                sb.Append('\n');
                sb.Append(_projections[i]);
            }
            return sb.ToString();
        }
    }
}
