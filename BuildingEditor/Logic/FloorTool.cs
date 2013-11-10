﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Shapes;

namespace WPFTest.Logic
{
    public class FloorTool : Tool
    {
        private Building _building;
        private Segment _selectionStart;
        private Segment _selectionEnd;
        private List<Segment> _selectedSegments;

        public FloorTool(Building b)
        {
            _building = b;
            Name = "Floor";
            _selectedSegments = new List<Segment>();
        }

        public override void CancelAction()
        {
            if (_selectionStart != null)
            {
                _selectionStart = _selectionEnd = null;
                UpdateSelectionPreview();
            }
        }

        public override void MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Segment segment = SenderToSegment(sender);
            _selectionEnd = _selectionStart = segment;

            UpdateSelectionPreview();
        }

        public override void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_selectionStart != null)
            {
                _selectionEnd = SenderToSegment(sender);
                UpdateSelectionPreview();
            }
            else
                SenderToSegment(sender).Preview = true;
        }

        public override void MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _selectionStart = _selectionEnd = null;
            Apply();
            UpdateSelectionPreview();
        }


        public override void MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_selectionStart != null) return;

            Segment segment = SenderToSegment(sender);
            segment.Preview = true;
        }

        public override void MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_selectionStart != null) return;

            Segment segment = SenderToSegment(sender);
            segment.Preview = false;
        }

        private void Apply()
        {
            SegmentType value = Clear == true ?  SegmentType.NONE : SegmentType.FLOOR;
            _selectedSegments.ForEach(x => x.Type = value);
            _building.UpdateBuilding();
        }

        private void UpdateSelectionPreview()
        {
            List<Segment> oldSelection = _selectedSegments;
            _selectedSegments = CalcualateAffectedSegments();
            oldSelection.Except(_selectedSegments).ToList().ForEach(x => x.Preview = false);
            _selectedSegments.ForEach(x => x.Preview = true);
        }

        private List<Segment> CalcualateAffectedSegments()
        {
            List<Segment> result = new List<Segment>();

            if (_selectionStart == null || _selectionEnd == null)
                return result;

            int rowBegin, rowEnd, colBegin, colEnd;
            rowBegin = Math.Min(_selectionStart.Row, _selectionEnd.Row);
            rowEnd = Math.Max(_selectionStart.Row, _selectionEnd.Row);
            colBegin = Math.Min(_selectionStart.Column, _selectionEnd.Column);
            colEnd = Math.Max(_selectionStart.Column, _selectionEnd.Column);

            for (int row = rowBegin; row <= rowEnd; row++)
                for (int col = colBegin; col <= colEnd; col++)
                    result.Add(_building.Data[row][col]);

            return result;
        }
    }
}
