#region Using directives

using System;
using NumericalMethods.Iterations;


#endregion

namespace NumericalMethods.MatrixAlgebra
{
    /// Object used to find the largest eigen value and the corresponding
    /// eigen vector of a matrix by successive approximations.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class LargestEigenvalueFinder : IterativeProcess
    {
        /// Eigenvalue
        private double _eigenvalue;
        /// Eigenvector
        private DhbVector _eigenvector;
        /// Eigenvector of transposed matrix
        private DhbVector _transposedEigenvector;
        /// Matrix.
        private Matrix _matrix;

        /// Constructor method.
        /// @param prec double
        /// @param a DhbMatrixAlgebra.Matrix
        public LargestEigenvalueFinder(double prec, Matrix a) : this(a)
        {
            this.DesiredPrecision = prec;
        }

        /// Constructor method.
        /// @param a DhbMatrixAlgebra.Matrix
        public LargestEigenvalueFinder(Matrix a)
        {
            _matrix = a;
            _eigenvalue = double.NaN;
        }

        /// Returns the eigen value found by the receiver.
        /// @return double
        public double Eigenvalue
        {
            get { return _eigenvalue; }
        }

        /// Returns the normalized eigen vector found by the receiver.
        /// @return DhbMatrixAlgebra.DhbVector
        public DhbVector Eigenvector
        {
            get { return _eigenvector * (1.0 / _eigenvector.Norm); }
        }

        /// Iterate matrix product in eigenvalue information.
        public override double EvaluateIteration()
        {
            double oldEigenvalue = _eigenvalue;
            _transposedEigenvector = _transposedEigenvector.SecureProduct(_matrix);
            _transposedEigenvector *= (1.0 / _transposedEigenvector[0]);
            _eigenvector = _matrix.SecureProduct(_eigenvector);
            _eigenvalue = _eigenvector[0];
            _eigenvector *= (1.0 / _eigenvalue);
            return double.IsNaN(oldEigenvalue)
                            ? 10 * this.DesiredPrecision
                            : Math.Abs(_eigenvalue - oldEigenvalue);
        }

        /// Set result to undefined.
        public override void InitializeIterations()
        {
            _eigenvalue = double.NaN;
            int n = _matrix.Columns;
            double[] eigenvectorComponents = new double[n];
            for (int i = 0; i < n; i++)
                eigenvectorComponents[i] = 1.0;
            _eigenvector = new DhbVector(eigenvectorComponents);
            n = _matrix.Rows;
            eigenvectorComponents = new double[n];
            for (int i = 0; i < n; i++)
                eigenvectorComponents[i] = 1.0;
            _transposedEigenvector = new DhbVector(eigenvectorComponents);
        }

        /// Returns a finder to find the next largest eigen value of the receiver's matrix.
        /// @return DhbMatrixAlgebra.LargestEigenvalueFinder
        public LargestEigenvalueFinder NextLargestEigenvalueFinder()
        {
            double norm = 1.0 / _eigenvector.SecureProduct(_transposedEigenvector);
            DhbVector v1 = _eigenvector * norm;
            return new LargestEigenvalueFinder(this.DesiredPrecision,
                    _matrix.SecureProduct(SymmetricMatrix.IdentityMatrix(v1.Dimension)
                                .SecureSubtract(v1.TensorProduct(_transposedEigenvector))));
        }

        /// Returns a string representation of the receiver.
        /// @return string
        public override string ToString()
        {
            return string.Format("{0} ({1})", _eigenvalue, _eigenvector);
        }
    }
}
