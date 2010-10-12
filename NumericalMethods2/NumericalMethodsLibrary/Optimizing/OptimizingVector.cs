#region Using directives

using System.Text;

using NumericalMethods.Interfaces;

#endregion

namespace NumericalMethods.Optimization
{
    /// Vector & function holder used in optimizing many-variable functions.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public abstract class OptimizingVector
    {
	    /// Value of the function to optimize.
        private double _value;
	    /// Position at which the value was evaluated.
        private double[] _position;
	    /// Value of the function to optimize.
        protected IManyVariableFunction _f;

        /// Constructor method.
        /// @param v double[]
        /// @param f DhbInterfaces.IManyVariableFunction
        public OptimizingVector(double[] v, IManyVariableFunction func)
        {
            _position = v;
            _f = func;
            _value = _f.Value(_position);
        }

        /// @return boolean	true if the receiver is "better" than 
        ///												the supplied point
        /// @param point OptimizingVector
        public abstract bool BetterThan(OptimizingVector entity);

        /// (used by the Simplex algorithm).
        /// @param v double[]
        public void ContractFrom(double[] v)
        {
            for (int i = 0; i < _position.Length; i++)
            {
                _position[i] += v[i];
                _position[i] *= 0.5;
            }
            _value = _f.Value(_position);
        }

        /// @return double	the receiver's position
        public double[] Position
        {
            get { return _position; }
        }

        /// @return double	the value of the function
        ///										at the receiver's position
        public double Value
        {
            get { return _value; }
        }

        /// (used by method ToString)..
        /// @return string
        protected abstract string PrintedKey();

        /// @return string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_value);
            sb.Append(PrintedKey());
            for (int i = 0; i < _position.Length; i++)
            {
                sb.Append(' ');
                sb.Append(_position[i]);
            }
            return sb.ToString();
        }
    }
}
