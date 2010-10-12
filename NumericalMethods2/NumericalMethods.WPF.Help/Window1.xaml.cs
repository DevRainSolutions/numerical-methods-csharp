using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Markup;
using System.ComponentModel;
using System.Xml;
using System.Xml.XPath;
namespace Help
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public bool RealTimeUpdate = true;
        public Window1()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            // ResizeMode = ResizeMode.NoResize;
            //SizeToContent = SizeToContent.WidthAndHeight;
        }
        void HandleSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (sender == null)
                return;

            Details.DataContext = (sender as ListBox).DataContext;
            ListBox lbi = sender as ListBox;

            //  Uri uri = new Uri(@"pack://application:,,,/test.htm", UriKind.Absolute);
            Uri uri = new Uri("Theory/free.htm", UriKind.RelativeOrAbsolute);
            Stream source = Application.GetResourceStream(uri).Stream;
            WebText.NavigateToStream(source);
            switch ((sender as ListBox).SelectedValue.ToString())
            {
                case "Bisection Method":
                      uri = new Uri("Theory/Approximation/MethodBisectin.htm", UriKind.RelativeOrAbsolute);
                      source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Chord Method":
                    uri = new Uri("Theory/Approximation/MethoHord.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Iteration Method":
                    uri = new Uri("Theory/Approximation/MethodIteration.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Newton Method":
                    uri = new Uri("Theory/Approximation/MethoNewton.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Euler Simple":
                    uri = new Uri("Theory/DifferentialEquations/EulerSimple.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Euler Modified":
                    uri = new Uri("Theory/DifferentialEquations/EulerModified.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Euler Corrected":
                    uri = new Uri("Theory/DifferentialEquations/EulerCorrected.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Runge-Kutta4":
                    uri = new Uri("Theory/DifferentialEquations/RungeKutta.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Chebishev":
                    uri = new Uri("Theory/Integration/Chebishev.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Simpson":
                    uri = new Uri("Theory/Integration/Simpson.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Simpson2":
                    uri = new Uri("Theory/Integration/Simpson2.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
                case "Trapezium":
                    uri = new Uri("Theory/Integration/Trapezium.htm", UriKind.RelativeOrAbsolute);
                    source = Application.GetResourceStream(uri).Stream;
                    WebText.NavigateToStream(source);
                    break;
            }

            /* TextRange documentTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
             Binding bind = new Binding();
            // string url = Environment.CurrentDirectory.ToString() + "/data/DirrefentialEquations/DirrefentialEquations.rtf";// "f:\\DirrefentialEquations.rtf";//= bind.XPath = "@src";*/
            //string url ="d:\\22.rtf";*/
            /* //string url2 = bind.XPath ("@src");
            XmlDocument document = new XmlDocument();
            document.Load("Samples.xml");
            XPathNavigator navigator = document.CreateNavigator();

            navigator.MoveToChild("Samples", "");
            navigator.MoveToChild("Category", "Approximate");
            navigator.MoveToChild("Sample ", "");

            string Sample = (string)navigator.ValueAs(typeof(string));*/

            //TextBox1.Text = Sample;
            /* using (FileStream fs = File.Open(url, FileMode.Open))
              {
                  documentTextRange.Load(fs, DataFormats.Rtf);
              }*/
        }

        protected void HandleTextChanged(object sender, TextChangedEventArgs me)
        {
            if (RealTimeUpdate) ParseCurrentBuffer();
        }

        private void ParseCurrentBuffer()
        {/*
            try
            {
                MemoryStream ms = new MemoryStream();
                StreamWriter sw = new StreamWriter(ms);
                string str = TextBox1.Text;
                sw.Write(str);
                sw.Flush();
                ms.Flush();
                ms.Position = 0;
                try
                {
                    object content = XamlReader.Load(ms);
                    if (content != null)
                    {

                       // cc.Children.Clear();
                      //  cc.Children.Add((UIElement)content);
                    }
                    TextBox1.Foreground = System.Windows.Media.Brushes.Black;
                    ErrorText.Text = "";
                }

                catch (XamlParseException xpe)
                {
                    TextBox1.Foreground = System.Windows.Media.Brushes.Red;
                    TextBox1.TextWrapping = TextWrapping.Wrap;
                    ErrorText.Text = xpe.Message.ToString();
                }
            }
            catch (Exception)
            {
                return;
            }*/
        }
        protected void onClickParseButton(object sender, RoutedEventArgs args)
        {
            ParseCurrentBuffer();
        }
        protected void ShowPreview(object sender, RoutedEventArgs args)
        {
            PreviewRow.Height = new GridLength(1, GridUnitType.Star);
            CodeRow.Height = new GridLength(0);
        }
        protected void ShowCode(object sender, RoutedEventArgs args)
        {
            PreviewRow.Height = new GridLength(0);
            CodeRow.Height = new GridLength(1, GridUnitType.Star);
        }
        protected void ShowSplit(object sender, RoutedEventArgs args)
        {
            PreviewRow.Height = new GridLength(1, GridUnitType.Star);
            CodeRow.Height = new GridLength(1, GridUnitType.Star);
        }



    }
}
