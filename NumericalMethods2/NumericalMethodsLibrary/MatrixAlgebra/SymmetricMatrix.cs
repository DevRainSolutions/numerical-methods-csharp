#region Using directives

#endregion

namespace NumericalMethods.MatrixAlgebra
{
    /// Symmetric matrix
    /// 
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class SymmetricMatrix : Matrix
    {

        private const int lupCRLCriticalDimension = 36;

        /// Creates a symmetric matrix with given components.
        /// @param a double[,]
        internal protected SymmetricMatrix(double[,] a) : base(a)
        {
        }

        /// @param n int
        /// @exception NegativeArraySizeException if n <= 0
        public SymmetricMatrix(int n) : base(n, n)
        {
        }

        /// Constructor method.
        /// @param n int
        /// @param m int
        /// @exception NegativeArraySizeException if n,m <= 0
        public SymmetricMatrix(int n, int m) : base(n, m)
        {
        }

        /// @return SymmetricMatrix	sum of the matrix with the supplied matrix.
        /// @param a DhbMatrix
        /// @exception DhbIllegalDimension if the supplied matrix does not
        ///											have the same dimensions.
        public static SymmetricMatrix operator+(SymmetricMatrix b, SymmetricMatrix a)
        {
            return new SymmetricMatrix(b.AddComponents(a));
        }

        /// Answers the inverse of the receiver computed via the CRL algorithm.
        /// @return DhbMatrixAlgebra.SymmetricMatrix
        /// @exception ArithmeticException if the matrix is singular.
        private SymmetricMatrix CrlInverse()
        {
            if (this.Rows == 1)
                return Inverse1By1();
            else if (this.Rows == 2)
                return Inverse2By2();
            Matrix[] splitMatrices = Split();
            SymmetricMatrix b1 = (SymmetricMatrix)splitMatrices[0].Inverse();
            Matrix cb1 = splitMatrices[2].SecureProduct(b1);
            SymmetricMatrix cb1cT = new SymmetricMatrix(
                        cb1.ProductWithTransposedComponents(splitMatrices[2]));
            splitMatrices[1] = ((SymmetricMatrix)
                            splitMatrices[1]).SecureSubtract(cb1cT).Inverse();
            splitMatrices[2] = splitMatrices[1].SecureProduct(cb1);
            splitMatrices[0] = b1.SecureAdd(new SymmetricMatrix(
                        cb1.TransposedProductComponents(splitMatrices[2])));
            return SymmetricMatrix.Join(splitMatrices);
        }

        /// @return DhbMatrixAlgebra.SymmetricMatrix
        /// @param	comp double[][]	components of the matrix
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension
        /// 			The supplied components are not those of a square matrix.
        /// @exception DhbMatrixAlgebra.DhbNonSymmetricComponents
        ///			The supplied components are not symmetric.
        public static SymmetricMatrix FromComponents(double[,] comp)
        {
            if (comp.GetLength(0) != comp.GetLength(1))
                throw new DhbIllegalDimension("Non symmetric components: a "
                                            + comp.GetLength(0) + " by " + comp.GetLength(1)
                                            + " matrix cannot be symmetric");
            for (int i = 0; i < comp.GetLength(0); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (comp[i, j] != comp[j, i])
                        throw new DhbNonSymmetricComponents(
                            string.Format("Non symmetric components: a[{0},{1}]= {2}, a[{1},{0}]= {3}",
                                            i, j, comp[i,j], comp[j, i] ) );
                }
            }
            return new SymmetricMatrix(comp);
        }

