using MorseCode;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class AudioOutput : IOutPut
    {
        public void OutPutSentence(List<List<bool[]>> sentence, string outputPath)
        {
            throw new NotImplementedException();
        }

        public void OutPutWord(List<bool[]> word, string outputPath)
        {
            throw new NotImplementedException();
        }
    }


}
