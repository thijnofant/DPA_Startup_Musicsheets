using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Win32;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;
using Note = PSAMControlLibrary.Note;

namespace DPA_Musicsheets_Thijn_van_Dijk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private MidiPlayer _player;
        //public ObservableCollection<MidiTrack> MidiTracks { get; private set; }

        // De OutputDevice is een midi device of het midikanaal van je PC.
        // Hierop gaan we audio streamen.
        // DeviceID 0 is je audio van je PC zelf.
        private OutputDevice _outputDevice = new OutputDevice(0);
        private Context context = new Context();

        public MainWindow()
        {
            //this.MidiTracks = new ObservableCollection<MidiTrack>();
            InitializeComponent();
            //FillPSAMViewer();

            context.InitializeProgram(this, this.staff, this.tb_LilypondEditor);
            //DataContext = MidiTracks;
            
            //notenbalk.LoadFromXmlFile("Resources/example.xml");
        }

        private void FillPSAMViewer()
        {
            staff.ClearMusicalIncipit();
            

            // Clef = sleutel
            staff.AddMusicalSymbol(new Clef(ClefType.GClef, 2));
            staff.AddMusicalSymbol(new PSAMControlLibrary.TimeSignature(TimeSignatureType.Numbers, 4, 4));
            /* 
                The first argument of Note constructor is a string representing one of the following names of steps: A, B, C, D, E, F, G. 
                The second argument is number of sharps (positive number) or flats (negative number) where 0 means no alteration. 
                The third argument is the number of an octave. 
                The next arguments are: duration of the note, stem direction and type of tie (NoteTieType.None if the note is not tied). 
                The last argument is a list of beams. If the note doesn't have any beams, it must still have that list with just one 
                    element NoteBeamType.Single (even if duration of the note is greater than eighth). 
                    To make it clear how beamlists work, let's try to add a group of two beamed sixteenths and eighth:
                        Note s1 = new Note("A", 0, 4, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start});
                        Note s2 = new Note("C", 1, 5, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.End });
                        Note e = new Note("D", 0, 5, MusicalSymbolDuration.Eighth, NoteStemDirection.Down, NoteTieType.None,new List<NoteBeamType>() { NoteBeamType.End });
                        viewer.AddMusicalSymbol(s1);
                        viewer.AddMusicalSymbol(s2);
                        viewer.AddMusicalSymbol(e); 
            */

            staff.AddMusicalSymbol(new PSAMControlLibrary.Note("A", 0, 4, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start }));
            staff.AddMusicalSymbol(new PSAMControlLibrary.Note("C", 1, 5, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.End }));
            staff.AddMusicalSymbol(new PSAMControlLibrary.Note("D", 0, 5, MusicalSymbolDuration.Eighth, NoteStemDirection.Down, NoteTieType.Start, new List<NoteBeamType>() { NoteBeamType.End }));
            staff.AddMusicalSymbol(new Barline());

            staff.AddMusicalSymbol(new PSAMControlLibrary.Note("D", 0, 5, MusicalSymbolDuration.Whole, NoteStemDirection.Down, NoteTieType.Stop, new List<NoteBeamType>() { NoteBeamType.Single }));
            staff.AddMusicalSymbol(new PSAMControlLibrary.Note("E", 0, 4, MusicalSymbolDuration.Quarter, NoteStemDirection.Up, NoteTieType.Start, new List<NoteBeamType>() { NoteBeamType.Single }) { NumberOfDots = 1 });
            staff.AddMusicalSymbol(new Barline());

            staff.AddMusicalSymbol(new PSAMControlLibrary.Note("C", 0, 4, MusicalSymbolDuration.Half, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single }));
            staff.AddMusicalSymbol(
                new PSAMControlLibrary.Note("E", 0, 4, MusicalSymbolDuration.Half, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single })
                { IsChordElement = true });
            staff.AddMusicalSymbol(
                new PSAMControlLibrary.Note("G", 0, 4, MusicalSymbolDuration.Half, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single })
                { IsChordElement = true });
            staff.AddMusicalSymbol(new Barline());
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            //if(_player != null)
            //{
            //    _player.Dispose();
            //}

            //_player = new MidiPlayer(_outputDevice);
            //_player.Play(txt_MidiFilePath.Text);
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Midi Files(.mid)|*.mid" };
            if (openFileDialog.ShowDialog() == true)
            {
                txt_MidiFilePath.Text = openFileDialog.FileName;
            }
        }
        
        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            //if (_player != null)
            //    _player.Dispose();
        }

        private void btn_ShowContent_Click(object sender, RoutedEventArgs e)
        {
            //ShowMidiTracks(MidiReader.ReadMidi(txt_MidiFilePath.Text));
        }

        //private void ShowMidiTracks(IEnumerable<MidiTrack> midiTracks)
        //{
        //    MidiTracks.Clear();
        //    foreach (var midiTrack in midiTracks)
        //    {
        //        MidiTracks.Add(midiTrack);
        //    }

        //    tabCtrl_MidiContent.SelectedIndex = 0;
        //}
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _outputDevice.Close();
            //if (_player != null)
            //{
            //    _player.Dispose();
            //}
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenFileBtn(object sender, RoutedEventArgs e)
        {
            context.CommandRegister.ExcecuteCommand("OpenFile");
        }

        private void btn_SaveLilypond_Click(object sender, RoutedEventArgs e)
        {
            context.CommandRegister.ExcecuteCommand("OpenFile");
        }

        private void btn_SaveMidi_Click(object sender, RoutedEventArgs e)
        {
            context.CommandRegister.ExcecuteCommand("OpenFile");
        }

        private void btn_SavePDF_Click(object sender, RoutedEventArgs e)
        {
            context.CommandRegister.ExcecuteCommand("OpenFile");
        }

        private void btn_SaveFile_Click(object sender, RoutedEventArgs e)
        {
            context.CommandRegister.ExcecuteCommand("SaveFile");
        }

        private void btn_undoByMem_Click(object sender, RoutedEventArgs e)
        {
            context.undoByMemento();
        }

        private void btn_redoMem_Click(object sender, RoutedEventArgs e)
        {
            context.redoByMemento();
        }
    }
}
