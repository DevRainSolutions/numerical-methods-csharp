using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumericalMethods.DifferentialEquations;
using NumericalMethods.MatrixAlgebra;
using NumericalMethods.SystemLinearEqualizations;

namespace NumericalMethods.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Function f = new Function((x, y) => Math.Sin(x) - Math.Cos(y));
            EulerModified eulerM = new EulerModified(f, 0, 1, 1, 5);
            EulerSimple eulerS = new EulerSimple(f, 0, 1, 1, 5);
            RungeKutta4 rk4 = new RungeKutta4(f, 0, 1, 1, 5);

           double[,] MAT = new double[,] { { 1, 23, 32, 1 }, { 1, 5, 1, 1 }, { 32, 1, 1, 1 }, { 1, 10, 1, 1 } };

           // double[,] MAT = new double[,] { { 3 ,0 ,2 }, { 2,0,-2 }, { 0,1,1 } };

            MatrixInverse MATINV = new MatrixInverse();
            MAT = MATINV.InvMatrix(MAT, 4);



             double[,] EQU = new double[,] { { 1 ,2 ,-1 }, { 3,5,-1 }, { -2,-1,-2 } };
             double[] ANS = new double[] { 6 ,2 ,4 };
            MatInvMethod SOLVER = new MatInvMethod ();
            SOLVER .MatrixInverseMethod (EQU,ANS ,3);
            double[] result = SOLVER.GetSolution();
           
           
        }
    }
}
