using System;
using System.Diagnostics;
using DPA_Musicsheets_Thijn_van_Dijk.IO;

namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class SavePDFCommand : Command
    {
        public override void Execute()
        {
            Debug.WriteLine("Commanding to write PDF");

            string path = context.GetSavePath("PDF|*.pdf");
            if (path != "")
            {
                var lilypondWriter = new SavePdf(context);

                try
                {
                    lilypondWriter.Save(this.context.MusicSheet, path);
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
        }

        public SavePDFCommand(Context con, CommandRegister reg) : base("SaveToPDF", con, reg)
        {
        }
    }
}

