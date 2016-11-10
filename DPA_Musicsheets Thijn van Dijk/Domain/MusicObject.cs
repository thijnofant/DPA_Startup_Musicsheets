using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public abstract class MusicObject : MusicLeaf
    {
        public bool AddHalfDuration{ get; private set; }

        public MusicDuration MusicDuration { get; private set; }

        protected MusicObject(bool addHalf, MusicDuration duration)
        {
            this.AddHalfDuration = addHalf;
            this.MusicDuration = duration;
        }
    }
}

