using System.Collections.Generic;
using System.IO;
using Draw.src.Interfaces;
using Newtonsoft.Json;

namespace Draw.src.Workers
{
    public class JSONSaveBehaviourWorker : ISaveFileWorker
    {
        public void SaveFile(string fileName, IList<Shape> listOfShapes)
        {
            using (var fs = new FileStream(fileName, FileMode.Truncate))
            {
            }
            using (var fileStream = File.Open(fileName, FileMode.OpenOrCreate))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                JsonSerializerSettings settings = GetJSONSettings();

                string strJson = JsonConvert.SerializeObject(listOfShapes, settings);
               
                streamWriter.WriteLine(strJson);
            }
        }

        public static JsonSerializerSettings GetJSONSettings()
        {
            return new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }
    }
}
