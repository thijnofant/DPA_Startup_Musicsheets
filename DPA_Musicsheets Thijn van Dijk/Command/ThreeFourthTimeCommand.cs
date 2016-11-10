namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class ThreeFourthTimeCommand : Command
    {
        public ThreeFourthTimeCommand(Context con, CommandRegister reg) : base("AddThreeFourthsTime", con, reg)
        {
        }

        public override void Execute()
        {
            context.AddTextInEditor(@" \time 3/4 ");
        }

        public override void Undo()
        {
            //todo undo this function
        }
    }
}

