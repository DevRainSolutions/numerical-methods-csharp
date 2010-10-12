using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using NumericalMethods;
using NumericalMethods.DifferentialEquations;
using NumericalMethods.Approximation;
using NumericalMethods.Integration;
using NumericalMethods.SystemLinearEqualizations;
using NumericalMethods.MatrixAlgebra;
using NumericalMethods.NonLinearEquations;
using NumericalMethods.Optimizing;
using NumericalMethods.Interpolation;
using NumericalMethods.Statistics;
using Help;
using System.Xml;
using Microsoft.Win32;
using MathParser;
using MathParser.TextModel;
using System.Windows.Markup;
namespace NumericalMethods.WPF.Help
{
    class NumericalMethods : Window
    {
        static string TestFunnction;
        TextBlock txtTextFunnction;
        string SelectedTreeName = "";
        TreeView treeMenu;
        MenuItem itemTree_Minimize;
        MenuItem itemTree_Normal;
        MenuItem itemTree_Extended;
        MenuItem itemTreeMenuBar;
        MenuItem itemResizeModeBar;
        MenuItem itemStyleBar;
        MenuItem itemStyleAllBar;
        ScrollViewer scrollMenu;
        Grid gridDown;

        TabItem itemPage1;
        Grid gridMaineTable;
        GroupBox boxParameters;
        TreeViewMenuAdd itemSelected;
        GroupBox boxResult;
        TextBox txtboxResult;
        GroupBox boxResultMathd;

        string[] initUniformGrid;
        string[] initUniformGrid2;

        double[] LinSysMasA;
        double[,] LinSysMatrixB;
        int range = 4;

        double[,] MatrixInit;
        int rangeMatrix = 4;


        TextBox sourceCode; //Відкритий код методу TextBox
        TextBox txtNumber; //interpolation
        TextBox txtPercentile;//interpolation
        TextBox txtBegin;// початкова точка для метода диф. рівняння
        TextBox txtEnd;// кінцева точка для метода диф. рівняння
        TextBox txtInit;// кількість точок для метода диф. рівняння
        TextBox txtPoints;// кількість точок для метода диф. рівняння

        int precision = 4;
        bool FileNew = false;// доступність кнопки File->New
        bool FileSave = false;// доступність кнопки File->Save
        bool FileSaveAs = false;// доступність кнопки File->SaveAs

        MenuItem itemNew;//File->New
        MenuItem itemSave;//File->Save
        MenuItem itemSaveAs;//File->SaveAs

        Button btnNew;//New
        Button btnOpen;//Open
        Button btnSave;//Save
        Button btnSaveAs;//SaveAs
        Button btnExecute;//Execute
        Button btnExit;//Exit
        Button btnTreeMenu_Minimize;
        Button btnTreeMenu_Normal;
        Button btnTreeMenu_Extended;

        Button btnResizeMode_Minimize;
        Button btnResizeMode_Resize;
        Button btnResizeMode_Resize_With_Grip;
        Button btnResizeMode_No_Resize;

        Button btnNoBorderOrCaption;
        Button btnSingle_border_Window;
        Button btn3D_border_Window;
        Button btn3D_Tool_Window;

