#region Using directives

using System;

#endregion

namespace NumericalMethods.Optimization
{
    /// Abstract chromosome manager.
    /// (genetic algorithm)
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public abstract class ChromosomeManager
    {
        /// Population size.
        private int _populationSize = 100;
        /// Rate of mutation. 
        private double _rateOfMutation = 0.1;
        /// Rate of crossover. 
        private double _rateOfCrossover = 0.1;
        /// Random generator. 
        private readonly Random _generator = new Random();

        /// Constructor method.
        public ChromosomeManager() : base()
        {
        }

        /// Constructor method.
        /// @param n int
        /// @param mRate double
        /// @param cRate double
        public ChromosomeManager(int n, double mRate, double cRate)
        {
            _populationSize = n;
            _rateOfMutation = mRate;
            _rateOfCrossover = cRate;
        }

        /// @param x object
        public abstract void AddCloneOf(object x);

        /// @param x object
        public abstract void AddCrossoversOf(object x, object y);

        /// @param x object
        public abstract void AddMutationOf(Object x);

        public abstract void AddRandomChromosome();

        /// @return int	the current size of the population
        public abstract int CurrentPopulationSize { get;}

        /// @return int	desired population size.
        public int PopulationSize
        {
            get { return _populationSize; }
            set { _populationSize = value; }
        }

        /// @return object (must be casted into the proper type
        ///								of chromosome)
        /// @param n int
        public abstract object IndividualAt(int n);

        /// @return boolean	true if the new generation is complete
        public bool IsFullyPopulated
        {
            get { return this.CurrentPopulationSize >= _populationSize; }
        }

        /// @return double	a random number (delegated to the generator)
        public double NextDouble()
        {
            return _generator.NextDouble();
        }

        /// @param x object
        /// @param y object
        public void Process(object x, object y)
        {
            double roll = _generator.NextDouble();
            if (roll < _rateOfCrossover)
                AddCrossoversOf(x, y);
            else if (roll < _rateOfCrossover + _rateOfMutation)
            {
                AddMutationOf(x);
                AddMutationOf(y);
            }
            else
            {
                AddCloneOf(x);
                AddCloneOf(y);
            }
        }

        /// Create a population of random chromosomes.
        public void RandomizePopulation()
        {
            Reset();
            while (!this.IsFullyPopulated)
                AddRandomChromosome();
        }

        /// Reset the population of the receiver.
        public abstract void Reset();

        /// @param n int	desired rate of crossover
        public int RateOfCrossover
        {
            set { _rateOfCrossover = value; }
        }

        /// @param n int	desired rate of mutation
        public int RateOfMutation
        {
            set { _rateOfMutation = value; }
        }
    }
}
