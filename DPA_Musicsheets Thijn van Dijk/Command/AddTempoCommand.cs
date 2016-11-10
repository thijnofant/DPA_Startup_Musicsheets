namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class AddTempoCommand : Command
    {
        public override void Execute()
        {
            context.AddTextInEditor(@" \tempo 4=120 ");
        }

        public override void Undo()
        {
            //todo undo this
        }

        public AddTempoCommand(Context con, CommandRegister reg) : base("AddTempo", con, reg)
        {
        }
    }
}

