using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumericalMethods.DifferentialEquations;

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

           
        }
    }
}
