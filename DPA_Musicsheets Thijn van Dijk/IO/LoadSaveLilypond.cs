using System;
using System.Diagnostics;
using System.IO;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;

namespace DPA_Musicsheets_Thijn_van_Dijk.IO
{
    public class LoadSaveLilypond : LoadSaveFileStrategy
    {
        public override MusicSheet Load(string uri)
        {
            Debug.WriteLine("Opening Lilypond");
            string lilypondContents = File.ReadAllText(uri);
            return new LilypondConverter().LilypondToSheet(lilypondContents);
        }

        public override bool Save(MusicSheet sheet, string uri)
        {
            Debug.WriteLine("Writing Lilypond");
            string lilypondContents = new LilypondConverter().SheetToLilypond(_context.MusicSheet);
            try
            {
                File.WriteAllText(uri, lilypondContents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public LoadSaveLilypond(Context cont) : base(cont){}
    }
}

