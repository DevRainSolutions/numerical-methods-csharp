using System;

namespace NumericalMethods.MatrixAlgebra
{
    public class MatrixDeterminant
    {
        /// <summary>
        /// Calculation determinant of matrix 
        /// </summary>

        /// <summary>
        ///  Parameters of entries:
        /// </summary>
        /// <param name="A">Array with numeration of elements[0..N-1, 0..N-1]</param>
        /// <param name="N">Dimension of matrix A</param>

        public  double MatrixDet(double[,] a,
            int n)
        {
            double result = 0;
            int[] pivots = new int[0];
            a = (double[,])a.Clone();
            MatrixLU matr = new MatrixLU(a, n, n);
            var result_matrlu = matr.GetSolution();
            var result_matrlu2 = matr.GetSolution2();
            result = rMatrixLuDet(ref result_matrlu,ref result_matrlu2 ,n);
            return result;
        }
        /// <summary>
        ///  Calculation of determinant of matrix, set LU-decomposition.
        /// </summary>

        /// <summary>
        /// Parameters of entries:
        /// </summary>
        /// <param name="A">LU-decomposition  of matrix   (job  of podprogrammy of RMatrixLU performance  ).</param>
        /// <param name="Pivots">Table of transpositions,  made during LU-decomposition.(job of podprogrammy of RMatrixLU performance).</param>
        /// <param name="N">Dimension of matrix A</param>

        public double rMatrixLuDet(ref double[,] a,
            ref int[] pivots,
            int n)
        {
            double result = 0;
            int i = 0;
            int s = 0;

            result = 1;
            s = 1;
            for (i = 0; i <= n - 1; i++)
            {
                result = result * a[i, i];
                if (pivots[i] != i)
                {
                    s = -s;
                }
            }
            result = result * s;
            return result;
        }
    }
}
