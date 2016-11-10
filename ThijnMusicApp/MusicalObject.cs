using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets
{
    public abstract class MusicalObject
    {

        public bool AddHalfDuration { get; set; }

        public Duration Duur { get; set; }

        public MusicalObject ElongateBy { get; set; }
    }
}