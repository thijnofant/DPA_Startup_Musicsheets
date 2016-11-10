using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public class Rest : MusicObject
    {
        public override MusicComponent Clone()
        {
            return new Rest(this.AddHalfDuration, this.MusicDuration);
        }

        public override void Visit(MusicComponentVisitor visitor)
        {
            visitor.RestResponse(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Rest(bool addHalf, MusicDuration duration) : base(addHalf, duration){}
    }
}

