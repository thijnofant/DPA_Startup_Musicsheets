namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class FourFourthTimeCommand: Command
    {
        public FourFourthTimeCommand(Context con, CommandRegister reg) : base("AddFourFourthTime", con, reg)
        {
        }

        public override void Execute()
        {
            context.AddTextInEditor(@" \time 4/4 ");
        }

        public override void Undo()
        {
            //todo undo this
        }
    }
}

