using System;
using System.Diagnostics;
using DPA_Musicsheets_Thijn_van_Dijk.IO;

namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class SaveLilypondCommand : Command
    {
        public override void Execute()
        {
            Debug.WriteLine("Commanding to write Lilipond");

            string path = context.GetSavePath("Lilypond|*.ly");
            if (path != "")
            {
                var lilypondWriter = new LoadSaveLilypond(this.context);

                try
                {
                    lilypondWriter.Save(this.context.MusicSheet, path);
                    context.MessageUser("Saving was succesfull");
                }
                catch (Exception e)
                {
                    context.MessageUser("Saving failed with errormessage" + e.Message);
                }
            }
        }

        public override void Undo()
        {
            //leave empty cause this can't be undone
        }

        public SaveLilypondCommand(Context con, CommandRegister reg) : base("SaveLilypond", con, reg) { }
    }
}

