using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets
{
    public class Timesignature
    {
        public int Upper { get; set; }
        public int Lower { get; set; }

        public Timesignature(int upper, int lower)
        {
            this.Upper = upper;
            this.Lower = lower;
        }
    }
}