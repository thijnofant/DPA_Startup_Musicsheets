using System.Diagnostics;
using System.IO;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;

namespace DPA_Musicsheets_Thijn_van_Dijk.IO
{
    public abstract class LoadSaveFileStrategy
    {
        protected Context _context ;

        public abstract MusicSheet Load(string uri);

        public abstract bool Save(MusicSheet sheet, string uri);

        public static LoadSaveFileStrategy GetStratFromURL(string url, Context cont)
        {
            string ext = Path.GetExtension(url);
            Debug.WriteLine(ext);
            switch (ext)
            {
                case ".ly":
                    Debug.WriteLine("File is lilypond");
                    return new LoadSaveLilypond(cont);
                case ".mid":
                    Debug.WriteLine("File is midi");
                    return new LoadSaveMidi(cont);
                case ".pdf":
                    Debug.WriteLine("File is PDF");
                    return new SavePdf(cont);
                default:
                    Debug.WriteLine("wat moet ik nou weer met deze file?");
                    return null;
            }
        }

        public LoadSaveFileStrategy(Context cont)
        {
            _context = cont;
        }
    }
}

