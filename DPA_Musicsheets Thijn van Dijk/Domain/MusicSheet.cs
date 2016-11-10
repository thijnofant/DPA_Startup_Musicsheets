using System.Collections.Generic;

namespace DPA_Musicsheets_Thijn_van_Dijk.Domain
{
    public class MusicSheet
    {
        public string Name{ get; private set; }

        public List<MusicComponent> MusicComponents { get; private set; }

        public MusicSheet(string name)
        {
            this.Name = name;
            this.MusicComponents = new List<MusicComponent>();
        }

        public MusicSheet Clone()
        {
            MusicSheet clone = new MusicSheet(Name);
            foreach (MusicComponent musicComponent in MusicComponents)
            {
                clone.MusicComponents.Add(musicComponent.Clone());
            }

            return clone;
        }

        public void AddMusicComponent(MusicComponent component)
        {
            this.MusicComponents.Add(component);
        }
    }
}

