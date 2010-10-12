#region Using directives

using System;

using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Optimization
{
    /// Multi-strategy optimizer of many-variable functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class MultiVariableGeneralOptimizer : MultiVariableOptimizer
    {
	    /// Initial range for random search.
        protected double[] _range;

        /// Constructor method.
        /// @param func IManyVariableFunction
        /// @param pointCreator DhbOptimizing.OptimizingPointFactory
        /// @param initialValue double[]
        public MultiVariableGeneralOptimizer(IManyVariableFunction func,
                            OptimizingPointFactory pointCreator, double[] initialValue)
                : base(func, pointCreator, initialValue)
        {
        }

        public override double EvaluateIteration()
        {
            HillClimbingOptimizer finder = new HillClimbingOptimizer(_f, _pointFactory,
                                                                                _result);
            finder.DesiredPrecision = this.DesiredPrecision;
            finder.MaximumIterations = this.MaximumIterations;
            finder.Evaluate();
            _result = finder.Result;
            return finder.Precision;
        }

        public override void InitializeIterations()
        {
            if (_range != null)
                PerformGeneticOptimization();
            PerformSimplexOptimization();
        }

        private void PerformGeneticOptimization()
        {
            VectorChromosomeManager manager = new VectorChromosomeManager();
            manager.SetRange(_range);
            manager.SetOrigin(_result);
            VectorGeneticOptimizer finder = new VectorGeneticOptimizer(_f, _pointFactory, manager);
            finder.Evaluate();
            _result = finder.Result;
        }

        private void PerformSimplexOptimization()
        {
            SimplexOptimizer finder = new SimplexOptimizer(_f, _pointFactory, _result);
            finder.DesiredPrecision = Math.Sqrt(this.DesiredPrecision);
            finder.MaximumIterations = this.MaximumIterations;
            finder.Evaluate();
            _result = finder.Result;
        }

        /// @param x double	component of the origin of the hypercube
        ///				constraining the domain of definition of the function
        public double[] Origin
        {
            set { _result = value; }
        }

        /// @param x double	components of the lengths of the hypercube
        ///				constraining the domain of definition of the function
        public double[] Range
        {
            set { _range = value; }
        }
    }
}
