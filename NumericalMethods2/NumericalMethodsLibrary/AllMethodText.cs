using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumericalMethods.Approximation;
namespace NumericalMethods
{
    public class AllMethodText
    {
        /// <summary>
        /// Approximate decision of equalization f(x)=0
        /// </summary>
        public static string Approximation_Bisection = @"
        public Bisection(FunctionOne f,double Left, double Right, double epsilon)
        {
            bool blnError = false;
            double c;
            do
            {
                c = (Left + Right) / 2;
                if (f(Left) * f(c) < 0)
                    Right = c;
                else
                    Left = c;
                if (f(c) == 0)
                {
                    blnError = (f(c - epsilon / 2) * f(c + epsilon / 2) < 0) ? false : true;
                    break;
                }
            }
            while ((f(c)) > epsilon || (Right - Left) > epsilon);
            if (blnError == false)
            {
                result = c;
            }
        }";
        public static string Approximation_Chord = @"
        public Сhord(FunctionOne function,FunctionOne df, double Left, double Right, double x0, double epsilon)
        {
            double xl;
            const double m = 2.41064f;
            const double M = 20.0828f;
            double xk;
            xk = x0 - (function(x0) / df(x0));
            do
            {
                xl = xk - function(xk) * (xk - x0) / (function(xk) - function(x0));
                x0 = xk;
                xk = xl;
            }
            while (Math.Abs(xk - x0) > Math.Sqrt(Math.Abs(2f * epsilon * m / M)));
            result = x0;
        }";
        public static string Approximation_Iterationmethod = @"
        public IterationMethod(FunctionOne function, double Left, double Right, double x0, double epsilon)
        {
            double xk;
            do
            {
                xk = g(x0, function);
                if (Math.Abs(xk - x0) < epsilon) break;
                else x0 = xk;
            }
            while (Math.Abs(Left - x0) > epsilon && Math.Abs(Right - x0) > epsilon);
            result = xk;
        }
        double g(double x, FunctionOne function)
        {
            return 0.1 * function(x) + x;
        }";
        public static string Approximation_Newton = @"
        public Newton(FunctionOne function, FunctionOne df, double Left, double Right, double x0, double epsilon)
        {
            const double m = 2.41064f;
            const double M = 20.0828f;
            double xk;
            xk = x0;
            do
            {
                x0 = xk;
                xk = x0 - (function(x0) / df(x0));
            }
            while (Math.Abs(xk - x0) > Math.Sqrt(Math.Abs(2f * epsilon * m / M)));
            result = x0;
        }";
        /// <summary>
        /// Differential Equations
        /// </summary>
        public static string DifferentialEquations_EulerCorrected = @"
        public EulerCorrected(Function function, double begin, double end, double y0, int pointsNum)
        {

            double y = 0;
            double y1;
            double f1;
            double x = 0;
            double h;
            result = new double[2, pointsNum + 1];
            h = (end - begin) / pointsNum;
            y1 = y0;
            for (int i = 0; i <= pointsNum; i++)
            {
                f1 = function(x, y);
                y = y1 +(h/2)*(function(x,y)+function(x + h ,y + h * f1));
                result[0, i] = x;
                result[1, i] = y1; 
                y1 = y;
                x = x + h;
            }
        }";
        public static string DifferentialEquations_EulerModified = @"
         public EulerModified(Function function, double begin, double end, double y0, int pointsNum)
        {
            double y=0;
            double y1;
            double f1;
            double x = 0;
            double h;
            result = new double[2, pointsNum+1];
            h = (end - begin) / pointsNum;
            y1 = y0;
            x = 0;
            result[0, 0] = x;
            result[1, 0] = y1;
            for (int i = 1; i <= pointsNum; i++)
            {
                f1 = function(x, y);
                x = x + h;
                y = y + f1 * h;
                y = y1 + h * (f1 + function(x, y)) / 2;
                y1 = y;
                result[0, i] = x;
                result[1, i] = y1;
            }
        }";
        public static string DifferentialEquations_EulerSimple = @"
        public EulerSimple(Function function, double begin, double end, double y0, int pointsNum)
        {
            double y;
            double y1;
            double x=0;
            double h;
            result = new double[2, pointsNum];

            h = (end - begin) / pointsNum;
            y1 = y0;
            y = 0;
            for (int i = 0; i < pointsNum; i++)
            {
                y = y1 + h * function(x, y);
                y1 = y;
                x = x + h;
                result[0, i] = x;
                result[1, i] = y;
            }
        }";
        public static string DifferentialEquations_RungeKutta4 = @"
         public RungeKutta4(Function function, double begin, double end, double y0, int pointsNum)
        {
            result = new double[2, pointsNum+1];
            double k1;
            double k2;
            double k3;
            double h = (end - begin) / pointsNum;
            double y1;
            double x=0;
            double y=0;
            y1 = y0;
            result[0, 0] = x;
            result[1, 0] = y1;
            for (int i = 1; i <= pointsNum; i++)
            {
                k1 = h * function(x, y);
                x = x + h / 2;
                y = y1 + k1 / 2;
                k2 = function(x, y) * h;
                y = y1 + k2 / 2;
                k3 = function(x, y) * h;
                x = x + h / 2;
                y = y1 + (k1 + 2 * k2 + 2 * k3 + function(x, y) * h) / 6;
                y1 = y;
                result[0, i] = x;
                result[1, i] = y1; 
            }
        }";
        /// <summary>
        /// Integration
        /// </summary>
        public static string Integration_Chebishev = @" 
        public Chebishev(FunctionOne f, double a, double b, int pointsNum)
        {
            double h;
            double j;
            double rez;
            int she = 4;
            result = new double[2, pointsNum+1];
            for (int i = 0; i <= pointsNum; i++)
            {
                h = (b - a) / (double)she;
                she += 4;
                rez = 0;
                for (j = a; j <= b; j = j + h)
                {
                    rez = rez + ChebushevMethod(j, j + h, f);
                }
                result[0, i] = rez;
                result[1, i] = h;
            }
        }
        double ChebushevMethod(double A, double B, FunctionOne f)
        {
            double[] t = { 0, -0.832498, -0.374513f, 0, 0.374513, 0.832498 };
            int k;
            double x, reten;
            reten = 0;
            for (k = 1; k <= 5; k++)
            {
                x = (double)(A + B) / 2 + (double)(B - A) * (double)t[k] / 2;
                reten += f(x);
            }
            reten = reten * (B - A) / 5;
            return reten;
        }  ";
        public static string Integration_Simpson = @"
        public Simpson(FunctionOne f, double a, double b, int pointsNum)
        {
            double fi, I=0;
            double h;
            int j=0;
            h = 0.015f;
            result = new double[2, pointsNum+1];
            for (int i = 0; i < pointsNum; i++)
            {
                fi = a + h;
                do
                {
                    if (j % 2 == 0)
                    {
                        I = I + 2 * f(fi);
                    }
                    else
                    {
                        I = I + 4 * f(fi);
                    }
                    fi = fi + h;
                    j = j + 1;
                } while (fi < b);
                I = I + f(a) + f(b);
                I = I * (h / 3);
                result[0, i] = I;
                result[1, i] = h;
                I = 0;
                h += 0.001f;
            }
        }";
        public static string Integration_Simpson2 = @"
        public Simpson2(FunctionOne f,double a, double b, int step_number)
        {
            double sum = 0;
            double step_size = (b - a) / step_number;
            for (int i = 0; i < step_number; i = i + 2) //Simpson algorithm samples the integrand in several point which significantly improves //precision.
                sum = sum + (f(a + i * step_size) + 4 * f(a + (i + 1) * step_size) + f(a + (i + 2) * step_size)) * step_size / 3; //divide the area under f(x)     //into step_number rectangles and sum their areas 
            this.Sum = sum;
        }   ";
        public static string Integration_Trapezium = @"
        public Trapezium(FunctionOne f, double a, double b, int pointsNum)
        {
            double h;
            double j;
            double rez;
            int she = 4;
            result = new double[2, pointsNum+1];
            for (int i = 0; i <= pointsNum; i++)
            {
                h = (double)(b - a) / (double)she;
                she += 4;
                rez = f(a) + f(b);
                for (j = a + h; j < b; j = j + h)
                {
                    rez = rez + 2 * f(j);
                }
                rez = rez * h / 2;
                result[0,i] = rez;
                result[1,i] = h;
            }
        }";
        /// <summary>
        /// System Linear Equalizations
        /// </summary>
        public static string SystemLinearEqualizations_Gaus = @" 
        public Gaus(int n, double[,] mas)
        {
           // mas = new double[n, n+1];
            //this.mas = mas;
            double[] x = new double[n];
            int[] otv = new int[2*n];
            result = new double[n];
            //At first all of roots one after another
            for (int i = 0; i < n + 1; i++)
                otv[i] = i;
            //Direct motion of method of Gausse
            for (int k = 0; k < n; k++)
            { //What position a staple must stand on
                glavelem(k, mas, n, otv);//Setting of staple
                for (int j = n; j >= k; j--)
                    mas[k, j] /= mas[k, k];
                for (int i = k + 1; i < n; i++)
                    for (int j = n; j >= k; j--)
                        mas[i, j] -= mas[k, j] * mas[i, k];
            }
            //Countermove
            for (int i = 0; i < n; i++)
                x[i] = mas[i, n];
            for (int i = n - 2; i >= 0; i--)
                for (int j = i + 1; j < n; j++)
                    x[i] -= x[j] * mas[i, j];
            //Conclusion of result
            int p = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (i == otv[j])
                    { //We place roots one after another
                        result[p] = x[j];
                        p++;
                        break;
                    }
        }
        /// <summary>
        /// Description glavelem
        /// </summary>

