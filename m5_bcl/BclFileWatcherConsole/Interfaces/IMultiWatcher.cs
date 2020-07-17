using System;
using System.Collections.Generic;
using System.Text;

namespace BclFileWatcherConsole.Interfaces
{
    public interface IMultiWatcher
    {
        public List<IWatcher> Watchers { get;}
        public void StartWatch();
        public void StopWatch();
    }
}
