using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using DPA_Musicsheets_Thijn_van_Dijk.Chain;
using DPA_Musicsheets_Thijn_van_Dijk.Command;
using DPA_Musicsheets_Thijn_van_Dijk.Domain;
using DPA_Musicsheets_Thijn_van_Dijk.Memento;
using DPA_Musicsheets_Thijn_van_Dijk.State;
using DPA_Musicsheets_Thijn_van_Dijk.Visitors;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace DPA_Musicsheets_Thijn_van_Dijk
{
    public class Context
    {
        //variable that allows the keydown thing to happen
        public Window Window { get; private set; }
        public TextBox LilypondEditor { get; private set; }

        public PSAMWPFControlLibrary.IncipitViewerWPF Staff { get; private set; }

        public int MemState { get; set; }
        private State.State state;
        private ExitState saveState;

        public MusicSheet MusicSheet { get; private set; }

        public CommandRegister CommandRegister { get; private set; }
        private KeyboardChain keyBoardChainOfCommand;
        private MementoCaretaker caretaker;
        private Timer time;
        private List<System.Windows.Input.Key> keysDown;

        public Context()
        {
            this.keysDown = new List<Key>();
            time = new Timer {Interval = 1500};
            time.Tick += Time_Tick;

            caretaker = new MementoCaretaker();
            MusicSheet = new MusicSheet("");
        }

        private void LilypondEditor_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            keysDown.Remove(e.Key);
            if (keysDown.Count == 0)
            {
                time.Start();
            }
        }

        private void LilypondEditor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            time.Stop();
            saveState.edit(this);
            keysDown.Add(e.Key);
        }

        private void Time_Tick(object sender, System.EventArgs e)
        {
            time.Stop();
            Debug.WriteLine("Lilypond to Sheet");
            SetMusicSheet(new IO.LilypondConverter().LilypondToSheet(this.LilypondEditor.Text));
            CreateMemento();
            int index = LilypondEditor.SelectionStart;
            CommandRegister.ExcecuteCommand("PrintSheet");
            LilypondEditor.SelectionStart = index;
        }

        public bool SetMusicSheet(MusicSheet newSheet)
        {
            if (newSheet != null)
            {
                this.MusicSheet = newSheet;
                this.LilypondEditor.Text = new IO.LilypondConverter().SheetToLilypond(MusicSheet);
                CommandRegister.ExcecuteCommand("PrintSheet");
                return true;
            }
            return false;   
        }

        public bool AddTextInEditor(string text)
        {
            int index = LilypondEditor.SelectionStart;
            if (LilypondEditor.IsFocused)
            {
                LilypondEditor.Text = LilypondEditor.Text.Insert(index, text);
                LilypondEditor.SelectionStart = index + text.Length;
                return true;
            }
            return false;
        }

        public void InitializeProgram(Window window, PSAMWPFControlLibrary.IncipitViewerWPF staff, TextBox lilypondEditor)
        {
            this.Window = window;
            this.Staff = staff;
            this.LilypondEditor = lilypondEditor;
            this.saveState = new Saved();
            this.state = new Typing();
            CreateMemento();

            Window.Closing += Window_Closing;

            LilypondEditor.KeyDown += LilypondEditor_KeyDown;
            LilypondEditor.KeyUp += LilypondEditor_KeyUp;
            CommandRegister = new CommandRegister(this);
            keyBoardChainOfCommand = new KeyboardChain(this, CommandRegister);
            CommandRegister.Init();
            keyBoardChainOfCommand.Init();
            
            MusicSheet.AddMusicComponent(new Clef(ClefType.GClef));
            MusicSheet.AddMusicComponent(new TimeSignature(4, 4));
            MusicSheet.AddMusicComponent(new Tempo(120));

            //single notes with vars
            MusicSheet.AddMusicComponent(new Note(false, MusicDuration.Half, NoteTone.a, 4, NoteMod.None));
            MusicSheet.AddMusicComponent(new Note(true,  MusicDuration.Half, NoteTone.a, 4, NoteMod.None));
            MusicSheet.AddMusicComponent(new Note(false, MusicDuration.Half, NoteTone.a, 4, NoteMod.Cross));
            MusicSheet.AddMusicComponent(new Note(false, MusicDuration.Half, NoteTone.a, 4, NoteMod.Mole));
            MusicSheet.AddMusicComponent(new Rest(false, MusicDuration.Half));
            MusicSheet.AddMusicComponent(new Rest(true, MusicDuration.Half));

            MusicSheet.AddMusicComponent(new Note(false, MusicDuration.Whole, NoteTone.a, 4, NoteMod.None));
            
            //chords
            MusicSheet.AddMusicComponent(new Chord()
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.a, 3, NoteMod.None))
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.e, 4, NoteMod.None))
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.g, 4, NoteMod.None))
                );


            
            MusicSheet.AddMusicComponent(new Chord()
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.c, 5, NoteMod.None))
                .AddNote(new Note(false, MusicDuration.Quarter, NoteTone.e, 5, NoteMod.None))
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.g, 5, NoteMod.None))
                );

            MusicSheet.AddMusicComponent(new Chord()
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.c, 4, NoteMod.None))
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.e, 4, NoteMod.None))
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.g, 4, NoteMod.None))
                );

            MusicSheet.AddMusicComponent(new Chord()
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.c, 4, NoteMod.None))
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.e, 4, NoteMod.None))
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.g, 4, NoteMod.None))
                );

            MusicSheet.AddMusicComponent(new Chord()
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.c, 5, NoteMod.None))
                .AddNote(new Note(false, MusicDuration.Quarter, NoteTone.e, 5, NoteMod.None))
                .AddNote(new Note(false, MusicDuration.Half, NoteTone.g, 5, NoteMod.None))
                );

            this.LilypondEditor.Text = new IO.LilypondConverter().SheetToLilypond(MusicSheet);
            CommandRegister.ExcecuteCommand("PrintSheet");
            //CommandRegister.ExcecuteCommand("SaveLilypond");
            //CommandRegister.ExcecuteCommand("OpenFile");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveState.HandleClose(this, e);
        }

        #region state functions

        public void SetSaveState(ExitState newState)
        {
            this.saveState = newState;
        }
        #endregion

        #region dialogs
        public string GetOpenPath(string filters = "Lilypond|*.ly|Midi|*.mid")
        {
            OpenFileDialog dial = new OpenFileDialog();
            dial.Filter = filters;
            dial.FilterIndex = 1;
            return dial.ShowDialog() == DialogResult.OK ? dial.FileName : "";
        }

        public string GetSavePath(string filters = "Lilypond|*.ly|Midi|*.mid|pdf|*.pdf")
        {
            SaveFileDialog dial = new SaveFileDialog();
            dial.AddExtension = true;
            dial.Filter = filters;
            dial.FilterIndex = 1;
            return dial.ShowDialog() == DialogResult.OK ? dial.FileName : "";
        }

        public void MessageUser(string message)
        {
            MessageBox.Show(message);
        }
        #endregion

        #region Memento functions
        public void RestoreFromMemento(Memento.Memento mem)
        {
            this.MemState = mem.state;
            this.MusicSheet = mem.musicSheet.Clone();
            this.LilypondEditor.Text = string.Copy(mem.editorContents);
            Debug.WriteLine(this.MemState);
        }

        public Memento.Memento CreateMemento()
        {
            var tempMem = caretaker.NewMemento(this);
            this.MemState = tempMem.state;
            Debug.WriteLine(this.MemState);
            return tempMem;
        }

        public void undoByMemento()
        {
            RestoreFromMemento(caretaker.GetMemento(this.MemState-1));
        }

        public void redoByMemento()
        {
            RestoreFromMemento(caretaker.GetMemento(this.MemState + 1));
        }

        #endregion
    }
}

