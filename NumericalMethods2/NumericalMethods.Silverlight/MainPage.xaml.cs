using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NumericalMethods_Silverlight.Code;
using Expression.Blend.SampleData.SaveData;
using NumericalMethods;
using SilverlightMathParser;
using System.IO.IsolatedStorage;
using System.Xml.Linq;
using System.IO;
using SilverlightDragNDrop;
using NumericalMethods.Silverlight;
namespace NumericalMethods_Silverlight
{
    public partial class MainPage : UserControl
    {

        string XMLdata;

        private string NameMethod = "";
        private double param1 = 0;
        private double param2 = 0;
        private double param3 = 0;
        private double param4 = 0;
        private string TestFunction = "";
        public int rangeArray;
        public double[] LinSysMasA;
        public double[,] LinSysMatrixB;
        public double[,] MatrixAlgebraA;
        // private AddMethods addMethods;

        public double[] massX;
        public double[] massF;
        public double[] massW;
        public double pointInterpolation;
        public double pointPercentile;
        public double pointGenerator;
        ProcessingData processingData;

        Image imgResMathcad;
        BitmapImage bmpImg;
        Uri uri;
        public MainPage()
        {

            InitializeComponent();

            /// addMethods = Resources["AddMethodsDataSource"] as AddMethods;
            // addMethods.ConnectWithRemoteMethod();
            AboutPanel.Visibility = Visibility.Collapsed;
            MatrixAlgebra.Visibility = Visibility.Collapsed;
            // ImageResMathcad.Visibility = Visibility.Collapsed;
            AproximateBisection.Visibility = Visibility.Collapsed;
            AproximateOther.Visibility = Visibility.Collapsed;
            LinearSystem.Visibility = Visibility.Collapsed;
            MaineProgram.Visibility = Visibility.Collapsed;
            panelStart.Visibility = Visibility.Visible;
            InitMenu();
            StoryboardIcon.Begin();

            Loaded += new RoutedEventHandler(Page_Loaded);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DragManager dm = new DragManager(LayoutRoot);
            //dm.Collision += dm_Collision;
            //dm.EnableDragableElement(Ellipse1);
            //dm.EnableDragableElement(TextBlockStatus);
            //  dm.EnableDragableElement(MaineProgram);
        }
        private void InitMenu()
        {
            string[] images = {"New.png", "Open.png", "filesave.png", 
                "filesaveAs.png", "run.png", "Info.png", "Help.png","Exit.png"};

            /* Rectangle rect = new Rectangle(); rect.Width = 10; MyMenuTable.Children.Add(rect);
**/
            Button btnNew = new Button();
            btnNew.Template = (ControlTemplate)App.Current.Resources["ImageButtonBar"];
            btnNew.Content = "Image/" + images[0];
            btnNew.Click += new RoutedEventHandler(btnNew_Click);
            ToolTip tolNew = new ToolTip();
            tolNew.Content = "Clear data";
            ToolTipService.SetToolTip(btnNew, tolNew);
            StackNew.Children.Add(btnNew);

            /*   rect = new Rectangle(); rect.Width = 10; MyMenuTable.Children.Add(rect);*/

            /*   Button btnOpen = new Button();
               btnOpen.Template = (ControlTemplate)App.Current.Resources["ImageButtonBar"];
               btnOpen.Content = "Image/" + images[1];
               btnOpen.Click += new RoutedEventHandler(btnOpen_Click);
               MyMenuTable.Children.Add(btnOpen);

               rect = new Rectangle(); rect.Width = 10; MyMenuTable.Children.Add(rect);

               Button btnSave = new Button();
               btnSave.Template = (ControlTemplate)App.Current.Resources["ImageButtonBar"];
               btnSave.Content = "Image/" + images[2];
               btnSave.Click += new RoutedEventHandler(btnSave_Click);
               MyMenuTable.Children.Add(btnSave);

               rect = new Rectangle(); rect.Width = 10; MyMenuTable.Children.Add(rect);

               Button btnSaveAs = new Button();
               btnSaveAs.Template = (ControlTemplate)App.Current.Resources["ImageButtonBar"];
               btnSaveAs.Content = "Image/" + images[3];
               btnSaveAs.Click += new RoutedEventHandler(btnSaveAs_Click);
               MyMenuTable.Children.Add(btnSaveAs);*/

            /*  rect = new Rectangle(); rect.Width = 10; MyMenuTable.Children.Add(rect);*/
            Button btnRun = new Button();
            btnRun.Template = (ControlTemplate)App.Current.Resources["ImageButtonBar"];
            btnRun.Content = "Image/" + images[4];
            btnRun.Click += new RoutedEventHandler(btnRun_Click);
             ToolTip tolRun = new ToolTip();
            tolRun.Content = "Execute the method";
            ToolTipService.SetToolTip(btnRun, tolRun);
            StackRun.Children.Add(btnRun);

            Button btnHelp = new Button();
            btnHelp.Template = (ControlTemplate)App.Current.Resources["ImageButtonBar"];
            btnHelp.Content = "Image/" + images[6];
            btnHelp.Click += new RoutedEventHandler(btnHelp_Click);
            ToolTip tolHelp = new ToolTip();
            tolHelp.Content = "Help of application";
            ToolTipService.SetToolTip(btnHelp, tolHelp);
            StackHelp.Children.Add(btnHelp);

            Rectangle rect = new Rectangle(); rect.Width = 10; stackUp.Children.Add(rect);
            Button btnExit = new Button();
            ToolTip tolExit = new ToolTip();
            tolExit.Content = "Quit the application";
            ToolTipService.SetToolTip(btnExit, tolExit);
            btnExit.Template = (ControlTemplate)App.Current.Resources["ImageButtonBar"];
            btnExit.Content = "Image/" + images[7];
            btnExit.Click += new RoutedEventHandler(btnExit_Click);
            StackExit.Children.Add(btnExit);
        }
        void NewFile()
        {
            switch (NameMethod)
            {

                //*** Approximate decision of equalization f(x)=0 ***
                case "Bisection Method":
                    {
                        AproximateBisection.txtInput1.Text = "";
                        AproximateBisection.txtInput2.Text = "";
                        AproximateBisection.txtInput3.Text = "";
                    }
                    break;
                case "Chord Method":
                    {
                        AproximateOther.txtInput1.Text = "";
                        AproximateOther.txtInput2.Text = "";
                        AproximateOther.txtInput3.Text = "";
                        AproximateOther.txtInput4.Text = "";
                    }
                    break;
                case "Iteration Method":
                    {
                        AproximateOther.txtInput1.Text = "";
                        AproximateOther.txtInput2.Text = "";
                        AproximateOther.txtInput3.Text = "";
                        AproximateOther.txtInput4.Text = "";
                    }
                    break;
                case "Newton Method":
                    {
                        AproximateOther.txtInput1.Text = "";
                        AproximateOther.txtInput2.Text = "";
                        AproximateOther.txtInput3.Text = "";
                        AproximateOther.txtInput4.Text = "";
                    }
                    break;

                //*** Differential Equations ***
                case "Euler Simple":
                    {
                        Diffrential.txtInput1.Text = "";
                        Diffrential.txtInput2.Text = "";
                        Diffrential.txtInput3.Text = "";
                        Diffrential.txtInput4.Text = "";
                    }
                    break;
                case "Euler Modified":
                    {
                        Diffrential.txtInput1.Text = "";
                        Diffrential.txtInput2.Text = "";
                        Diffrential.txtInput3.Text = "";
                        Diffrential.txtInput4.Text = "";
                    }
                    break;
                case "Euler Corrected":
                    {
                        Diffrential.txtInput1.Text = "";
                        Diffrential.txtInput2.Text = "";
                        Diffrential.txtInput3.Text = "";
                        Diffrential.txtInput4.Text = "";
                    }
                    break;
                case "Runge-Kutta4":
                    {
                        Diffrential.txtInput1.Text = "";
                        Diffrential.txtInput2.Text = "";
                        Diffrential.txtInput3.Text = "";
                        Diffrential.txtInput4.Text = "";

                    }
                    break;

                //*** Integration ***
                case "Chebishev":
                    CntrIntegration.txtInput1.Text = "";
                    CntrIntegration.txtInput2.Text = "";
                    CntrIntegration.txtInput3.Text = "";
                    break;
                case "Simpson":
                    CntrIntegration.txtInput1.Text = "";
                    CntrIntegration.txtInput2.Text = "";
                    CntrIntegration.txtInput3.Text = "";
                    break;
                case "Simpson2":
                    CntrIntegration.txtInput1.Text = "";
                    CntrIntegration.txtInput2.Text = "";
                    CntrIntegration.txtInput3.Text = "";
                    break;
                case "Trapezium":
                    CntrIntegration.txtInput1.Text = "";
                    CntrIntegration.txtInput2.Text = "";
                    CntrIntegration.txtInput3.Text = "";
                    break;




            }

        }
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog. .DefaultExt = ".xml";
            openFileDialog.Filter = "XML Files (.xml)|*.xml";
            //openFileDialog.Title = "Open a XML file";
            //openFileDialog.RestoreDirectory = true;
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                XMLdata = openFileDialog.File.FullName;
            }
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            NewFile();
            textBoxResult.Text = "";
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            processingData = new ProcessingData();
            xmlMethod method = new xmlMethod()
            {
                Approximate_Decision = new method()
                {
                    ID = "BisectionMethod",
                    Function = "tan(0.9464 * x) - 1.3825*x",
                    ParamInput1 = "1",
                    ParamInput2 = "1",
                    ParamInput3 = "1",
                    ParamInput4 = "1"
                }
            };
            processingData.Save(method);
            // processingData.SeveTest();
            textBoxResult.Text = processingData.resultXML;

