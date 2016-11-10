using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using DPA_Musicsheets_Thijn_van_Dijk.Command;
using DPA_Musicsheets_Thijn_van_Dijk.Conf;

namespace DPA_Musicsheets_Thijn_van_Dijk.Chain
{
    public class SavePDFHandler : ChainHandler
    {
        public SavePDFHandler(Context con, CommandRegister reg, ChainHandler nextHandler = null) : base(con, reg, nextHandler)
        {
        }

        public override bool Handle(List<Key> keys)
        {
            if (CanHandle(keys))
            {
                Debug.WriteLine("PDF save Chain handler handling");
                this.commandRegister.ExcecuteCommand("SaveToPDF");
                base.Handle(keys); //comment out this line to make the chain break if this handler can handle this
                return true;
            }
            else
            {
                return base.Handle(keys);
            }
        }

        public override bool CanHandle(List<Key> keys)
        {
            return CompareKeyLists(keys, KeyboardConfig.Instance.config["SaveToPDF"]) || CompareKeyLists(keys, KeyboardConfig.Instance.config["altSaveToPDF"]);
        }
    }
}

