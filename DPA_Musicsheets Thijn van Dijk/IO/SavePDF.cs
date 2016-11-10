using System;
using System.Diagnostics;
using System.IO;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;

namespace DPA_Musicsheets_Thijn_van_Dijk.IO
{
    public class SavePdf : LoadSaveFileStrategy
    {
        public override MusicSheet Load(string uri)
        {
            throw new System.NotImplementedException();
        }

        public override bool Save(MusicSheet sheet, string uri)
        {
            string lilypondLocation = @"D:\Program Files (x86)\LilyPond\usr\bin\lilypond.exe";
            string sourceFolder = @"D:\Users\Thijn\Desktop\DPA temp\";
            string sourceFileName = "Twee-emmertjes-water-halen";

            LoadSaveLilypond saver = new LoadSaveLilypond(_context);
            saver.Save(sheet, $"{sourceFolder}{sourceFileName}.ly");
            
            var process = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = sourceFolder,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = String.Format("--pdf \"{0}{1}\"", sourceFolder, sourceFileName + ".ly"),
                    FileName = lilypondLocation
                }
            };

            process.Start();
            while (!process.HasExited) { }

            File.Copy(sourceFolder + sourceFileName + ".pdf", uri, true);

            return true;
        }



        public SavePdf(Context cont) : base(cont)
        {
        }
    }
}

