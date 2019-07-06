using System;
using System.Collections.Generic;
using System.Drawing;

namespace Draw.src.Helpers
{
    public class PolygonHlp
    {
        
        public static PointF GetCentroidGeneric(List<PointF> polygon)
        {
            double centerX = 0.0f;
            double centerY = 0.0f;
            var polygonCount = polygon.Count;

            for (int i = 0; i < polygon.Count; i++)
            {
                centerX += polygon[i].X;
                centerY += polygon[i].Y;
            }
            
            var resultX = centerX / polygonCount;
            var resultY = centerY / polygonCount;
            return new PointF((float)resultX, (float)resultY);
        }


        public static List<PointF> RotatePolygon(List<PointF> polygon, PointF centroid, double angle)
        {
            for (int i = 0; i < polygon.Count; i++)
            {
                polygon[i] = RotatePoint(polygon[i], centroid, angle);
            }

            return polygon;
        }

        public static PointF RotatePoint(PointF point, PointF centroid, double angle)
        {
            float x = (float)(centroid.X + ((point.X - centroid.X) * Math.Cos(angle) - (point.Y - centroid.Y) * Math.Sin(angle)));

            float y = (float)(centroid.Y + ((point.X - centroid.X) * Math.Sin(angle) + (point.Y - centroid.Y) * Math.Cos(angle)));

            return new PointF(x, y);
        }
    }
}
