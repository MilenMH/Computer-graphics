using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw.src.Helpers
{
    public class PropertyMapper
    {
        private Dictionary<string, Delegate> PropertyDictionary { get; set; }

        private delegate float MapStandartFloatPropertyDelegate(string[] parameters);

        private delegate Pen MapStandartPenPropertyDelegate(string[] parameters);

        private delegate Color MapStandartColorPropertyDelegate(string[] parameters);

        private delegate PointF MapStandartPointFPropertyDelegate(string[] parameters);

        private delegate DashStyle MapStandartDashStylePropertyDelegate(string[] parameters);

        private delegate bool MapStandartBoolPropertyDelegate(string[] parameters);

        private delegate int MapStandartIntPropertyDelegate(string[] parameters);

        public PropertyMapper()
        {
            Init();
        }

        private void Init()
        {
            this.PropertyDictionary = new Dictionary<string, Delegate>();
            this.PropertyDictionary.Add("x", new MapStandartFloatPropertyDelegate(MapStandartFloatPropertyFunc));
            this.PropertyDictionary.Add("y", new MapStandartFloatPropertyDelegate(MapStandartFloatPropertyFunc));
            this.PropertyDictionary.Add("width", new MapStandartFloatPropertyDelegate(MapStandartFloatPropertyFunc));
            this.PropertyDictionary.Add("height", new MapStandartFloatPropertyDelegate(MapStandartFloatPropertyFunc));
            this.PropertyDictionary.Add("bordercolor", new MapStandartColorPropertyDelegate(MapStandartColorPropertyFunc));
            this.PropertyDictionary.Add("fillcolor", new MapStandartColorPropertyDelegate(MapStandartColorPropertyFunc));
            this.PropertyDictionary.Add("point", new MapStandartPointFPropertyDelegate(MapStandartPointFPropertyFunc));
            this.PropertyDictionary.Add("dashstyle", new MapStandartDashStylePropertyDelegate(MapStandartDashStylePropertyFunc));
            this.PropertyDictionary.Add("temporaryflag", new MapStandartBoolPropertyDelegate(MapStandartBoolPropertyFunc));
            this.PropertyDictionary.Add("transparency", new MapStandartIntPropertyDelegate(MapStandartIntPropertyFunc));
        }

        public object[] MapObjectProperties(string shapeAsString)
        {
            var objectLines = shapeAsString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var listOfObjects = new List<object>();
            var firstLine = objectLines[0];
            foreach (var line in objectLines)
            {
                if (line != firstLine)
                {
                    var lineParts = line.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    var resultObj = PropertyDictionary[lineParts[0].Trim().ToLower()].DynamicInvoke(new object[] { lineParts });
                    listOfObjects.Add(resultObj);
                }
            }
            return listOfObjects.ToArray();
        }

        private float MapStandartFloatPropertyFunc(string[] parameters)
        {
            return float.Parse(parameters[1].Trim());
        }

        private Pen MapStandartPenPropertyFunc(string[] parameters)
        {
            return new Pen(Color.FromName(parameters[1].Trim()));
        }

        private Color MapStandartColorPropertyFunc(string[] parameters)
        {
            return ColorTranslator.FromHtml(parameters[1].Trim());
        }

        private PointF MapStandartPointFPropertyFunc(string[] parameters)
        {
            return new PointF(float.Parse(parameters[2]), float.Parse(parameters[4]));
        }

        private DashStyle MapStandartDashStylePropertyFunc(string[] parameters)
        {
              return(DashStyle)Enum.Parse(typeof(DashStyle), parameters[1].Trim());
        }

        private bool MapStandartBoolPropertyFunc(string[] parameters)
        {
            return bool.Parse(parameters[1].Trim());
        }

        private int MapStandartIntPropertyFunc(string[] parameters)
        {
            return int.Parse(parameters[1].Trim());
        }
    }
}
