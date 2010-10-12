#region Using directives

using NumericalMethods.Interfaces;
using NumericalMethods.Iterations;

#endregion

namespace NumericalMethods.Optimization
{
    /// Abstract optimizer of many-variable functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public abstract class MultiVariableOptimizer : IterativeProcess
    {
	    /// Value of the function to optimize.
        protected IManyVariableFunction _f;
	    /// Best value found so far: must be set to determine the dimension
	    /// of the argument of the function.
        protected double[] _result;
	    /// Optimizing strategy (minimum or maximum).
        protected OptimizingPointFactory _pointFactory;

        /// Constructor method.
        public MultiVariableOptimizer(IManyVariableFunction func,
                OptimizingPointFactory pointCreator, double[] initialValue)
        {
            _f = func;
            _pointFactory = pointCreator;
            this.InitialValue = initialValue;
        }

        /// @return double[]	result of the receiver
        public virtual double[] Result
        {
            get { return _result; }
        }

        /// @param v double[]	educated guess for the optimum's location
        public double[] InitialValue
        {
            set { _result = value; }
        }

        // TODO: Define a comparator for OptimizingPoints based on BetterThan()
        // so that we can use BCL's Array.Sort() method, instead of this one
        /// Use bubble sort to sort the best points
        protected void SortPoints(OptimizingVector[] bestPoints)
        {
            OptimizingVector temp;
            int n = bestPoints.Length;
            int bound = n - 1;
            int i, m;
            while (bound >= 0)
            {
                m = -1;
                for (i = 0; i < bound; i++)
                {
                    if (bestPoints[i + 1].BetterThan(bestPoints[i]))
                    {
                        temp = bestPoints[i];
                        bestPoints[i] = bestPoints[i + 1];
                        bestPoints[i + 1] = temp;
                        m = i;
                    }
                }
                bound = m;
            }
        }
    }
}
