using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public enum ClefType {
        GClef = 0,
        CClef = 1,
        FClef = 2
    }

    public class Clef: MusicLeaf
    {
        public ClefType ClefType { get; private set; }

        public override MusicComponent Clone()
        {
            return new Clef(this.ClefType);
        }

        public override void Visit(MusicComponentVisitor visitor)
        {
            visitor.CleffResponse(this);
        }

        public Clef(ClefType type)
        {
            this.ClefType = type;
        }
    }
}
