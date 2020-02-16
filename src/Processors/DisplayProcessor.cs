using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Draw.src.Models;

namespace Draw
{
	public class DisplayProcessor
	{
		#region Constructor
		
		public DisplayProcessor()
		{
            this.ShapeList = new Dictionary<int, List<Shape>>();
            this.CurrentTab = 1;
            this.ShapeList.Add(CurrentTab, new List<Shape>());
        }

        #endregion

        #region Properties

        public Dictionary<int, List<Shape>> ShapeList { get; set; }

        public int CurrentTab { get; set; }

        #endregion

        #region Drawing


        public void ReDraw(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Draw(e.Graphics);
		}
		
		public virtual void Draw(Graphics grfx)
		{
            if (ShapeList.ContainsKey(CurrentTab))
            {
                foreach (Shape item in ShapeList[CurrentTab])
                {
                    DrawShape(grfx, item);
                }
            }
		}
		
		public virtual void DrawShape(Graphics grfx, Shape item)
		{
			item.DrawSelf(grfx);
		}
		
		#endregion
	}
}
