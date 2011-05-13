using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumericalMethods.Optimizing;


namespace NumericalMethods.Tests.Optimization
{
    [TestClass]
    public class CMAESOptimizerTest
    {
        private double initialFitness = 0;

        [TestMethod]
        public void CMAESTest1()
        {
            double[] s = new double[2]{3,3};
            initialFitness = Rosenbrock(s);
            CMAESOptimizer cmaes = new CMAESOptimizer( Rosenbrock , problemDimension:2, seed:s);
            cmaes.OptimizationCompleted += new OptimizationCompletedEventHandler(cmaes_OptimizationCompleted);
            cmaes.optimizeAsync();
        }

        void cmaes_OptimizationCompleted(object sender, OptimizationCompletedEventArgs e)
        {
            Console.WriteLine("Best result: {0:d}", e.ResultFitness);
            Debug.Assert(e.ResultFitness < initialFitness);
        }

        public double Rosenbrock(double[] x)
        {
            double res = 0;
            for (int i = 0; i < x.Length - 1; ++i)
                res += 100 * Math.Pow(x[i + 1] - x[i] * x[i], 2) + Math.Pow(1 - x[i], 2);
            return res;
        }
    }
  
}
