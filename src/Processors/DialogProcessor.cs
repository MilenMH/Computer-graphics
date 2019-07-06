using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;

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

        public bool IsDragging { get; set; }

        public PointF LastLocation { get; set; }

        public Color FillColor { get; set; }

        public PointF OnMouseDownPoint { get; set; }

        public PointF OnMouseUpPoint { get; set; }

        public void AddRectangle(float x, float y, float width, float height)
        {
            Shape rect = new RectangleShape(x, y, width, height, Pens.Black, Color.White);
            rect.FillColor = Color.White;

            ShapeList.Add(rect);
        }

        public void AddRandomTriangle(PointF p1, PointF p2, PointF p3)
        {
            Shape rect = new TriangleShape(p1, p2, p3, Pens.Black, Color.White);
            rect.FillColor = Color.White;

            ShapeList.Add(rect);

        }

        public void AddEllipse(float x, float y, float width, float height)
        {
            Shape ellipse = new EllipseShape(x, y, width, height, Pens.Black, Color.White);
            ellipse.FillColor = Color.White;

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

        public void TranslateTo(PointF p)
        {
            if (this.Selection != null)
            {
                this.Selection.MoveToNextDestination(p, this.LastLocation);
                this.LastLocation = p;
            }
        }
    }
}
