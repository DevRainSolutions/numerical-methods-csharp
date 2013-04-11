
using System;

namespace NumericalMethods.MatrixAlgebra
{
    public class MatrixTranspose
    {

        /// <summary>
        /// Calculation Transpose of matrix
        /// http://www.mathwords.com/t/transpose_of_a_matrix.htm
        /// </summary>
        /// Coded by : ahmed osama  (ahmed_osama@hotmail.com)
        /// <summary>
        ///  Parameters of entries:
        /// </summary>
        /// <param name="MAT">Array with numeration of elements[0..N-1, 0..N-1]</param>
        /// <param name="N">Dimension of matrix A</param>
      


        public double[,] TransMatrix(double[,] MAT, int N)
        {
           
            double[,] ResultMat = new double[N , N];

            for (int RX = 0; RX < N; RX++)
            {
                for (int RY = 0; RY < N; RY++)
                {

                    ResultMat[RX, RY] = MAT[RY, RX];
                }
            }

            return ResultMat;
        }

    }








}
