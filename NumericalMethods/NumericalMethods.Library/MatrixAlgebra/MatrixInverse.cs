
using System;

namespace NumericalMethods.MatrixAlgebra
{
    public class MatrixInverse
    {

        /// <summary>
        /// Calculation inverse of matrix
        /// http://www.mathwords.com/i/inverse_of_a_matrix.htm  using method 3
        /// </summary>
        /// Coded by : ahmed osama  (ahmed_osama@hotmail.com)
        /// <summary>
        ///  Parameters of entries:
        /// </summary>
        /// <param name="MAT">Array with numeration of elements[0..N-1, 0..N-1]</param>
        /// <param name="N">Dimension of matrix A</param>
       


        public double[,] InvMatrix(double[,] MAT, int N)
        {
           
            double[,] ResultMat = new double[N , N];

            MatrixDeterminant MatDet = new MatrixDeterminant ();

            double DET = MatDet.MatrixDet(MAT, N);
            if (DET == 0)
            {
                throw new System.ArgumentException("Matrix is singular ");
               
            }
            
            MatrixCofactor MatCof = new MatrixCofactor ();

            ResultMat = MatCof.AdjointMatrix(MAT, N);

            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    ResultMat[x, y] = ResultMat[x, y] / DET;
                }
            }

            return ResultMat;
        }



      


    }








}
