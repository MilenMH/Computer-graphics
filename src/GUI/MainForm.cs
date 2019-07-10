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
            if (ButtonMultiMove.Checked)
            {
                dialogProcessor.MultiSelection = TraverseOverSelectedMatrix(false).ToList();

                dialogProcessor.IsDragging = true;
                dialogProcessor.LastLocation = e.Location;
            }

            dialogProcessor.OnMouseDownPoint = e.Location;
            dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
            if (ButtonMainNavigator.Checked)
            {
                if (dialogProcessor.Selection != null)
                {
                    dialogProcessor.IsDragging = true;
                    dialogProcessor.LastLocation = e.Location;
                }
            }
            if (ButtonFillColor.Checked)
            {
                if (dialogProcessor.Selection != null)
                {
                    dialogProcessor.Selection.FillColor = dialogProcessor.FillColor;
                }
            }
            if (ButtonDelete.Checked)
            {
                if (dialogProcessor.Selection != null)
                {
                    dialogProcessor.ShapeList.Remove(dialogProcessor.Selection);
                }
            }

            if (ButtonDrowRectangle.Checked)
            {
                dialogProcessor.DrowTemporaryRectangle = true;
            }
            if (ButtonDrowTriangle.Checked)
            {
                dialogProcessor.DrowTemporaryTriangle = true;
            }
            if (ButtonDrowEllipse.Checked)
            {
                dialogProcessor.DrowTemporaryEllipse = true;
            }
            RerenderMainCanvas();
        }

        void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dialogProcessor.IsDragging && !ButtonMultiMove.Checked)
            {
                dialogProcessor.TranslateTo(dialogProcessor.Selection, dialogProcessor.LastLocation, e.Location);
                dialogProcessor.LastLocation = e.Location;

            }
            if (ButtonMultiMove.Checked && dialogProcessor.IsDragging && dialogProcessor.MultiSelection != null)
            {

                dialogProcessor.MultiMoveTo(dialogProcessor.LastLocation, e.Location);
                dialogProcessor.LastLocation = e.Location;

            }
            var startPoint = dialogProcessor.OnMouseDownPoint;
            var endPoint = e.Location;
            var shapeParams = GetShapesParamsByTwoPoints(startPoint, endPoint);
            if (ButtonDrowRectangle.Checked && dialogProcessor.DrowTemporaryRectangle)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddRectangle(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6, true);
            }
            if (ButtonDrowTriangle.Checked && dialogProcessor.DrowTemporaryTriangle)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddTriangle(
                    new PointF(shapeParams.Item1, shapeParams.Item4),
                    new PointF(shapeParams.Item1, shapeParams.Item2),
                    new PointF(shapeParams.Item3, shapeParams.Item2), true);
            }
            if (ButtonDrowEllipse.Checked && dialogProcessor.DrowTemporaryEllipse)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddEllipse(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6, true);
            }
            viewPort.Invalidate();
        }

        void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dialogProcessor.OnMouseUpPoint = e.Location;
            dialogProcessor.IsDragging = false;
            if (ButtonMultiSelect.Checked)
            {
                ButtonMultiSelect.Checked = false;
                var setOfShapesThatHasToBeRotated = TraverseOverSelectedMatrix(false);
                ChageBorderOfSetOfShapes(this.dialogProcessor.ShapeList, GlobalConstants.DefaultDashStyle);
                ChageBorderOfSetOfShapes(setOfShapesThatHasToBeRotated, DashStyle.Dot);
            }

            var shapeParams = GetShapesParamsByTwoPoints(
                dialogProcessor.OnMouseDownPoint, dialogProcessor.OnMouseUpPoint);


            if (ButtonDrowEllipse.Checked)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddEllipse(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6);
            }
            if (ButtonDrowRectangle.Checked)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddRectangle(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6);
            }
            if (ButtonDrowTriangle.Checked)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddTriangle(
                    new PointF(shapeParams.Item1, shapeParams.Item4), 
                    new PointF(shapeParams.Item1, shapeParams.Item2), 
                    new PointF(shapeParams.Item3, shapeParams.Item2));
            }
            if (ButtonMultiMove.Checked)
            {
                ButtonMultiMove.Checked = false;
                ChageBorderOfSetOfShapes(this.dialogProcessor.ShapeList, GlobalConstants.DefaultDashStyle);
                ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            }
            if (ButtonDrowRectangle.Checked && dialogProcessor.DrowTemporaryRectangle)
            {
                ButtonDrowRectangle.Checked = false;
                dialogProcessor.DrowTemporaryRectangle = false;
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
            }
            if (ButtonDrowTriangle.Checked && dialogProcessor.DrowTemporaryTriangle)
            {
                ButtonDrowTriangle.Checked = false;
                dialogProcessor.DrowTemporaryTriangle = false;
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
            }
            if (ButtonDrowEllipse.Checked && dialogProcessor.DrowTemporaryEllipse)
            {
                ButtonDrowEllipse.Checked = false;
                dialogProcessor.DrowTemporaryEllipse = false;
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
            }
            RerenderMainCanvas();
        }

        void DrawRectangleSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowEllipse.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            viewPort.Invalidate();
        }

        private void DrawTriangleSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowEllipse.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            viewPort.Invalidate();
        }

        private void DrawEllipseSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            viewPort.Invalidate();
        }

        private void OnMainNavigator_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDrowEllipse.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
        }

        private void OnMultiSelect_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDrowEllipse.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
        }

        private void FillColor_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowEllipse.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowEllipse.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonMultiMove.Checked = false;
        }

        private void ButtonMultiMove_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowEllipse.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
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

        private Sextuple<float, float, float, float, float, float> GetShapesParamsByTwoPoints(PointF startPoint, PointF endPoint)
        {
            float minX = Math.Min(startPoint.X, endPoint.X);
            float minY = Math.Min(startPoint.Y, endPoint.Y);
            float maxX = Math.Max(startPoint.X, endPoint.X);
            float maxY = Math.Max(startPoint.Y, endPoint.Y);
            float width = maxX - minX;
            float height = maxY - minY;
            return new Sextuple<float, float, float, float, float, float>(minX, minY, maxX, maxY, width, height);
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

        private void RerenderMainCanvas()
        {
            viewPort.Invalidate();
        }
    }
}
