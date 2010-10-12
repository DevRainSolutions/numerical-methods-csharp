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
	public partial class CntrStatistics : UserControl
    {
        public double[] massX;
        public double[] massF;
        public double pointPercentile;
		public CntrStatistics()
		{
			// Required to initialize variables
			InitializeComponent();
        }
        int Count(string str)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
                if (str[i] == ';')
                    count++;

            return count;
        }

        private void nameFunStatistics_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            string strLineX = "";
            string strLineF = "";
            int count = 0;
            int s = 0;
            for (int i = 0; i < nameFunStatistics.Text.Length; i++)
            {
                s++;
                strLineX = strLineX + nameFunStatistics.Text[i];
                if (nameFunStatistics.Text[i] == '\n')
                    break;
            }
            count = Count(strLineX);
            massX = new double[count];
            for (int i = 0; i < count; i++)
            {
                massX[i] = Convert.ToDouble(strLineX.Split(';')[i]);
            }
            for (int i = s; i < nameFunStatistics.Text.Length; i++)
            {
                s++;
                strLineF = strLineF + nameFunStatistics.Text[i];
                if (nameFunStatistics.Text[i] == '\n')
                    break;
            }
            count = Count(strLineF);
            massF = new double[count];
            for (int i = 0; i < count; i++)
            {
                massF[i] = Convert.ToDouble(strLineF.Split(';')[i]);
            }
            
        }

        private void txtInputData1_TextChanged(object sender, TextChangedEventArgs e)
        {
            pointPercentile = Convert.ToDouble(txtInputData1.Text);
        }
	}
}