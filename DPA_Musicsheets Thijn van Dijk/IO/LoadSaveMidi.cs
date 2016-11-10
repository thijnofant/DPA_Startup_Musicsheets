using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Documents;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets_Thijn_van_Dijk.IO
{
    public class LoadSaveMidi : LoadSaveFileStrategy
    {
        private MusicSheetBuilder _builder;
        private int lastKeycode = 0;
        private int lastAbsTime = 0;
        private bool lastNoteCompleted = true;
        private TimeSignature currentSignature = new TimeSignature(4,4);
        private Sequence seq;

        public override MusicSheet Load(string uri)
        {
            _builder = new MusicSheetBuilder(uri);

            _builder.AddClef(ClefType.GClef);

            seq = MidiReader.ReadMidi(uri);
            List<Tuple<TimeSignature, int>> timesignatures = new List<Tuple<TimeSignature, int>>();

            var tracks = MidiReader.ReadSequence(seq);

            foreach (MidiTrack midiTrack in tracks)
            {
                Debug.WriteLine(midiTrack.TrackName);
                foreach (string message in midiTrack.Messages)
                {
                    if (message.Contains("TimeSignature"))
                    {
                        messageToTimeSignature(message);
                    }
                    else if (message.Contains("Tempo"))
                    {
                        messageToTempo(message);
                    }
                    else if (message.Contains("NoteOn"))
                    {
                        //lastKeycode;
                        //lastAbsTime;
                        //lastNoteCompleted;

                        var parts = message.Split(',');
                        int newKeycode = int.Parse(parts[0].Split(':')[1].Trim());//keycode
                        int newAbsTime = int.Parse(parts[2].Split(':')[1].Trim());//absolute time
                        int howFarPressed = int.Parse(parts[4].Split(':')[1].Trim());//keyDownPercentage

                        if (lastNoteCompleted)
                        {
                            if (newAbsTime > lastAbsTime)
                            {
                                var dict = this.msToDuration(newAbsTime - lastAbsTime, currentSignature, seq);
                                bool addHalf = dict["addHalf"] == "true";
                                MusicDuration dur = intToDuration(int.Parse(dict["duration"]));
                                _builder.AddRest(dur, addHalf);
                            }
                            if (howFarPressed > 10)
                            {
                                lastNoteCompleted = false;
                                lastKeycode = newKeycode;
                                lastAbsTime = newAbsTime;
                            }
                        }
                        else
                        {
                            if (howFarPressed == 0)
                            {
                                var durDict = msToDuration(newAbsTime - lastAbsTime, currentSignature, seq);
                                var duration = intToDuration(int.Parse(durDict["duration"]));
                                bool addHalf = durDict["addHalf"] == "true";
                                var dict = keycodeToToneAndOctave(newKeycode);
                                var tone = stringToTone(dict["Tone"]);
                                int octave = int.Parse(dict["Octave"]);
                                var mod = NoteMod.None;
                                if (dict["Mod"] == "Cross")
                                {
                                    mod = NoteMod.Cross;
                                }
                                _builder.AddNote(tone, octave, duration, mod, addHalf);
                                lastNoteCompleted = true;
                                lastKeycode = newKeycode;
                                lastAbsTime = newAbsTime;
                            }
                        }

                        //keydown t/m keyup absolutetime keyup - absolutetime keydown = lenght in ms
                        //if keydown absolutetime van nieuwe noot != keyup absolutetime van oude noot = timediference = rest
                    }
                    //todo chords maybe later
                    
                }
            }
            return _builder.GetResult();
        }

        private void messageToTempo(string message)
        {
            var parts = message.Split(':');
            _builder.AddTempo( int.Parse(parts[1]));
        }

        private Tuple<TimeSignature, int> messageToTimeSignature(string message)
        {
            var parts = message.Split(':');
            parts = parts[1].Split('/');
            int top = int.Parse(parts[0]);
            int bottom = int.Parse(parts[1]);
            return new Tuple<TimeSignature, int>(new TimeSignature(top, bottom), 0);
        }

        public override bool Save(MusicSheet sheet, string uri)
        {
            throw new System.NotImplementedException();
        }

        public LoadSaveMidi(Context cont) : base(cont)
        {
        }

        #region helpers

        public Dictionary<string, string> msToDuration(int ms, TimeSignature sig, Sequence seq)
        {
            Dictionary<string, string> reval = new Dictionary<string, string>();
            double percentageOfBeatNote = ms / (seq.Division* 1.0);
            double percentageOfWholeNote = (1.0 / sig.Bottom) * percentageOfBeatNote;
            for (int noteLength = 32; noteLength >= 1; noteLength /= 2)
            {
                double absoluteNoteLength = (1.0 / noteLength);
                if (percentageOfWholeNote <= absoluteNoteLength)
                {
                    reval.Add("duration", ((noteLength).ToString()));
                    reval.Add("addHalf", "false");
                    return reval;
                }
                else if (percentageOfWholeNote <= absoluteNoteLength + absoluteNoteLength/2)
                {
                    reval.Add("duration", ((noteLength).ToString()));
                    reval.Add("addHalf", "true");
                    return reval;
                }
                // Hoe met punten om te gaan...?
                // Tip: Deze zijn 1.5 keer de absoluteNoteLength. (1 keer voor de noot en 0.5 keer voor de punt)            
            }
            reval.Add("duration", ((1).ToString()));
            reval.Add("addHalf", "false");
            return reval;
            //todo finish this method

        }

        public Dictionary<string, string> keycodeToToneAndOctave(int keycode)
        {
            Dictionary<string, string> reval = new Dictionary<string, string>();

            int octavelenght = 12;
            int notevalue = (keycode % octavelenght) + 1 ;
            int octave = (keycode/octavelenght) -1;

            string[] noteNames = { "C",       "C",    "D",     "D",    "E",    "F",     "F",    "G",     "G",    "A",    "A",     "B" };
            string[] noteMods = { "none", "Cross", "none", "Cross", "none", "none", "Cross", "none", "Cross", "none", "Cross", "none" };

            string tone = noteNames[notevalue - 1];
            string mod = noteMods[notevalue -1];
                        
            reval.Add("Tone", tone);
            reval.Add("Octave", octave.ToString());
            reval.Add("Mod", mod);
            return reval;
        }

        private Domain.NoteTone stringToTone(string tone)
        {
            switch (tone.ToLower())
            {
                case "a":
                    return NoteTone.a;
                case "b":
                    return NoteTone.b;
                case "c":
                    return NoteTone.c;
                case "d":
                    return NoteTone.d;
                case "e":
                    return NoteTone.e;
                case "f":
                    return NoteTone.f;
                case "g":
                    return NoteTone.g;
                default:
                    return NoteTone.c;
            }
        }

        private Domain.MusicDuration intToDuration(int lilypondDuration)
        {
            switch (lilypondDuration)
            {
                case 1:
                    return MusicDuration.Whole;
                case 2:
                    return MusicDuration.Half;
                case 4:
                    return MusicDuration.Quarter;
                case 8:
                    return MusicDuration.Eight;
                case 16:
                    return MusicDuration.Sixteenth;
                default:
                    return MusicDuration.Whole;
            }
        }
        #endregion
    }
}

