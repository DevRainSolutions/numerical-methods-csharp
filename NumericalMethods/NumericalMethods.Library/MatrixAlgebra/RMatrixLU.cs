

using System;
namespace NumericalMethods.MatrixAlgebra
{
    public class MatrixLU 
    {
        /// <summary>
        /// Equation solution
        /// </summary>
        double[,] result;
        int[] result2;

        public const int lunb = 8;

        /*************************************************************************
        LU-разложение матрицы общего вида размера M x N

        ѕодпрограмма вычисл€ет LU-разложение пр€моугольной матрицы общего  вида  с
        частичным выбором ведущего элемента (с перестановками строк).

        ¬ходные параметры:
            A       -   матрица A. Ќумераци€ элементов: [0..M-1, 0..N-1]
            M       -   число строк в матрице A
            N       -   число столбцов в матрице A

        ¬ыходные параметры:
            A       -   матрицы L и U в компактной форме (см. ниже).
                        Ќумераци€ элементов: [0..M-1, 0..N-1]
            Pivots  -   матрица перестановок в компактной форме (см. ниже).
                        Ќумераци€ элементов: [0..Min(M-1,N-1)]

        ћатрица A представл€етс€, как A = P * L * U, где P - матрица перестановок,
        матрица L - нижнетреугольна€ (или нижнетрапецоидальна€, если M>N) матрица,
        U - верхнетреугольна€ (или верхнетрапецоидальна€, если M<N) матрица.

        –ассмотрим разложение более подробно на примере при M=4, N=3:

                           (  1          )    ( U11 U12 U13  )
        A = P1 * P2 * P3 * ( L21  1      )  * (     U22 U23  )
                           ( L31 L32  1  )    (         U33  )
                           ( L41 L42 L43 )

        «десь матрица L  имеет  размер  M  x  Min(M,N),  матрица  U  имеет  размер
        Min(M,N) x N, матрица  P(i)  получаетс€  путем  перестановки  в  единичной
        матрице размером M x M строк с номерами I и Pivots[I]

        –езультатом работы алгоритма €вл€ютс€ массив Pivots  и  следующа€ матрица,
        замещающа€  матрицу  A,  и  сохран€юща€  в компактной форме матрицы L и U
        (пример приведен дл€ M=4, N=3):

         ( U11 U12 U13 )
         ( L21 U22 U23 )
         ( L31 L32 U33 )
         ( L41 L42 L43 )

         ак видно, единична€ диагональ матрицы L  не  сохран€етс€.
        ≈сли N>M, то соответственно мен€ютс€ размеры матриц и расположение
        элементов.

        *************************************************************************/
        public MatrixLU (double[,] a,int m,int n)
        {
            int [] pivots=new int[n];
            double[,] b = new double[0, 0];
            double[] t = new double[0];
            int[] bp = new int[0];
            int minmn = 0;
            int i = 0;
            int ip = 0;
            int j = 0;
            int j1 = 0;
            int j2 = 0;
            int cb = 0;
            int nb = 0;
            double v = 0;
            int i_ = 0;
            int i1_ = 0;

            System.Diagnostics.Debug.Assert(lunb >= 1, "RMatrixLU internal error");
            nb = lunb;

            //
            // Decide what to use - blocked or unblocked code
            //
            if (n <= 1 | Math.Min(m, n) <= nb | nb == 1)
            {

                //
                // Unblocked code
                //
                rmatrixlu2(ref a, m, n, ref pivots);
            }
            else
            {

                //
                // Blocked code.
                // First, prepare temporary matrix and indices
                //
                b = new double[m - 1 + 1, nb - 1 + 1];
                t = new double[n - 1 + 1];
                pivots = new int[Math.Min(m, n) - 1 + 1];
                minmn = Math.Min(m, n);
                j1 = 0;
                j2 = Math.Min(minmn, nb) - 1;

                //
                // Main cycle
                //
                while (j1 < minmn)
                {
                    cb = j2 - j1 + 1;

                    //
                    // LU factorization of diagonal and subdiagonal blocks:
                    // 1. Copy columns J1..J2 of A to B
                    // 2. LU(B)
                    // 3. Copy result back to A
                    // 4. Copy pivots, apply pivots
                    //
                    for (i = j1; i <= m - 1; i++)
                    {
                        i1_ = (j1) - (0);
                        for (i_ = 0; i_ <= cb - 1; i_++)
                        {
                            b[i - j1, i_] = a[i, i_ + i1_];
                        }
                    }
                    rmatrixlu2(ref b, m - j1, cb, ref bp);
                    for (i = j1; i <= m - 1; i++)
                    {
                        i1_ = (0) - (j1);
                        for (i_ = j1; i_ <= j2; i_++)
                        {
                            a[i, i_] = b[i - j1, i_ + i1_];
                        }
                    }
                    for (i = 0; i <= cb - 1; i++)
                    {
                        ip = bp[i];
                        pivots[j1 + i] = j1 + ip;
                        if (bp[i] != i)
                        {
                            if (j1 != 0)
                            {

                                //
                                // Interchange columns 0:J1-1
                                //
                                for (i_ = 0; i_ <= j1 - 1; i_++)
                                {
                                    t[i_] = a[j1 + i, i_];
                                }
                                for (i_ = 0; i_ <= j1 - 1; i_++)
                                {
                                    a[j1 + i, i_] = a[j1 + ip, i_];
                                }
                                for (i_ = 0; i_ <= j1 - 1; i_++)
                                {
                                    a[j1 + ip, i_] = t[i_];
                                }
                            }
                            if (j2 < n - 1)
                            {

                                //
                                // Interchange the rest of the matrix, if needed
                                //
                                for (i_ = j2 + 1; i_ <= n - 1; i_++)
                                {
                                    t[i_] = a[j1 + i, i_];
                                }
                                for (i_ = j2 + 1; i_ <= n - 1; i_++)
                                {
                                    a[j1 + i, i_] = a[j1 + ip, i_];
                                }
                                for (i_ = j2 + 1; i_ <= n - 1; i_++)
                                {
                                    a[j1 + ip, i_] = t[i_];
                                }
                            }
                        }
                    }

                    //
                    // Compute block row of U
                    //
                    if (j2 < n - 1)
                    {
                        for (i = j1 + 1; i <= j2; i++)
                        {
                            for (j = j1; j <= i - 1; j++)
                            {
                                v = a[i, j];
                                for (i_ = j2 + 1; i_ <= n - 1; i_++)
                                {
                                    a[i, i_] = a[i, i_] - v * a[j, i_];
                                }
                            }
                        }
                    }

                    //
                    // Update trailing submatrix
                    //
                    if (j2 < n - 1)
                    {
                        for (i = j2 + 1; i <= m - 1; i++)
                        {
                            for (j = j1; j <= j2; j++)
                            {
                                v = a[i, j];
                                for (i_ = j2 + 1; i_ <= n - 1; i_++)
                                {
                                    a[i, i_] = a[i, i_] - v * a[j, i_];
                                }
                            }
                        }
                    }

                    //
                    // Next step
                    //
                    j1 = j2 + 1;
                    j2 = Math.Min(minmn, j1 + nb) - 1;
                }
            }
            result = a;
            result2 = pivots;
        }
        public static void rmatrixlu2(ref double[,] a,
            int m,
            int n,
            ref int[] pivots)
        {
            int i = 0;
            int j = 0;
            int jp = 0;
            double[] t1 = new double[0];
            double s = 0;
            int i_ = 0;

            pivots = new int[Math.Min(m - 1, n - 1) + 1];
            t1 = new double[Math.Max(m - 1, n - 1) + 1];
            System.Diagnostics.Debug.Assert(m >= 0 & n >= 0, "Error in LUDecomposition: incorrect function arguments");

            //
            // Quick return if possible
            //
            if (m == 0 | n == 0)
            {
                return;
            }
            for (j = 0; j <= Math.Min(m - 1, n - 1); j++)
            {

                //
                // Find pivot and test for singularity.
                //
                jp = j;
                for (i = j + 1; i <= m - 1; i++)
                {
                    if (Math.Abs(a[i, j]) > Math.Abs(a[jp, j]))
                    {
                        jp = i;
                    }
                }
                pivots[j] = jp;
                if (a[jp, j] != 0)
                {

                    //
                    //Apply the interchange to rows
                    //
                    if (jp != j)
                    {
                        for (i_ = 0; i_ <= n - 1; i_++)
                        {
                            t1[i_] = a[j, i_];
                        }
                        for (i_ = 0; i_ <= n - 1; i_++)
                        {
                            a[j, i_] = a[jp, i_];
                        }
                        for (i_ = 0; i_ <= n - 1; i_++)
                        {
                            a[jp, i_] = t1[i_];
                        }
                    }

                    //
                    //Compute elements J+1:M of J-th column.
                    //
                    if (j < m)
                    {
                        jp = j + 1;
                        s = 1 / a[j, j];
                        for (i_ = jp; i_ <= m - 1; i_++)
                        {
                            a[i_, j] = s * a[i_, j];
                        }
                    }
                }
                if (j < Math.Min(m, n) - 1)
                {

                    //
                    //Update trailing submatrix.
                    //
                    jp = j + 1;
                    for (i = j + 1; i <= m - 1; i++)
                    {
                        s = a[i, j];
                        for (i_ = jp; i_ <= n - 1; i_++)
                        {
                            a[i, i_] = a[i, i_] - s * a[j, i_];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public double[,] GetSolution()
        {
            return result;
        }
        public int[] GetSolution2()
        {
            return result2;
        }
    }
}

