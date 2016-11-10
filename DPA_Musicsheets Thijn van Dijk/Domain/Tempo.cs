using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public class Tempo : MusicLeaf
    {
        public int Bpm{ get; private set; }

        public override MusicComponent Clone()
        {
            return new Tempo(this.Bpm);
        }

        public override void Visit(MusicComponentVisitor visitor)
        {
            visitor.TempoResponse(this);
        }

        public Tempo(int bpm)
        {
            Bpm = bpm;
        }
    }
}

