using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets
{
    public class TrackPiece
    {

        public int Tempo { get; set; }

        public List<MusicalObject> MusicalObjects { get; set; }

        public Timesignature Timesignature { get; set; }

        public TrackPiece(int tempo, Timesignature signature)
        {
            this.Tempo = tempo;
            this.Timesignature = signature;
            this.MusicalObjects = new List<MusicalObject>();
        }

        public void AddMusicalObject(MusicalObject mObject)
        {
            this.MusicalObjects.Add(mObject);
        }
    }
}