using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
namespace YuMV.NumericalMethods
{
    class DialogOptionPrecision : Window
    {
        // Четыре элемента TextBox для ввода числовых данных 
        TextBox txtbox = new TextBox();
        Button btnOk;
        public DialogOptionPrecision()
        {
            // Стандартные настройки для диалоговых окон 
            Title = "Option -> Precision";
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
            grpbox.Header = "Number digit after dot:";
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

            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);

            // Размещение элементов Label и TextBox в элементе Grid 

            Label lbl = new Label();
            lbl.Content = "Value:";
            lbl.Margin = new Thickness(6);
            lbl.VerticalAlignment = VerticalAlignment.Center;
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);
            txtbox = new TextBox();
            txtbox.TextChanged += TextBoxOnTextChanged;
            txtbox.MinWidth = 48;
            txtbox.Margin = new Thickness(6);
            grid.Children.Add(txtbox);
            Grid.SetRow(txtbox, 0);
            Grid.SetColumn(txtbox, 1);


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
        }
        // Кнопка OK доступна только в том случае, если элементы TextBox 
        // содержат числовые данные 
        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
           // Precision_d = int.Parse(txtbox.Text);

        }
        public int Precision
        {
            set
            {
                txtbox.Text = value.ToString();
            }
            get
            {
                return int.Parse( txtbox.Text);
            }
        }
        // Кнопка OK убирает диалоговое окно с экрана 
        void OkButtonOnClick(object sender, RoutedEventArgs args)
        {
      
            DialogResult = true;
        }
    }
}