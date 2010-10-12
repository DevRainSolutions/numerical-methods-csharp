#region Using directives

using System;
using System.Text;

#endregion

namespace NumericalMethods.MatrixAlgebra
{
    /// Vector implementation
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class DhbVector
    {
        protected double[] _components;

        internal double[] Components
        {
            get { return _components; }
        }

        /// Create a vector of given dimension.
        /// NOTE: The supplied array of components must not be changed.
        /// @param comp double[]
        public DhbVector(double[] comp)
        {
            if (comp.Length <= 0)
                throw new OverflowException("Vector components cannot be empty");
            _components = new double[comp.Length];
            comp.CopyTo(_components, 0);
        }

        /// Create a vector of given dimension.
        /// @param dimension int dimension of the vector; must be positive.
        public DhbVector(int dimension)
        {
            if (dimension <= 0)
                throw new ArgumentOutOfRangeException(
                                        "Requested vector size: " + dimension);
            _components = new double[dimension];
            //Clear();
        }

        /// @param x double[]
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension if the vector
        /// and supplied vector do not have the same dimension.
        public void Accumulate(double[] x)
        {
            if (_components.Length != x.Length)
                throw new DhbIllegalDimension("Attempt to add a "
                            + _components.Length + "-dimension vector to a "
                                            + x.Length + "-dimension array");
            for (int i = 0; i < _components.Length; i++)
                _components[i] += x[i];
        }

        /// @param v DhbMatrixAlgebra.DhbVector
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension if the vector
        /// and supplied vector do not have the same dimension.
        public void Accumulate(DhbVector v)
        {
            Accumulate(v._components);
        }

        /// @param x double[]
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension if the vector
        /// and supplied vector do not have the same dimension.
        public void AccumulateNegated(double[] x)
        {
            if (_components.Length != x.Length)
                throw new DhbIllegalDimension("Attempt to add a "
                                + _components.Length + "-dimension vector to a "
                                                + x.Length + "-dimension array");
            for (int i = 0; i < _components.Length; i++)
                _components[i] -= x[i];
        }

        /// @param v DhbMatrixAlgebra.DhbVector
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension if the vector
        /// and supplied vector do not have the same dimension.
        public void AccumulateNegated(DhbVector v)
        {
            AccumulateNegated(v._components);
        }

        /// @return DHBmatrixAlgebra.DhbVector sum of the vector with
        ///												the supplied vector
        /// @param v DHBmatrixAlgebra.DhbVector
        /// @exception DHBmatrixAlgebra.DhbIllegalDimension if the vector
        /// 				and supplied vector do not have the same dimension.
        public static DhbVector operator+(DhbVector w, DhbVector v)
        {
            int n = w.Dimension;
            if (n != v.Dimension)
                throw new DhbIllegalDimension("Attempt to add a "
                                + n + "-dimension vector to a "
                                        + v.Dimension + "-dimension vector");
            double[] newComponents = new double[n];
            for (int i = 0; i < n; i++)
                newComponents[i] = w._components[i] + v._components[i];
            return new DhbVector(newComponents);
        }

        /// Sets all components of the receiver to 0.
        public void Clear()
        {
            Array.Clear(_components, 0, _components.Length);
        }

        /// @return double
        /// @param n int
        public double this[int n]
        {
            get { return _components[n]; }
        }

        /// Returns the dimension of the vector.
        /// @return int
        public int Dimension
        {
            get { return _components.Length; }
        }

        /// @return true if the supplied vector is equal to the receiver
        /// @param v DHBmatrixAlgebra.DhbVector
        public bool Equals(DhbVector v)
        {
            if (v.Dimension != _components.Length)
                return false;
            for (int i = 0; i < _components.Length; i++)
            {
                if (v._components[i] != _components[i])
                    return false;
            }
            return true;
        }

        public override bool Equals(object o)
        {
            return (o is DhbVector) ? this.Equals(o as DhbVector) : false;
        }


        public override int GetHashCode()
        {
            return _components.GetHashCode();
        }


        public static bool operator==(DhbVector w, DhbVector v)
        {
            return w.Equals(v);
        }

        public static bool operator!=(DhbVector w, DhbVector v)
        {
            return !w.Equals(v);
        }


        /// Computes the norm of a vector.
        public double Norm
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < _components.Length; i++)
                    sum += _components[i] * _components[i];
                return Math.Sqrt(sum);
            }
        }

        /// @param x double
        public DhbVector NormalizedBy(double x)
        {
            for (int i = 0; i < _components.Length; i++)
                _components[i] /= x;
            return this;
        }

        /// Computes the product of the vector by a number.
        /// @return DHBmatrixAlgebra.DhbVector
        /// @param d double
        public static DhbVector operator*(DhbVector w, double d)
        {
            double[] newComponents = new double[w.Dimension];
            for (int i = 0; i < w.Dimension; i++)
                newComponents[i] = d * w._components[i];
            return new DhbVector(newComponents);
        }

        /// Compute the scalar product (or dot product) of two vectors.
        /// @return double the scalar product of the receiver with the argument
        /// @param v DHBmatrixAlgebra.DhbVector
        /// @exception DHBmatrixAlgebra.DhbIllegalDimension if the dimension
        ///												of v is not the same.
        public static double operator*(DhbVector w, DhbVector v)
        {
            if (w.Dimension != v.Dimension)
                throw new DhbIllegalDimension(
                            "Dot product with mismatched dimensions: "
                            + w.Dimension + ", " + v.Dimension);
            return w.SecureProduct(v);
        }

        /// Computes the product of the transposed vector with a matrix
        /// @return MatrixAlgebra.DhbVector
        /// @param a MatrixAlgebra.Matrix
        public static DhbVector operator*(DhbVector w, Matrix a)
        {
            int n = a.Rows;
            int m = a.Columns;
            if (w.Dimension != n)
                throw new DhbIllegalDimension(
                            "Product error: transposed of a " + w.Dimension
                            + "-dimension vector cannot be multiplied with a "
                                                    + n + " by " + m + " matrix");
            return w.SecureProduct(a);
        }

        /// @param x double
        public DhbVector ScaledBy(double x)
        {
            for (int i = 0; i < _components.Length; i++)
                _components[i] *= x;
            return this;
        }

        /// Compute the scalar product (or dot product) of two vectors.
        /// No dimension checking is made.
        /// @return double the scalar product of the receiver with the argument
        /// @param v DHBmatrixAlgebra.DhbVector
        internal protected double SecureProduct(DhbVector v)
        {
            double sum = 0;
            for (int i = 0; i < _components.Length; i++)
                sum += _components[i] * v._components[i];
            return sum;
        }

        /// Computes the product of the transposed vector with a matrix
        /// @return MatrixAlgebra.DhbVector
        /// @param a MatrixAlgebra.Matrix
        internal protected DhbVector SecureProduct(Matrix a)
        {
            int n = a.Rows;
            int m = a.Columns;
            double[] vectorComponents = new double[m];
            for (int j = 0; j < m; j++)
            {
                //vectorComponents[j] = 0; // not necessary as new double[m] sets all values to zero
                for (int i = 0; i < n; i++)
                    vectorComponents[j] += _components[i] * a.Components[i, j];
            }
            return new DhbVector(vectorComponents);
        }

        /// @return DHBmatrixAlgebra.DhbVector	subtract the supplied vector
        ///													to the receiver
        /// @param v DHBmatrixAlgebra.DhbVector
        /// @exception DHBmatrixAlgebra.DhbIllegalDimension if the vector
        /// and supplied vector do not have the same dimension.
        public static DhbVector operator-(DhbVector w, DhbVector v)
        {
            if (w.Dimension != v.Dimension)
                throw new DhbIllegalDimension("Attempt to add a "
                                + w.Dimension + "-dimension vector to a "
                                        + v.Dimension + "-dimension vector");
            double[] newComponents = new double[w.Dimension];
            for (int i = 0; i < w.Dimension; i++)
                newComponents[i] = w._components[i] - v._components[i];
            return new DhbVector(newComponents);
        }

        /// @return MatrixAlgebra.Matrix	tensor product with the specified
        ///																vector
        /// @param v MatrixAlgebra.DhbVector	second vector to build tensor
        ///														product with.
        public Matrix TensorProduct(DhbVector v)
        {
            int n = _components.Length;
            int m = v.Dimension;
            double[,] newComponents = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    newComponents[i, j] = _components[i] * v._components[j];
            }
            return n == m ? new SymmetricMatrix(newComponents)
                                  : new Matrix(newComponents);
        }

        /// @return double[]	a copy of the components of the receiver.
        public double[] ToComponents()
        {
            double[] answer = new double[_components.Length];
            _components.CopyTo(answer, 0);
            return answer;
        }

        /// Returns a string representation of the vector.
        /// @return string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            char[] separator = { '[', ' ' };
            for (int i = 0; i < _components.Length; i++)
            {
                sb.Append(separator);
                sb.Append(_components[i]);
                if (i == 0) separator[0] = ',';
            }
            sb.Append(']');
            return sb.ToString();
        }
    }
}
