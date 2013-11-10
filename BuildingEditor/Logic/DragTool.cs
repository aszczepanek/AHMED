﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFTest.Logic
{
    public class DragTool : Tool
    {
        private bool _dragEnabled;
        private FrameworkElement _element;
        private TranslateTransform _transform;
        private Point _start;

        public DragTool(FrameworkElement element)
        {
            _element = element;

            TransformGroup group = (TransformGroup)_element.RenderTransform;
            _transform = group.Children.OfType<TranslateTransform>().First();

            Name = "Drag";
        }

        public override void CancelAction()
        {
            _dragEnabled = false;
        }

        public override void MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _start = e.GetPosition(_element);
            _dragEnabled = true;
        }

        public override void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!_dragEnabled) return;

            Point pos = e.GetPosition(_element);

            double dX = pos.X - _start.X;
            double dY = pos.Y - _start.Y;

            _transform.X += dX;
            _transform.Y += dY;
        }

        public override void MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _dragEnabled = false;
        }

        public override void MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        public override void MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }
    }
}
