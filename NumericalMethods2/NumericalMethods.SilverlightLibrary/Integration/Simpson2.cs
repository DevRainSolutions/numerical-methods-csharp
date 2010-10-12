using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.Integration
{
    public class Simpson2
    {
        private double Sum { get; set; }

        public Simpson2(FunctionOne f,double a, double b, int step_number)
        {
            double sum = 0;
            double step_size = (b - a) / step_number;
            for (int i = 0; i < step_number; i = i + 2) //Simpson algorithm samples the integrand in several point which significantly improves //precision.
                sum = sum + (f(a + i * step_size) + 4 * f(a + (i + 1) * step_size) + f(a + (i + 2) * step_size)) * step_size / 3; //divide the area under f(x)     //into step_number rectangles and sum their areas 
            this.Sum = sum;
        }

        /// <summary>
        /// Returns equation solution
        /// </summary>
        /// <returns>Equation solution</returns>
        public double GetSolution() 
        {
            return this.Sum;
        }
    }
}
