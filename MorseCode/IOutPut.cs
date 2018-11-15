using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MorseCode
{
    public interface IOutPut
    {
        void OutPutWord(List<bool[]> word,string outputPath);
        void OutPutSentence(List<List<bool[]>> sentence, string outputPath);
       
    }
}
