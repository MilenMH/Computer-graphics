using System;
using System.Drawing;

namespace Draw.src.Helpers
{
    public class DimentionCalculator
    {

        public static Sextuple<float, float, float, float, float, float> GetShapesParamsByTwoPoints
            (PointF startPoint, PointF endPoint)
        {
            float minX = Math.Min(startPoint.X, endPoint.X);
            float minY = Math.Min(startPoint.Y, endPoint.Y);
            float maxX = Math.Max(startPoint.X, endPoint.X);
            float maxY = Math.Max(startPoint.Y, endPoint.Y);
            float width = maxX - minX;
            float height = maxY - minY;
            return new Sextuple<float, float, float, float, float, float>(minX, minY, maxX, maxY, width, height);
        }

        public static Sextuple<float, float, float, float, float, float> GetShapesParamsByThreePoints
            (PointF point1, PointF point2, PointF point3)
        {
            float minX = Math.Min(Math.Min(point1.X, point2.X), point3.X);
            float minY = Math.Min(Math.Min(point1.Y, point2.Y), point3.Y);
            float maxX = Math.Max(Math.Max(point1.X, point2.X), point3.X);
            float maxY = Math.Max(Math.Max(point1.Y, point2.Y), point3.Y);
            float width = maxX - minX;
            float height = maxY - minY;
            return new Sextuple<float, float, float, float, float, float>(minX, minY, maxX, maxY, width, height);
        }
    }
}
