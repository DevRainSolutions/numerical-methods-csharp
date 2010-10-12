#region Using directives

using System;
using System.Text;
using NumericalMethods.Interfaces;
using NumericalMethods.DhbFunctionEvaluation;
using NumericalMethods.Iterations;

#endregion

namespace NumericalMethods.Statistics
{
    /// Subclasses of this class represent probability density function.
    /// The value of the funtion f (x) represents the probability that a
    /// continuous random variable takes values in the interval [x, x+dx[.
    /// A norm is defined for the case where the function is overlayed over
    /// a set of experimental points or a histogram.
    /// 
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public abstract class ProbabilityDensityFunction : IParametrizedOneVariableFunction
    {
        public abstract double Value(double x);

        public abstract double[] Parameters { get; set; }

        /// Random generator needed if random numbers are needed
        /// (lazy initialization used).
        private RandomGenerator _generator = null;

        /// Compute an approximation of the gradient.
        /// @return double[]
        /// @param x double
        public double[] ApproximateValueAndGradient(double x)
        {
            double temp, delta;
            double[] localParams = this.Parameters;
            double[] answer = new double[localParams.Length + 1];
            answer[0] = this.Value(x);
            for (int i = 0; i < localParams.Length; i++)
            {
                temp = localParams[i];
                delta = Math.Abs(temp) > DhbMath.DefaultNumericalPrecision
                                ? 0.0001 * temp : 0.0001;
                localParams[i] += delta;
                this.Parameters = localParams;
                answer[i + 1] = (this.Value(x) - answer[0]) / delta;
                localParams[i] = temp;
            }
            this.Parameters = localParams;
            return answer;
        }

        /// @return double average of the distribution.
        public abstract double Average { get; set;}

        /// Returns the probability of finding a random variable smaller than
        /// or equal to x.
        /// This method assumes that the probability density is 0 for x < 0.
        /// If this is not the case, the subclass must implement this method.
        /// @return integral of the probability density function from 0 to x.
        /// @param x double upper limit of intergral.
        public abstract double DistributionValue(double x);

        /// Returns the probability of finding a random variable between x1 and x2.
        /// Computing is made using the method distributionValue(x).
        /// This method should be used by distributions whose distributionValue
        /// is computed using a method overiding the default one.
        /// @return double integral of the probability density function from x1 to x2.
        /// @param x1 double lower limit of intergral.
        /// @param x2 double upper limit of intergral.
        public virtual double DistributionValue(double x1, double x2)
        {
            return DistributionValue(x2) - DistributionValue(x1);
        }

        /// @return RandomGenerator a random number generator that has seed 
        protected RandomGenerator Generator
        {
            get
            {
                if (this._generator == null)
                    this._generator = new RandomGenerator();
                return this._generator;
            }
        }

        /// @return double the value for which the distribution function is
        /// equal to x.
        /// @param x double value of the distribution function.
        /// @exception ArgumentOutOfRangeException
        ///							if the argument is not between 0 and 1.
        public double InverseDistributionValue(double x)
        {
            if (x < 0 || x > 1)
                throw new ArgumentOutOfRangeException("argument must be between 0 and 1");
            return PrivateInverseDistributionValue(x);
        }

        /// @return double kurtosis of the distribution.
        public virtual double Kurtosis
        {
            get { return double.NaN; }
        }

        /// @return string the name of the distribution.
        public abstract String Name { get; }

        /// This method assumes that the range of the argument has been checked.
        /// Computation is made using the Newton zero finder.
        /// @return double the value for which the distribution function
        ///													is equal to x.
        /// @param x double value of the distribution function.
        protected virtual double PrivateInverseDistributionValue(double x)
        {
            OffsetDistributionFunction distribution = new OffsetDistributionFunction(this, x);
            NewtonZeroFinder zeroFinder = new NewtonZeroFinder(distribution, this, this.Average);
            zeroFinder.DesiredPrecision = DhbMath.DefaultNumericalPrecision;
            zeroFinder.Evaluate();
            return zeroFinder.Result;
        }

        /// @return double a random number distributed according to the receiver.
        public virtual double Random()
        {
            return PrivateInverseDistributionValue(this.Generator.NextDouble());
        }

        /// @return double skewness of the distribution.
        public virtual double Skewness
        {
            get { return double.NaN; }
        }

        /// NOTE: subclass MUST implement one of the two method variance
        ///		 or standardDeviation.
        /// @return double standard deviation of the distribution from the variance.
        public virtual double StandardDeviation
        {
            get { return Math.Sqrt(Variance); }
            set { throw new InvalidOperationException("Can not set the standard deviation value"); }
        }

        /// @return string name and parameters of the distribution.
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(this.Name);
            char[] separator = { '(', ' ' };
            double[] parameters = this.Parameters;
            for (int i = 0; i < parameters.Length; i++)
            {
                sb.Append(separator);
                sb.Append(parameters[i]);
                if (i == 0)
                    separator[0] = ',';
            }
            sb.Append(')');
            return sb.ToString();
        }

        /// Evaluate the distribution and the gradient of the distribution with respect
        /// to the parameters.
        /// @return double[]	0: distribution's value, 1,2,...,n distribution's gradient
        /// @param x double
        public virtual double[] ValueAndGradient(double x)
        {
            return ApproximateValueAndGradient(x);
        }

        /// NOTE: subclass MUST implement one of the two method variance
        ///		 or standardDeviation.
        /// @return double variance of the distribution from the standard deviation.
        public virtual double Variance
        {
            get
            {
                double v = this.StandardDeviation;
                return v * v;
            }
        }
    }
}
