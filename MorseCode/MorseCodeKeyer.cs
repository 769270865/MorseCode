using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace MorseCode
{
    public class MorseCodeKeyer
    {
        public event EventHandler<EventArgs> OnStartNewLetter;
        public event EventHandler<EventArgs> OnStartNewWord;
        

        public int BaseUnitTime { get; private set; }

        public char CurrentKeyedCharacter { get; private set; }

        IKeyerInput keyerInput;
        List<bool> keyedElements;

        Stopwatch keyPressedLength;
        Timer NewLetterTimer;
        Timer NewWordTimer;

        public MorseCodeKeyer(int baseUnit, IKeyerInput Input)
        {
            BaseUnitTime = baseUnit;
            keyPressedLength = new Stopwatch();
            keyerInput = Input;
            keyerInput.OnKeyDown += onKeyDown;
            keyerInput.OnKeyUp += onKeyUp;

            NewLetterTimer = new Timer(3 * BaseUnitTime) { AutoReset = true, Enabled = false };
            NewLetterTimer.Elapsed += startNewLetter;
            
            NewWordTimer = new Timer(7 * BaseUnitTime) { AutoReset = true, Enabled = false };
            NewWordTimer.Elapsed += startNewWord;
            
        }
        void onKeyDown(object e,EventArgs args )
        {
            keyPressedLength.Start();

            NewLetterTimer.Stop(); 
           
            NewWordTimer.Stop();
        }
        void onKeyUp(object e, EventArgs args)
        {
            keyPressedLength.Stop();

            keyedElements.Add(evulateDotDash((int)keyPressedLength.ElapsedMilliseconds));

            keyPressedLength.Reset();

            evulateCurrentCharacter(keyedElements);

            NewLetterTimer.Stop();
            NewLetterTimer.Start();
            NewWordTimer.Stop();
            NewWordTimer.Start();
           

        }
        void startNewLetter(object o,ElapsedEventArgs args)
        {
            keyedElements = new List<bool>();
            CurrentKeyedCharacter = '\0';
            OnStartNewLetter.Invoke(this, new EventArgs());
        }
        void startNewWord(object o,ElapsedEventArgs args)
        {
            keyedElements = new List<bool>();
            CurrentKeyedCharacter = '\0';
            OnStartNewWord.Invoke(this, new EventArgs());
        }

        bool evulateDotDash(int keyPressedLength)
        {

            return keyPressedLength >= BaseUnitTime - (BaseUnitTime * 0.25) && keyPressedLength <= BaseUnitTime + (BaseUnitTime * 0.25);
            
        }
        void evulateCurrentCharacter(List<bool> keyedElements)
        {
            CurrentKeyedCharacter = MorseCodeConvert.characterToMorseCode.FirstOrDefault(p => p.Value == keyedElements.ToArray()).Key;
            
        }

    }
}
