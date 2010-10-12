#region Using directives

using System;
using System.Text;

using NumericalMethods.Interfaces;
using NumericalMethods.Iterations;


#endregion

namespace NumericalMethods.Optimization
{
    /// Optimizer of one-variable functions
    /// (uses golden search algorithm).
    ///
    /// @author Didier H. Besset
    public class OneVariableFunctionOptimizer : FunctionalIterator
    {
        private readonly static double GoldenSection = (3 - Math.Sqrt(5)) / 2;
	    /// Best points found so far.
        private OptimizingPoint[] _bestPoints = null;
	    /// Optimizing strategy (minimum or maximum).
        private OptimizingPointFactory _pointFactory;

        /// Constructor method
        /// @param func IOneVariableFunction
        /// @param pointCreator OptimizingPointFactory	a factory to create
        ///													strategy points
        public OneVariableFunctionOptimizer(IOneVariableFunction func,
                                        OptimizingPointFactory pointCreator)
                    : base(func)
        {
            _pointFactory = pointCreator;
        }

        /// @return double	the relative precision on the result
        private double ComputePrecision()
        {
            return RelativePrecision(Math.Abs(_bestPoints[2].Position
                                                - _bestPoints[1].Position),
                                      Math.Abs(_bestPoints[0].Position));
        }

        /// @return double	current precision of result
        public override double EvaluateIteration()
        {
            if (_bestPoints[2].Position - _bestPoints[1].Position
                        > _bestPoints[1].Position - _bestPoints[0].Position)
                ReducePoints(2);
            else
                ReducePoints(0);
            _result = _bestPoints[1].Position;
            return ComputePrecision();
        }

        public override void InitializeIterations()
        {
            OptimizingBracketFinder bracketFinder =
                                new OptimizingBracketFinder(_f, _pointFactory);
            bracketFinder.InitialValue = _result;
            bracketFinder.Evaluate();
            _bestPoints = bracketFinder.BestPoints;
        }

        /// Apply bisection on points 1 and n
        /// @param n int	index of worst point of bisected interval
        private void ReducePoints(int n)
        {
            double x = _bestPoints[1].Position;
            x += GoldenSection * (_bestPoints[n].Position - x);
            OptimizingPoint newPoint = _pointFactory.CreatePoint(x, _f);
            if (newPoint.BetterThan(_bestPoints[1]))
            {
                _bestPoints[2 - n] = _bestPoints[1];
                _bestPoints[1] = newPoint;
            }
            else
                _bestPoints[n] = newPoint;
        }

        /// @return java.lang.String
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                string.Format("{0} iterations, precision = {1}",
                                this.Iterations, this.Precision ) );
            for (int i = 0; i < _bestPoints.Length; i++)
            {
                sb.Append('\n');
                sb.Append(_bestPoints[i]);
            }
            return sb.ToString();
        }
    }
}
