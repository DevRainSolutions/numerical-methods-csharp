#region Using directives

using System;

using NumericalMethods.DhbFunctionEvaluation;

#endregion

namespace NumericalMethods.Iterations
{
    /// An iterative process is a general structure managing iterations.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public abstract class IterativeProcess
    {
        /// Number of iterations performed.
        private int _iterations;
        /// Maximum allowed number of iterations.
        private int _maximumIterations = 50;
        /// Desired precision.
        private double _desiredPrecision = DhbMath.DefaultNumericalPrecision;
        /// Achieved precision.
        private double _precision;

        /// Performs the iterative process.
        /// Note: this method does not return anything because Java does not
        /// allow mixing double, int, or objects
        public void Evaluate()
        {
            _iterations = 0;
            InitializeIterations();
            while (_iterations++ < _maximumIterations)
            {
                _precision = EvaluateIteration();
                if (HasConverged)
                    break;
            }
            FinalizeIterations();
        }

        /// Evaluate the _result of the current interation.
        /// @return the estimated precision of the _result.
        abstract public double EvaluateIteration();

        /// Perform eventual clean-up operations
        /// (mustbe implement by subclass when needed).
        public virtual void FinalizeIterations()
        {
        }

        /// Returns the desired precision.
        public double DesiredPrecision
        {
            get { return _desiredPrecision; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException
                                            ("Non-positive precision: " + value);
                _desiredPrecision = value;
            }
        }

        /// Returns the number of iterations performed.
        public int Iterations
        {
            get { return _iterations; }
        }

        /// Returns the maximum allowed number of iterations.
        public int MaximumIterations
        {
            get { return _maximumIterations; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException
                                    ("Non-positive maximum iteration: " + value);
                _maximumIterations = value;
            }
        }

        /// Returns the attained precision.
        public double Precision
        {
            get { return _precision; }
        }

        /// Check to see if the _result has been attained.
        /// @return boolean
        public bool HasConverged
        {
            get { return _precision < _desiredPrecision; }
        }

        /// Initializes internal parameters to start the iterative process.
        public virtual void InitializeIterations()
        {
        }

        /// @return double
        /// @param epsilon double
        /// @param x double
        public double RelativePrecision(double epsilon, double x)
        {
            return x > DhbMath.DefaultNumericalPrecision
                                                    ? epsilon / x : epsilon;
        }

    }
}
