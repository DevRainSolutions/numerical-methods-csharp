using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace NumericalMethods_Silverlight
{
    public partial class ModifyMatrixAlgebra : ChildWindow
    {
        private int range;
        private double[,] MatrA;
        private string[] strLineA;
        public ModifyMatrixAlgebra()
        {
            InitializeComponent();
        }
        public void SetMatrRange(int range)
        {
            matrRange.Text = range.ToString();
        }

        public void SetMatrA(double[,] MatrA, int range)
        {
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    matrA.Text = matrA.Text + MatrA[i, j].ToString() + " ";
                }
                matrA.Text = matrA.Text + "\n";
            }
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
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            strLineA = new string[20];
            int s = 0;

            for (int i = 0; i < matrA.Text.Length; i++)
            {
                if (matrA.Text[i] == '\n')
                    s++;
                else
                    strLineA[s] = strLineA[s] + matrA.Text[i];
            }

            if (matrRange.Text != "")
            {
                range = Convert.ToInt32(matrRange.Text);
                MatrA = new double[2 * range, 2 * range];
            }
            else
                MessageBox.Show("Enter dimension of equation (n x n)");
            if (matrA.Text != "")
            {
                for (int i = 0; i < range; i++)
                {

                    for (int j = 0; j < range; j++)
                        MatrA[i, j] = Convert.ToDouble(strLineA[i].Split(' ')[j]);
                }
                
            }
            else
                MessageBox.Show("Enter matrix A");

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void matrRange_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (matrRange.Text != "")
            {
                range = Convert.ToInt32(matrRange.Text);
                if (range == 0 || range == 1)
                {
                    matrA.IsEnabled = false;
                    matrA.Text = "";
                }
                else
                {
                    if (range <= 20)
                    {
                        matrA.IsEnabled = true;
                        matrA.Text = "";
                        for (int i = 0; i < range; i++)
                        {
                            for (int j = 0; j < range; j++)
                            {
                                matrA.Text = matrA.Text + "0 ";
                            }
                            matrA.Text = matrA.Text + " \n";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Range <= 20.");
                        matrA.IsEnabled = false;
                        matrA.Text = "";
                    }
                }
            }

            else
            {
                matrA.Text = "";
                matrA.IsEnabled = false;
            }
        }
    }

}

