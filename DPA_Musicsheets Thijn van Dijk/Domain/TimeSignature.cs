using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public class TimeSignature : MusicLeaf
    {
        public int Top{ get; private set; }

        public int Bottom{ get; private set; }

        public TimeSignature(int top, int bottom)
        {
            Top = top;
            Bottom = bottom;
        }

        public override MusicComponent Clone()
        {
            return new TimeSignature(Top, Bottom);
        }

        public override void Visit(MusicComponentVisitor visitor)
        {
            visitor.TimeSignatureResponse(this);
        }
    }
}