        void glavelem(int k, double[,] mas, int n, int[] otv)
        {
            int i = 0;
            int j, i_max = k, j_max = k;
            double temp;
            //We search a maximal on the module element.    
            for (i = k; i < n; i++)
                for (j = k; j < n; j++)
                    if (Math.Abs(mas[i_max, j_max]) < Math.Abs(mas[i, j]))
                    {
                        i_max = i;
                        j_max = j;
                    }
            //We move lines.        
            for (j = k; j < n + 1; j++)
            {
                temp = mas[k, j];
                mas[k, j] = mas[i_max, j];
                mas[i_max, j] = temp;
            }
            //We move columns.   
            for (i = 0; i < n; i++)
            {
                temp = mas[i, k];
                mas[i, k] = mas[i, j_max];
                mas[i, j_max] = temp;
            }
            //We take into account the change of order of roots.  
            i = otv[k];
            otv[k] = otv[j_max];
            otv[j_max] = i;
        }  ";
        public static string SystemLinearEqualizations_Zeidel = @"
        public Zeidel(int n, double[,] A,double[] B)
        {
            result = new double[n];
           // this.n = n;
            //A = new double[n, n];
           // B = new double[n];
            int[] otv = new int[n];
            double[] X = new double[n];

            double eps = 0.0001f;
            for (int i = 0; i < n; i++)
            {
                X[i] = 0;
            }
            if (ZeidelMethod(A, B, X, eps, n) == 1)
            {
                for (int i = 0; i < n; i++)
                {
                    result[i] = X[i];
                }
            }
        }
        /// <summary>
        /// Description Zeidel Method
        /// </summary>
        public int ZeidelMethod(double[,] a, double[] b, double[] x, double e, int n)
        {
            int i, j;
            double s1, s2, v, m;
            
            do
            {
                m = 0;
                for (i = 0; i < n; i++)
                {
                    s1 = 0;
                    s2 = 0;
                    for (j = 0; j < i; j++) s1 += a[i, j] * x[j];
                    for (j = i; j < n; j++) s2 += a[i, j] * x[j];
                    v = x[i];
                    x[i] -= (1 / a[i, i]) * (s1 + s2 - b[i]);
                    if ((v - x[i]) > m)
                        m = (v - x[i]);
                }
            }
            while (m >= e);
            return 1;
        } ";
        /// <summary>
        /// Matrix Algebra
        /// </summary>
        public static string MatrixAlgebra_MatrixDeterminan = @"
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
        }";
        public static string MatrixAlgebra_RMatrixLU = @"
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
        }";
        public static string MatrixAlgebra_RMatrixLuInverse = @"
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
        }";

        /// <summary>
        /// NonLinear Equations
        /// </summary>
        public static string NonLinearEquations_HalfDivision = @"
        public HalfDivision(FunctionOne Fr,double x0, double x1, double d)
        {
            int t=0;
            int j = 0;
            double x2;
            double y = Math.Abs(x0 - x1);
            while (y > d)
            {
                t++;
                x2 = (x0 + x1) / 2;
                if (Fr(x0) * Fr(x2) > 0)
                    x0 = x2;
                else
                    x1 = x2;
                y = Math.Abs(x0 - x1);
            }
            result[0 , j] = x1;
            result[1 , j] = t;
        }";
        public static string NonLinearEquations_MetodHord = @"
         public HordMetod(FunctionOne Fr, double xa, double xb,  int iter)
        {
            double xlast;
            double x = 0;
            double e = 1f;
            if (Fr(xa) * Fr(xb) >= 0)
            {
                result[0, 0] = 0;
                result[1, 0] = 0;
                // return 0;
            }
            iter = 0;
            do
            {
                xlast = x;
                x = xb - Fr(xb) * (xb - xa) / (Fr(xb) - Fr(xa));
                if (Fr(x) * Fr(xa) > 0)
                    xa = x;
                else
                    xb = x;
                iter++;
            }
            while (Math.Abs(x - xlast) > e);
            result[0, 0] = x;
            result[1, 0] = iter;
           // return 1;
        }";
        public static string NonLinearEquations_Newton = @"
         public NewtonMethod(FunctionOne Fr, double x, double d)
        {
            int t = 0;
            double x1, y;
            do
            {
                t++;
                x1 = x - Fr(x) / Fr1(x, d,Fr);
                x = x1;
                y = Fr(x);
            }
            while (Math.Abs(y) >= d);
            result[0,0] = x;
            result[1, 0] = t;
        }
        /// <summary>
        /// Description Fr1
        /// </summary>
        public double Fr1(double x, double d, FunctionOne Fr)
        {
            return (Fr(x + d) - Fr(x)) / d;
        }";
        public static string NonLinearEquations_Secant = @"
        public Secant(FunctionOne Fr,double shag, double delta)
        {
            for (double i = 0; i <= 10; i = i + dl)
            {
                if (Fr(i) * Fr(i + dl) < 0)
                {
                    SecantMethod(i + dl, Fr, shag,delta);
                }
            }
        }
        /// <summary>
        /// Description SecantMethod
        /// </summary>
        void SecantMethod(double x0, FunctionOne Fr, double shag, double delta)
        {
            int j=0;
            double x1;
            x1 = x0 - Fr(x0) / fsh(x0,Fr,shag);
            while (Math.Abs(x1 - x0) <= delta)
            {
                x1 = x0 - Fr(x0) / fsh(x0,Fr,shag);
                if (Math.Abs(x1 - x0) > delta)
                    x0 = x1;
            }
            result[0, 0] = x1;
            result[1, 0] = j;
        }
        /// <summary>
        /// Description fsh
        /// </summary>
        double fsh(double x, FunctionOne Fr, double shag)
        {
            return (Fr(x + shag) - Fr(x)) / shag;
        }";
        /// <summary>
        /// Optimizing
        /// </summary>
        public static string Optimizing_Brentopt = @"
        public Brentopt(FunctionOne f, double a, double b, double epsilon)
        {
            double ia = 0;
            double ib = 0;
            double bx = 0;
            double d = 0;
            double e = 0;
            double etemp = 0;
            double fu = 0;
            double fv = 0;
            double fw = 0;
            double fx = 0;
            int iter = 0;
            double p = 0;
            double q = 0;
            double r = 0;
            double u = 0;
            double v = 0;
            double w = 0;
            double x = 0;
            double xm = 0;
            double cgold = 0;

            cgold = 0.3819660;
            bx = 0.5 * (a + b);
            if (a < b)
            {
                ia = a;
            }
            else
            {
                ia = b;
            }
            if (a > b)
            {
                ib = a;
            }
            else
            {
                ib = b;
            }
            v = bx;
            w = v;
            x = v;
            e = 0.0;
            fx = f(x);
            fv = fx;
            fw = fx;
            for (iter = 1; iter <= 100; iter++)
            {
                xm = 0.5 * (ia + ib);
                if (Math.Abs(x - xm) <= epsilon * 2 - 0.5 * (ib - ia))
                {
                    break;
                }
                if (Math.Abs(e) > epsilon)
                {
                    r = (x - w) * (fx - fv);
                    q = (x - v) * (fx - fw);
                    p = (x - v) * q - (x - w) * r;
                    q = 2 * (q - r);
                    if (q > 0)
                    {
                        p = -p;
                    }
                    q = Math.Abs(q);
                    etemp = e;
                    e = d;
                    if (!(Math.Abs(p) >= Math.Abs(0.5 * q * etemp) | p <= q * (ia - x) | p >= q * (ib - x)))
                    {
                        d = p / q;
                        u = x + d;
                        if (u - ia < epsilon * 2 | ib - u < epsilon * 2)
                        {
                            d = mysign(epsilon, xm - x);
                        }
                    }
                    else
                    {
                        if (x >= xm)
                        {
                            e = ia - x;
                        }
                        else
                        {
                            e = ib - x;
                        }
                        d = cgold * e;
                    }
                }
                else
                {
                    if (x >= xm)
                    {
                        e = ia - x;
                    }
                    else
                    {
                        e = ib - x;
                    }
                    d = cgold * e;
                }
                if (Math.Abs(d) >= epsilon)
                {
                    u = x + d;
                }
                else
                {
                    u = x + mysign(epsilon, d);
                }
                fu = f(u);
                if (fu <= fx)
                {
                    if (u >= x)
                    {
                        ia = x;
                    }
                    else
                    {
                        ib = x;
                    }
                    v = w;
                    fv = fw;
                    w = x;
                    fw = fx;
                    x = u;
                    fx = fu;
                }
                else
                {
                    if (u < x)
                    {
                        ia = u;
                    }
                    else
                    {
                        ib = u;
                    }
                    if (fu <= fw | w == x)
                    {
                        v = w;
                        fv = fw;
                        w = u;
                        fw = fu;
                    }
                    else
                    {
                        if (fu <= fv | v == x | v == 2)
                        {
                            v = u;
                            fv = fu;
                        }
                    }
                }
            }
            resultMin = x;
            result = fx;
        }


        private double mysign(double a,
            double b)
        {
            double result = 0;

            if (b > 0)
            {
                result = Math.Abs(a);
            }
            else
            {
                result = -Math.Abs(a);
            }
            return result;
        }";
        public static string Optimizing_GoldenSection = @"
         public GoldenSection(FunctionOne f,double a,double b, int n)
        {
            int i = 0;
            double s1 = 0;
            double s2 = 0;
            double u1 = 0;
            double u2 = 0;
            double fu1 = 0;
            double fu2 = 0;

            s1 = (3 - Math.Sqrt(5)) / 2;
            s2 = (Math.Sqrt(5) - 1) / 2;
            u1 = a + s1 * (b - a);
            u2 = a + s2 * (b - a);
            fu1 = f(u1);
            fu2 = f(u2);
            for (i = 1; i <= n; i++)
            {
                if (fu1 <= fu2)
                {
                    b = u2;
                    u2 = u1;
                    fu2 = fu1;
                    u1 = a + s1 * (b - a);
                    fu1 = f(u1);
                }
                else
                {
                    a = u1;
                    u1 = u2;
                    fu1 = fu2;
                    u2 = a + s2 * (b - a);
                    fu2 = f(u2);
                }
            }
            resultA = a;
            resultB = b;
        }";
        public static string Optimizing_Pijavsky = @"
        public Pijavsky(FunctionOne f, double a,double b,int n,double l)
        {
            double[] points = new double[0];
            double[] values = new double[0];
            double[] ratings = new double[0];
            int i = 0;
            int j = 0;
            double t = 0;
            double maxrating = 0;
            int maxratingpos = 0;
            double minpoint = 0;
            double minvalue = 0;

            points = new double[n + 1 + 1];
            values = new double[n + 1 + 1];
            ratings = new double[n + 1 + 1];
            points[0] = a;
            points[1] = b;
            values[0] = f(a);
            values[1] = f(b);
            for (i = 2; i <= n + 1; i++)
            {
                for (j = 1; j <= i - 1; j++)
                {
                    ratings[j] = l / 2 * (points[j] - points[j - 1]) - (double)(1) / (double)(2) * (values[j] + values[j - 1]);
                }
                maxrating = ratings[1];
                maxratingpos = 1;
                for (j = 2; j <= i - 1; j++)
                {
                    if (ratings[j] > maxrating)
                    {
                        maxratingpos = j;
                        maxrating = ratings[j];
                    }
                }
                points[i] = (double)(1) / (double)(2) * (points[maxratingpos] + points[maxratingpos - 1]) - (double)(1) / (double)(2) / l * (values[maxratingpos] - values[maxratingpos - 1]);
                values[i] = f(points[i]);
                for (j = i; j >= 2; j--)
                {
                    if (points[j] < points[j - 1])
                    {
                        t = points[j];
                        points[j] = points[j - 1];
                        points[j - 1] = t;
                        t = values[j];
                        values[j] = values[j - 1];
                        values[j - 1] = t;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            minpoint = points[0];
            minvalue = values[0];
            for (i = 1; i <= n + 1; i++)
            {
                if (values[i] < minvalue)
                {
                    minvalue = values[i];
                    minpoint = points[i];
                }
            }
            result = minpoint;
        }";
        /// <summary>
        /// Interpolation
        /// </summary>
        public static string Interpolation_LagrangeInterpolator = @"
         public LagrangeInterpolator( double[] x, double[] f,int n, double t)
        {

            f = (double[])f.Clone();

            NevilleInterpolator neville = new NevilleInterpolator(x, f, n, t);
            result = neville.GetSolution();
        }";
        public static string Interpolation_NevilleInterpolator = @"
        public NevilleInterpolator( double[] x,double[] f,int n,double t)
        {
            int m = 0;
            int i = 0;

            f = (double[])f.Clone();

            n = n - 1;
            for (m = 1; m <= n; m++)
            {
                for (i = 0; i <= n - m; i++)
                {
                    f[i] = ((t - x[i + m]) * f[i] + (x[i] - t) * f[i + 1]) / (x[i] - x[i + m]);
                }
            }
            result = f[0];
        }";
        public static string Interpolation_NewtonInterpolator = @"
        public NewtonInterpolator(double[] x, double[] f, int n, double t)
        {
            double F, LN, XX, X = 1;
            int i, j, k;
            for (i = 1, LN = f[0]; i < n; i++)
            {
                X *= (t - x[i - 1]);
                for (j = 0, F = 0; j <= i; j++)
                {
                    for (k = 0, XX = 1; k <= i; k++)
                    {
                        if (k != j)
                            XX *= x[j] - x[k];
                    }
                    F += f[j] / XX;
                }
                LN += X * F;
            }
            result = LN;
        }";
        public static string Interpolation_SplineInterpolator = @"
        public SplineInterpolator(double[] x, double[] y, int N, double t)
        {
            double[] c=new double[1000];
            BuildLinearSpline( x, y, N,ref c);
            int n = 0;
            int l = 0;
            int r = 0;
            int m = 0;
            n = (int)Math.Round(c[2]);
            //
            // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
            //
            l = 3;
            r = 3 + n - 2 + 1;
            while (l != r - 1)
            {
                m = (l + r) / 2;
                if (c[m] >= t)
                {
                    r = m;
                }
                else
                {
                    l = m;
                }
            }

            //
            // Interpolation
            //
            t = t - c[l];
            m = 3 + n + 4 * (l - 3);
            result = c[m] + t * (c[m + 1] + t * (c[m + 2] + t * c[m + 3]));
        }

        /*************************************************************************
   Построение таблицы коэффициентов кусочно-линейного сплайна

   Входные параметры:
       X           -   абсциссы, массив с нумерацией элементов [0..N-1]
       Y           -   значения функции,
                       массив с нумерацией элементов [0..N-1]
       N           -   число точек, N>=2

   Выходные параметры:
       C           -   таблица коэффициентов сплайна для использования в
                       подпрограмме SplineInterpolation
   *************************************************************************/
        public void BuildLinearSpline(double[] x,double[] y, int n,ref double[] c)
        {
            int i = 0;
            int tblsize = 0;
            x = (double[])x.Clone();
            y = (double[])y.Clone();
            //
            // Sort points
            //
            SortPoints(ref x, ref y, n);
            tblsize = 3 + n + (n - 1) * 4;
            c = new double[tblsize - 1 + 1];
            c[0] = tblsize;
            c[1] = 3;
            c[2] = n;
            for (i = 0; i <= n - 1; i++)
            {
                c[3 + i] = x[i];
            }
            for (i = 0; i <= n - 2; i++)
            {
                c[3 + n + 4 * i + 0] = y[i];
                c[3 + n + 4 * i + 1] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
                c[3 + n + 4 * i + 2] = 0;
                c[3 + n + 4 * i + 3] = 0;
            }
        }

        /*************************************************************************
        Sort  points
        *************************************************************************/
        private void SortPoints(ref double[] x,ref double[] y,int n)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int t = 0;
            double tmp = 0;
            bool isascending = new bool();
            bool isdescending = new bool();
            isascending = true;
            isdescending = true;
            for (i = 1; i <= n - 1; i++)
            {
                isascending = isascending & x[i] > x[i - 1];
                isdescending = isdescending & x[i] < x[i - 1];
            }
            if (isascending)
            {
                return;
            }
            if (isdescending)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    j = n - 1 - i;
                    if (j <= i)
                    {
                        break;
                    }
                    tmp = x[i];
                    x[i] = x[j];
                    x[j] = tmp;
                    tmp = y[i];
                    y[i] = y[j];
                    y[j] = tmp;
                }
                return;
            }
            if (n == 1)
            {
                return;
            }
            i = 2;
            do
            {
                t = i;
                while (t != 1)
                {
                    k = t / 2;
                    if (x[k - 1] >= x[t - 1])
                    {
                        t = 1;
                    }
                    else
                    {
                        tmp = x[k - 1];
                        x[k - 1] = x[t - 1];
                        x[t - 1] = tmp;
                        tmp = y[k - 1];
                        y[k - 1] = y[t - 1];
                        y[t - 1] = tmp;
                        t = k;
                    }
                }
                i = i + 1;
            }
            while (i <= n);
            i = n - 1;
            do
            {
                tmp = x[i];
                x[i] = x[0];
                x[0] = tmp;
                tmp = y[i];
                y[i] = y[0];
                y[0] = tmp;
                t = 1;
                while (t != 0)
                {
                    k = 2 * t;
                    if (k > i)
                    {
                        t = 0;
                    }
                    else
                    {
                        if (k < i)
                        {
                            if (x[k] > x[k - 1])
                            {
                                k = k + 1;
                            }
                        }
                        if (x[t - 1] >= x[k - 1])
                        {
                            t = 0;
                        }
                        else
                        {
                            tmp = x[k - 1];
                            x[k - 1] = x[t - 1];
                            x[t - 1] = tmp;
                            tmp = y[k - 1];
                            y[k - 1] = y[t - 1];
                            y[t - 1] = tmp;
                            t = k;
                        }
                    }
                }
                i = i - 1;
            }
            while (i >= 1);
        }";
        public static string Interpolation_BarycentricInterpolation = @"
         public BarycentricInterpolation(double[] x,double[] f,double[] w,int n,double t)
        {
            double s1 = 0;
            double s2 = 0;
            double v = 0;
            double threshold = 0;
            double s = 0;
            int i = 0;
            int j = 0;

            threshold = Math.Sqrt(AP.Math.MinRealNumber);
            j = 0;
            s = t - x[0];
            for (i = 1; i <= n - 1; i++)
            {
                if (Math.Abs(t - x[i]) < Math.Abs(s))
                {
                    s = t - x[i];
                    j = i;
                }
            }
            if (s == 0)
            {
                result = f[j];
            }
            if (Math.Abs(s) > threshold)
            {

                //
                // use fast formula
                //
                j = -1;
                s = 1.0;
            }

            //
            // Calculate using safe or fast barycentric formula
            //
            s1 = 0;
            s2 = 0;
            for (i = 0; i <= n - 1; i++)
            {
                if (i != j)
                {
                    v = s * w[i] / (t - x[i]);
                    s1 = s1 + v * f[i];
                    s2 = s2 + v;
                }
                else
                {
                    v = w[i];
                    s1 = s1 + v * f[i];
                    s2 = s2 + v;
                }
            }
            result = s1 / s2;
        }";
        /// <summary>
        /// Statistics
        /// </summary>
        public static string Statistics_CorrelationPearson = @"
        /*************************************************************************
        Pearson product-moment correlation coefficient

        Input parameters:
            X       -   sample 1 (array indexes: [0..N-1])
            Y       -   sample 2 (array indexes: [0..N-1])
            N       -   sample size.

        Result:
            Pearson product-moment correlation coefficient

        *************************************************************************/
        public CorrelationPearson(double[] x, double[] y, int n)
        {
            int i = 0;
            double xmean = 0;
            double ymean = 0;
            double s = 0;
            double xv = 0;
            double yv = 0;
            double t1 = 0;
            double t2 = 0;

            xv = 0;
            yv = 0;
            if (n <= 1)
            {
                result = 0;
            }

            //
            // Mean
            //
            xmean = 0;
            ymean = 0;
            for (i = 0; i <= n - 1; i++)
            {
                xmean = xmean + x[i];
                ymean = ymean + y[i];
            }
            xmean = xmean / n;
            ymean = ymean / n;

            //
            // numerator and denominator
            //
            s = 0;
            xv = 0;
            yv = 0;
            for (i = 0; i <= n - 1; i++)
            {
                t1 = x[i] - xmean;
                t2 = y[i] - ymean;
                xv = xv + AP.Math.Sqr(t1);
                yv = yv + AP.Math.Sqr(t2);
                s = s + t1 * t2;
            }
            if (xv == 0 | yv == 0)
            {
                result = 0;
            }
            else
            {
                result = s / (Math.Sqrt(xv) * Math.Sqrt(yv));
            }
        }";
        public static string Statistics_CorrelationSpearmansRank = @"
         /*************************************************************************
        Spearman's rank correlation coefficient

        Input parameters:
            X       -   sample 1 (array indexes: [0..N-1])
            Y       -   sample 2 (array indexes: [0..N-1])
            N       -   sample size.

        Result:
            Spearman's rank correlation coefficient
        *************************************************************************/
        public CorrelationSpearmansRank(double[] x, double[] y, int n)
        {
            x = (double[])x.Clone();
            y = (double[])y.Clone();

            rankx(ref x, n);
            rankx(ref y, n);
            CorrelationPearson corelP = new CorrelationPearson(x, y, n);
            result =corelP.GetSolution() ;
        }


        /*************************************************************************
        Internal ranking subroutine
        *************************************************************************/
        private static void rankx(ref double[] x,int n)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int t = 0;
            double tmp = 0;
            int tmpi = 0;
            double[] r = new double[0];
            int[] c = new int[0];


            //
            // Prepare
            //
            if (n <= 1)
            {
                x[0] = 1;
                return;
            }
            r = new double[n - 1 + 1];
            c = new int[n - 1 + 1];
            for (i = 0; i <= n - 1; i++)
            {
                r[i] = x[i];
                c[i] = i;
            }

            //
            // sort {R, C}
            //
            if (n != 1)
            {
                i = 2;
                do
                {
                    t = i;
                    while (t != 1)
                    {
                        k = t / 2;
                        if (r[k - 1] >= r[t - 1])
                        {
                            t = 1;
                        }
                        else
                        {
                            tmp = r[k - 1];
                            r[k - 1] = r[t - 1];
                            r[t - 1] = tmp;
                            tmpi = c[k - 1];
                            c[k - 1] = c[t - 1];
                            c[t - 1] = tmpi;
                            t = k;
                        }
                    }
                    i = i + 1;
                }
                while (i <= n);
                i = n - 1;
                do
                {
                    tmp = r[i];
                    r[i] = r[0];
                    r[0] = tmp;
                    tmpi = c[i];
                    c[i] = c[0];
                    c[0] = tmpi;
                    t = 1;
                    while (t != 0)
                    {
                        k = 2 * t;
                        if (k > i)
                        {
                            t = 0;
                        }
                        else
                        {
                            if (k < i)
                            {
                                if (r[k] > r[k - 1])
                                {
                                    k = k + 1;
                                }
                            }
                            if (r[t - 1] >= r[k - 1])
                            {
                                t = 0;
                            }
                            else
                            {
                                tmp = r[k - 1];
                                r[k - 1] = r[t - 1];
                                r[t - 1] = tmp;
                                tmpi = c[k - 1];
                                c[k - 1] = c[t - 1];
                                c[t - 1] = tmpi;
                                t = k;
                            }
                        }
                    }
                    i = i - 1;
                }
                while (i >= 1);
            }

            //
            // compute tied ranks
            //
            i = 0;
            while (i <= n - 1)
            {
                j = i + 1;
                while (j <= n - 1)
                {
                    if (r[j] != r[i])
                    {
                        break;
                    }
                    j = j + 1;
                }
                for (k = i; k <= j - 1; k++)
                {
                    r[k] = 1 + ((double)(i + j - 1)) / (double)(2);
                }
                i = j;
            }

            //
            // back to x
            //
            for (i = 0; i <= n - 1; i++)
            {
                x[c[i]] = r[i];
            }
        }";
        public static string Statistics_DescriptiveStatisticsADev = @"
        /*************************************************************************
        ADev

        Input parameters:
            X   -   sample (array indexes: [0..N-1])
            N   -   sample size
        
        Output parameters:
            ADev-   ADev
        *************************************************************************/
        public DescriptiveStatisticsADev( double[] x,int n)
        {
            int i = 0;
            double mean = 0;
            double adev = 0;
            if (n <= 0)
            {
                return;
            }
            //
            // Mean
            //
            for (i = 0; i <= n - 1; i++)
            {
                mean = mean + x[i];
            }
            mean = mean / n;
            //
            // ADev
            //
            for (i = 0; i <= n - 1; i++)
            {
                adev = adev + Math.Abs(x[i] - mean);
            }
            adev = adev / n;
            result = adev;
        }";

        public static string Statistics_DescriptiveStatisticsMedian = @"
        /*************************************************************************
         Median calculation.

         Input parameters:
             X   -   sample (array indexes: [0..N-1])
             N   -   sample size

         Output parameters:
             Median
         *************************************************************************/
        public DescriptiveStatisticsADevMedian(double[] x,int n)
        {
            int i = 0;
            int ir = 0;
            int j = 0;
            int l = 0;
            int midp = 0;
            int k = 0;
            double a = 0;
            double tval = 0;
            double median = 0;
            x = (double[])x.Clone();
            //
            // Some degenerate cases
            //
            if (n <= 0)
            {
                return;
            }
            if (n == 1)
            {
                median = x[0];
                return;
            }
            if (n == 2)
            {
                median = 0.5 * (x[0] + x[1]);
                result = median;
            }
            //
            // Common case, N>=3.
            // Choose X[(N-1)/2]
            //
            l = 0;
            ir = n - 1;
            k = (n - 1) / 2;
            while (true)
            {
                if (ir <= l + 1)
                {
                    //
                    // 1 or 2 elements in partition
                    //
                    if (ir == l + 1 & x[ir] < x[l])
                    {
                        tval = x[l];
                        x[l] = x[ir];
                        x[ir] = tval;
                    }
                    break;
                }
                else
                {
                    midp = (l + ir) / 2;
                    tval = x[midp];
                    x[midp] = x[l + 1];
                    x[l + 1] = tval;
                    if (x[l] > x[ir])
                    {
                        tval = x[l];
                        x[l] = x[ir];
                        x[ir] = tval;
                    }
                    if (x[l + 1] > x[ir])
                    {
                        tval = x[l + 1];
                        x[l + 1] = x[ir];
                        x[ir] = tval;
                    }
                    if (x[l] > x[l + 1])
                    {
                        tval = x[l];
                        x[l] = x[l + 1];
                        x[l + 1] = tval;
                    }
                    i = l + 1;
                    j = ir;
                    a = x[l + 1];
                    while (true)
                    {
                        do
                        {
                            i = i + 1;
                        }
                        while (x[i] < a);
                        do
                        {
                            j = j - 1;
                        }
                        while (x[j] > a);
                        if (j < i)
                        {
                            break;
                        }
                        tval = x[i];
                        x[i] = x[j];
                        x[j] = tval;
                    }
                    x[l + 1] = x[j];
                    x[j] = a;
                    if (j >= k)
                    {
                        ir = j - 1;
                    }
                    if (j <= k)
                    {
                        l = i;
                    }
                }
            }
            //
            // If N is odd, return result
            //
            if (n % 2 == 1)
            {
                median = x[k];
                result = median;
            }
            a = x[n - 1];
            for (i = k + 1; i <= n - 1; i++)
            {
                if (x[i] < a)
                {
                    a = x[i];
                }
            }
            median = 0.5 * (x[k] + a);
            result = median;
        }";
        public static string Statistics_DescriptiveStatisticsMoments = @"
        /*************************************************************************
        Calculation of the distribution moments: mean, variance, slewness, kurtosis.

        Input parameters:
            X       -   sample. Array with whose indexes range within [0..N-1]
            N       -   sample size.
        
        Output parameters:
            Mean    -   mean.
            Variance-   variance.
            Skewness-   skewness (if variance<>0; zero otherwise).
            Kurtosis-   kurtosis (if variance<>0; zero otherwise).


        *************************************************************************/
        public DescriptiveStatisticsMoments(double[] x,int n)
        {
            int i = 0;
            double v = 0;
            double v1 = 0;
            double v2 = 0;
            double stddev = 0;
            double mean = 0;
            if (n <= 0)
            {
                return;
            }
            //
            // Mean
            //
            for (i = 0; i <= n - 1; i++)
            {
                mean = mean + x[i];
            }
            mean = mean / n;
            //
            // Variance (using corrected two-pass algorithm)
            //
            if (n != 1)
            {
                v1 = 0;
                for (i = 0; i <= n - 1; i++)
                {
                    v1 = v1 + AP.Math.Sqr(x[i] - mean);
                }
                v2 = 0;
                for (i = 0; i <= n - 1; i++)
                {
                    v2 = v2 + (x[i] - mean);
                }
                v2 = AP.Math.Sqr(v2) / n;
                variance = (v1 - v2) / (n - 1);
                if (variance < 0)
                {
                    variance = 0;
                }
                stddev = Math.Sqrt(variance);
            }
            //
            // Skewness and kurtosis
            //
            if (stddev != 0)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    v = (x[i] - mean) / stddev;
                    v2 = AP.Math.Sqr(v);
                    skewness = skewness + v2 * v;
                    kurtosis = kurtosis + AP.Math.Sqr(v2);
                }
                skewness = skewness / n;
                kurtosis = kurtosis / n - 3;
            }
            result = mean;
        }";
        public static string Statistics_DescriptiveStatisticsPercentile = @"
        /*************************************************************************
         Percentile calculation.

         Input parameters:
             X   -   sample (array indexes: [0..N-1])
             N   -   sample size, N>1
             P   -   percentile (0<=P<=1)

         Output parameters:
             V   -   percentile

         *************************************************************************/
        public DescriptiveStatisticsPercentile(double[] x,int n,double p)
        {
            int i1 = 0;
            double t = 0;
            double v = 0;
            x = (double[])x.Clone();
            InternalStatSort(ref x, n);
            if (p == 0)
            {
                v = x[0];
                result = v;
            }
            if (p == 1)
            {
                v = x[n - 1];
                result = v;
            }
            t = p * (n - 1);
            i1 = (int)Math.Floor(t);
            t = t - (int)Math.Floor(t);
            v = x[i1] * (1 - t) + x[i1 + 1] * t;
            result = v;
        }
        private  void InternalStatSort(ref double[] arr,int n)
        {
            int i = 0;
            int k = 0;
            int t = 0;
            double tmp = 0;

            if (n == 1)
            {
                return;
            }
            i = 2;
            do
            {
                t = i;
                while (t != 1)
                {
                    k = t / 2;
                    if (arr[k - 1] >= arr[t - 1])
                    {
                        t = 1;
                    }
                    else
                    {
                        tmp = arr[k - 1];
                        arr[k - 1] = arr[t - 1];
                        arr[t - 1] = tmp;
                        t = k;
                    }
                }
                i = i + 1;
            }
            while (i <= n);
            i = n - 1;
            do
            {
                tmp = arr[i];
                arr[i] = arr[0];
                arr[0] = tmp;
                t = 1;
                while (t != 0)
                {
                    k = 2 * t;
                    if (k > i)
                    {
                        t = 0;
                    }
                    else
                    {
                        if (k < i)
                        {
                            if (arr[k] > arr[k - 1])
                            {
                                k = k + 1;
                            }
                        }
                        if (arr[t - 1] >= arr[k - 1])
                        {
                            t = 0;
                        }
                        else
                        {
                            tmp = arr[k - 1];
                            arr[k - 1] = arr[t - 1];
                            arr[t - 1] = tmp;
                            t = k;
                        }
                    }
                }
                i = i - 1;
            }
            while (i >= 1);
        }";
        /// <summary>
        /// Random Generator
        /// </summary>
        public static string RandomGeneratorsMethod1 = @"
        *************************************************************************
        Генератор равномерно распределенных случайных вещественных чисел
        в диапазоне (0, 1)
        *************************************************************************/
        public RandomGeneratorsMethod1()
        {
            result = rndbasemaxr * rndintegerbase();
        }
        /*************************************************************************
        Генерация случайного целого числа  в  диапазоне  (0, RndIntegerMax())  (не
        включая  границы  интервала).  Эта  подпрограмма является основой для всех
        подпрограмм этого модуля и обладает максимальным быстродействием.
       L'Ecuyer, Efficient and portable combined random number generators
        *************************************************************************/
        public  int rndintegerbase()
        {
            int results = 0;
            int k = 0;
            //
            // Initialize S1 and S2 if needed
            //
            if (rndbases1 < 1 | rndbases1 >= rndbasem1)
            {
                rndbases1 = 1 + AP.Math.RandomInteger(32000);
            }
            if (rndbases2 < 1 | rndbases2 >= rndbasem2)
            {
                rndbases2 = 1 + AP.Math.RandomInteger(32000);
            }
            //
            // Process S1, S2
            //
            k = rndbases1 / 53668;
            rndbases1 = 40014 * (rndbases1 - k * 53668) - k * 12211;
            if (rndbases1 < 0)
            {
                rndbases1 = rndbases1 + 2147483563;
            }
            k = rndbases2 / 52774;
            rndbases2 = 40692 * (rndbases2 - k * 52774) - k * 3791;
            if (rndbases2 < 0)
            {
                rndbases2 = rndbases2 + 2147483399;
            }

            //
            // Result
            //
            results = rndbases1 - rndbases2;
            if (results < 1)
            {
                results = results + 2147483562;
                return results;
            }
            return results;
        }";
        public static string RandomGeneratorsMethod2 = @"
        /*************************************************************************
        Генератор равномерно распределенных случайных целых чисел
        в диапазоне [0, N)
        *************************************************************************/
        public RandomGeneratorsMethod2(int n)
        {
            result = rndintegerbase() % n;
        }";
        public static string RandomGeneratorsMethod3 = @"
         /*************************************************************************
        Генератор нормально распределенных случайных чисел.

        Генерирует   два   независимых   случайных   числа,   имеющих  стандартное
        распределение.   По   затратам   времени  равен   подпрограмме  RndNormal,
        генерирующей одно случайное число.

        *************************************************************************/
        public RandomGeneratorsMethod3()
        {
            double x1=0;
            double x2=0;
            double u = 0;
            double v = 0;
            double s = 0;
            bool execute = true;
            while (execute==true)
            {
                RandomGeneratorsMethod1 rand1 = new RandomGeneratorsMethod1();
                u = 2 * rand1.GetSolution() - 1;
                RandomGeneratorsMethod1 rand11 = new RandomGeneratorsMethod1();
                v = 2 * rand11.GetSolution() - 1;
                s = AP.Math.Sqr(u) + AP.Math.Sqr(v);
                if (s > 0 & s < 1)
                {
                    s = Math.Sqrt(-(2 * Math.Log(s) / s));
                    x1 = u * s;
                    x2 = v * s;
                    execute = false;
                }
                result1 = x1;
                result2 = x2;
            }
            
        }";
        public static string RandomGeneratorsMethod4 = @"
         /*************************************************************************
        Генератор нормально распределенных случайных чисел.

        Генерирует одно случайное  число, имеющее  стандартное  распределение.  По
        затратам времени равен подпрограмме RndNormal2, генерирующей два случайных
        числа.

        *************************************************************************/
        public RandomGeneratorsMethod4()
        {
            RandomGeneratorsMethod3 rand3 = new RandomGeneratorsMethod3();
            result = rand3.GetSolution1();
        }";
        public static string RandomGeneratorsMethod5 = @"
         /*************************************************************************
        Генератор экспоненциально распределенных случайных чисел

        *************************************************************************/
        public RandomGeneratorsMethod5(double lambda)
        {
            RandomGeneratorsMethod1 rand1 = new RandomGeneratorsMethod1();
            result = -(Math.Log(rand1.GetSolution()) / lambda);
        }";

    }

}
