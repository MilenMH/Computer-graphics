using Draw.src.Attributes;
using Draw.src.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;


namespace Draw.src.Model
{
    public class TriangleShape : Shape
    {
        [Importable]    
        public TriangleShape(PointF point1, PointF point2, PointF point3, 
            Color borderColor, Color fillColor, DashStyle dashStyle = GlobalConstants.DefaultDashStyle, 
            bool temporaryFlag = false)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            this.Point3 = point3;
            base.BorderColor = borderColor;
            base.FillColor =  fillColor;
            base.TemporaryFlag = temporaryFlag;
            base.DashStyle = dashStyle;
            base.UniqueIdentifier = Guid.NewGuid();
        }

        public PointF Point1 { get; set; }

        public PointF Point2 { get; set; }

        public PointF Point3 { get; set; }


        float Sign(PointF p1, PointF p2, PointF p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        public override bool Contains(PointF checkPoint)
        {
            float d1;
            float d2;
            float d3;

            bool has_neg, has_pos;

            d1 = Sign(checkPoint, Point1, Point2);
            d2 = Sign(checkPoint, Point2, Point3);
            d3 = Sign(checkPoint, Point3, Point1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);

        }

        public override void DrawSelf(Graphics grfx)
        {
            var points = new PointF[] { this.Point1, this.Point2, this.Point3 };
            grfx.FillPolygon(new SolidBrush(FillColor), points);
            var border = new Pen(BorderColor);
            border.DashStyle = DashStyle;
            grfx.DrawPolygon(border, points);
        }

        public override void MoveToNextDestination(PointF next, PointF last)
        {
            this.Point1 = new PointF(Point1.X + next.X - last.X, Point1.Y + next.Y - last.Y);
            this.Point2 = new PointF(Point2.X + next.X - last.X, Point2.Y + next.Y - last.Y);
            this.Point3 = new PointF(Point3.X + next.X - last.X, Point3.Y + next.Y - last.Y);
        }

        public override Shape NewShapeRotatedToRigth()
        {
            var listOfPoints = new List<PointF>() { this.Point1, this.Point2, this.Point3 };
            var center = PolygonHlp.GetCentroidGeneric(new List<PointF>() { this.Point1, this.Point2, this.Point3 });
            var rotatedPoints = PolygonHlp.RotatePolygon(listOfPoints, center, GlobalConstants.RadiansRepresentationOfNinetyDegrees);
            return new TriangleShape(rotatedPoints[0], rotatedPoints[1], rotatedPoints[2], this.BorderColor, base.FillColor);
        }

        public override void Enlarge()
        {
            var points = PolygonHlp.GetEnlargedPolygon(new List<PointF>() { this.Point1, this.Point2, this.Point3}, -1F);
            this.Point1 = points[0];
            this.Point2 = points[1];
            this.Point3 = points[2];
        }

        public override void Shrink()
        {
            var points = PolygonHlp.GetEnlargedPolygon(new List<PointF>() { this.Point1, this.Point2, this.Point3 }, 1F);
            this.Point1 = points[0];
            this.Point2 = points[1];
            this.Point3 = points[2];
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("TriangleShape");
            stringBuilder.AppendLine("Point : X : " + Point1.X + " : Y : " + Point1.Y);
            stringBuilder.AppendLine("Point : X : " + Point2.X + " : Y : " + Point2.Y);
            stringBuilder.AppendLine("Point : X : " + Point3.X + " : Y : " + Point3.Y);
            stringBuilder.AppendLine("BorderColor : " + base.BorderColor.Name);
            stringBuilder.AppendLine("FillColor : " + ColorTranslator.ToHtml(base.FillColor));
            stringBuilder.AppendLine("DashStyle : " + base.DashStyle);
            stringBuilder.AppendLine("TemporaryFlag : " + base.TemporaryFlag);
            return stringBuilder.ToString();
        }


    }
}
