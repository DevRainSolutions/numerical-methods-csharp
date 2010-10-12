using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace YuMV.NumericalMethods
{
    public class TreeViewMenuAdd : ImagedTreeViewItem
    {
        // Конструктор строит неполное дерево каталогов 
        public TreeViewMenuAdd(string NameMenu, string Type)
        {

            Text = NameMenu;
            if (Type == "maine")
            {
                Selectedlmage = Unselectedlmage = new BitmapImage(
                new Uri(Environment.CurrentDirectory.ToString() + "/images/treeMenu0.png"));
            }
            else
            {
                Selectedlmage = new BitmapImage(
                new Uri(Environment.CurrentDirectory.ToString() + "/images/treeMenu2.png"));
                Unselectedlmage = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString() + "/images/treeMenu1.png"));
            }
        }
    }
}