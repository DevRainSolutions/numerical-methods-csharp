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

namespace NumericalMethods_Silverlight.Code
{
    public class xmlMethod
    {
        public method Approximate_Decision { get; set; }
        public method Differential_Equations { get; set; }
        public method Integration { get; set; }
        public string NonLinearEqualization { get; set; }
        public string Optimizing { get; set; }
        public string Interpolation { get; set; }
        public string Statistics { get; set; }
        public string MatrixAlgebra { get; set; }
        public string LinearSystems { get; set; }
    }
    public class method
    {
        public string ID { get; set; }
        public string Function { get; set; }
        public string ParamInput1 { get; set; }
        public string ParamInput2 { get; set; }
        public string ParamInput3 { get; set; }
        public string ParamInput4 { get; set; }
    }
}
