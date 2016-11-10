using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public class Note : MusicObject
    {
        public NoteTone Tone { get; private set; }

        public int Octave{ get; private set; }

        public NoteMod NoteMod { get; private set; }

        public override MusicComponent Clone()
        {
            return new Note(this.AddHalfDuration, this.MusicDuration, this.Tone, this.Octave, this.NoteMod);
        }

        public override void Visit(MusicComponentVisitor visitor)
        {
            visitor.NoteResponse(this);
        }

        public Note(bool addHalf, MusicDuration duration, NoteTone tone, int octave, NoteMod noteMod) : base(addHalf, duration)
        {
            Tone = tone;
            Octave = octave;
            NoteMod = noteMod;
        }
    }
}

