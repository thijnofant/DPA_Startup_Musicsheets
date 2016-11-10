using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets
{
    public class Cleff
    {
        public String CleffType { get; private set; }

        public Cleff(String clefftype)
        {
            this.CleffType = clefftype;
        }
    }
}