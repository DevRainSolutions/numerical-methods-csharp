#region Using directives

using System;

using NumericalMethods.MatrixAlgebra;

#endregion

namespace NumericalMethods.Optimization
{
    /// Chromosome manager for vector chromosomes.
    /// (genetic algorithm)
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class VectorChromosomeManager : ChromosomeManager
    {
	    /// Population.
        private DhbVector[] _population;
	    /// Current population size.
        private int _fillIndex;
	    /// Origin of values.
        private DhbVector _origin;
	    /// Range of values.
        private DhbVector _range;

        /// Default constructor method.
        public VectorChromosomeManager() : base()
        {
        }

        /// Constructor method.
        /// @param n int
        /// @param mRate double
        /// @param cRate double
        public VectorChromosomeManager(int n, double mRate, double cRate)
            : base(n, mRate, cRate)
        {
        }

        /// @param x DhbVector
        public override void AddCloneOf(object x)
        {
            double[] v = ((DhbVector)x).ToComponents();
            try { _population[_fillIndex++] = new DhbVector(v); }
            catch (OverflowException) { } ;
        }

        /// @param x DhbVector
        /// @param y DhbVector
        public override void AddCrossoversOf(object x, object y)
        {
            double[] v = ((DhbVector)x).ToComponents();
            double[] w = ((DhbVector)x).ToComponents();
            int n = (int)(this.NextDouble() * (_origin.Dimension - 1));
            for (int i = 0; i < n; i++)
            {
                double temp = v[i];
                v[i] = w[i];
                w[i] = temp;
            }
            try
            {
                _population[_fillIndex++] = new DhbVector(v);
                _population[_fillIndex++] = new DhbVector(w);
            }
            catch (OverflowException) { } ;
        }

        /// @param x DhbVector
        public override void AddMutationOf(object x)
        {
            double[] v = ((DhbVector)x).ToComponents();
            int i = (int)(this.NextDouble() * _origin.Dimension);
            v[i] = RandomComponent(i);
            try { _population[_fillIndex++] = new DhbVector(v); }
            catch (OverflowException) { } ;
        }

        public override void AddRandomChromosome()
        {
            double[] v = new double[_origin.Dimension];
            for (int i = 0; i < _origin.Dimension; i++)
                v[i] = RandomComponent(i);
            try { _population[_fillIndex++] = new DhbVector(v); }
            catch (OverflowException) { } ;
        }

        /// @return int	the current size of the population
        public override int CurrentPopulationSize
        {
            get { return _fillIndex; }
        }

        /// @return Vector	vector at given index
        /// @param n int
        public override object IndividualAt(int n)
        {
            return _population[n];
        }

        /// @return double
        /// @param n int
        private double RandomComponent(int n)
        {
            return _origin[n] + this.NextDouble() * _range[n];
        }

        /// Allocated memory for a new generation.
        public override void Reset()
        {
            _population = new DhbVector[this.PopulationSize];
            _fillIndex = 0;
        }

        /// @param x double	component of the origin of the hypercube
        ///	constraining the domain of definition of the function to optimize
        /// @exception OverflowException
        ///						when the size of the array is 0
        public void SetOrigin(double[] x)
        {
            this._origin = new DhbVector(x);
        }

        /// @param v DhbVector 	origin of the hypercube
        ///	constraining the domain of definition of the function to optimize
        public DhbVector Origin
        {
            set { _origin = value; }
        }

        /// @param x double	components of the lengths of the hypercube
        ///	constraining the domain of definition of the function to optimize
        /// @exception OverflowException
        ///						when the size of the array is 0
        public void SetRange(double[] x)
        {
            this.Range = new DhbVector(x);
        }

        /// @param v DhbVector	lengths of the hypercube
        ///	constraining the domain of definition of the function to optimize
        public DhbVector Range
        {
            set { _range = value; }
        }
    }
}
