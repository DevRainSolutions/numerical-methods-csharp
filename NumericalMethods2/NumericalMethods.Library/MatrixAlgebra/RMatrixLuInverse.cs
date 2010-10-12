using System;

namespace NumericalMethods.MatrixAlgebra
{
    public class RMatrixLuInverse
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[,] result;
        /*************************************************************************
        Обращение матрицы, заданной LU-разложением

        Входные параметры:
            A       -   LU-разложение  матрицы   (результат   работы  подпрограммы
                        RMatrixLU).
            Pivots  -   таблица перестановок,  произведенных в ходе LU-разложения.
                        (результат работы подпрограммы RMatrixLU).
            N       -   размерность матрицы

        Выходные параметры:
            A       -   матрица, обратная к исходной. Массив с нумерацией
                        элементов [0..N-1, 0..N-1]

        Результат:
            True,  если исходная матрица невырожденная.
            False, если исходная матрица вырожденная.
        *************************************************************************/
        public  bool rmatrixluinverse(double[,] a,int n,int [] pivots)
        {
            bool resultbool = new bool();
            double[] work = new double[0];;
            int i = 0;
            int j = 0;
            int jp = 0;
            double v = 0;
            int i_ = 0;

            resultbool = true;

            //
            // Quick return if possible
            //
            if (n == 0)
            {
                return resultbool;
            }
            work = new double[n - 1 + 1];

            //
            // Form inv(U)
            //
            if (!rmatrixtrinverse(ref a, n, true, false))
            {

                resultbool = false;
                return resultbool;
            }

            //
            // Solve the equation inv(A)*L = inv(U) for inv(A).
            //
            for (j = n - 1; j >= 0; j--)
            {

                //
                // Copy current column of L to WORK and replace with zeros.
                //
                for (i = j + 1; i <= n - 1; i++)
                {
                    work[i] = a[i, j];
                    a[i, j] = 0;
                }

                //
                // Compute current column of inv(A).
                //
                if (j < n - 1)
                {
                    for (i = 0; i <= n - 1; i++)
                    {
                        v = 0.0;
                        for (i_ = j + 1; i_ <= n - 1; i_++)
                        {
                            v += a[i, i_] * work[i_];
                        }
                        a[i, j] = a[i, j] - v;
                    }
                }
            }

            //
            // Apply column interchanges.
            //
            for (j = n - 2; j >= 0; j--)
            {
                jp = pivots[j];
                if (jp != j)
                {
                    for (i_ = 0; i_ <= n - 1; i_++)
                    {
                        work[i_] = a[i_, j];
                    }
                    for (i_ = 0; i_ <= n - 1; i_++)
                    {
                        a[i_, j] = a[i_, jp];
                    }
                    for (i_ = 0; i_ <= n - 1; i_++)
                    {
                        a[i_, jp] = work[i_];
                    }
                }
            }
            result = a;
            return resultbool;
            
        }
        public static bool rmatrixtrinverse(ref double[,] a, int n, bool isupper, bool isunittriangular)
        {
            bool result = new bool();
            bool nounit = new bool();
            int i = 0;
            int j = 0;
            double v = 0;
            double ajj = 0;
            double[] t = new double[0];
            int i_ = 0;

            result = true;
            t = new double[n - 1 + 1];

            //
            // Test the input parameters.
            //
            nounit = !isunittriangular;
            if (isupper)
            {

                //
                // Compute inverse of upper triangular matrix.
                //
                for (j = 0; j <= n - 1; j++)
                {
                    if (nounit)
                    {
                        if (a[j, j] == 0)
                        {
                            result = false;
                            return result;
                        }
                        a[j, j] = 1 / a[j, j];
                        ajj = -a[j, j];
                    }
                    else
                    {
                        ajj = -1;
                    }

                    //
                    // Compute elements 1:j-1 of j-th column.
                    //
                    if (j > 0)
                    {
                        for (i_ = 0; i_ <= j - 1; i_++)
                        {
                            t[i_] = a[i_, j];
                        }
                        for (i = 0; i <= j - 1; i++)
                        {
                            if (i < j - 1)
                            {
                                v = 0.0;
                                for (i_ = i + 1; i_ <= j - 1; i_++)
                                {
                                    v += a[i, i_] * t[i_];
                                }
                            }
                            else
                            {
                                v = 0;
                            }
                            if (nounit)
                            {
                                a[i, j] = v + a[i, i] * t[i];
                            }
                            else
                            {
                                a[i, j] = v + t[i];
                            }
                        }
                        for (i_ = 0; i_ <= j - 1; i_++)
                        {
                            a[i_, j] = ajj * a[i_, j];
                        }
                    }
                }
            }
            else
            {

                //
                // Compute inverse of lower triangular matrix.
                //
                for (j = n - 1; j >= 0; j--)
                {
                    if (nounit)
                    {
                        if (a[j, j] == 0)
                        {
                            result = false;
                            return result;
                        }
                        a[j, j] = 1 / a[j, j];
                        ajj = -a[j, j];
                    }
                    else
                    {
                        ajj = -1;
                    }
                    if (j < n - 1)
                    {

                        //
                        // Compute elements j+1:n of j-th column.
                        //
                        for (i_ = j + 1; i_ <= n - 1; i_++)
                        {
                            t[i_] = a[i_, j];
                        }
                        for (i = j + 1; i <= n - 1; i++)
                        {
                            if (i > j + 1)
                            {
                                v = 0.0;
                                for (i_ = j + 1; i_ <= i - 1; i_++)
                                {
                                    v += a[i, i_] * t[i_];
                                }
                            }
                            else
                            {
                                v = 0;
                            }
                            if (nounit)
                            {
                                a[i, j] = v + a[i, i] * t[i];
                            }
                            else
                            {
                                a[i, j] = v + t[i];
                            }
                        }
                        for (i_ = j + 1; i_ <= n - 1; i_++)
                        {
                            a[i_, j] = ajj * a[i_, j];
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public double[,] GetSolution()
        {
            return result;
        }
    }
}
