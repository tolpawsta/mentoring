using System;
using System.Collections.Generic;
using System.Text;

namespace BclFileWatcherConsole.Interfaces
{
    public interface IWatcherManager
    {
        IWatcher CreateWatcher(string dir);
    }
}