        ToolBarTray trayTop;
        string ParamInput1;
        string ParamInput2;
        string ParamInput3;
        string ParamInput4;
        string XMLdata = Environment.CurrentDirectory.ToString() + "/Data/SaveData.xml";
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new NumericalMethods());
        }
        void InpuParamMethod(string NameTypeMethod, string NameInMethod)
        {
            XmlDocument XmldocInput = new XmlDocument();
            XmldocInput.Load(XMLdata);
            XmlNode node = XmldocInput.SelectSingleNode("/Numerical_Methods/" + NameTypeMethod + "[@id='" + NameInMethod + "']");
            if (node != null)
            {
                TestFunnction = node.Attributes["Function"].Value;
                ParamInput1 = node.Attributes["ParamInput1"].Value;
                ParamInput2 = node.Attributes["ParamInput2"].Value;
                ParamInput3 = node.Attributes["ParamInput3"].Value;
                ParamInput4 = node.Attributes["ParamInput4"].Value;
            }
        }
        void InpuLinearSystems(string NameTypeMethod, string NameInMethod)
        {
            string str1 = "";
            string str2 = "";
            XmlDocument XmldocInput = new XmlDocument();
            XmldocInput.Load(XMLdata);
            XmlNode node = XmldocInput.SelectSingleNode("/Numerical_Methods/" + NameTypeMethod + "[@id='" + NameInMethod + "']");
            if (node != null)
            {
                str1 = node.Attributes["LinSysMasA"].Value;
                str2 = node.Attributes["LinSysMatrixB"].Value;
                range = Convert.ToInt32(node.Attributes["Range"].Value);
                LinSysMasA = new double[range];
                for (int i = 0; i < range; i++)
                {
                    LinSysMasA[i] = Convert.ToDouble(str1.Split(' ')[i]);
                }
                int s = 0;
                for (int i = 0; i < range; i++)
                {
                    for (int j = 0; j < range; j++)
                    {
                        LinSysMatrixB[i, j] = Convert.ToDouble(str2.Split(' ')[s]);
                        s++;
                    }
                }
            }
        }
        void InpuMatrix(string NameTypeMethod, string NameInMethod)
        {
            string str = "";
            XmlDocument XmldocInput = new XmlDocument();
            XmldocInput.Load(XMLdata);
            XmlNode node = XmldocInput.SelectSingleNode("/Numerical_Methods/" + NameTypeMethod + "[@id='" + NameInMethod + "']");
            if (node != null)
            {
                str = node.Attributes["Matrix"].Value;
                rangeMatrix = Convert.ToInt32(node.Attributes["Range"].Value);
                MatrixInit = new double[rangeMatrix, rangeMatrix];
                int s = 0;
                for (int i = 0; i < rangeMatrix; i++)
                {
                    for (int j = 0; j < rangeMatrix; j++)
                    {
                        MatrixInit[i, j] = Convert.ToDouble(str.Split(' ')[s]);
                        s++;
                    }
                }
            }
        }
        public NumericalMethods()
        {

            LinSysMasA = new double[1000];
            LinSysMatrixB = new double[1000, 1000];

            Title = "Numerical Methods 9.3";
            ResizeMode = ResizeMode.CanMinimize;
            Background = SystemColors.ControlBrush;
            // this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
            //*********************************************************
            // Створення обєкта DockPanel для меню 
            //*********************************************************
            DockPanel dock = new DockPanel();
            // dock.LastChildFill = false;
            Content = dock;
            //*********************************************************
            //Створення меню , розташоване  у верхньому краї вікна 
            //*********************************************************
            Menu menu = new Menu();
            menu.Background = new SolidColorBrush(Colors.SlateGray);
            menu.IsTabStop = false;
            menu.Foreground = Brushes.White;
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);
            Menu(menu);
            //*********************************************************
            // Створення панелі інструментів, розташована  у верхньому краї вікна 
            //*********************************************************
            trayTop = new ToolBarTray();
            dock.Children.Add(trayTop);
            DockPanel.SetDock(trayTop, Dock.Top);



            ToolBar toolbar = new ToolBar();
            trayTop.ToolBars.Add(toolbar);
            MyToolBarFile(toolbar);
            //*********************************************************
            // Створення стрічки стану 
            //*********************************************************
            StatusBar status = new StatusBar();
            StatusBarItem statitem = new StatusBarItem();
            statitem.Content = "Status data";
            status.Items.Add(statitem);
            // Розміщення стрічки стану у нижньому краї панелі 
            DockPanel.SetDock(status, Dock.Bottom);
            dock.Children.Add(status);
            //*********************************************************
            // Створення обєкта Grid 
            //*********************************************************
            // dock.Background = Brushes.Gray;
            Grid grid = new Grid();
            // grid.Background = Brushes.Gray;
            grid.Margin = new Thickness(5);
            dock.Children.Add(grid);
            MyGridTable(grid);

            //Icon = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString() + "/images/icon.ico"));;
            //Icon = new BitmapImage(new Uri("F:\\icon.ico")); ;
            if (FileNew == false)
            {
                itemNew.IsEnabled = FileNew;
                itemNew.Foreground = Brushes.Gray;
                btnNew.IsEnabled = false;
            }
            if (FileSave == false)
            {
                itemSave.IsEnabled = false;
                itemSave.Foreground = Brushes.Gray;
                btnSave.IsEnabled = false;
            }
            if (FileSaveAs == false)
            {
                itemSaveAs.IsEnabled = false;
                itemSaveAs.Foreground = Brushes.Gray;
                btnSaveAs.IsEnabled = false;
            }
            btnExecute.IsEnabled = false;

        }
        //*************MyGridTable************************
        void MyGridTable(Grid grid)
        {
            ///  grid.Width =1750;
            // Определения строк и столбцов 
            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = new GridLength(1, GridUnitType.Star);
            grid.RowDefinitions.Add(rowdef);
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = new GridLength(250, GridUnitType.Pixel);
            grid.ColumnDefinitions.Add(coldef);
            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(5, GridUnitType.Pixel);
            grid.ColumnDefinitions.Add(coldef);
            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);

            // Oбєкт TreeView (0,0)
            Border bord = new Border();
            grid.Children.Add(bord);
            Grid.SetRow(bord, 0);
            Grid.SetColumn(bord, 0);
            bord.BorderBrush = Brushes.Gray;
            bord.Background = Brushes.White;
            bord.BorderThickness = new Thickness(2);
            CornerRadius c = new CornerRadius();
            c.BottomLeft = 20; c.BottomRight = 20;
            c.TopLeft = 20; c.TopRight = 20;
            bord.CornerRadius = c;

            Border bordIn = new Border();

            bordIn.BorderBrush = Brushes.YellowGreen;
            bord.Child = bordIn;
            bordIn.Background = Brushes.White;
            bordIn.BorderThickness = new Thickness(2);
            CornerRadius c2 = new CornerRadius();
            c2.BottomLeft = 20; c2.BottomRight = 20;
            c2.TopLeft = 20; c2.TopRight = 20;
            bordIn.CornerRadius = c2;

            scrollMenu = new ScrollViewer();
            scrollMenu.Margin = new Thickness(5);
            scrollMenu.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            bordIn.Child = scrollMenu;
            treeMenu = new TreeView();
            scrollMenu.Content = treeMenu;
            treeMenu.Background = Brushes.White;
            treeMenu.BorderBrush = Brushes.White;
            treeMenu.Margin = new Thickness(5);
            treeMenu.HorizontalAlignment = HorizontalAlignment.Stretch;
            treeMenu.VerticalAlignment = VerticalAlignment.Stretch;

            TreeViewMenu("Normal");


            // Oбєкт GridSplitter (0,1)
            GridSplitter split = new GridSplitter();
            split.Margin = new Thickness(0, 20, 0, 20);
            // split.Opacity = 0;
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 5;
            split.Background = Brushes.White;
            grid.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);

            // Створення головного вікна (0,2)

            TabControl tabControl = new TabControl();
            tabControl.BorderBrush = Brushes.Gray;
            tabControl.BorderThickness = new Thickness(3);
            // tabControl.FlowDirection = FlowDirection.RightToLeft;
            itemPage1 = new TabItem();

            itemPage1.Header = "Testing program";
            TabItem itemPage2 = new TabItem();
            itemPage2.Header = "Code realization of method";

            StackPanel stak = new StackPanel();

            ScrollViewer scroll = new ScrollViewer();
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            //scroll.VerticalAlignment = VerticalAlignment.Top;

            //sourceCode = new TextBlock();
            sourceCode = new TextBox();
            sourceCode.IsReadOnly = true;
            sourceCode.BorderBrush = Brushes.Gray;
            sourceCode.BorderThickness = new Thickness(0);
            stak.Children.Add(sourceCode);
            itemPage2.Content = scroll;
            scroll.Content = stak;


            tabControl.Items.Add(itemPage1);
            tabControl.Items.Add(itemPage2);
            grid.Children.Add(tabControl);
            Grid.SetRow(tabControl, 0);
            Grid.SetColumn(tabControl, 2);

            gridMaineTable = new Grid();
            itemPage1.Content = gridMaineTable;
            gridMaineTable.Margin = new Thickness(4);
            MyGridMaineTable(gridMaineTable);


        }
        void MyGridMaineTable(Grid gridMaineTable)
        {
            //gridMaineTable.ShowGridLines = true;
            // Определения строк и столбцов 
            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = new GridLength(150, GridUnitType.Pixel);
            gridMaineTable.RowDefinitions.Add(rowdef);
            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(1, GridUnitType.Star);
            gridMaineTable.RowDefinitions.Add(rowdef);
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = new GridLength(1, GridUnitType.Star);
            gridMaineTable.ColumnDefinitions.Add(coldef);

            //******** Oбєкт boxParameters (0,0)*******************************

            boxParameters = new GroupBox();
            boxParameters.BorderBrush = Brushes.Gray;
            boxParameters.BorderThickness = new Thickness(2);
            boxParameters.Header = "Parameters";
            DockPanel inpuPanel = new DockPanel();
            boxParameters.Content = inpuPanel;




            gridMaineTable.Children.Add(boxParameters);
            Grid.SetRow(boxParameters, 0);
            Grid.SetColumn(boxParameters, 0);
            //*****************************************************************

            //****************** Oбєкт gridDown (1,0)**************************
            gridDown = new Grid();
            gridMaineTable.Children.Add(gridDown);
            Grid.SetRow(gridDown, 1);
            Grid.SetColumn(gridDown, 0);

            RowDefinition rowdefDown = new RowDefinition();
            rowdefDown.Height = new GridLength(1, GridUnitType.Star);
            gridDown.RowDefinitions.Add(rowdefDown);
            ColumnDefinition coldefDown = new ColumnDefinition();
            coldefDown.Width = new GridLength(50, GridUnitType.Star);
            gridDown.ColumnDefinitions.Add(coldefDown);
            coldefDown = new ColumnDefinition();
            coldefDown.Width = new GridLength(50, GridUnitType.Star);
            gridDown.ColumnDefinitions.Add(coldefDown);

            boxResult = new GroupBox();
            boxResult.BorderBrush = Brushes.Gray;
            boxResult.BorderThickness = new Thickness(2);
            boxResult.Margin = new Thickness(0);
            boxResult.Header = "Result of Program";
            gridDown.Children.Add(boxResult);
            Grid.SetRow(boxResult, 0);
            Grid.SetColumn(boxResult, 0);

            txtboxResult = new TextBox();
            txtboxResult.BorderThickness = new Thickness(0);
            txtboxResult.Text = "";


            // Первый объект TextBox 
            //  RichTextBox txtboxResult = new RichTextBox();
            /* TextBox txtboxResult = new TextBox();
             txtboxResult.Text = new DateTime(1980, 1, 1).ToShortDateString();
             // txtboxBegin.TextChanged += TextBoxOnTextChanged;
             boxResult.Content = txtboxResult;*/

            // Oобєкт LabelTreeViewName (1,0)
            boxResultMathd = new GroupBox();
            boxResultMathd.BorderBrush = Brushes.Gray;
            boxResultMathd.BorderThickness = new Thickness(2);
            boxResultMathd.Header = "Result of Mathcad";
            //  box.Width = GridLength.Auto;
            //  box.Height = GridLength.Auto;
            gridDown.Children.Add(boxResultMathd);
            Grid.SetRow(boxResultMathd, 0);
            Grid.SetColumn(boxResultMathd, 1);


            /* Canvas canvas = new Canvas();
             canvas.Background = Brushes.Black;
             boxResultMathd.Content = canvas;
             Rectangle rect = new Rectangle();
             rect.Fill = Brushes.Red;
             rect.Width = 100;
             rect.Height = 100;
             canvas.Children.Add(rect);
             Canvas.SetBottom(rect, 0);
             Canvas.SetLeft(rect, 0);*/

        }
        //*************/MyGridTable************************

        //*************Menu************************
        void Menu(Menu menu)
        {

            // Створення меню File 
            MenuItem itemFile = new MenuItem();
            itemFile.Header = "_File";
            menu.Items.Add(itemFile);
            itemNew = new MenuItem();
            itemNew.Foreground = Brushes.Black;
            itemNew.Header = "_New";
            itemNew.Click += OnFileNewClick;
            itemFile.Items.Add(itemNew);
            MenuItem itemOpen = new MenuItem();
            itemOpen.Foreground = Brushes.Black;
            itemOpen.Header = "_Open";
            itemOpen.Click += OnFileOpenClick;
            itemFile.Items.Add(itemOpen);
            itemSave = new MenuItem();
            itemSave.Foreground = Brushes.Black;
            itemSave.Header = "_Save";
            itemSave.Click += OnFileSaveClick;
            itemFile.Items.Add(itemSave);
            itemSaveAs = new MenuItem();
            itemSaveAs.Foreground = Brushes.Black;
            itemSaveAs.Header = "_Save As";
            itemSaveAs.Click += OnFileSaveAsClick;
            itemFile.Items.Add(itemSaveAs);
            itemFile.Items.Add(new Separator());
            MenuItem itemExit = new MenuItem();
            itemExit.Header = "_Exit";
            itemExit.Foreground = Brushes.Black;
            itemExit.Click += OnExitClick;
            // itemExit.Command=CommandManager.
            itemFile.Items.Add(itemExit);

            // Створення меню View 
            MenuItem itemView = new MenuItem();
            itemView.Header = "_View";
            menu.Items.Add(itemView);
            MenuItem itemTree_menu = new MenuItem();
            itemTree_menu.Foreground = Brushes.Black;
            itemTree_menu.Header = "Tree menu";
            itemView.Items.Add(itemTree_menu);
            itemTree_Minimize = new MenuItem();
            itemTree_Minimize.Foreground = Brushes.Black;
            itemTree_Minimize.Header = "Minimize";
            itemTree_Minimize.IsChecked = false;
            itemTree_Minimize.Click += OnTreeMenu_Minimize;
            itemTree_menu.Items.Add(itemTree_Minimize);
            itemTree_Normal = new MenuItem();
            itemTree_Normal.Foreground = Brushes.Black;
            itemTree_Normal.Header = "Normal";
            itemTree_Normal.IsChecked = true;
            itemTree_Normal.Click += OnTreeMenu_Normal;
            itemTree_menu.Items.Add(itemTree_Normal);
            itemTree_Extended = new MenuItem();
            itemTree_Extended.Foreground = Brushes.Black;
            itemTree_Extended.Header = "Extended";
            itemTree_Extended.IsChecked = false;
            itemTree_Extended.Click += OnTreeMenu_Extended;
            itemTree_menu.Items.Add(itemTree_Extended);

            MenuItem itemToolBar = new MenuItem();
            itemToolBar.Foreground = Brushes.Black;
            itemToolBar.Header = "ToolBar";
            itemView.Items.Add(itemToolBar);
            MenuItem itemFileBar = new MenuItem();
            itemFileBar.Foreground = Brushes.Black;
            itemFileBar.Header = "File Bar";
            itemFileBar.IsChecked = true;
            itemFileBar.IsEnabled = false;
            itemToolBar.Items.Add(itemFileBar);
            itemTreeMenuBar = new MenuItem();
            itemTreeMenuBar.Foreground = Brushes.Black;
            itemTreeMenuBar.Header = "Tree menu Bar";
            itemTreeMenuBar.IsChecked = false;
            itemTreeMenuBar.Click += OnMenuTollBar;
            itemToolBar.Items.Add(itemTreeMenuBar);
            itemResizeModeBar = new MenuItem();
            itemResizeModeBar.Foreground = Brushes.Black;
            itemResizeModeBar.Header = "Resize Mode Bar";
            itemResizeModeBar.IsChecked = false;
            itemResizeModeBar.Click += OnMenuTollBar;
            itemToolBar.Items.Add(itemResizeModeBar);
            itemStyleBar = new MenuItem();
            itemStyleBar.Header = "Style Bar";
            itemStyleBar.IsChecked = false;
            itemStyleBar.Click += OnMenuTollBar;
            itemStyleBar.Foreground = Brushes.Black;
            itemToolBar.Items.Add(itemStyleBar);
            itemStyleAllBar = new MenuItem();
            itemStyleAllBar.Header = "All Bar";
            itemStyleAllBar.IsChecked = false;
            itemStyleAllBar.Click += OnMenuTollBar;
            itemStyleAllBar.Foreground = Brushes.Black;
            itemToolBar.Items.Add(itemStyleAllBar);


            // Створення меню Window 
            MenuItem itemWindow = new MenuItem();
            itemWindow.Header = "_Window";
            menu.Items.Add(itemWindow);
            // ResizeMode
            MenuItem itemResizeMode = new MenuItem();
            itemResizeMode.Header = "Resize Mode";
            itemResizeMode.Foreground = Brushes.Black;
            itemWindow.Items.Add(itemResizeMode);
            MenuItem itemMinimize = new MenuItem();
            itemMinimize.Header = "Minimize";
            itemMinimize.Foreground = Brushes.Black;
            itemMinimize.Click += OnWindowCanMaximizeClick;
            itemResizeMode.Items.Add(itemMinimize);
            MenuItem itemCanResize = new MenuItem();
            itemCanResize.Header = "Resize";
            itemCanResize.Foreground = Brushes.Black;
            itemCanResize.Click += OnWindowCanResizeClick;
            itemResizeMode.Items.Add(itemCanResize);
            MenuItem itemCanResizeWithGrip = new MenuItem();
            itemCanResizeWithGrip.Foreground = Brushes.Black;
            itemCanResizeWithGrip.Header = "Resize With Grip";
            itemCanResizeWithGrip.Click += OnWindowCanResizeWithGripClick;
            itemResizeMode.Items.Add(itemCanResizeWithGrip);
            MenuItem itemNoResize = new MenuItem();
            itemNoResize.Header = "No Resize";
            itemNoResize.Foreground = Brushes.Black;
            itemNoResize.Click += OnWindowNoResizeClick;
            itemResizeMode.Items.Add(itemNoResize);
            //Style
            MenuItem itemStyle = new MenuItem();
            itemStyle.Header = "Style";
            itemStyle.Foreground = Brushes.Black;
            itemWindow.Items.Add(itemStyle);
            MenuItem itemNoborder = new MenuItem();
            itemNoborder.Header = "No border or caption";
            itemNoborder.Foreground = Brushes.Black;
            itemNoborder.Click += OnWindowNoborderClick;
            itemStyle.Items.Add(itemNoborder);
            MenuItem itemSingleBorder = new MenuItem();
            itemSingleBorder.Header = "Single-border window";
            itemSingleBorder.Foreground = Brushes.Black;
            itemSingleBorder.Click += OnWindowSingleBorderClick;
            itemStyle.Items.Add(itemSingleBorder);
            MenuItem item3DBorder = new MenuItem();
            item3DBorder.Header = "3D-border window";
            item3DBorder.Foreground = Brushes.Black;
            item3DBorder.Click += OnWindow3DBorderClick;
            itemStyle.Items.Add(item3DBorder);
            MenuItem itemTool = new MenuItem();
            itemTool.Header = "Tool window";
            itemTool.Foreground = Brushes.Black;
            itemTool.Click += OnWindowToolClick;
            itemStyle.Items.Add(itemTool);
            // State
            MenuItem itemState = new MenuItem();
            itemState.Header = "State";
            itemState.Foreground = Brushes.Black;
            itemWindow.Items.Add(itemState);
            MenuItem itemMaximized = new MenuItem();
            itemMaximized.Header = "Maximized";
            itemMaximized.Foreground = Brushes.Black;
            itemMaximized.Click += OnWindowMaximizedClick;
            itemState.Items.Add(itemMaximized);
            MenuItem itemMinimized = new MenuItem();
            itemMinimized.Header = "Minimized";
            itemMinimized.Foreground = Brushes.Black;
            itemMinimized.Click += OnWindowMinimizedClick;
            itemState.Items.Add(itemMinimized);
            MenuItem itemNormal = new MenuItem();
            itemNormal.Header = "Normal";
            itemNormal.Foreground = Brushes.Black;
            itemNormal.Click += OnWindowNormalClick;
            itemState.Items.Add(itemNormal);
            MenuItem itemTaskbar = new MenuItem();
            itemTaskbar.Header = "_Show in Taskbar";
            itemTaskbar.Foreground = Brushes.Black;
            itemTaskbar.IsCheckable = true;
            itemTaskbar.IsChecked = ShowInTaskbar;
            itemTaskbar.Click += TaskbarOnClick;
            itemWindow.Items.Add(itemTaskbar);
            MenuItem itemTopmost = new MenuItem();
            itemTopmost.Header = "_Topmost";
            itemTopmost.Foreground = Brushes.Black;
            itemTopmost.IsCheckable = true;
            itemTopmost.IsChecked = Topmost;
            itemTopmost.Checked += TopmostOnCheck;
            itemTopmost.Unchecked += TopmostOnCheck;
            itemWindow.Items.Add(itemTopmost);

            // Створення меню Option 
            MenuItem itemOption = new MenuItem();
            itemOption.Header = "_Options";
            menu.Items.Add(itemOption);
            MenuItem itemPrecision = new MenuItem();
            itemPrecision.Header = "Precision";
            itemPrecision.Foreground = Brushes.Black;
            itemOption.Items.Add(itemPrecision);
            MenuItem itemCalculation = new MenuItem();
            itemCalculation.Header = "Calculation";
            itemCalculation.Foreground = Brushes.Black;
            itemCalculation.Click += OnOptionPrecisionClick;
            itemPrecision.Items.Add(itemCalculation);
            /* MenuItem itemInputFunction = new MenuItem();
             itemInputFunction.Header = "Input";
             itemInputFunction.Foreground = Brushes.Black;
             itemOption.Items.Add(itemInputFunction);
             MenuItem itemNewFunction = new MenuItem();
             itemNewFunction.Header = "New Function";
             itemNewFunction.Foreground = Brushes.Black;
             itemNewFunction.Click += OnNewFunctionClick;
             itemInputFunction.Items.Add(itemNewFunction);*/


            // Створення меню Help 
            MenuItem itemHelp = new MenuItem();
            itemHelp.Header = "_Help";
            menu.Items.Add(itemHelp);
            MenuItem itemMethods = new MenuItem();
            itemMethods.Header = "Methods";
            itemMethods.Foreground = Brushes.Black;
            itemMethods.Click += OnMethodsClick;
            itemHelp.Items.Add(itemMethods);
            MenuItem itemAbout = new MenuItem();
            itemAbout.Header = "About";
            itemAbout.Foreground = Brushes.Black;
            itemAbout.Click += OnAboutClick;
            itemHelp.Items.Add(itemAbout);
        }
        void OnMenuTollBar(object sender, RoutedEventArgs args)
        {
            ToolBar toolbar;
            MenuItem item = sender as MenuItem;
            //itemStyleAllBar.IsChecked = false;

            // itemTreeMenuBar.IsChecked = true;
            //  itemResizeModeBar.IsChecked = true;
            // itemStyleBar.IsChecked = true;
            switch (item.Header.ToString())
            {
                case "Tree menu Bar":

                    if (itemTreeMenuBar.IsChecked == false)
                    {
                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarTree(toolbar);
                        itemTreeMenuBar.IsChecked = true;
                    }
                    else
                    {
                        itemTreeMenuBar.IsChecked = false;
                        trayTop.ToolBars.Clear();
                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarFile(toolbar);

                        if (itemResizeModeBar.IsChecked == true)
                        {
                            toolbar = new ToolBar();
                            trayTop.ToolBars.Add(toolbar);
                            MyToolBarTree(toolbar);
                            itemResizeModeBar.IsChecked = true;
                            itemStyleAllBar.IsChecked = false;
                        }
                        if (itemStyleBar.IsChecked == true)
                        {
                            toolbar = new ToolBar();
                            trayTop.ToolBars.Add(toolbar);
                            MyToolBarStyle(toolbar);
                            itemStyleBar.IsChecked = true;
                            itemStyleAllBar.IsChecked = false;
                        }
                    }
                    break;
                case "Resize Mode Bar":

                    if (itemResizeModeBar.IsChecked == false)
                    {
                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarResizeMode(toolbar);
                        itemResizeModeBar.IsChecked = true;
                    }
                    else
                    {
                        itemResizeModeBar.IsChecked = false;
                        trayTop.ToolBars.Clear();
                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarFile(toolbar);

                        if (itemTreeMenuBar.IsChecked == true)
                        {
                            toolbar = new ToolBar();
                            trayTop.ToolBars.Add(toolbar);
                            MyToolBarResizeMode(toolbar);
                            itemTreeMenuBar.IsChecked = true;
                            itemStyleAllBar.IsChecked = false;
                        }
                        if (itemStyleBar.IsChecked == true)
                        {
                            toolbar = new ToolBar();
                            trayTop.ToolBars.Add(toolbar);
                            MyToolBarStyle(toolbar);
                            itemStyleBar.IsChecked = true;
                            itemStyleAllBar.IsChecked = false;
                        }
                    }
                    break;
                case "Style Bar":

                    if (itemStyleBar.IsChecked == false)
                    {
                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarStyle(toolbar);
                        itemStyleBar.IsChecked = true;
                    }
                    else
                    {
                        itemStyleBar.IsChecked = false;
                        trayTop.ToolBars.Clear();
                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarFile(toolbar);

                        if (itemTreeMenuBar.IsChecked == true)
                        {
                            toolbar = new ToolBar();
                            trayTop.ToolBars.Add(toolbar);
                            MyToolBarResizeMode(toolbar);
                            itemTreeMenuBar.IsChecked = true;
                            itemStyleAllBar.IsChecked = false;
                        }
                        if (itemResizeModeBar.IsChecked == true)
                        {
                            toolbar = new ToolBar();
                            trayTop.ToolBars.Add(toolbar);
                            MyToolBarTree(toolbar);
                            itemResizeModeBar.IsChecked = true;
                            itemStyleAllBar.IsChecked = false;
                        }

                    }
                    break;
                case "All Bar":
                    if (itemStyleAllBar.IsChecked == true)
                    {
                        trayTop.ToolBars.Clear();
                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarFile(toolbar);
                        itemStyleAllBar.IsChecked = false;
                        itemResizeModeBar.IsChecked = false;
                        itemStyleBar.IsChecked = false;
                        itemTreeMenuBar.IsChecked = false;
                    }
                    else
                    {
                        itemStyleAllBar.IsChecked = true;


                        trayTop.ToolBars.Clear();
                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarFile(toolbar);



                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarStyle(toolbar);
                        itemStyleBar.IsChecked = true;

                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarResizeMode(toolbar);
                        itemResizeModeBar.IsChecked = true;


                        toolbar = new ToolBar();
                        trayTop.ToolBars.Add(toolbar);
                        MyToolBarTree(toolbar);
                        itemTreeMenuBar.IsChecked = true;


                    }
                    break;

            }
            if (FileNew == false)
            {
                itemNew.IsEnabled = FileNew;
                itemNew.Foreground = Brushes.Gray;
                btnNew.IsEnabled = false;
            }
            if (FileSave == false)
            {
                itemSave.IsEnabled = false;
                itemSave.Foreground = Brushes.Gray;
                btnSave.IsEnabled = false;
            }
            if (FileSaveAs == false)
            {
                itemSaveAs.IsEnabled = false;
                itemSaveAs.Foreground = Brushes.Gray;
                btnSaveAs.IsEnabled = false;
            }
            btnExecute.IsEnabled = false;



        }
        void OnFileNewClick(object sender, RoutedEventArgs args)
        {
            switch (itemSelected.Text)
            {
                //*** Approximate decision of equalization f(x)=0 ***
                case "Bisection Method":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    break;
                case "Chord Method":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    txtPoints.Text = "";
                    break;
                case "Iteration Method":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    txtPoints.Text = "";
                    break;
                case "Newton Method":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    txtPoints.Text = "";
                    break;
                //--------------------------
                //*** Differential Equations ***
                case "Euler Simple":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    txtPoints.Text = "";
                    break;
                case "Euler Modified":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    txtPoints.Text = "";
                    break;
                case "Euler Corrected":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    txtPoints.Text = "";
                    break;
                case "Runge-Kutta4":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    txtPoints.Text = "";
                    break;
                //*** Integration ***
                case "Chebishev":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    break;
                case "Simpson":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    break;
                case "Simpson 2":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    break;
                case "Trapezium":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    break;
                //*** Non Linear equalization ***
                case "Half Division":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    break;
                case "Hord Metod":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    break;
                case "Newton Metod":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    break;
                case "Secant Metod":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    break;
                //*** Optimizing***
                case "Brentopt":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    break;
                case "Golden Section":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    break;
                case "Pijavsky":
                    txtBegin.Text = "";
                    txtEnd.Text = "";
                    txtInit.Text = "";
                    txtPoints.Text = "";
                    break;
                //*** Interpolation ***
                case "Lagrange Interpolator":
                    txtNumber.Text = "";
                    break;
                case "Newton Interpolator":
                    txtNumber.Text = "";
                    break;
                case "Neville Interpolator":
                    txtNumber.Text = "";
                    break;
                case "Spline Interpolator":
                    txtNumber.Text = "";
                    break;
                case "Barycentric Interpolator":
                    txtNumber.Text = "";
                    break;
                //*** Statistics ***
                case "Correlation Pearson":

                    break;
                case "Correlation Spearmans Rank":

                    break;
                case "Descriptive Statistics A Dev":

                    break;
                case "Descriptive Statistics Median":

                    break;
                case "Descriptive Statistics Moments":

                    break;
                case "Descriptive Statistics Percentile":
                    txtNumber.Text = "";
                    break;
                case "Method 1":

                    break;
                case "Method 2":
                    txtNumber.Text = "";
                    break;
                case "Method 3":

                    break;
                case "Method 4":

                    break;
                case "Method 5":
                    txtNumber.Text = "";
                    break;
                //*** Matrix Algebra ***
                case "Matrix Determinant":

                    break;
                case "Decomposition of matrix LU":

                    break;
                case "Matrix Inverse LU":

                    break;
                //*** Linear Systems ***
                case "Gaus":

                    break;
                case "Zeidel":

                    break;
            }
        }
        void OnFileOpenClick(object sender, RoutedEventArgs args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".xml";
            openFileDialog.Filter = "XML Files (.xml)|*.xml";
            openFileDialog.Title = "Open a XML file";
            openFileDialog.RestoreDirectory = true;
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                XMLdata = openFileDialog.FileName;
            }
        }
        void OnFileSaveClick(object sender, RoutedEventArgs args)
        {
            switch (SelectedTreeName)
            {
                case "Bisection Method":
                    SaveInpuParamMethod(false, "Approximate_Decision", "BisectionMethod", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Chord Method":
                    SaveInpuParamMethod(false, "Approximate_Decision", "ChordMethod", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Iteration Method":
                    SaveInpuParamMethod(false, "Approximate_Decision", "IterationMethod", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Newton Method":
                    SaveInpuParamMethod(false, "Approximate_Decision", "NewtonMethod", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Euler Simple":
                    SaveInpuParamMethod(false, "Differential_Equations", "EulerSimple", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Euler Modified":
                    SaveInpuParamMethod(false, "Differential_Equations", "EulerModified", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Euler Corrected":
                    SaveInpuParamMethod(false, "Differential_Equations", "EulerCorrected", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Runge-Kutta4":
                    SaveInpuParamMethod(false, "Differential_Equations", "RungeKutta4", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Chebishev":
                    SaveInpuParamMethod(false, "Integration", "Chebishev", txtBegin.Text, TestFunnction, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Simpson":
                    SaveInpuParamMethod(false, "Integration", "Simpson", txtBegin.Text, TestFunnction, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Simpson 2":
                    SaveInpuParamMethod(false, "Integration", "Simpson2", txtBegin.Text, TestFunnction, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Trapezium":
                    SaveInpuParamMethod(false, "Integration", "Trapezium", txtBegin.Text, TestFunnction, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Half Division":
                    SaveInpuParamMethod(false, "NonLinearEqualization", "HalfDivision", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Hord Metod":
                    SaveInpuParamMethod(false, "NonLinearEqualization", "HordMetod", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Newton Metod":
                    SaveInpuParamMethod(false, "NonLinearEqualization", "NewtonMetod", TestFunnction, txtBegin.Text, txtEnd.Text, "", "");
                    break;
                case "Secant Metod":
                    SaveInpuParamMethod(false, "NonLinearEqualization", "SecantMetod", TestFunnction, txtBegin.Text, txtEnd.Text, "", "");
                    break;
                case "Brentopt":
                    SaveInpuParamMethod(false, "Optimizing", "Brentopt", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Golden Section":
                    SaveInpuParamMethod(false, "Optimizing", "GoldenSection", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Pijavsky":
                    SaveInpuParamMethod(false, "Optimizing", "Pijavsky", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Lagrange Interpolator":
                    SaveInpuParamMethod(false, "Interpolation", "LagrangeInterpolator", TestFunnction, txtNumber.Text, "", "", "");
                    break;
                case "Newton Interpolator":
                    SaveInpuParamMethod(false, "Interpolation", "NewtonInterpolator", TestFunnction, txtNumber.Text, "", "", "");
                    break;
                case "Neville Interpolator":
                    SaveInpuParamMethod(false, "Interpolation", "NevilleInterpolator", TestFunnction, txtNumber.Text, "", "", "");
                    break;
                case "Spline Interpolator":
                    SaveInpuParamMethod(false, "Interpolation", "SplineInterpolator", TestFunnction, txtNumber.Text, "", "", "");
                    break;
                case "Barycentric Interpolator":
                    SaveInpuParamMethod(false, "Interpolation", "BarycentricInterpolator", TestFunnction, txtNumber.Text, "", "", "");
                    break;
                case "Correlation Pearson":
                    SaveInpuParamMethod(false, "Statistics", "CorrelationPearson", TestFunnction, "", "", "", "");
                    break;
                case "Correlation Spearmans Rank":
                    SaveInpuParamMethod(false, "Statistics", "CorrelationSpearmansRank", TestFunnction, "", "", "", "");
                    break;
                case "Descriptive Statistics ADev":
                    SaveInpuParamMethod(false, "Statistics", "DescriptiveStatisticsADev", TestFunnction, "", "", "", "");
                    break;
                case "Descriptive Statistics Median":
                    SaveInpuParamMethod(false, "Statistics", "DescriptiveStatisticsMedian", TestFunnction, "", "", "", "");
                    break;
                case "Descriptive Statistics Moments":
                    SaveInpuParamMethod(false, "Statistics", "DescriptiveStatisticsMoments", TestFunnction, "", "", "", "");
                    break;
                case "Descriptive Statistics Percentile":
                    SaveInpuParamMethod(false, "Statistics", "DescriptiveStatisticsPercentile", TestFunnction, "", "", "", "");
                    break;
                case "Gaus":
                    string str1 = "";
                    string str2 = "";
                    for (int i = 0; i < range; i++)
                    {
                        str1 = str1 + LinSysMasA[i].ToString() + " ";
                    }
                    for (int i = 0; i < range; i++)
                    {
                        for (int j = 0; j < range; j++)
                        {
                            str2 = str2 + LinSysMatrixB[i, j].ToString() + " ";
                        }
                    }
                    SaveInpuParamLinearSystems(false, "LinearSystems", "Gaus", range.ToString(), str1, str2);
                    break;
                case "Zeidel":
                    str1 = "";
                    str2 = "";
                    for (int i = 0; i < range; i++)
                    {
                        str1 = str1 + LinSysMasA[i].ToString() + " ";
                    }
                    for (int i = 0; i < range; i++)
                    {
                        for (int j = 0; j < range; j++)
                        {
                            str2 = str2 + LinSysMatrixB[i, j].ToString() + " ";
                        }
                    }
                    SaveInpuParamLinearSystems(false, "LinearSystems", "Zeidel", range.ToString(), str1, str2);
                    break;
                case "Matrix Determinant":
                    string str = "";
                    for (int i = 0; i < rangeMatrix; i++)
                    {
                        for (int j = 0; j < rangeMatrix; j++)
                        {
                            str = str + MatrixInit[i, j].ToString() + " ";
                        }
                    }
                    SaveInpuParamMatrix(false, "MatrixAlgebra", "MatrixDeterminant", rangeMatrix.ToString(), str);
                    break;
                case "Decomposition of matrix LU":
                    str = "";
                    for (int i = 0; i < rangeMatrix; i++)
                    {
                        for (int j = 0; j < rangeMatrix; j++)
                        {
                            str = str + MatrixInit[i, j].ToString() + " ";
                        }
                    }
                    SaveInpuParamMatrix(false, "MatrixAlgebra", "DecompositionOfMatrixLU", rangeMatrix.ToString(), str);
                    break;
                case "Matrix Inverse LU":
                    str = "";
                    for (int i = 0; i < rangeMatrix; i++)
                    {
                        for (int j = 0; j < rangeMatrix; j++)
                        {
                            str = str + MatrixInit[i, j].ToString() + " ";
                        }
                    }
                    SaveInpuParamMatrix(false, "MatrixAlgebra", "MatrixInverseLU", rangeMatrix.ToString(), str);
                    break;
            }
        }
        void OnFileSaveAsClick(object sender, RoutedEventArgs args)
        {
            switch (SelectedTreeName)
            {
                case "Bisection Method":
                    SaveInpuParamMethod(true, "Approximate_Decision", "BisectionMethod", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Chord Method":
                    SaveInpuParamMethod(true, "Approximate_Decision", "ChordMethod", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Iteration Method":
                    SaveInpuParamMethod(true, "Approximate_Decision", "IterationMethod", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Newton Method":
                    SaveInpuParamMethod(true, "Approximate_Decision", "NewtonMethod", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Euler Simple":
                    SaveInpuParamMethod(true, "Differential_Equations", "EulerSimple", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Euler Modified":
                    SaveInpuParamMethod(true, "Differential_Equations", "EulerModified", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Euler Corrected":
                    SaveInpuParamMethod(true, "Differential_Equations", "EulerCorrected", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Runge-Kutta4":
                    SaveInpuParamMethod(true, "Differential_Equations", "RungeKutta4", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Chebishev":
                    SaveInpuParamMethod(true, "Integration", "Chebishev", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Simpson":
                    SaveInpuParamMethod(true, "Integration", "Simpson", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Simpson 2":
                    SaveInpuParamMethod(true, "Integration", "Simpson2", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Trapezium":
                    SaveInpuParamMethod(true, "Integration", "Trapezium", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Half Division":
                    SaveInpuParamMethod(true, "NonLinearEqualization", "HalfDivision", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Hord Metod":
                    SaveInpuParamMethod(true, "NonLinearEqualization", "HordMetod", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Newton Metod":
                    SaveInpuParamMethod(true, "NonLinearEqualization", "NewtonMetod", TestFunnction, txtBegin.Text, txtEnd.Text, "", "");
                    break;
                case "Secant Metod":
                    SaveInpuParamMethod(true, "NonLinearEqualization", "SecantMetod", TestFunnction, txtBegin.Text, txtEnd.Text, "", "");
                    break;
                case "Brentopt":
                    SaveInpuParamMethod(true, "Optimizing", "Brentopt", txtBegin.Text, TestFunnction, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Golden Section":
                    SaveInpuParamMethod(true, "Optimizing", "GoldenSection", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, "");
                    break;
                case "Pijavsky":
                    SaveInpuParamMethod(true, "Optimizing", "Pijavsky", TestFunnction, txtBegin.Text, txtEnd.Text, txtInit.Text, txtPoints.Text);
                    break;
                case "Lagrange Interpolator":
                    SaveInpuParamMethod(true, "Interpolation", "LagrangeInterpolator", TestFunnction, txtNumber.Text, "", "", "");
                    break;
                case "Newton Interpolator":
                    SaveInpuParamMethod(true, "Interpolation", "NewtonInterpolator", TestFunnction, txtNumber.Text, "", "", "");
                    break;
                case "Neville Interpolator":
                    SaveInpuParamMethod(true, "Interpolation", "NevilleInterpolator", TestFunnction, txtNumber.Text, "", "", "");
                    break;
                case "Spline Interpolator":
                    SaveInpuParamMethod(true, "Interpolation", "SplineInterpolator", TestFunnction, txtNumber.Text, "", "", "");
                    break;
                case "Barycentric Interpolator":
                    SaveInpuParamMethod(true, "Interpolation", "BarycentricInterpolator", TestFunnction, txtNumber.Text, "", "", "");
                    break;
                case "Correlation Pearson":
                    SaveInpuParamMethod(true, "Statistics", "CorrelationPearson", TestFunnction, "", "", "", "");
                    break;
                case "Correlation Spearmans Rank":
                    SaveInpuParamMethod(true, "Statistics", "CorrelationSpearmansRank", TestFunnction, "", "", "", "");
                    break;
                case "Descriptive Statistics ADev":
                    SaveInpuParamMethod(true, "Statistics", "DescriptiveStatisticsADev", TestFunnction, "", "", "", "");
                    break;
                case "Descriptive Statistics Median":
                    SaveInpuParamMethod(true, "Statistics", "DescriptiveStatisticsMedian", TestFunnction, "", "", "", "");
                    break;
                case "Descriptive Statistics Moments":
                    SaveInpuParamMethod(true, "Statistics", "DescriptiveStatisticsMoments", TestFunnction, "", "", "", "");
                    break;
                case "Descriptive Statistics Percentile":
                    SaveInpuParamMethod(true, "Statistics", "DescriptiveStatisticsPercentile", TestFunnction, "", "", "", "");
                    break;
                case "Gaus":
                    string str1 = "";
                    string str2 = "";
                    for (int i = 0; i < range; i++)
                    {
                        str1 = str1 + LinSysMasA[i].ToString() + " ";
                    }
                    for (int i = 0; i < range; i++)
                    {
                        for (int j = 0; j < range; j++)
                        {
                            str2 = str2 + LinSysMatrixB[i, j].ToString() + " ";
                        }
                    }
                    SaveInpuParamLinearSystems(true, "LinearSystems", "Gaus", range.ToString(), str1, str2);
                    break;
                case "Zeidel":
                    str1 = "";
                    str2 = "";
                    for (int i = 0; i < range; i++)
                    {
                        str1 = str1 + LinSysMasA[i].ToString() + " ";
                    }
                    for (int i = 0; i < range; i++)
                    {
                        for (int j = 0; j < range; j++)
                        {
                            str2 = str2 + LinSysMatrixB[i, j].ToString() + " ";
                        }
                    }
                    SaveInpuParamLinearSystems(true, "LinearSystems", "Zeidel", range.ToString(), str1, str2);
                    break;
                case "Matrix Determinant":
                    string str = "";
                    for (int i = 0; i < rangeMatrix; i++)
                    {
                        for (int j = 0; j < rangeMatrix; j++)
                        {
                            str = str + MatrixInit[i, j].ToString() + " ";
                        }
                    }
                    SaveInpuParamMatrix(true, "MatrixAlgebra", "MatrixDeterminant", rangeMatrix.ToString(), str);
                    break;
                case "Decomposition of matrix LU":
                    str = "";
                    for (int i = 0; i < rangeMatrix; i++)
                    {
                        for (int j = 0; j < rangeMatrix; j++)
                        {
                            str = str + MatrixInit[i, j].ToString() + " ";
                        }
                    }
                    SaveInpuParamMatrix(true, "MatrixAlgebra", "DecompositionOfMatrixLU", rangeMatrix.ToString(), str);
                    break;
                case "Matrix Inverse LU":
                    str = "";
                    for (int i = 0; i < rangeMatrix; i++)
                    {
                        for (int j = 0; j < rangeMatrix; j++)
                        {
                            str = str + MatrixInit[i, j].ToString() + " ";
                        }
                    }
                    SaveInpuParamMatrix(true, "MatrixAlgebra", "MatrixInverseLU", rangeMatrix.ToString(), str);
                    break;
            }
        }
        void SaveInpuParamMatrix(bool SaveAsBool, string NameTypeMethod, string NameInMethod, string R, string M)
        {
            string xmlText = "";
            string[] txtNameTypeMethod = { "Approximate_Decision", "Differential_Equations", "Integration", "NonLinearEqualization" ,
                                         "Optimizing","Interpolation","Statistics","RandomGenerator","MatrixAlgebra","LinearSystems"};
            string[] txtNameInMethod = { "BisectionMethod", "ChordMethod", "IterationMethod", "NewtonMethod", 
                                           "EulerSimple", "EulerModified", "EulerCorrected", "RungeKutta4", 
                                           "Chebishev", "Simpson","Simpson2","Trapezium",
                                           "HalfDivision","HordMetod","NewtonMetod","SecantMetod",
                                       "Brentopt","GoldenSection","Pijavsky",
                                       "LagrangeInterpolator","NewtonInterpolator","NevilleInterpolator","SplineInterpolator","BarycentricInterpolator",
                                       "CorrelationPearson","CorrelationSpearmansRank","DescriptiveStatisticsADev","DescriptiveStatisticsMedian","DescriptiveStatisticsMoments","DescriptiveStatisticsPercentile",
                                       "Method1","Method2","Method3","Method4","Method5",
                                       "MatrixDeterminant","DecompositionOfMatrixLU","MatrixInverseLU",
                                       "Gaus","Zeidel"};
            int[] numberInTypeMethod = { 4, 4, 4, 4, 3, 5, 6, 5, 3, 2 };
            XmlDocument doc = new XmlDocument();
            xmlText = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                            "<Numerical_Methods>";
            int statData1 = 0;
            int statData2 = 0;
            for (int i = 0; i < 10; i++)
            {
                statData2 += numberInTypeMethod[i];
                for (int j = statData1; j < statData2; j++)
                {
                    if (txtNameTypeMethod[i] == NameTypeMethod && txtNameInMethod[j] == NameInMethod)
                    {
                        xmlText += "<" + NameTypeMethod
                                    + " id=\"" + NameInMethod
                                    + "\" Range=\"" + R
                                    + "\" Matrix=\"" + M
                                    + "\"/>";

                    }
                    else
                    {
                        if ((txtNameTypeMethod[i] == "MatrixAlgebra" && txtNameInMethod[j] == "MatrixDeterminant" && txtNameInMethod[j] != NameInMethod) || (txtNameTypeMethod[i] == "MatrixAlgebra" && txtNameInMethod[j] == "DecompositionOfMatrixLU" && txtNameInMethod[j] != NameInMethod) || (txtNameTypeMethod[i] == "MatrixAlgebra" && txtNameInMethod[j] == "MatrixInverseLU" && txtNameInMethod[j] != NameInMethod))
                        {
                            string str = "";
                            InpuMatrix("MatrixAlgebra", txtNameInMethod[j]);
                            for (int r1 = 0; r1 < rangeMatrix; r1++)
                            {
                                for (int r2 = 0; r2 < rangeMatrix; r2++)
                                {
                                    str = str + MatrixInit[r1, r2] + " ";
                                }
                            }

                            xmlText += "<" + txtNameTypeMethod[i]
                               + " id=\"" + txtNameInMethod[j]
                               + "\" Range=\"" + rangeMatrix
                               + "\" Matrix=\"" + str
                               + "\"/>";
                        }
                        else
                        {
                            if ((txtNameTypeMethod[i] == "LinearSystems" && txtNameInMethod[j] == "Gaus") || (txtNameTypeMethod[i] == "LinearSystems" && txtNameInMethod[j] == "Zeidel"))
                            {

                                InpuLinearSystems("LinearSystems", txtNameInMethod[j]);

                                string str1 = "";
                                string str2 = "";
                                for (int t1 = 0; t1 < range; t1++)
                                    str1 = str1 + LinSysMasA[t1] + " ";
                                for (int r1 = 0; r1 < range; r1++)
                                {
                                    for (int r2 = 0; r2 < range; r2++)
                                    {
                                        str2 = str2 + LinSysMatrixB[r1, r2] + " ";
                                    }
                                }
                                xmlText += "<" + txtNameTypeMethod[i]
                                        + " id=\"" + txtNameInMethod[j]
                                        + "\" Range=\"" + range
                                        + "\" LinSysMasA=\"" + str1
                                        + "\" LinSysMatrixB=\"" + str2
                                        + "\"/>";
                            }
                            else
                            {

                                InpuParamMethod(txtNameTypeMethod[i], txtNameInMethod[j]);
                                xmlText += "<" + txtNameTypeMethod[i]
                                            + " id=\"" + txtNameInMethod[j]
                                              + "\" Function=\"" + TestFunnction
                                            + "\" ParamInput1=\"" + ParamInput1
                                            + "\" ParamInput2=\"" + ParamInput2
                                            + "\" ParamInput3=\"" + ParamInput3
                                            + "\" ParamInput4=\"" + ParamInput4
                                            + "\"/>";
                            }
                        }
                    }
                }
                statData1 = statData2;
            }
            xmlText += "</Numerical_Methods>";
            doc.LoadXml(xmlText);
            if (SaveAsBool == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML Files|*.xml";
                saveFileDialog.Title = "Save as:";
                saveFileDialog.RestoreDirectory = true;
                // Show the Dialog.
                // If the user clicked OK in the dialog and
                if (saveFileDialog.ShowDialog() == true)
                {
                    // this.Cursor = new Cursor(openFileDialog1.OpenFile());
                    doc.Save(saveFileDialog.OpenFile());
                    MessageBox.Show("Data save in file \'" + saveFileDialog.SafeFileName + ".\'", "Info");
                }
            }
            else
            {
                doc.Save(Environment.CurrentDirectory.ToString() + "/Data/SaveData.xml");
                MessageBox.Show("Data save in file \'SaveData.xml.\'", "Info");
            }
        }
        void SaveInpuParamLinearSystems(bool SaveAsBool, string NameTypeMethod, string NameInMethod, string R, string A, string B)
        {
            string xmlText = "";
            string[] txtNameTypeMethod = { "Approximate_Decision", "Differential_Equations", "Integration", "NonLinearEqualization" ,
                                         "Optimizing","Interpolation","Statistics","RandomGenerator","MatrixAlgebra","LinearSystems"};
            string[] txtNameInMethod = { "BisectionMethod", "ChordMethod", "IterationMethod", "NewtonMethod", 
                                           "EulerSimple", "EulerModified", "EulerCorrected", "RungeKutta4", 
                                           "Chebishev", "Simpson","Simpson2","Trapezium",
                                           "HalfDivision","HordMetod","NewtonMetod","SecantMetod",
                                       "Brentopt","GoldenSection","Pijavsky",
                                       "LagrangeInterpolator","NewtonInterpolator","NevilleInterpolator","SplineInterpolator","BarycentricInterpolator",
                                       "CorrelationPearson","CorrelationSpearmansRank","DescriptiveStatisticsADev","DescriptiveStatisticsMedian","DescriptiveStatisticsMoments","DescriptiveStatisticsPercentile",
                                       "Method1","Method2","Method3","Method4","Method5",
                                       "MatrixDeterminant","DecompositionOfMatrixLU","MatrixInverseLU",
                                       "Gaus","Zeidel"};
            int[] numberInTypeMethod = { 4, 4, 4, 4, 3, 5, 6, 5, 3, 2 };
            XmlDocument doc = new XmlDocument();
            xmlText = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                            "<Numerical_Methods>";
            int statData1 = 0;
            int statData2 = 0;
            for (int i = 0; i < 10; i++)
            {
                statData2 += numberInTypeMethod[i];
                for (int j = statData1; j < statData2; j++)
                {
                    if (txtNameTypeMethod[i] == NameTypeMethod && txtNameInMethod[j] == NameInMethod)
                    {
                        xmlText += "<" + NameTypeMethod
                                    + " id=\"" + NameInMethod
                                    + "\" Range=\"" + R
                                    + "\" LinSysMasA=\"" + A
                                    + "\" LinSysMatrixB=\"" + B
                                    + "\"/>";
                    }
                    else
                    {
                        if ((txtNameTypeMethod[i] == "MatrixAlgebra" && txtNameInMethod[j] == "MatrixDeterminant" && txtNameInMethod[j] != NameInMethod) || (txtNameTypeMethod[i] == "MatrixAlgebra" && txtNameInMethod[j] == "DecompositionOfMatrixLU" && txtNameInMethod[j] != NameInMethod) || (txtNameTypeMethod[i] == "MatrixAlgebra" && txtNameInMethod[j] == "MatrixInverseLU" && txtNameInMethod[j] != NameInMethod))
                        {
                            string str = "";
                            InpuMatrix("MatrixAlgebra", txtNameInMethod[j]);
                            for (int r1 = 0; r1 < rangeMatrix; r1++)
                            {
                                for (int r2 = 0; r2 < rangeMatrix; r2++)
                                {
                                    str = str + MatrixInit[r1, r2] + " ";
                                }
                            }

                            xmlText += "<" + txtNameTypeMethod[i]
                               + " id=\"" + txtNameInMethod[j]
                               + "\" Range=\"" + rangeMatrix
                               + "\" Matrix=\"" + str
                               + "\"/>";
                        }
                        else
                        {
                            if ((txtNameTypeMethod[i] == "LinearSystems" && txtNameInMethod[j] == "Gaus" && txtNameInMethod[j] != NameInMethod) || (txtNameTypeMethod[i] == "LinearSystems" && txtNameInMethod[j] == "Zeidel" && txtNameInMethod[j] != NameInMethod))
                            {

                                InpuLinearSystems("LinearSystems", txtNameInMethod[j]);

                                string str1 = "";
                                string str2 = "";
                                for (int t1 = 0; t1 < range; t1++)
                                    str1 = str1 + LinSysMasA[t1] + " ";
                                for (int r1 = 0; r1 < range; r1++)
                                {
                                    for (int r2 = 0; r2 < range; r2++)
                                    {
                                        str2 = str2 + LinSysMatrixB[r1, r2] + " ";
                                    }
                                }
                                xmlText += "<" + txtNameTypeMethod[i]
                                        + " id=\"" + txtNameInMethod[j]
                                        + "\" Range=\"" + range
                                        + "\" LinSysMasA=\"" + str1
                                        + "\" LinSysMatrixB=\"" + str2
                                        + "\"/>";
                            }
                            else
                            {
                                InpuParamMethod(txtNameTypeMethod[i], txtNameInMethod[j]);
                                xmlText += "<" + txtNameTypeMethod[i]
                                            + " id=\"" + txtNameInMethod[j]
                                              + "\" Function=\"" + TestFunnction
                                            + "\" ParamInput1=\"" + ParamInput1
                                            + "\" ParamInput2=\"" + ParamInput2
                                            + "\" ParamInput3=\"" + ParamInput3
                                            + "\" ParamInput4=\"" + ParamInput4
                                            + "\"/>";
                            }
                        }
                    }
                }
                statData1 = statData2;
            }
            xmlText += "</Numerical_Methods>";
            doc.LoadXml(xmlText);
            if (SaveAsBool == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML Files|*.xml";
                saveFileDialog.Title = "Save as:";
                saveFileDialog.RestoreDirectory = true;
                // Show the Dialog.
                // If the user clicked OK in the dialog and
                if (saveFileDialog.ShowDialog() == true)
                {
                    // this.Cursor = new Cursor(openFileDialog1.OpenFile());
                    doc.Save(saveFileDialog.OpenFile());
                    MessageBox.Show("Data save in file \'" + saveFileDialog.SafeFileName + ".\'", "Info");
                }
            }
            else
            {
                doc.Save(Environment.CurrentDirectory.ToString() + "/Data/SaveData.xml");
                MessageBox.Show("Data save in file \'SaveData.xml.\'", "Info");
            }
        }
        void SaveInpuParamMethod(bool SaveAsBool, string NameTypeMethod, string NameInMethod, string function, string Param1, string Param2, string Param3, string Param4)
        {
            string xmlText = "";
            string[] txtNameTypeMethod = { "Approximate_Decision", "Differential_Equations", "Integration", "NonLinearEqualization" ,
                                         "Optimizing","Interpolation","Statistics","RandomGenerator","MatrixAlgebra","LinearSystems"};
            string[] txtNameInMethod = { "BisectionMethod", "ChordMethod", "IterationMethod", "NewtonMethod", 
                                           "EulerSimple", "EulerModified", "EulerCorrected", "RungeKutta4", 
                                           "Chebishev", "Simpson","Simpson2","Trapezium",
                                           "HalfDivision","HordMetod","NewtonMetod","SecantMetod",
                                       "Brentopt","GoldenSection","Pijavsky",
                                       "LagrangeInterpolator","NewtonInterpolator","NevilleInterpolator","SplineInterpolator","BarycentricInterpolator",
                                       "CorrelationPearson","CorrelationSpearmansRank","DescriptiveStatisticsADev","DescriptiveStatisticsMedian","DescriptiveStatisticsMoments","DescriptiveStatisticsPercentile",
                                       "Method1","Method2","Method3","Method4","Method5",
                                       "MatrixDeterminant","DecompositionOfMatrixLU","MatrixInverseLU",
                                       "Gaus","Zeidel"};
            int[] numberInTypeMethod = { 4, 4, 4, 4, 3, 5, 6, 5, 3, 2 };
            XmlDocument doc = new XmlDocument();
            xmlText = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                            "<Numerical_Methods>";
            int statData1 = 0;
            int statData2 = 0;
            for (int i = 0; i < 10; i++)
            {
                statData2 += numberInTypeMethod[i];
                for (int j = statData1; j < statData2; j++)
                {
                    if (txtNameTypeMethod[i] == NameTypeMethod && txtNameInMethod[j] == NameInMethod)
                    {

                        xmlText += "<" + NameTypeMethod
                                    + " id=\"" + NameInMethod
                                    + "\" Function=\"" + function
                                    + "\" ParamInput1=\"" + Param1
                                    + "\" ParamInput2=\"" + Param2
                                    + "\" ParamInput3=\"" + Param3
                                    + "\" ParamInput4=\"" + Param4
                                    + "\"/>";
                    }
                    else
                    {

                        if ((txtNameTypeMethod[i] == "MatrixAlgebra" && txtNameInMethod[j] == "MatrixDeterminant" && txtNameInMethod[j] != NameInMethod) || (txtNameTypeMethod[i] == "MatrixAlgebra" && txtNameInMethod[j] == "DecompositionOfMatrixLU" && txtNameInMethod[j] != NameInMethod) || (txtNameTypeMethod[i] == "MatrixAlgebra" && txtNameInMethod[j] == "MatrixInverseLU" && txtNameInMethod[j] != NameInMethod))
                        {
                            string str = "";
                            InpuMatrix("MatrixAlgebra", txtNameInMethod[j]);
                            for (int r1 = 0; r1 < rangeMatrix; r1++)
                            {
                                for (int r2 = 0; r2 < rangeMatrix; r2++)
                                {
                                    str = str + MatrixInit[r1, r2] + " ";
                                }
                            }

                            xmlText += "<" + txtNameTypeMethod[i]
                               + " id=\"" + txtNameInMethod[j]
                               + "\" Range=\"" + rangeMatrix
                               + "\" Matrix=\"" + str
                               + "\"/>";
                        }
                        else
                        {
                            if ((txtNameTypeMethod[i] == "LinearSystems" && txtNameInMethod[j] == "Gaus") || (txtNameTypeMethod[i] == "LinearSystems" && txtNameInMethod[j] == "Zeidel"))
                            {

                                InpuLinearSystems("LinearSystems", txtNameInMethod[j]);

                                string str1 = "";
                                string str2 = "";
                                for (int t1 = 0; t1 < range; t1++)
                                    str1 = str1 + LinSysMasA[t1] + " ";
                                for (int r1 = 0; r1 < range; r1++)
                                {
                                    for (int r2 = 0; r2 < range; r2++)
                                    {
                                        str2 = str2 + LinSysMatrixB[r1, r2] + " ";
                                    }
                                }
                                xmlText += "<" + txtNameTypeMethod[i]
                                        + " id=\"" + txtNameInMethod[j]
                                        + "\" Range=\"" + range
                                        + "\" LinSysMasA=\"" + str1
                                        + "\" LinSysMatrixB=\"" + str2
                                        + "\"/>";
                            }
                            else
                            {
                                InpuParamMethod(txtNameTypeMethod[i], txtNameInMethod[j]);
                                xmlText += "<" + txtNameTypeMethod[i]
                                            + " id=\"" + txtNameInMethod[j]
                                             + "\" Function=\"" + TestFunnction
                                            + "\" ParamInput1=\"" + ParamInput1
                                            + "\" ParamInput2=\"" + ParamInput2
                                            + "\" ParamInput3=\"" + ParamInput3
                                            + "\" ParamInput4=\"" + ParamInput4
                                            + "\"/>";
                            }
                        }
                    }
                }
                statData1 = statData2;
            }
            xmlText += "</Numerical_Methods>";
            doc.LoadXml(xmlText);
            if (SaveAsBool == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML Files|*.xml";
                saveFileDialog.Title = "Save as:";
                saveFileDialog.RestoreDirectory = true;
                // Show the Dialog.
                // If the user clicked OK in the dialog and
                if (saveFileDialog.ShowDialog() == true)
                {
                    // this.Cursor = new Cursor(openFileDialog1.OpenFile());
                    doc.Save(saveFileDialog.OpenFile());
                    MessageBox.Show("Data save in file \'" + saveFileDialog.SafeFileName + ".\'", "Info");
                }
            }
            else
            {
                doc.Save(Environment.CurrentDirectory.ToString() + "/Data/SaveData.xml");
                MessageBox.Show("Data save in file \'SaveData.xml \'", "Info");
            }
            doc.RemoveAll();

        }
        void UnimplementedOnFileClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            string strltem = item.Header.ToString().Replace("_", "");
            MessageBox.Show("The " + strltem + " option has not yet been implemented", Title);
        }

        void OnAboutClick(object sender, RoutedEventArgs args)
        {
            DialogAbout dlg = new DialogAbout();
            dlg.Owner = this;
            dlg.ShowDialog();
        }
        void OnMethodsClick(object sender, RoutedEventArgs args)
        {
            Window1 dlg = new Window1();
            dlg.Owner = this;

            dlg.Show();
        }
        void OnOptionPrecisionClick(object sender, RoutedEventArgs args)
        {
            DialogOptionPrecision dlgPec = new DialogOptionPrecision();
            dlgPec.Owner = this;
            dlgPec.Precision = precision;
            if (dlgPec.ShowDialog().GetValueOrDefault())
            {
                precision = dlgPec.Precision;
            }
        }
        void OnNewFunctionClick(object sender, RoutedEventArgs args)
        {
            DlgInputFunction dlgPec = new DlgInputFunction();
            dlgPec.Owner = this;
            dlgPec.Function = TestFunnction;
            // dlgPec.ShowDialog();
            if (dlgPec.ShowDialog().GetValueOrDefault())
            {
                TestFunnction = dlgPec.Function;
                txtTextFunnction.Text = TestFunnction;
            }
        }
        void OnNewEnterEquationClick(object sender, RoutedEventArgs args)
        {
            DlgInputSystemEquation dlgPec = new DlgInputSystemEquation();
            dlgPec.Owner = this;
            dlgPec.MatrixA = LinSysMasA;
            dlgPec.MatrixB = LinSysMatrixB;
            dlgPec.Range = range;
            dlgPec.InitDlg();
            // dlgPec.Function = TestFunnction;
            // dlgPec.ShowDialog();
            if (dlgPec.ShowDialog().GetValueOrDefault())
            {
                LinSysMasA = dlgPec.MatrixA;
                LinSysMatrixB = dlgPec.MatrixB;
                range = dlgPec.Range;
                switch (itemSelected.Text)
                {
                    case "Gaus":

                        LinearSystemsDesign();

                        break;
                    case "Zeidel":

                        LinearSystemsDesign();

                        break;
                }

            }
        }
        void OnNewEnterMatrixClick(object sender, RoutedEventArgs args)
        {
            DlgInputMatrix dlgPec = new DlgInputMatrix();
            dlgPec.Owner = this;
            dlgPec.Matrix = MatrixInit;
            dlgPec.Range = rangeMatrix - 1;
            dlgPec.InitDlg();
            // dlgPec.Function = TestFunnction;
            // dlgPec.ShowDialog();
            if (dlgPec.ShowDialog().GetValueOrDefault())
            {
                MatrixInit = dlgPec.Matrix;
                rangeMatrix = dlgPec.Range + 1;
                switch (itemSelected.Text)
                {
                    case "Matrix Determinant":
                        MatrixDesign();
                        break;
                    case "Decomposition of matrix LU":
                        MatrixDesign();
                        break;
                    case "Matrix Inverse LU":
                        MatrixDesign();
                        break;
                }

            }
        }
        void OnExitClick(object sender, RoutedEventArgs args)
        {
            Close();
        }
        void OnWindowNoborderClick(object sender, RoutedEventArgs args)
        {
            WindowStyle = WindowStyle.None;
        }
        void OnWindowSingleBorderClick(object sender, RoutedEventArgs args)
        {
            WindowStyle = WindowStyle.SingleBorderWindow;
        }
        void OnWindow3DBorderClick(object sender, RoutedEventArgs args)
        {
            WindowStyle = WindowStyle.ThreeDBorderWindow;
        }
        void OnWindowToolClick(object sender, RoutedEventArgs args)
        {
            WindowStyle = WindowStyle.ToolWindow;
        }

        void OnWindowMaximizedClick(object sender, RoutedEventArgs args)
        {
            WindowState = WindowState.Maximized;
        }
        void OnWindowMinimizedClick(object sender, RoutedEventArgs args)
        {
            WindowState = WindowState.Minimized;
        }
        void OnWindowNormalClick(object sender, RoutedEventArgs args)
        {
            WindowState = WindowState.Normal;
        }

        void OnWindowCanMaximizeClick(object sender, RoutedEventArgs args)
        {
            ResizeMode = ResizeMode.CanMinimize;
        }
        void OnWindowCanResizeClick(object sender, RoutedEventArgs args)
        {
            ResizeMode = ResizeMode.CanResize;
        }
        void OnWindowCanResizeWithGripClick(object sender, RoutedEventArgs args)
        {
            ResizeMode = ResizeMode.CanResizeWithGrip;
        }
        void OnWindowNoResizeClick(object sender, RoutedEventArgs args)
        {
            ResizeMode = ResizeMode.NoResize;
        }

        void TaskbarOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            ShowInTaskbar = item.IsChecked;
        }
        void TopmostOnCheck(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            Topmost = item.IsChecked;
        }

        void OnTreeMenu_Minimize(object sender, RoutedEventArgs args)
        {

            itemTree_Normal.IsChecked = false;
            itemTree_Extended.IsChecked = false;
            itemTree_Minimize.IsChecked = true;
            treeMenu = new TreeView();
            scrollMenu.Content = treeMenu;
            treeMenu.Background = Brushes.White;
            treeMenu.BorderBrush = Brushes.White;
            treeMenu.Margin = new Thickness(5);
            treeMenu.HorizontalAlignment = HorizontalAlignment.Stretch;
            treeMenu.VerticalAlignment = VerticalAlignment.Stretch;
            TreeViewMenu("Minimize");
        }
        void OnTreeMenu_Normal(object sender, RoutedEventArgs args)
        {
            itemTree_Minimize.IsChecked = false;
            itemTree_Extended.IsChecked = false;
            itemTree_Normal.IsChecked = true;
            treeMenu = new TreeView();
            scrollMenu.Content = treeMenu;
            treeMenu.Background = Brushes.White;
            treeMenu.BorderBrush = Brushes.White;
            treeMenu.Margin = new Thickness(5);
            treeMenu.HorizontalAlignment = HorizontalAlignment.Stretch;
            treeMenu.VerticalAlignment = VerticalAlignment.Stretch;
            TreeViewMenu("Normal");
        }
        void OnTreeMenu_Extended(object sender, RoutedEventArgs args)
        {
            itemTree_Minimize.IsChecked = false;
            itemTree_Normal.IsChecked = false;
            itemTree_Extended.IsChecked = true;
            treeMenu = new TreeView();
            scrollMenu.Content = treeMenu;
            treeMenu.Background = Brushes.White;
            treeMenu.BorderBrush = Brushes.White;
            treeMenu.Margin = new Thickness(5);
            treeMenu.HorizontalAlignment = HorizontalAlignment.Stretch;
            treeMenu.VerticalAlignment = VerticalAlignment.Stretch;
            TreeViewMenu("Extended");
        }
        //*************/Menu************************

        //*************ToolBar************************
        void MyToolBarFile(ToolBar toolbar)
        {
            RoutedUICommand[] comm = 
                { 
                    ApplicationCommands.New, 
                    ApplicationCommands.Open, 
                    ApplicationCommands.Save,
                    ApplicationCommands.SaveAs,
                };
            string[] strImages = 
                { 
                    "New.png", "Open.png", "filesave.png", "filesaveAs.png","Execute.png","Info.png","Help.png","Exit.png"
                };
            // Створення кнопок панелі інструментів
            Image img;

            ToolTip tip;
            // toolbar.Items.Add(new Separator());
            // Створення обєкта btnNew 
            btnNew = new Button();
            btnNew.Command = comm[0];
            toolbar.Items.Add(btnNew);
            // Створення обєкта Image как содержимого btnNew 
            img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[0]));
            btnNew.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            tip = new ToolTip();
            tip.Content = comm[0].Text;
            btnNew.ToolTip = tip;
            // Включеня UlCommand в привязці команд вікна 
            CommandBindings.Add(new CommandBinding(comm[0], OnFileNewClick));

            // Створення обєкта btnOpen 
            btnOpen = new Button();
            btnOpen.Command = comm[1];
            toolbar.Items.Add(btnOpen);
            // Створення обєкта Image как содержимого btnNew 
            img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[1]));
            btnOpen.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            tip = new ToolTip();
            tip.Content = comm[1].Text;
            btnOpen.ToolTip = tip;
            // Включеня UlCommand в привязці команд вікна 
            CommandBindings.Add(new CommandBinding(comm[1], OnFileOpenClick));

            // Створення обєкта btnSave 
            btnSave = new Button();
            btnSave.Command = comm[2];
            toolbar.Items.Add(btnSave);
            // Створення обєкта Image как содержимого btnNew 
            img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[2]));
            btnSave.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            tip = new ToolTip();
            tip.Content = comm[2].Text;
            btnSave.ToolTip = tip;
            // Включеня UlCommand в привязці команд вікна 
            CommandBindings.Add(new CommandBinding(comm[2], OnFileSaveClick));

            // Створення обєкта btnSave 
            btnSaveAs = new Button();
            btnSaveAs.Command = comm[3];
            toolbar.Items.Add(btnSaveAs);
            // Створення обєкта Image как содержимого btnNew 
            img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[3]));
            btnSaveAs.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            tip = new ToolTip();
            tip.Content = comm[3].Text;
            btnSaveAs.ToolTip = tip;
            // Включеня UlCommand в привязці команд вікна 
            CommandBindings.Add(new CommandBinding(comm[3], OnFileSaveAsClick));

            toolbar.Items.Add(new Separator());
            toolbar.Items.Add(new Separator());

            // Створення обєкта btnExecute
            btnExecute = new Button();
            // btnExit.Command = comm[2];
            btnExecute.Click += OnExecuteButton;
            toolbar.Items.Add(btnExecute);
            // Створення обєкта Image как содержимого btnNew 
            img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Fill;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[4]));

            btnExecute.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            tip = new ToolTip();
            tip.Content = "Execute";
            btnExecute.ToolTip = tip;


            toolbar.Items.Add(new Separator());
            toolbar.Items.Add(new Separator());

            // Створення обєкта btnInfo
            Button btnInfo = new Button();
            btnInfo.Click += OnAboutClick;
            toolbar.Items.Add(btnInfo);
            // Створення обєкта Image как содержимого btnNew 
            img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[5]));
            btnInfo.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            tip = new ToolTip();
            tip.Content = "Info";
            btnInfo.ToolTip = tip;

            // Створення обєкта btnHelp
            Button btnHelp = new Button();
            btnHelp.Click += OnMethodsClick;
            toolbar.Items.Add(btnHelp);
            // Створення обєкта Image как содержимого btnNew 
            img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[6]));
            btnHelp.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            tip = new ToolTip();
            tip.Content = "Help";
            btnHelp.ToolTip = tip;

            toolbar.Items.Add(new Separator());
            toolbar.Items.Add(new Separator());
            // Створення обєкта btnExit
            btnExit = new Button();
            // btnExit.Command = comm[2];
            btnExit.Click += OnExitClick;
            toolbar.Items.Add(btnExit);
            // Створення обєкта Image как содержимого btnNew 
            img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[7]));
            btnExit.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            tip = new ToolTip();
            tip.Content = "Exit";
            btnExit.ToolTip = tip;
        }
        void MyToolBarTree(ToolBar toolbar)
        {
            string[] strNames = 
                { 
                    "Minimize", "Normal", "Extended"
                };
            string[] strImages = 
                { 
                    "Minimize.png", "Normal.png", "Extended.png"
                };
            Image img;
            ToolTip tip;
            // Створення кнопок панелі інструментів

            // Створення обєкта btnTreeMenu_Minimize 
            btnTreeMenu_Minimize = new Button();
            btnTreeMenu_Minimize.Click += OnTreeMenu_Minimize;
            toolbar.Items.Add(btnTreeMenu_Minimize);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[0]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btnTreeMenu_Minimize.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[0];
            btnTreeMenu_Minimize.ToolTip = tip;

            // Створення обєкта btnTreeMenu_Normal 
            btnTreeMenu_Normal = new Button();
            btnTreeMenu_Normal.Click += OnTreeMenu_Normal;
            toolbar.Items.Add(btnTreeMenu_Normal);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[1]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btnTreeMenu_Normal.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[1];
            btnTreeMenu_Normal.ToolTip = tip;

            // Створення обєкта btnTreeMenu_Extended
            btnTreeMenu_Extended = new Button();
            btnTreeMenu_Extended.Click += OnTreeMenu_Extended;
            toolbar.Items.Add(btnTreeMenu_Extended);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[2]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btnTreeMenu_Extended.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[2];
            btnTreeMenu_Extended.ToolTip = tip;
        }
        void MyToolBarResizeMode(ToolBar toolbar)
        {
            string[] strNames = 
                { 
                    "Minimize", "Resize", "Resize With Grip", "No Resize"
                };
            string[] strImages = 
                { 
                    "ResizeModeMinimize.png", "ResizeModeResize.png", "ResizeWithGrip.png", "ResizeModeNoResize.png"
                };
            Image img;
            ToolTip tip;
            // Створення кнопок панелі інструментів

            // Створення обєкта btnTreeMenu_Minimize 
            btnResizeMode_Minimize = new Button();
            btnResizeMode_Minimize.Click += OnWindowCanMaximizeClick;
            toolbar.Items.Add(btnResizeMode_Minimize);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[0]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btnResizeMode_Minimize.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[0];
            btnResizeMode_Minimize.ToolTip = tip;

            btnResizeMode_Resize = new Button();
            btnResizeMode_Resize.Click += OnWindowCanResizeClick;
            toolbar.Items.Add(btnResizeMode_Resize);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[1]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btnResizeMode_Resize.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[1];
            btnResizeMode_Resize.ToolTip = tip;

            btnResizeMode_Resize_With_Grip = new Button();
            btnResizeMode_Resize_With_Grip.Click += OnWindowCanResizeWithGripClick;
            toolbar.Items.Add(btnResizeMode_Resize_With_Grip);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[2]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btnResizeMode_Resize_With_Grip.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[2];
            btnResizeMode_Resize_With_Grip.ToolTip = tip;


            btnResizeMode_No_Resize = new Button();
            btnResizeMode_No_Resize.Click += OnWindowNoResizeClick;
            toolbar.Items.Add(btnResizeMode_No_Resize);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[3]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btnResizeMode_No_Resize.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[3];
            btnResizeMode_No_Resize.ToolTip = tip;


        }
        void MyToolBarStyle(ToolBar toolbar)
        {
            string[] strNames = 
                { 
                    "No border or caption", "Single-border window", "3D-border window", "Tool window"
                };
            string[] strImages = 
                { 
                    "Noborderorcaption.png", "Single_borderwindow.png", "3D_borderWindow.png", "ToolWindow.png"
                };

            Image img;
            ToolTip tip;
            // Створення кнопок панелі інструментів

            // Створення обєкта btnNoBorderOrCaption
            btnNoBorderOrCaption = new Button();
            btnNoBorderOrCaption.Click += OnWindowNoborderClick;
            toolbar.Items.Add(btnNoBorderOrCaption);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[0]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btnNoBorderOrCaption.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[0];
            btnNoBorderOrCaption.ToolTip = tip;

            // Створення обєкта btnSingle_border_Window
            btnSingle_border_Window = new Button();
            btnSingle_border_Window.Click += OnWindowSingleBorderClick;
            toolbar.Items.Add(btnSingle_border_Window);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[1]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btnSingle_border_Window.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[1];
            btnSingle_border_Window.ToolTip = tip;

            // Створення обєкта btn3D_border_Window
            btn3D_border_Window = new Button();
            btn3D_border_Window.Click += OnWindow3DBorderClick;
            toolbar.Items.Add(btn3D_border_Window);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[2]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btn3D_border_Window.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[2];
            btn3D_border_Window.ToolTip = tip;

            // Створення обєкта btn3D_border_Window
            btn3D_Tool_Window = new Button();
            btn3D_Tool_Window.Click += OnWindowToolClick;
            toolbar.Items.Add(btn3D_Tool_Window);
            // Створення обєкта Image как содержимого btnTreeMenu_Minimize 
            img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString()
                + "/images/" + strImages[3]));
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Uniform;
            btn3D_Tool_Window.Content = img;
            // Створення обєкта ToolTip
            tip = new ToolTip();
            tip.Content = strNames[3];
            btn3D_Tool_Window.ToolTip = tip;



        }
        //*************/ToolBar************************

        //*************TreeViewMenu************************
        ControlTemplate DesignTextBox()
        {
            // Создание объекта ControlTemplate для Button 
            ControlTemplate template = new ControlTemplate(typeof(Button));
            // Создание объекта FrameworkElementFactory для Border 
            FrameworkElementFactory factoryBorder = new FrameworkElementFactory(typeof(Border));
            // Назначение имени для последующих ссылок 
            factoryBorder.Name = "border";
            // Задание некоторых свойств по умолчанию 
            factoryBorder.SetValue(Border.BorderBrushProperty, Brushes.Gray);
            factoryBorder.SetValue(Border.BorderThicknessProperty, new Thickness(3));
            factoryBorder.SetValue(Border.BackgroundProperty, SystemColors.ControlLightBrush);

            // Создание объекта FrameworkElementFactory для ContentPresenter 
            FrameworkElementFactory factoryContent = new FrameworkElementFactory(typeof(ContentPresenter));
            // Назначение имени для последующих ссылок 
            factoryContent.Name = "content";
            // Привязка свойств ContentPresenter к свойствам Button 
            factoryContent.SetValue(ContentPresenter.ContentProperty, new TemplateBindingExtension(Button.ContentProperty));
            // Обратите внимание: свойство Padding кнопки 
            // соответствует свойству Margin содержимого! 
            factoryContent.SetValue(ContentPresenter.MarginProperty, new TemplateBindingExtension(Button.PaddingProperty));
            // Назначение ContentPresenter дочерним объектом Border 
            factoryBorder.AppendChild(factoryContent);
            // Border назначается корневым узлом визуального дерева 
            template.VisualTree = factoryBorder;
            // Определение триггера для условия IsMouseOver=true 
            Trigger trig = new Trigger();
            trig.Property = UIElement.IsVisibleProperty;
            trig.Value = true;
            // Связывание объекта Setter с триггером 
            // для изменения свойства CornerRadius элемента Border. 
            Setter set = new Setter();
            set.Property = Border.CornerRadiusProperty;
            set.Value = new CornerRadius(12);
            set.TargetName = "border";
            // Включение объекта Setter в коллекцию Setters триггера 
            trig.Setters.Add(set);
            // Определение объекта Setter для изменения FontStyle. 
            // (Для свойства кнопки задавать TargetName не нужно.) 
            set = new Setter();
            set.Property = Control.FontStyleProperty;
            set.Value = FontStyles.Italic;
            // Добавление в коллекцию Setters того же триггера 
            trig.Setters.Add(set);
            // Включение триггера в шаблон 
            template.Triggers.Add(trig);
            // Определение триггера для IsPressed 
            /* trig = new Trigger();
             trig.Property = Button.IsPressedProperty;
             trig.Value = true;
             set = new Setter();
             set.Property = Border.BackgroundProperty;
             set.Value = SystemColors.ControlDarkBrush;
             set.TargetName = "border";
             // Включение объекта Setter в коллекцию Setters триггера 
             trig.Setters.Add(set);*/
            // Включение триггера в шаблон 
            template.Triggers.Add(trig);

            return template;
        }
        void TreeViewMenu(string view)
        {
            bool IsExpandedMaine = false; ;
            bool IsExpandedOther = false;
            switch (view)
            {
                case "Minimize":
                    IsExpandedMaine = false;
                    IsExpandedOther = false;
                    break;
                case "Normal":
                    IsExpandedMaine = true;
                    IsExpandedOther = false;
                    break;
                case "Extended":
                    IsExpandedMaine = true;
                    IsExpandedOther = true;
                    break;
            }

            //TreeViewItem itemNumericalMethods = new TreeViewItem();
            treeMenu.Items.Clear();
            TreeViewMenuAdd itemNumericalMethods = new TreeViewMenuAdd("Numerical Methods", "maine");
            itemNumericalMethods.IsExpanded = IsExpandedMaine;
            // itemNumericalMethods.Header = "Numerical Methods";
            treeMenu.Items.Add(itemNumericalMethods);
            treeMenu.SelectedItemChanged += TreeViewOnSelectedItemChanged;

            TreeViewMenuAdd itemApproximate = new TreeViewMenuAdd("Approximate decision of \n equalization f(x)=0", "maine");
            itemApproximate.IsExpanded = IsExpandedOther;
            itemNumericalMethods.Items.Add(itemApproximate);

            TreeViewMenuAdd itemBisectionMethod = new TreeViewMenuAdd("Bisection Method", "");
            itemApproximate.Items.Add(itemBisectionMethod);
            TreeViewMenuAdd itemChordMethod = new TreeViewMenuAdd("Chord Method", "");
            itemApproximate.Items.Add(itemChordMethod);
            TreeViewMenuAdd itemIterationMethod = new TreeViewMenuAdd("Iteration Method", "");
            itemApproximate.Items.Add(itemIterationMethod);
            TreeViewMenuAdd itemNewtonnMethod = new TreeViewMenuAdd("Newton Method", "");
            itemApproximate.Items.Add(itemNewtonnMethod);


            TreeViewMenuAdd itemDifferentEquations = new TreeViewMenuAdd("Differential Equations", "maine");
            itemDifferentEquations.IsExpanded = IsExpandedOther;
            itemNumericalMethods.Items.Add(itemDifferentEquations);
            TreeViewMenuAdd itemEulerSimple = new TreeViewMenuAdd("Euler Simple", "");
            itemDifferentEquations.Items.Add(itemEulerSimple);
            TreeViewMenuAdd itemEulerModified = new TreeViewMenuAdd("Euler Modified", "");
            itemDifferentEquations.Items.Add(itemEulerModified);
            TreeViewMenuAdd itemEulerCorrected = new TreeViewMenuAdd("Euler Corrected", "");
            itemDifferentEquations.Items.Add(itemEulerCorrected);
            TreeViewMenuAdd itemRungeKutta4 = new TreeViewMenuAdd("Runge-Kutta4", "");
            itemDifferentEquations.Items.Add(itemRungeKutta4);

            TreeViewMenuAdd itemIntegration = new TreeViewMenuAdd("Integration", "maine");
            itemIntegration.IsExpanded = IsExpandedOther;
            itemNumericalMethods.Items.Add(itemIntegration);
            TreeViewMenuAdd itemChebishev = new TreeViewMenuAdd("Chebishev", "");
            itemIntegration.Items.Add(itemChebishev);
            TreeViewMenuAdd itemSimpson = new TreeViewMenuAdd("Simpson", "");
            itemIntegration.Items.Add(itemSimpson);
            TreeViewMenuAdd itemSimpson2 = new TreeViewMenuAdd("Simpson 2", "");
            itemIntegration.Items.Add(itemSimpson2);
            TreeViewMenuAdd itemTrapezium = new TreeViewMenuAdd("Trapezium", "");
            itemIntegration.Items.Add(itemTrapezium);

            TreeViewMenuAdd itemInterpolation = new TreeViewMenuAdd("Interpolation", "maine");
            itemInterpolation.IsExpanded = IsExpandedOther;
            itemNumericalMethods.Items.Add(itemInterpolation);
            TreeViewMenuAdd itemLagrangeInterpolator = new TreeViewMenuAdd("Lagrange Interpolator", "");
            itemInterpolation.Items.Add(itemLagrangeInterpolator);
            TreeViewMenuAdd itemNewtonInterpolator = new TreeViewMenuAdd("Newton Interpolator", "");
            itemInterpolation.Items.Add(itemNewtonInterpolator);
            TreeViewMenuAdd itemNevilleInterpolator = new TreeViewMenuAdd("Neville Interpolator", "");
            itemInterpolation.Items.Add(itemNevilleInterpolator);
            TreeViewMenuAdd itemSplineInterpolator = new TreeViewMenuAdd("Spline Interpolator", "");
            itemInterpolation.Items.Add(itemSplineInterpolator);
            TreeViewMenuAdd itemBarycentricInterpolator = new TreeViewMenuAdd("Barycentric Interpolator", "");
            itemInterpolation.Items.Add(itemBarycentricInterpolator);

            TreeViewMenuAdd itemLinearSystems = new TreeViewMenuAdd("Linear Systems", "maine");
            itemLinearSystems.IsExpanded = IsExpandedOther;
            itemNumericalMethods.Items.Add(itemLinearSystems);
            TreeViewMenuAdd itemGaus = new TreeViewMenuAdd("Gaus", "");
            itemLinearSystems.Items.Add(itemGaus);
            TreeViewMenuAdd itemZeidel = new TreeViewMenuAdd("Zeidel", "");
            itemLinearSystems.Items.Add(itemZeidel);

            TreeViewMenuAdd itemNonLinearequalization = new TreeViewMenuAdd("Non Linear equalization", "maine");
            itemNonLinearequalization.IsExpanded = IsExpandedOther;
            itemNumericalMethods.Items.Add(itemNonLinearequalization);
            TreeViewMenuAdd itemHalfDivision = new TreeViewMenuAdd("Half Division", "");
            itemNonLinearequalization.Items.Add(itemHalfDivision);
            TreeViewMenuAdd itemHordMetod = new TreeViewMenuAdd("Hord Metod", "");
            itemNonLinearequalization.Items.Add(itemHordMetod);
            TreeViewMenuAdd itemNewtonMetod = new TreeViewMenuAdd("Newton Metod", "");
            itemNonLinearequalization.Items.Add(itemNewtonMetod);
            TreeViewMenuAdd itemSecantMetod = new TreeViewMenuAdd("Secant Metod", "");
            itemNonLinearequalization.Items.Add(itemSecantMetod);

            TreeViewMenuAdd itemMatrixAlgebra = new TreeViewMenuAdd("Matrix Algebra", "maine");
            itemMatrixAlgebra.IsExpanded = IsExpandedOther;
            itemNumericalMethods.Items.Add(itemMatrixAlgebra);
            TreeViewMenuAdd itemMatrixDeterminant = new TreeViewMenuAdd("Matrix Determinant", "");
            itemMatrixAlgebra.Items.Add(itemMatrixDeterminant);
            TreeViewMenuAdd itemDecompositionofmatrixLU = new TreeViewMenuAdd("Decomposition of matrix LU", "");
            itemMatrixAlgebra.Items.Add(itemDecompositionofmatrixLU);
            TreeViewMenuAdd itemMatrixInverseLU = new TreeViewMenuAdd("Matrix Inverse LU", "");
            itemMatrixAlgebra.Items.Add(itemMatrixInverseLU);

            TreeViewMenuAdd itemOptimizing = new TreeViewMenuAdd("Optimizing", "maine");
            itemOptimizing.IsExpanded = IsExpandedOther;
            itemNumericalMethods.Items.Add(itemOptimizing);
            TreeViewMenuAdd itemBrentopt = new TreeViewMenuAdd("Brentopt", "");
            itemOptimizing.Items.Add(itemBrentopt);
            TreeViewMenuAdd itemGoldenSection = new TreeViewMenuAdd("Golden Section", "");
            itemOptimizing.Items.Add(itemGoldenSection);
            TreeViewMenuAdd itemPijavsky = new TreeViewMenuAdd("Pijavsky", "");
            itemOptimizing.Items.Add(itemPijavsky);

            /* TreeViewMenuAdd itemRegression = new TreeViewMenuAdd("Regression", "maine");
             itemNumericalMethods.Items.Add(itemRegression);
             TreeViewMenuAdd itemLinearRegression = new TreeViewMenuAdd("Linear Regression", "");
             itemRegression.Items.Add(itemLinearRegression);
             TreeViewMenuAdd itemEstimatedPolynomial = new TreeViewMenuAdd("Estimated Polynomial", "");
             itemRegression.Items.Add(itemEstimatedPolynomial);
             TreeViewMenuAdd itemPolynomialFunction = new TreeViewMenuAdd("Polynomial Function", "");
             itemRegression.Items.Add(itemPolynomialFunction);*/

            TreeViewMenuAdd itemStatistics = new TreeViewMenuAdd("Statistics", "maine");
            itemStatistics.IsExpanded = IsExpandedOther;
            itemNumericalMethods.Items.Add(itemStatistics);
            TreeViewMenuAdd itemCorrelationPearson = new TreeViewMenuAdd("Correlation Pearson", "");
            itemStatistics.Items.Add(itemCorrelationPearson);
            TreeViewMenuAdd itemCorrelationSpearmansRank = new TreeViewMenuAdd("Correlation Spearmans Rank", "");
            itemStatistics.Items.Add(itemCorrelationSpearmansRank);
            TreeViewMenuAdd itemDescriptiveStatisticsADev = new TreeViewMenuAdd("Descriptive Statistics A Dev", "");
            itemStatistics.Items.Add(itemDescriptiveStatisticsADev);
            TreeViewMenuAdd itemDescriptiveStatisticsMedian = new TreeViewMenuAdd("Descriptive Statistics Median", "");
            itemStatistics.Items.Add(itemDescriptiveStatisticsMedian);
            TreeViewMenuAdd itemDescriptiveStatisticsMoments = new TreeViewMenuAdd("Descriptive Statistics Moments", "");
            itemStatistics.Items.Add(itemDescriptiveStatisticsMoments);
            TreeViewMenuAdd itemDescriptiveStatisticsPercentile = new TreeViewMenuAdd("Descriptive Statistics Percentile", "");
            itemStatistics.Items.Add(itemDescriptiveStatisticsPercentile);

            TreeViewMenuAdd itemRandomGenerator = new TreeViewMenuAdd("Random Generator", "maine");
            itemRandomGenerator.IsExpanded = IsExpandedOther;
            itemStatistics.Items.Add(itemRandomGenerator);
            TreeViewMenuAdd itemMethod1 = new TreeViewMenuAdd("Method 1", "");
            itemRandomGenerator.Items.Add(itemMethod1);
            TreeViewMenuAdd itemMethod2 = new TreeViewMenuAdd("Method 2", "");
            itemRandomGenerator.Items.Add(itemMethod2);
            TreeViewMenuAdd itemMethod3 = new TreeViewMenuAdd("Method 3", "");
            itemRandomGenerator.Items.Add(itemMethod3);
            TreeViewMenuAdd itemMethod4 = new TreeViewMenuAdd("Method 4", "");
            itemRandomGenerator.Items.Add(itemMethod4);
            TreeViewMenuAdd itemMethod5 = new TreeViewMenuAdd("Method 5", "");
            itemRandomGenerator.Items.Add(itemMethod5);

        }

        void TreeViewOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> args)
        {
            boxParameters.Content = "";
            boxResult.Content = "";
            sourceCode.Text = "";
            boxResultMathd.Content = "";
            // Получение выделенного узла 
            itemSelected = args.NewValue as TreeViewMenuAdd;

            Image img = new Image();
            img.Stretch = Stretch.Fill;
            boxResultMathd.Content = img;
            Uri uri;
            FileNew = false;
            FileSave = false;
            FileSaveAs = false;
            btnExecute.IsEnabled = false;
            SelectedTreeName = itemSelected.Text;
            switch (itemSelected.Text)
            {

                //*** Approximate decision of equalization f(x)=0 ***
                case "Bisection Method":
                    sourceCode.Text = AllMethodText.Approximation_Bisection;
                    InpuParamMethod("Approximate_Decision", "BisectionMethod");
                    InputParametrs(TestFunnction, "Left limit of segment:", "Right limit of segment:", "Exactness of calculation:", "",
                         ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, false);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/Approximation/MethodBisectin.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Chord Method":
                    InpuParamMethod("Approximate_Decision", "ChordMethod");
                    sourceCode.Text = AllMethodText.Approximation_Chord;
                    InputParametrs(TestFunnction, "Left limit of segment:", "Right limit of segment:", "Initial approaching:", "Exactness of calculation:",
                       ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, true);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/Approximation/MethoHord.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Iteration Method":
                    InpuParamMethod("Approximate_Decision", "IterationMethod");
                    sourceCode.Text = AllMethodText.Approximation_Iterationmethod;
                    InputParametrs(TestFunnction, "Left limit of segment:", "Right limit of segment:", "Initial approaching:", "Exactness of calculation:",
                       ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, true);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/Approximation/MethodIteration.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Newton Method":
                    InpuParamMethod("Approximate_Decision", "NewtonMethod");
                    sourceCode.Text = AllMethodText.Approximation_Newton;
                    InputParametrs(TestFunnction, "Left limit of segment:", "Right limit of segment:", "Initial approaching:", "Exactness of calculation:",
                         ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, true);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/Approximation/MethoNewton.jpg");
                    img.Source = new BitmapImage(uri); ;
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                //--------------------------
                //*** Differential Equations ***
                case "Euler Simple":
                    InpuParamMethod("Differential_Equations", "EulerSimple");
                    sourceCode.Text = AllMethodText.DifferentialEquations_EulerSimple;
                    InputParametrs(TestFunnction, "Beginning of interval:", "End of interval:", "Initial value:", "Points number:",
                       ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, true);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/DifferentialEquations/DirrefentialEquations.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Euler Modified":
                    InpuParamMethod("Differential_Equations", "EulerModified");
                    sourceCode.Text = AllMethodText.DifferentialEquations_EulerModified;
                    InputParametrs(TestFunnction, "Beginning of interval:", "End of interval:", "Initial value:", "Points number:",
                        ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, true);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/DifferentialEquations/DirrefentialEquations.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Euler Corrected":
                    InpuParamMethod("Differential_Equations", "EulerCorrected");
                    sourceCode.Text = AllMethodText.DifferentialEquations_EulerCorrected;
                    InputParametrs(TestFunnction, "Beginning of interval:", "End of interval:", "Initial value:", "Points number:",
                        ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, true);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/DifferentialEquations/DirrefentialEquations.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Runge-Kutta4":
                    InpuParamMethod("Differential_Equations", "RungeKutta4");
                    sourceCode.Text = AllMethodText.DifferentialEquations_RungeKutta4;
                    // boxParameters.Content = "";
                    InputParametrs(TestFunnction, "Beginning of interval:", "End of interval:", "Initial value:", "Points number:",
                        ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, true);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/DifferentialEquations/DirrefentialEquations.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                //*** Integration ***
                case "Chebishev":
                    InpuParamMethod("Integration", "Chebishev");
                    sourceCode.Text = AllMethodText.Integration_Chebishev;
                    InputParametrs(TestFunnction, "Beginning of interval a:", "End of interval b:", "Points number:", "",
                        ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, false);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/Integration/AllMethods.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Simpson":
                    InpuParamMethod("Integration", "Simpson");
                    sourceCode.Text = AllMethodText.Integration_Simpson;
                    InputParametrs(TestFunnction, "Beginning of interval a:", "End of interval b:", "Points number:", "",
                        ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, false);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/Integration/AllMethods.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Simpson 2":
                    InpuParamMethod("Integration", "Simpson2");
                    sourceCode.Text = AllMethodText.Integration_Simpson2;
                    InputParametrs(TestFunnction, "Beginning of interval a:", "End of interval b:", "Points number:", "",
                     ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, false);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/Integration/AllMethods.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Trapezium":
                    InpuParamMethod("Integration", "Trapezium");
                    sourceCode.Text = AllMethodText.Integration_Trapezium;
                    InputParametrs(TestFunnction, "Beginning of interval a:", "End of interval b:", "Points number:", "",
                       ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, false);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/Integration/AllMethods.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                //*** Non Linear equalization ***
                case "Half Division":
                    InpuParamMethod("NonLinearEqualization", "HalfDivision");
                    sourceCode.Text = AllMethodText.NonLinearEquations_HalfDivision;
                    InputParametrs(TestFunnction, "Beginning of interval:", "End of interval:", "Exactness of calculation:", "",
                        ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, false);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/NonLinear/AllMethods.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Hord Metod":
                    InpuParamMethod("NonLinearEqualization", "HordMetod");
                    sourceCode.Text = AllMethodText.NonLinearEquations_MetodHord;
                    InputParametrs(TestFunnction, "Beginning of interval:", "End of interval:", "Amount divisions of segmen:", "",
                        ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, false);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/NonLinear/AllMethods.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Newton Metod":
                    InpuParamMethod("NonLinearEqualization", "NewtonMetod");
                    sourceCode.Text = AllMethodText.NonLinearEquations_Newton;
                    InputParametrs(TestFunnction, "Initial approaching:", "Amount divisions of segmen:", "", "",
                        ParamInput1, true, ParamInput2, true, ParamInput3, false, ParamInput4, false);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/NonLinear/AllMethods.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Secant Metod":
                    InpuParamMethod("NonLinearEqualization", "SecantMetod");
                    sourceCode.Text = AllMethodText.NonLinearEquations_Secant;
                    InputParametrs(TestFunnction, "Delta:", "Step of treatment:", "", "",
                         ParamInput1, true, ParamInput2, true, ParamInput3, false, ParamInput4, false);
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/NonLinear/AllMethods.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                //*** Optimizing***
                case "Brentopt":
                    InpuParamMethod("Optimizing", "Brentopt");
                    sourceCode.Text = AllMethodText.Optimizing_Brentopt;
                    InputParametrs(TestFunnction, "Beginning of interval a:", "End of interval b:", "Epsilon", "",
                       ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Golden Section":
                    InpuParamMethod("Optimizing", "GoldenSection");
                    sourceCode.Text = AllMethodText.Optimizing_GoldenSection;
                    InputParametrs(TestFunnction, "Beginning of interval a:", "End of interval b:", "Points number:", "",
                          ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Pijavsky":
                    InpuParamMethod("Optimizing", "Pijavsky");
                    sourceCode.Text = AllMethodText.Optimizing_Pijavsky;
                    InputParametrs(TestFunnction, "Beginning of interval a:", "End of interval b:", "Constant of Lipshits(>0):", "Number of steps of search:",
                       ParamInput1, true, ParamInput2, true, ParamInput3, true, ParamInput4, true);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                //*** Interpolation ***
                case "Lagrange Interpolator":
                    InpuParamMethod("Interpolation", "LagrangeInterpolator");
                    sourceCode.Text = AllMethodText.Interpolation_LagrangeInterpolator;
                    Interpolation(2, "X  -   abscissas of points. \nF  -   values of functions in these points.", "Point of interpolation",
                         ParamInput1, true, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Newton Interpolator":
                    InpuParamMethod("Interpolation", "NewtonInterpolator");
                    sourceCode.Text = AllMethodText.Interpolation_NewtonInterpolator;
                    Interpolation(2, "X  -   abscissas of points. \nF  -   values of functions in these points.", "Point of interpolation",
                        ParamInput1, true, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Neville Interpolator":
                    InpuParamMethod("Interpolation", "NevilleInterpolator");
                    sourceCode.Text = AllMethodText.Interpolation_NevilleInterpolator;
                    Interpolation(2, "X  -   abscissas of points. \nF  -   values of functions in these points.", "Point of interpolation",
                         ParamInput1, true, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Spline Interpolator":
                    InpuParamMethod("Interpolation", "SplineInterpolator");
                    sourceCode.Text = AllMethodText.Interpolation_SplineInterpolator;
                    Interpolation(2, "X  -   abscissas of points. \nF  -   values of functions in these points.", "Point of interpolation",
                         ParamInput1, true, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Barycentric Interpolator":
                    InpuParamMethod("Interpolation", "BarycentricInterpolator");
                    sourceCode.Text = AllMethodText.Interpolation_BarycentricInterpolation;
                    Interpolation(3, "X - abscissas of points,  W - gravimetric coefficients, \n F  -  values of functions in these points.",
                        "Point of interpolation", ParamInput1, true, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                //*** Statistics ***
                case "Correlation Pearson":
                    InpuParamMethod("Statistics", "CorrelationPearson");
                    sourceCode.Text = AllMethodText.Statistics_CorrelationPearson;
                    Interpolation(2, "X  -   sample 1,  F  -   sample 2.", "No parameter",
                         "", false, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Correlation Spearmans Rank":
                    InpuParamMethod("Statistics", "CorrelationSpearmansRank");
                    sourceCode.Text = AllMethodText.Statistics_CorrelationSpearmansRank;
                    Interpolation(2, "X  -   sample 1,  F  -   sample 2.", "No parameter",
                         "", false, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Descriptive Statistics A Dev":
                    InpuParamMethod("Statistics", "DescriptiveStatisticsADev");
                    sourceCode.Text = AllMethodText.Statistics_DescriptiveStatisticsADev;
                    Interpolation(1, "X  -   sample 1.", "No parameter",
                         "", false, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Descriptive Statistics Median":
                    InpuParamMethod("Statistics", "DescriptiveStatisticsMedian");
                    sourceCode.Text = AllMethodText.Statistics_DescriptiveStatisticsMedian;
                    Interpolation(1, "X  -   sample 1.", "No parameter",
                         "", false, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Descriptive Statistics Moments":
                    InpuParamMethod("Statistics", "DescriptiveStatisticsMoments");
                    sourceCode.Text = AllMethodText.Statistics_DescriptiveStatisticsMoments;
                    Interpolation(1, "X  -   sample 1.", "No parameter",
                         "", false, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Descriptive Statistics Percentile":
                    InpuParamMethod("Statistics", "DescriptiveStatisticsPercentile");
                    sourceCode.Text = AllMethodText.Statistics_DescriptiveStatisticsPercentile;
                    Interpolation(1, "X  -   sample 1.", "Percentile (0<=P<=1):",
                         "0,4", true, "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Method 1":
                    InpuParamMethod("RandomGenerator", "Method1");
                    sourceCode.Text = AllMethodText.RandomGeneratorsMethod1;
                    RandomGenerator(0, "\n\nGenerator of the evenly distributed random material numbers  in a range [0, 1]",
                        "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Method 2":
                    InpuParamMethod("RandomGenerator", "Method2");
                    sourceCode.Text = AllMethodText.RandomGeneratorsMethod2;
                    RandomGenerator(1, "\n    Generator of the evenly distributed random \n         material numbers  in a range [0, N]",
                       "Input range N:", ParamInput1, true);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Method 3":
                    InpuParamMethod("RandomGenerator", "Method3");
                    sourceCode.Text = AllMethodText.RandomGeneratorsMethod3;
                    RandomGenerator(0, "                 Generator of the normally distributed random numbers." +
                "\n   Generates two independent random   numbers,having standard distributing. " +
                "\n   On the expenses of time equal to podprogramme of RndNormal,generating " +
                "\n                                       one random number.",
                        "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Method 4":
                    InpuParamMethod("RandomGenerator", "Method4");
                    sourceCode.Text = AllMethodText.RandomGeneratorsMethod4;
                    //sourceCode.Text = AllMethodText.RandomGeneratorsMethod3;
                    // sourceCode.Text = AllMethodText.RandomGeneratorsMethod1;
                    RandomGenerator(0, "                      Generator of the normally distributed random numbers." +
                "\n              Generates one random  number, having  the standard  distributing. " +
                "\n               On the expenses of time equal to podprogramme of RndNormal2, to " +
                "\n                                        generating two random numbers",
                        "", "", false);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Method 5":
                    InpuParamMethod("RandomGenerator", "Method5");
                    sourceCode.Text = AllMethodText.RandomGeneratorsMethod5;
                    RandomGenerator(1, "\n                Generator of the exponentially \n                distributed random numbers.",
                      "Input lambda N:", ParamInput1, true);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                //*** Matrix Algebra ***
                case "Matrix Determinant":
                    InpuMatrix("MatrixAlgebra", "MatrixDeterminant");
                    // InpuParamMethod("MatrixAlgebra", "MatrixDeterminant");
                    sourceCode.Text = AllMethodText.MatrixAlgebra_MatrixDeterminan;
                    MatrixDesign();
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Decomposition of matrix LU":
                    InpuMatrix("MatrixAlgebra", "DecompositionOfMatrixLU");
                    sourceCode.Text = AllMethodText.MatrixAlgebra_RMatrixLU;
                    MatrixDesign();
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Matrix Inverse LU":
                    InpuMatrix("MatrixAlgebra", "MatrixInverseLU");
                    sourceCode.Text = AllMethodText.MatrixAlgebra_RMatrixLuInverse;
                    MatrixDesign();
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                //*** Linear Systems ***
                case "Gaus":
                    InpuLinearSystems("LinearSystems", "Gaus");
                    sourceCode.Text = AllMethodText.SystemLinearEqualizations_Gaus;
                    LinearSystemsDesign();
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/LinearSystems/allMethods.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
                case "Zeidel":
                    InpuLinearSystems("LinearSystems", "Zeidel");
                    sourceCode.Text = AllMethodText.SystemLinearEqualizations_Zeidel;
                    LinearSystemsDesign();
                    uri = new Uri(Environment.CurrentDirectory.ToString() + "/Data/LinearSystems/allMethods.jpg");
                    img.Source = new BitmapImage(uri);
                    FileNew = true;
                    FileSave = true;
                    FileSaveAs = true;
                    btnExecute.IsEnabled = true;
                    break;
            }

            if (FileSave == false)
            {
                itemSave.IsEnabled = false;
                btnSave.IsEnabled = false;
                itemSave.Foreground = Brushes.Gray;
            }
            else
            {
                itemSave.IsEnabled = true;
                btnSave.IsEnabled = true;
                itemSave.Foreground = Brushes.Black;
            }
            if (FileSaveAs == false)
            {
                itemSaveAs.IsEnabled = false;
                itemSaveAs.Foreground = Brushes.Gray;
                btnSaveAs.IsEnabled = false;
            }
            else
            {
                btnSaveAs.IsEnabled = true;
                itemSaveAs.IsEnabled = true;
                itemSaveAs.Foreground = Brushes.Black;
            }
            if (FileNew == false)
            {
                itemNew.IsEnabled = false;
                btnNew.IsEnabled = false;
                itemNew.Foreground = Brushes.Gray;
            }
            else
            {
                btnNew.IsEnabled = true;
                itemNew.IsEnabled = true;
                itemNew.Foreground = Brushes.Black;
            }
        }

        void LinearSystemsDesign()
        {
            int NumberRows = range;
            int NumberCols = range * 2 + 1;
            DockPanel panelParam = new DockPanel();
            panelParam.LastChildFill = false;
            boxParameters.Content = panelParam;

            Canvas panelTestFunc = new Canvas();
            panelParam.Children.Add(panelTestFunc);
            DockPanel.SetDock(panelTestFunc, Dock.Left);
            panelTestFunc.Margin = new Thickness(5);
            panelTestFunc.Width = 500;
            panelTestFunc.Height = 110;
            panelTestFunc.Background = Brushes.Black;
            Label lblText = new Label();
            lblText.Content = "\n        Test System \n Linear Equalizations";
            lblText.Foreground = Brushes.White;
            lblText.HorizontalAlignment = HorizontalAlignment.Center;
            panelTestFunc.Children.Add(lblText);
            Canvas.SetTop(lblText, 20);
            Canvas.SetLeft(lblText, 20);


            // Створення обєкта btnEnterEquation
            Button btnEnterEquation = new Button();
            // btnExit.Command = comm[2];
            btnEnterEquation.Click += OnNewEnterEquationClick;
            // Створення обєкта Image как содержимого btnNew 
            Image img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Fill;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString() + "/images/EnterFunction.png"));
            btnEnterEquation.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            ToolTip tip = new ToolTip();
            tip.Content = "Change function";
            btnEnterEquation.ToolTip = tip;
            panelTestFunc.Children.Add(btnEnterEquation);
            Canvas.SetTop(btnEnterEquation, 5);
            Canvas.SetLeft(btnEnterEquation, 5);
            //------------------------
            DockPanel dockUniformGrid = new DockPanel();
            panelTestFunc.Children.Add(dockUniformGrid);
            Canvas.SetBottom(dockUniformGrid, 5);
            Canvas.SetRight(dockUniformGrid, 15);

            StackPanel stak = new StackPanel();
            ScrollViewer scroll = new ScrollViewer();
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            TextBox txtMatrix = new TextBox();
            txtMatrix.Width = 300;
            txtMatrix.Height = 100;
            txtMatrix.IsReadOnly = true;
            txtMatrix.BorderBrush = Brushes.Gray;
            txtMatrix.BorderThickness = new Thickness(0);
            stak.Children.Add(txtMatrix);
            scroll.Content = stak;
            dockUniformGrid.Children.Add(scroll);

            int x = 0, y = 0;
            for (int i = 0; i < NumberRows; i++)
            {
                for (int j = 1; j <= NumberCols; j++)
                {
                    if (j % 2 == 1)
                    {
                        if (NumberCols == j)
                        {
                            txtMatrix.Text = txtMatrix.Text + LinSysMasA[x].ToString() + "\n";
                            x++;
                            y = 0;
                        }
                        else
                            txtMatrix.Text = txtMatrix.Text + LinSysMatrixB[x, y].ToString() + "";
                    }
                    else
                    {
                        y++;
                        if (NumberCols == j + 1)

                            txtMatrix.Text = txtMatrix.Text + "*x" + y + "=";
                        else
                            txtMatrix.Text = txtMatrix.Text + "*x" + y + "+";
                    }
                }
            }
            //------------------------------

            //***To execute calculations***
            Canvas panelExecute = new Canvas();
            panelParam.Children.Add(panelExecute);
            DockPanel.SetDock(panelTestFunc, Dock.Left);
            panelExecute.Margin = new Thickness(5);
            panelExecute.Width = 240;
            panelExecute.Height = 110;
            panelExecute.Background = Brushes.Black;
            Label lblTextExe = new Label();
            panelExecute.Children.Add(lblTextExe);
            lblTextExe.Content = "To execute calculations";
            lblTextExe.Foreground = Brushes.White;
            lblTextExe.HorizontalAlignment = HorizontalAlignment.Center;
            Button btnExecute = new Button();
            // CommandBindings.Add(new CommandBinding(btnExecute, OnExecuteButton));
            btnExecute.Click += OnExecuteButton;
            btnExecute.Background = Brushes.White;
            btnExecute.Width = 50;
            panelExecute.Children.Add(btnExecute);
            btnExecute.Content = "Go";
            Canvas.SetTop(lblTextExe, 10);
            Canvas.SetLeft(lblTextExe, 50);
            Canvas.SetBottom(btnExecute, 30);
            Canvas.SetLeft(btnExecute, 90);

            txtboxResult = new TextBox();
            txtboxResult.Text = "";
            boxResult.Content = txtboxResult;
        }
        void MatrixDesign()
        {
            int NumberCols = rangeMatrix;

            DockPanel panelParam = new DockPanel();
            panelParam.LastChildFill = false;
            boxParameters.Content = panelParam;
            Canvas panelTestFunc = new Canvas();
            panelParam.Children.Add(panelTestFunc);
            DockPanel.SetDock(panelTestFunc, Dock.Left);
            panelTestFunc.Margin = new Thickness(5);
            panelTestFunc.Width = 300;
            panelTestFunc.Height = 110;
            panelTestFunc.Background = Brushes.Black;
            Label lblText = new Label();
            lblText.Content = "Test matrix:";
            lblText.Foreground = Brushes.White;
            lblText.HorizontalAlignment = HorizontalAlignment.Center;
            panelTestFunc.Children.Add(lblText);
            Canvas.SetTop(lblText, 40);
            Canvas.SetLeft(lblText, 10);

            // Створення обєкта btnEnterEquation
            Button btnEnterMatrix = new Button();
            // btnExit.Command = comm[2];
            btnEnterMatrix.Click += OnNewEnterMatrixClick;
            // Створення обєкта Image как содержимого btnNew 
            Image img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Fill;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString() + "/images/EnterFunction.png"));
            btnEnterMatrix.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            ToolTip tip = new ToolTip();
            tip.Content = "Change matrix";
            btnEnterMatrix.ToolTip = tip;
            panelTestFunc.Children.Add(btnEnterMatrix);
            Canvas.SetTop(btnEnterMatrix, 5);
            Canvas.SetLeft(btnEnterMatrix, 5);
            //------------------------




            DockPanel dockUniformGrid = new DockPanel();

            panelTestFunc.Children.Add(dockUniformGrid);
            Canvas.SetTop(dockUniformGrid, 5);
            Canvas.SetLeft(dockUniformGrid, 90);

            // Создание декоративного объекта Border 
            Border bord = new Border();
            bord.HorizontalAlignment = HorizontalAlignment.Center;
            bord.BorderBrush = SystemColors.ControlDarkDarkBrush;
            bord.BorderThickness = new Thickness(1);
            dockUniformGrid.Children.Add(bord);

            StackPanel stak = new StackPanel();
            ScrollViewer scroll = new ScrollViewer();
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            TextBox txtMatrix = new TextBox();
            txtMatrix.Width = 180;
            txtMatrix.Height = 100;
            txtMatrix.IsReadOnly = true;
            txtMatrix.BorderBrush = Brushes.Gray;
            txtMatrix.BorderThickness = new Thickness(0);
            stak.Children.Add(txtMatrix);
            scroll.Content = stak;
            bord.Child = scroll;
            // dockUniformGrid.Children.Add(scroll);

            txtMatrix.Text = "";
            for (int i = 0; i < NumberCols-1; i++)
            {
                for (int j = 0; j < NumberCols-1; j++)
                {
                    txtMatrix.Text = txtMatrix.Text + "  " + MatrixInit[i, j].ToString() + "  ";
                }
                txtMatrix.Text = txtMatrix.Text + "  \n";
            }


            //***maine rectangle***
            Canvas panelParamFunc = new Canvas();
            panelParam.Children.Add(panelParamFunc);
            DockPanel.SetDock(panelParamFunc, Dock.Left);
            panelParamFunc.Width = 180;
            panelParamFunc.Height = 110;
            panelParamFunc.Background = Brushes.Black;

            Grid gridData = new Grid();
            gridData.Margin = new Thickness(3);
            //gridData.ShowGridLines = true;
            panelParamFunc.Children.Add(gridData);

            RowDefinition rowdefDate = new RowDefinition();
            rowdefDate.Height = new GridLength(1, GridUnitType.Star);
            gridData.RowDefinitions.Add(rowdefDate);
            rowdefDate = new RowDefinition();
            rowdefDate.Height = new GridLength(1, GridUnitType.Star);
            gridData.RowDefinitions.Add(rowdefDate);

            ColumnDefinition coldefDate = new ColumnDefinition();
            // coldefDate.Width = new GridLength(180, GridUnitType.Pixel);
            gridData.ColumnDefinitions.Add(coldefDate);
            coldefDate = new ColumnDefinition();
            gridData.ColumnDefinitions.Add(coldefDate);

            //***(0,0)***
            Label lblNumberColumns = new Label();
            lblNumberColumns.Foreground = Brushes.White;
            lblNumberColumns.Content = "Columns & Rows:";
            gridData.Children.Add(lblNumberColumns);
            Grid.SetRow(lblNumberColumns, 0);
            Grid.SetColumn(lblNumberColumns, 0);

            //***(0,1)***
            txtNumber = new TextBox();
            txtNumber.HorizontalAlignment = HorizontalAlignment.Left;
            txtNumber.Width = 50;
            txtNumber.Height = 20;
            txtNumber.IsEnabled = false;
            txtNumber.Text = (NumberCols - 1).ToString();
            // txtNumber.IsEnabled = Number_IsEnabled;

            gridData.Children.Add(txtNumber);
            Grid.SetRow(txtNumber, 0);
            Grid.SetColumn(txtNumber, 1);


            //***(1,0)***
            /*  Label lblNumberRows = new Label();
              lblNumberRows.Foreground = Brushes.White;
              lblNumberRows.Content = NumberRows_Content;
              gridData.Children.Add(lblNumberRows);
              Grid.SetRow(lblNumberRows, 1);
              Grid.SetColumn(lblNumberRows, 0);

              //***(1,1)***
              txtNumberRows = new TextBox();
              txtNumberRows.HorizontalAlignment = HorizontalAlignment.Left;
              txtNumberRows.Text = NumberRows.ToString();
              //txtPercentile.IsEnabled = Percentile_IsEnabled;
              txtNumberRows.Width = 50;
              txtNumberRows.Height = 20;
              /// if (Percentile_IsEnabled)
              // {
              gridData.Children.Add(txtNumberRows);
              Grid.SetRow(txtNumberRows, 1);
              Grid.SetColumn(txtNumberRows, 1);*/
            ///}


            //***To execute calculations***
            Canvas panelExecute = new Canvas();
            panelParam.Children.Add(panelExecute);
            DockPanel.SetDock(panelTestFunc, Dock.Left);
            panelExecute.Margin = new Thickness(5);
            panelExecute.Width = 240;
            panelExecute.Height = 110;
            panelExecute.Background = Brushes.Black;
            Label lblTextExe = new Label();
            panelExecute.Children.Add(lblTextExe);
            lblTextExe.Content = "To execute calculations";
            lblTextExe.Foreground = Brushes.White;
            lblTextExe.HorizontalAlignment = HorizontalAlignment.Center;
            Button btnExecute = new Button();
            // CommandBindings.Add(new CommandBinding(btnExecute, OnExecuteButton));
            btnExecute.Click += OnExecuteButton;
            btnExecute.Background = Brushes.White;
            btnExecute.Width = 50;
            panelExecute.Children.Add(btnExecute);
            btnExecute.Content = "Go";
            Canvas.SetTop(lblTextExe, 10);
            Canvas.SetLeft(lblTextExe, 50);
            Canvas.SetBottom(btnExecute, 30);
            Canvas.SetLeft(btnExecute, 90);

            txtboxResult = new TextBox();
            txtboxResult.Text = "";
            boxResult.Content = txtboxResult;
        }

        void InputParametrs(string text_Text, string lblBegin_Content, string lblEnd_Content, string lblInit_Content,
            string lblPoints_Content, string txtBegin_Text, bool txtBegin_IsEnabled, string txtEnd_Text, bool txtEnd_IsEnabled,
            string txtInit_Text, bool txtInit_IsEnabled, string txtPoints_Text, bool txtPoints_IsEnabled)
        {
            DockPanel panelParam = new DockPanel();
            panelParam.LastChildFill = false;
            boxParameters.Content = panelParam;

            Canvas panelTestFunc = new Canvas();
            panelParam.Children.Add(panelTestFunc);
            DockPanel.SetDock(panelTestFunc, Dock.Left);
            panelTestFunc.Margin = new Thickness(5);
            panelTestFunc.Width = 240;
            panelTestFunc.Height = 110;
            panelTestFunc.Background = Brushes.Black;
            Label lblText = new Label();
            lblText.Content = "Test function";
            lblText.Foreground = Brushes.White;
            lblText.HorizontalAlignment = HorizontalAlignment.Center;
            panelTestFunc.Children.Add(lblText);
            Canvas.SetTop(lblText, 10);
            Canvas.SetLeft(lblText, 80);

            RoundedButton btnTestFun = new RoundedButton();
            btnTestFun.Click += OnNewFunctionClick;
            btnTestFun.Background = Brushes.White;
            btnTestFun.Width = panelTestFunc.Width - 10;
            panelTestFunc.Children.Add(btnTestFun);
            txtTextFunnction = new TextBlock();
            txtTextFunnction.Text = text_Text;
            btnTestFun.Child = txtTextFunnction;

            Canvas.SetBottom(btnTestFun, 30);
            Canvas.SetLeft(btnTestFun, 5);




            // Створення обєкта btnExecute
            Button btnEnterFunction = new Button();
            // btnExit.Command = comm[2];
            btnEnterFunction.Click += OnNewFunctionClick;
            // Створення обєкта Image как содержимого btnNew 
            Image img = new Image();
            img.Width = 25;
            img.Height = 25;
            img.Stretch = Stretch.Fill;
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString() + "/images/EnterFunction.png"));
            btnEnterFunction.Content = img;
            // Створення обєкта ToolTip на основе текста UlCommand 
            ToolTip tip = new ToolTip();
            tip.Content = "Change function";
            btnEnterFunction.ToolTip = tip;


            panelTestFunc.Children.Add(btnEnterFunction);
            Canvas.SetTop(btnEnterFunction, 5);
            Canvas.SetLeft(btnEnterFunction, 5);
            // panelTestFunc.Children.Add(text);

            //***maine rectangle***
            Canvas panelParamFunc = new Canvas();
            panelParam.Children.Add(panelParamFunc);
            DockPanel.SetDock(panelParamFunc, Dock.Left);
            panelParamFunc.Width = 240;
            panelParamFunc.Height = 110;
            panelParamFunc.Background = Brushes.Black;

            Grid gridData = new Grid();
            gridData.Margin = new Thickness(3);
            //gridData.ShowGridLines = true;
            panelParamFunc.Children.Add(gridData);

            RowDefinition rowdefDate = new RowDefinition();
            rowdefDate.Height = new GridLength(1, GridUnitType.Star);
            gridData.RowDefinitions.Add(rowdefDate);
            rowdefDate = new RowDefinition();
            rowdefDate.Height = new GridLength(1, GridUnitType.Star);
            gridData.RowDefinitions.Add(rowdefDate);

            rowdefDate = new RowDefinition();
            rowdefDate.Height = new GridLength(1, GridUnitType.Star);
            gridData.RowDefinitions.Add(rowdefDate);

            rowdefDate = new RowDefinition();
            rowdefDate.Height = new GridLength(1, GridUnitType.Star);
            gridData.RowDefinitions.Add(rowdefDate);

            ColumnDefinition coldefDate = new ColumnDefinition();
            coldefDate.Width = new GridLength(180, GridUnitType.Pixel);
            gridData.ColumnDefinitions.Add(coldefDate);
            coldefDate = new ColumnDefinition();
            gridData.ColumnDefinitions.Add(coldefDate);

            //***(0,0)***
            Label lblBegin = new Label();
            lblBegin.Foreground = Brushes.White;
            lblBegin.Content = lblBegin_Content;
            gridData.Children.Add(lblBegin);
            Grid.SetRow(lblBegin, 0);
            Grid.SetColumn(lblBegin, 0);

            //***(0,1)***
            txtBegin = new TextBox();
            txtBegin.HorizontalAlignment = HorizontalAlignment.Left;
            txtBegin.Width = 50;
            txtBegin.Height = 20;
            txtBegin.Text = txtBegin_Text;
            txtBegin.IsEnabled = txtBegin_IsEnabled;
            if (txtBegin_IsEnabled)
            {
                gridData.Children.Add(txtBegin);
                Grid.SetRow(txtBegin, 0);
                Grid.SetColumn(txtBegin, 1);
            }

            //***(1,0)***
            Label lblEnd = new Label();
            lblEnd.Foreground = Brushes.White;
            lblEnd.Content = lblEnd_Content;
            gridData.Children.Add(lblEnd);
            Grid.SetRow(lblEnd, 1);
            Grid.SetColumn(lblEnd, 0);

            //***(1,1)***
            txtEnd = new TextBox();
            txtEnd.HorizontalAlignment = HorizontalAlignment.Left;
            txtEnd.Text = txtEnd_Text;
            txtEnd.IsEnabled = txtEnd_IsEnabled;
            txtEnd.Width = 50;
            txtEnd.Height = 20;
            if (txtEnd_IsEnabled)
            {
                gridData.Children.Add(txtEnd);
                Grid.SetRow(txtEnd, 1);
                Grid.SetColumn(txtEnd, 1);
            }

            //***(2,0)***
            Label lblInit = new Label();
            lblInit.Foreground = Brushes.White;
            lblInit.Content = lblInit_Content;
            gridData.Children.Add(lblInit);
            Grid.SetRow(lblInit, 2);
            Grid.SetColumn(lblInit, 0);

            //***(2,1)***
            txtInit = new TextBox();
            txtInit.HorizontalAlignment = HorizontalAlignment.Left;
            txtInit.Text = txtInit_Text;
            txtInit.IsEnabled = txtInit_IsEnabled;
            txtInit.Width = 50;
            txtInit.Height = 20;
            if (txtInit_IsEnabled)
            {
                gridData.Children.Add(txtInit);
                Grid.SetRow(txtInit, 2);
                Grid.SetColumn(txtInit, 1);
            }

            //***(3,0)***
            Label lblPoints = new Label();
            lblPoints.Foreground = Brushes.White;
            lblPoints.Content = lblPoints_Content;
            gridData.Children.Add(lblPoints);
            Grid.SetRow(lblPoints, 3);
            Grid.SetColumn(lblPoints, 0);

            //***(3,1)***
            txtPoints = new TextBox();
            txtPoints.HorizontalAlignment = HorizontalAlignment.Left;
            txtPoints.Text = txtPoints_Text;
            txtPoints.IsEnabled = txtPoints_IsEnabled;
            txtPoints.Width = 50;
            txtPoints.Height = 20;
            if (txtPoints_IsEnabled)
            {
                gridData.Children.Add(txtPoints);
                Grid.SetRow(txtPoints, 3);
                Grid.SetColumn(txtPoints, 1);
            }

            //***To execute calculations***
            Canvas panelExecute = new Canvas();
            panelParam.Children.Add(panelExecute);
            DockPanel.SetDock(panelTestFunc, Dock.Left);
            panelExecute.Margin = new Thickness(5);
            panelExecute.Width = 240;
            panelExecute.Height = 110;
            panelExecute.Background = Brushes.Black;
            Label lblTextExe = new Label();
            panelExecute.Children.Add(lblTextExe);
            lblTextExe.Content = "To execute calculations";
            lblTextExe.Foreground = Brushes.White;
            lblTextExe.HorizontalAlignment = HorizontalAlignment.Center;
            // Button btnExecute = new Button();
            RoundedButton btnExecute = new RoundedButton();
            // CommandBindings.Add(new CommandBinding(btnExecute, OnExecuteButton));
            btnExecute.Click += OnExecuteButton;
            btnExecute.Background = Brushes.White;
            btnExecute.Width = 50;
            panelExecute.Children.Add(btnExecute);
            // btnExecute.Content = "Go";
            TextBlock txt = new TextBlock();
            txt.Text = "Go";
            btnExecute.Child = txt;

            Canvas.SetTop(lblTextExe, 10);
            Canvas.SetLeft(lblTextExe, 50);
            Canvas.SetBottom(btnExecute, 30);
            Canvas.SetLeft(btnExecute, 90);

            txtboxResult = new TextBox();
            txtboxResult.Margin = new Thickness(5);
            txtboxResult.BorderThickness = new Thickness(0);
            txtboxResult.Text = "";
            boxResult.Content = txtboxResult;
        }

        void Interpolation(int type, string lblText_Content, string lblNumber_Content, string Number_Text, bool Number_IsEnabled,
            string Percentile_Content, string Percentile_Text, bool Percentile_IsEnabled)
        {
            DockPanel panelParam = new DockPanel();
            panelParam.LastChildFill = false;
            boxParameters.Content = panelParam;

            Canvas panelTestFunc = new Canvas();
            panelParam.Children.Add(panelTestFunc);
            DockPanel.SetDock(panelTestFunc, Dock.Left);
            panelTestFunc.Margin = new Thickness(5);
            panelTestFunc.Width = 300;
            panelTestFunc.Height = 110;
            panelTestFunc.Background = Brushes.Black;
            Label lblText = new Label();
            lblText.Content = lblText_Content;
            lblText.Foreground = Brushes.White;
            lblText.HorizontalAlignment = HorizontalAlignment.Center;
            panelTestFunc.Children.Add(lblText);

            Canvas.SetTop(lblText, 3);
            Canvas.SetLeft(lblText, 10);



            //------------------------
            int NumberCols = 7;
            int NumberRows = type + 1;


            initUniformGrid = new string[] { "1,5", "1,55", "1,6", "1,65", "1,7", "1,75",
                "-1,1", "-0,9", "-0,7", "-0,4", "-0,2", "0,1" };
            initUniformGrid2 = new string[] { "1,5", "1,55", "1,6", "1,65", "1,7", "1,75", 
                "-1,1", "-0,9", "-0,7", "-0,4", "-0,2", "0,1",
            "50","20","90","11","10","15"};
            UniformGrid unigrid;

            DockPanel dockUniformGrid = new DockPanel();
            //  dockUniformGrid.Width = panelTestFunc.Width;

            panelTestFunc.Children.Add(dockUniformGrid);
            Canvas.SetBottom(dockUniformGrid, 5);
            Canvas.SetLeft(dockUniformGrid, 15);
            /*ScrollViewer scroll = new ScrollViewer();
           scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            dockUniformGrid.Children.Add(scroll);*/

            // Создание декоративного объекта Border 
            Border bord = new Border();
            bord.HorizontalAlignment = HorizontalAlignment.Center;
            bord.BorderBrush = SystemColors.ControlDarkDarkBrush;
            bord.BorderThickness = new Thickness(1);
            dockUniformGrid.Children.Add(bord);
            // Создание UniformGrid как дочернего объекта Border 
            unigrid = new UniformGrid();
            unigrid.Rows = NumberRows;
            unigrid.Columns = NumberCols;
            bord.Child = unigrid;
            // Создание объектов Tile для заполнения 
            // всех ячеек, кроме одной 
            for (int i = 0; i < NumberCols; i++)
            {
                if (i == 0)
                {
                    Button btnUn = new Button();
                    btnUn.Height = 19;
                    btnUn.Width = 35;
                    btnUn.Content = "";
                    unigrid.Children.Add(btnUn);
                }
                else
                {
                    Button btnUn = new Button();
                    btnUn.Height = 19;
                    btnUn.Width = 35;
                    btnUn.Content = (i).ToString();
                    unigrid.Children.Add(btnUn);
                }
            }
            int s = 0;
            for (int i = NumberCols; i < NumberRows * NumberCols; i++)
            {
                if (i == NumberCols)
                {
                    Button btnUn = new Button();
                    btnUn.Height = 19;
                    btnUn.Width = 35;
                    btnUn.Content = "X";
                    unigrid.Children.Add(btnUn);
                }
                else
                {
                    if (type == 1)
                    {
                        TextBox textUn = new TextBox();
                        textUn.Height = 19;
                        textUn.Width = 35;
                        textUn.Text = initUniformGrid[s];
                        textUn.Name = "TextBox" + s;
                        textUn.TextChanged += TextBoxOnTextChanged;
                        s++;
                        unigrid.Children.Add(textUn);
                    }
                    if (type == 2)
                    {
                        if (i == 2 * NumberCols)
                        {
                            Button btnUn = new Button();
                            btnUn.Height = 19;
                            btnUn.Width = 35;
                            btnUn.Content = "F";
                            unigrid.Children.Add(btnUn);
                        }
                        else
                        {
                            TextBox textUn = new TextBox();
                            textUn.Height = 19;
                            textUn.Width = 35;
                            textUn.Text = initUniformGrid[s];
                            textUn.Name = "TextBox" + s;
                            textUn.TextChanged += TextBoxOnTextChanged;
                            s++;
                            unigrid.Children.Add(textUn);
                        }
                    }
                    if (type == 3)
                    {
                        if (i == 2 * NumberCols)
                        {
                            Button btnUn = new Button();
                            btnUn.Height = 19;
                            btnUn.Width = 35;
                            btnUn.Content = "F";
                            unigrid.Children.Add(btnUn);
                        }
                        else
                        {
                            if (i == 3 * NumberCols)
                            {
                                Button btnUn = new Button();
                                btnUn.Height = 19;
                                btnUn.Width = 35;
                                btnUn.Content = "W";
                                unigrid.Children.Add(btnUn);
                            }
                            else
                            {
                                TextBox textUn = new TextBox();
                                textUn.Height = 19;
                                textUn.Width = 35;
                                textUn.Text = initUniformGrid2[s];
                                textUn.Name = "TextBox" + s;
                                textUn.TextChanged += TextBoxOnTextChanged;
                                s++;
                                unigrid.Children.Add(textUn);
                            }
                        }
                    }
                }

            }
            //  dockUniformGrid.VerticalAlignment = VerticalAlignment.Center;
            //------------------------------

            //***maine rectangle***
            Canvas panelParamFunc = new Canvas();
            panelParam.Children.Add(panelParamFunc);
            DockPanel.SetDock(panelParamFunc, Dock.Left);
            panelParamFunc.Width = 180;
            panelParamFunc.Height = 110;
            panelParamFunc.Background = Brushes.Black;

            Grid gridData = new Grid();
            gridData.Margin = new Thickness(3);
            //gridData.ShowGridLines = true;
            panelParamFunc.Children.Add(gridData);

            RowDefinition rowdefDate = new RowDefinition();
            rowdefDate.Height = new GridLength(1, GridUnitType.Star);
            gridData.RowDefinitions.Add(rowdefDate);
            rowdefDate = new RowDefinition();
            rowdefDate.Height = new GridLength(1, GridUnitType.Star);
            gridData.RowDefinitions.Add(rowdefDate);

            ColumnDefinition coldefDate = new ColumnDefinition();
            // coldefDate.Width = new GridLength(180, GridUnitType.Pixel);
            gridData.ColumnDefinitions.Add(coldefDate);
            coldefDate = new ColumnDefinition();
            gridData.ColumnDefinitions.Add(coldefDate);

            //***(0,0)***
            Label lblNumber = new Label();
            lblNumber.Foreground = Brushes.White;
            lblNumber.Content = lblNumber_Content;
            gridData.Children.Add(lblNumber);
            Grid.SetRow(lblNumber, 0);
            Grid.SetColumn(lblNumber, 0);

            //***(0,1)***
            txtNumber = new TextBox();
            txtNumber.HorizontalAlignment = HorizontalAlignment.Left;
            txtNumber.Width = 50;
            txtNumber.Height = 20;
            txtNumber.Text = Number_Text;
            txtNumber.IsEnabled = Number_IsEnabled;
            if (Number_IsEnabled)
            {
                gridData.Children.Add(txtNumber);
                Grid.SetRow(txtNumber, 0);
                Grid.SetColumn(txtNumber, 1);
            }

            //***(1,0)***
            Label lblPercentile = new Label();
            lblPercentile.Foreground = Brushes.White;
            lblPercentile.Content = Percentile_Content;
            gridData.Children.Add(lblPercentile);
            Grid.SetRow(lblPercentile, 1);
            Grid.SetColumn(lblPercentile, 0);

            //***(1,1)***
            txtPercentile = new TextBox();
            txtPercentile.HorizontalAlignment = HorizontalAlignment.Left;
            txtPercentile.Text = Percentile_Text;
            txtPercentile.IsEnabled = Percentile_IsEnabled;
            txtPercentile.Width = 50;
            txtPercentile.Height = 20;
            if (Percentile_IsEnabled)
            {
                gridData.Children.Add(txtPercentile);
                Grid.SetRow(txtPercentile, 1);
                Grid.SetColumn(txtPercentile, 1);
            }

            //***To execute calculations***
            Canvas panelExecute = new Canvas();
            panelParam.Children.Add(panelExecute);
            DockPanel.SetDock(panelTestFunc, Dock.Left);
            panelExecute.Margin = new Thickness(5);
            panelExecute.Width = 240;
            panelExecute.Height = 110;
            panelExecute.Background = Brushes.Black;
            Label lblTextExe = new Label();
            panelExecute.Children.Add(lblTextExe);
            lblTextExe.Content = "To execute calculations";
            lblTextExe.Foreground = Brushes.White;
            lblTextExe.HorizontalAlignment = HorizontalAlignment.Center;
            RoundedButton btnExecute = new RoundedButton();
            // CommandBindings.Add(new CommandBinding(btnExecute, OnExecuteButton));
            btnExecute.Click += OnExecuteButton;
            btnExecute.Background = Brushes.White;
            btnExecute.Width = 50;
            panelExecute.Children.Add(btnExecute);
            TextBlock txt = new TextBlock();
            txt.Text = "Go";
            btnExecute.Child = txt;
            Canvas.SetTop(lblTextExe, 10);
            Canvas.SetLeft(lblTextExe, 50);
            Canvas.SetBottom(btnExecute, 30);
            Canvas.SetLeft(btnExecute, 90);

            txtboxResult = new TextBox();
            txtboxResult.Text = "";
            boxResult.Content = txtboxResult;
        }

        void RandomGenerator(int type, string lblText_Content, string lblNumber_Content, string Number_Text, bool Number_IsEnabled)
        {
            DockPanel panelParam = new DockPanel();
            panelParam.LastChildFill = false;
            boxParameters.Content = panelParam;

            Canvas panelTestFunc = new Canvas();
            panelParam.Children.Add(panelTestFunc);
            DockPanel.SetDock(panelTestFunc, Dock.Left);
            panelTestFunc.Margin = new Thickness(5);
            if (type == 0)
                panelTestFunc.Width = 470;
            else
                panelTestFunc.Width = 300;
            panelTestFunc.Height = 110;
            panelTestFunc.Background = Brushes.Black;
            Label lblText = new Label();
            lblText.Content = lblText_Content;
            lblText.Foreground = Brushes.White;
            lblText.HorizontalAlignment = HorizontalAlignment.Center;
            panelTestFunc.Children.Add(lblText);

            Canvas.SetTop(lblText, 3);
            Canvas.SetLeft(lblText, 10);


            if (type == 1)
            {

                //***maine rectangle***
                Canvas panelParamFunc = new Canvas();
                panelParam.Children.Add(panelParamFunc);
                DockPanel.SetDock(panelParamFunc, Dock.Left);
                panelParamFunc.Width = 180;
                panelParamFunc.Height = 110;
                panelParamFunc.Background = Brushes.Black;

                Grid gridData = new Grid();
                gridData.Margin = new Thickness(3);
                panelParamFunc.Children.Add(gridData);

                RowDefinition rowdefDate = new RowDefinition();
                rowdefDate.Height = new GridLength(1, GridUnitType.Star);
                gridData.RowDefinitions.Add(rowdefDate);
                rowdefDate = new RowDefinition();
                rowdefDate.Height = new GridLength(1, GridUnitType.Star);
                gridData.RowDefinitions.Add(rowdefDate);

                ColumnDefinition coldefDate = new ColumnDefinition();
                // coldefDate.Width = new GridLength(180, GridUnitType.Pixel);
                gridData.ColumnDefinitions.Add(coldefDate);
                coldefDate = new ColumnDefinition();
                gridData.ColumnDefinitions.Add(coldefDate);

                //***(0,0)***
                Label lblNumber = new Label();
                lblNumber.Foreground = Brushes.White;
                lblNumber.Content = lblNumber_Content;
                gridData.Children.Add(lblNumber);
                Grid.SetRow(lblNumber, 0);
                Grid.SetColumn(lblNumber, 0);

                //***(0,1)***
                txtNumber = new TextBox();
                txtNumber.HorizontalAlignment = HorizontalAlignment.Left;
                txtNumber.Width = 50;
                txtNumber.Height = 20;
                txtNumber.Text = Number_Text;
                txtNumber.IsEnabled = Number_IsEnabled;
                if (Number_IsEnabled)
                {
                    gridData.Children.Add(txtNumber);
                    Grid.SetRow(txtNumber, 0);
                    Grid.SetColumn(txtNumber, 1);
                }
            }

            //***To execute calculations***
            Canvas panelExecute = new Canvas();
            panelParam.Children.Add(panelExecute);
            DockPanel.SetDock(panelTestFunc, Dock.Left);
            panelExecute.Margin = new Thickness(5);
            panelExecute.Width = 240;
            panelExecute.Height = 110;
            panelExecute.Background = Brushes.Black;
            Label lblTextExe = new Label();
            panelExecute.Children.Add(lblTextExe);
            lblTextExe.Content = "To execute calculations";
            lblTextExe.Foreground = Brushes.White;
            lblTextExe.HorizontalAlignment = HorizontalAlignment.Center;
            Button btnExecute = new Button();
            // CommandBindings.Add(new CommandBinding(btnExecute, OnExecuteButton));
            btnExecute.Click += OnExecuteButton;
            btnExecute.Background = Brushes.White;
            btnExecute.Width = 50;
            panelExecute.Children.Add(btnExecute);
            btnExecute.Content = "Go";
            Canvas.SetTop(lblTextExe, 10);
            Canvas.SetLeft(lblTextExe, 50);
            Canvas.SetBottom(btnExecute, 30);
            Canvas.SetLeft(btnExecute, 90);

            txtboxResult = new TextBox();
            txtboxResult.Text = "";
            boxResult.Content = txtboxResult;
        }

        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            TextBox txt = args.Source as TextBox;
            // MessageBox.Show(txt.Name + " has been clicked", Title);
            switch (txt.Name)
            {
                case "TextBox0":
                    initUniformGrid[0] = txt.Text;
                    initUniformGrid2[0] = txt.Text;
                    break;
                case "TextBox1":
                    initUniformGrid[1] = txt.Text;
                    initUniformGrid2[1] = txt.Text;
                    break;
                case "TextBox2":
                    initUniformGrid[2] = txt.Text;
                    initUniformGrid2[2] = txt.Text;
                    break;
                case "TextBox3":
                    initUniformGrid[3] = txt.Text;
                    initUniformGrid2[3] = txt.Text;
                    break;
                case "TextBox4":
                    initUniformGrid[4] = txt.Text;
                    initUniformGrid2[4] = txt.Text;
                    break;
                case "TextBox5":
                    initUniformGrid[5] = txt.Text;
                    initUniformGrid2[5] = txt.Text;
                    break;
                case "TextBox6":
                    initUniformGrid[6] = txt.Text;
                    break;
                case "TextBox7":
                    initUniformGrid[7] = txt.Text;
                    initUniformGrid2[7] = txt.Text;
                    break;
                case "TextBox8":
                    initUniformGrid[8] = txt.Text;
                    initUniformGrid2[8] = txt.Text;
                    break;
                case "TextBox9":
                    initUniformGrid[9] = txt.Text;
                    initUniformGrid2[9] = txt.Text;
                    break;
                case "TextBox10":
                    initUniformGrid[10] = txt.Text;
                    initUniformGrid2[10] = txt.Text;
                    break;
                case "TextBox11":
                    initUniformGrid[11] = txt.Text;
                    initUniformGrid2[11] = txt.Text;
                    break;
                case "TextBox12":
                    initUniformGrid2[12] = txt.Text;
                    break;
                case "TextBox13":
                    initUniformGrid2[13] = txt.Text;
                    break;
                case "TextBox14":
                    initUniformGrid2[14] = txt.Text;
                    break;
                case "TextBox15":
                    initUniformGrid2[15] = txt.Text;
                    break;
                case "TextBox16":
                    initUniformGrid2[16] = txt.Text;
                    break;
                case "TextBox17":
                    initUniformGrid2[17] = txt.Text;
                    break;

            }

        }

        void OnExecuteButton(object sender, RoutedEventArgs args)
        {
            /// RoutedUICommand comm = args.Command as RoutedUICommand;
            /// MessageBox.Show(comm.Name + " command not yet implemented", Title);
            // Получение выделенного узла 
            //  TreeViewMenuAdd item = argsItem.NewValue as TreeViewMenuAdd;
            // lblBig.Content = item.Text;
            // MessageBox.Show("<<" + item.Text + ">>" + " command not yet implemented", Title);
            txtboxResult.Text = "";
            switch (itemSelected.Text)
            {

                //*** Approximate decision of equalization f(x)=0 ***
                case "Bisection Method":
                    {
                        Bisection bisect = new Bisection(new FunctionOne(TestFunBisection), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text));
                        txtboxResult.Text = "\n Result of the program: \n" +
                            "    x= " + string.Format("{0:f" + precision + "}", bisect.GetSolution());
                    }
                    break;
                case "Chord Method":
                    {
                        Сhord chord = new Сhord(new FunctionOne(TestFunNewton), new FunctionOne(TestFunNewton2), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text), double.Parse(txtPoints.Text));
                        txtboxResult.Text = "\n Result of the program: \n" +
                            "   x= " + string.Format("{0:f" + precision + "}", chord.GetSolution());
                    }
                    break;
                case "Iteration Method":
                    {
                        IterationMethod itermet = new IterationMethod(new FunctionOne(TestFunIteration), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text), double.Parse(txtPoints.Text));
                        txtboxResult.Text = "\n Result of the program: \n" +
                            "   x= " + string.Format("{0:f" + precision + "}", itermet.GetSolution());
                    }
                    break;
                case "Newton Method":
                    {
                        Newton newton = new Newton(new FunctionOne(TestFunNewton), new FunctionOne(TestFunNewton2), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text), double.Parse(txtPoints.Text));
                        txtboxResult.Text = "\n Result of the program: \n" +
                            "   x= " + string.Format("{0:f" + precision + "}", newton.GetSolution());
                    }
                    break;
                //*** Differential Equations ***
                case "Euler Simple":
                    {
                        EulerSimple eulersimpl = new EulerSimple(new Function(TestFunDifferEquations), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text), int.Parse(txtPoints.Text));
                        var result_eulersimpl = eulersimpl.GetSolution();

                      //  boxResult.Content = txtboxResult;
                        //txtboxResult.Text = "";

                       /* string strXaml = "<Button xmlns='http://schemas.microsoft.com/" + 
                            "winfх/2006/xaml/presentation'" + 
                            " Foreground='LightSeaGreen' FontSize='24pt'>" + 
                            " Click me!" + 
                            "</Button>"; */
                        //StringReader strreader = new StringReader(strXaml); 
                        //XmlTextReader xmlreader = new XmlTextReader(strreader); 
                        //object obj = XamlReader.Load(xmlreader);
                       // boxResult.Content = obj; 

                       /* Canvas canvResult = new Canvas();
                        boxResult.Content = canvResult;
                        canvResult.Background = Brushes.Black;
                         Line lineY = new Line();
                         lineY.Stroke = Brushes.Green;
                         lineY.X1 = 20;
                         lineY.Y1 = 450;
                         lineY.X2 = 50;
                         lineY.Y2 = 90;
                         canvResult.Children.Add(lineY);*/


                        // Объект Polyline назначается содержимым окна 
                        Canvas canvResult = new Canvas();
                       // boxResult.Content = canvResult;
                      /*  Polyline poly = new Polyline(); 
                        poly.VerticalAlignment = VerticalAlignment.Center; 
                        poly.Stroke = SystemColors.WindowTextBrush; 
                        poly.StrokeThickness = 2;
                        boxResult.Content = poly; 
                        // Определение точек 
                        for (int ii = 0;ii < 2000; ii++) 
                                poly.Points.Add(new Point(ii, 96 * (1 - Math.Sin(ii * Math.PI / 192)))); 
                        */

                        // Canvas.SetBottom(lineY, 0);
                        // Canvas.SetLeft(lineY, 0);

                        // Polyline Pline = new Polyline();
                        // Pline.Stroke = Brushes.Red;
                        // Pline.StrokeThickness = 3;
                        // canvResult.Children.Add(Pline);
                        //Canvas.SetBottom(Pline, 100);
                        //Canvas.SetLeft(Pline, 5); 


                      //  Image MyImage = new Image();
                       // boxResult.Content = MyImage;
                        // подготавливаем элементы
                        // инициализируем группу
                        //DrawingGroup MyDrawingGroup = new DrawingGroup();
                        // добавляем в нее готовые элементы
                        //MyDrawingGroup.Children.Add(lineDrawing);
                        // рисуем группу в картинку
                        //DrawingImage MyDrawingImage = new DrawingImage(MyDrawingGroup);
                        // отображаем картикку в Image
                       // MyImage.Source = MyDrawingImage;

                       // double x1 = 0,y1=0;
                        ///double x2 = 0, y2 = 0;
                        Polyline poly = new Polyline();
                       // poly.VerticalAlignment = VerticalAlignment.Center;
                        poly.Stroke = SystemColors.WindowTextBrush;
                        poly.StrokeThickness = 5;
                        boxResult.Content = poly;
                        for (int j = 0; j < int.Parse(txtPoints.Text); j++)//int.Parse(txtPoints.Text)
                        {//
                           // txtboxResult.Text = txtboxResult.Text + string.Format("{0:f" + precision + "}", result_eulersimpl[0, j])
                            //    + " : "
                            //  + string.Format("{0:f" + precision + "}", result_eulersimpl[1, j]) + "\n";
                            //  Pline.Points.Add(new Point(result_eulersimpl[0, j] * 50, result_eulersimpl[1, j] * 50));
                         
                            //for (int ii = 0; ii < 2000; ii++)
                                poly.Points.Add(new Point(result_eulersimpl[0, j]*10, result_eulersimpl[1, j]*20)); 
                           /* txtboxResult.Text = txtboxResult.Text + string.Format("{0:f}", j)
                                 + " : "
                                + string.Format("{0:f}", j) + "\n";*/
                           // Pline.Points.Add(new Point(result_eulersimpl[0, j] * 10, result_eulersimpl[1, j] * 10));

                           // x1 = result_eulersimpl[0, j] * 10;
                            //y1 = result_eulersimpl[1, j] * 10;
                           // x2 = result_eulersimpl[0, j] * 10;
                           // y2 = result_eulersimpl[1, j] * 10;
                          //  GeometryDrawing lineDrawing = new GeometryDrawing(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
                          // new Pen(Brushes.Black, 10), new LineGeometry(new Point(x1, y1), new Point(x2, y2)));
                          //  MyDrawingGroup.Children.Add(lineDrawing);
                           
                            /*  Rectangle rect = new Rectangle();
                              rect.Fill = Brushes.Red;
                              rect.Stroke = Brushes.Blue;
                              rect.Width = result_eulersimpl[0, j] * 30;
                              rect.Height = result_eulersimpl[1, j] * 30;
                              canvResult.Children.Add(rect);
                              Canvas.SetBottom(rect, 0);
                              Canvas.SetLeft(rect, result_eulersimpl[0, j] * 30);*/
                            //      pre =result_eulersimpl[0, j ] * 30;
                        }
                       // MyImage.Source = MyDrawingImage;
                       // canvResult.Children.Add(MyImage);
                       // Canvas.SetBottom(MyImage, 0);
                       // Canvas.SetLeft(MyImage, 0); 

                    }
                    break;
                case "Euler Modified":
                    {
                        EulerModified eulerModif = new EulerModified(new Function(TestFunDifferEquations), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text), int.Parse(txtPoints.Text));
                        var result_eulerModif = eulerModif.GetSolution();
                        txtboxResult.Text = "";
                        for (int j = 0; j < int.Parse(txtPoints.Text); j++)
                        {
                            txtboxResult.Text = txtboxResult.Text + string.Format("{0:f" + precision + "}", result_eulerModif[0, j])
                                + " : " + string.Format("{0:f" + precision + "}", result_eulerModif[1, j]) + "\n";
                        }
                        // MessageBox.Show("<<Euler Modified>>" + " command not yet implemented", Title);
                    }
                    break;
                case "Euler Corrected":
                    {
                        EulerCorrected eulerCorrect = new EulerCorrected(new Function(TestFunDifferEquations), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text), int.Parse(txtPoints.Text));
                        var result_eulerCorrect = eulerCorrect.GetSolution();
                        txtboxResult.Text = "";
                        for (int j = 0; j < int.Parse(txtPoints.Text); j++)
                        {
                            txtboxResult.Text = txtboxResult.Text + string.Format("{0:f" + precision + "}", result_eulerCorrect[0, j])
                                + " : " + string.Format("{0:f" + precision + "}", result_eulerCorrect[1, j]) + "\n";
                        }
                        //MessageBox.Show("<<Euler Corrected>>" + " command not yet implemented", Title);
                    }
                    break;
                case "Runge-Kutta4":
                    {
                        RungeKutta4 rungeKutta4 = new RungeKutta4(new Function(TestFunDifferEquations), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text), int.Parse(txtPoints.Text));
                        var result_rungeKutta4 = rungeKutta4.GetSolution();
                        txtboxResult.Text = "";
                        //  txtboxResult.Text = txtboxResult.Text+ TestFunDifferEquations(0,0).ToString();
                        //txtboxResult.Text = "\n\n";
                        for (int j = 0; j < int.Parse(txtPoints.Text); j++)
                        {
                            txtboxResult.Text = txtboxResult.Text + string.Format("{0:f" + precision + "}", result_rungeKutta4[0, j])
                                + " : " + string.Format("{0:f" + precision + "}", result_rungeKutta4[1, j]) + "\n";
                        }
                        // MessageBox.Show("<<Runge-Kutta4>>" + " command not yet implemented", Title);
                        //txtboxResult.Text = TestFunDifferEquations(0, 0).ToString();
                    }
                    break;
                //*** Integration ***
                case "Chebishev":
                    Chebishev chebish = new Chebishev(new FunctionOne(TestFunInteger), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), int.Parse(txtInit.Text));
                    var result_chebish = chebish.GetSolution();

                    for (int j = 0; j <= int.Parse(txtInit.Text); j++)
                        txtboxResult.Text = txtboxResult.Text + "\n   h = " + string.Format("{0:f" + precision + "}", result_chebish[1, j]) + "   \t   integral = "
                            + string.Format("{0:f" + precision + "}", result_chebish[0, j]);
                    break;
                case "Simpson":
                    Simpson simps = new Simpson(new FunctionOne(TestFunInteger), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), int.Parse(txtInit.Text));
                    var result_simps = simps.GetSolution();
                    for (int j = 0; j <= int.Parse(txtInit.Text); j++)
                        txtboxResult.Text = txtboxResult.Text + "\n   h = " + string.Format("{0:f" + precision + "}", result_simps[1, j]) +
                             " \t   integral = " + string.Format("{0:f" + precision + "}", result_simps[0, j]);
                    break;
                case "Simpson 2":
                    Simpson2 simps2 = new Simpson2(new FunctionOne(TestFunInteger), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), int.Parse(txtInit.Text));
                    var result_simps2 = simps2.GetSolution();
                    txtboxResult.Text = "\n\n  integral = " + string.Format("{0:f" + precision + "}", result_simps2);
                    break;
                case "Trapezium":
                    Trapezium trapez = new Trapezium(new FunctionOne(TestFunInteger), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), int.Parse(txtInit.Text));
                    var result_trapez = trapez.GetSolution();
                    txtboxResult.Text = "";
                    for (int j = 0; j <= double.Parse(txtInit.Text); j++)
                        txtboxResult.Text = txtboxResult.Text + "\n   h = " + string.Format("{0:f" + precision + "}", result_trapez[1, j])
                            + " \t   integral = " + string.Format("{0:f" + precision + "}", result_trapez[0, j]);
                    break;
                //*** Non Linear equalization ***
                case "Half Division":
                    HalfDivision halfdiv = new HalfDivision(new FunctionOne(TestFunNonLinearEquations), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text));
                    txtboxResult.Text = "\n    X = " + string.Format("{0:f" + precision + "}", halfdiv.GetSolution()[0, 0])
                        + "       Iterations = " + string.Format("{0:f" + precision + "}", halfdiv.GetSolution()[1, 0]);
                    break;
                case "Hord Metod":
                    HordMetod hormet = new HordMetod(new FunctionOne(TestFunNonLinearEquations), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), int.Parse(txtInit.Text));
                    txtboxResult.Text = "\n    X = " + string.Format("{0:f" + precision + "}", hormet.GetSolution()[0, 0])
                        + "       Iterations = " + string.Format("{0:f" + precision + "}", hormet.GetSolution()[1, 0]);
                    break;

                case "Newton Metod":
                    NewtonMethod newt = new NewtonMethod(new FunctionOne(TestFunNonLinearEquations), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text));
                    txtboxResult.Text = "\n     X = " + string.Format("{0:f" + precision + "}", newt.GetSolution()[0, 0])
                        + "       Iterations = " + string.Format("{0:f" + precision + "}", newt.GetSolution()[1, 0]);
                    break;
                case "Secant Metod":
                    Secant sec = new Secant(new FunctionOne(TestFunNonLinearEquations), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text));
                    txtboxResult.Text = "\n     X = " + string.Format("{0:f" + precision + "}", sec.GetSolution()[0, 0])
                        + "       Iterations = " + string.Format("{0:f" + precision + "}", sec.GetSolution()[1, 0]);
                    break;
                //*** Optimizing***
                case "Brentopt":
                    Brentopt brent = new Brentopt(new FunctionOne(TestFunOptimizing), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text));
                    txtboxResult.Text = "\n    Point of the found minimum :";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    XMin = " + string.Format("{0:f" + precision + "}", brent.GetSolution());
                    txtboxResult.Text = txtboxResult.Text + "\n\n     A value of function is in the found minimum :";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    F(XMin) = " + string.Format("{0:f" + precision + "}", brent.GetSolutionFunction());
                    break;
                case "Golden Section":
                    GoldenSection godsection = new GoldenSection(new FunctionOne(TestFunOptimizing), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), int.Parse(txtInit.Text));
                    txtboxResult.Text = "\n    Scopes   of segment  which a decision of task is on .";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    a = " + string.Format("{0:f" + precision + "}", godsection.GetSolutionA());
                    txtboxResult.Text = txtboxResult.Text + "\n\n    b = " + string.Format("{0:f" + precision + "}", godsection.GetSolutionB());
                    break;
                case "Pijavsky":
                    Pijavsky pijavsky = new Pijavsky(new FunctionOne(TestFunOptimizing), double.Parse(txtBegin.Text), double.Parse(txtEnd.Text), double.Parse(txtInit.Text), int.Parse(txtPoints.Text));
                    txtboxResult.Text = "\n    Abscissa of the best point from found..";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    F = " + string.Format("{0:f" + precision + "}", pijavsky.GetSolution());
                    break;
                //*** Interpolation ***
                case "Lagrange Interpolator":
                    double[] XLagr = new double[6];
                    int i = 0;
                    for (i = 0; i < 6; i++)
                        XLagr[i] = double.Parse(initUniformGrid[i]);
                    double[] FLagr = new double[6];
                    for (int j = 0; j < 6; j++)
                        FLagr[j] = double.Parse(initUniformGrid[j + i]);

                    LagrangeInterpolator lagran = new LagrangeInterpolator(XLagr, FLagr, 6, double.Parse(txtNumber.Text));
                    txtboxResult.Text = "\n    A value interpolation polynomial is in the point of interpolation.";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    P = " + string.Format("{0:f" + precision + "}", lagran.GetSolution());
                    break;
                case "Newton Interpolator":
                    double[] XNew = new double[6];
                    for (i = 0; i < 6; i++)
                        XNew[i] = double.Parse(initUniformGrid[i]);
                    double[] FNew = new double[6];
                    for (int j = 0; j < 6; j++)
                        FNew[j] = double.Parse(initUniformGrid[j + i]);
                    NewtonInterpolator newinterpol = new NewtonInterpolator(XNew, FNew, 6, double.Parse(txtNumber.Text));
                    txtboxResult.Text = "\n    A value interpolation polynomial is in the point of interpolation.";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    P = " + string.Format("{0:f" + precision + "}", newinterpol.GetSolution());
                    break;
                case "Neville Interpolator":
                    double[] XNewil = new double[6];
                    for (i = 0; i < 6; i++)
                        XNewil[i] = double.Parse(initUniformGrid[i]);
                    double[] FNewil = new double[6];
                    for (int j = 0; j < 6; j++)
                        FNewil[j] = double.Parse(initUniformGrid[j + i]);
                    NevilleInterpolator newill = new NevilleInterpolator(XNewil, FNewil, 6, double.Parse(txtNumber.Text));
                    txtboxResult.Text = "\n    A value interpolation polynomial is in the point of interpolation.";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    P = " + string.Format("{0:f" + precision + "}", newill.GetSolution());
                    break;
                case "Spline Interpolator":
                    double[] XSpline = new double[6];
                    for (i = 0; i < 6; i++)
                        XSpline[i] = double.Parse(initUniformGrid[i]); ;
                    double[] FSpline = new double[6];
                    for (int j = 0; j < 6; j++)
                        FSpline[j] = double.Parse(initUniformGrid[j + i]);
                    SplineInterpolator spline = new SplineInterpolator(XSpline, FSpline, 6, double.Parse(txtNumber.Text));
                    txtboxResult.Text = "\n    A value interpolation polynomial is in the point of interpolation.";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    P = " + string.Format("{0:f" + precision + "}", spline.GetSolution());
                    break;
                case "Barycentric Interpolator":
                    double[] XBary = new double[6];
                    for (i = 0; i < 6; i++)
                        XBary[i] = double.Parse(initUniformGrid2[i]); ;
                    double[] FBary = new double[6];
                    for (int j = 0; j < 6; j++)
                        FBary[j] = double.Parse(initUniformGrid2[j + i]);
                    double[] WBary = new double[6];
                    for (int j = 0; j < 6; j++)
                        WBary[j] = double.Parse(initUniformGrid2[j + 12]);
                    BarycentricInterpolation barycen = new BarycentricInterpolation(XBary, FBary, WBary, 6, double.Parse(txtNumber.Text));
                    txtboxResult.Text = "\n    A value interpolation polynomial is in the point of interpolation.";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    P = " + string.Format("{0:f" + precision + "}", barycen.GetSolution());
                    break;
                //*** Statistics ***
                case "Correlation Pearson":
                    double[] XCorelP = new double[6];
                    for (i = 0; i < 6; i++)
                        XCorelP[i] = double.Parse(initUniformGrid[i]);
                    double[] YCorelP = new double[6];
                    for (int j = 0; j < 6; j++)
                        YCorelP[j] = double.Parse(initUniformGrid[j + 6]);

                    CorrelationPearson corelperson = new CorrelationPearson(XCorelP, YCorelP, 6);
                    txtboxResult.Text = "\n    Pearson product-moment correlation coefficient.";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    K = " + string.Format("{0:f" + precision + "}", corelperson.GetSolution());
                    break;
                case "Correlation Spearmans Rank":
                    double[] XCorelSR = new double[6];
                    for (i = 0; i < 6; i++)
                        XCorelSR[i] = double.Parse(initUniformGrid[i]);
                    double[] YCorelSR = new double[6];
                    for (int j = 0; j < 6; j++)
                        YCorelSR[j] = double.Parse(initUniformGrid[j + 6]);

                    CorrelationSpearmansRank corelspear = new CorrelationSpearmansRank(XCorelSR, YCorelSR, 6);
                    txtboxResult.Text = "\n    Pearson product-moment correlation coefficient.";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    K = " + string.Format("{0:f" + precision + "}", corelspear.GetSolution());
                    break;
                case "Descriptive Statistics A Dev":
                    double[] XDecripSt = new double[6];
                    for (i = 0; i < 6; i++)
                        XDecripSt[i] = double.Parse(initUniformGrid[i]);
                    DescriptiveStatisticsADev desceripSt = new DescriptiveStatisticsADev(XDecripSt, 6);
                    txtboxResult.Text = "\n    Output parameters:";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    ADev = " + string.Format("{0:f" + precision + "}", desceripSt.GetSolution());
                    break;
                case "Descriptive Statistics Median":
                    double[] XDecripM = new double[6];
                    for (i = 0; i < 6; i++)
                        XDecripM[i] = double.Parse(initUniformGrid[i]);
                    DescriptiveStatisticsADevMedian desceripM = new DescriptiveStatisticsADevMedian(XDecripM, 6);
                    txtboxResult.Text = "\n    Output parameters:";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    M = " + string.Format("{0:f" + precision + "}", desceripM.GetSolution());
                    break;
                case "Descriptive Statistics Moments":
                    double[] XDecripMo = new double[6];
                    for (i = 0; i < 6; i++)
                        XDecripMo[i] = double.Parse(initUniformGrid[i]);
                    DescriptiveStatisticsMoments desceripMo = new DescriptiveStatisticsMoments(XDecripMo, 6);
                    txtboxResult.Text = "\n    Output parameters:";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    M = " + string.Format("{0:f" + precision + "}", desceripMo.GetSolution());
                    txtboxResult.Text = txtboxResult.Text + "\n\n    Variance = " + string.Format("{0:f" + precision + "}", desceripMo.variance);
                    txtboxResult.Text = txtboxResult.Text + "\n\n    Skewness = " + string.Format("{0:f" + precision + "}", desceripMo.skewness) + " (if variance<>0; zero otherwise)";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    Kurtosis = " + string.Format("{0:f" + precision + "}", desceripMo.kurtosis) + " (if variance<>0; zero otherwise)";
                    break;
                case "Descriptive Statistics Percentile":
                    double[] XDecripP = new double[6];
                    for (i = 0; i < 6; i++)
                        XDecripP[i] = double.Parse(initUniformGrid[i]);
                    DescriptiveStatisticsPercentile desceripP = new DescriptiveStatisticsPercentile(XDecripP, 6, double.Parse(txtNumber.Text));
                    txtboxResult.Text = "\n    Output parameters:";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    V = " + string.Format("{0:f" + precision + "}", desceripP.GetSolution());
                    break;
                case "Method 1":
                    RandomGeneratorsMethod1 random1 = new RandomGeneratorsMethod1();
                    txtboxResult.Text = "\n    Output parameters:";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    Random = " + string.Format("{0:f" + precision + "}", random1.GetSolution());
                    break;
                case "Method 2":
                    RandomGeneratorsMethod2 random2 = new RandomGeneratorsMethod2(int.Parse(txtNumber.Text));
                    txtboxResult.Text = "\n    Output parameters:";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    Random = " + string.Format("{0:f" + precision + "}", random2.GetSolution());
                    break;
                case "Method 3":
                    RandomGeneratorsMethod3 random3 = new RandomGeneratorsMethod3();
                    txtboxResult.Text = "\n    Output parameters:";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    Random = " + string.Format("{0:f" + precision + "}", random3.GetSolution());
                    break;
                case "Method 4":
                    RandomGeneratorsMethod4 random4 = new RandomGeneratorsMethod4();
                    txtboxResult.Text = "\n    Output parameters:";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    Random = " + string.Format("{0:f" + precision + "}", random4.GetSolution());
                    break;
                case "Method 5":
                    RandomGeneratorsMethod5 random5 = new RandomGeneratorsMethod5(double.Parse(txtNumber.Text));
                    txtboxResult.Text = "\n    Output parameters:";
                    txtboxResult.Text = txtboxResult.Text + "\n\n    Random = " + string.Format("{0:f" + precision + "}", random5.GetSolution());
                    break;
                //*** Matrix Algebra ***
                case "Matrix Determinant":
                    MatrixDeterminant matrdet = new MatrixDeterminant();
                    txtboxResult.Text = " \n\n\n  Determinant =  " + string.Format("{0:f" + precision + "}", matrdet.MatrixDet(MatrixInit, rangeMatrix-1)) + ";\n";
                    break;
                case "Decomposition of matrix LU":
                    txtboxResult.Text = "";
                    MatrixLU matrlu = new MatrixLU(MatrixInit, 4, 4);
                    var result_matrlu = matrlu.GetSolution();
                    var result_matrlu2 = matrlu.GetSolution2();
                    for (int ii = 0; ii < rangeMatrix - 1; ii++)
                    {
                        for (int j = 0; j < rangeMatrix - 1; j++)
                            txtboxResult.Text = txtboxResult.Text + "  \t " + string.Format("{0:f" + precision + "}", result_matrlu[ii, j]);
                        txtboxResult.Text = txtboxResult.Text + " \n";
                    }
                    txtboxResult.Text = txtboxResult.Text + " \n     ";
                    for (int ii = 0; ii < rangeMatrix - 1; ii++)
                        txtboxResult.Text = txtboxResult.Text + "      " + string.Format("{0:f" + precision + "}", result_matrlu2[ii]);
                    txtboxResult.Text = txtboxResult.Text + " \n ";
                    break;
                case "Matrix Inverse LU":
                    RMatrixLuInverse matrluinv = new RMatrixLuInverse();

                    MatrixLU matrlu2 = new MatrixLU(MatrixInit, rangeMatrix - 1, rangeMatrix - 1);
                    if (matrluinv.rmatrixluinverse(MatrixInit, rangeMatrix - 1, matrlu2.GetSolution2()) == true)
                    {
                        txtboxResult.Text = "\n             An inverse matrix exists \n\n ";
                        var result_matrluinv = matrluinv.GetSolution();
                        for (int ii = 0; ii < rangeMatrix - 1; ii++)
                        {
                            for (int j = 0; j < rangeMatrix - 1; j++)
                            {
                                txtboxResult.Text = txtboxResult.Text + "    \t" + string.Format("{0:f" + precision + "}", result_matrluinv[ii, j]);
                            }
                            txtboxResult.Text = txtboxResult.Text + "\n\n";
                        }
                    }
                    else
                        txtboxResult.Text = "\n             An inverse matrix does not exist";

                    break;
                //*** Linear Systems ***
                case "Gaus":
                    double[,] LinSysMatrix;
                    LinSysMatrix = new double[100, 100];
                    for (int l = 0; l < range; l++)
                    {
                        for (int j = 0; j < range; j++)
                        {
                            LinSysMatrix[l, j] = LinSysMatrixB[l, j];
                        }
                        LinSysMatrix[l, range] = LinSysMasA[l];
                    }
                    Gaus gaus = new Gaus(4, LinSysMatrix);
                    var result_gaus = gaus.GetSolution();
                    txtboxResult.Text = "";
                    for (int j = 0; j < result_gaus.Length; j++)
                        txtboxResult.Text = txtboxResult.Text + "\n         X" + j + "  =  "
                            + string.Format("{0:f" + precision + "}", result_gaus[j]) + ";";
                    break;
                case "Zeidel":
                    Zeidel zeidel = new Zeidel(4, LinSysMatrixB, LinSysMasA);
                    var result_zeidel = zeidel.GetSolution();
                    txtboxResult.Text = "";
                    for (int j = 0; j < result_zeidel.Length; j++)
                        txtboxResult.Text = txtboxResult.Text + "\n         X" + j + "  =  "
                            + string.Format("{0:f" + precision + "}", result_zeidel[j]) + ";";
                    break;
            }
        }
        public double TestFunDifferEquations(double x, double y)
        {
            //  string text = "(x^3-x*y)+8";
            Parser p = new Parser();
            p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
            p.Grammar.AddNamedConstant("y", Convert.ToDouble(y));
            p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
            p.Grammar.AddNamedConstant("Y", Convert.ToDouble(y));
            return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());
        }
        public static double TestFunNewton(double x)
        {
            // return (2.113f * x * x * x - 6.44f * x * x - 3.19f * x + 15.13f);
            Parser p = new Parser();
            p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
            p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
            return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());
        }
        public static double TestFunIteration(double x)
        {
            //return (0.1697f * x * x * x - 0.5693f * x * x - 1.6000f * x + 3.7300f);
            Parser p = new Parser();
            p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
            p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
            return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());
        }
        public static double TestFunBisection(double x)
        {
            // return (Math.Tan(0.9464f * x) - 1.3825f * x);
            Parser p = new Parser();
            p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
            p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
            return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());
        }
        public static double TestFunNewton2(double x)
        {
            return (3 * 2.113f * x * x + 2 * (-6.44f) * x - 3.19f);
        }
        public static double TestFunInteger(double x)
        {
            //return Math.Sqrt(x) * Math.Sin(x);
            Parser p = new Parser();
            p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
            p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
            return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());
        }
        public static double TestFunNonLinearEquations(double x)
        {
            // return Math.Cos(x) - x * x + 1;
            Parser p = new Parser();
            p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
            p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
            return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());
        }
        public static double TestFunOptimizing(double x)
        {
            // return Math.Cos(x) - x * x * x + 9;
            Parser p = new Parser();
            p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
            p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
            return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());
        }
        //*************/TreeViewMenu************************
    }
}