        /// @return SymmetricMatrix	an identity matrix of size n
        /// @param n int
        public static SymmetricMatrix IdentityMatrix(int n)
        {
            double[,] a = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                a[i, i] = 1;
            }
            return new SymmetricMatrix(a);
        }

        /// @return DhbMatrix		inverse of the receiver.
        /// @exception ArithmeticException if the receiver is
        ///													a singular matrix.
        public override Matrix Inverse()
        {
            return this.Rows < lupCRLCriticalDimension
                        ? new SymmetricMatrix(new LUPDecomposition(this).InverseMatrixComponents())
                        : CrlInverse();
        }

        /// Compute the inverse of the receiver in the case of a 1 by 1 matrix.
        /// Internal use only: no check is made.
        /// @return DhbMatrixAlgebra.SymmetricMatrix
        private SymmetricMatrix Inverse1By1()
        {
            double[,] newComponents = new double[1, 1];
            newComponents[0, 0] = 1 / _components[0, 0];
            return new SymmetricMatrix(newComponents);
        }

        /// Compute the inverse of the receiver in the case of a 2 by 2 matrix.
        /// Internal use only: no check is made.
        /// @return DhbMatrixAlgebra.SymmetricMatrix
        private SymmetricMatrix Inverse2By2()
        {
            double[,] newComponents = new double[2, 2];
            double inverseDeterminant = 1 / (_components[0, 0] * _components[1, 1]
                                        - _components[0, 1] * _components[1, 0]);
            newComponents[0, 0] = inverseDeterminant * _components[1, 1];
            newComponents[1, 1] = inverseDeterminant * _components[0, 0];
            newComponents[0, 1] = newComponents[1, 0] = -inverseDeterminant * _components[1, 0];
            return new SymmetricMatrix(newComponents);
        }

        /// Build a matrix from 3 parts (inverse of split).
        /// @return DhbMatrixAlgebra.SymmetricMatrix
        /// @param a DhbMatrixAlgebra.Matrix[]
        private static SymmetricMatrix Join(Matrix[] a)
        {
            int p = a[0].Rows;
            int n = p + a[1].Rows;
            double[,] newComponents = new double[n, n];
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < p; j++)
                    newComponents[i, j] = a[0][i, j];
                for (int j = p; j < n; j++)
                    newComponents[i, j] = newComponents[j, i] = -a[2][j - p, i];
            }
            for (int i = p; i < n; i++)
            {
                for (int j = p; j < n; j++)
                    newComponents[i, j] = a[1].Components[i - p, j - p];
            }
            return new SymmetricMatrix(newComponents);
        }

        /// @return int
        /// @param n int
        private int LargestPowerOf2SmallerThan(int n)
        {
            int m = 2;
            int m2;
            while (true)
            {
                m2 = 2 * m;
                if (m2 >= n)
                    return m;
                m = m2;
            }
        }

        /// @return DhbMatrixAlgebra.SymmetricMatrix
        /// @param a double
        public static Matrix operator *(SymmetricMatrix b, double a)
        {
            return new SymmetricMatrix(b.ProductComponents(a));
        }

        /// @return Matrix		product of the receiver with the supplied matrix
        /// @param a Matrix
        /// @exception DhbIllegalDimension If the number of columns of
        /// the receivers are not equal to the number of rows
        /// of the supplied matrix.
        public static SymmetricMatrix operator*(SymmetricMatrix b, SymmetricMatrix a)
        {
            return new SymmetricMatrix(b.ProductComponents(a));
        }

        /// @return DhbMatrixAlgebra.Matrix	product of the receiver with
        /// 								the transpose of the supplied matrix
        /// @param a DhbMatrixAlgebra.Matrix
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension If the number of
        ///			columns of the receiver are not equal to the number of
        ///			columns of the supplied matrix.
        public SymmetricMatrix ProductWithTransposed(SymmetricMatrix a)
        {
            if (a.Columns != this.Columns)
                throw new DhbIllegalDimension(
                            string.Format(
                                "Operation error: cannot multiply a {0} by {1}"
                                + " matrix with the transpose of a {2} by {3} matrix",
                                this.Rows, this.Columns, a.Rows, a.Columns ) );

            return new SymmetricMatrix(ProductWithTransposedComponents(a));
        }

        /// Same as add ( SymmetricMatrix a), but without dimension checking.
        /// @return DhbMatrixAlgebra.SymmetricMatrix
        /// @param a DhbMatrixAlgebra.SymmetricMatrix
        protected SymmetricMatrix SecureAdd(SymmetricMatrix a)
        {
            return new SymmetricMatrix(AddComponents(a));
        }	

        /// Same as product(DhbSymmetricMatrix a), but without dimension checking.
        /// @return DhbMatrixAlgebra.SymmetricMatrix
        /// @param a DhbMatrixAlgebra.SymmetricMatrix
        protected SymmetricMatrix SecureProduct(SymmetricMatrix a)
        {
            return new SymmetricMatrix(ProductComponents(a));
        }

        /// Same as subtract ( SymmetricMatrix a), but without dimension checking.
        /// @return DhbMatrixAlgebra.SymmetricMatrix
        /// @param a DhbMatrixAlgebra.SymmetricMatrix
        protected SymmetricMatrix SecureSubtract(SymmetricMatrix a)
        {
            return new SymmetricMatrix(SubtractComponents(a));
        }

        /// Divide the receiver into 3 matrices or approximately equal dimension.
        /// @return DhbMatrixAlgebra.Matrix[]	Array of splitted matrices
        private Matrix[] Split()
        {
            int n = this.Rows;
            int p = LargestPowerOf2SmallerThan(n);
            int q = n - p;
            double[,] a = new double[p, p];
            double[,] b = new double[q, q];
            double[,] c = new double[q, p];
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < p; j++)
                    a[i, j] = _components[i, j];
                for (int j = p; j < n; j++)
                    c[j - p, i] = _components[i, j];
            }
            for (int i = p; i < n; i++)
            {
                for (int j = p; j < n; j++)
                    b[i - p, j - p] = _components[i, j];
            }
            return new Matrix[]
                    { new SymmetricMatrix(a), new SymmetricMatrix(b), new Matrix(c) };
        }

        /// @return DHBmatrixAlgebra.SymmetricMatrix
        /// @param a DHBmatrixAlgebra.SymmetricMatrix
        /// @exception DHBmatrixAlgebra.DhbIllegalDimension (from constructor).
        public static SymmetricMatrix operator-(SymmetricMatrix b, SymmetricMatrix a)
        {
            return new SymmetricMatrix(b.SubtractComponents(a));
        }

        /// @return DHBmatrixAlgebra.Matrix		the same matrix
        public override Matrix Transpose()
        {
            return this;
        }

        /// @return DhbMatrixAlgebra.SymmetricMatrix	product of the tranpose
        /// 							of the receiver with the supplied matrix
        /// @param a DhbMatrixAlgebra.SymmetricMatrix
        /// @exception DhbMatrixAlgebra.DhbIllegalDimension If the number of
        ///						rows of the receiver are not equal to
        ///						the number of rows of the supplied matrix.
        public SymmetricMatrix TransposedProduct(SymmetricMatrix a)
        {
            if (a.Rows != this.Rows)
                throw new DhbIllegalDimension(
                        string.Format(
                            "Operation error: cannot multiply a tranposed "
                            + "{0} by {1} matrix with a {2} by {3} matrix",
                            this.Rows, this.Columns, a.Rows, a.Columns ) );
            return new SymmetricMatrix(TransposedProductComponents(a));
        }
    }
}
