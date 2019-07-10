using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draw.src.Helpers
{

    public class Sextuple<T1, T2, T3, T4, T5, T6>
    {
        public Sextuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
        }

        public T1 Item1 { get; set; }

        public T2 Item2 { get; set; }

        public T3 Item3 { get; set; }

        public T4 Item4 { get; set; }

        public T5 Item5 { get; set; }

        public T6 Item6 { get; set; }
    }
}
