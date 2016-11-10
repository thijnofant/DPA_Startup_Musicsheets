namespace DPA_Musicsheets_Thijn_van_Dijk.State
{
    public class Saved : ExitState
    {
        public override void edit(Context con)
        {
            con.SetSaveState(new Unsaved());
        }
    }
}

