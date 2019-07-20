using Draw.src.Helpers;
using Draw.src.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Draw.src.Workers
{
    public class CustomSaveBehaviourWorker : ISaveFileWorker
    {
        public void SaveFile(string fileName, IList<Shape> listOfShapes)
        {
            using (var fs = new FileStream(fileName, FileMode.Truncate))
            {
            }
            using (var fileStream = File.Open(fileName, FileMode.OpenOrCreate))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                var lastShapesGuid = listOfShapes.LastOrDefault()?.UniqueIdentifier;
                foreach (var shape in listOfShapes)
                {
                    streamWriter.Write(shape.ToString());
                    if (shape.UniqueIdentifier != lastShapesGuid)
                    {
                        streamWriter.WriteLine(GlobalConstants.DefaultSeparator);
                    }
                }
            }
        }
    }
}
