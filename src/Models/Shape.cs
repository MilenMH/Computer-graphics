﻿using Draw.src.Helpers;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw.src.Models
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

        public Color BorderColor { get; set; }

        public Guid UniqueIdentifier { get; set; }

        public bool TemporaryFlag { get; set; }

        public DashStyle DashStyle { get; set; }

        #endregion

        public bool Equals(Shape other)
        {
            return other.UniqueIdentifier == this.UniqueIdentifier;
        }

        public override abstract string ToString();

        #region Custom Methods

        public abstract bool Contains(PointF point);

        public abstract void DrawSelf(Graphics grfx);

        public abstract void MoveToNextDestination(PointF next, PointF last);

        public abstract Shape NewShapeRotatedToRigth(float radians = GlobalConstants.RadiansRepresentationOfThirtyDegrees);

        public abstract void Enlarge();

        public abstract void Shrink();

        #endregion
    }
}
