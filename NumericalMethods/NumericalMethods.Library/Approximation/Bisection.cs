using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.Approximation
{
    public class Bisection : ApproximationBase
    {
       
        public Bisection(FunctionOne f,double left, double right, double eps) :base(f,left,right,eps)
        {
            this.Calculate();
        }

        protected override void Calculate()
        {
            bool blnError = false;
            double c;
            do
            {
                this.IterationsNumber++;
                c = (left + right) / 2;
                if (function(left) * function(c) < 0)
                    right = c;
                else
                    left = c;
                if (function(c) == 0)
                {
                    blnError = (function(c - epsilon / 2) * function(c + epsilon / 2) < 0) ? false : true;
                    break;
                }
            }
            while ((function(c)) > epsilon || (right - left) > epsilon);
            if (blnError == false)
            {
                this.Result = c;
            }
        }
    }
}
