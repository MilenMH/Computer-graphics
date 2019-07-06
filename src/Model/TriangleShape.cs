using Draw.src.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Draw.src.Model
{
    public class TriangleShape : Shape
    {
        public TriangleShape(PointF p1, PointF p2, PointF p3, Pen borderColor, Color fillColor)
        {
            this.Point1 = p1;
            this.Point2 = p2;
            this.Point3 = p3;
            base.BorderColor = borderColor;
            base.FillColor = fillColor;
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
            grfx.DrawPolygon(BorderColor, points);
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
    }
}
