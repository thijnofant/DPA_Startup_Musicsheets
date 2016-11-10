using System.Windows.Forms;

namespace DPA_Musicsheets_Thijn_van_Dijk.State
{
    public class Unsaved : ExitState
    {
        public override void save(Context con)
        {
            con.SetSaveState(new Saved());
        }
        public override void HandleClose(Context con, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This file isn't saved. Are you sure you want to exit the program?", "Exit", MessageBoxButtons.YesNo);
            switch (dialogResult)
            {
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    e.Cancel = true;
                    break;
            }
        }
    }
}

