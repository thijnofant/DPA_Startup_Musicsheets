using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public abstract class MusicComponent
    {
        public abstract MusicComponent Clone();
        public abstract void Visit(MusicComponentVisitor visitor);
    }
}

