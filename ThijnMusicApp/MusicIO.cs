using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets
{
    public interface MusicIO
    {
        MusicSheet Load(string path);
        bool Save(string path, MusicSheet sheet);
    }
}