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

namespace System.NumericalMethods
{
    public delegate void OptimizationCompletedEventHandler(object sender, OptimizationCompletedEventArgs e);
    public class MultidimensionalOptimizer
    {
        #region props, vars, delegates & events
        /// <summary>
        /// This event is raised on the conclusion of the often lengthy optimization process.
        /// </summary>
        public event OptimizationCompletedEventHandler OptimizationCompleted;
        
        /// <summary>
        /// This delegate is a placeholder for the user defined function that our optimizer will attempt to optimize
        /// </summary>
        /// <param name="x"> Is the multidimensional point in space for which the fitness function is to be evaluated</param>
        /// <returns>The fitness for the input function</returns>
        private delegate double FitnessFunctionD(double[] x);
        public FitnessFunctionD f;
        #endregion

        #region ctor, setup and alikes
        MultidimensionalOptimizer()
        {

        }
        #endregion
        
        #region methods
        protected virtual void onOptimizationCompleted(OptimizationCompletedEventArgs e)
        {
            if (OptimizationCompleted != null)
                OptimizationCompleted(this, e);
            return;
        }
        public virtual void optimizeAsync();
        #endregion
    }
    public class OptimizationCompletedEventArgs: EventArgs
    {
        public double ResultFitness { get; private set; }
        public double[] ResultLocation { get; private set; }


        OptimizationCompletedEventArgs()
        {
        }
    }
}
