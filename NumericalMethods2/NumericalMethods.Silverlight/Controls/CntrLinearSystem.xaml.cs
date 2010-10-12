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
	public partial class CntrLinearSystem : UserControl
    {
        ModifyLinearSystem wndModify;
        double[] MatrA;
        double[,] MatrB;
        int range = 0;
		public CntrLinearSystem()
		{
			// Required to initialize variables
			InitializeComponent();
        }
        public double[] MatrixA
        {
            get { return MatrA; }
            set { MatrA = value; }
        }
        public double[,] MatrixB
        {
            get { return MatrB; }
            set { MatrB = value; }
        }
        public int Range
        {
            get { return range; }
            set { range = value; }
        }
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {

            wndModify = new ModifyLinearSystem();
            wndModify.Closed += new EventHandler(WndModify_Closed);
            wndModify.SetMatrRange(range);
            wndModify.SetMatrA(MatrA, range);
            wndModify.SetMatrB(MatrB, range);
            wndModify.Show();
        }
        private void WndModify_Closed(object sender, EventArgs e)
        {
            if ((bool)wndModify.DialogResult)
            {
                MatrixA = wndModify.MatrixA;
                MatrixB = wndModify.MatrixB;
                Range = wndModify.Range;

                int x = 0, y = 0;
                int NumberRows = Range;
                int NumberCols = Range * 2 + 1;
                nameRange.Text = Range.ToString();
              nameLinearSystem.Text = "";

                for (int i = 0; i < NumberRows; i++)
                {
                    for (int j = 1; j <= NumberCols; j++)
                    {
                        if (j % 2 == 1)
                        {
                            if (NumberCols == j)
                            {
                                nameLinearSystem.Text = nameLinearSystem.Text + MatrA[x].ToString() + "\n";
                                x++;
                                y = 0;
                            }
                            else
                                nameLinearSystem.Text = nameLinearSystem.Text + MatrB[x, y].ToString() + "";
                        }
                        else
                        {
                            y++;
                            if (NumberCols == j + 1)

                                nameLinearSystem.Text =nameLinearSystem.Text + "*x" + y + "=";
                            else
                                nameLinearSystem.Text = nameLinearSystem.Text + "*x" + y + "+";
                        }
                    }
                }

            }
        }
	}
}