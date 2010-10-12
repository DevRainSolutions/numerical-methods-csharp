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
    public partial class CntrInterpolation : UserControl
    {
       
        public double[] massX;
        public double[] massF;
        public double[] massW;
        public CntrInterpolation()
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
        private void nameFunInterpolation_TextChanged(object sender, TextChangedEventArgs e)
        {
         string strLineX = "";
            string strLineF = "";
            string strLineW = "";
            int count=0;

            int s = 0;
            for (int i = 0; i < nameFunInterpolation.Text.Length; i++)
            {
                s++;
                strLineX = strLineX + nameFunInterpolation.Text[i];
                if (nameFunInterpolation.Text[i] == '\n')
                    break;
            }
            count=Count(strLineX);
            massX = new double[count];
            for (int i = 0; i < count; i++)
            {
                massX[i] = Convert.ToDouble(strLineX.Split(';')[i]);
            }


            for (int i = s; i < nameFunInterpolation.Text.Length; i++)
            {
                s++;
                strLineF = strLineF + nameFunInterpolation.Text[i];
                if (nameFunInterpolation.Text[i] == '\n')
                    break;
            }
            count = Count(strLineF);
            massF = new double[count];
            for (int i = 0; i < count; i++)
            {
                massF[i] = Convert.ToDouble(strLineF.Split(';')[i]);
            }
            for (int i = s; i < nameFunInterpolation.Text.Length; i++)
            {
                s++;
                strLineW = strLineW + nameFunInterpolation.Text[i];
                if (nameFunInterpolation.Text[i] == '\n')
                    break;
            }
            count = Count(strLineW);
            massW = new double[count];
            for (int i = 0; i < count; i++)
            {
                massW[i] = Convert.ToDouble(strLineW.Split(';')[i]);
            }
        }
    }
}