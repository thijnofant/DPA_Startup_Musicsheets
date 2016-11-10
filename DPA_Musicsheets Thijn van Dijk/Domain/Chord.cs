using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public class Chord : MusicComposite
    {
        public override MusicComponent Clone()
        {
            return new Chord(this);
        }

        public override void Visit(MusicComponentVisitor visitor)
        {
            visitor.ChordResponse(this);
        }

        public MusicDuration GetDuration()
        {
            MusicDuration reVal = MusicDuration.Sixteenth;

            foreach (MusicComponent component in this.MusicComponents)
            {
                Note temp = (Note)component;
                if (temp.MusicDuration < reVal)
                {
                    reVal = temp.MusicDuration;
                }
            }

            return reVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="note"></param>
        /// <returns>self for chaining</returns>
        public Chord AddNote(Note note){
            this.MusicComponents.Add(note);
            return this;
        }

        public Chord(MusicComposite chord): base(chord){}
        public Chord() { }
    }
}

