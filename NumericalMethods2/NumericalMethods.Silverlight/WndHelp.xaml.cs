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
using Liquid;
using System.Collections.ObjectModel;
using NumericalMethods.Silverlight;
namespace NumericalMethods_Silverlight
{
    public partial class WndHelp : ChildWindow
    {
        private Button bMaine;
        private string[] images;
        private bool _rootBuilding = true;
        private bool SelectMenu = false;
        public WndHelp()
        {
            InitializeComponent();
            this.Title = "Help";
            NumericalMethodsTree.BuildRoot();
            NumericalMethodsTree.EnableCheckboxes = false;
            string image = "Maine.JPG";

            bMaine = new Button();
            bMaine.Template = (ControlTemplate)App.Current.Resources["ImageButtonTemplate"];
            bMaine.Content = "Image/HelpImages/" + image;
            bMaine.GotFocus += new RoutedEventHandler(ButtonGotFocus);
            bMaine.MouseEnter += new MouseEventHandler(ButtonEnter);
            bMaine.MouseLeave += new MouseEventHandler(ButtonLeave);
            MyPanel.Children.Add(bMaine);

            /*lastFocus = bMaine;
            VisualStateManager.GoToState(bMaine, "Selected", true);
          Storyboard s = (Storyboard)Resources["DropImageStoryboard"];
          s.Begin();
          MyLargeButton.Content = bMaine.Content;*/


            OnOpened();
        }
        protected override void OnOpened()
        {
            Storyboard s = (Storyboard)Resources["DropImageStoryboard"];
            s.Begin();
            MyLargeButton.Content = bMaine.Content;
        }
        private Button lastFocus;

        private void ButtonGotFocus(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b == lastFocus) return;
            if (lastFocus != null)
                VisualStateManager.GoToState(lastFocus, "Cold", true);
            lastFocus = b;
            VisualStateManager.GoToState(b, "Selected", true);
            Storyboard s = (Storyboard)Resources["DropImageStoryboard"];
            s.Begin();
            MyLargeButton.Content = b.Content;
            //	currentImageButton=new Button();
            //currentImageButton.Content=b.Content;
        }

