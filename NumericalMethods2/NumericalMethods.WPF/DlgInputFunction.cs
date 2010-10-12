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
    class DlgInputFunction : Window
    {
        Button btnOk;
        TextBox source;
        TextBlock OutPut;
        //RichTextBox source;
        string function = "Non";
        string[] operations = { "+", "-", "*", "/", "^", "sqrt()", "cos", "sin", "tan", "( )", "pi", "e", "abs", "exp", "log" };
        public DlgInputFunction()
        {
            Title = " Input Function";
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
            grpbox.Header = "To amend or input you function:";
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
            //RichTextBox source = new RichTextBox();
            source = new TextBox();
            source.Foreground = Brushes.Black;
            source.Width = 300;

            //source.Height = 80; 
            source.TextChanged += inputTb_TextChanged;
            source.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Children.Add(source);
            Grid.SetRow(source, 0);
            Grid.SetColumn(source, 0);

            OutPut = new TextBlock();
            OutPut.Margin = new Thickness(2);
            OutPut.Foreground = Brushes.YellowGreen;
            OutPut.Width = 200;

            //source.Height = 80; 
            //OutPut.TextChanged += inputTb_TextChanged;
            source.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Children.Add(OutPut);
            Grid.SetRow(OutPut, 1);
            Grid.SetColumn(OutPut, 0);


            GroupBox grpboxParam = new GroupBox();
            grpboxParam.BorderBrush = Brushes.Gray;
            grpboxParam.BorderThickness = new Thickness(1);
            grpboxParam.Header = "Access operations:";
            grpboxParam.Margin = new Thickness(2);
            grid.Children.Add(grpboxParam);
            Grid.SetRow(grpboxParam, 3);
            Grid.SetColumn(grpboxParam, 0);

            UniformGrid unigridPanel = new UniformGrid();
            grpboxParam.Content = unigridPanel;
            unigridPanel.Margin = new Thickness(3);
            unigridPanel.Rows = 4;
            unigridPanel.Columns = 5;
            for (int i = 0; i < operations.Length; i++)
            {
                Label lbloper = new Label();
                lbloper.Content = operations[i];
                lbloper.Width = 30;
                unigridPanel.Children.Add(lbloper);
            }

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
        ControlTemplate DesignButton()
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
            trig.Property = UIElement.IsMouseOverProperty;
            trig.Value = true;
            // Связывание объекта Setter с триггером 
            // для изменения свойства CornerRadius элемента Border. 
            Setter set = new Setter();
            set.Property = Border.CornerRadiusProperty;
            set.Value = new CornerRadius(24);
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
            trig = new Trigger();
            trig.Property = Button.IsPressedProperty;
            trig.Value = true;
            set = new Setter();
            set.Property = Border.BackgroundProperty;
            set.Value = SystemColors.ControlDarkBrush;
            set.TargetName = "border";
            // Включение объекта Setter в коллекцию Setters триггера 
            trig.Setters.Add(set);
            // Включение триггера в шаблон 
            template.Triggers.Add(trig);

            return template;
        }
        // Кнопка OK убирает диалоговое окно с экрана 
        Parser p = new Parser();
        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            // Precision_d = int.Parse(txtbox.Text);

        }
        private void inputTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            source = (TextBox)sender;
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
            }

        }
        public string Function
        {
            get { return function; }
            set
            {
                function = value;
                source.Text = value;
            }
        }
        void OkButtonOnClick(object sender, RoutedEventArgs args)
        {
            try
            {
                if (source.Text.Length > 0)
                {
                    function = source.Text.Trim();
                    // p.Parameters.Add("x");
                    // p.Parameters.Add("X");
                    //  p.Parameters.Add("y");
                    // p.Parameters.Add("Y");
                   // p.Grammar.AddNamedConstant("x", Convert.ToDouble(1));
                   // p.Grammar.AddNamedConstant("y", Convert.ToDouble(1));
                   // p.Grammar.AddNamedConstant("X", Convert.ToDouble(1));
                    ///p.Grammar.AddNamedConstant("Y", Convert.ToDouble(1));
                    //MessageBox.Show(p.Parse(source.Text).Tree.ToPolishInversedNotationString(), "You function");//.ToPolishInversedNotationString()

                    DialogResult = true;

                    //txtResult.Text = p.Parse(text).Tree.ToPolishInversedNotationString();
                }
            }
            catch (ParserException exc)
            {
                MessageBox.Show(exc.Message, "Error");

                // txtResult.Text = exc.Message;
            }
        }
    }
}