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
    class DlgInputSystemEquation : Window
    {
        Button btnOk;
        TextBox RangeEquation;
        TextBox txtMatrA;
        TextBox txtMatrB;
        double[] MatrA;
        double[,] MatrB;
        int range = 0;
        public DlgInputSystemEquation()
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
            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);
            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);


            // Размещение элементов Label и TextBox в элементе Grid 
            //RichTextBox source = new RichTextBox();

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
            lblbText.Content = "Dimension of equation (n x n):";
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


            Label lblbTextB = new Label();
            lblbTextB.Foreground = Brushes.Black;
            lblbTextB.HorizontalAlignment = HorizontalAlignment.Left;
            lblbTextB.Content = "b1[], b2[], b3[], ... ,bn[]";
            grid.Children.Add(lblbTextB);
            Grid.SetRow(lblbTextB, 2);
            Grid.SetColumn(lblbTextB, 0);

            Label lblbTextA = new Label();
            lblbTextA.Foreground = Brushes.Black;
            lblbTextA.Content = "a[]";
            lblbTextA.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Children.Add(lblbTextA);
            Grid.SetRow(lblbTextA, 2);
            Grid.SetColumn(lblbTextA, 1);

            StackPanel stak = new StackPanel();
            ScrollViewer scroll = new ScrollViewer();
            scroll.Width = 250;
            scroll.Height = 150;
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtMatrB = new TextBox();
            txtMatrB.BorderThickness = new Thickness(2);
            stak.Children.Add(txtMatrB);
            scroll.Content = stak;
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 3);
            Grid.SetColumn(scroll, 0);

            StackPanel stakA = new StackPanel();
            ScrollViewer scrollA = new ScrollViewer();
            scrollA.Width = 50;
            scrollA.Height = 150;
            scrollA.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtMatrA = new TextBox();
            txtMatrA.BorderThickness = new Thickness(2);
            stakA.Children.Add(txtMatrA);
            scrollA.Content = stakA;
            grid.Children.Add(scrollA);
            Grid.SetRow(scrollA, 3);
            Grid.SetColumn(scrollA, 1);

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
                txtMatrB.IsEnabled = false;
                txtMatrA.IsEnabled = false;
            }
        }
        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {

            if (RangeEquation.Text != "")
            {
                range = Convert.ToInt32(RangeEquation.Text);
                if (range == 0 || range == 1)
                {
                    txtMatrB.IsEnabled = false;
                    txtMatrA.IsEnabled = false;
                    txtMatrB.Text = "";
                    txtMatrA.Text = "";
                }
                else
                {
                    if (range <= 20)
                    {
                        txtMatrB.IsEnabled = true;
                        txtMatrA.IsEnabled = true;
                        txtMatrB.Text = "";
                        txtMatrA.Text = "";
                        for (int i = 0; i < range; i++)
                        {
                            txtMatrA.Text = txtMatrA.Text + "0\n";
                            for (int j = 0; j < range; j++)
                            {
                                txtMatrB.Text = txtMatrB.Text + "0 ";
                            }
                            txtMatrB.Text = txtMatrB.Text + " \n";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Range <= 20.", "Error");
                        txtMatrB.IsEnabled = false;
                        txtMatrA.IsEnabled = false;
                        txtMatrB.Text = "";
                        txtMatrA.Text = "";
                    }
                }
            }
            else
            {
                txtMatrB.Text = "";
                txtMatrA.Text = "";
                txtMatrB.IsEnabled = false;
                txtMatrA.IsEnabled = false;
            }
        }
        private void inputTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            /* source = (TextBox)sender;
             string text = source.Text;
             try
             {
                 if (text.Length > 0)
                 {
                     p.Parameters.Add("x");
                     p.Parameters.Add("X");
                     p.Parameters.Add("y");
                     p.Parameters.Add("Y");
                     OutPut.Text = p.Parse(text).Tree.ToPolishInversedNotationString();
                 }
             }
             catch (ParserException exc)
             {
                 OutPut.Text = exc.Message;
             }*/

        }
        public double[] MatrixA
        {
            get { return MatrA; }
            set { MatrA = value; }
        }
        public double[,] MatrixB
        {
            get { return MatrB; }
            set { MatrB = value; }
        }
        public int Range
        {
            get { return range; }
            set { range = value; }
        }
        public void InitDlg()
        {
            RangeEquation.Text = range.ToString();
            txtMatrA.Text = "";
            txtMatrB.Text = "";
            for (int i = 0; i < range; i++)
            {
                txtMatrA.Text = txtMatrA.Text + MatrA[i]+ "\n";
                for (int j = 0; j < range; j++)
                {
                    txtMatrB.Text = txtMatrB.Text + MatrixB [i,j]+ " ";
                }
                txtMatrB.Text = txtMatrB.Text + " \n";
            }
        }
        void OkButtonOnClick(object sender, RoutedEventArgs args)
        {

            if (RangeEquation.Text != "")
            {
                range = Convert.ToInt32(RangeEquation.Text);
                MatrA = new double[2 * range];
                MatrB = new double[2 * range, 2 * range];
            }
            else
                MessageBox.Show("Enter dimension of equation (n x n)", "Error");
            string strLine = "";
            if (txtMatrB.Text != "")
            {
                if (txtMatrA.Text != "")
                {
                    for (int i = 0; i < range; i++)
                    {
                        strLine = txtMatrB.GetLineText(i);
                        for (int j = 0; j < range; j++)
                            MatrB[i, j] = Convert.ToDouble(strLine.Split(' ')[j]);
                    }
                    for (int i = 0; i < range; i++)
                    {
                        strLine = txtMatrA.GetLineText(i);
                        MatrA[i] = Convert.ToDouble(strLine);
                    }

                   // MessageBox.Show(MatrA[0].ToString() + " " + MatrA[1].ToString(), "Res");
                    DialogResult = true;
                }
                else
                    MessageBox.Show("Enter matrix A", "Error");
            }
            else
                MessageBox.Show("Enter matrix B", "Error");
        }
    }
}