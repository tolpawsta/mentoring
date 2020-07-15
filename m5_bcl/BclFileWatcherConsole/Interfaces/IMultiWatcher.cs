using System;
using System.Collections.Generic;
using System.Text;

namespace BclFileWatcherConsole.Interfaces
{
    public interface IMultiWatcher
    {
        public void StartWatch(IEnumerable<string> directoiesPath);
        public void StopWatch();
    }
}
