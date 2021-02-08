using Draw.src.Helpers;
using Draw.src.Models;
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
            Color = Color.White;
        }
        public Shape Selection { get; set; }

        public Shape SelectionCopy { get; set; }

        public IEnumerable<Shape> MultiSelection { get; set; }

        public bool IsDragging { get; set; }

        public PointF LastLocation { get; set; }

        public Color Color { get; set; }

        public PointF OnMouseDownPoint { get; set; }

        public PointF OnMouseUpPoint { get; set; }

        public bool DrawTemporaryRectangle { get; set; }

        public bool DrawTemporaryTriangle { get; set; }

        public bool DrawTemporaryEllipse { get; set; }

        public bool DrawTemporaryLine { get; set; }

        public bool DrawTemporaryCopyShape { get; set; }

        public bool DrawTemporaryReuleauxTriangle { get; set; }

        public bool DrawTemporaryEnvelope { get; set; }

        public bool DrawTemporaryGenericCircle { get; set; }

        public bool MultiSelectFlag { get; set; }

        public void AddRectangle(float x, float y, float width, float height, DashStyle dashStyle, bool temporary = false, int transparency = 255)
        {
            Shape rectangle = new RectangleShape(new PointF(x, y + height), new PointF(x, y),
                new PointF(x + width, y), new PointF(x + width, y + height), Color.Black, Color.FromArgb(transparency, Color.White), dashStyle, temporary);
            ShapeList[CurrentTab].Add(rectangle);
        }

        public void AddTriangle(PointF p1, PointF p2, PointF p3, DashStyle dashStyle, bool temporary = false, int transparency = 255)
        {
            Shape triangle = new TriangleShape(p1, p2, p3, Color.Black, Color.FromArgb(transparency, Color.White), dashStyle, temporary);
            ShapeList[CurrentTab].Add(triangle);
        }

        public void AddEllipse(float x, float y, float width, float height, DashStyle dashStyle, bool temporary = false, int transparency = 255)
        {
            Shape ellipse = new EllipseShape(new PointF(x, y + height), new PointF(x, y),
                new PointF(x + width, y), new PointF(x + width, y + height), Color.Black, Color.FromArgb(transparency, Color.White), dashStyle, temporary);
            ShapeList[CurrentTab].Add(ellipse);
        }

        public void AddGenericCircle(float x, float y, float width, float height, DashStyle dashStyle, bool temporary = false, int transparency = 255)
        {
            var radiusToCenterPoints = new int[0];
            //var radiusToCenterPoints = new int[] { 0, 90, 180, 270};

            var radiusToRadiusPoints = new src.Helpers.Tuple<int, int>[] {
                new src.Helpers.Tuple<int, int>(90, 320),
                new src.Helpers.Tuple<int, int>(135, 270),
            };

            Shape genericCircle = new GenericCircle(new PointF(x, y + height), new PointF(x, y),
                new PointF(x + width, y), new PointF(x + width, y + height), Color.Black, 
                Color.FromArgb(transparency, Color.White), radiusToCenterPoints, radiusToRadiusPoints, dashStyle, temporary);
            ShapeList[CurrentTab].Add(genericCircle);
        }

        public void AddLine(PointF p1, PointF p2, DashStyle dashStyle, bool temporary = false, int transparency = 255)
        {
            Shape triangle = new LineShape(p1, p2, Color.Black, Color.FromArgb(transparency, Color.White), dashStyle, temporary);
            ShapeList[CurrentTab].Add(triangle);
        }

        public void AddReuleauxTriangle(PointF p1, PointF p2, DashStyle dashStyle, bool temporary = false, int transparency = 255)
        {
            Shape triangle = new ReuleauxTriangleShape(p1, p2, Color.Black, Color.FromArgb(transparency, Color.White), dashStyle, temporary);
            ShapeList[CurrentTab].Add(triangle);
        }

        internal void AddEnvelope(float x, float y, float width, float height, DashStyle dashStyle, bool temporary = false, int transparency = 255)
        {
            Shape rectangle = new EnvelopeShape(new PointF(x, y + height), new PointF(x, y),
                new PointF(x + width, y), new PointF(x + width, y + height), Color.Black, Color.FromArgb(transparency, Color.White), dashStyle, temporary);
            ShapeList[CurrentTab].Add(rectangle);
        }

        public Shape ContainsPoint(PointF point)
        {
            for (int i = ShapeList[CurrentTab].Count - 1; i >= 0; i--)
            {
                if (ShapeList[CurrentTab][i].Contains(point))
                {
                    return ShapeList[CurrentTab][i];
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
