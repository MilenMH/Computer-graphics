using Draw.src.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Draw
{
	public class RectangleShape : Shape
	{
		#region Constructor
		
		public RectangleShape(float x, float y, float width, float height, Pen borderColor, Color fillColor)
		{
            base.X = x;
            base.Y = y;
            this.Width = width;
            this.Height = height;
            base.BorderColor = borderColor;
            base.FillColor = fillColor;
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
			grfx.DrawRectangle(BorderColor,base.X, base.Y, this.Width, this.Height);
			
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

        #endregion
    }
}
