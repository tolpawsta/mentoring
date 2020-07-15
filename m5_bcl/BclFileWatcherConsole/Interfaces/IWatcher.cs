using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BclFileWatcherConsole.Interfaces
{
    public interface IWatcher
    {
        event FileSystemEventHandler FileSystemEvent;
        event ErrorEventHandler ErrorEvent;
        event EventHandler StartWatchEvent;
        event EventHandler StopWatchEvent;
        void StartWatch();
        void StopWatch();
    }
}
