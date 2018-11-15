using InputManager;
using MorseCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioPlayer
{

    
    class ConsoleKeyerInput : IKeyerInput,IDisposable
    {
        public event EventHandler<EventArgs> OnKeyDown;
        public event EventHandler<EventArgs> OnKeyUp;

        ConsoleKey morseKey;
        CancellationTokenSource tokenSource;

        public int KeyCode => throw new NotImplementedException();

        public ConsoleKeyerInput(ConsoleKey key)
        {
            morseKey = key;
            tokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(new WaitCallback(ListenKeyEvent),
                                          tokenSource.Token );

            KeyboardHook.KeyDown += new KeyboardHook.KeyDownEventHandler((i) => Console.WriteLine(i + "hgfh"));
        }
        public void ListenKeyEvent(object canclationToken)
        {

            

        }

       
        public void Dispose()
        {
            tokenSource.Cancel();
        }
    }
}
