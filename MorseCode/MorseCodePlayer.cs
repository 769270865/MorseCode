using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MorseCode
{
    public class MorseCodePlayer
    {
        IMorseCodePlayer morseCodePlayer;
        IOutPut outPut;

        public MorseCodePlayer(IMorseCodePlayer player,IOutPut audioOutPut)
        {
            morseCodePlayer = player;
            outPut = audioOutPut;
        }

        public void PlayCharacter(char character,PlayingProgress progress,CancellationToken cancellationToken)
        {
            morseCodePlayer.PlayCharacter(MorseCodeConvert.characterToMorseCode[character],progress,cancellationToken);
        }
        

        public void PlayWord(string word, PlayingProgress progress, CancellationToken canclelationToken)
        {        
            morseCodePlayer.PlayWord(MorseCodeConvert.WordToMorseElement(word), progress,canclelationToken);
        }
        public void OutPutWord(string word,string audioFileOutPutPath)
        {

            List<bool[]> wordElements = MorseCodeConvert.WordToMorseElement(word);

            outPut.OutPutWord(wordElements, audioFileOutPutPath);

            
        }
        public void PlaySentence(string sentence, PlayingProgress progress, CancellationToken canclelationToken)
        {
          
            morseCodePlayer.PlayWords(MorseCodeConvert.SentenceToMorseElements(sentence), progress, canclelationToken);
          
        }
        public void OutputSentence(string sentence, string audioFileOutPutPath)
        {
            List<List<bool[]>> elements = MorseCodeConvert.SentenceToMorseElements(sentence);

            outPut.OutPutSentence(elements, audioFileOutPutPath);
            
        }
        
    }
}
