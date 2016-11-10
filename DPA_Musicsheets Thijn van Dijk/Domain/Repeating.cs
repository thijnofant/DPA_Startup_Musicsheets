using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public class Repeating : MusicComposite
    {
        public override MusicComponent Clone()
        {
            return new Repeating(this);
        }

        public override void Visit(MusicComponentVisitor visitor)
        {
            visitor.RepeatingResponse(this);
        }

        public void AddNote(Note note) { this.MusicComponents.Add(note); }

        public Repeating(MusicComposite chord): base(chord){ }
    }
}

