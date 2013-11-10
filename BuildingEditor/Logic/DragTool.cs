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
        private IInputElement _reference;

        public DragTool(FrameworkElement element, IInputElement reference)
        {
            _element = element;
            _reference = reference;

            TransformGroup group = (TransformGroup)_element.RenderTransform;
            _transform = group.Children.OfType<TranslateTransform>().First();

            Name = "Drag";
        }

        public override void CancelAction()
        {
            _dragEnabled = false;
        }

        #region Mouse event handlers
        public override void MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _start = e.GetPosition(_reference);
            _dragEnabled = true;
        }

        public override void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!_dragEnabled) return;

            Point pos = e.GetPosition(_reference);

            double dX = pos.X - _start.X;
            double dY = pos.Y - _start.Y;

            _start = pos;

            _transform.X += dX;
            _transform.Y += dY;
        }

        public override void MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _dragEnabled = false;
        }
        #endregion
    }
}
