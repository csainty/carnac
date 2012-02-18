using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Carnac.Logic;

namespace Carnac.Logger
{
    public abstract class AbstractLogger : IObserver<KeyPress>, IDisposable
    {
        private Stopwatch _sw = new Stopwatch();
        
        public virtual void OnCompleted() { }
        public virtual void OnError(Exception error) { }
        public abstract void Log(KeyPress value, TimeSpan interval);

        public void OnNext(KeyPress value)
        {
            TimeSpan interval;
            if (_sw.IsRunning)
            {
                interval = _sw.Elapsed;
                _sw.Restart();
            }
            else {
                interval = TimeSpan.Zero;
                _sw.Start();
            }
            Log(value, interval);
        }

        public virtual void Dispose() {
            _sw.Stop();
        }
    }
}
