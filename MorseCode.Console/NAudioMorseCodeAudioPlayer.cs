using MorseCode;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public class NAudioMorseCodeAudioPlayer : IMorseCodePlayer
    {
        

        public int BaseUnitLength { get; private set; }

        public NAudioMorseCodeAudioPlayer(int baseUnitLength)
        {
            BaseUnitLength = baseUnitLength;
        }

        public event EventHandler<EventArgs> OnMorseCodeFinishPlaying;

        public void PlayCharacter(bool[] elements, PlayingProgress progress,CancellationToken cancellationToken)
        {
            List<Action> audioAction = generateMorseCodeAudioActionActionLetter(elements);
            ThreadPool.QueueUserWorkItem(new WaitCallback(PlayMorseCode),
                                        new object[] {audioAction,progress,cancellationToken });
        }

        public void PlayWord(List<bool[]> word, PlayingProgress progress, CancellationToken cancellationToken)
        {
            List<Action> audioAction = generateMorseCodeAudioActionWord(word);
            ThreadPool.QueueUserWorkItem(new WaitCallback(PlayMorseCode),
                                       new object[] { audioAction, progress, cancellationToken });
        }

        public void PlayWords(List<List<bool[]>> words, PlayingProgress progress, CancellationToken cancellationToken)
        {
            List<Action> audioAction = generateMorseCodeAudioActions(words);
            ThreadPool.QueueUserWorkItem(new WaitCallback(PlayMorseCode),
                                       new object[] { audioAction, progress, cancellationToken });
        }


        void PlayDash()
        {
            var tone = new SignalGenerator() { Gain = 1, Frequency = 900, Type = SignalGeneratorType.Sin };
            playTone(tone, BaseUnitLength * 3);
        }
        void PlayDot()
        {
            var tone = new SignalGenerator() { Gain = 1, Frequency = 900, Type = SignalGeneratorType.Sin };
            playTone(tone, BaseUnitLength);
        }
        void PlayEmpty()
        {
            var tone = new SignalGenerator() { Gain = 0, Frequency = 2000, Type = SignalGeneratorType.Sin };
            
            playTone(tone, BaseUnitLength);
        }
        void playTone(ISampleProvider singnal, int time)
        {
            using (var wo = new WaveOutEvent())
            {
               
               
                wo.Init(singnal);
                wo.Play();
                Thread.Sleep(time);
                wo.Stop();

            }
        }


        List<Action> generateMorseCodeAudioActions(List<List<bool[]>> sentence)
        {
            List<Action> audioActions = new List<Action>();
            foreach (var word in sentence)
            {
                audioActions.AddRange(generateMorseCodeAudioActionWord(word));
                //Letter have 3 spacing, require 4 more for Morse code standar of 7 spacing between words
                audioActions.AddRange(RepeatAudioAction(PlayEmpty, 4));              
            }

            return audioActions;
        }
        List<Action> generateMorseCodeAudioActionWord(List<bool[]>word)
        {
            List<Action> audioActions = new List<Action>();
            foreach (var letter in word)
            {
                audioActions.AddRange(generateMorseCodeAudioActionActionLetter(letter));
                // Letter element have 1space spacing on it self, require 2 more spacing for Morse code standar of 3 space between letter
                audioActions.AddRange(RepeatAudioAction(PlayEmpty, 2));
            }
            
            return audioActions;
        }
        List<Action> generateMorseCodeAudioActionActionLetter(bool[] letters)
        {
            List<Action> audioActions = new List<Action>();
            foreach (var element in letters)
            {
                audioActions.Add(element ? new Action(PlayDash) : new Action(PlayDot));
                // Morse code standar, 1 space spacing between element in smae letter 
                audioActions.AddRange(RepeatAudioAction(PlayEmpty, 1));
            }
            

            return audioActions;
        }


        private static List<Action> RepeatAudioAction(Action AudioAction, int count)
        {
            List<Action> blanks = new List<Action>();
            for (int i = 0; i < count; i++)
            {
                blanks.Add(AudioAction);
            }
            return blanks;

        }
        void PlayMorseCode(object objects)
        {
            object[] objectArray = objects as object[];

            List<Action> audioActions = objectArray[0] as List<Action>;
            PlayingProgress progress = objectArray[1] as PlayingProgress;
            CancellationToken cancellationToken = (CancellationToken)objectArray[2];

            foreach (var audio in audioActions)
            {
                audio.Invoke();
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
            OnMorseCodeFinishPlaying?.Invoke(this, new EventArgs());
        }

    }
}
