using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.LinearSystems
{
    public class SecantLinear
    {
        public bool HasSolution { get; set; }

        private double Solution { get; set; }

        public double GetSolution()
        {
            return this.Solution;
        }


        public SecantLinear(int step_number, double point1, double point2, FunctionOne f)
        {
            double p2, p1, p0, prec = .0001f; //set precision to .0001
            int i;
            p0 = f(point1);
            p1 = f(point2);
            p2 = p1 - f(p1) * (p1 - p0) / (f(p1) - f(p0)); //secant formula

            for (i = 0; System.Math.Abs(p2) > prec && i < step_number; i++) //iterate till precision goal is not met or the maximum //number of steps is reached
            {
                p0 = p1;
                p1 = p2;
                p2 = p1 - f(p1) * (p1 - p0) / (f(p1) - f(p0));
            }

            if (i <= step_number)
            {
                this.Solution = p2;
                this.HasSolution = true;
            }
            else
            {
                this.HasSolution = false;
            }
        }
    }
}
