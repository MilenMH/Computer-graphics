using Draw.src.Attributes;
using Draw.src.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Draw.src.Models
{
    public class EllipseShape : Shape
    {
        [Importable]
        public EllipseShape(PointF point1, PointF point2, PointF point3, PointF point4, Color borderColor,
            Color fillColor, DashStyle dashStyle = GlobalConstants.DefaultDashStyle,
            bool temporaryFlag = false, float rotationAngleInDegrees = 0, double rotationAngleInRadians = 0)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            this.Point3 = point3;
            this.Point4 = point4;
            base.BorderColor = borderColor;
            base.FillColor = fillColor;
            base.TemporaryFlag = temporaryFlag;
            base.DashStyle = DashStyle;
            this.RotationAngleInDegrees = rotationAngleInDegrees;
            this.RotationAngleInRadians = rotationAngleInRadians;
            base.UniqueIdentifier = Guid.NewGuid();
        }

        public PointF Point1 { get; set; }

        public PointF Point2 { get; set; }

        public PointF Point3 { get; set; }

        public PointF Point4 { get; set; }

        public float RotationAngleInDegrees { get; set; }

        public double RotationAngleInRadians { get; set; }

        public override bool Contains(PointF point)
        {
            var semiAAxis = (Point4.X - Point2.X) / 2D;
            var semiBAxis = (Point4.Y - Point2.Y) / 2D;
            var xCenter = Point2.X + semiAAxis;
            var yCenter = Point2.Y + semiBAxis;
            return Math.Pow((Math.Cos(RotationAngleInRadians) * (point.X - xCenter) + Math.Sin(RotationAngleInRadians) * (point.Y - yCenter)), 2D) / Math.Pow(semiAAxis, 2D)
                + Math.Pow((Math.Sin(RotationAngleInRadians) * (point.X - xCenter) - Math.Cos(RotationAngleInRadians) * (point.Y - yCenter)), 2D) / Math.Pow(semiBAxis, 2D) <= 1;
        }


        public override void DrawSelf(Graphics grfx)
        {
            var rect = new Rectangle((int)Point2.X, (int)Point2.Y, (int)Point4.X - (int)Point2.X, (int)Point4.Y - (int)Point2.Y);
            var center = new PointF((Point2.X + Point4.X) / 2, (Point2.Y + Point4.Y) / 2);

            using (Pen pen = new Pen(base.BorderColor))
            using (SolidBrush brush = new SolidBrush(FillColor))
            {
                grfx.TranslateTransform(center.X, center.Y);
                grfx.RotateTransform(this.RotationAngleInDegrees);
                grfx.TranslateTransform(-center.X, -center.Y);
                grfx.DrawEllipse(pen, rect);
                grfx.FillEllipse(brush, rect); ;
                grfx.ResetTransform();
            }
        }

        public override void MoveToNextDestination(PointF next, PointF last)
        {
            var differenceX = next.X - last.X;
            var differenceY = next.Y - last.Y;

            this.Point1 = new PointF(this.Point1.X + differenceX, this.Point1.Y + differenceY);
            this.Point2 = new PointF(this.Point2.X + differenceX, this.Point2.Y + differenceY);
            this.Point3 = new PointF(this.Point3.X + differenceX, this.Point3.Y + differenceY);
            this.Point4 = new PointF(this.Point4.X + differenceX, this.Point4.Y + differenceY);
        }

        public override Shape NewShapeRotatedToRigth(float radians = GlobalConstants.RadiansRepresentationOfThirtyDegrees)
        {
            if (radians == GlobalConstants.RadiansRepresentationOfThirtyDegrees)
            {
                this.RotationAngleInDegrees += 30;
            }
            else if (radians == GlobalConstants.RadiansRepresentationOfThreeHundredAndThirtyDegrees)
            {
                this.RotationAngleInDegrees -= 30;
            }
            this.RotationAngleInRadians += radians;
            this.RotationAngleInRadians = this.RotationAngleInRadians > 6.28318531D ? this.RotationAngleInRadians - 6.28318531D : this.RotationAngleInRadians;

            this.RotationAngleInDegrees = this.RotationAngleInDegrees < 360 ? this.RotationAngleInDegrees + 360 : this.RotationAngleInDegrees;
            this.RotationAngleInDegrees = this.RotationAngleInDegrees > 360 ? this.RotationAngleInDegrees - 360 : this.RotationAngleInDegrees;

            var ellipse = new EllipseShape(this.Point1, this.Point2, this.Point3, this.Point4,
                this.BorderColor, this.FillColor, base.DashStyle, base.TemporaryFlag, 
                this.RotationAngleInDegrees, this.RotationAngleInRadians);
            return ellipse;
        }

        public override void Enlarge()
        {
            var points = PolygonHlp.GetEnlargedPolygon(new List<PointF>() { this.Point1, this.Point2, this.Point3, this.Point4 }, -1F);
            this.Point1 = points[0];
            this.Point2 = points[1];
            this.Point3 = points[2];
            this.Point4 = points[3];
        }

        public override void Shrink()
        {
            var points = PolygonHlp.GetEnlargedPolygon(new List<PointF>() { this.Point1, this.Point2, this.Point3, this.Point4 }, 1F);
            this.Point1 = points[0];
            this.Point2 = points[1];
            this.Point3 = points[2];
            this.Point4 = points[3];
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("EllipseShape");
            stringBuilder.AppendLine("Point : X : " + Point1.X + " : Y : " + Point1.Y);
            stringBuilder.AppendLine("Point : X : " + Point2.X + " : Y : " + Point2.Y);
            stringBuilder.AppendLine("Point : X : " + Point3.X + " : Y : " + Point3.Y);
            stringBuilder.AppendLine("Point : X : " + Point4.X + " : Y : " + Point4.Y);
            stringBuilder.AppendLine("BorderColor : " + base.BorderColor.Name);
            stringBuilder.AppendLine("FillColor : " + ColorTranslator.ToHtml(base.FillColor));
            stringBuilder.AppendLine("DashStyle : " + base.DashStyle);
            stringBuilder.AppendLine("TemporaryFlag : " + base.TemporaryFlag);
            stringBuilder.AppendLine("RotationAngleInDegrees : " + this.RotationAngleInDegrees);
            stringBuilder.AppendLine("RotationAngleInRadians : " + this.RotationAngleInRadians);
            return stringBuilder.ToString();

        }
    }
}
