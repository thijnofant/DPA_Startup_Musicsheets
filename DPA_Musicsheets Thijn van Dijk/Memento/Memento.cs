using DPA_Musicsheets_Thijn_van_Dijk.Domain;

namespace DPA_Musicsheets_Thijn_van_Dijk.Memento
{
    public class Memento
    {
        public MusicSheet musicSheet { get; private set; }

        public string editorContents { get; private set; }

        public int state { get; private set; }

        public Memento(MusicSheet musicSheet, string editorContents, int state)
        {
            this.musicSheet = musicSheet;
            this.editorContents = editorContents;
            this.state = state;
        }
    }
}

