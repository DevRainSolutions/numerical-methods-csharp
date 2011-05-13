#region Using directives

using System;
using System.Text;

#endregion

namespace NumericalMethods.MatrixAlgebra
{
    /// Class representing matrix
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class Matrix
    {
        protected double[,] _components;
        protected LUPDecomposition _lupDecomposition = null;

        internal double[,] Components
        {
            get { return _components; }
        }

        /// Creates a matrix with given components.
        /// NOTE: the components must not be altered after the definition.
        /// @param a double[,]
        public Matrix(double[,] a)
        {
            _components = a;
        }

        /// Creates a null matrix of given dimensions.
        /// @param n int	number of rows
        /// @param m int	number of columns
        /// @exception NegativeArraySizeException
        public Matrix(int n, int m)
        {
            if (n <= 0 || m <= 0)
                throw new IndexOutOfRangeException(
                                        "Requested matrix size: " + n + " by " + m);
            _components = new double[n, m];
            Clear();
        }

        /// @param a MatrixAlgebra.Matrix
        /// @exception MatrixAlgebra.DhbIllegalDimension if the supplied matrix
        ///									does not have the same dimensions.
        public void Accumulate(Matrix a)
        {
            if (a.Rows != this.Rows || a.Columns != this.Columns)
                throw new DhbIllegalDimension("Operation error: cannot add a"
                                        + a.Rows + " by " + a.Columns
                                            + " matrix to a " + this.Rows + " by "
                                                        + this.Columns + " matrix");

            int m = _components.GetLength(1);
            for (int i = 0; i < _components.GetLength(0); i++)
            {
                for (int j = 0; j < m; j++)
                    _components[i, j] += a[i, j];
            }
        }

        /// @return MatrixAlgebra.Matrix		sum of the receiver with the
        ///													supplied matrix.
        /// @param a MatrixAlgebra.Matrix
        /// @exception MatrixAlgebra.DhbIllegalDimension if the supplied matrix
        ///									does not have the same dimensions.
//        public Matrix Add(Matrix a)
//        {
//            if (a.Rows != rows() || a.columns() != columns())
//                throw new DhbIllegalDimension("Operation error: cannot add a"
//                                            + a.Rows + " by " + a.columns()
//                                                + " matrix to a " + rows() + " by "
//                                                        + columns() + " matrix");
//            return new Matrix(addComponents(a));
//        }

        /// @return MatrixAlgebra.Matrix		sum of the receiver with the
        ///													supplied matrix.
        /// @param a MatrixAlgebra.Matrix
        /// @exception MatrixAlgebra.DhbIllegalDimension if the supplied matrix
        ///									does not have the same dimensions.
        public static Matrix operator +(Matrix b, Matrix a)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new DhbIllegalDimension("Operation error: cannot add a"
                                            + a.Rows + " by " + a.Columns
                                                + " matrix to a " + b.Rows + " by "
                                                        + b.Columns + " matrix");
            return new Matrix(b.AddComponents(a));
        }

