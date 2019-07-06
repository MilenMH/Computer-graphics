using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Draw
{
	public class DialogProcessor : DisplayProcessor
	{
		
		public DialogProcessor()
		{
            Random = new Random();
            FillColor = Color.White;
        }

        public Random Random { get; set; }

        public Shape Selection { get; set; }

        public bool IsDragging { get; set;  }
		
		public PointF LastLocation { get; set; }

        public Color FillColor { get; set; }

        public PointF OnMouseDownPoint_ForRotation { get; set; }

        public PointF OnMouseUpPoint_ForRotation { get; set; }

        public void AddRandomRectangle()
		{
			int x = Random.Next(100,1000);
			int y = Random.Next(100,600);

            //PointF first = new PointF(x, y);
            //PointF second = new PointF(x + 50, y);
            //PointF third = new PointF(x, y + 50);
            //PointF fourth = new PointF(x +50, y + 50);
            //
            //var listOfPoints = new List<PointF>() { first, second, third, fourth };

            Shape rect = new RectangleShape(x,y,100,200, Pens.Black, Color.White);
			rect.FillColor = Color.White;

			ShapeList.Add(rect);
		}

        public void AddRandomTriangle()
        {
            int x = Random.Next(100, 1000);
            int y = Random.Next(100, 600);
            PointF first = new PointF(x, y);
            PointF second = new PointF(x + 50, y);
            PointF third = new PointF(x, y + 50);

            Shape rect = new TriangleShape(first, second, third, Pens.Black, Color.White);
            rect.FillColor = Color.White;

            ShapeList.Add(rect);

        }

        public void AddRandomEllipse()
        {
            int x = Random.Next(100, 1000);
            int y = Random.Next(100, 600);

            Shape rect = new EllipseShape(100, 100, 100, 200, Pens.Black, Color.White);
            rect.FillColor = Color.White;

            ShapeList.Add(rect);
        }

        public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){
					return ShapeList[i];
				}	
			}
			return null;
		}
		
		public void TranslateTo(PointF p)
		{
            if (this.Selection != null)
            {
                this.Selection.MoveToNextDestination(p, this.LastLocation);
                this.LastLocation = p;
            }
        }
    }
}
