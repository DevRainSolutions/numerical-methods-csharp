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

namespace NumericalMethods.DifferentialEquations
{
    public class DifferentialEquationsBase
    {
        protected Function function;
        protected double begin;
        protected double end;
        protected double y0;
        protected int pointsNum;
        protected double h;

        public double[,] Result { get; protected set; }

        public double Step
        {
            get { return h; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="function">Function to be solved delegate</param>
        /// <param name="begin">Interval start point value</param>
        /// <param name="end">Interval end point value</param>
        /// <param name="y0">Starting condition</param>
        /// <param name="pointsNum">Points number</param>
        public DifferentialEquationsBase(Function function, double begin, double end, double y0, int pointsNum)
        {
            this.function = function;
            this.begin = begin;
            this.end = end;
            this.y0 = y0;
            this.pointsNum = pointsNum;
            h = (end - begin) / pointsNum;

            this.Calculate();
        }

        protected virtual void Calculate()
        {

        }
    }
}
