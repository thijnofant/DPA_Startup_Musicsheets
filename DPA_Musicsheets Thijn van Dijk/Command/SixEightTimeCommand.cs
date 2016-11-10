namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class SixEightTimeCommand : Command
    {
        public SixEightTimeCommand(Context con, CommandRegister reg) : base("AddSixEigthTime", con, reg)
        {
        }

        public override void Execute()
        {
            context.AddTextInEditor(@" \time 6/8 ");
        }

        public override void Undo()
        {
            //todo undo this function
        }
    }
}

