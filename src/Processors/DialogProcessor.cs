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
            Color = Color.White;
        }

        public Random Random { get; set; }

        public Shape Selection { get; set; }

        public Shape SelectionCopy { get; set; }

        public IEnumerable<Shape> MultiSelection { get; set; }

        public bool IsDragging { get; set; }

        public PointF LastLocation { get; set; }

        public Color Color { get; set; }

        public PointF OnMouseDownPoint { get; set; }

        public PointF OnMouseUpPoint { get; set; }

        public bool DrowTemporaryRectangle { get; set; }

        public bool DrowTemporaryTriangle { get; set; }

        public bool DrowTemporaryEllipse { get; set; }

        public bool DrowTemporaryCopyShape { get; set; }

        public bool MultiSelectFlag { get; set; }

        public void AddRectangle(float x, float y, float width, float height, DashStyle dashStyle, bool temporary = false, int transparency = 255)
        {
            Shape rectangle = new RectangleShape(new PointF(x, y + height), new PointF(x, y),
                new PointF(x + width, y), new PointF(x + width, y + height), Color.Black, Color.FromArgb(transparency, Color.White), dashStyle, temporary);
            ShapeList.Add(rectangle);
        }

        public void AddTriangle(PointF p1, PointF p2, PointF p3, DashStyle dashStyle, bool temporary = false, int transparency = 255)
        {
            Shape triangle = new TriangleShape(p1, p2, p3, Color.Black, Color.FromArgb(transparency, Color.White), dashStyle, temporary);
            ShapeList.Add(triangle);
        }

        public void AddEllipse(float x, float y, float width, float height, DashStyle dashStyle, bool temporary = false, int transparency = 255)
        {
            Shape ellipse = new EllipseShape(new PointF(x, y + height), new PointF(x, y),
                new PointF(x + width, y), new PointF(x + width, y + height), Color.Black, Color.FromArgb(transparency, Color.White), dashStyle, temporary);
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
