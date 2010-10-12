#region Using directives

using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Optimization
{
    /// Abstract genetic algorithm.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public abstract class GeneticOptimizer : MultiVariableOptimizer
    {
	    /// Chromosome manager.
        private ChromosomeManager _chromosomeManager;

        /// Constructor method.
        /// @param func IManyVariableFunction
        /// @param pointCreator DhbOptimizing.OptimizingPointFactory
        /// @param chrManager ChromosomeManager
        public GeneticOptimizer(IManyVariableFunction func,
            OptimizingPointFactory pointCreator, ChromosomeManager chrManager)
                    : base(func, pointCreator, null)
        {
            _chromosomeManager = chrManager;
        }

        /// @param x object
        public abstract void CollectPoint(object x);

        /// Collect points for the entire population.
        public void CollectPoints()
        {
            Reset();
            for (int i = 0; i < _chromosomeManager.PopulationSize; i++)
                CollectPoint(_chromosomeManager.IndividualAt(i));
        }

        /// This method causes the receiver to exhaust the maximum number of
        /// iterations. It may be overloaded by a subclass (hence "protected")
        /// if a convergence criteria can be defined.
        /// @return double
        protected double ComputePrecision()
        {
            return 1d;
        }

        /// @return double
        public override double EvaluateIteration()
        {
            double[] randomScale = RandomScale();
            _chromosomeManager.Reset();
            while (!_chromosomeManager.IsFullyPopulated)
            {
                _chromosomeManager.Process(
                                    IndividualAt(RandomIndex(randomScale)),
                                    IndividualAt(RandomIndex(randomScale)));
            }
            CollectPoints();
            return ComputePrecision();
        }

        /// @return object	(must be casted into the proper type)
        /// @param n int
        public abstract object IndividualAt(int n);

        /// Create a random population.
        public override void InitializeIterations()
        {
            InitializeIterations(_chromosomeManager.PopulationSize);
            _chromosomeManager.RandomizePopulation();
            CollectPoints();
        }

        /// @param n int	size of the initial population
        public abstract void InitializeIterations(int n);

        /// @return int	an index generated randomly
        /// @param randomScale double[]	fitness scale (integral)
        protected int RandomIndex(double[] randomScale)
        {
            double roll = _chromosomeManager.NextDouble();
            if (roll < randomScale[0])
                return 0;
            int n = 0;
            int m = randomScale.Length;
            while (n < m - 1)
            {
                int k = (n + m) / 2;
                if (roll < randomScale[k])
                    m = k;
                else
                    n = k;
            }
            return m;
        }

        /// @return double[]	integral fitness scale.
        public abstract double[] RandomScale();

        public abstract void Reset();
    }
}
