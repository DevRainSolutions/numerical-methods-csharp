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
using System.Windows.Browser;
using System.Collections.ObjectModel;

namespace NumericalMethods_Silverlight.Code
{
    public class AddMethods : INotifyPropertyChanged
    {

        string[] themeMethod = new string[] { "Approximate decision of \n equalization f(x)=0", 
                                              "Differential Equations", 
                                              "Integration" };
        string[,] themeName = new string[,] {{"Bisection Method", "Chord Method", "Iteration Method","Newton Method" },
                                             {"Euler Simple","Euler Modified","Euler Corrected","Runge-Kutta4"},
                                             {"Chebishev","Simpson","Simpson 2","Trapezium"}
                                            };

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Method> MethodApproximate { get; internal set; }
        public string RemoteThemeApproximate { get; set; }
        public ObservableCollection<Method> MethodDifferential { get; internal set; }
        public string RemoteThemeDifferential { get; set; }


        public AddMethods()
        {
            if (HtmlPage.IsEnabled == false)
            {
                DataMethodApproximate();
                DataMethodDifferential();
            }
        }

        private void DataMethodApproximate()
        {
            RemoteThemeApproximate = themeMethod[0];
            MethodApproximate = new ObservableCollection<Method>();
            MethodApproximate.Add(new Method
            {
                ThemeName1 = themeName[0, 0],
                ThemeName2 = themeName[0, 1],
                ThemeName3 = themeName[0, 2],
                ThemeName4 = themeName[0, 3]
            });
        }
        private void DataMethodDifferential()
        {
            RemoteThemeDifferential = themeMethod[1];
            MethodDifferential = new ObservableCollection<Method>();
            MethodDifferential.Add(new Method
            {
                ThemeName1 = themeName[1, 0],
                ThemeName2 = themeName[1, 1],
                ThemeName3 = themeName[1, 2],
                ThemeName4 = themeName[1, 3]
            });
        }
        public void ConnectWithRemoteMethod()
        {
            DataMethodApproximate();
            DataMethodDifferential();
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("RemoteThemeApproximate"));
                PropertyChanged(this, new PropertyChangedEventArgs("MethodApproximate"));
                PropertyChanged(this, new PropertyChangedEventArgs("RemoteThemeDifferential"));
                PropertyChanged(this, new PropertyChangedEventArgs("MethodDifferential"));
            }
        }
    }
}
