using Draw.src.Helpers;
using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
    public class DialogProcessor : DisplayProcessor
    {

        public DialogProcessor()
        {
            Random = new Random();
            FillColor = Color.White;
        }

        public Random Random { get; set; }

        public Shape Selection { get; set; }

        public IEnumerable<Shape> MultiSelection { get; set; }

        public bool IsDragging { get; set; }

        public PointF LastLocation { get; set; }

        public Color FillColor { get; set; }

        public PointF OnMouseDownPoint { get; set; }

        public PointF OnMouseUpPoint { get; set; }

        public bool DrowTemporaryRectangle { get; set; }

        public bool DrowTemporaryTriangle { get; set; }

        public bool DrowTemporaryEllipse { get; set; }

        public void AddRectangle(float x, float y, float width, float height, bool temporary = false)
        {
            Shape rectangle = new RectangleShape(x, y, width, height, Pens.Black, Color.White);
            rectangle.FillColor = Color.White;

            if (temporary)
            {
                var oldColor = rectangle.BorderColor.Color;
                var newPen = new Pen(oldColor);
                newPen.DashStyle = DashStyle.Dot;
                rectangle.BorderColor = newPen;
                rectangle.TemporaryFlag = temporary;
            }

            ShapeList.Add(rectangle);
        }

        public void AddTriangle(PointF p1, PointF p2, PointF p3, bool temporary = false)
        {
            Shape triangle = new TriangleShape(p1, p2, p3, Pens.Black, Color.White);
            triangle.FillColor = Color.White;

            if (temporary)
            {
                var oldColor = triangle.BorderColor.Color;
                var newPen = new Pen(oldColor);
                newPen.DashStyle = DashStyle.Dot;
                triangle.BorderColor = newPen;
                triangle.TemporaryFlag = temporary;
            }

            ShapeList.Add(triangle);

        }

        public void AddEllipse(float x, float y, float width, float height, bool temporary = false)
        {
            Shape ellipse = new EllipseShape(x, y, width, height, Pens.Black, Color.White);
            ellipse.FillColor = Color.White;

            if (temporary)
            {
                var oldColor = ellipse.BorderColor.Color;
                var newPen = new Pen(oldColor);
                newPen.DashStyle = DashStyle.Dot;
                ellipse.BorderColor = newPen;
                ellipse.TemporaryFlag = temporary;
            }

            ShapeList.Add(ellipse);
        }

        public Shape ContainsPoint(PointF point)
        {
            for (int i = ShapeList.Count - 1; i >= 0; i--)
            {
                if (ShapeList[i].Contains(point))
                {
                    return ShapeList[i];
                }
            }
            return null;
        }

        internal void TranslateTo(Shape shape, PointF previousLocation, PointF nextLocation)
        {
            if (shape != null)
            {
                shape.MoveToNextDestination(nextLocation, previousLocation);
            }
        }

        internal void MultiMoveTo(PointF previousLocation, PointF nextLocation)
        {
            foreach (var shape in MultiSelection)
            {
                TranslateTo(shape, previousLocation, nextLocation);
            }
        }
    }
}
