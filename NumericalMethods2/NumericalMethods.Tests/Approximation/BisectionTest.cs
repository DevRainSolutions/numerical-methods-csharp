using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumericalMethods.Approximation;

namespace NumericalMethods.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    /// http://twtmas.mpei.ac.ru/mas/worksheets/Bisection.mcd
    /// http://en.wikipedia.org/wiki/Bisection_method
    [TestClass]
    public class BisectionTest
    {
        public BisectionTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void BisectionTest1()
        {
            FunctionOne f = new FunctionOne(x=> 4*Math.Pow(x,3) + 3*Math.Pow(x,2) - 4*x - 5);
            double eps = 0.001;
            Bisection b = new Bisection(f, -0.1, 1.9, eps);
            Assert.AreEqual(true, Math.Abs(1.125 - b.Result) <= eps);
        }

        [TestMethod]
        public void BisectionTest2()
        {
            FunctionOne f = new FunctionOne(x => Math.Pow(x, 2));
            double eps = 0.00001;
            Bisection b = new Bisection(f, -1, 1, eps);
            Assert.AreEqual(0, b.Result);
            Assert.AreEqual(1, b.IterationsNumber);
        }
    }
}
