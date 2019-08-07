using Draw.src.Attributes;
using Draw.src.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Draw.src.Model
{
	public class RectangleShape : Shape
	{
        #region Constructor

        [Importable]
        public RectangleShape(PointF point1, PointF point2, PointF point3, PointF point4, Color borderColor, 
            Color fillColor, DashStyle dashStyle = GlobalConstants.DefaultDashStyle,
            bool temporaryFlag = false)
		{
            this.Point1 = point1;
            this.Point2 = point2;
            this.Point3 = point3;
            this.Point4 = point4;
            base.BorderColor = borderColor;
            base.FillColor = fillColor;
            base.TemporaryFlag = temporaryFlag;
            base.DashStyle = dashStyle;
            base.UniqueIdentifier = Guid.NewGuid();
		}

        #endregion

        #region Properties

        public PointF Point1 { get; set; }

        public PointF Point2 { get; set; }

        public PointF Point3 { get; set; }

        public PointF Point4 { get; set; }

        #endregion

        #region Methods

        public override bool Contains(PointF point)
		{
            return PolygonHlp.IsInPolygon(new PointF[] { this.Point1, this.Point2, this.Point3, this.Point4 }, point);
		}
		
		public override void DrawSelf(Graphics grfx)
		{
            var points = new PointF[] { this.Point1, this.Point2, this.Point3, this.Point4 };
            grfx.FillPolygon(new SolidBrush(FillColor), points);
            var border = new Pen(BorderColor);
            border.DashStyle = DashStyle;
            grfx.DrawPolygon(border, points);

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
            var listOfPoints = new List<PointF>()
            {
                this.Point1,
                this.Point2,
                this.Point3,
                this.Point4
            };
            var center = PolygonHlp.GetCentroidGeneric(listOfPoints);
            var rotatedPoints = PolygonHlp.RotatePolygon(listOfPoints, center, radians);

            return new RectangleShape(rotatedPoints[0], rotatedPoints[1], rotatedPoints[2], 
                rotatedPoints[3], this.BorderColor, base.FillColor);
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
            stringBuilder.AppendLine("RectangleShape");
            stringBuilder.AppendLine("Point : X : " + Point1.X + " : Y : " + Point1.Y);
            stringBuilder.AppendLine("Point : X : " + Point2.X + " : Y : " + Point2.Y);
            stringBuilder.AppendLine("Point : X : " + Point3.X + " : Y : " + Point3.Y);
            stringBuilder.AppendLine("Point : X : " + Point4.X + " : Y : " + Point4.Y);
            stringBuilder.AppendLine("BorderColor : " + base.BorderColor.Name);
            stringBuilder.AppendLine("FillColor : " + ColorTranslator.ToHtml(base.FillColor));
            stringBuilder.AppendLine("DashStyle : " + base.DashStyle);
            stringBuilder.AppendLine("TemporaryFlag : " + base.TemporaryFlag);
            return stringBuilder.ToString();
        }

        #endregion
    }
}
