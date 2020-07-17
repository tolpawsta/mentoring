using System;
using System.Collections.Generic;
using System.Text;

namespace BclFileWatcherConsole.Interfaces
{
    public interface ISubscriber
    {
        void Subscribe(IMultiWatcher multiWatcher);
        void Subscribe(IWatcher watcher);
    }
}
