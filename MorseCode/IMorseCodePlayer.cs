using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MorseCode
{
    public interface IMorseCodePlayer
    {
        int BaseUnitLength { get;  }

        event EventHandler<EventArgs> OnMorseCodeFinishPlaying;

        void PlayCharacter(bool[] elements,PlayingProgress progress,CancellationToken cancellationToken);

        void PlayWord(List<bool[]> letters , PlayingProgress progress , CancellationToken canclelationToken);

        void PlayWords(List<List<bool[]>> words, PlayingProgress progress, CancellationToken cancellationToken);

    }
}
