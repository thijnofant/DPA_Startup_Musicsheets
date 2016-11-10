using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;

namespace DPA_Musicsheets_Thijn_van_Dijk.Memento
{
    public class MementoCaretaker
    {
        private List<Memento> mementos;

        public MementoCaretaker()
        {
            this.mementos = new List<Memento>();
        }

        ///  
        public Memento NewMemento(Context cont)
        {
            var temp = new Memento(cont.MusicSheet.Clone(), string.Copy(cont.LilypondEditor.Text),
                cont.MemState+1);
            mementos = mementos.Where(x => x.state != temp.state).ToList();
            mementos.Add(temp);
            return temp;
        }

        ///  
        public Memento GetMemento(int state)
        {
            Memento temp = mementos[0];
            if (state < 0)
            {
                return mementos[0];
            }
            foreach (Memento memento in mementos)
            {
                if (memento.state == state)
                {
                    return memento;
                }
                if (memento.state > temp.state)
                {
                    temp = memento;
                }
            }
            return temp;
        }

    }
}

