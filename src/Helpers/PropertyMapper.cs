using System;
using System.Collections.Generic;
using System.Drawing;

namespace Draw.src.Helpers
{
    public class PropertyMapper
    {
        private Dictionary<string, Delegate> PropertyDictionary { get; set; }

        private delegate float MapStandartFloatPropertyDelegate(string[] parameters);

        private delegate Pen MapStandartPenPropertyDelegate(string[] parameters);

        private delegate Color MapStandartColorPropertyDelegate(string[] parameters);

        private delegate PointF MapStandartPointFPropertyDelegate(string[] parameters);

        public PropertyMapper()
        {
            Init();
        }

        private void Init()
        {
            this.PropertyDictionary = new Dictionary<string, Delegate>();
            this.PropertyDictionary.Add("x", new MapStandartFloatPropertyDelegate(MapStandartFroatPropertyFunc));
            this.PropertyDictionary.Add("y", new MapStandartFloatPropertyDelegate(MapStandartFroatPropertyFunc));
            this.PropertyDictionary.Add("width", new MapStandartFloatPropertyDelegate(MapStandartFroatPropertyFunc));
            this.PropertyDictionary.Add("height", new MapStandartFloatPropertyDelegate(MapStandartFroatPropertyFunc));
            this.PropertyDictionary.Add("bordercolor", new MapStandartPenPropertyDelegate(MapStandartPenPropertyFunc));
            this.PropertyDictionary.Add("fillcolor", new MapStandartColorPropertyDelegate(MapStandartColorPropertyFunc));
            this.PropertyDictionary.Add("point", new MapStandartPointFPropertyDelegate(MapStandartPointFPropertyFunc));

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

        private float MapStandartFroatPropertyFunc(string[] parameters)
        {
            return float.Parse(parameters[1].Trim());
        }

        private Pen MapStandartPenPropertyFunc(string[] parameters)
        {
            return new Pen(Color.FromName(parameters[1].Trim()));
        }

        private Color MapStandartColorPropertyFunc(string[] parameters)
        {
            return Color.FromName(parameters[1].Trim());
        }

        private PointF MapStandartPointFPropertyFunc(string[] parameters)
        {
            return new PointF(float.Parse(parameters[2]), float.Parse(parameters[4]));
        }
    }
}
