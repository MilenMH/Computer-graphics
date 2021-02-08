using Draw.src.Attributes;
using Draw.src.Helpers;
using Draw.src.Interfaces;
using Draw.src.Models;
using Draw.src.Workers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewPortPaint(object sender, PaintEventArgs e)
        {
            dialogProcessor.ReDraw(sender, e);
        }

        private void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (ButtonMultiMove.Checked)
            {
                dialogProcessor.MultiSelection = TraverseOverSelectedMatrix(false).ToList();

                dialogProcessor.IsDragging = true;
                dialogProcessor.LastLocation = e.Location;
            }
            if (ButtonMultiSelect.Checked)
            {
                dialogProcessor.MultiSelectFlag = true;
            }

            dialogProcessor.OnMouseDownPoint = e.Location;
            dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);

            if (ButtonMainNavigator.Checked && dialogProcessor.Selection != null)
            {
                dialogProcessor.IsDragging = true;
                dialogProcessor.LastLocation = e.Location;
            }

            if (ButtonFillColor.Checked && dialogProcessor.Selection != null)
            {
                dialogProcessor.Selection.FillColor = dialogProcessor.Color;
            }

            if (ButtonBorderColor.Checked && dialogProcessor.Selection != null)
            {
                dialogProcessor.Selection.BorderColor = dialogProcessor.Color;
            }

            if (ButtonDelete.Checked && dialogProcessor.Selection != null)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].Remove(dialogProcessor.Selection);
            }

            if (ButtonDrawLine.Checked)
            {
                dialogProcessor.DrawTemporaryLine = true;
            }

            if (ButtonDrawRectangle.Checked)
            {
                dialogProcessor.DrawTemporaryRectangle = true;
            }
            if (ButtonDrowTriangle.Checked)
            {
                dialogProcessor.DrawTemporaryTriangle = true;
            }
            if (ButtonDrawEllipse.Checked)
            {
                dialogProcessor.DrawTemporaryEllipse = true;
            }
            if (ButtonDrawReuleauxTriangle.Checked)
            {
                dialogProcessor.DrawTemporaryReuleauxTriangle = true;
            }
            if (ButtonDrawEnvelope.Checked)
            {
                dialogProcessor.DrawTemporaryEnvelope = true;
            }
            if (ButtonDrawGenericCircle.Checked)
            {
                dialogProcessor.DrawTemporaryGenericCircle = true;
            }
            if (ButtonCopy.Checked && dialogProcessor.Selection != null)
            {
                dialogProcessor.DrawTemporaryCopyShape = true;
                dialogProcessor.LastLocation = e.Location;


                JsonSerializerSettings settings = JSONSaveBehaviourWorker.GetJSONSettings();
                var serialized = JsonConvert.SerializeObject(dialogProcessor.Selection, settings);

                var copyOfSelection = JsonConvert.DeserializeObject<Shape>(serialized, settings);
                copyOfSelection.UniqueIdentifier = Guid.NewGuid();
                copyOfSelection.TemporaryFlag = true;
                dialogProcessor.SelectionCopy = copyOfSelection;
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].Add(copyOfSelection);

            }
            if (ButtonPlus.Checked && dialogProcessor.Selection != null)
            {
                dialogProcessor.Selection.Enlarge();
            }
            if (ButtonMinus.Checked && dialogProcessor.Selection != null)
            {
                var selectionGuid = dialogProcessor.Selection.UniqueIdentifier;
                dialogProcessor.Selection.Shrink();
            }
            RerenderMainCanvas();
        }

        private void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
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
            var shapeParams = DimentionCalculator.GetShapesParamsByTwoPoints(startPoint, endPoint);

            if (ButtonMultiSelect.Checked && dialogProcessor.MultiSelectFlag)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddRectangle(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6, DashStyle.Dot, true, 0);

            }

            if (ButtonDrawRectangle.Checked && dialogProcessor.DrawTemporaryRectangle)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddRectangle(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6, DashStyle.Dot, true);
            }
            if (ButtonDrowTriangle.Checked && dialogProcessor.DrawTemporaryTriangle)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddTriangle(
                    new PointF(shapeParams.Item1, shapeParams.Item4),
                    new PointF(shapeParams.Item1, shapeParams.Item2),
                    new PointF(shapeParams.Item3, shapeParams.Item2), DashStyle.Dot, true);
            }
            if (ButtonDrawLine.Checked && dialogProcessor.DrawTemporaryLine)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddLine(startPoint, endPoint, DashStyle.Dot, true);
            }
            if (ButtonDrawEllipse.Checked && dialogProcessor.DrawTemporaryEllipse)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddEllipse(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6, DashStyle.Dot, true);
            }
            if (ButtonDrawReuleauxTriangle.Checked && dialogProcessor.DrawTemporaryReuleauxTriangle)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddReuleauxTriangle(startPoint, endPoint, DashStyle.Dot, true);
            }
            if (ButtonDrawEnvelope.Checked && dialogProcessor.DrawTemporaryEnvelope)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddEnvelope(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6, DashStyle.Dot, true);
            }
            if (ButtonDrawGenericCircle.Checked && dialogProcessor.DrawTemporaryGenericCircle)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddGenericCircle(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6, DashStyle.Dot, true);
            }
            if (ButtonCopy.Checked && dialogProcessor.DrawTemporaryCopyShape && dialogProcessor.SelectionCopy != null)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.SelectionCopy.MoveToNextDestination(e.Location, dialogProcessor.LastLocation);
                dialogProcessor.SelectionCopy.UniqueIdentifier = Guid.NewGuid();
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].Add(dialogProcessor.SelectionCopy);
                dialogProcessor.LastLocation = e.Location;
            }
            RerenderMainCanvas();
        }

        private void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dialogProcessor.OnMouseUpPoint = e.Location;
            dialogProcessor.IsDragging = false;
            if (ButtonMultiSelect.Checked)
            {
                ButtonMultiSelect.Checked = false;
                dialogProcessor.MultiSelectFlag = false;
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                var setOfShapesThatHasToBeRotated = TraverseOverSelectedMatrix(false);
                ChageBorderOfSetOfShapes(this.dialogProcessor.ShapeList[dialogProcessor.CurrentTab], GlobalConstants.DefaultDashStyle);
                ChageBorderOfSetOfShapes(setOfShapesThatHasToBeRotated, DashStyle.Dot);
            }

            var shapeParams = DimentionCalculator.GetShapesParamsByTwoPoints(
                dialogProcessor.OnMouseDownPoint, dialogProcessor.OnMouseUpPoint);


            if (ButtonFillColor.Checked)
            {
                ButtonFillColor.Checked = false;
            }

            if (ButtonBorderColor.Checked)
            {
                ButtonBorderColor.Checked = false;
            }

            if (ButtonMultiMove.Checked)
            {
                ButtonMultiMove.Checked = false;
                ChageBorderOfSetOfShapes(this.dialogProcessor.ShapeList[dialogProcessor.CurrentTab], GlobalConstants.DefaultDashStyle);
                ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            }
            if (ButtonDrawRectangle.Checked && dialogProcessor.DrawTemporaryRectangle)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddRectangle(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6,
                    DashStyle.Solid, false);
                ButtonDrawRectangle.Checked = false;
                dialogProcessor.DrawTemporaryRectangle = false;
            }
            if (ButtonDrowTriangle.Checked && dialogProcessor.DrawTemporaryTriangle)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddTriangle(
                    new PointF(shapeParams.Item1, shapeParams.Item4),
                    new PointF(shapeParams.Item1, shapeParams.Item2),
                    new PointF(shapeParams.Item3, shapeParams.Item2),
                    DashStyle.Solid, false);
                ButtonDrowTriangle.Checked = false;
                dialogProcessor.DrawTemporaryTriangle = false;
            }
            if (ButtonDrawEllipse.Checked && dialogProcessor.DrawTemporaryEllipse)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddEllipse(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6,
                    DashStyle.Solid, false);
                ButtonDrawEllipse.Checked = false;
                dialogProcessor.DrawTemporaryEllipse = false;
            }
            if (ButtonDrawLine.Checked && dialogProcessor.DrawTemporaryLine)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddLine(dialogProcessor.OnMouseDownPoint, dialogProcessor.OnMouseUpPoint, DashStyle.Solid, false);
                ButtonDrawLine.Checked = false;
                dialogProcessor.DrawTemporaryLine = false;
            }
            if (ButtonDrawReuleauxTriangle.Checked && dialogProcessor.DrawTemporaryReuleauxTriangle)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddReuleauxTriangle(dialogProcessor.OnMouseDownPoint, dialogProcessor.OnMouseUpPoint, DashStyle.Solid, false);
                ButtonDrawReuleauxTriangle.Checked = false;
                dialogProcessor.DrawTemporaryReuleauxTriangle = false;
            }
            if (ButtonDrawEnvelope.Checked && dialogProcessor.DrawTemporaryEnvelope)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddEnvelope(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6,
                    DashStyle.Solid, false);
                ButtonDrawEnvelope.Checked = false;
                dialogProcessor.DrawTemporaryEnvelope = false;
            }
            if (ButtonDrawGenericCircle.Checked && dialogProcessor.DrawTemporaryGenericCircle)
            {
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddGenericCircle(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6,
                    DashStyle.Solid, false);
                ButtonDrawGenericCircle.Checked = false;
                dialogProcessor.DrawTemporaryGenericCircle = false;
            }
            if (ButtonCopy.Checked && dialogProcessor.DrawTemporaryCopyShape)
            {
                ButtonCopy.Checked = false;
                dialogProcessor.DrawTemporaryCopyShape = false;
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.SelectionCopy.TemporaryFlag = false;
                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].Add(dialogProcessor.SelectionCopy);
                dialogProcessor.SelectionCopy = null;
            }
            RerenderMainCanvas();
        }

        private void DrawRectangleSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            viewPort.Invalidate();
        }

        private void DrawTriangleSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            viewPort.Invalidate();
        }

        private void DrawEllipseSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, false, true);
            viewPort.Invalidate();
        }

        private void OnMainNavigator_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
            viewPort.Invalidate();
        }

        private void OnMultiSelect_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
        }

        private void FillColor_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
            viewPort.Invalidate();
        }


        private void BorderColor_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonCopy.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
            viewPort.Invalidate();
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonDelete.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
            viewPort.Invalidate();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
            viewPort.Invalidate();
        }

        private void ButtonMultiMove_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
        }

        private void Plus_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
        }

        private void Minus_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonPlus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
        }

        private void DrawRelauxTriangleSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawLine.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
        }

        private void DrawLineSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonDrawEnvelope.Checked = false;
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonPlus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
        }

        private void DrawEnvelopeSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonPlus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawGenericCircle.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
        }

        private void DrawGenericCircleSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonPlus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawEnvelope.Checked = false;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
        }

        private void SetFillColor(object sender, EventArgs e)
        {
            var buttonColor = sender.ToString().Replace("ButtonFillColor", "");
            var colorProperty = this.colorsDictionary[buttonColor];
            dialogProcessor.Color = colorProperty;
            ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, true);
            statusBar.Items[0].Text = $"Last Action : {sender.ToString()}";
        }

        #region Rotation Process

        private void RotateRight(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;

            if (dialogProcessor.ShapeList[dialogProcessor.CurrentTab] != null)
            {
                TraverseOverSelectedMatrix(true);
                var setOfShapes = TraverseOverSelectedMatrix(false);
                ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, false);
                ChageBorderOfSetOfShapes(setOfShapes, DashStyle.Dot);
                RerenderMainCanvas();

            }
        }

        private void RotateLeft(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrawEllipse.Checked = false;
            ButtonDrawRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonPlus.Checked = false;
            ButtonMinus.Checked = false;
            ButtonDrawReuleauxTriangle.Checked = false;
            ButtonDrawLine.Checked = false;

            if (dialogProcessor.ShapeList[dialogProcessor.CurrentTab] != null)
            {
                TraverseOverSelectedMatrix(true, GlobalConstants.RadiansRepresentationOfThreeHundredAndThirtyDegrees);
                var setOfShapes = TraverseOverSelectedMatrix(false);
                ResetRotationProcess(GlobalConstants.DefaultDashStyle, true, false);
                ChageBorderOfSetOfShapes(setOfShapes, DashStyle.Dot);
                RerenderMainCanvas();

            }

        }

        private HashSet<Shape> TraverseOverSelectedMatrix(bool rotate, float radians = GlobalConstants.RadiansRepresentationOfThirtyDegrees)
        {
            var minXCordinate = Convert.ToInt32(Math.Min(dialogProcessor.OnMouseDownPoint.X, dialogProcessor.OnMouseUpPoint.X));
            var maxXCordinate = Convert.ToInt32(Math.Max(dialogProcessor.OnMouseDownPoint.X, dialogProcessor.OnMouseUpPoint.X));

            var minYCordinate = Convert.ToInt32(Math.Min(dialogProcessor.OnMouseDownPoint.Y, dialogProcessor.OnMouseUpPoint.Y));
            var maxYCordinate = Convert.ToInt32(Math.Max(dialogProcessor.OnMouseDownPoint.Y, dialogProcessor.OnMouseUpPoint.Y));

            var setOfShapesWhichNeedsToBeRotated = new HashSet<Shape>();
            var shapeListCount = dialogProcessor.ShapeList[dialogProcessor.CurrentTab].Count;

            for (int i = shapeListCount - 1; i >= 0; i--)
            {
                var shape = dialogProcessor.ShapeList[dialogProcessor.CurrentTab][i];
                for (int tempX = minXCordinate; tempX < maxXCordinate; tempX++)
                {
                    for (int tempY = minYCordinate; tempY < maxYCordinate; tempY++)
                    {
                        if (!setOfShapesWhichNeedsToBeRotated.Contains(shape) && shape.Contains(new PointF(tempX, tempY)))
                        {
                            setOfShapesWhichNeedsToBeRotated.Add(shape);
                            if (rotate)
                            {
                                var rotated = shape.NewShapeRotatedToRigth(radians);
                                setOfShapesWhichNeedsToBeRotated.Add(rotated);
                                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].Remove(shape);
                                dialogProcessor.ShapeList[dialogProcessor.CurrentTab].Add(rotated);
                            }
                        }
                    }
                }
            }
            return setOfShapesWhichNeedsToBeRotated;
        }

        private void ResetRotationProcess(DashStyle dashStyle, bool rerenderMainCanvas, bool resetRotationMatrix)
        {
            ChageBorderOfSetOfShapes(this.dialogProcessor.ShapeList[dialogProcessor.CurrentTab], dashStyle);
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
                shape.DashStyle = dashStyle;
            }
        }

        private void RerenderMainCanvas()
        {
            viewPort.Invalidate();
        }

        private void MainMenuButtonSave_Click(object sender, EventArgs e)
        {
            var workerName = ((ToolStripMenuItem)sender).Name + "BehaviourWorker";
            var type = GetType(Assembly.GetExecutingAssembly(), "Draw.src.Workers", workerName);
            var worker = (ISaveFileWorker)Activator.CreateInstance(type);

            this.SaveFile(dialogProcessor.ShapeList[dialogProcessor.CurrentTab], worker);
        }

        private void MainMenuButtonLoad_Click(object sender, EventArgs e)
        {
            this.LoadFile();
        }

        #region Custom Export Import


        private void SaveFile(IList<Shape> listOfShapes, ISaveFileWorker worker)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save File";
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK
                && System.IO.File.Exists(saveFileDialog.FileName)
                && System.IO.Path.GetExtension(saveFileDialog.FileName) == GlobalConstants.DefaultFileExtension)
            {
                worker.SaveFile(saveFileDialog.FileName, listOfShapes);
            }
        }



        private void LoadFile()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Load File";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DefaultExt = "txt";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK
                && System.IO.File.Exists(openFileDialog.FileName)
                && System.IO.Path.GetExtension(openFileDialog.FileName) == GlobalConstants.DefaultFileExtension)
            {
                using (var fileStream = File.Open(openFileDialog.FileName, FileMode.Open))
                using (var streamReader = new StreamReader(fileStream))
                {
                    var stringBuilder = new StringBuilder();
                    var line = streamReader.ReadLine();
                    while (line != null)
                    {
                        stringBuilder.AppendLine(line);
                        line = streamReader.ReadLine();
                    }
                    var stringReporesentation = stringBuilder.ToString();
                    if (stringReporesentation.StartsWith("{\"$type\""))
                    {
                        JsonSerializerSettings settings = JSONSaveBehaviourWorker.GetJSONSettings();
                        var shapes = JsonConvert.DeserializeObject<List<Shape>>(stringReporesentation, settings);
                        shapes.ForEach(s => s.UniqueIdentifier = Guid.NewGuid());
                        dialogProcessor.ShapeList[dialogProcessor.CurrentTab].AddRange(shapes);
                    }
                    else
                    {
                        dialogProcessor.ShapeList[dialogProcessor.CurrentTab].AddRange(CustomLoadFile(stringBuilder.ToString()));
                    }
                }
            }
        }

        private IList<Shape> CustomLoadFile(string shapesAsText)
        {
            var arrayOfShapes = shapesAsText.ToString().Split(
                       GlobalConstants.DefaultSeparator.ToCharArray(),
                       StringSplitOptions.RemoveEmptyEntries);

            var propertyMapper = new PropertyMapper();

            var resultList = new List<Shape>();

            foreach (var shapeAsString in arrayOfShapes)
            {

                var shapeParts = shapeAsString.ToString().Split(
                    Environment.NewLine.ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);

                var type = GetType(Assembly.GetExecutingAssembly(), "Draw.src.Models", shapeParts[0]);
                var constructor = type.GetConstructors().Where(c =>
                    c.GetCustomAttributes(false).Where(a
                        => a.GetType() == typeof(Importable)).FirstOrDefault() != null)
                    .FirstOrDefault();

                var constructorParameters = constructor.GetParameters();

                var parameters = propertyMapper.MapObjectProperties(shapeAsString);

                var instance = (Shape)Activator.CreateInstance(type, parameters);

                resultList.Add(instance);
            }
            return resultList;
        }

        #endregion

        private Type GetType(Assembly assembly, string nameSpace, string typeName)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)
                            && String.Equals(t.Name, typeName, StringComparison.Ordinal))
                      .FirstOrDefault();
        }

        private void ViewPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add && dialogProcessor.Selection != null)
            {
                dialogProcessor.Selection.Enlarge();
            }
            if (e.KeyCode == Keys.Subtract && dialogProcessor.Selection != null)
            {
                dialogProcessor.Selection.Shrink();
            }

            RerenderMainCanvas();

        }

        private void AddNewTab_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"\d+");
            var newTabIndex = 0;

            foreach (var item in this.TabContainer.Items)
            {
                if (item is ToolStripButton)
                {
                    var castedItem = (ToolStripButton)item;
                    Match match = regex.Match(castedItem.Name);
                    if (match.Success)
                    {
                        var possibleNewIndex = int.Parse(match.Value);
                        if (possibleNewIndex > newTabIndex)
                        {
                            newTabIndex = possibleNewIndex;
                        }
                    }
                }
            }

            newTabIndex++;

            if (this.resources == null)
            {
                resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            }

            var newTabButton = new ToolStripButton();
            newTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            newTabButton.Image = ((System.Drawing.Image)(resources.GetObject("Tab" + newTabIndex + ".Image")));
            newTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            newTabButton.Image = ((System.Drawing.Image)(resources.GetObject("Tab" + newTabIndex + ".Image")));
            newTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            newTabButton.Name = ("Tab" + newTabIndex);
            newTabButton.Size = new System.Drawing.Size(44, 24);
            newTabButton.Text = ("Tab" + newTabIndex);
            newTabButton.Click += new System.EventHandler(this.Tab_Click);

            var newTabButtonX = new ToolStripButton();
            newTabButtonX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            newTabButtonX.Image = ((System.Drawing.Image)(resources.GetObject("CloseTab" + newTabIndex + ".Image")));
            newTabButtonX.ImageTransparentColor = System.Drawing.Color.Magenta;
            newTabButtonX.Name = ("CloseTab" + newTabIndex);
            newTabButtonX.Size = new System.Drawing.Size(23, 24);
            newTabButtonX.Text = "X";
            newTabButtonX.ToolTipText = ("Close Tab " + newTabIndex);
            newTabButtonX.Click += new System.EventHandler(this.CloseTab_Click);

            var separator = new ToolStripSeparator();
            separator.Name = "toolStripSeparator00" + newTabIndex;
            separator.Size = new System.Drawing.Size(6, 27);

            dialogProcessor.ShapeList.Add(newTabIndex, new List<Shape>());

            var itemsCount = this.TabContainer.Items.Count;

            this.TabContainer.Items.Insert(itemsCount - 1, separator);
            this.TabContainer.Items.Insert(itemsCount - 1, newTabButtonX);
            this.TabContainer.Items.Insert(itemsCount - 1, newTabButton);
        }

        private void CloseTab_Click(object sender, EventArgs e)
        {
            List<int> indexes = FoundButtonsRelatedToClosingButton(sender);
            RemoveButtonsRelatedToClosingTab(indexes);
            FindAndClickExistingTab();
        }

        #region ClosingTab
        private void RemoveButtonsRelatedToClosingTab(List<int> indexes)
        {
            indexes.Reverse();
            foreach (var inner in indexes)
            {
                this.TabContainer.Items.RemoveAt(inner);
            }
        }

        private List<int> FoundButtonsRelatedToClosingButton(object sender)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(((ToolStripButton)sender).Name);
            List<int> indexes = new List<int>();
            var index = 0;
            if (match.Success)
            {
                var buttonIndex = match.Value;
                foreach (var item in this.TabContainer.Items)
                {

                    if (((item is ToolStripButton && ((ToolStripButton)item).Name.StartsWith("Tab") && ((ToolStripButton)item).Name.Remove(0, 3).Equals(match.Value))
                        || (item is ToolStripButton && ((ToolStripButton)item).Name.StartsWith("CloseTab") && ((ToolStripButton)item).Name.Remove(0, 8).Equals(match.Value)))
                        || (item is ToolStripSeparator && ((ToolStripSeparator)item).Name.StartsWith("toolStripSeparator00") && ((ToolStripSeparator)item).Name.Remove(0, 20).Equals(match.Value)))
                    {
                        indexes.Add(index);
                    }

                    index++;
                }
                var matchAsInt = int.Parse(match.Value);
                this.dialogProcessor.ShapeList.Remove(matchAsInt);
            }

            return indexes;
        }

        private void FindAndClickExistingTab()
        {
            var found = false;
            if (this.TabContainer.Items.Count > 1)
            {
                var indexForNewItem = 0;
                foreach (var item in this.TabContainer.Items)
                {
                    if (item is ToolStripButton)
                    {
                        var castedItem = (ToolStripButton)item;
                        if (castedItem.Name.StartsWith("Tab"))
                        {
                            castedItem.PerformClick();
                            found = true;
                            break;
                        }
                    }
                    indexForNewItem++;
                }
            }
            if (!found)
            {
                ExitToolStripMenuItem.PerformClick();
            }
        }
        #endregion

        private void Tab_Click(object sender, EventArgs e)
        {
            var button = ((ToolStripButton)sender);

            foreach (var item in this.TabContainer.Items)
            {
                if (item is ToolStripButton)
                {
                    var selected = ((ToolStripButton)item);
                    selected.Checked = false;
                    if (selected.Name.Equals(button.Name))
                    {
                        selected.Checked = true;
                    }
                }
            }

            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(button.Name);
            var index = int.Parse(match.Value);
            this.dialogProcessor.CurrentTab = index;
            ButtonMainNavigator.Checked = false;
            ButtonMainNavigator.PerformClick();
        }


    }
}
