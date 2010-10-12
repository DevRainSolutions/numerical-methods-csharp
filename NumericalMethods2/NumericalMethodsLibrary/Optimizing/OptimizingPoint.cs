#region Using directives

using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Optimization
{
    ///
    /// Point & function holder used in optimizing one-variable functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public abstract class OptimizingPoint
    {
	    /// Value of the function to optimize.
        private double _value;
	    /// Position at which the value was evaluated.
        private double _position;

        /// Constructor method
        /// @param x double	position at which the goal function is evaluated.
        /// @param f IOneVariableFunction	function to optimize.
        public OptimizingPoint(double x, IOneVariableFunction f)
        {
            _position = x;
            _value = f.Value(x);
        }

        /// @return boolean	true if the receiver is "better" than 
        ///												the supplied point
        /// @param point OptimizingPoint
        public abstract bool BetterThan(OptimizingPoint entity);

        /// @return double	the receiver's position
        public double Position
        {
            get { return _position; }
        }

        /// @return double	the value of the function at the receiver's
        ///															position
        public double Value
        {
            get { return _value; }
        }

        /// (used by method ToString).
        /// @return string
        protected abstract string PrintedKey();

        /// @return string
        public override string ToString()
        {
            return string.Format("{0}{1}{2}", _value, this.PrintedKey(), _position);
        }
    }
}
