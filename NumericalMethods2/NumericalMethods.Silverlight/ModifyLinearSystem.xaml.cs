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
    public partial class ModifyLinearSystem : ChildWindow
    {
        private int range;
        private double[] MatrA;
        private double[,] MatrB;
        private string[] strLineA;
        private string[] strLineB;
        public ModifyLinearSystem()
        {
            InitializeComponent();
        }
        public void SetMatrRange(int range)
        {
            matrRange.Text = range.ToString();
        }
        public void SetMatrA(double[] MatrA, int range)
        {
            for (int i = 0; i < range; i++)
            {
                matrA.Text = matrA.Text + MatrA[i].ToString() + "\n";
            }
        }
        public void SetMatrB(double[,] MatrB, int range)
        {
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    matrB.Text = matrB.Text + MatrB[i, j].ToString() + " ";
                }
                matrB.Text = matrB.Text + "\n";
            }
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
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            strLineB = new string[20];
            strLineA = new string[20];
            int s = 0;
            for (int i = 0; i < matrB.Text.Length; i++)
            {
                if (matrB.Text[i] == '\n')
                    s++;
                else
                    strLineB[s] = strLineB[s] + matrB.Text[i];
            }
            s = 0;
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
                MatrA = new double[2 * range];
                MatrB = new double[2 * range, 2 * range];
            }
            else
                MessageBox.Show("Enter dimension of equation (n x n)");

            if (matrB.Text != "")
            {
                if (matrA.Text != "")
                {
                    for (int i = 0; i < range; i++)
                    {

                        for (int j = 0; j < range; j++)
                            MatrB[i, j] = Convert.ToDouble(strLineB[i].Split(' ')[j]);
                    }
                    for (int i = 0; i < range; i++)
                    {
                        MatrA[i] = Convert.ToDouble(strLineA[i]);
                    }
                    DialogResult = true;
                }
                else
                    MessageBox.Show("Enter matrix A");
            }
            else
                MessageBox.Show("Enter matrix B");

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
                    matrB.IsEnabled = false;
                    matrA.IsEnabled = false;
                    matrB.Text = "";
                    matrA.Text = "";
                }
                else
                {
                    if (range <= 20)
                    {
                        matrB.IsEnabled = true;
                        matrA.IsEnabled = true;
                        matrB.Text = "";
                        matrA.Text = "";
                        for (int i = 0; i < range; i++)
                        {
                            matrA.Text = matrA.Text + "0\n";
                            for (int j = 0; j < range; j++)
                            {
                                matrB.Text = matrB.Text + "0 ";
                            }
                            matrB.Text = matrB.Text + " \n";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Range <= 20.");
                        matrB.IsEnabled = false;
                        matrA.IsEnabled = false;
                        matrB.Text = "";
                        matrA.Text = "";
                    }
                }
            }
            else
            {
                matrB.Text = "";
                matrA.Text = "";
                matrB.IsEnabled = false;
                matrA.IsEnabled = false;
            }
        }
    }
}

