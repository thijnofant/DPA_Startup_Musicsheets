namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class AddCleffCommand : Command
    {
        public override void Execute()
        {
            context.AddTextInEditor(@" \clef treble ");
        }

        public override void Undo()
        {
            //todo implement undo
            throw new System.NotImplementedException();
        }

        public AddCleffCommand(Context con, CommandRegister reg) : base("AddCleff", con, reg)
        {
        }
    }
}

