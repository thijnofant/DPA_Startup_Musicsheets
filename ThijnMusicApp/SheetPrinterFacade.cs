using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSAMControlLibrary;
using PSAMWPFControlLibrary;

namespace DPA_Musicsheets
{
    public class SheetPrinterFacade
    {       
        public IncipitViewerWPF Staff { get; private set; }

        public SheetPrinterFacade(IncipitViewerWPF staff)
        {
            this.Staff = staff;
        }

        public void PrintMusicSheet(MusicSheet sheet)
        {
            Staff.ClearMusicalIncipit();
            foreach (MusicTrack track in sheet.Tracks)
            {
                this.PrintTrack(track);
            }
        }

        public void PrintMusicalObject(MusicalObject mObject)
        {
            MusicalSymbol print = null;
            if (mObject is Rest)
            {
                print = new PSAMControlLibrary.Rest(DurationToPSAMDuration(mObject.Duur));
            }

            if (mObject is MusicNote)
            {
                var obj = (MusicNote) mObject;
                print = new Note(obj.Tone, 0, obj.Octave, DurationToPSAMDuration(mObject.Duur), NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single });
            }
            if (print != null)
            {
                this.Staff.AddMusicalSymbol(print);
            }
        }

        public MusicalSymbolDuration DurationToPSAMDuration(Duration dur) {
            switch (dur)
            {
                case Duration.Whole:
                    return MusicalSymbolDuration.Whole;
                case Duration.Half:
                    return MusicalSymbolDuration.Half;
                case Duration.Fourth:
                    return MusicalSymbolDuration.Quarter;
                case Duration.Eight:
                    return MusicalSymbolDuration.Eighth;
                case Duration.Sixteenth:
                    return MusicalSymbolDuration.Sixteenth;
                default:
                    return MusicalSymbolDuration.Quarter;
            }
        }

        public void PrintTrack(MusicTrack track)
        {
            if (track.Cleff.CleffType == "G")
            {
                this.Staff.AddMusicalSymbol(new Clef(ClefType.GClef, 2));
            }

            foreach (TrackPiece piece in track.TrackPieces)
            {
                this.PrintTrackPiece(piece);
            }
        }

        public void PrintTrackPiece(TrackPiece piece)
        {
            if (piece.Timesignature!= null)
            {
                Staff.AddMusicalSymbol(new TimeSignature(TimeSignatureType.Numbers, (uint)piece.Timesignature.Upper, (uint)piece.Timesignature.Lower));
            }
            foreach (MusicalObject mObj in piece.MusicalObjects)
            {
                this.PrintMusicalObject(mObj);
            }
        }
    }
}