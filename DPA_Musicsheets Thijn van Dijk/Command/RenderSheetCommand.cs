using DPA_Musicsheets_Thijn_van_Dijk.Domain;
using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class RenderSheetCommand: Command
    {
        public RenderSheetCommand(Context con, CommandRegister reg) : base("PrintSheet", con, reg)
        {
        }

        public override void Execute()
        {
            SheetPrintVisitor sheetPrintVisitor = new SheetPrintVisitor();
            sheetPrintVisitor.Print(context.MusicSheet, context.Staff);
        }

        public override void Undo()
        {
            //leave empty cause this can't be undone
        }
    }
}