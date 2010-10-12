using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace YuMV.NumericalMethods
{
    class DlgMethods : Window
    {
        Button btnOk;
        public DlgMethods()
        {
            Title = "About";
            ShowInTaskbar = false;

            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ResizeMode = ResizeMode.NoResize;
            SizeToContent = SizeToContent.WidthAndHeight;

            StackPanel stack = new StackPanel();
            Content = stack;
            Grid grid = new Grid();
            grid.Margin = new Thickness(6);
            stack.Children.Add(grid);

            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);

            Label lblText = new Label();
            lblText.Content = "Numerical Methods v9.00";
            lblText.FontSize = 24;
            lblText.Foreground = Brushes.YellowGreen;
            //lblText.FontStyle
            grid.Children.Add(lblText);
            Grid.SetRow(lblText, 0);
            Grid.SetColumn(lblText, 0);

           /* Label lblText2 = new Label();
            lblText2.Content = "Author:";
            lblText2.FontSize = 12;
            lblText2.Foreground = Brushes.Black;
            lblText2.HorizontalAlignment = HorizontalAlignment.Center;*/

            Border bord = new Border();
            bord.Background = Brushes.SlateGray;
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
            bordIn.Background = Brushes.SlateGray;
            bordIn.BorderBrush = Brushes.YellowGreen;
            bord.Child = bordIn;
            bordIn.Background = Brushes.White;
            bordIn.BorderThickness = new Thickness(2);
            CornerRadius c2 = new CornerRadius();
            c2.BottomLeft = 20; c2.BottomRight = 20;
            c2.TopLeft = 20; c2.TopRight = 20;
            bordIn.CornerRadius = c2;

            DockPanel dock = new DockPanel();
            dock.Margin = new Thickness(10);
            bordIn.Child = dock;

            Expander expanderMenu;
            //*** "Approximate decision of \n equalization f(x)=0 ***
              expanderMenu = new Expander();
            dock.Children.Add(expanderMenu);
            DockPanel.SetDock(expanderMenu, Dock.Top);
            expanderMenu.Header = "Approximate decision of \n equalization f(x)=0";
            expanderMenu.Foreground = Brushes.White;
            expanderMenu.Margin = new Thickness(0, 0, 0, 10);
            expanderMenu.Background = Brushes.SlateGray;
            ListView ListApproximateDecision = new ListView();
            ListViewItem BisectionMethodItem = new ListViewItem();
            BisectionMethodItem.Content = "Bisection Method";
            ListViewItem ChordMethodItem = new ListViewItem();
            ChordMethodItem.Content = "Chord Method";
            ListViewItem IterationMethodItem = new ListViewItem();
            IterationMethodItem.Content = "Iteration Method";
            ListViewItem NewtonMethodItem = new ListViewItem();
            NewtonMethodItem.Content = "Newton Method";
            ListApproximateDecision.Items.Add(BisectionMethodItem);
            ListApproximateDecision.Items.Add(ChordMethodItem);
            ListApproximateDecision.Items.Add(IterationMethodItem);
            ListApproximateDecision.Items.Add(NewtonMethodItem);
            expanderMenu.Content = ListApproximateDecision;

              expanderMenu = new Expander();
            dock.Children.Add(expanderMenu);
            expanderMenu.Foreground = Brushes.White;
            expanderMenu.Header = "Numerical Methods";
            expanderMenu.Margin = new Thickness(0, 0, 0, 10);
            expanderMenu.Background = Brushes.SlateGray;
            ListView list2 = new ListView();
            ListViewItem listV12 = new ListViewItem();
            listV12.Content = "Method1";
            ListViewItem listV22 = new ListViewItem();
            listV22.Content = "Method2";
            list2.Items.Add(listV12);
            list2.Items.Add(listV22);
            expanderMenu.Content = list2;

            Grid.SetRow(bord, 1);
            Grid.SetColumn(bord, 0);


           
           /* grid.Children.Add(lblText3);
            Grid.SetRow(lblText3, 2);
            Grid.SetColumn(lblText3, 0);*/

            // Создание элемента UniformGrid для кнопок OK и Cancel 
            UniformGrid unigrid = new UniformGrid();
            unigrid.Rows = 1;
            unigrid.Columns = 1;
            stack.Children.Add(unigrid);
            // Создание объекта Button 
            btnOk = new Button();
            // Назначение шаблона 
            //btnOk.Template = DesignButton();
            btnOk.IsDefault = true;
            btnOk.Padding = new Thickness(5);
            btnOk.Margin = new Thickness(12);
            btnOk.HorizontalAlignment = HorizontalAlignment.Center;
            btnOk.VerticalAlignment = VerticalAlignment.Center;
            btnOk.Content = "OK";
            btnOk.Click += OkButtonOnClick;
            unigrid.Children.Add(btnOk);
        }
        void OkButtonOnClick(object sender, RoutedEventArgs args)
        {
            DialogResult = true;
        }
    }
}