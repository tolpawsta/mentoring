using BclFileWatcherConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BclFileWatcherConsole.Impl.Listeners
{
    public class WatcherLogger : IWatcherListener
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
