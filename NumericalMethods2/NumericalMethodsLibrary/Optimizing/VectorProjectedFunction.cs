#region Using directives

using NumericalMethods.Interfaces;
using NumericalMethods.MatrixAlgebra;

#endregion

namespace NumericalMethods.Optimization
{
    /// Projection of a many-variable function 
    ///								onto a one-dimensional direction.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class VectorProjectedFunction : IOneVariableFunction
    {
	    /// Value of the function to optimize.
        private IManyVariableFunction _f;
	    /// Origin for function evaluation.
        private DhbVector _origin;
	    /// Direction along which the function is evaluated.
        private DhbVector _direction;

        /// Constructor method.
        /// @param func IManyVariableFunction	function to project
        /// @param x double[]	origin of projected function
        /// @param d double[]	direction of projection
        /// @exception OverflowException if dimension of x or d is 0.
        public VectorProjectedFunction(IManyVariableFunction func,
                                                    double[] x, double[] d)
        {
            _f = func;
            SetOrigin(x);
            SetDirection(d);
        }

        /// Constructor method.
        /// @param func IManyVariableFunction	function to project
        /// @param x DhbVector	origin of projected function
        /// @param d DhbVector	direction of projection
        public VectorProjectedFunction(IManyVariableFunction func,
                                                    DhbVector x, DhbVector d)
        {
            _f = func;
            this.Origin = x;
            this.Direction = d;
        }

        /// @param x double[]	origin of projected function
        /// @exception DhbIllegalDimension
        ///						if dimension of x is not that of the origin.
        public DhbVector ArgumentAt(double x)
        {
            DhbVector v = _direction * x;
            v.Accumulate(_origin);
            return v;
        }

        /// @return DhbMatrixAlgebra.DhbVector	direction of the receiver
        public DhbVector Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// @return DhbMatrixAlgebra.DhbVector	origin of the receiver
        public DhbVector Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        /// @param v DhbMatrixAlgebra.DhbVector
        /// @exception OverflowException if dimension of v is 0.
        public void SetDirection(double[] v)
        {
            _direction = new DhbVector(v);
        }

        /// @param v DhbMatrixAlgebra.DhbVector
        /// @exception OverflowException if dimension of v is 0.
        public void SetOrigin(double[] v)
        {
            _origin = new DhbVector(v);
        }

        /// Returns a String that represents the value of this object.
        /// @return a string representation of the receiver
        public override string ToString()
        {
            return string.Format("{0} -> {1}", _origin, _direction);
        }

        /// @return double	value of the function
        /// @param x double	distance from the origin in unit of direction. 
        public double Value(double x)
        {
            try
            {
                return _f.Value(ArgumentAt(x).ToComponents());
            }
            catch (DhbIllegalDimension) { return double.NaN; }
        }
    }
}
