using System;
using System.Collections.Generic;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;

public class MusicSheetBuilder
{
	private MusicSheet newMusicSheet;

    public MusicSheetBuilder(string name)
    {
        newMusicSheet = new MusicSheet(name);
    }

	public MusicSheet GetResult()
	{
	    return newMusicSheet;
	}

	public void AddMusicComponent(MusicComponent component)
	{
	    if (component != null)
	    {
            newMusicSheet.MusicComponents.Add(component);
        }
	}

    public void AddNote(NoteTone tone = NoteTone.c, int octave = 4, MusicDuration duration = MusicDuration.Whole, NoteMod noteMod = NoteMod.None, bool addHalf = false)
    {
        this.AddMusicComponent(new Note(addHalf, duration, tone, octave, noteMod));
    }

    public void AddRest(MusicDuration duration = MusicDuration.Whole, bool addHalf = false)
    {
        this.AddMusicComponent(new Rest(addHalf, duration));
    }

    public void AddTempo(int bpm)
    {
        this.AddMusicComponent(new Tempo(bpm));
    }

    public void AddTimeSignature(int top, int bottom)
    {
        this.AddMusicComponent(new TimeSignature(top, bottom));
    }

    public void AddChord(List<Note> notes)
    {
        Chord tempCord = new Chord();
        foreach (Note note in notes)
        {
            tempCord.AddNote(note);
        }
        AddMusicComponent(tempCord);
    }

    public void AddClef(ClefType type = ClefType.GClef)
    {
        AddMusicComponent(new Clef(type));
    }

    public void AddRepeating()
    {
        throw new NotImplementedException();
    }
}

