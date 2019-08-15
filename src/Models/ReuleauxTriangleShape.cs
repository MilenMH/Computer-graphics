using Draw.src.Attributes;
using Draw.src.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Draw.src.Models
{
    public class ReuleauxTriangleShape : Shape
    {
        [Importable]
        public ReuleauxTriangleShape(PointF point1, PointF point2,
            Color borderColor, Color fillColor, DashStyle dashStyle = GlobalConstants.DefaultDashStyle,
            bool temporaryFlag = false, float rotationAngleInDegrees = 0, double rotationAngleInRadians = 0)
        {
            this.RecalculateAndSetPoints(point1, point2);

            base.BorderColor = borderColor;
            base.FillColor = fillColor;
            base.TemporaryFlag = temporaryFlag;
            base.DashStyle = dashStyle;
            this.RotationAngleInDegrees = rotationAngleInDegrees;
            this.RotationAngleInRadians = rotationAngleInRadians;
            base.UniqueIdentifier = Guid.NewGuid();
        }

        public PointF Point1 { get; set; }

        public PointF Point2 { get; set; }

        public Sextuple<float, float, float, float, float, float> АncillaryValues { get; set; }
        
        public float MinDimentionsDifference { get; set; }

        public float RotationAngleInDegrees { get; set; }

        public double RotationAngleInRadians { get; set; }

        public override bool Contains(PointF point)
        {
            var centroid = new PointF(this.АncillaryValues.Item1 + this.MinDimentionsDifference / 2f
                , this.АncillaryValues.Item2 + this.MinDimentionsDifference / 2f);

            point = PolygonHlp.RotatePoint(point, centroid, this.RotationAngleInRadians);

            var distance = DimentionCalculator.DistanceBetweenTwoPoints(centroid.X, centroid.Y, point.X, point.Y);
            var radius = this.MinDimentionsDifference / 2f;

            return point.X > centroid.X && point.Y < centroid.Y && distance <= radius;
        }

        public override void DrawSelf(Graphics grfx)
        {
            if (this.MinDimentionsDifference < 1)
            {
                return;
            }

            var topMidPoint = new PointF(this.АncillaryValues.Item1 + this.MinDimentionsDifference / 2f ,
                this.АncillaryValues.Item2);
            var rightMidPoint = new PointF(this.АncillaryValues.Item1 + MinDimentionsDifference ,
                this.АncillaryValues.Item2 + this.MinDimentionsDifference / 2f);
            var centroid = new PointF(this.АncillaryValues.Item1 + this.MinDimentionsDifference / 2f
                , this.АncillaryValues.Item2 + this.MinDimentionsDifference / 2f);

            using (var brush = new SolidBrush(this.FillColor))
            using (var border = new Pen(this.BorderColor))
            using (var path = new GraphicsPath())
            using (var innerBorder = new Pen(this.FillColor, 2))
            {
                grfx.TranslateTransform(centroid.X, centroid.Y);
                grfx.RotateTransform(this.RotationAngleInDegrees);
                grfx.TranslateTransform(-centroid.X, -centroid.Y);
              
                var points = new PointF[] { topMidPoint, rightMidPoint, centroid };
                grfx.FillPolygon(brush, points);
                border.DashStyle = base.DashStyle;

                grfx.DrawLine(innerBorder, rightMidPoint, topMidPoint);
                grfx.DrawLine(border, centroid, rightMidPoint);
                grfx.DrawLine(border, centroid, topMidPoint);

                path.AddArc(new RectangleF(
                    this.АncillaryValues.Item1, this.АncillaryValues.Item2,
                        this.MinDimentionsDifference, this.MinDimentionsDifference),
                    270,
                    90);
                grfx.SmoothingMode = SmoothingMode.HighQuality;
                grfx.FillPath(brush, path);
                grfx.DrawPath(border, path);

                grfx.ResetTransform();
            }
        }

        public override void Enlarge()
        {
            this.RecalculateAndSetPoints(
                new PointF(this.Point1.X - 1, this.Point1.Y - 1),
                new PointF(this.Point2.X + 1, this.Point2.Y + 1));
        }

        public override void Shrink()
        {
            this.RecalculateAndSetPoints(
                new PointF(this.Point1.X + 1, this.Point1.Y + 1),
                new PointF(this.Point2.X - 1, this.Point2.Y - 1));
        }

        public override void MoveToNextDestination(PointF next, PointF last)
        {
            this.RecalculateAndSetPoints(
                new PointF(Point1.X + next.X - last.X, Point1.Y + next.Y - last.Y),
                new PointF(Point2.X + next.X - last.X, Point2.Y + next.Y - last.Y));
        }

        public override Shape NewShapeRotatedToRigth(float radians = 0.5235988F)
        {
            if (radians == GlobalConstants.RadiansRepresentationOfThirtyDegrees)
            {
                this.RotationAngleInDegrees += 30;
            }
            else if (radians == GlobalConstants.RadiansRepresentationOfThreeHundredAndThirtyDegrees)
            {
                this.RotationAngleInDegrees -= 30;
            }
            this.RotationAngleInRadians -= radians;
            this.RotationAngleInRadians = this.RotationAngleInRadians < 0D ? this.RotationAngleInRadians + 6.28318531D : this.RotationAngleInRadians;

            this.RotationAngleInDegrees = this.RotationAngleInDegrees < 360 ? this.RotationAngleInDegrees + 360 : this.RotationAngleInDegrees;
            this.RotationAngleInDegrees = this.RotationAngleInDegrees > 360 ? this.RotationAngleInDegrees - 360 : this.RotationAngleInDegrees;

            var reuleauxTriangle = new ReuleauxTriangleShape(this.Point1, this.Point2,
                this.BorderColor, this.FillColor, base.DashStyle, base.TemporaryFlag);
            reuleauxTriangle.RotationAngleInDegrees = this.RotationAngleInDegrees;
            reuleauxTriangle.RotationAngleInRadians = this.RotationAngleInRadians;
            return reuleauxTriangle;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("ReuleauxTriangleShape");
            stringBuilder.AppendLine("Point : X : " + Point1.X + " : Y : " + Point1.Y);
            stringBuilder.AppendLine("Point : X : " + Point2.X + " : Y : " + Point2.Y);
            stringBuilder.AppendLine("BorderColor : " + base.BorderColor.Name);
            stringBuilder.AppendLine("FillColor : " + ColorTranslator.ToHtml(base.FillColor));
            stringBuilder.AppendLine("DashStyle : " + base.DashStyle);
            stringBuilder.AppendLine("TemporaryFlag : " + base.TemporaryFlag);
            stringBuilder.AppendLine("RotationAngleInDegrees : " + this.RotationAngleInDegrees);
            stringBuilder.AppendLine("RotationAngleInRadians : " + this.RotationAngleInRadians);
            return stringBuilder.ToString();
        }

        private void RecalculateAndSetPoints(PointF point1, PointF point2)
        {
            this.АncillaryValues = DimentionCalculator.GetShapesParamsByTwoPoints(point1, point2);
            this.Point1 = new PointF(this.АncillaryValues.Item1, this.АncillaryValues.Item2);
            this.MinDimentionsDifference = Math.Min(this.АncillaryValues.Item5, this.АncillaryValues.Item6);
            this.Point2 = new PointF(this.Point1.X + this.MinDimentionsDifference,
                this.Point1.Y + this.MinDimentionsDifference);
        }

        private float Sign(PointF p1, PointF p2, PointF p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }
    }
}