        /// Computes the components of the sum of the receiver and
        ///													a supplied matrix.
        /// @return double[,]
        /// @param a MatrixAlgebra.Matrix
        protected double[,] AddComponents(Matrix a)
        {
            int n = this.Rows;
            int m = this.Columns;
            double[,] newComponents = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    newComponents[i, j] = _components[i, j] + a._components[i, j];
            }
            return newComponents;
        }

        public void Clear()
        {
            Array.Clear(_components, 0, _components.Length);
        }

        /// @return int	the number of columns of the matrix
        public int Columns
        {
            get { return _components.GetLength(1); }
        }

        /// @return double
        /// @param n int
        /// @param m int
        public double this[int n, int m]
        {
            get { return _components[n, m]; }
            set { _components[n, m] = value; }
        }

        /// @return double
        /// @exception MatrixAlgebra.DhbIllegalDimension if the supplied 
        ///												matrix is not square.
        public double Determinant()
        {
            return LupDecomposition().Determinant();
        }

        public static bool operator ==(Matrix b, Matrix a)
        {
            return b.Equals(a);
        }

        public static bool operator !=(Matrix b, Matrix a)
        {
            return !b.Equals(a);
        }


        public override bool Equals(object obj)
        {
            return obj is Matrix ? this.Equals(obj as Matrix) : false;
        }


        public override int GetHashCode()
        {
            return this._components.GetHashCode();
        }

        /// @return true if the supplied matrix is equal to the receiver.
        /// @param a MatrixAlgebra.Matrix
        public bool Equals(Matrix a)
        {
            int n = this.Rows;
            if (a.Rows != n)
                return false;
            int m = this.Columns;
            if (a.Columns != m)
                return false;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a._components[i, j] != _components[i, j])
                        return false;
                }
            }
            return true;
        }

        /// @return DhbMatrixAlgebra.DhbMatrix		inverse of the receiver
        ///				or pseudoinverse if the receiver is not a square matrix.
        /// @exception ArithmeticException if the receiver is
        ///													a singular matrix.
        public virtual Matrix Inverse()
        {
            try
            {
                return new Matrix(LupDecomposition().InverseMatrixComponents());
            }
            catch (DhbIllegalDimension)
            {
                return new Matrix(
                     TransposedProduct().Inverse()
                             .ProductWithTransposedComponents(this));
            }
        }

        /// @return boolean
        public bool IsSquare
        {
            get { return this.Rows == this.Columns; }
        }

        /// @return LUPDecomposition	the LUP decomposition of the receiver.
        /// @exception DhbIllegalDimension if the receiver is not
        ///													a square matrix.
        protected LUPDecomposition LupDecomposition()
        {
            if (_lupDecomposition == null)
                _lupDecomposition = new LUPDecomposition(this);
            return _lupDecomposition;
        }

        /// @return MatrixAlgebra.Matrix		product of the matrix with
        ///													a supplied number
        /// @param a double	multiplicand.
        public static Matrix operator*(Matrix b, double a)
        {
            return new Matrix(b.ProductComponents(a));
        }

        /// Computes the product of the matrix with a vector.
        /// @return DHBmatrixAlgebra.DhbVector
        /// @param v DHBmatrixAlgebra.DhbVector
        public static DhbVector operator*(Matrix b, DhbVector v)
        {
            int n = b.Rows;
            int m = b.Columns;
            if (v.Dimension != m)
                throw new DhbIllegalDimension("Product error: " + n + " by " + m
                    + " matrix cannot by multiplied with vector of dimension "
                                                            + v.Dimension);
            return b.SecureProduct(v);
        }

        /// @return MatrixAlgebra.Matrix		product of the receiver with the
        ///													supplied matrix
        /// @param a MatrixAlgebra.Matrix
        /// @exception MatrixAlgebra.DhbIllegalDimension If the number of
        ///					columns of the receiver are not equal to the
        ///								number of rows of the supplied matrix.
        public static Matrix operator*(Matrix b, Matrix a)
        {
            if (a.Rows != b.Columns)
                throw new DhbIllegalDimension(
                                    "Operation error: cannot multiply a"
                                            + b.Rows + " by " + b.Columns
                                                + " matrix with a " + a.Rows
                                                    + " by " + a.Columns
                                                                + " matrix");
            return new Matrix(b.ProductComponents(a));
        }

        /// @return double[,]
        /// @param a double
        protected double[,] ProductComponents(double a)
        {
            int n = this.Rows;
            int m = this.Columns;
            double[,] newComponents = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    newComponents[i, j] = a * _components[i, j];
            }
            return newComponents;
        }

        /// @return double[,]	the components of the product of the receiver
        ///											with the supplied matrix
        /// @param a MatrixAlgebra.Matrix
        protected double[,] ProductComponents(Matrix a)
        {
            int p = this.Columns;
            int n = this.Rows;
            int m = a.Columns;
            double[,] newComponents = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < p; k++)
                        sum += _components[i, k] * a._components[k, j];
                    newComponents[i, j] = sum;
                }
            }
            return newComponents;
        }

        /// @return MatrixAlgebra.Matrix	product of the receiver with the
        ///									tranpose of the supplied matrix
        /// @param a MatrixAlgebra.Matrix
        /// @exception MatrixAlgebra.DhbIllegalDimension If the number of
        ///						columns of the receiver are not equal to
        ///						the number of columns of the supplied matrix.
        public Matrix ProductWithTransposed(Matrix a)
        {
            if (a.Columns != this.Columns)
                throw new DhbIllegalDimension(
                                "Operation error: cannot multiply a " + this.Rows
                                    + " by " + this.Columns
                                        + " matrix with the transpose of a "
                                            + a.Rows + " by " + a.Columns
                                                                + " matrix");
            return new Matrix(ProductWithTransposedComponents(a));
        }

        /// @return double[][]	the components of the product of the receiver
        ///							with the transpose of the supplied matrix
        /// @param a MatrixAlgebra.Matrix
        internal protected double[,] ProductWithTransposedComponents(Matrix a)
        {
            int p = this.Columns;
            int n = this.Rows;
            int m = a.Rows;
            double[,] newComponents = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < p; k++)
                        sum += _components[i, k] * a._components[j, k];
                    newComponents[i, j] = sum;
                }
            }
            return newComponents;
        }

        /// @return int	the number of rows of the matrix
        public int Rows
        {
            get { return _components.GetLength(0); }
        }

        /// Computes the product of the matrix with a vector.
        /// @return DHBmatrixAlgebra.DhbVector
        /// @param v DHBmatrixAlgebra.DhbVector
        internal protected DhbVector SecureProduct(DhbVector v)
        {
            int n = this.Rows;
            int m = this.Columns;
            double[] vectorComponents = new double[n];
            for (int i = 0; i < n; i++)
            {
                vectorComponents[i] = 0;
                for (int j = 0; j < m; j++)
                    vectorComponents[i] += _components[i, j] * v.Components[j];
            }
            return new DhbVector(vectorComponents);
        }

        /// Same as product(Matrix a), but without dimension checking.
        /// @return MatrixAlgebra.Matrix		product of the receiver with the
        ///													supplied matrix
        /// @param a MatrixAlgebra.Matrix
        internal protected Matrix SecureProduct(Matrix a)
        {
            return new Matrix(ProductComponents(a));
        }

        /// Same as subtract ( DhbMarix a), but without dimension checking.
        /// @return MatrixAlgebra.Matrix
        /// @param a MatrixAlgebra.Matrix
        internal protected Matrix SecureSubtract(Matrix a)
        {
            return new Matrix(SubtractComponents(a));
        }

        /// @return MatrixAlgebra.Matrix		subtract the supplied matrix to
        ///														the receiver.
        /// @param a MatrixAlgebra.Matrix
        /// @exception MatrixAlgebra.DhbIllegalDimension if the supplied matrix
        ///									does not have the same dimensions.
        public static Matrix operator-(Matrix b, Matrix a)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new DhbIllegalDimension(
                            "Product error: cannot subtract a" + a.Rows
                                    + " by " + a.Columns + " matrix to a "
                                        + b.Rows + " by " + b.Columns + " matrix");
            return new Matrix(b.SubtractComponents(a));
        }

        /// @return double[,]
        /// @param a MatrixAlgebra.Matrix
        protected double[,] SubtractComponents(Matrix a)
        {
            int n = this.Rows;
            int m = this.Columns;
            double[,] newComponents = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    newComponents[i, j] = _components[i, j] - a._components[i, j];
            }
            return newComponents;
        }

        /// @return double[,]	a copy of the components of the receiver.
        public double[,] ToComponents()
        {
            double[,] answer = new double[this.Rows, this.Columns];
            _components.CopyTo(answer, 0);
            return answer;
        }

        /// Returns a string representation of the system.
        /// @return string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            char[] separator = { '[', ' ' };
            int n = this.Rows;
            int m = this.Columns;
            for (int i = 0; i < n; i++)
            {
                separator[0] = '{';
                for (int j = 0; j < m; j++)
                {
                    sb.Append(separator);
                    sb.Append(_components[i, j]);
                    if (j == 0) separator[0] = ' ';
                }
                sb.Append('}');
                sb.Append('\n');
            }
            return sb.ToString();
        }

        /// @return MatrixAlgebra.Matrix		transpose of the receiver
        public virtual Matrix Transpose()
        {
            int n = this.Rows;
            int m = this.Columns;
            double[,] newComponents = new double[m, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    newComponents[j, i] = _components[i, j];
            }
            return new Matrix(newComponents);
        }

        /// @return DhbMatrixAlgebra.SymmetricMatrix	the transposed product
        ///										of the receiver with itself.
        public SymmetricMatrix TransposedProduct()
        {
            return new SymmetricMatrix(TransposedProductComponents(this));
        }

        /// @return MatrixAlgebra.Matrix	product of the tranpose of the
        ///									receiver with the supplied matrix
        /// @param a MatrixAlgebra.Matrix
        /// @exception MatrixAlgebra.DhbIllegalDimension If the number of rows
        /// 							of the receiver are not equal to 
        ///							the number of rows of the supplied matrix.
        public Matrix TransposedProduct(Matrix a)
        {
            if (a.Rows != this.Rows)
                throw new DhbIllegalDimension(
                            "Operation error: cannot multiply a tranposed "
                                    + this.Rows + " by " + this.Columns
                                        + " matrix with a " + a.Rows + " by "
                                                    + a.Columns + " matrix");
            return new Matrix(TransposedProductComponents(a));
        }

        /// @return double[][]	the components of the product of the
        ///											transpose of the receiver
        /// with the supplied matrix.
        /// @param a MatrixAlgebra.Matrix
        internal protected double[,] TransposedProductComponents(Matrix a)
        {
            int p = this.Rows;
            int n = this.Columns;
            int m = a.Columns;
            double[,] newComponents = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < p; k++)
                        sum += _components[k, i] * a._components[k, j];
                    newComponents[i, j] = sum;
                }
            }
            return newComponents;
        }

        ///<summary>
        ///Set all components outside the main diagonal to zero and the components in the main diagonal to 1.
        ///if the instance is not a squared matrix returns exception (note that this is an "in-place" operation).
        ///</summary>
        public void setToIdentity()
        {
            if (!this.IsSquare)
                throw new InvalidOperationException("The setToIdentity() method is only valid on squared matrices.");
            else
            {
                for (int i = 0; i < this.Rows ; i++)
                {
                    for (int j = 0; j < this.Columns; j++)
                    {
                        if(i==j)
                            _components[i, j] = 1.0;
                        else
                            _components[i, j] = 0.0;
                    }
                }
            }
        }

        ///<summary>
        ///Set all components to val (note that this is an "in-place" operation).
        ///</summary>
        ///<param name="val">
        ///desired value to which all elements in the matrix will be set to.
        /// </param>
        public void setTo(double val)
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    _components[i, j] = val;
                }
            }
        }
    }
}
