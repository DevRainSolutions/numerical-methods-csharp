using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace YuMV.NumericalMethods
{
    public class ImagedTreeViewItem : TreeViewItem
    {
        TextBlock text;
        Image img;
        ImageSource srcSelected, srcUnselected;
        // Конструктор создает панель StackPanel с изображением и текстом 
        public ImagedTreeViewItem()
        {
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Header = stack;
            img = new Image();
            img.Width = 20;
            img.Height = 20;
            img.VerticalAlignment = VerticalAlignment.Center;
            img.Margin = new Thickness(0, 0, 2, 0);
            stack.Children.Add(img);
            text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(text);
        }
        // Открытые свойства текста и изображений 
        public string Text
        {
            set { text.Text = value; }
            get { return text.Text; }
        }
        public ImageSource Selectedlmage
        {
            set
            {
                srcSelected = value;
                if (IsSelected)
                    img.Source = srcSelected;
            }
            get { return srcSelected; }
        }
        public ImageSource Unselectedlmage
        {
            set
            {
                srcUnselected = value;
                if (!IsSelected)
                    img.Source = srcUnselected;
            }
            get { return srcUnselected; }
        }
        // Переопределения событий для задания изображений 
        protected override void OnSelected(RoutedEventArgs args)
        {
            base.OnSelected(args);
            img.Source = srcSelected;
        }
        protected override void OnUnselected(RoutedEventArgs args)
        {
            base.OnUnselected(args);
            img.Source = srcUnselected;
        }
    }
}