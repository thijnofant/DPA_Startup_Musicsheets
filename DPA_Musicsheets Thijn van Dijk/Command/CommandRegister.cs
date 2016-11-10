using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public class CommandRegister
    {
        private List<Command> _commandHistory;
        private int _historyIndex = 1;
        private Dictionary<string, Command> _commands;
        private Context _context;

        public CommandRegister(Context con)
        {
            this._context = con;

            this._commands = new Dictionary<string, Command>();
            this._commandHistory = new List<Command>();
        }

        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void Init()
        {
            new SaveLilypondCommand(_context, this);
            new ThreeFourthTimeCommand(_context, this);
            new SixEightTimeCommand(_context, this);
            new SaveMidiCommand(_context, this);
            new SavePDFCommand(_context, this);
            new PlayMusicCommand(_context, this);
            new SaveFileCommand(_context, this);
            new OpenFileCommand(_context, this);
            new FourFourthTimeCommand(_context, this);
            new AddTempoCommand(_context, this);
            new AddCleffCommand(_context, this);
            new RenderSheetCommand(_context, this);
        }

        public void ExcecuteCommand(string commandKey)
        {
            Command comToExecute = this._commands[commandKey];

            if (comToExecute != null)
            {
                comToExecute.Execute();
                _commandHistory.Add(comToExecute);
                _historyIndex++;
            }
        }

        public void UndoLastCommand()
        {
            //todo fix this
            _historyIndex--;
            this._commandHistory[_historyIndex]?.Undo();
        }

        public void RegisterCommand(string name, Command command)
        {
            this._commands.Add(name, command);
        }
    }
}

