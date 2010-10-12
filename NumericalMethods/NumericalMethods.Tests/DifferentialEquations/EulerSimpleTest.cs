using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumericalMethods.DifferentialEquations;

namespace NumericalMethods.Tests.DifferentialEquations
{
    [TestClass]
    public class EulerSimpleTest
    {
        [TestMethod]
        public void EulerModifiedTest1()
        {
            Function f = new Function((x , y) => Math.Sin(x) - Math.Cos(y));
            double eps = 0.001;
            EulerSimple euler = new EulerSimple(f, 0, 1, 1, 5);
            Assert.AreEqual(0, euler.Result[0,0]);
            Assert.AreEqual(1, euler.Result[0,1]);
            Assert.AreEqual(0.4, euler.Result[2, 0]);
            Assert.AreEqual(0.2, euler.Step);
            Assert.AreEqual(0.806, euler.Result[2,1]);
        }

    }
}