        private void ButtonEnter(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b.IsFocused) return;
            VisualStateManager.GoToState(b, "Hot", true);
        }

        private void ButtonLeave(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b.IsFocused) return;
            VisualStateManager.GoToState(b, "Cold", true);
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        void testTree_NodeCheckChanged(object sender, TreeEventArgs e)
        {

        }
        private void Tree_NodeClick(object sender, EventArgs e)
        {
            Node n = (Node)sender;
            switch (n.ID)
            {
                case "0":
                    images = new string[] { "Maine.JPG" };
                    SelectMenu = true;
                    break;
                //****** Approximation *******
                case "11":
                    images = new string[] { "Approximation/1_1.JPG", "Approximation/1_2.JPG", "Approximation/1_3.JPG" };
                    SelectMenu = true;
                    break;
                case "12":
                    images = new string[] { "Approximation/2_1.JPG", "Approximation/2_2.JPG", "Approximation/2_3.JPG", "Approximation/2_4.JPG" };
                    SelectMenu = true;
                    break;
                case "13":
                    images = new string[] { "Approximation/3_1.JPG", "Approximation/3_2.JPG", "Approximation/3_3.JPG", "Approximation/3_4.JPG" };
                    SelectMenu = true;
                    break;
                case "14":
                    images = new string[] { "Approximation/4_1.JPG", "Approximation/4_2.JPG", "Approximation/4_3.JPG", "Approximation/4_4.JPG", "Approximation/4_5.JPG" };
                    SelectMenu = true;
                    break;

                //*****  DifferentialEquations  ******
                case "21":
                    images = new string[] { "DifferentialEquations/1_1.JPG", "DifferentialEquations/1_2.JPG", "DifferentialEquations/1_3.JPG" };
                    SelectMenu = true;
                    break;
                case "22":
                    images = new string[] { "DifferentialEquations/3_1.JPG", "DifferentialEquations/3_2.JPG", "DifferentialEquations/3_3.JPG" };
                    SelectMenu = true;
                    break;
                case "23":
                    images = new string[] { "DifferentialEquations/2_1.JPG", "DifferentialEquations/2_2.JPG", "DifferentialEquations/2_3.JPG" };
                    SelectMenu = true;
                    break;
                case "24":
                    images = new string[] { "DifferentialEquations/4_1.JPG", "DifferentialEquations/4_2.JPG", "DifferentialEquations/4_3.JPG" };
                    SelectMenu = true;
                    break;
                //****** Integration ******
                case "31":
                    images = new string[] { "Integration/1_1.JPG", "Integration/1_2.JPG", "Integration/1_3.JPG" };
                    SelectMenu = true;
                    break;
                case "32":
                    images = new string[] { "Integration/2_1.JPG", "Integration/2_2.JPG", "Integration/2_3.JPG", "Integration/2_4.JPG", "Integration/2_5.JPG" };
                    SelectMenu = true;
                    break;
                case "33":
                    images = new string[] { "Integration/3_1.JPG", "Integration/3_2.JPG", "Integration/3_3.JPG" };
                    SelectMenu = true;
                    break;
                //******* LinearSystems ********
                case "41":
                    images = new string[] { "LinearSystems/1_1.JPG", "LinearSystems/1_2.JPG", "LinearSystems/1_3.JPG", "LinearSystems/1_4.JPG", "LinearSystems/1_5.JPG", "LinearSystems/1_7.JPG", "LinearSystems/1_8.JPG" };
                    SelectMenu = true;
                    break;
                case "42":
                    images = new string[] { "LinearSystems/2_1.JPG", "LinearSystems/2_2.JPG", "LinearSystems/2_3.JPG", "LinearSystems/2_4.JPG", "LinearSystems/2_5.JPG" };
                    SelectMenu = true;
                    break;
                //******* NonLinear ********
                case "51":
                    images = new string[] { "NonLinear/1_1.JPG", "NonLinear/1_2.JPG", "NonLinear/1_3.JPG" };
                    SelectMenu = true;
                    break;
                case "52":
                    images = new string[] { "NonLinear/2_1.JPG", "NonLinear/2_2.JPG", "NonLinear/2_3.JPG", "NonLinear/2_4.JPG" };
                    SelectMenu = true;
                    break;
                case "53":
                    images = new string[] { "NonLinear/3_1.JPG", "NonLinear/3_2.JPG", "NonLinear/3_3.JPG", "NonLinear/3_4.JPG" };
                    SelectMenu = true;
                    break;
                case "54":
                    images = new string[] { "NonLinear/4_1.JPG", "NonLinear/4_2.JPG", "NonLinear/4_3.JPG", "NonLinear/4_4.JPG" };
                    SelectMenu = true;
                    break;

                //******* Interpolation ********
                case "61":
                    images = new string[] { "Interpolation/1_1.JPG", "Interpolation/1_2.JPG" };
                    SelectMenu = true;
                    break;
                case "62":
                    images = new string[] { "Interpolation/2_1.JPG", "Interpolation/2_2.JPG", "Interpolation/2_3.JPG" };
                    SelectMenu = true;
                    break;
                //******* MatrixAlgebra ********
                case "71":
                    images = new string[] { "MatrixAlgebra/1_1.JPG", "MatrixAlgebra/1_2.JPG", "MatrixAlgebra/1_3.JPG", "MatrixAlgebra/1_4.JPG", "MatrixAlgebra/1_5.JPG" };
                    SelectMenu = true;
                    break;
                case "72":
                    images = new string[] { "MatrixAlgebra/2_1.JPG", "MatrixAlgebra/2_2.JPG", "MatrixAlgebra/2_3.JPG", "MatrixAlgebra/2_4.JPG", "MatrixAlgebra/2_5.JPG", "MatrixAlgebra/2_6.JPG", "MatrixAlgebra/2_7.JPG" };
                    SelectMenu = true;
                    break;
                case "73":
                    images = new string[] { "MatrixAlgebra/3_1.JPG", "MatrixAlgebra/3_2.JPG", "MatrixAlgebra/3_3.JPG", "MatrixAlgebra/3_4.JPG", "MatrixAlgebra/3_5.JPG", "MatrixAlgebra/3_6.JPG", "MatrixAlgebra/3_7.JPG" };
                    SelectMenu = true;
                    break;
                //******* Optimizing ********
                case "81":
                    images = new string[] { "Optimizing/1_1.JPG", "Optimizing/1_2.JPG", "Optimizing/1_3.JPG", "Optimizing/1_4.JPG", "Optimizing/1_5.JPG", "Optimizing/1_6.JPG" };
                    SelectMenu = true;
                    break;
                case "82":
                    images = new string[] { "Optimizing/2_1.JPG", "Optimizing/2_2.JPG", "Optimizing/2_3.JPG", "Optimizing/2_4.JPG", "Optimizing/2_5.JPG" };
                    SelectMenu = true;
                    break;
                case "83":
                    images = new string[] { "Optimizing/3_1.JPG", "Optimizing/3_2.JPG", "Optimizing/3_4.JPG", "Optimizing/3_5.JPG"};
                    SelectMenu = true;
                    break;
                //******* Statistics ********
                case "91":
                    images = new string[] { "Statistics/1_1.JPG", "Statistics/1_2.JPG", "Statistics/1_3.JPG", "Statistics/1_4.JPG"};
                    SelectMenu = true;
                    break;
                case "92":
                    images = new string[] { "Statistics/2_1.JPG", "Statistics/2_2.JPG", "Statistics/2_3.JPG", "Statistics/2_4.JPG", "Statistics/2_5.JPG" };
                    SelectMenu = true;
                    break;
                default:
                    SelectMenu = false;
                    break;
            }
            if (SelectMenu)
            {
                MyPanel.Children.Clear();
                MyLargeButton.Content = "";
                int k = 0;
                foreach (string img in images)
                {
                    Button b = new Button();
                    b.Template = (ControlTemplate)App.Current.Resources["ImageButtonTemplate"];
                    b.Content = "Image/HelpImages/" + img;
                    b.GotFocus += new RoutedEventHandler(ButtonGotFocus);
                    b.MouseEnter += new MouseEventHandler(ButtonEnter);
                    b.MouseLeave += new MouseEventHandler(ButtonLeave);

                    if (k == 0)
                    {
                        /* if (bb == lastFocus) return;
                         if (lastFocus != null)
                             VisualStateManager.GoToState(lastFocus, "Cold", true);
                         lastFocus = bb;
                         VisualStateManager.GoToState(bb, "Selected", true);*/
                        Storyboard s = (Storyboard)Resources["DropImageStoryboard"];
                        s.Begin();
                        MyLargeButton.Content = b.Content;
                    }

                    MyPanel.Children.Add(b);

                    k++;
                }
            }
        }

        private void Tree_Drop(object sender, TreeEventArgs e)
        {
            e.DropAction = Tree.DropActions.InsertBefore;
        }
        private void NumericalMethodsTree_Populate(object sender, TreeEventArgs e)
        {
            ObservableCollection<Node> nodes = (sender is Tree ? ((Tree)sender).Nodes : ((Node)sender).Nodes);

            if (_rootBuilding)
            {
                nodes.Add(new Node("0", "Numerical Methods", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                _rootBuilding = false;
            }
            else
            {
                switch (e.ID)
                {
                    case "0":
                        nodes.Add(new Node("1", "Approximate", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                        nodes.Add(new Node("2", "Differential Equations", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                        nodes.Add(new Node("3", "Integration", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                        nodes.Add(new Node("4", "Linear Systems", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                        nodes.Add(new Node("5", "Non Linear equalization", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                        nodes.Add(new Node("6", "Interpolation", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                        nodes.Add(new Node("7", "Matrix Algebra", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                        nodes.Add(new Node("8", "Optimizing", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                        nodes.Add(new Node("9", "Statistics", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                       // nodes.Add(new Node("10", "Random Generator", true, "image/treeView/Closed.png", "image/treeView/catalog.png"));
                        break;
                    case "1":
                        nodes.Add(new Node("11", "Bisection Method", false, "image/treeView/method.png"));
                        nodes.Add(new Node("12", "Chord Method", false, "image/treeView/method.png"));
                        nodes.Add(new Node("13", "Iteration Method", false, "image/treeView/method.png"));
                        nodes.Add(new Node("14", "Newton Method", false, "image/treeView/method.png"));
                        break;
                    case "2":
                        nodes.Add(new Node("21", "Euler Simple", false, "image/treeView/method.png"));
                        nodes.Add(new Node("22", "Euler Modified", false, "image/treeView/method.png"));
                        nodes.Add(new Node("23", "Euler Corrected", false, "image/treeView/method.png"));
                        nodes.Add(new Node("24", "Runge-Kutta4", false, "image/treeView/method.png"));
                        break;
                    case "3":
                        nodes.Add(new Node("31", "Chebishev", false, "image/treeView/method.png"));
                        nodes.Add(new Node("32", "Simpson", false, "image/treeView/method.png"));
                        nodes.Add(new Node("33", "Trapezium", false, "image/treeView/method.png"));
                        break;
                    case "4":
                        nodes.Add(new Node("41", "Gaus", false, "image/treeView/method.png"));
                        nodes.Add(new Node("42", "Zeidel", false, "image/treeView/method.png"));
                        break;
                    case "5":
                        nodes.Add(new Node("51", "Half Division", false, "image/treeView/method.png"));
                        nodes.Add(new Node("52", "Hord Metod", false, "image/treeView/method.png"));
                        nodes.Add(new Node("53", "Newton Metod", false, "image/treeView/method.png"));
                        nodes.Add(new Node("54", "Secant Metod", false, "image/treeView/method.png"));
                        break;
                    case "6":
                        nodes.Add(new Node("61", "Lagrange Interpolator", false, "image/treeView/method.png"));
                        nodes.Add(new Node("62", "Newton Interpolator", false, "image/treeView/method.png"));
                        //nodes.Add(new Node("63", "Neville Interpolator", false, "image/treeView/method.png"));
                        //nodes.Add(new Node("64", "Spline Interpolator", false, "image/treeView/method.png"));
                        // nodes.Add(new Node("64", "Barycentric Interpolator", false, "image/treeView/method.png"));
                        break;
                    case "7":
                        nodes.Add(new Node("71", "Matrix Determinant", false, "image/treeView/method.png"));
                        nodes.Add(new Node("72", "Decomposition of matrix LU", false, "image/treeView/method.png"));
                        nodes.Add(new Node("73", "Matrix Inverse LU", false, "image/treeView/method.png"));
                        break;
                    case "8":
                        nodes.Add(new Node("81", "Brentopt", false, "image/treeView/method.png"));
                        nodes.Add(new Node("82", "GoldenSection", false, "image/treeView/method.png"));
                        nodes.Add(new Node("83", "Pijavsky", false, "image/treeView/method.png"));

                        break;
                    case "9":
                        nodes.Add(new Node("91", "Correlation Pearson", false, "image/treeView/method.png"));
                        nodes.Add(new Node("92", "Correlation Spearmans Rank", false, "image/treeView/method.png"));
                       // nodes.Add(new Node("93", "Descriptive Statistics A Dev", false, "image/treeView/method.png"));
                       // nodes.Add(new Node("94", "Descriptive Statistics Median", false, "image/treeView/method.png"));
                       // nodes.Add(new Node("95", "Descriptive Statistics Moments", false, "image/treeView/method.png"));
                       // nodes.Add(new Node("96", "Descriptive Statistics Percentile", false, "image/treeView/method.png"));
                        break;
                   /* case "10":
                        nodes.Add(new Node("101", "Generator 1", false, "image/treeView/method.png"));
                        nodes.Add(new Node("102", "Generator 2", false, "image/treeView/method.png"));
                        nodes.Add(new Node("103", "Generator 3", false, "image/treeView/method.png"));
                        nodes.Add(new Node("104", "Generator 4", false, "image/treeView/method.png"));
                        nodes.Add(new Node("105", "Generator 5", false, "image/treeView/method.png"));

                        break;*/
                }
            }
        }



    }
}

