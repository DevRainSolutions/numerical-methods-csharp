using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Liquid;
using System.Collections.ObjectModel;
namespace NumericalMethods_Silverlight
{
	public partial class Help : UserControl
    {
        private bool _rootBuilding = true;
		public Help()
		{
			// Required to initialize variables
			InitializeComponent();
            testTree.BuildRoot();
        }
        void testTree_NodeCheckChanged(object sender, TreeEventArgs e)
        {
        }
        private void Tree_NodeClick(object sender, EventArgs e)
        {
            // TODO: Do something here when a node has been selected
        }

        private void Tree_Drop(object sender, TreeEventArgs e)
        {
            e.DropAction = Tree.DropActions.InsertBefore;
        }
        private void Tree_Populate(object sender, TreeEventArgs e)
        {
            ObservableCollection<Node> nodes = (sender is Tree ? ((Tree)sender).Nodes : ((Node)sender).Nodes);

            if (_rootBuilding)
            {
                nodes.Add(new Node("0", "My Documents", true, "images/folder.png", "images/folderOpen.png"));
                _rootBuilding = false;
            }
            else
            {
                switch (e.ID)
                {
                    case "0":
                        nodes.Add(new Node("1", "My Music", true, "images/folder.png", "images/folderOpen.png") { IsEnabled = false });
                        nodes.Add(new Node("2", "My Pictures", true, "images/folder.png", "images/folderOpen.png"));
                        nodes.Add(new Node("3", "My Videos", true, "images/folder.png", "images/folderOpen.png"));
                        nodes.Add(new Node("8", "Connect.xls", false, "images/xls.png"));
                        nodes.Add(new Node("6", "Latest.doc", false, "images/doc.png"));
                        nodes.Add(new Node("7", "Light.doc", false, "images/doc.png"));
                        nodes.Add(new Node("4", "Listing.pdf", false, "images/pdf.png"));
                        nodes.Add(new Node("5", "Update.pdf", false, "images/pdf.png") { IsEnabled = false });
                        break;
                    case "1":
                        nodes.Add(new Node("10", "Downloads", true, "images/folder.png", "images/folderOpen.png"));
                        nodes.Add(new Node("11", "Favourites", true, "images/folder.png", "images/folderOpen.png"));
                        nodes.Add(new Node("12", "Recent", true, "images/folder.png", "images/folderOpen.png"));
                        nodes.Add(new Node("13", "Track 01.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("14", "Track 02.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("15", "Track 03.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("16", "Track 04.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("17", "Track 05.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("18", "Track 06.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("19", "Track 07.mp3", false, "images/mp3.png"));
                        break;
                    case "10":
                        nodes.Add(new Node("16", "Track 04.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("17", "Track 05.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("18", "Track 06.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("19", "Track 07.mp3", false, "images/mp3.png"));
                        break;
                    case "11":
                        nodes.Add(new Node("13", "Track 01.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("14", "Track 02.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("15", "Track 03.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("18", "Track 06.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("19", "Track 07.mp3", false, "images/mp3.png"));
                        break;
                    case "12":
                        nodes.Add(new Node("14", "Track 02.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("15", "Track 03.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("16", "Track 04.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("18", "Track 06.mp3", false, "images/mp3.png"));
                        nodes.Add(new Node("19", "Track 07.mp3", false, "images/mp3.png"));
                        break;
                    case "2":
                        nodes.Add(new Node("13", "Image 1.gif", false, "images/gif.png"));
                        nodes.Add(new Node("14", "Image 2.gif", false, "images/gif.png"));
                        nodes.Add(new Node("15", "Image 3.gif", false, "images/gif.png"));
                        nodes.Add(new Node("16", "Image 4.gif", false, "images/gif.png"));
                        nodes.Add(new Node("17", "Image 5.gif", false, "images/gif.png"));
                        nodes.Add(new Node("18", "Image 6.gif", false, "images/gif.png"));
                        nodes.Add(new Node("19", "Image 7.gif", false, "images/gif.png"));
                        break;
                    case "3":
                        nodes.Add(new Node("13", "Video 01.mp4", false, "images/mp4.png"));
                        nodes.Add(new Node("14", "Video 02.mp4", false, "images/mp4.png"));
                        nodes.Add(new Node("15", "Video 03.mp4", false, "images/mp4.png"));
                        nodes.Add(new Node("16", "Video 04.mp4", false, "images/mp4.png"));
                        nodes.Add(new Node("17", "Video 05.mp4", false, "images/mp4.png"));
                        nodes.Add(new Node("18", "Video 06.mp4", false, "images/mp4.png"));
                        nodes.Add(new Node("19", "Video 07.mp4", false, "images/mp4.png"));
                        break;
                }
            }
        }

        private void UpBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }
	}
}