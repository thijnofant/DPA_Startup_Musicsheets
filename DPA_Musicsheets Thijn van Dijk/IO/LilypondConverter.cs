using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;
using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.IO
{
    //todo dubbele code weg halen
    public class LilypondConverter
    {
        private int lastOctave = 3;
        private NoteTone lastTone = NoteTone.c;
        private bool doneReading = false;
        private int index = 0;
        private MusicSheetBuilder sheetBuilder;

        public string SheetToLilypond(Domain.MusicSheet sheet)
        {
            
            MusicsheetToLilypondVisitor visitor = new MusicsheetToLilypondVisitor();
            return visitor.SheetToLilypond(sheet);
        }

        public MusicSheet LilypondToSheet(string lilypond)
        {
            sheetBuilder = new MusicSheetBuilder("New Sheet");

            string[] tokens = lilypond.Trim().Split(' ');

            while (doneReading == false)
            {
                tokenToRelative(tokens);
                if (index >= tokens.Length)
                {
                    doneReading = true;
                }
            }

            MusicSheet temp = sheetBuilder.GetResult();

            return temp;
        }

        #region chain

        private bool tokenToRelative(string[] tokens)
        {
            string word = tokens[index].Trim();
            if (word == @"\relative")
            {
                if (!string.IsNullOrWhiteSpace(tokens[index + 1]) && char.IsLetter(tokens[index + 1].Trim()[0]))
                {
                    var temp =this.charToTone(tokens[index + 1][0]);
                    int amountUp = tokens[index + 1].Count(f => f == '\'');
                    int amountDown = tokens[index + 1].Count(f => f == ',');
                    int octave = this.getOctave(temp, amountUp - amountDown);

                    this.lastTone = temp;
                    this.lastOctave = octave;

                }
                index += 2;
                return true;
            }
            return tokenToCleff(tokens);
        }

        private bool tokenToCleff(string[] tokens)
        {
            string word = tokens[index].Trim();
            if (word == @"\clef")
            {
                sheetBuilder.AddClef(stringToCleffType(tokens[index + 1]));
                index += 2;
                return true;
            }
            return tokenToChord(tokens);
        }

        private bool tokenToChord(string[] tokens)
        {
            if (tokens[index] == "<")
            {
                List<Note> notes = new List<Note>();
                int amountOfLoops = 1;

                while (!tokens[index + amountOfLoops].Contains(">"))
                {
                    amountOfLoops++;
                }

                bool addhalfduration = tokens[index + amountOfLoops].Contains(".");
                MusicDuration lenght = this.intToDuration(int.Parse(tokens[index + amountOfLoops].Substring(1).TrimEnd('.')));

                for (int i = 1; i < amountOfLoops; i++)
                {
                    if (!string.IsNullOrWhiteSpace(tokens[index + i]))
                    {
                        NoteTone tone = this.charToTone(tokens[index + i][0]);
                        //octave
                        int amountUp = tokens[index + i].Count(f => f == '\'');
                        int amountDown = tokens[index + i].Count(f => f == ',');
                        int octave = this.getOctave(tone, amountUp - amountDown);

                        NoteMod mod = NoteMod.None;
                        if (tokens[index + 1].Length > 3)
                        {
                            if (tokens[index + i].Substring(1, 2) == "is")
                            {
                                mod = NoteMod.Cross;
                            }
                            else if (tokens[index + i].Substring(1, 2) == "es")
                            {
                                mod = NoteMod.Mole;
                            }
                        }
                        notes.Add(new Note(addhalfduration, lenght, tone, octave, mod));
                    }
                }

                this.sheetBuilder.AddChord(notes);

                index += 1 + amountOfLoops;
                return true;
            }
            /*go to next handler*/
            return tokenToRepeat(tokens);
        }

        private bool tokenToRepeat(string[] tokens)
        {
            //todo fix this
            if (tokens[index] == @"\repeat")
            {
                index++;
            }
            if (tokens[index] == "volta")
            {
                index++;
            }
            if (tokens[index] == @"\alternative")
            {
                index++;
            }

            /*go to next handler*/
            return tokenToRest(tokens);
        }

        private bool tokenToRest(string[] tokens)
        {
            if ( !string.IsNullOrWhiteSpace(tokens[index]) && tokens[index].Trim()[0] == 'r')
            {
                string word = tokens[index].Trim();

                //duration
                Domain.MusicDuration duration = intToDuration(1);
                bool addHalf = false;
                if (word[word.Length - 1] == '.')
                {
                    addHalf = true;

                    if (char.IsDigit(word[word.Length - 3]))
                    {
                        Debug.WriteLine(word.Substring(word.Length - 2, 2));
                        duration = intToDuration(int.Parse(word.Substring(word.Length - 3, 2)));
                    }
                    else
                    {
                        duration = intToDuration(int.Parse(word.Substring(word.Length - 2, 1)));
                    }
                }
                else
                {
                    if (char.IsDigit(word[word.Length - 2]))
                    {
                        Debug.WriteLine(word.Substring(word.Length - 2, 2));
                        duration = intToDuration(int.Parse(word.Substring(word.Length - 2, 2)));
                    }
                    else
                    {
                        duration = intToDuration(int.Parse(word.Substring(word.Length - 1, 1)));
                    }
                }


                sheetBuilder.AddRest(duration, addHalf);
                index += 1;
                return true;
            }
            return tokenToNote(tokens);
        }

        private bool tokenToNote(string[] tokens)
        {
            if (!string.IsNullOrWhiteSpace(tokens[index]) && char.IsLetter(tokens[index].Trim()[0]) && tokens[index].Length >= 2)
            {
                string word = tokens[index].Trim();

                //note
                NoteTone tone = charToTone(word[0]);

                //notemod
                NoteMod mod = NoteMod.None;
                if (word.Length > 3)
                {
                    if (word.Substring(1, 2) == "is")
                    {
                        mod = NoteMod.Cross;
                    }
                    else if (word.Substring(1, 2) == "es")
                    {
                        mod = NoteMod.Mole;
                    }
                }

                //octave
                int octave = this.getOctave(tone, word.Count(f => f == '\'') - word.Count(f => f == ','));

                //duration
                Domain.MusicDuration duration = intToDuration(1);
                bool addHalf = false;
                if (word[word.Length - 1] == '.')
                {
                    addHalf = true;

                    if (char.IsDigit(word[word.Length - 3]))
                    {
                        Debug.WriteLine(word.Substring(word.Length - 2, 2));
                        duration = intToDuration(int.Parse(word.Substring(word.Length - 3, 2)));
                    }
                    else
                    {
                        duration = intToDuration(int.Parse(word.Substring(word.Length - 2, 1)));
                    }
                }
                else
                {
                    if (char.IsDigit(word[word.Length - 2]))
                    {
                        Debug.WriteLine(word.Substring(word.Length - 2, 2));
                        duration = intToDuration(int.Parse(word.Substring(word.Length - 2, 2)));
                    }
                    else
                    {
                        duration = intToDuration(int.Parse(word.Substring(word.Length - 1, 1)));
                    }
                }

                sheetBuilder.AddNote(tone, octave, duration, mod,addHalf);
                index += 1;
                return true;
            }
            return tokenToTempo(tokens);
        }     

        private bool tokenToTempo(string[] tokens)
        {
            if (false /*can read*/)
            {
                return true;
            }
            /*go to next handler*/
            return tokenToTimesignature(tokens);
        }

        private bool tokenToTimesignature(string[] tokens)
        {
            string word = tokens[index].Trim();
            if (word == @"\time")
            {
                string[] time = tokens[index + 1].Trim().Split('/');
                int top = int.Parse(time[0].ToString());
                int bottom = int.Parse(time[1].ToString());
                this.sheetBuilder.AddTimeSignature(top, bottom);
                index += 2;
                return true;
            }
            return tokenToNothing(tokens);
        }

        private bool tokenToNothing(string[] tokens)
        {
            index++;
            return false;
        }
        

        

        #endregion

        #region helpers

        private Domain.ClefType stringToCleffType(string lilypondString)
        {
            switch (lilypondString)
            {
                case "treble":
                    return ClefType.GClef;
                case "tenor":
                    return ClefType.CClef;
                case "bass":
                    return ClefType.FClef;
                default:
                    return ClefType.GClef;
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

        private Domain.NoteTone charToTone(char tone)
        {
            switch (tone.ToString().ToLower())
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

        private int getOctave(NoteTone tone, int amountUpDown)
        {
            int reval = lastOctave + amountUpDown;

            int _old = (int) this.lastTone;
            int _new = (int) tone;

            if (_old != _new)
            {
                if (_old > _new)
                {
                    //true
                    if (_old - _new >= 4)
                    {
                        reval ++;
                    }
                }
                else if(_new > _old)
                {
                    if (_new - _old >= 4)
                    {
                        reval--;
                    }
                }
            }

            lastOctave = reval;
            lastTone = tone;
            return reval;
        }
        #endregion
    }
}