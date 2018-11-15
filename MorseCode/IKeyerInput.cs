using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseCode
{
    public interface IKeyerInput
    {
       event EventHandler<EventArgs> OnKeyDown;
       event EventHandler<EventArgs> OnKeyUp;
       int KeyCode { get; }
    }
}
