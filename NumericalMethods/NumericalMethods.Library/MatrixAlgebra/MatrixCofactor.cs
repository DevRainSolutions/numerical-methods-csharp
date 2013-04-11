
using System;

namespace NumericalMethods.MatrixAlgebra
{
    public class MatrixCofactor
    {

        /// <summary>
        /// Calculation cofactor of matrix
        /// http://www.mathwords.com/c/cofactor_matrix.htm
        /// </summary>
        /// Coded by : ahmed osama  (ahmed_osama@hotmail.com)
        /// <summary>
        ///  Parameters of entries:
        /// </summary>
        /// <param name="MAT">Array with numeration of elements[0..N-1, 0..N-1]</param>
        /// <param name="N">Dimension of matrix A</param>
       


        public double[,] CofacMatrix(double[,] MAT, int N)
        {
            double[,] tmp = new double[N - 1, N - 1];
            double[,] ResultMat = new double[N , N];

            for (int RY = 0; RY < N; RY++)
            {
                for (int RX = 0; RX < N; RX++)
                {
                    int XX = 0;
                    int YY = 0;
                    for (int y = 0; y < N; y++)
                    {
                        for (int x = 0; x < N; x++)
                        {
                            if (x != RY & y != RX)
                            {
                                tmp[XX, YY] = MAT[x, y];
                                XX++;
                                if (XX == N - 1)
                                {
                                    XX = 0;
                                    YY++;
                                }


                            }
                        }
                    }

                    MatrixDeterminant MatDet = new MatrixDeterminant ();
                    ResultMat[RY, RX] =Math.Pow (-1, RX+RY+2)  * MatDet.MatrixDet(tmp, N-1);
                }
            }

            return ResultMat;
        }



        /// <summary>
        /// Calculation Adjoint of matrix
        /// http://www.mathwords.com/a/adjoint.htm
        /// </summary>
        /// Coded by : ahmed osama  (ahmed_osama@hotmail.com)
        /// <summary>
        ///  Parameters of entries:
        /// </summary>
        /// <param name="MAT">Array with numeration of elements[0..N-1, 0..N-1]</param>
        /// <param name="N">Dimension of matrix A</param>
        public double[,] AdjointMatrix (double [,] MAT, int N)
        {
            double[,] ResultMat = new double[N, N];
            ResultMat = CofacMatrix(MAT, N);
            
            MatrixTranspose  TRANS = new MatrixTranspose ();

             ResultMat =TRANS.TransMatrix(ResultMat, N);
             return ResultMat;
        }


    }








}
