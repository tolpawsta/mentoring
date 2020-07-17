using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BclFileWatcherConsole.Interfaces
{
    public interface IHandler
    {
        void FileSystemEventHandle(object s, FileSystemEventArgs e);
    }
}
