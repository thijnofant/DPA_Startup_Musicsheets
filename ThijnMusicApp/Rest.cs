using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets
{
    public class Rest : MusicalObject
    {
        public Rest(Duration duration)
        {
            this.Duur = duration;
        }
    }
}