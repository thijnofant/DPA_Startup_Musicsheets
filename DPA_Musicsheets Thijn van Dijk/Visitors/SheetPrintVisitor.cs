using System;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;
using System.Collections.Generic;
using PSAMControlLibrary;
using Clef = DPA_Musicsheets_Thijn_van_Dijk.Domain.Clef;
using ClefType = DPA_Musicsheets_Thijn_van_Dijk.Domain.ClefType;
using Note = DPA_Musicsheets_Thijn_van_Dijk.Domain.Note;
using Rest = DPA_Musicsheets_Thijn_van_Dijk.Domain.Rest;
using TimeSignature = DPA_Musicsheets_Thijn_van_Dijk.Domain.TimeSignature;

namespace DPA_Musicsheets_Thijn_van_Dijk.Visitors
{
    public class SheetPrintVisitor : MusicComponentVisitor
    {
        private List<PSAMControlLibrary.MusicalSymbol> _symbolsToPrint;
        private TimeSignature _currentTimeSignature = new TimeSignature(4,4);
        private double _amountOfTime = 0;

        public void Print(MusicSheet sheet, PSAMWPFControlLibrary.IncipitViewerWPF staff)
        {
            //notenbalk leeg maken
            _symbolsToPrint = new List<PSAMControlLibrary.MusicalSymbol>();
            staff.ClearMusicalIncipit();
            
            //alle componenten aflopen en toevoegen aan lijst van te printen symbolen
            foreach (var component in sheet.MusicComponents)
            {
                component.Visit(this);
            }

            //toevoegen aan notenbalk
            foreach (var printable in _symbolsToPrint)
            {
                staff.AddMusicalSymbol(printable);
            }
        }


        public override void NoteResponse(Note note)
        {
            this._symbolsToPrint.Add( new PSAMControlLibrary.Note(note.Tone.ToString().ToUpper(),(int) note.NoteMod, note.Octave, ConvertDurations(note.MusicDuration), 
                NoteStemDirection.Up, NoteTieType.None, 
                new List<NoteBeamType>() { NoteBeamType.Single }) {NumberOfDots = note.AddHalfDuration? 1:0})
            ;
            addMeasureBar(note);
        }

        public override void RestResponse(Rest rest)
        {
            this._symbolsToPrint.Add(new PSAMControlLibrary.Rest(ConvertDurations(rest.MusicDuration)) {NumberOfDots = rest.AddHalfDuration? 1:0 });
            addMeasureBar(rest);
        }

        public void addMeasureBar(MusicComponent comp)
        {
            if (comp is Domain.MusicObject)
            {
                var temp = (MusicObject)comp;
                _amountOfTime += convertDurationIntoAmountOfTime(temp.MusicDuration, temp.AddHalfDuration);
            }
            else if(comp is Chord)
            {
                var temp = (Chord)comp;
                _amountOfTime += convertDurationIntoAmountOfTime(temp.GetDuration(), false);
            }
            if (_amountOfTime >= _currentTimeSignature.Top)
            {
                this._symbolsToPrint.Add(new PSAMControlLibrary.Barline());
                _amountOfTime = 0;
            }
        }



        public override void ChordResponse(Chord chord)
        {
            for (int i = 0; i < chord.MusicComponents.Count; i++)
            {
                Note temp = (Note) chord.MusicComponents[i];
                if (i == 0)
                {
                    this._symbolsToPrint.Add(new PSAMControlLibrary.Note(temp.Tone.ToString().ToUpper(), (int) temp.NoteMod, temp.Octave, 
                    ConvertDurations(chord.GetDuration()),
                        NoteStemDirection.Up, NoteTieType.None,
                        new List<NoteBeamType>() {NoteBeamType.Single}));
                }
                else
                {
                    this._symbolsToPrint.Add(new PSAMControlLibrary.Note(temp.Tone.ToString().ToUpper(), (int)temp.NoteMod, temp.Octave,
                        ConvertDurations(chord.GetDuration()),
                        NoteStemDirection.Up, NoteTieType.None,
                        new List<NoteBeamType>() {NoteBeamType.Single}) {IsChordElement = true});
                }
            }
            addMeasureBar(chord);
        }

        public override void RepeatingResponse(Repeating repeating)
        {
            //todo print een repeat
            throw new System.NotImplementedException();
        }

        public override void TempoResponse(Tempo tempo)
        {
            this._symbolsToPrint.Add(new PSAMControlLibrary.TimeSignature(TimeSignatureType.Numbers, (uint)tempo.Bpm, 1));
        }

        public override void TimeSignatureResponse(TimeSignature signature)
        {
            this._currentTimeSignature = (TimeSignature) signature.Clone();
            this._symbolsToPrint.Add(new PSAMControlLibrary.TimeSignature(TimeSignatureType.Numbers, (uint)signature.Top, (uint)signature.Bottom));
        }

        public override void CleffResponse(Clef clef)
        {
            this._symbolsToPrint.Add(ConvertClef(clef));
        }
        #region helperfunctions
        public MusicalSymbolDuration ConvertDurations(MusicDuration duration)
        {
            switch (duration)
            {
                case MusicDuration.Whole:
                    return MusicalSymbolDuration.Whole;
                case MusicDuration.Half:
                    return MusicalSymbolDuration.Half;
                case MusicDuration.Quarter:
                    return MusicalSymbolDuration.Quarter;
                case MusicDuration.Eight:
                    return MusicalSymbolDuration.Eighth;
                case MusicDuration.Sixteenth:
                    return MusicalSymbolDuration.Sixteenth;
                default:
                    return MusicalSymbolDuration.Whole;
            }
        }

        public PSAMControlLibrary.Clef ConvertClef(Clef clef)
        {
            switch (clef.ClefType)
            {
                case ClefType.GClef:
                    return new PSAMControlLibrary.Clef(PSAMControlLibrary.ClefType.GClef, 2);
                case ClefType.CClef:
                    return new PSAMControlLibrary.Clef(PSAMControlLibrary.ClefType.CClef, 4);
                case ClefType.FClef:
                    return new PSAMControlLibrary.Clef(PSAMControlLibrary.ClefType.FClef, 4);
                default:
                    return new PSAMControlLibrary.Clef(PSAMControlLibrary.ClefType.GClef, 2);
            }
        }

        private double convertDurationIntoAmountOfTime(Domain.MusicDuration duration, bool addhalf)
        {
            double reval;
            switch (duration)
            {
                case MusicDuration.Whole:
                    reval = _currentTimeSignature.Bottom;
                    break;
                case MusicDuration.Half:
                    reval = _currentTimeSignature.Bottom / 2.0;
                    break;
                case MusicDuration.Quarter:
                    reval = _currentTimeSignature.Bottom / 4.0;
                    break;
                case MusicDuration.Eight:
                    reval = _currentTimeSignature.Bottom / 8.0;
                    break;
                case MusicDuration.Sixteenth:
                    reval = _currentTimeSignature.Bottom / 16.0;
                    break;
                default:
                    reval = _currentTimeSignature.Bottom;
                    break;
            }
            if (addhalf)
            {
                reval = reval + reval/2;
            }
            return reval;
        }

        #endregion
    }
}

