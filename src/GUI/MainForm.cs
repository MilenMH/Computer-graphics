using Draw.src.Attributes;
using Draw.src.Helpers;
using Draw.src.Interfaces;
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
                dialogProcessor.ShapeList.Remove(dialogProcessor.Selection);
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
            if (ButtonCopy.Checked && dialogProcessor.Selection != null)
            {
                dialogProcessor.DrowTemporaryCopyShape = true;
                dialogProcessor.LastLocation = e.Location;
                var settings = JSONSaveBehaviourWorker.GetJSONSettings();
                var copyOfSelection = JsonConvert.DeserializeObject<Shape>(JsonConvert.SerializeObject(dialogProcessor.Selection, settings), settings);
                copyOfSelection.TemporaryFlag = true;
                copyOfSelection.UniqueIdentifier = Guid.NewGuid();
                dialogProcessor.SelectionCopy = copyOfSelection;
                dialogProcessor.ShapeList.Add(dialogProcessor.SelectionCopy);
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
            if (ButtonDrowRectangle.Checked && dialogProcessor.DrowTemporaryRectangle)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddRectangle(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6, DashStyle.Dot, true);
            }
            if (ButtonDrowTriangle.Checked && dialogProcessor.DrowTemporaryTriangle)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddTriangle(
                    new PointF(shapeParams.Item1, shapeParams.Item4),
                    new PointF(shapeParams.Item1, shapeParams.Item2),
                    new PointF(shapeParams.Item3, shapeParams.Item2), DashStyle.Dot, true);
            }
            if (ButtonDrowEllipse.Checked && dialogProcessor.DrowTemporaryEllipse)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddEllipse(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6, DashStyle.Dot, true);
            }
            if (ButtonCopy.Checked && dialogProcessor.DrowTemporaryCopyShape && dialogProcessor.SelectionCopy != null)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.SelectionCopy.MoveToNextDestination(e.Location, dialogProcessor.LastLocation);
                dialogProcessor.SelectionCopy.UniqueIdentifier = Guid.NewGuid();
                dialogProcessor.ShapeList.Add(dialogProcessor.SelectionCopy);
                dialogProcessor.LastLocation = e.Location;
            }
            viewPort.Invalidate();
        }

        private void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
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

            var shapeParams = DimentionCalculator.GetShapesParamsByTwoPoints(
                dialogProcessor.OnMouseDownPoint, dialogProcessor.OnMouseUpPoint);


            if (ButtonDrowEllipse.Checked)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddEllipse(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6,
                    DashStyle.Solid, false);
            }
            if (ButtonDrowRectangle.Checked)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddRectangle(
                    shapeParams.Item1, shapeParams.Item2, shapeParams.Item5, shapeParams.Item6,
                    DashStyle.Solid, false);
            }
            if (ButtonDrowTriangle.Checked)
            {
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.AddTriangle(
                    new PointF(shapeParams.Item1, shapeParams.Item4),
                    new PointF(shapeParams.Item1, shapeParams.Item2),
                    new PointF(shapeParams.Item3, shapeParams.Item2),
                    DashStyle.Solid, false);
            }

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
            if (ButtonCopy.Checked && dialogProcessor.DrowTemporaryCopyShape)
            {
                ButtonCopy.Checked = false;
                dialogProcessor.DrowTemporaryCopyShape = false;
                dialogProcessor.ShapeList.RemoveAll(s => s.TemporaryFlag);
                dialogProcessor.SelectionCopy.TemporaryFlag = false;
                dialogProcessor.ShapeList.Add(dialogProcessor.SelectionCopy);
                dialogProcessor.SelectionCopy = null;
            }
            RerenderMainCanvas();
        }

        private void DrawRectangleSpeedButton_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowEllipse.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
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
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
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
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
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
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
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
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
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
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
        }


        private void BorderColor_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowEllipse.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ButtonDelete.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonCopy.Checked = false;
            ButtonFillColor.Checked = false;
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            ButtonDrowTriangle.Checked = false;
            ButtonMultiSelect.Checked = false;
            ButtonMainNavigator.Checked = false;
            ButtonDrowEllipse.Checked = false;
            ButtonDrowRectangle.Checked = false;
            ButtonFillColor.Checked = false;
            ButtonMultiMove.Checked = false;
            ButtonDelete.Checked = false;
            ButtonBorderColor.Checked = false;
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
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
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
            ButtonCopy.Checked = false;
            ButtonBorderColor.Checked = false;
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

            this.SaveFile(dialogProcessor.ShapeList, worker);
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
                        dialogProcessor.ShapeList.AddRange(shapes);
                    }
                    else
                    {
                        CustomLoadFile(stringBuilder.ToString());
                    }
                }
            }
        }

        private void CustomLoadFile(string shapesAsText)
        {
            var arrayOfShapes = shapesAsText.ToString().Split(
                       GlobalConstants.DefaultSeparator.ToCharArray(),
                       StringSplitOptions.RemoveEmptyEntries);

            var propertyMapper = new PropertyMapper();

            foreach (var shapeAsString in arrayOfShapes)
            {

                var shapeParts = shapeAsString.ToString().Split(
                    Environment.NewLine.ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);

                var type = GetType(Assembly.GetExecutingAssembly(), "Draw.src.Model", shapeParts[0]);
                var constructor = type.GetConstructors().Where(c =>
                    c.GetCustomAttributes(false).Where(a
                        => a.GetType() == typeof(Importable)).FirstOrDefault() != null)
                    .FirstOrDefault();

                var constructorParameters = constructor.GetParameters();

                var parameters = propertyMapper.MapObjectProperties(shapeAsString);

                var instance = (Shape)Activator.CreateInstance(type, parameters);

                dialogProcessor.ShapeList.Add(instance);
            }
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
    }
}
