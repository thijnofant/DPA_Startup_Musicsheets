using DPA_Musicsheets_Thijn_van_Dijk.Visitors;

namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class PlayMusicCommand : Command
    {
        public override void Execute()
        {
            var player = new AudioPlayerVisitor();
            player.Play(context.MusicSheet);
        }

        public override void Undo()
        {
            //leave empty this can't be undone
        }

        public PlayMusicCommand(Context con, CommandRegister reg) : base("PlayMusic", con, reg)
        {
        }
    }
}

