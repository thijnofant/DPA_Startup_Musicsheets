using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets
{
    public class MusicNote : MusicalObject
    {
        public String Tone { get; set; }


        public int Octave { get; set; }

        public NoteMod NoteMod { get; set; }

        public MusicNote(String tone, NoteMod mod, int octave, Duration duur, bool addHalfDuration = false)
        {
            this.Duur = Duur;
            this.Tone = tone;
            this.Octave = octave;
            this.NoteMod = mod;
            this.AddHalfDuration = addHalfDuration;
        }
    }
}