using BclFileWatcherConsole.Interfaces;
using System;
using System.IO;

namespace BclFileWatcherConsole.Impl.Listeners
{
    public class WatcherHandler : IWatcherListener
    {

        public void ErrorEventHandler(object s, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void FileSystemEventHandle(object s, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
