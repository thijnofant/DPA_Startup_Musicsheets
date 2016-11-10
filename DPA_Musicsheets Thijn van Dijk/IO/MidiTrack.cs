using System.Collections.ObjectModel;

namespace DPA_Musicsheets_Thijn_van_Dijk.IO
{
    public class MidiTrack
    {
        public string TrackName { get; set; }
        public ObservableCollection<string> Messages { get; private set; }

        public MidiTrack()
        {
            this.Messages = new ObservableCollection<string>();
        }
    }
}