using System.Collections.Generic;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;

namespace DPA_Musicsheets_Thijn_van_Dijk.Visitors
{
    public class AudioPlayerVisitor : MusicComponentVisitor
    {
        public override void NoteResponse(Note note)
        {
            throw new System.NotImplementedException();
        }

        public override void RestResponse(Rest rest)
        {
            throw new System.NotImplementedException();
        }

        public override void ChordResponse(Chord chord)
        {
            throw new System.NotImplementedException();
        }

        public override void RepeatingResponse(Repeating repeating)
        {
            throw new System.NotImplementedException();
        }

        public override void TempoResponse(Tempo tempo)
        {
            throw new System.NotImplementedException();
        }

        public override void TimeSignatureResponse(TimeSignature signature)
        {
            throw new System.NotImplementedException();
        }

        public override void CleffResponse(Clef clef)
        {
            throw new System.NotImplementedException();
        }

        public void Play(MusicSheet sheet)
        {
            //Noten omzetten naar midi
            //Midi afspelen op audio device
            //alle componenten aflopen en toevoegen aan lijst van te printen symbolen
        }
    }
}

