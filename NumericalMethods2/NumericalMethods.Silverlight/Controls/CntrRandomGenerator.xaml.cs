using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace NumericalMethods_Silverlight
{
	public partial class CntrRandomGenerator : UserControl
	{
        public double pointGenerator;
		public CntrRandomGenerator()
		{
			// Required to initialize variables
			InitializeComponent();
		}
		private void txtInputData_TextChanged(object sender, TextChangedEventArgs e)
        {
            pointGenerator = Convert.ToDouble(txtInputData.Text);
        }
	}
}