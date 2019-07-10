using System;
using System.Drawing;

namespace Draw
{
	public abstract class Shape : IEquatable<Shape>
	{

        #region Constructors
		
		public Shape()
		{
		}
        #endregion

        #region Properties

        public float X { get; set; }

        public float Y { get; set; }

        public Color FillColor { get ; set; }

        public Pen BorderColor { get; set; }

        public Guid UniqueIdentifier { get; set; }

        public bool TemporaryFlag { get; set; }

        #endregion

        public bool Equals(Shape other)
        {
            return other.UniqueIdentifier == this.UniqueIdentifier;
        }

        #region Custom Methods

        public abstract bool Contains(PointF point);

        public abstract void DrawSelf(Graphics grfx);

        public abstract void MoveToNextDestination(PointF next, PointF last);

        public abstract Shape NewShapeRotatedToRigth();

        #endregion
    }
}
