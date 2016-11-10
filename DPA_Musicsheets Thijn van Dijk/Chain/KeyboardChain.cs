using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using DPA_Musicsheets_Thijn_van_Dijk.Command;

namespace DPA_Musicsheets_Thijn_van_Dijk.Chain
{
    public class KeyboardChain
    {
        private List<System.Windows.Input.Key> keysDown;
        private Context context;
        private CommandRegister register;
        private ChainHandler firstHandler;

        public KeyboardChain(Context con, CommandRegister reg)
        {
            this.context = con;
            this.register = reg;
            keysDown = new List<Key>();
        }

        public void Init()
        {
            firstHandler = new PlayMusicHandler(context, register);
            firstHandler
                .SetNext(new OpenFileHandler(context, register))
                .SetNext(new SaveLilypondHandler(context, register))
                .SetNext(new SaveMidiHandler(context, register))
                .SetNext(new SavePDFHandler(context, register))
                .SetNext(new AddCleffHandler(context, register))
                .SetNext(new AddTempoHandler(context, register))
                .SetNext(new AddFourFourthTimeHandler(context, register))
                .SetNext(new AddSixEightTimeHandler(context, register))
                .SetNext(new AddThreeFourthTimeHandler(context, register));

            context.Window.KeyDown += KeyDown;
            context.Window.KeyUp += KeyUp;
        }

        public void KeyUp(object sender, KeyEventArgs e)
        {
            keysDown.Remove(e.Key);
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine(string.Format("Key {0} was pressed", e.Key));
            if (!keysDown.Contains(e.Key))
            {
                keysDown.Add(e.Key);
            }
            if (firstHandler.Handle(keysDown)) // The chain always returns handled/not handled.
            {
                e.Handled = true;
                keysDown.Clear();
            }
        }

        public bool Handle(List<System.Windows.Input.Key> keys)
        {
            if (firstHandler != null) { return firstHandler.Handle(keys); }
            else { return false; }
        }
    }
}

