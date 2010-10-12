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
using System.ComponentModel;

namespace NumericalMethods_Silverlight.Code
{
    public class Method : INotifyPropertyChanged
    {
        private string themeMethod;
        private string themeName1;
        private string themeName2;
        private string themeName3;
        private string themeName4;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        public string ThemeMethod
        {
            set
            {
                themeMethod = value;
                NotifyPropertyChanged("ThemeMethod");
            }
            get
            {
                return themeMethod;
            }
        }

        public string ThemeName1
        {
            set
            {
                themeName1 = value;
                NotifyPropertyChanged("ThemeName1");
            }
            get
            {
                return themeName1;
            }
        }
        public string ThemeName2
        {
            set
            {
                themeName2 = value;
                NotifyPropertyChanged("ThemeName2");
            }
            get
            {
                return themeName2;
            }
        }
        public string ThemeName3
        {
            set
            {
                themeName3 = value;
                NotifyPropertyChanged("ThemeName3");
            }
            get
            {
                return themeName3;
            }
        }
        public string ThemeName4
        {
            set
            {
                themeName4 = value;
                NotifyPropertyChanged("ThemeName4");
            }
            get
            {
                return themeName4;
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
