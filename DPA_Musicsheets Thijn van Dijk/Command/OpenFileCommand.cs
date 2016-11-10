namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class OpenFileCommand : Command
    {
        public override void Execute()
        {
            string path = context.GetOpenPath();
            if (path != "")
            {
                IO.LoadSaveFileStrategy loader = IO.LoadSaveFileStrategy.GetStratFromURL(path, context);
                if (context.SetMusicSheet(loader.Load(path)))
                {
                    context.MessageUser("Succes");
                }
                else
                {
                    context.MessageUser("Error");
                }
            }
        }

        public override void Undo()
        {
            //leave empty
        }

        public OpenFileCommand(Context con, CommandRegister reg) : base("OpenFile", con, reg)
        {
        }
    }
}

