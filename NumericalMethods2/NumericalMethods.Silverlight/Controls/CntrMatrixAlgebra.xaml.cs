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
    public partial class CntrMatrixAlgebra : UserControl
    {
        ModifyMatrixAlgebra wndModify;
        double[,] MatrA;
             private string[] strLineA;
        int range = 0;
        public CntrMatrixAlgebra()
        {
            // Required to initialize variables
            InitializeComponent();
        }

        public double[,] MatrixA
        {
            get { return MatrA; }
            set { MatrA = value; }
        }
        public int Range
        {
            get { return range; }
            set { range = value; }
        }
        public void ReadData()
        {
            strLineA = new string[20];
            int s = 0;

            for (int i = 0; i < nameMatrix.Text.Length; i++)
            {
                if (nameMatrix.Text[i] == '\n')
                    s++;
                else
                    strLineA[s] = strLineA[s] + nameMatrix.Text[i];
            }

            if (nameRange.Text != "")
            {
                range = Convert.ToInt32(nameRange.Text);
                MatrA = new double[2 * range, 2 * range];
            }
            if (nameMatrix.Text != "")
            {
                for (int i = 0; i < range; i++)
                {

                    for (int j = 0; j < range; j++)
                        MatrA[i, j] = Convert.ToDouble(strLineA[i].Split(' ')[j]);
                }

            }
        }
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {

            wndModify = new ModifyMatrixAlgebra();
            wndModify.Closed += new EventHandler(WndModify_Closed);


            ReadData();

            wndModify.SetMatrRange(range);
            wndModify.SetMatrA(MatrA, range);

            wndModify.Show();
        }
        private void WndModify_Closed(object sender, EventArgs e)
        {
            if ((bool)wndModify.DialogResult)
            {
                MatrixA = wndModify.MatrixA;
                Range = wndModify.Range;
                int NumberRows = Range;

                nameRange.Text = Range.ToString();
                nameMatrix.Text = "";


                for (int i = 0; i < NumberRows; i++)
                {
                    for (int j = 0; j < NumberRows; j++)
                    {
                        nameMatrix.Text = nameMatrix.Text + MatrA[i, j].ToString() + " ";
                        if (NumberRows == j + 1)
                        {
                            nameMatrix.Text = nameMatrix.Text + "\n";
                        }

                        
                    }
                }

            }
        }
    }
}