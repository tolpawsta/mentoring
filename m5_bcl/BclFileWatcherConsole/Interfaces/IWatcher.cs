using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BclFileWatcherConsole.Interfaces
{
    public interface IWatcher
    {
        event FileSystemEventHandler FileSystemEvent;
        event EventHandler<RenamedEventArgs> RenamedEvent;
        event ErrorEventHandler ErrorEvent;
        event EventHandler<FileSystemInfo> StartWatchEvent;
        event EventHandler<FileSystemInfo> StopWatchEvent;
        void StartWatch();
        void StopWatch();
    }
}
