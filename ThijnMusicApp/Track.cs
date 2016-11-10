using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets
{
    public class MusicTrack
    {

        public String Name { get; private set; }

        public List<TrackPiece> TrackPieces { get; private set; }

        public Cleff Cleff { get; set; }

        public MusicTrack(String name, Cleff cleff)
        {
            this.Name = name;
            this.Cleff = cleff;
            this.TrackPieces = new List<TrackPiece>();
        }
    }
}