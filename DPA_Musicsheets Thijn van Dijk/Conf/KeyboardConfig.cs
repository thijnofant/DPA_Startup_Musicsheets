using System.Collections.Generic;
using Key = System.Windows.Input.Key;

namespace DPA_Musicsheets_Thijn_van_Dijk.Conf
{
    public class KeyboardConfig
    {
        public Dictionary<string, List<Key>> config { get; private set; }

        private static KeyboardConfig _instance;

        private KeyboardConfig()
        {
            config = new Dictionary<string, List<Key>>
            {
                {"SaveLilypond", new List<Key> {Key.LeftCtrl, Key.S}},
                {"altSaveLilypond", new List<Key> {Key.RightCtrl, Key.S}},
                {"SaveToPDF", new List<Key> {Key.LeftCtrl, Key.P}},
                {"altSaveToPDF", new List<Key> {Key.RightCtrl, Key.P}},
                {"SaveToMidi", new List<Key> {Key.LeftCtrl, Key.M}},
                {"altSaveToMidi", new List<Key> {Key.RightCtrl, Key.M}},
                {"OpenFile", new List<Key> {Key.LeftCtrl, Key.O}},
                {"PlayMusic", new List<Key> {Key.LeftCtrl, Key.F}},
                {"AddCleff", new List<Key> {Key.LeftShift, Key.C}},
                {"AddTempo", new List<Key> {Key.LeftShift, Key.S}},
                {"AddFourFourthTime", new List<Key> {Key.LeftCtrl, Key.T, Key.D4}},
                {"AddThreeFourthsTime", new List<Key> {Key.LeftCtrl, Key.T, Key.D3}},
                {"AddSixEigthTime", new List<Key> {Key.LeftCtrl, Key.T, Key.D6}}
            };
        }

        public static KeyboardConfig Instance => _instance ?? (_instance = new KeyboardConfig());
    }
}

