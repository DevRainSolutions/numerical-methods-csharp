using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;

namespace SilverlightDragNDrop
{
    public class DragManager
    {
        private bool isDragging = false;
        private Point lastMousePosition;
        private UIElement layoutRoot;
        private UIElement elementToDrag;

        public event EventHandler<CollisionEventArgs> Collision;

        public DragManager(UIElement layoutRoot)
        {
            this.layoutRoot = layoutRoot;
        }


        public void EnableDragableElement(UIElement elementToDrag)
        {
            this.elementToDrag = elementToDrag;
            this.elementToDrag.MouseLeftButtonDown += element_MouseLeftButtonDown;
            this.elementToDrag.MouseMove += elementToDrag_MouseMove;
            this.elementToDrag.MouseLeftButtonUp += elementToDrag_MouseLeftButtonUp;

        }

        public void DisableDragableElement()
        {
            elementToDrag.MouseLeftButtonDown -= element_MouseLeftButtonDown;
            elementToDrag.MouseMove -= elementToDrag_MouseMove;
            elementToDrag.MouseLeftButtonUp -= elementToDrag_MouseLeftButtonUp;

        }

        void elementToDrag_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).ReleaseMouseCapture();
            isDragging = false;
        }

        void elementToDrag_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                UIElement element = (UIElement)sender;
                TranslateTransform transform = GetTranslateTransform(element);
                Point currentMousePosition = e.GetPosition(layoutRoot);
                double mouseX = currentMousePosition.X - lastMousePosition.X;
                double mouseY = currentMousePosition.Y - lastMousePosition.Y;
                transform.X += mouseX;
                transform.Y += mouseY;
                if (Collision != null)
                {
                    List<UIElement> collidedElements = VisualTreeHelper.FindElementsInHostCoordinates(currentMousePosition, layoutRoot) as List<UIElement>;
                    collidedElements.Remove(element);
                    collidedElements.Remove(layoutRoot);

                    if (collidedElements.Count() > 0)
                    {
                        CollisionEventArgs args = new CollisionEventArgs() { Element = element, Position = currentMousePosition, CollidedElements = collidedElements };
                        Collision(this, args);
                    }
                }
                lastMousePosition = currentMousePosition;

            }
        }

        void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            lastMousePosition = e.GetPosition(layoutRoot);
            ((UIElement)sender).CaptureMouse();
        }

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            TranslateTransform translateTransform = null;
            if (element.RenderTransform is TranslateTransform)
            {
                translateTransform = element.RenderTransform as TranslateTransform;
            }
            else if (element.RenderTransform is TransformGroup)
            {
                TransformGroup group = element.RenderTransform as TransformGroup;
                foreach (GeneralTransform transform in group.Children)
                {
                    if (transform is TranslateTransform)
                    {
                        translateTransform = transform as TranslateTransform;
                    }
                }
            }
            else
            {
                translateTransform = new TranslateTransform();
                element.RenderTransform = translateTransform;
            }
            return translateTransform;
        }

    }

    public class CollisionEventArgs : EventArgs
    {

        public UIElement Element { get; set; }
        public Point Position { get; set; }
        public List<UIElement> CollidedElements { get; set; }

    }
}
