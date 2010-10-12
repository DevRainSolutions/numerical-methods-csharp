using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace NumericalMethods.Approximation
{
    public abstract class ApproximationBase
    {
        protected FunctionOne function;
        protected FunctionOne df;
        protected double left;
        protected double right;
        protected double epsilon;

        public double Result{ get; protected set; }
        public double IterationsNumber { get; protected set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="f">Function to be solved delegate.</param>
        /// <param name="left">Left border of the set interval.</param>
        /// <param name="right">Right border of the set interval.</param>
        /// <param name="eps">Exactness conducting of calculations.</param>
        public ApproximationBase(FunctionOne f, double left, double right, double eps = 0.00001)
        {
            this.function = f;
            this.left = left;
            this.right = right;
            this.epsilon = eps;
        }

        public ApproximationBase(FunctionOne f, FunctionOne df, double left, double right, double eps = 0.00001)
        {
            this.function = f;
            this.function = df;
            this.left = left;
            this.right = right;
            this.epsilon = eps;
        }

        protected virtual void Calculate()
        {

        }
    }
}
