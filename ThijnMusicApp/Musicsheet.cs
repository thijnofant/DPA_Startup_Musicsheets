using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets
{
    public class MusicSheet
    {
        public List<MusicTrack> Tracks { get; private set; }

        public String Name { get; private set; }

        public MusicSheet(String name)
        {
            this.Name = name;
            this.Tracks = new List<MusicTrack>();
        }
    }
}