using System;
using System.Diagnostics;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;

namespace DPA_Musicsheets_Thijn_van_Dijk.Visitors
{
    public class MusicsheetToLilypondVisitor: MusicComponentVisitor
    {
        private string _lilypond;
        private int lastOctave = 4;
        private NoteTone lastTone = NoteTone.c;

        public string SheetToLilypond(MusicSheet sheet)
        {
            _lilypond = "\\relative c' { ";

            foreach (var component in sheet.MusicComponents)
            {
                component.Visit(this);
            }

            _lilypond += "}";
            return _lilypond;
        }

        public override void NoteResponse(Note note)
        {
            _lilypond += NoteWithoutTempo(note);
            _lilypond += (int) note.MusicDuration;
            if (note.AddHalfDuration)
                _lilypond += ".";
            _lilypond += " ";

            lastOctave = note.Octave;
            lastTone = note.Tone;
        }

        public string NoteWithoutTempo(Note note)
        {
            string reString = "";
            reString += note.Tone.ToString();
            reString += NoteModToString(note.NoteMod);
            reString += amountUpdown(note);
            return reString;
        }

        public override void RestResponse(Rest rest)
        {
            _lilypond += "r";
            _lilypond += (int) rest.MusicDuration;
            if (rest.AddHalfDuration) _lilypond += ".";
            _lilypond += " ";
        }

        public override void ChordResponse(Chord chord)
        {
            _lilypond += "< ";
            foreach (Note note in chord.MusicComponents)
            {
                _lilypond +=  NoteWithoutTempo(note);
                lastOctave = note.Octave;
                lastTone = note.Tone;
                _lilypond += " ";
            }
            _lilypond += ">";
            _lilypond += (int) chord.GetDuration();
            _lilypond += " ";
        }

        public override void RepeatingResponse(Repeating repeating)
        {
            //todo repeat
        }

        public override void TempoResponse(Tempo tempo)
        {
            _lilypond += @"\tempo 4 = " + tempo.Bpm + " ";
        }

        public override void TimeSignatureResponse(TimeSignature signature)
        {
            _lilypond += @"\time ";
            _lilypond += signature.Top+"/"+signature.Bottom;
            _lilypond += " ";
        }

        public override void CleffResponse(Clef clef)
        {
            _lilypond += @"\clef ";
            _lilypond += ClefToClef(clef);
            _lilypond += " ";
        }

        #region helpers

        public string NoteModToString(Domain.NoteMod mod)
        {
            switch (mod)
            {
                case NoteMod.Mole:
                    return "es";
                case NoteMod.Cross:
                    return "is";
                case NoteMod.None:
                    return "";
                default:
                    return "";
            }
        }

        public string ClefToClef(Clef clef)
        {
            switch (clef.ClefType)
            {
                case ClefType.GClef:
                    return "treble";
                case ClefType.CClef:
                    return "tenor";
                case ClefType.FClef:
                    return "bass";
                default:
                    return "treble";
            }
        }

        public string amountUpdown(Note note)
        {
            //todo fix this
            string reString = "";

            int newNote = note.Octave * 7 + (int)note.Tone;
            int lastNote = lastOctave * 7 + (int)lastTone;

            int difference = newNote - lastNote;


            if (difference > 0)
            {
                while (difference >= 4)
                {
                    reString += "'";
                    difference -= 7;
                }
            }
            else
            {
                while (difference <= -4)
                {
                    reString += ",";
                    difference += 7;
                }
            }
            return reString;
        }
        #endregion
    }
}