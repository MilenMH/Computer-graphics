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
            dialogProcessor.OnMouseDownPoint = e.Location;
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
            dialogProcessor.OnMouseUpPoint = new PointF(e.X, e.Y);
            dialogProcessor.IsDragging = false;
            if (ButtonMultiSelect.Checked)
            {
                dialogProcessor.OnMouseUpPoint = e.Location;
                ButtonMultiSelect.Checked = false;
                var setOfShapesThatHasToBeRotated = TraverseOverSelectedMatrix(false);
                ChageBorderOfSetOfShapes(this.dialogProcessor.ShapeList, GlobalConstants.DefaultDashStyle);
                ChageBorderOfSetOfShapes(setOfShapesThatHasToBeRotated, DashStyle.Dot);
            }

            var onMouseDownPoint = dialogProcessor.OnMouseDownPoint;
            var onMouseUpPoint = dialogProcessor.OnMouseUpPoint;
            var minX = Math.Min(onMouseDownPoint.X, onMouseUpPoint.X);
            var minY = Math.Min(onMouseDownPoint.Y, onMouseUpPoint.Y);
            var maxX = Math.Max(onMouseDownPoint.X, onMouseUpPoint.X);
            var maxY = Math.Max(onMouseDownPoint.Y, onMouseUpPoint.Y);
            var width = maxX - minX;
            var height = maxY - minY;

            if (ButtonDrowElipse.Checked)
            {
                dialogProcessor.AddEllipse(minX, minY, width, height);
            }
            if (ButtonDrowRectangle.Checked)
            {
                dialogProcessor.AddRectangle(minX, minY, width, height);
            }
            if (ButtonDrowTriangle.Checked)
            {
                dialogProcessor.AddRandomTriangle(
                    new PointF(minX, maxY), new PointF(minX, minY), new PointF(maxX, minY));
            }
            RerenderMainCanvas();
        }

        void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowElipse.Checked = false;
            ButtonFillColor.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            viewPort.Invalidate();
        }

        private void DrawTriangleSpeedButtonClick(object sender, EventArgs e)
        {
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowElipse.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            viewPort.Invalidate();
        }

        private void DrawEllipseSpeedButtonClick(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            viewPort.Invalidate();
        }

        private void OnMainNavigatorClick(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDrowElipse.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
        }

        private void OnMultiSelectClick(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDrowElipse.Checked = false;
            ButtonDrowRectangle.Checked = false;
        }

        private void FillColor(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowElipse.Checked = false;
            ButtonDrowRectangle.Checked = false;
        }

        private void SetFillColor(object sender, EventArgs e)
        {
            var buttonColor = sender.ToString().Replace("ButtonFillColor", "");
            var colorProperty = this.colorsDictionary[buttonColor];
            dialogProcessor.FillColor = colorProperty;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
            statusBar.Items[0].Text = $"Last Action : {sender.ToString()}";
        }

        #region Rotation Process

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
            var minXCordinate = Convert.ToInt32(Math.Min(dialogProcessor.OnMouseDownPoint.X, dialogProcessor.OnMouseUpPoint.X));
            var maxXCordinate = Convert.ToInt32(Math.Max(dialogProcessor.OnMouseDownPoint.X, dialogProcessor.OnMouseUpPoint.X));

            var minYCordinate = Convert.ToInt32(Math.Min(dialogProcessor.OnMouseDownPoint.Y, dialogProcessor.OnMouseUpPoint.Y));
            var maxYCordinate = Convert.ToInt32(Math.Max(dialogProcessor.OnMouseDownPoint.Y, dialogProcessor.OnMouseUpPoint.Y));

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

        private void SetRotationMatrixToDefaultValues()
        {
            dialogProcessor.OnMouseDownPoint = new PointF(0F, 0F);
            dialogProcessor.OnMouseUpPoint = new PointF(0F, 0F);
        }

        #endregion

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

        private void RerenderMainCanvas()
        {
            viewPort.Invalidate();
        }
    }
}
