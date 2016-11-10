using System.Collections.Generic;
using System.Diagnostics;
using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public abstract class MusicComposite : MusicComponent
    {
        public List<MusicComponent> MusicComponents { get; private set; }

        protected MusicComposite()
        {
            MusicComponents = new List<MusicComponent>();
        }

        protected MusicComposite(MusicComposite comp)
        {
            MusicComponents = new List<MusicComponent>();
            foreach (MusicComponent musicComponent in comp.MusicComponents)
            {
                this.MusicComponents.Add(musicComponent.Clone());
            }
        }

    }
}

