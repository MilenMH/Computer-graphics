using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

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

        public static List<PointF> GetEnlargedPolygon(
            List<PointF> old_points, float offset)
        {
            List<PointF> enlarged_points = new List<PointF>();
            int num_points = old_points.Count;
            for (int j = 0; j < num_points; j++)
            {
                int i = (j - 1);
                if (i < 0) i += num_points;
                int k = (j + 1) % num_points;

                Vector v1 = new Vector(
                    old_points[j].X - old_points[i].X,
                    old_points[j].Y - old_points[i].Y);
                v1.Normalize();
                v1 *= offset;
                Vector n1 = new Vector(-v1.Y, v1.X);

                PointF pij1 = new PointF(
                    (float)(old_points[i].X + n1.X),
                    (float)(old_points[i].Y + n1.Y));
                PointF pij2 = new PointF(
                    (float)(old_points[j].X + n1.X),
                    (float)(old_points[j].Y + n1.Y));

                Vector v2 = new Vector(
                    old_points[k].X - old_points[j].X,
                    old_points[k].Y - old_points[j].Y);
                v2.Normalize();
                v2 *= offset;
                Vector n2 = new Vector(-v2.Y, v2.X);

                PointF pjk1 = new PointF(
                    (float)(old_points[j].X + n2.X),
                    (float)(old_points[j].Y + n2.Y));
                PointF pjk2 = new PointF(
                    (float)(old_points[k].X + n2.X),
                    (float)(old_points[k].Y + n2.Y));

                bool lines_intersect, segments_intersect;
                PointF poi, close1, close2;
                FindIntersection(pij1, pij2, pjk1, pjk2,
                    out lines_intersect, out segments_intersect,
                    out poi, out close1, out close2);

                enlarged_points.Add(poi);
            }

            return enlarged_points;
        }

        private static void FindIntersection(
            PointF p1, PointF p2, PointF p3, PointF p4,
            out bool lines_intersect, out bool segments_intersect,
            out PointF intersection,
            out PointF close_p1, out PointF close_p2)
        {
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                    / denominator;
            if (float.IsInfinity(t1))
            {
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointF(float.NaN, float.NaN);
                close_p1 = new PointF(float.NaN, float.NaN);
                close_p2 = new PointF(float.NaN, float.NaN);
                return;
            }
            lines_intersect = true;

            float t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                    / -denominator;

            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            segments_intersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }
    }
}
