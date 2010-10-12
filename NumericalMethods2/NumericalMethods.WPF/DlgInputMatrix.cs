using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;
using MathParser;
using MathParser.TextModel;
namespace YuMV.NumericalMethods
{
    class DlgInputMatrix : Window
    {
        Button btnOk;
        TextBox RangeEquation;
        TextBox txtMatrix;

        double[,] MatrixData;
        int range = 0;
        public DlgInputMatrix()
        {

            Title = " Input Equation";
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;
            // Создание объекта StackPanel 
            // и его назначение содержимым окна 
            StackPanel stack = new StackPanel();
            Content = stack;
            // Создание GroupBox как дочернего объекта StackPanel 
            GroupBox grpbox = new GroupBox();
            grpbox.BorderBrush = Brushes.Gray;
            grpbox.BorderThickness = new Thickness(2);
            grpbox.Header = "Parameters:";
            grpbox.Margin = new Thickness(20);
            stack.Children.Add(grpbox);
            // Назначение объекта Grid содержимым GroupBox 
            Grid grid = new Grid();
            grid.Margin = new Thickness(15);
            grpbox.Content = grid;
            // Две строки и четыре столбца 
            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);
            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(10, GridUnitType.Pixel);
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);

            // Размещение элементов Label и TextBox в элементе Grid 
            GroupBox grpboxIn = new GroupBox();
            grpboxIn.BorderThickness = new Thickness(2);
            grpboxIn.Header = "Matrix data:";
            grpboxIn.Margin = new Thickness(3);
            grid.Children.Add(grpboxIn);
            Grid.SetRow(grpboxIn, 0);
            Grid.SetColumn(grpboxIn, 0);

            Grid gridIn = new Grid();
            gridIn.Margin = new Thickness(5);
            grpboxIn.Content = gridIn;
            // Две строки и четыре столбца 
            RowDefinition rowdefIn = new RowDefinition();
            rowdefIn.Height = GridLength.Auto;
            gridIn.RowDefinitions.Add(rowdefIn);
            ColumnDefinition coldefIn = new ColumnDefinition();
            coldefIn.Width = GridLength.Auto;
            gridIn.ColumnDefinitions.Add(coldefIn);
            coldefIn = new ColumnDefinition();
            coldefIn.Width = GridLength.Auto;
            gridIn.ColumnDefinitions.Add(coldefIn);

            Label lblbText = new Label();
            lblbText.Foreground = Brushes.Black;
            lblbText.Content = "Dimension of matrix (n x n):";
            lblbText.HorizontalAlignment = HorizontalAlignment.Left;
            gridIn.Children.Add(lblbText);
            Grid.SetRow(lblbText, 0);
            Grid.SetColumn(lblbText, 0);

            RangeEquation = new TextBox();
            RangeEquation.Margin = new Thickness(2);
            RangeEquation.TextChanged += TextBoxOnTextChanged;
            RangeEquation.Width = 50;
            gridIn.Children.Add(RangeEquation);
            Grid.SetRow(RangeEquation, 0);
            Grid.SetColumn(RangeEquation, 1);

            StackPanel stak = new StackPanel();
            ScrollViewer scroll = new ScrollViewer();
            scroll.Width = 300;
            scroll.Height = 200;
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtMatrix = new TextBox();
            txtMatrix.BorderThickness = new Thickness(2);
            stak.Children.Add(txtMatrix);
            scroll.Content = stak;
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 2);
            Grid.SetColumn(scroll, 0);

            // Создание элемента UniformGrid для кнопок OK и Cancel 
            UniformGrid unigrid = new UniformGrid();
            unigrid.Rows = 1;
            unigrid.Columns = 2;
            stack.Children.Add(unigrid);
            btnOk = new Button();
            btnOk.Content = "OK";
            btnOk.IsDefault = true;
            // btnOk.IsEnabled = false;
            btnOk.MinWidth = 60;
            btnOk.Margin = new Thickness(12);
            btnOk.HorizontalAlignment = HorizontalAlignment.Right;
            btnOk.Click += OkButtonOnClick;
            unigrid.Children.Add(btnOk);
            Button btnCancel = new Button();
            btnCancel.Content = "Cancel";
            btnCancel.IsCancel = true;
            btnCancel.MinWidth = 60;
            btnCancel.Margin = new Thickness(12);
            btnCancel.HorizontalAlignment = HorizontalAlignment.Left;
            unigrid.Children.Add(btnCancel);

            if (RangeEquation.Text != "")
            {
                range = Convert.ToInt32(RangeEquation.Text);
            }
            else
            {
                txtMatrix.IsEnabled = false;
            }
        }
        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {

            if (RangeEquation.Text != "")
            {
                range = Convert.ToInt32(RangeEquation.Text);
                if (range == 0 || range == 1)
                {
                    txtMatrix.IsEnabled = false;
                    txtMatrix.Text = "";
                }
                else
                {
                    if (range <= 20)
                    {
                        txtMatrix.IsEnabled = true;
                        txtMatrix.Text = "";
                        for (int i = 0; i < range; i++)
                        {
                            for (int j = 0; j < range; j++)
                            {
                                txtMatrix.Text = txtMatrix.Text + "0 ";
                            }
                            txtMatrix.Text = txtMatrix.Text + " \n";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Range <= 20.", "Error");
                        txtMatrix.IsEnabled = false;
                        txtMatrix.Text = "";
                    }
                }
            }
            else
            {
                txtMatrix.Text = "";
                txtMatrix.IsEnabled = false;
            }
        }
        private void inputTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public double[,] Matrix
        {
            get { return MatrixData; }
            set { MatrixData = value; }
        }
        public int Range
        {
            get { return range; }
            set { range = value; }
        }
        public void InitDlg()
        {
            RangeEquation.Text = range.ToString();
            txtMatrix.Text = "";
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    txtMatrix.Text = txtMatrix.Text + MatrixData[i, j] + " ";
                }
                txtMatrix.Text = txtMatrix.Text + " \n";
            }
        }
        void OkButtonOnClick(object sender, RoutedEventArgs args)
        {

            if (RangeEquation.Text != "")
            {
                range = Convert.ToInt32(RangeEquation.Text);
                MatrixData = new double[2 * range, 2 * range];
            }
            else
                MessageBox.Show("Enter dimension of matrix (n x n)", "Error");
            string strLine = "";

            if (txtMatrix.Text != "")
            {
                for (int i = 0; i < range; i++)
                {
                    strLine = txtMatrix.GetLineText(i);
                    for (int j = 0; j < range; j++)
                        MatrixData[i, j] = Convert.ToDouble(strLine.Split(' ')[j]);
                }

                DialogResult = true;
            }
            else
                MessageBox.Show("Enter matrix", "Error");

        }
    }
}