using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;

namespace DPA_Musicsheets_Thijn_van_Dijk.Visitors
{
    public abstract class MusicComponentVisitor
    {
        public abstract void NoteResponse(Note note);
        public abstract void RestResponse(Rest rest);
        public abstract void ChordResponse(Chord chord);
        public abstract void RepeatingResponse(Repeating repeating);
        public abstract void TempoResponse(Tempo tempo);
        public abstract void TimeSignatureResponse(TimeSignature signature);
        public abstract void CleffResponse(Clef clef);
    }
}
