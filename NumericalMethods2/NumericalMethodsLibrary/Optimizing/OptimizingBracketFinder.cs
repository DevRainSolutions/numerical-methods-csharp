#region Using directives

using System;

using NumericalMethods.Interfaces;
using NumericalMethods.Iterations;

#endregion

namespace NumericalMethods.Optimization
{
    /// Finds a bracket for the optimum of a one-variable function.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class OptimizingBracketFinder : FunctionalIterator
    {
        private OptimizingPoint[] _bestPoints = null;
        private OptimizingPointFactory _pointFactory;

        /// Constructor method
        /// @param func IOneVariableFunction
        /// @param pointCreator OptimizingPointFactory	a factory to create
        ///													strategy points
        public OptimizingBracketFinder(IOneVariableFunction func,
                                        OptimizingPointFactory pointCreator)
                    : base(func)
        {
            _pointFactory = pointCreator;
        }

        /// @return double	1 as long as no bracket has been found
        private double ComputePrecision()
        {
            return _bestPoints[1].BetterThan(_bestPoints[0]) &&
                                    _bestPoints[1].BetterThan(_bestPoints[2])
                        ? 0 : 1;
        }

        /// @return double	pseudo-precision of the current search
        public override double EvaluateIteration()
        {
            if (_bestPoints[0].BetterThan(_bestPoints[1]))
                MoveTowardNegative();
            else if (_bestPoints[2].BetterThan(_bestPoints[1]))
                MoveTowardPositive();
            return ComputePrecision();
        }

        /// @return OptimizingPoint[]	a triplet bracketing the optimum
        public OptimizingPoint[] BestPoints
        {
            get { return _bestPoints; }
        }

        /// Use random locations (drunkard's walk algorithm).
        public override void InitializeIterations()
        {
            Random generator = new Random();
            _bestPoints = new OptimizingPoint[3];
            if (double.IsNaN(_result))
                _result = generator.NextDouble();
            _bestPoints[0] = _pointFactory.CreatePoint(_result, _f);
            _bestPoints[1] = _pointFactory.CreatePoint(generator.NextDouble()
                                            + _bestPoints[0].Position, _f);
            _bestPoints[2] = _pointFactory.CreatePoint(generator.NextDouble()
                                            + _bestPoints[1].Position, _f);
        }

        /// Shift the best points toward negative positions.
        private void MoveTowardNegative()
        {
            OptimizingPoint newPoint = _pointFactory.CreatePoint(
                                    3 * _bestPoints[0].Position
                                    - 2 * _bestPoints[1].Position, _f);
            _bestPoints[2] = _bestPoints[1];
            _bestPoints[1] = _bestPoints[0];
            _bestPoints[0] = newPoint;
        }

        /// Shift the best points toward positive positions.
        private void MoveTowardPositive()
        {
            OptimizingPoint newPoint = _pointFactory.CreatePoint(
                                        3 * _bestPoints[2].Position
                                        - 2 * _bestPoints[1].Position, _f);
            _bestPoints[0] = _bestPoints[1];
            _bestPoints[1] = _bestPoints[2];
            _bestPoints[2] = newPoint;
        }
    }
}
