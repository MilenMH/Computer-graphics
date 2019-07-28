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
        public RectangleShape(float x, float y, float width, float height, Color borderColor, 
            Color fillColor, DashStyle dashStyle = GlobalConstants.DefaultDashStyle,
            bool temporaryFlag = false)
		{
            base.X = x;
            base.Y = y;
            this.Width = width;
            this.Height = height;
            base.BorderColor = borderColor;
            base.FillColor = fillColor;
            base.TemporaryFlag = temporaryFlag;
            base.DashStyle = dashStyle;
            base.UniqueIdentifier = Guid.NewGuid();
		}

        #endregion

        #region Properties

        public float Width { get; set; }

        public float Height { get; set; }

        #endregion

        #region Methods

        public override bool Contains(PointF point)
		{
            return point.X >= base.X
                && point.X <= base.X + this.Width
                && point.Y >= base.Y
                && point.Y <= base.Y + this.Height;
		}
		
		public override void DrawSelf(Graphics grfx)
		{
			
			grfx.FillRectangle(new SolidBrush(FillColor),base.X, base.Y, this.Width, this.Height);
            var border = new Pen(BorderColor);
            border.DashStyle = DashStyle;
            grfx.DrawRectangle(border, base.X, base.Y, this.Width, this.Height);
			 
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

            float width =  maxX - minX;
            float height = maxY - minY;

            return new RectangleShape(minX, minY, width, height, this.BorderColor, base.FillColor);

        }

        public override void Enlarge()
        {
            this.X -= 1;
            this.Y -= 1;
            this.Width += 2;
            this.Height += 2;
        }

        public override void Shrink()
        {
            this.X += 1;
            this.Y += 1;
            this.Width -= 2;
            this.Height -= 2;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("RectangleShape");
            stringBuilder.AppendLine("X : " + base.X);
            stringBuilder.AppendLine("Y : " + base.Y);
            stringBuilder.AppendLine("Width : " + this.Width);
            stringBuilder.AppendLine("Height : " + this.Height);
            stringBuilder.AppendLine("BorderColor : " + base.BorderColor.Name);
            stringBuilder.AppendLine("FillColor : " + ColorTranslator.ToHtml(base.FillColor));
            stringBuilder.AppendLine("DashStyle : " + base.DashStyle);
            stringBuilder.AppendLine("TemporaryFlag : " + base.TemporaryFlag);
            return stringBuilder.ToString();
        }

        #endregion
    }
}