            /*XDocument doc = new XDocument(
                               new XComment("This is a comment2222222222222"),
                               new XElement("Root",
                                   new XElement("Child1", "data1"),
                                   new XElement("Child2", "data2")
                               )
                           );

            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isoStream =
                    new IsolatedStorageFileStream("myFile.xml", FileMode.Create, isoStore))
                {
                    doc.Save(isoStream);
                }
            }

            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("myFile.xml", FileMode.Open, isoStore))
                {
                    XDocument doc1 = XDocument.Load(isoStream);
                    textBoxResult.Text = doc1.ToString();
                }
            }*/

            // MessageBox.Show("File is save");
        }
        private void btnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            Parser MathParser = new Parser();
            int x = 2;
            int y = 2;
            string InputExpression = "x^2 - y+5";
            string newStr = "";
            for (int i = 0; i < InputExpression.Length; i++)
            {
                switch (InputExpression[i])
                {
                    case 'x': newStr += x.ToString();
                        break;
                    case 'X': newStr += x.ToString();
                        break;
                    case 'y': newStr += y.ToString();
                        break;
                    case 'Y': newStr += y.ToString();
                        break;
                    default:
                        newStr += InputExpression[i];
                        break;
                }
            }
            MathParser.Evaluate(newStr);
            textBoxResult.Text = MathParser.Result.ToString();
        }
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            StoryboardAbout.Begin();
        }
        /*  private void btnExit_Click(object sender, RoutedEventArgs e)
          {
              StoryboardAbout.Begin();
          }*/
        private void btnAboutExit_Click(object sender, RoutedEventArgs e)
        {
            StoryboardAboutExit.Begin();
        }
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            WndHelp wndHelp = new WndHelp();
            wndHelp.Show();
        }
        private void ElipsResultMathcad_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //StoryboardResMathcadEnd.Begin();
        }
        private void ImageResMathcad_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // StoryboardResMathcadEnd.Begin();
			
        }
        private void ElipsresultProgram_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // StoryboardTextResultEnd.Begin();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          //  StoryboardTextResultEnd.Begin();
        }
        void ReadData()
        {
            switch (NameMethod)
            {

                //*** Approximate decision of equalization f(x)=0 ***
                case "Bisection Method":
                    {
                        param1 = double.Parse(AproximateBisection.txtInput1.Text);
                        param2 = double.Parse(AproximateBisection.txtInput2.Text);
                        param3 = double.Parse(AproximateBisection.txtInput3.Text);
                        TestFunction = AproximateBisection.nameFunction.Text.ToString();

                    }
                    break;
                case "Chord Method":
                    {
                        param1 = double.Parse(AproximateOther.txtInput1.Text);
                        param2 = double.Parse(AproximateOther.txtInput2.Text);
                        param3 = double.Parse(AproximateOther.txtInput3.Text);
                        param4 = double.Parse(AproximateOther.txtInput4.Text);
                        TestFunction = AproximateOther.nameFunction.Text.ToString();
                    }
                    break;
                case "Iteration Method":
                    {
                        param1 = double.Parse(AproximateOther.txtInput1.Text);
                        param2 = double.Parse(AproximateOther.txtInput2.Text);
                        param3 = double.Parse(AproximateOther.txtInput3.Text);
                        param4 = double.Parse(AproximateOther.txtInput4.Text);
                        TestFunction = AproximateOther.nameFunction.Text.ToString();
                    }
                    break;
                case "Newton Method":
                    {
                        param1 = double.Parse(AproximateOther.txtInput1.Text);
                        param2 = double.Parse(AproximateOther.txtInput2.Text);
                        param3 = double.Parse(AproximateOther.txtInput3.Text);
                        param4 = double.Parse(AproximateOther.txtInput4.Text);
                        TestFunction = AproximateOther.nameFunction.Text.ToString();
                    }
                    break;

                //*** Differential Equations ***
                case "Euler Simple":
                    {
                        param1 = double.Parse(Diffrential.txtInput1.Text);
                        param2 = double.Parse(Diffrential.txtInput2.Text);
                        param3 = double.Parse(Diffrential.txtInput3.Text);
                        param4 = double.Parse(Diffrential.txtInput4.Text);
                        TestFunction = Diffrential.nameFunction.Text.ToString();
                    }
                    break;
                case "Euler Modified":
                    {
                        param1 = double.Parse(Diffrential.txtInput1.Text);
                        param2 = double.Parse(Diffrential.txtInput2.Text);
                        param3 = double.Parse(Diffrential.txtInput3.Text);
                        param4 = double.Parse(Diffrential.txtInput4.Text);
                        TestFunction = Diffrential.nameFunction.Text.ToString();
                    }
                    break;
                case "Euler Corrected":
                    {
                        param1 = double.Parse(Diffrential.txtInput1.Text);
                        param2 = double.Parse(Diffrential.txtInput2.Text);
                        param3 = double.Parse(Diffrential.txtInput3.Text);
                        param4 = double.Parse(Diffrential.txtInput4.Text);
                        TestFunction = Diffrential.nameFunction.Text.ToString();
                    }
                    break;
                case "Runge-Kutta4":
                    {
                        param1 = double.Parse(Diffrential.txtInput1.Text);
                        param2 = double.Parse(Diffrential.txtInput2.Text);
                        param3 = double.Parse(Diffrential.txtInput3.Text);
                        param4 = double.Parse(Diffrential.txtInput4.Text);
                        TestFunction = Diffrential.nameFunction.Text.ToString();

                    }
                    break;

                //*** Integration ***
                case "Chebishev":
                    param1 = double.Parse(CntrIntegration.txtInput1.Text);
                    param2 = double.Parse(CntrIntegration.txtInput2.Text);
                    param3 = double.Parse(CntrIntegration.txtInput3.Text);
                    TestFunction = CntrIntegration.nameFunction.Text.ToString();
                    break;
                case "Simpson":
                    param1 = double.Parse(CntrIntegration.txtInput1.Text);
                    param2 = double.Parse(CntrIntegration.txtInput2.Text);
                    param3 = double.Parse(CntrIntegration.txtInput3.Text);
                    TestFunction = CntrIntegration.nameFunction.Text.ToString();
                    break;
                case "Simpson2":
                    param1 = double.Parse(CntrIntegration.txtInput1.Text);
                    param2 = double.Parse(CntrIntegration.txtInput2.Text);
                    param3 = double.Parse(CntrIntegration.txtInput3.Text);
                    TestFunction = CntrIntegration.nameFunction.Text.ToString();
                    break;
                case "Trapezium":
                    param1 = double.Parse(CntrIntegration.txtInput1.Text);
                    param2 = double.Parse(CntrIntegration.txtInput2.Text);
                    param3 = double.Parse(CntrIntegration.txtInput3.Text);
                    TestFunction = CntrIntegration.nameFunction.Text.ToString();
                    break;
                //*** Non Linear equalization  ***
                case "Half Division":
                    param1 = double.Parse(NonLinear.txtInput1.Text);
                    param2 = double.Parse(NonLinear.txtInput2.Text);
                    param3 = double.Parse(NonLinear.txtInput3.Text);
                    TestFunction = NonLinear.nameFunction.Text.ToString();
                    break;
                case "Hord Metod":
                    param1 = double.Parse(NonLinear.txtInput1.Text);
                    param2 = double.Parse(NonLinear.txtInput2.Text);
                    param3 = double.Parse(NonLinear.txtInput3.Text);
                    TestFunction = NonLinear.nameFunction.Text.ToString();
                    break;
                case "Newton Metod":
                    param1 = double.Parse(NonLinear.txtInput1.Text);
                    param2 = double.Parse(NonLinear.txtInput2.Text);
                    TestFunction = NonLinear.nameFunction.Text.ToString();
                    break;
                case "Secant Metod":
                    param1 = double.Parse(NonLinear.txtInput1.Text);
                    param2 = double.Parse(NonLinear.txtInput2.Text);
                    TestFunction = NonLinear.nameFunction.Text.ToString();
                    break;
                //**************Linear Systems**************
                case "Gaus":
                    LinSysMasA = LinearSystem.MatrixA;
                    LinSysMatrixB = LinearSystem.MatrixB;
                    rangeArray = LinearSystem.Range;
                    break;
                case "Zeidel":
                    LinSysMasA = LinearSystem.MatrixA;
                    LinSysMatrixB = LinearSystem.MatrixB;
                    rangeArray = LinearSystem.Range;
                    break;

                //*** Interpolation ***
                case "Lagrange Interpolator":
                    massX = Interpolation.massX;
                    massF = Interpolation.massF;

                    break;
                case "Newton Interpolator":
                    massX = Interpolation.massX;
                    massF = Interpolation.massF;
                    break;
                case "Neville Interpolator":
                    massX = Interpolation.massX;
                    massF = Interpolation.massF;
                    break;
                case "Spline Interpolator":
                    massX = Interpolation.massX;
                    massF = Interpolation.massF;
                    break;
                case "Barycentric Interpolator":
                    massX = Interpolation.massX;
                    massW = Interpolation.massW;
                    break;
                //*** Matrix Algebra ***
                case "Matrix Determinant":
                    MatrixAlgebra.ReadData();
                    MatrixAlgebraA = MatrixAlgebra.MatrixA;
                    rangeArray = MatrixAlgebra.Range;
                    break;
                case "RMatrix LU":
                    MatrixAlgebra.ReadData();
                    MatrixAlgebraA = MatrixAlgebra.MatrixA;
                    rangeArray = MatrixAlgebra.Range;
                    break;
                case "Matrix Inverse LU":
                    MatrixAlgebra.ReadData();
                    MatrixAlgebraA = MatrixAlgebra.MatrixA;
                    rangeArray = MatrixAlgebra.Range;

                    break;

                //*** Optimizing***
                case "Brentopt":
                    param1 = double.Parse(Optimizing.txtInput1.Text);
                    param2 = double.Parse(Optimizing.txtInput2.Text);
                    param3 = double.Parse(Optimizing.txtInput3.Text);
                    TestFunction = Optimizing.nameFunction.Text.ToString();
                    break;
                case "Golden Section":
                    param1 = double.Parse(Optimizing.txtInput1.Text);
                    param2 = double.Parse(Optimizing.txtInput2.Text);
                    param3 = double.Parse(Optimizing.txtInput3.Text);
                    TestFunction = Optimizing.nameFunction.Text.ToString();
                    break;
                case "Pijavsky":
                    param1 = double.Parse(Optimizing.txtInput1.Text);
                    param2 = double.Parse(Optimizing.txtInput2.Text);
                    param3 = double.Parse(Optimizing.txtInput3.Text);
                    param4 = double.Parse(Optimizing.txtInput4.Text);
                    TestFunction = Optimizing.nameFunction.Text.ToString();
                    break;
                //*** Statistics ***
                case "Correlation Pearson":

                    massX = Statistics.massX;
                    massF = Statistics.massF;
                    break;
                case "Correlation Spearmans Rank":
                    massX = Statistics.massX;
                    massF = Statistics.massF;
                    break;
                case "Descriptive Statistics A Dev":
                    massX = Statistics.massX;

                    break;
                case "Descriptive Statistics Median":
                    massX = Statistics.massX;
                    break;
                case "Descriptive Statistics Moments":
                    massX = Statistics.massX;
                    break;
                case "Descriptive Statistics Percentile":
                    massX = Statistics.massX;
                    pointPercentile = Statistics.pointPercentile;
                    break;
                case "Generator 1":

                    break;
                case "Generator 2":
                    pointGenerator = RandomGenerator.pointGenerator;
                    break;
                case "Generator 3":

                    break;
                case "Generator 4":

                    break;
                case "Generator 5":
                    pointGenerator = RandomGenerator.pointGenerator;
                    break;
                default:
                    param1 = 0;
                    param2 = 0;
                    param3 = 0;
                    param4 = 0;
                    break;



            }

        }
        private void MyVisiblePanel()
        {
			StoryboardResultMathcad.Begin();
			 StoryboardTextResult.Begin();
			
			StoryboardTextResult.Begin();
            textBoxResult.Visibility = Visibility.Visible;
            StackResMathcad.Visibility = Visibility.Visible;
            Diffrential.Visibility = Visibility.Collapsed;
            AproximateBisection.Visibility = Visibility.Collapsed;
            AproximateOther.Visibility = Visibility.Collapsed;
            LinearSystem.Visibility = Visibility.Collapsed;
            NonLinear.Visibility = Visibility.Collapsed;
            CntrIntegration.Visibility = Visibility.Collapsed;
            Interpolation.Visibility = Visibility.Collapsed;
            MatrixAlgebra.Visibility = Visibility.Collapsed;
            Optimizing.Visibility = Visibility.Collapsed;
            Statistics.Visibility = Visibility.Collapsed;
            RandomGenerator.Visibility = Visibility.Collapsed;
            StoryboardApproximateOther.Stop();
            StoryboardApproximateBisection.Stop();
            StoryboardNonLinear.Stop();
            StoryboardDifferential.Stop();
            StoryboardIntegration.Stop();
            StoryboardLinearSystem.Stop();
            StoryboardInterpolation.Stop();
            StoryboardMatrixAlgebra.Stop();
            StoryboardOptimizing.Stop();
            StoryboardStatistics.Stop();
            StoryboardRandomGenerator.Stop();
            NameMethod = "";
            textBoxSource.Text = "";
            textBoxResult.Text = "";
        }
        private void listBoxApproximate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyVisiblePanel();


            processingData = new ProcessingData();
            switch (listBoxApproximate.SelectedIndex)
            {
                case 0:
                    textBoxSource.Text = AllMethodText.Approximation_Bisection;
                    if (MyTabControl.SelectedIndex == 0)
                    {
                        StoryboardApproximateBisection.Begin();
                        AproximateBisection.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AproximateBisection.Opacity = 100;
                    }
                    NameMethod = "Bisection Method";
                    processingData.InpuParamMethod("Approximate_Decision", "BisectionMethod");
                    AproximateBisection.txtInput1.Text = processingData.ParamInput1;
                    AproximateBisection.txtInput2.Text = "";
                    AproximateBisection.txtInput2.Text = processingData.ParamInput2;
                    AproximateBisection.txtInput3.Text = processingData.ParamInput3;
                    AproximateBisection.nameFunction.Text = processingData.TestFunnction;
                    uri = new Uri("/Data/Approximation/MethodBisectin.jpg", UriKind.Relative);
                    break;
                case 1:
                    textBoxSource.Text = AllMethodText.Approximation_Chord;
                    if (MyTabControl.SelectedIndex == 0)
                    {
                        StoryboardApproximateOther.Begin();
                        AproximateOther.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AproximateOther.Opacity = 100;
                    }
                    NameMethod = "Chord Method";
                    processingData.InpuParamMethod("Approximate_Decision", "ChordMethod");
                    AproximateOther.txtInput1.Text = processingData.ParamInput1;
                    AproximateOther.txtInput2.Text = processingData.ParamInput2;
                    AproximateOther.txtInput3.Text = processingData.ParamInput3;
                    AproximateOther.txtInput4.Text = processingData.ParamInput4;
                    AproximateOther.nameFunction.Text = processingData.TestFunnction;
                    uri = new Uri("/Data/Approximation/MethoHord.jpg", UriKind.Relative);
                    break;
                case 2:
                    textBoxSource.Text = AllMethodText.Approximation_Iterationmethod;
                    NameMethod = "Iteration Method";
                    if (MyTabControl.SelectedIndex == 0)
                    {
                        StoryboardApproximateOther.Begin();
                        AproximateOther.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AproximateOther.Opacity = 100;
                    }
                    processingData.InpuParamMethod("Approximate_Decision", "IterationMethod");
                    AproximateOther.txtInput1.Text = processingData.ParamInput1;
                    AproximateOther.txtInput2.Text = processingData.ParamInput2;
                    AproximateOther.txtInput3.Text = processingData.ParamInput3;
                    AproximateOther.txtInput4.Text = processingData.ParamInput4;
                    AproximateOther.nameFunction.Text = processingData.TestFunnction;
                    uri = new Uri("/Data/Approximation/MethodIteration.jpg", UriKind.Relative);
                    break;
                case 3:
                    textBoxSource.Text = AllMethodText.Approximation_Newton;
                    NameMethod = "Newton Method";
                    if (MyTabControl.SelectedIndex == 0)
                    {
                        StoryboardApproximateOther.Begin();
                        AproximateOther.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AproximateOther.Opacity = 100;
                    }
                    processingData.InpuParamMethod("Approximate_Decision", "NewtonMethod");
                    AproximateOther.txtInput1.Text = processingData.ParamInput1;
                    AproximateOther.txtInput2.Text = processingData.ParamInput2;
                    AproximateOther.txtInput3.Text = processingData.ParamInput3;
                    AproximateOther.txtInput4.Text = processingData.ParamInput4;
                    AproximateOther.nameFunction.Text = processingData.TestFunnction;
                    uri = new Uri("/Data/Approximation/MethoNewton.jpg", UriKind.Relative);
                    break;
            };
            imgResMathcad = new Image();
            imgResMathcad.HorizontalAlignment = HorizontalAlignment.Stretch;
            imgResMathcad.VerticalAlignment = VerticalAlignment.Stretch;
            imgResMathcad.Height = 275;
            imgResMathcad.MouseLeftButtonDown += new MouseButtonEventHandler(ImageResMathcad_MouseLeftButtonDown);
            imgResMathcad.Stretch = Stretch.Fill;
            bmpImg = new BitmapImage();
            bmpImg.UriSource = uri;
            imgResMathcad.Source = bmpImg;
            StackResMathcad.Children.Clear();
            StackResMathcad.Children.Add(imgResMathcad);
        }

        private void listBoxDifferential_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyVisiblePanel();
            Diffrential.Visibility = Visibility.Visible;
            if (MyTabControl.SelectedIndex == 0)
            {
                StoryboardDifferential.Begin();
            }
            else
            {
                Diffrential.Opacity = 100;
            }
            param1 = 0;
            param2 = 0;
            param3 = 0;
            param4 = 0;
            NameMethod = "";
            processingData = new ProcessingData();
            switch (listBoxDifferential.SelectedIndex)
            {
                case 0:
                    NameMethod = "Euler Simple";
                    processingData.InpuParamMethod("Differential_Equations", "EulerSimple");
                    Diffrential.txtInput1.Text = processingData.ParamInput1;
                    Diffrential.txtInput2.Text = processingData.ParamInput2;
                    Diffrential.txtInput3.Text = processingData.ParamInput3;
                    Diffrential.txtInput4.Text = processingData.ParamInput4;
                    textBoxSource.Text = AllMethodText.DifferentialEquations_EulerSimple;
                    Diffrential.nameFunction.Text = processingData.TestFunnction;

                    break;
                case 1:
                    NameMethod = "Euler Modified";
                    processingData.InpuParamMethod("Differential_Equations", "EulerModified");
                    Diffrential.txtInput1.Text = processingData.ParamInput1;
                    Diffrential.txtInput2.Text = processingData.ParamInput2;
                    Diffrential.txtInput3.Text = processingData.ParamInput3;
                    Diffrential.txtInput4.Text = processingData.ParamInput4;
                    Diffrential.nameFunction.Text = processingData.TestFunnction;
                    textBoxSource.Text = AllMethodText.DifferentialEquations_EulerModified;

                    break;
                case 2:
                    NameMethod = "Euler Corrected";
                    processingData.InpuParamMethod("Differential_Equations", "EulerCorrected");
                    Diffrential.txtInput1.Text = processingData.ParamInput1;
                    Diffrential.txtInput2.Text = processingData.ParamInput2;
                    Diffrential.txtInput3.Text = processingData.ParamInput3;
                    Diffrential.txtInput4.Text = processingData.ParamInput4;
                    Diffrential.nameFunction.Text = processingData.TestFunnction;
                    textBoxSource.Text = AllMethodText.DifferentialEquations_EulerCorrected;
                    break;
                case 3:
                    NameMethod = "Runge-Kutta4";
                    processingData.InpuParamMethod("Differential_Equations", "RungeKutta4");
                    Diffrential.txtInput1.Text = processingData.ParamInput1;
                    Diffrential.txtInput2.Text = processingData.ParamInput2;
                    Diffrential.txtInput3.Text = processingData.ParamInput3;
                    Diffrential.txtInput4.Text = processingData.ParamInput4;
                    Diffrential.nameFunction.Text = processingData.TestFunnction;
                    textBoxSource.Text = AllMethodText.DifferentialEquations_RungeKutta4;
                    break;
            };
            uri = new Uri("/Data/DifferentialEquations/DirrefentialEquations.JPG", UriKind.Relative);
            imgResMathcad = new Image();
            imgResMathcad.HorizontalAlignment = HorizontalAlignment.Stretch;
            imgResMathcad.VerticalAlignment = VerticalAlignment.Stretch;
            imgResMathcad.Height = 275;
            imgResMathcad.Stretch = Stretch.Fill;
            bmpImg = new BitmapImage();
            bmpImg.UriSource = uri;
            imgResMathcad.Source = bmpImg;
            StackResMathcad.Children.Clear();
            StackResMathcad.Children.Add(imgResMathcad);
        }

        private void listBoxLinearSystems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyVisiblePanel();
            LinearSystem.Visibility = Visibility.Visible;
            if (MyTabControl.SelectedIndex == 0)
            {
                StoryboardLinearSystem.Begin();
            }
            else
            {
                LinearSystem.Opacity = 100;
            }

            NameMethod = "";
            processingData = new ProcessingData();

            switch (listBoxLinearSystems.SelectedIndex)
            {
                case 0:
                    NameMethod = "Gaus";
                    processingData.InpuLinearSystems("LinearSystems", "Gaus");
                    LinSysMasA = processingData.LinSysMasA;
                    LinSysMatrixB = processingData.LinSysMatrixB;
                    rangeArray = processingData.rangeArray;
                    LinearSystem.MatrixA = LinSysMasA;
                    LinearSystem.MatrixB = LinSysMatrixB;
                    LinearSystem.Range = rangeArray;
                    textBoxSource.Text = AllMethodText.SystemLinearEqualizations_Gaus;
                    int x = 0, y = 0;
                    int NumberRows = processingData.rangeArray;
                    int NumberCols = processingData.rangeArray * 2 + 1;
                    LinearSystem.nameRange.Text = processingData.rangeArray.ToString();
                    LinearSystem.nameLinearSystem.Text = "";

                    for (int i = 0; i < NumberRows; i++)
                    {
                        for (int j = 1; j <= NumberCols; j++)
                        {
                            if (j % 2 == 1)
                            {
                                if (NumberCols == j)
                                {
                                    LinearSystem.nameLinearSystem.Text = LinearSystem.nameLinearSystem.Text + LinSysMasA[x].ToString() + "\n";
                                    x++;
                                    y = 0;
                                }
                                else
                                    LinearSystem.nameLinearSystem.Text = LinearSystem.nameLinearSystem.Text + LinSysMatrixB[x, y].ToString() + "";
                            }
                            else
                            {
                                y++;
                                if (NumberCols == j + 1)

                                    LinearSystem.nameLinearSystem.Text = LinearSystem.nameLinearSystem.Text + "*x" + y + "=";
                                else
                                    LinearSystem.nameLinearSystem.Text = LinearSystem.nameLinearSystem.Text + "*x" + y + "+";
                            }
                        }
                    }

                    break;
                case 1:
                    NameMethod = "Zeidel";
                    processingData.InpuParamMethod("LinearSystems", "Zeidel");

                    textBoxSource.Text = AllMethodText.SystemLinearEqualizations_Zeidel;

                    break;

            };
            uri = new Uri("/Data/LinearSystems/allMethods.JPG", UriKind.Relative);
            imgResMathcad = new Image();
            imgResMathcad.HorizontalAlignment = HorizontalAlignment.Stretch;
            imgResMathcad.VerticalAlignment = VerticalAlignment.Stretch;
            imgResMathcad.Height = 275;
            imgResMathcad.Stretch = Stretch.Fill;
            bmpImg = new BitmapImage();
            bmpImg.UriSource = uri;
            imgResMathcad.Source = bmpImg;
            StackResMathcad.Children.Clear();
            StackResMathcad.Children.Add(imgResMathcad);
        }

        private void listBoxNonLinear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyVisiblePanel();
            NonLinear.Visibility = Visibility.Visible;
            if (MyTabControl.SelectedIndex == 0)
            {
                StoryboardNonLinear.Begin();
            }
            else
            {
                NonLinear.Opacity = 100;
            }

            param1 = 0;
            param2 = 0;
            param3 = 0;
            param4 = 0;
            NameMethod = "";
            processingData = new ProcessingData();

            switch (listBoxNonLinear.SelectedIndex)
            {
                case 0:
                    NameMethod = "Half Division";
                    processingData.InpuParamMethod("NonLinearEqualization", "HalfDivision");
                    NonLinear.txtInput1.Text = processingData.ParamInput1;
                    NonLinear.txtInput2.Text = processingData.ParamInput2;
                    NonLinear.txtInput3.Text = processingData.ParamInput3;
                    NonLinear.text3.Visibility = Visibility.Visible;
                    NonLinear.txtInput3.Visibility = Visibility.Visible;
                    textBoxSource.Text = AllMethodText.NonLinearEquations_HalfDivision;
                    NonLinear.nameFunction.Text = processingData.TestFunnction;
                    break;
                case 1:
                    NameMethod = "Hord Metod";
                    processingData.InpuParamMethod("NonLinearEqualization", "HordMetod");
                    NonLinear.txtInput1.Text = processingData.ParamInput1;
                    NonLinear.txtInput2.Text = processingData.ParamInput2;
                    NonLinear.txtInput3.Text = processingData.ParamInput3;
                    NonLinear.text3.Visibility = Visibility.Visible;
                    NonLinear.txtInput3.Visibility = Visibility.Visible;
                    NonLinear.nameFunction.Text = processingData.TestFunnction;
                    textBoxSource.Text = AllMethodText.NonLinearEquations_MetodHord;

                    break;
                case 2:
                    NameMethod = "Newton Metod";
                    processingData.InpuParamMethod("NonLinearEqualization", "NewtonMetod");
                    NonLinear.txtInput1.Text = processingData.ParamInput1;
                    NonLinear.txtInput2.Text = processingData.ParamInput2;
                    NonLinear.text3.Visibility = Visibility.Collapsed;
                    NonLinear.txtInput3.Visibility = Visibility.Collapsed;
                    NonLinear.nameFunction.Text = processingData.TestFunnction;
                    textBoxSource.Text = AllMethodText.NonLinearEquations_Newton;
                    break;
                case 3:
                    NameMethod = "Secant Metod";
                    processingData.InpuParamMethod("NonLinearEqualization", "SecantMetod");
                    NonLinear.txtInput1.Text = processingData.ParamInput1;
                    NonLinear.txtInput2.Text = processingData.ParamInput2;
                    NonLinear.text3.Visibility = Visibility.Collapsed;
                    NonLinear.txtInput3.Visibility = Visibility.Collapsed;
                    NonLinear.nameFunction.Text = processingData.TestFunnction;
                    textBoxSource.Text = AllMethodText.NonLinearEquations_Secant;
                    break;
            };
            uri = new Uri("/Data/NonLinear/allMethods.JPG", UriKind.Relative);
            imgResMathcad = new Image();
            imgResMathcad.HorizontalAlignment = HorizontalAlignment.Stretch;
            imgResMathcad.VerticalAlignment = VerticalAlignment.Stretch;
            imgResMathcad.Height = 275;
            imgResMathcad.Stretch = Stretch.Fill;
            bmpImg = new BitmapImage();
            bmpImg.UriSource = uri;
            imgResMathcad.Source = bmpImg;
            StackResMathcad.Children.Clear();
            StackResMathcad.Children.Add(imgResMathcad);
        }

        private void listBoxMatrixAlgebra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyVisiblePanel();
            MatrixAlgebra.Visibility = Visibility.Visible;
            if (MyTabControl.SelectedIndex == 0)
            {
                StoryboardMatrixAlgebra.Begin();
            }
            else
            {
                MatrixAlgebra.Opacity = 100;
            }

            NameMethod = "";
            processingData = new ProcessingData();

            switch (listBoxMatrixAlgebra.SelectedIndex)
            {
                case 0:
                    NameMethod = "Matrix Determinant";
                    processingData.InpuMatrixAlgebra("MatrixAlgebra", "MatrixDeterminant");

                    MatrixAlgebraA = processingData.MatrixAlgebraA; ;
                    rangeArray = processingData.rangeArray;
                    textBoxSource.Text = AllMethodText.MatrixAlgebra_MatrixDeterminan;
                    MatrixAlgebra.nameRange.Text = processingData.rangeArray.ToString();
                    MatrixAlgebra.nameMatrix.Text = "";

                    for (int i = 0; i < processingData.rangeArray; i++)
                    {
                        for (int j = 0; j < processingData.rangeArray; j++)
                        {


                            MatrixAlgebra.nameMatrix.Text = MatrixAlgebra.nameMatrix.Text + MatrixAlgebraA[i, j].ToString() + " ";


                            if (processingData.rangeArray == j + 1)
                            {
                                MatrixAlgebra.nameMatrix.Text = MatrixAlgebra.nameMatrix.Text + "\n";
                            }
                        }
                    }
                    uri = new Uri("/Data/MatrixAlgebra/MatrixDeterminant.jpg", UriKind.Relative);
                    break;
                case 1:
                    NameMethod = "RMatrix LU";
                    processingData.InpuMatrixAlgebra("MatrixAlgebra", "RMatrixLU");

                    MatrixAlgebraA = processingData.MatrixAlgebraA; ;
                    rangeArray = processingData.rangeArray;

                    textBoxSource.Text = AllMethodText.MatrixAlgebra_RMatrixLU;
                    MatrixAlgebra.nameRange.Text = processingData.rangeArray.ToString();
                    MatrixAlgebra.nameMatrix.Text = "";

                    for (int i = 0; i < processingData.rangeArray; i++)
                    {
                        for (int j = 0; j < processingData.rangeArray; j++)
                        {
                            MatrixAlgebra.nameMatrix.Text = MatrixAlgebra.nameMatrix.Text + MatrixAlgebraA[i, j].ToString() + " ";
                            if (processingData.rangeArray == j + 1)
                            {
                                MatrixAlgebra.nameMatrix.Text = MatrixAlgebra.nameMatrix.Text + "\n";
                            }
                        }
                    }
                    uri = new Uri("/Data/MatrixAlgebra/MatrixLU.jpg", UriKind.Relative);
                    break;
                case 2:
                    NameMethod = "Matrix Inverse LU";
                    processingData.InpuMatrixAlgebra("MatrixAlgebra", "MatrixInverseLU");

                    MatrixAlgebraA = processingData.MatrixAlgebraA; ;
                    rangeArray = processingData.rangeArray;
                    textBoxSource.Text = AllMethodText.MatrixAlgebra_RMatrixLuInverse;
                    MatrixAlgebra.nameRange.Text = processingData.rangeArray.ToString();
                    MatrixAlgebra.nameMatrix.Text = "";

                    for (int i = 0; i < processingData.rangeArray; i++)
                    {
                        for (int j = 0; j < processingData.rangeArray; j++)
                        {
                            MatrixAlgebra.nameMatrix.Text = MatrixAlgebra.nameMatrix.Text + MatrixAlgebraA[i, j].ToString() + " ";
                            if (processingData.rangeArray == j + 1)
                            {
                                MatrixAlgebra.nameMatrix.Text = MatrixAlgebra.nameMatrix.Text + "\n";
                            }
                        }
                    }
                    uri = new Uri("/Data/MatrixAlgebra/MatrixInverse.jpg", UriKind.Relative);
                    break;

            };

            imgResMathcad = new Image();
            imgResMathcad.HorizontalAlignment = HorizontalAlignment.Stretch;
            imgResMathcad.VerticalAlignment = VerticalAlignment.Stretch;
            imgResMathcad.Height = 275;
            imgResMathcad.Stretch = Stretch.Fill;
            bmpImg = new BitmapImage();
            bmpImg.UriSource = uri;
            imgResMathcad.Source = bmpImg;
            StackResMathcad.Children.Clear();
            StackResMathcad.Children.Add(imgResMathcad);
        }



        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            textBoxResult.Text = "";
            
            CalculateMethods calculate = new CalculateMethods();
            ReadData();

            calculate.ExecuteMethod(NameMethod, param1, param2, param3, param4,
                TestFunction, rangeArray, LinSysMasA, LinSysMatrixB,
                massX, massF, massW, pointInterpolation,
                MatrixAlgebraA,
                pointPercentile,
                pointGenerator);
            textBoxResult.Text = calculate.GetSolution();


        }

        private void listBoxIntegration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyVisiblePanel();

            CntrIntegration.Visibility = Visibility.Visible;
            if (MyTabControl.SelectedIndex == 0)
            {
                StoryboardIntegration.Begin();
            }
            else
            {
                CntrIntegration.Opacity = 100;
            }
            NameMethod = "";
            processingData = new ProcessingData();
            switch (listBoxIntegration.SelectedIndex)
            {
                case 0:
                    NameMethod = "Chebishev";
                    processingData.InpuParamMethod("Integration", "Chebishev");
                    // processingData.InpuParamMethod("Integration", "Chebishev");
                    CntrIntegration.txtInput1.Text = processingData.ParamInput1;
                    CntrIntegration.txtInput2.Text = processingData.ParamInput2;
                    CntrIntegration.txtInput3.Text = processingData.ParamInput3;
                    CntrIntegration.nameFunction.Text = processingData.TestFunnction;
                    textBoxSource.Text = AllMethodText.Integration_Chebishev;
                    break;
                case 1:
                    NameMethod = "Simpson";
                    processingData.InpuParamMethod("Integration", "Simpson");
                    CntrIntegration.txtInput1.Text = processingData.ParamInput1;
                    CntrIntegration.txtInput2.Text = processingData.ParamInput2;
                    CntrIntegration.txtInput3.Text = processingData.ParamInput3;
                    CntrIntegration.nameFunction.Text = processingData.TestFunnction;
                    textBoxSource.Text = AllMethodText.Integration_Simpson;

                    break;
                case 2:
                    NameMethod = "Simpson2";
                    processingData.InpuParamMethod("Integration", "Simpson2");
                    CntrIntegration.txtInput1.Text = processingData.ParamInput1;
                    CntrIntegration.txtInput2.Text = processingData.ParamInput2;
                    CntrIntegration.txtInput3.Text = processingData.ParamInput3;
                    CntrIntegration.nameFunction.Text = processingData.TestFunnction;
                    textBoxSource.Text = AllMethodText.Integration_Simpson2;

                    break;
                case 3:
                    NameMethod = "Trapezium";
                    processingData.InpuParamMethod("Integration", "Trapezium");
                    CntrIntegration.txtInput1.Text = processingData.ParamInput1;
                    CntrIntegration.txtInput2.Text = processingData.ParamInput2;
                    CntrIntegration.txtInput3.Text = processingData.ParamInput3;
                    CntrIntegration.nameFunction.Text = processingData.TestFunnction;
                    textBoxSource.Text = AllMethodText.Integration_Trapezium;

                    break;
            };
            uri = new Uri("/Data/Integration/AllMethods.JPG", UriKind.Relative);
            imgResMathcad = new Image();
            imgResMathcad.HorizontalAlignment = HorizontalAlignment.Stretch;
            imgResMathcad.VerticalAlignment = VerticalAlignment.Stretch;
            imgResMathcad.Height = 275;
            imgResMathcad.Stretch = Stretch.Fill;
            bmpImg = new BitmapImage();
            bmpImg.UriSource = uri;
            imgResMathcad.Source = bmpImg;
            StackResMathcad.Children.Clear();
            StackResMathcad.Children.Add(imgResMathcad);
        }
        private void listBoxInterpolation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyVisiblePanel();

            Interpolation.Visibility = Visibility.Visible;
            if (MyTabControl.SelectedIndex == 0)
            {
                StoryboardInterpolation.Begin();
            }
            else
            {
                Interpolation.Opacity = 100;
            }
            NameMethod = "";
            processingData = new ProcessingData();
            Interpolation.NameInputFun.Text = "X:\nF:\n";
            switch (listBoxInterpolation.SelectedIndex)
            {
                case 0:
                    NameMethod = "Lagrange Interpolator";
                    processingData.InpuInterpolation("Interpolation", "LagrangeInterpolator");
                    massX = processingData.massX;
                    massF = processingData.massF;
                    Interpolation.nameFunInterpolation.Text = "";
                    pointInterpolation = processingData.pointInterpolation;
                    Interpolation.txtInputData.Text = pointInterpolation.ToString();
                    for (int i = 0; i < processingData.massX.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massX[i].ToString() + ";";
                    }
                    Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + "\n";
                    for (int i = 0; i < processingData.massF.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massF[i].ToString() + ";";
                    }
                    textBoxSource.Text = AllMethodText.Interpolation_BarycentricInterpolation;
                    break;
                case 1:
                    NameMethod = "Newton Interpolator";
                    processingData.InpuInterpolation("Interpolation", "NewtonInterpolator");
                    massX = processingData.massX;
                    massF = processingData.massF;
                    Interpolation.nameFunInterpolation.Text = "";
                    pointInterpolation = processingData.pointInterpolation;
                    Interpolation.txtInputData.Text = pointInterpolation.ToString();
                    for (int i = 0; i < processingData.massX.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massX[i].ToString() + ";";
                    }
                    Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + "\n";
                    for (int i = 0; i < processingData.massF.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massF[i].ToString() + ";";
                    }
                    textBoxSource.Text = AllMethodText.Interpolation_NewtonInterpolator;

                    break;
                case 2:
                    NameMethod = "Neville Interpolator";
                    processingData.InpuInterpolation("Interpolation", "NevilleInterpolator");
                    massX = processingData.massX;
                    massF = processingData.massF;
                    Interpolation.nameFunInterpolation.Text = "";
                    pointInterpolation = processingData.pointInterpolation;
                    Interpolation.txtInputData.Text = pointInterpolation.ToString();
                    for (int i = 0; i < processingData.massX.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massX[i].ToString() + ";";
                    }
                    Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + "\n";
                    for (int i = 0; i < processingData.massF.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massF[i].ToString() + ";";
                    }
                    textBoxSource.Text = AllMethodText.Interpolation_NevilleInterpolator;

                    break;
                case 3:
                    NameMethod = "Spline Interpolator";
                    processingData.InpuInterpolation("Interpolation", "SplineInterpolator");
                    massX = processingData.massX;
                    massF = processingData.massF;
                    Interpolation.nameFunInterpolation.Text = "";
                    pointInterpolation = processingData.pointInterpolation;
                    Interpolation.txtInputData.Text = pointInterpolation.ToString();
                    for (int i = 0; i < processingData.massX.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massX[i].ToString() + ";";
                    }
                    Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + "\n";
                    for (int i = 0; i < processingData.massF.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massF[i].ToString() + ";";
                    }
                    textBoxSource.Text = AllMethodText.Interpolation_SplineInterpolator;

                    break;
                case 4:

                    NameMethod = "Barycentric Interpolator";
                    processingData.InpuInterpolation("Interpolation", "BarycentricInterpolator");
                    massX = processingData.massX;
                    massF = processingData.massF;
                    massW = processingData.massW;
                    pointInterpolation = processingData.pointInterpolation;
                    Interpolation.NameInputFun.Text = "";
                    Interpolation.NameInputFun.Text = "X:\nF:\nW:\n";
                    Interpolation.nameFunInterpolation.Text = "";
                    Interpolation.txtInputData.Text = pointInterpolation.ToString();
                    for (int i = 0; i < massX.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massX[i].ToString() + ";";
                    }
                    Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + "\n";
                    for (int i = 0; i < massF.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massF[i].ToString() + ";";
                    }
                    Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + "\n";
                    for (int i = 0; i < massW.Length; i++)
                    {
                        Interpolation.nameFunInterpolation.Text = Interpolation.nameFunInterpolation.Text + massW[i].ToString() + ";";
                    }
                    textBoxSource.Text = AllMethodText.Interpolation_BarycentricInterpolation;

                    break;
            };
            uri = new Uri("/Data/Interpolation/AllMethods.JPG", UriKind.Relative);
            imgResMathcad = new Image();
            imgResMathcad.HorizontalAlignment = HorizontalAlignment.Stretch;
            imgResMathcad.VerticalAlignment = VerticalAlignment.Stretch;
            imgResMathcad.Height = 275;
            imgResMathcad.Stretch = Stretch.Fill;
            bmpImg = new BitmapImage();
            bmpImg.UriSource = uri;
            imgResMathcad.Source = bmpImg;
            StackResMathcad.Children.Clear();
            StackResMathcad.Children.Add(imgResMathcad);
        }
        private void MyFullscreen_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Host.Content.IsFullScreen = !App.Current.Host.Content.IsFullScreen;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            ExpDifferential.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;
            ExpStatistics.IsExpanded = false;
            ExpNonLinear.IsExpanded = false;
            ExpApproximate.IsExpanded = false;
            MyTabControl.Visibility = Visibility.Collapsed;
            //MaineTable.Visibility = Visibility.Collapsed;
            textBoxResult.Visibility = Visibility.Collapsed;
            StackResMathcad.Visibility = Visibility.Collapsed;

            panelStart.Visibility = Visibility.Collapsed;
            btnStart.Visibility = Visibility.Collapsed;
            MaineProgram.Visibility = Visibility.Visible;
            StoryboardStartProgram.Begin();
        }



        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            btnStart.Visibility = Visibility.Visible;
            panelStart.Visibility = Visibility.Visible;
            StoryboardStartScreen.Stop();
            MyTabControl.Visibility = Visibility.Collapsed;
            StoryboardStartProgram.Stop();
            MaineProgram.Visibility = Visibility.Collapsed;
        }

        private void listBoxOptimizing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyVisiblePanel();
            Optimizing.Visibility = Visibility.Visible;
            if (MyTabControl.SelectedIndex == 0)
            {
                StoryboardOptimizing.Begin();
            }
            else
            {
                Optimizing.Opacity = 100;
            }
            processingData = new ProcessingData();
            switch (listBoxOptimizing.SelectedIndex)
            {
                case 0:
                    textBoxSource.Text = AllMethodText.Optimizing_Brentopt;

                    NameMethod = "Brentopt";
                    processingData.InpuParamMethod("Optimizing", "Brentopt");
                    Optimizing.txtInput1.Text = processingData.ParamInput1;
                    Optimizing.txtInput2.Text = processingData.ParamInput2;
                    Optimizing.txtInput3.Text = processingData.ParamInput3;
                    Optimizing.txtInput4.Visibility = Visibility.Collapsed;
                    Optimizing.txtName4.Visibility = Visibility.Collapsed;
                    Optimizing.nameFunction.Text = processingData.TestFunnction;

                    break;
                case 1:
                    textBoxSource.Text = AllMethodText.Optimizing_Brentopt;

                    NameMethod = "Golden Section";
                    processingData.InpuParamMethod("Optimizing", "GoldenSection");
                    Optimizing.txtInput1.Text = processingData.ParamInput1;
                    Optimizing.txtInput2.Text = processingData.ParamInput2;
                    Optimizing.txtInput3.Text = processingData.ParamInput3;
                    Optimizing.txtName3.Text = "Points number";
                    Optimizing.txtInput4.Visibility = Visibility.Collapsed;
                    Optimizing.txtName4.Visibility = Visibility.Collapsed;
                    Optimizing.nameFunction.Text = processingData.TestFunnction;

                    break;
                case 2:
                    textBoxSource.Text = AllMethodText.Optimizing_Pijavsky;
                    NameMethod = "Pijavsky";
                    processingData.InpuParamMethod("Optimizing", "Pijavsky");
                    AproximateOther.txtInput1.Text = processingData.ParamInput1;
                    AproximateOther.txtInput2.Text = processingData.ParamInput2;
                    AproximateOther.txtInput3.Text = processingData.ParamInput3;
                    AproximateOther.txtInput4.Text = processingData.ParamInput4;
                    Optimizing.txtName3.Text = "Constant of lipshits(>0)";
                    Optimizing.txtInput4.Visibility = Visibility.Visible;
                    Optimizing.txtName4.Visibility = Visibility.Visible;
                    AproximateOther.nameFunction.Text = processingData.TestFunnction;

                    break;

            };
            uri = new Uri("/Data/Optimizing/AllMethods.jpg", UriKind.Relative);
            imgResMathcad = new Image();
            imgResMathcad.HorizontalAlignment = HorizontalAlignment.Stretch;
            imgResMathcad.VerticalAlignment = VerticalAlignment.Stretch;
            imgResMathcad.Height = 275;
            imgResMathcad.MouseLeftButtonDown += new MouseButtonEventHandler(ImageResMathcad_MouseLeftButtonDown);
            imgResMathcad.Stretch = Stretch.Fill;
            bmpImg = new BitmapImage();
            bmpImg.UriSource = uri;
            imgResMathcad.Source = bmpImg;
            StackResMathcad.Children.Clear();
            StackResMathcad.Children.Add(imgResMathcad);
        }

        private void listBoxStatistics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyVisiblePanel();

            Statistics.Visibility = Visibility.Visible;
            if (MyTabControl.SelectedIndex == 0)
            {
                StoryboardStatistics.Begin();
            }
            else
            {
                Statistics.Opacity = 100;
            }
            NameMethod = "";
            processingData = new ProcessingData();
            switch (listBoxStatistics.SelectedIndex)
            {
                case 0:
                    NameMethod = "Correlation Pearson";
                    processingData.InpuStatistics("Statistics", "CorrelationPearson");
                    massX = processingData.massX;
                    massF = processingData.massF;
                    Statistics.NameInputFun1.Text = "X:\nF:";
                    Statistics.nameFunStatistics.Text = "";
                    Statistics.txtInputData1.Visibility = Visibility.Collapsed;
                    Statistics.text2.Visibility = Visibility.Collapsed;
                    Statistics.txtInputData1.Text = pointInterpolation.ToString();
                    for (int i = 0; i < processingData.massX.Length; i++)
                    {
                        Statistics.nameFunStatistics.Text = Statistics.nameFunStatistics.Text + massX[i].ToString() + ";";
                    }
                    Statistics.nameFunStatistics.Text = Statistics.nameFunStatistics.Text + "\n";
                    for (int i = 0; i < processingData.massF.Length; i++)
                    {
                        Statistics.nameFunStatistics.Text = Statistics.nameFunStatistics.Text + massF[i].ToString() + ";";
                    }
                    textBoxSource.Text = AllMethodText.Statistics_CorrelationPearson;
                    uri = new Uri("/Data/Statistics/CorrelationPearson.JPG", UriKind.Relative);
                    break;
                case 1:
                    NameMethod = "Correlation Spearmans Rank";
                    processingData.InpuStatistics("Statistics", "CorrelationSpearmansRank");
                    massX = processingData.massX;
                    massF = processingData.massF;
                    Statistics.NameInputFun1.Text = "X:\nF:";
                    Statistics.nameFunStatistics.Text = "";
                    Statistics.txtInputData1.Visibility = Visibility.Collapsed;
                    Statistics.text2.Visibility = Visibility.Collapsed;
                    Statistics.txtInputData1.Text = pointInterpolation.ToString();
                    for (int i = 0; i < processingData.massX.Length; i++)
                    {
                        Statistics.nameFunStatistics.Text = Statistics.nameFunStatistics.Text + massX[i].ToString() + ";";
                    }
                    Statistics.nameFunStatistics.Text = Statistics.nameFunStatistics.Text + "\n";
                    for (int i = 0; i < processingData.massF.Length; i++)
                    {
                        Statistics.nameFunStatistics.Text = Statistics.nameFunStatistics.Text + massF[i].ToString() + ";";
                    }
                    textBoxSource.Text = AllMethodText.Statistics_CorrelationSpearmansRank;
                    uri = new Uri("/Data/Statistics/CorrelationPearson.JPG", UriKind.Relative);
                    break;

                case 2:
                    NameMethod = "Descriptive Statistics Median";
                    processingData.InpuStatistics("Statistics", "DescriptiveStatisticsMedian");
                    massX = processingData.massX;
                    Statistics.NameInputFun1.Text = "X:\n";
                    Statistics.nameFunStatistics.Text = "";
                    Statistics.txtInputData1.Visibility = Visibility.Collapsed;
                    Statistics.text2.Visibility = Visibility.Collapsed;
                    Statistics.txtInputData1.Text = pointInterpolation.ToString();
                    for (int i = 0; i < processingData.massX.Length; i++)
                    {
                        Statistics.nameFunStatistics.Text = Statistics.nameFunStatistics.Text + massX[i].ToString() + ";";
                    }
                    textBoxSource.Text = AllMethodText.Statistics_DescriptiveStatisticsADev;
                    uri = new Uri("/Data/Statistics/Median.JPG", UriKind.Relative);
                    break;
                case 3:
                    NameMethod = "Descriptive Statistics Moments";
                    processingData.InpuStatistics("Statistics", "DescriptiveStatisticsMoments");
                    massX = processingData.massX;
                    Statistics.NameInputFun1.Text = "X:\n";
                    Statistics.nameFunStatistics.Text = "";
                    Statistics.txtInputData1.Visibility = Visibility.Collapsed;
                    Statistics.text2.Visibility = Visibility.Collapsed;
                    Statistics.txtInputData1.Text = pointInterpolation.ToString();
                    for (int i = 0; i < processingData.massX.Length; i++)
                    {
                        Statistics.nameFunStatistics.Text = Statistics.nameFunStatistics.Text + massX[i].ToString() + ";";
                    }
                    textBoxSource.Text = AllMethodText.Statistics_DescriptiveStatisticsADev;
                    uri = new Uri("/Data/Statistics/Moment.JPG", UriKind.Relative);
                    break;
                case 4:
                    NameMethod = "Descriptive Statistics Percentile";
                    processingData.InpuStatistics("Statistics", "DescriptiveStatisticsPercentile");
                    massX = processingData.massX;
                    pointPercentile = processingData.pointPercentile;
                    Statistics.NameInputFun1.Text = "X:\n";
                    Statistics.pointPercentile = pointPercentile;
                    Statistics.txtInputData1.Visibility = Visibility.Visible;
                    Statistics.txtInputData1.Text = pointPercentile.ToString();
                    Statistics.text2.Visibility = Visibility.Visible;
                    Statistics.nameFunStatistics.Text = "";
                    for (int i = 0; i < processingData.massX.Length; i++)
                    {
                        Statistics.nameFunStatistics.Text = Statistics.nameFunStatistics.Text + massX[i].ToString() + ";";
                    }
                    textBoxSource.Text = AllMethodText.Statistics_DescriptiveStatisticsADev;
                    uri = new Uri("/Data/Statistics/Percentile.JPG", UriKind.Relative);
                    break;
            };

            imgResMathcad = new Image();
            imgResMathcad.HorizontalAlignment = HorizontalAlignment.Stretch;
            imgResMathcad.VerticalAlignment = VerticalAlignment.Stretch;
            imgResMathcad.Height = 275;
            imgResMathcad.Stretch = Stretch.Fill;
            bmpImg = new BitmapImage();
            bmpImg.UriSource = uri;
            imgResMathcad.Source = bmpImg;
            StackResMathcad.Children.Clear();
            StackResMathcad.Children.Add(imgResMathcad);
        }

        private void listBoxRandomGenerator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyVisiblePanel();

            RandomGenerator.Visibility = Visibility.Visible;
            if (MyTabControl.SelectedIndex == 0)
            {
                StoryboardRandomGenerator.Begin();
            }
            else
            {
                RandomGenerator.Opacity = 100;
            }
            NameMethod = "";
            processingData = new ProcessingData();
            switch (listBoxRandomGenerator.SelectedIndex)
            {
                case 0:
                    NameMethod = "Generator 1";
                    RandomGenerator.txtSampleName.Text = "\n\nGenerator of the evenly distributed random material numbers  in a range [0, 1]";
                    RandomGenerator.textName.Visibility = Visibility.Collapsed;
                    RandomGenerator.txtInputData.Visibility = Visibility.Collapsed;
                    textBoxSource.Text = AllMethodText.RandomGeneratorsMethod1;

                    break;
                case 1:
                    NameMethod = "Generator 2";
                    RandomGenerator.textName.Visibility = Visibility.Visible;
                    RandomGenerator.txtInputData.Visibility = Visibility.Visible;

                    RandomGenerator.txtSampleName.Text = "\nGenerator of the evenly distributed random material numbers in a range [0, N]";
                    RandomGenerator.textName.Text = "Input range N:";
                    processingData.InpuRandomGenerator("RandomGenerator", "Generator2");
                    pointGenerator = processingData.pointGenerator;

                    RandomGenerator.txtInputData.Text = pointGenerator.ToString();
                    textBoxSource.Text = AllMethodText.RandomGeneratorsMethod2;

                    break;
                case 2:
                    NameMethod = "Generator 3";
                    textBoxSource.Text = AllMethodText.RandomGeneratorsMethod3;
                    RandomGenerator.txtSampleName.Text = " Generator of the normally distributed random numbers. Generates two independent random   numbers,having standard distributing. On the expenses of time equal to podprogramme of RndNormal,generating one random number.";
                    RandomGenerator.textName.Visibility = Visibility.Collapsed;
                    RandomGenerator.txtInputData.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    NameMethod = "Generator 4";
                    textBoxSource.Text = AllMethodText.RandomGeneratorsMethod4;
                    RandomGenerator.txtSampleName.Text = " Generator of the normally distributed random numbers.Generates one random  number, having  the standard  distributing. On the expenses of time equal to podprogramme of RndNormal2, to generating two random numbers";
                    RandomGenerator.textName.Visibility = Visibility.Collapsed;
                    RandomGenerator.txtInputData.Visibility = Visibility.Collapsed;
                    break;
                case 4:
                    NameMethod = "Generator 5";
                    textBoxSource.Text = AllMethodText.RandomGeneratorsMethod5;
                    RandomGenerator.txtSampleName.Text = " \n          Generator of the exponentially distributed random numbers.";
                    RandomGenerator.textName.Text = "Input lambda N:";
                    processingData.InpuRandomGenerator("RandomGenerator", "Generator5");
                    RandomGenerator.textName.Visibility = Visibility.Visible;
                    RandomGenerator.txtInputData.Visibility = Visibility.Visible;
                    pointGenerator = processingData.pointGenerator;
                    RandomGenerator.txtInputData.Text = pointGenerator.ToString();
                    break;

            };
            /* uri = new Uri("/Data/Integration/AllMethods.JPG", UriKind.Relative);
             imgResMathcad = new Image();
             imgResMathcad.HorizontalAlignment = HorizontalAlignment.Stretch;
             imgResMathcad.VerticalAlignment = VerticalAlignment.Stretch;
             imgResMathcad.Height = 275;
             imgResMathcad.Stretch = Stretch.Fill;
             bmpImg = new BitmapImage();
             bmpImg.UriSource = uri;
             imgResMathcad.Source = bmpImg;
             StackResMathcad.Children.Clear();
             StackResMathcad.Children.Add(imgResMathcad);*/
        }

        private void imageInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StoryboardAbout.Begin();

        }
        private void ExpandedVisible()
        {
            StoryboardStartScreen.Begin();
            MyTabControl.Visibility = Visibility.Visible;
            MyVisiblePanel();
            textBoxResult.Text = "";
            textBoxResult.Visibility = Visibility.Collapsed;
            StackResMathcad.Visibility = Visibility.Collapsed;
            MyTabControl.SelectedIndex = 0;
            /*ExpDifferential.IsExpanded = false;
            ExpApproximate.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;
            ExpStatistics.IsExpanded = false;*/

        }
        private void ExpApproximate_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandedVisible();

            ExpDifferential.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;
            ExpStatistics.IsExpanded = false;
            ExpNonLinear.IsExpanded = false;

            ExpApproximate.IsExpanded = true;
        }

        private void ExpDifferential_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandedVisible();

            ExpApproximate.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;
            ExpStatistics.IsExpanded = false;
            ExpNonLinear.IsExpanded = false;

            ExpDifferential.IsExpanded = true;
        }

        private void ExpIntegration_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandedVisible();
			
            ExpApproximate.IsExpanded = false;
            ExpDifferential.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;
            ExpStatistics.IsExpanded = false;
            ExpNonLinear.IsExpanded = false;

            ExpIntegration.IsExpanded = true;
        }

        private void ExpInterpolation_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandedVisible();
			
            ExpApproximate.IsExpanded = false;
            ExpDifferential.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;
            ExpStatistics.IsExpanded = false;
            ExpNonLinear.IsExpanded = false;

            ExpInterpolation.IsExpanded = true;
        }

        private void ExpLinearSystems_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandedVisible();
			
            ExpApproximate.IsExpanded = false;
            ExpDifferential.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;
            ExpStatistics.IsExpanded = false;
            ExpNonLinear.IsExpanded = false;

            ExpLinearSystems.IsExpanded = true;
        }

        private void ExpMatrixAlgebra_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandedVisible();
			
            ExpApproximate.IsExpanded = false;
            ExpDifferential.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;
            ExpStatistics.IsExpanded = false;
            ExpNonLinear.IsExpanded = false;

            ExpMatrixAlgebra.IsExpanded = true;
        }

        private void ExpOptimizing_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandedVisible();
			
            ExpApproximate.IsExpanded = false;
            ExpDifferential.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;
            ExpStatistics.IsExpanded = false;
            ExpNonLinear.IsExpanded = false;

            ExpOptimizing.IsExpanded = true;
        }

        private void ExpStatistics_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandedVisible();
			
            ExpApproximate.IsExpanded = false;
            ExpDifferential.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpNonLinear.IsExpanded = false;

            ExpStatistics.IsExpanded = true;
        }

        private void ExpRandomGenerator_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandedVisible();
			
            ExpApproximate.IsExpanded = false;
            ExpDifferential.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpStatistics.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpNonLinear.IsExpanded = false;

            ExpRandomGenerator.IsExpanded = true;
        }

        private void ExpNonLinear_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandedVisible();
			
            ExpApproximate.IsExpanded = false;
            ExpDifferential.IsExpanded = false;
            ExpIntegration.IsExpanded = false;
            ExpInterpolation.IsExpanded = false;
            ExpLinearSystems.IsExpanded = false;
            ExpMatrixAlgebra.IsExpanded = false;
            ExpStatistics.IsExpanded = false;
            ExpOptimizing.IsExpanded = false;
            ExpRandomGenerator.IsExpanded = false;

            ExpNonLinear.IsExpanded = true;
        }












    }
}
