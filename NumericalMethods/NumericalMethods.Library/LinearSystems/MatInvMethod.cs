using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumericalMethods.MatrixAlgebra;

namespace NumericalMethods.SystemLinearEqualizations
{
    public class MatInvMethod
    {
       
        double[] result;

        
        
        
         /// <summary>
        /// Solve N Linear Equations [A][x]=[B] using Matrix Inverse Method
        /// http://www.intmath.com/matrices-determinants/6-matrices-linear-equations.php  
        /// </summary>
        /// Coded by : ahmed osama  (ahmed_osama@hotmail.com)
        /// <summary>
        ///  Parameters of entries:
        /// </summary>
        /// <param name="A">Coeff. Array N x N , </param>
        /// <param name="B">Ans. array N item </param>
        /// <param name="N">No. of Linear Equations </param>
        /// 
        
        public void MatrixInverseMethod ( double [,] A , double [] B , int N)
        {
            double [,] tmp;
            result = new double[N];

            MatrixInverse MATINV = new MatrixInverse();
            tmp= MATINV.InvMatrix(A, N);

            for (int R = 0; R < N; R++)
            {
                for (int C = 0; C < N; C++)
                {
                    result[R] = result[R] + tmp[R, C] * B[C];
                }
            }




        }


        public double[] GetSolution()
        {
            return result;
        }
    }
}
