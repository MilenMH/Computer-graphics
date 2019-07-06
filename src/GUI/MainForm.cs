using Draw.src.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Draw
{
    public partial class MainForm : Form
    {
        private DialogProcessor dialogProcessor = new DialogProcessor();

        private Dictionary<string, Color> colorsDictionary;

        public MainForm()
        {
            InitializeComponent();
            this.InitializeColorDictionary();
        }

        private void InitializeColorDictionary()
        {
            this.colorsDictionary = typeof(Color)
              .GetProperties(BindingFlags.Public | BindingFlags.Static)
              .Where(p => p.PropertyType == typeof(Color))
              .ToDictionary(p => p.Name,
                            p => (Color)p.GetValue(null, null));
        }

        void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        void ViewPortPaint(object sender, PaintEventArgs e)
        {
            dialogProcessor.ReDraw(sender, e);
        }

        void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (ButtonMainNavigator.Checked)
            {
                dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
                if (dialogProcessor.Selection != null)
                {
                    statusBar.Items[0].Text = "Last action : selection of a shape";
                    dialogProcessor.IsDragging = true;
                    dialogProcessor.LastLocation = e.Location;
                    RerenderMainCanvas();
                }
            }
            else if (ButtonMultiSelect.Checked)
            {
                dialogProcessor.OnMouseDownPoint_ForRotation = e.Location;
            }
            else if (ButtonFillColor.Checked)
            {
                dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
                if (dialogProcessor.Selection != null)
                {
                    dialogProcessor.Selection.FillColor = dialogProcessor.FillColor;
                    RerenderMainCanvas();
                }
            }
        }

        void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dialogProcessor.IsDragging)
            {
                dialogProcessor.TranslateTo(e.Location);
                viewPort.Invalidate();
            }
            statusBar.Items[0].Text = $"X:{e.X}  Y:{e.Y}";
        }

        void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dialogProcessor.IsDragging = false;
            if (ButtonMultiSelect.Checked)
            {
                dialogProcessor.OnMouseUpPoint_ForRotation = e.Location;
                ButtonMultiSelect.Checked = false;
                var setOfShapesThatHasToBeRotated = TraverseOverSelectedMatrix(false);
                ChageBorderOfSetOfShapes(this.dialogProcessor.ShapeList, GlobalConstants.DefaultDashStyle);
                ChageBorderOfSetOfShapes(setOfShapesThatHasToBeRotated, DashStyle.Dot);
                RerenderMainCanvas();
            }
        }

        void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomRectangle();

            statusBar.Items[0].Text = "Last action : Drawing a rectangle";

            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);

            viewPort.Invalidate();
        }

        private void DrawTriangleSpeedButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomTriangle();

            statusBar.Items[0].Text = "Last action : Drawing a triangle";

            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);

            viewPort.Invalidate();
        }

        private void DrawEllipseSpeedButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllipse();

            statusBar.Items[0].Text = "Last action : Drawing a ellipse";

            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);

            viewPort.Invalidate();
        }

        private void SetFillColor(object sender, EventArgs e)
        {
            var buttonColor = sender.ToString().Replace("ButtonFillColor", "");
            var colorProperty = this.colorsDictionary[buttonColor];
            dialogProcessor.FillColor = colorProperty;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
            statusBar.Items[0].Text = $"Last Action : {sender.ToString()}";
        }

        private void RotateRight(object sender, EventArgs e)
        {
            //Active buttons
            ButtonMainNavigator.Checked = false;
            ButtonMultiSelect.Checked = false;

            if (dialogProcessor.ShapeList != null)
            {
                TraverseOverSelectedMatrix(true);
                var setOfShapes = TraverseOverSelectedMatrix(false);
                ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, false);
                ChageBorderOfSetOfShapes(setOfShapes, DashStyle.Dot);
                RerenderMainCanvas();
                
            }
        }

        private HashSet<Shape> TraverseOverSelectedMatrix(bool rotate)
        {
            var minXCordinate = Convert.ToInt32(Math.Min(dialogProcessor.OnMouseDownPoint_ForRotation.X, dialogProcessor.OnMouseUpPoint_ForRotation.X));
            var maxXCordinate = Convert.ToInt32(Math.Max(dialogProcessor.OnMouseDownPoint_ForRotation.X, dialogProcessor.OnMouseUpPoint_ForRotation.X));

            var minYCordinate = Convert.ToInt32(Math.Min(dialogProcessor.OnMouseDownPoint_ForRotation.Y, dialogProcessor.OnMouseUpPoint_ForRotation.Y));
            var maxYCordinate = Convert.ToInt32(Math.Max(dialogProcessor.OnMouseDownPoint_ForRotation.Y, dialogProcessor.OnMouseUpPoint_ForRotation.Y));

            var setOfShapesWhichNeedsToBeRotated = new HashSet<Shape>();
            var shapeListCount = dialogProcessor.ShapeList.Count;

            for (int i = shapeListCount - 1; i >= 0; i--)
            {
                var shape = dialogProcessor.ShapeList[i];
                for (int tempX = minXCordinate; tempX < maxXCordinate; tempX++)
                {
                    for (int tempY = minYCordinate; tempY < maxYCordinate; tempY++)
                    {
                        if (!setOfShapesWhichNeedsToBeRotated.Contains(shape) && shape.Contains(new PointF(tempX, tempY)))
                        {
                            setOfShapesWhichNeedsToBeRotated.Add(shape);
                            if (rotate)
                            {
                                var rotated = shape.NewShapeRotatedToRigth();
                                setOfShapesWhichNeedsToBeRotated.Add(rotated);
                                dialogProcessor.ShapeList.Remove(shape);
                                dialogProcessor.ShapeList.Add(rotated);
                            }
                        }
                    }
                }
            }
            return setOfShapesWhichNeedsToBeRotated;
        }

        private void ResetRotationProcess(DashStyle dashStyle, bool rerenderMainCanvas, bool resetRotationMatrix)
        {
            ChageBorderOfSetOfShapes(this.dialogProcessor.ShapeList, dashStyle);
            if (rerenderMainCanvas)
            {
                RerenderMainCanvas();
            }
            if (resetRotationMatrix)
            {
                SetRotationMatrixToDefaultValues();
            }
        }

        private void ChageBorderOfSetOfShapes(IEnumerable<Shape> setOfShapes, DashStyle dashStyle)
        {
            foreach (var shape in setOfShapes)
            {
                var oldColor = shape.BorderColor.Color;
                var newPen = new Pen(oldColor);
                newPen.DashStyle = dashStyle;
                shape.BorderColor = newPen;
            }
        }

        private void SetRotationMatrixToDefaultValues()
        {
            dialogProcessor.OnMouseDownPoint_ForRotation = new PointF(0F, 0F);
            dialogProcessor.OnMouseUpPoint_ForRotation = new PointF(0F, 0F);
        }

        private void RerenderMainCanvas()
        {
            viewPort.Invalidate();
        }

        private void OnMainNavigatorClick(object sender, EventArgs e)
        {
            ButtonMultiSelect.Checked = false;
            ButtonFillColor.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
        }

        private void OnMultiSelectClick(object sender, EventArgs e)
        {
            ButtonMainNavigator.Checked = false;
            ButtonFillColor.Checked = false;
        }

        private void FillColor(object sender, EventArgs e)
        {
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
        }
    }
}
