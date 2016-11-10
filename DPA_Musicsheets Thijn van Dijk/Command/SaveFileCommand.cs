using System;

namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class SaveFileCommand : Command
    {
        public override void Execute()
        {
            string path = context.GetSavePath();
            if (path != "")
            {
                IO.LoadSaveFileStrategy saver = IO.LoadSaveFileStrategy.GetStratFromURL(path, context);
                try
                {
                    saver.Save(context.MusicSheet, path);
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

        public SaveFileCommand(Context con, CommandRegister reg) : base("SaveFile", con, reg)
        {
        }
    }
}

