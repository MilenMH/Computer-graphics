using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draw.src.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Constructor, AllowMultiple = true) ]
    class Importable : System.Attribute
    {
    }
}
