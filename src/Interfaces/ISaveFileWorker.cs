using Draw.src.Models;
using System.Collections.Generic;

namespace Draw.src.Interfaces
{
    public interface ISaveFileWorker
    {
         void SaveFile(string fileName, IList<Shape> listOfShapes);
    }
}
