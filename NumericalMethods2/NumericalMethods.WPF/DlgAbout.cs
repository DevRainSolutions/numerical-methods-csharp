using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;

namespace YuMV.NumericalMethods
{
    class DialogAbout : Window
    {
        Button btnOk;
        public DialogAbout()
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
            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);

            Label lblText = new Label();
            lblText.Content = "Numerical Methods v9.3";
            lblText.FontSize = 24;
            lblText.Foreground = Brushes.YellowGreen;
            //lblText.FontStyle
            grid.Children.Add(lblText);
            Grid.SetRow(lblText, 0);
            Grid.SetColumn(lblText, 0);

            Image img = new Image();
            Uri uri = new Uri(Environment.CurrentDirectory.ToString() + "/images/about.ico");
            img.Source = new BitmapImage(uri);
            img.Height = 150;
            img.Width = 150;
            img.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Children.Add(img);
            Grid.SetRow(img, 1);
            Grid.SetColumn(img, 0);
            Label lblText2 = new Label();
            lblText2.Content = " Developer: Michael Yushchenko";
            lblText2.FontSize = 14;
            lblText2.Foreground = Brushes.Black;
            lblText2.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Children.Add(lblText2);
            Grid.SetRow(lblText2, 2);
            Grid.SetColumn(lblText2, 0);
            Label lblText3 = new Label();
            lblText3.Content = "Coordinator: Krakovetskiy Aleksandr";
            lblText3.FontSize = 14;
            lblText3.Foreground = Brushes.Black;
            lblText3.HorizontalAlignment = HorizontalAlignment.Center;

            grid.Children.Add(lblText3);
            Grid.SetRow(lblText3, 3);
            Grid.SetColumn(lblText3, 0);

            // Создание элемента UniformGrid для кнопок OK и Cancel 
            UniformGrid unigrid = new UniformGrid();
            unigrid.Rows = 1;
            unigrid.Columns = 1;
            stack.Children.Add(unigrid);

            // Создание объекта Button 
            btnOk = new Button();
            // Назначение шаблона 
            btnOk.Template = DesignButton();
            btnOk.IsDefault = true;
            btnOk.Padding = new Thickness(5);
            btnOk.Margin = new Thickness(12);
            btnOk.HorizontalAlignment = HorizontalAlignment.Right;
            btnOk.VerticalAlignment = VerticalAlignment.Center;
            btnOk.Content = "OK";
            btnOk.Click += OkButtonOnClick;
            unigrid.Children.Add(btnOk);
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
        void OkButtonOnClick(object sender, RoutedEventArgs args)
        {
            DialogResult = true;
        }
    }
}