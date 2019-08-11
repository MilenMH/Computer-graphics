using Draw.src.Attributes;
using Draw.src.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Draw.src.Model
{
    public class LineShape : Shape
    {
        [Importable]
        public LineShape(PointF point1, PointF point2,
            Color borderColor, Color fillColor, DashStyle dashStyle = GlobalConstants.DefaultDashStyle,
            bool temporaryFlag = false)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            base.BorderColor = borderColor;
            base.FillColor = fillColor;
            base.TemporaryFlag = temporaryFlag;
            base.DashStyle = dashStyle;
            base.UniqueIdentifier = Guid.NewGuid();
        }

        public PointF Point1 { get; set; }

        public PointF Point2 { get; set; }

        public override bool Contains(PointF point)
        {
            var ab = Math.Sqrt((Point2.X - Point1.X) * (Point2.X - Point1.X) + (Point2.Y - Point1.Y) * (Point2.Y - Point1.Y));
            var ap = Math.Sqrt((point.X - Point1.X) * (point.X - Point1.X) + (point.Y - Point1.Y) * (point.Y - Point1.Y));
            var pb = Math.Sqrt((Point2.X - point.X) * (Point2.X - point.X) + (Point2.Y - point.Y) * (Point2.Y - point.Y));
            if (Math.Abs(ab - (ap + pb)) < 0.1)
            {
                return true;
            }
            return false;
        }

        public override void DrawSelf(Graphics grfx)
        {
            var pen = new Pen(new SolidBrush(BorderColor));
            pen.DashStyle = base.DashStyle;
            grfx.DrawLine(pen, this.Point1, this.Point2);
        }

        public override void MoveToNextDestination(PointF next, PointF last)
        {
            this.Point1 = new PointF(Point1.X + next.X - last.X, Point1.Y + next.Y - last.Y);
            this.Point2 = new PointF(Point2.X + next.X - last.X, Point2.Y + next.Y - last.Y);
        }

        public override Shape NewShapeRotatedToRigth(float radians = 0.5235988F)
        {
            var listOfPoints = new List<PointF>() { this.Point1, this.Point2 };
            var center = PolygonHlp.GetCentroidGeneric(new List<PointF>() { this.Point1, this.Point2 });
            var rotatedPoints = PolygonHlp.RotatePolygon(listOfPoints, center, radians);
            return new LineShape(rotatedPoints[0], rotatedPoints[1], this.BorderColor, base.FillColor);
        }

        public override void Enlarge()
        {
            var m = (Point2.Y - Point1.Y) / (Point2.X - Point1.X);
            var b = (m * Point1.X - Point1.Y) * (-1);

            var xDifference = Math.Abs(Point1.X - Point2.X);
            var yDifference = Math.Abs(Point1.Y - Point2.Y);

            if (xDifference >= yDifference)
            {
                var newMaxX = Math.Max(Point1.X, Point2.X) + 1;
                var newMinX = Math.Min(Point1.X, Point2.X) - 1;

                var newYRelatedToMaxX = m * newMaxX + b;
                var newYRelatedTiMinX = m * newMinX + b;

                this.Point1 = new PointF(newMaxX, newYRelatedToMaxX);
                this.Point2 = new PointF(newMinX, newYRelatedTiMinX);
            }
            else
            {
                var newMaxY = Math.Max(Point1.Y, Point2.Y) + 1;
                var newMinY = Math.Min(Point1.Y, Point2.Y) - 1;

                var newXRelatedToMaxY = (newMaxY - b) / m;
                var newXRelatedTiMinY = (newMinY - b) / m;

                this.Point1 = new PointF(newXRelatedToMaxY, newMaxY);
                this.Point2 = new PointF(newXRelatedTiMinY, newMinY);
            }
        }

        public override void Shrink()
        {
            var m = (Point2.Y - Point1.Y) / (Point2.X - Point1.X);
            var b = (m * Point1.X - Point1.Y) * (-1);

            var xDifference = Math.Abs(Point1.X - Point2.X);
            var yDifference = Math.Abs(Point1.Y - Point2.Y);
            if (xDifference >= yDifference)
            {
                var newMaxX = Math.Max(Point1.X, Point2.X) - 1;
                var newMinX = Math.Min(Point1.X, Point2.X) + 1;

                var newYRelatedToMaxX = m * newMaxX + b;
                var newYRelatedTiMinX = m * newMinX + b;

                this.Point1 = new PointF(newMaxX, newYRelatedToMaxX);
                this.Point2 = new PointF(newMinX, newYRelatedTiMinX);
            }
            else
            {
                var newMaxY = Math.Max(Point1.Y, Point2.Y) - 1;
                var newMinY = Math.Min(Point1.Y, Point2.Y) + 1;

                var newXRelatedToMaxY = (newMaxY - b) / m;
                var newXRelatedTiMinY = (newMinY - b) / m;

                this.Point1 = new PointF(newXRelatedToMaxY, newMaxY);
                this.Point2 = new PointF(newXRelatedTiMinY, newMinY);
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("LineShape");
            stringBuilder.AppendLine("Point : X : " + Point1.X + " : Y : " + Point1.Y);
            stringBuilder.AppendLine("Point : X : " + Point2.X + " : Y : " + Point2.Y);
            stringBuilder.AppendLine("BorderColor : " + base.BorderColor.Name);
            stringBuilder.AppendLine("FillColor : " + ColorTranslator.ToHtml(base.FillColor));
            stringBuilder.AppendLine("DashStyle : " + base.DashStyle);
            stringBuilder.AppendLine("TemporaryFlag : " + base.TemporaryFlag);
            return stringBuilder.ToString();
        }
    }
}
