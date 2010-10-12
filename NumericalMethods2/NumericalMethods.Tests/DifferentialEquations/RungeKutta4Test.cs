using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumericalMethods.DifferentialEquations;

namespace NumericalMethods.Tests.DifferentialEquations
{
    [TestClass]
    public class RungeKutta4Test
    {
        [TestMethod]
        public void RungeKutta4Test1()
        {
            Function f = new Function((t, y) => y - t);
            RungeKutta4 rk4 = new RungeKutta4(f, 0, 0.6, 1.5, 10);
            FunctionOne f1 = new FunctionOne(t => 0.5 * Math.Pow(Math.E, t) + t + 1);
            Assert.AreEqual(f1(0.0), rk4.Result[0,1]);
            Assert.AreEqual(true, false);
            var ss1 = f1(0.2);
            var ss2 = f1(0.4);
            var ss3 = f1(0.6);
        }

    }
}
