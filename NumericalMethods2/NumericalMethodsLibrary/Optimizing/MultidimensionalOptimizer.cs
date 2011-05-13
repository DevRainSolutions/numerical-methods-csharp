using System;
using System.Threading;

namespace NumericalMethods.Optimization
{
    public delegate void OptimizationCompletedEventHandler(object sender, OptimizationCompletedEventArgs e);
    
    public abstract class MultidimensionalOptimizer 
    {
        #region props, vars, delegates & events
        /// <summary>
        /// This event is raised on the conclusion of the often lengthy optimization process.
        /// </summary>
        public event OptimizationCompletedEventHandler OptimizationCompleted;

        public Func<double[],double> f;

        #endregion

        #region ctor, setup and alikes
        public MultidimensionalOptimizer()
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
        public virtual void optimizeAsync() 
        {
            if (f == null)
                throw new Exception("Atempting to optimize and undefined fitness function.");
            else
            {
                var worker = new Thread(new ThreadStart(this.optimize));
                worker.Start();
            }
        }
        public abstract void optimize();
        #endregion
    }

    public class OptimizationCompletedEventArgs: EventArgs
    {
        public double ResultFitness { get; private set; }
        public double[] ResultLocation { get; private set; }

        //A private ctor that does nothing just to prohibit initializing
        //one this type of objects without the args
        private OptimizationCompletedEventArgs() { }

        //The usable ctor
        OptimizationCompletedEventArgs(double fitness, double[] loc)
        {
            ResultFitness = fitness;
            ResultLocation = loc;
        }
    }
}
