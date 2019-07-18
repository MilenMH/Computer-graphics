using Draw.src.Attributes;
using Draw.src.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
    public class EllipseShape : Shape
    {
        [Importable]
        public EllipseShape(float x, float y, float width, float height, Pen borderColor, Color fillColor)
        {
            base.X = x;
            base.Y = y;
            this.Width = width;
            this.Height = height;
            base.BorderColor = borderColor;
            base.FillColor = fillColor;
            base.UniqueIdentifier = Guid.NewGuid();
        }

        public float Width { get; set; }

        public float Height { get; set; }


        public override bool Contains(PointF point)
        {
            float _xRadius = this.Width / 2;
            float _yRadius = this.Height / 2;

            PointF center = new PointF(this.X + _xRadius, this.Y + _yRadius);

            PointF normalized = new PointF(point.X - center.X ,
                                         point.Y - center.Y );

            return ((double)(normalized.X * normalized.X)
                     / (_xRadius * _xRadius)) + ((double)(normalized.Y * normalized.Y) 
                     / (_yRadius * _yRadius))
                <= 1.0;
        }

        public override void DrawSelf(Graphics grfx)
        {
            grfx.FillEllipse(new SolidBrush(FillColor), base.X, base.Y, this.Width, this.Height);
            grfx.DrawEllipse(BorderColor, base.X, base.Y, this.Width, this.Height);
        }

        public override void MoveToNextDestination(PointF next, PointF last)
        {
            this.X += next.X - last.X;
            this.Y += next.Y - last.Y;
        }

        public override Shape NewShapeRotatedToRigth()
        {
            var listOfPoints = new List<PointF>()
            {
                new PointF(base.X, base.Y),
                new PointF(base.X + this.Width, base.Y),
                new PointF(base.X, base.Y + this.Height),
                new PointF(base.X + this.Width, base.Y + this.Height)
            };
            var center = PolygonHlp.GetCentroidGeneric(listOfPoints);
            var rotatedPoints = PolygonHlp.RotatePolygon(listOfPoints, center, GlobalConstants.RadiansRepresentationOfNinetyDegrees);

            float maxX = 0f;
            float maxY = 0f;
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            rotatedPoints.ForEach(p =>
            {
                maxX = p.X > maxX ? p.X : maxX;
                maxY = p.Y > maxY ? p.Y : maxY;
                minX = p.X < minX ? p.X : minX;
                minY = p.Y < minY ? p.Y : minY;
            });

            float width = maxX - minX;
            float height = maxY - minY;

            return new EllipseShape(minX, minY, width, height, this.BorderColor, base.FillColor);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("EllipseShape");
            stringBuilder.AppendLine("X : " + base.X);
            stringBuilder.AppendLine("Y : " + base.Y);
            stringBuilder.AppendLine("Width : " + this.Width);
            stringBuilder.AppendLine("Height : " + this.Height);
            stringBuilder.AppendLine("BorderColor : " + base.BorderColor.Color.Name);
            stringBuilder.AppendLine("FillColor : " + base.FillColor.Name);
            return stringBuilder.ToString();

        }
    }
}
