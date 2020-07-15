using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BclFileWatcherConsole.Interfaces
{
    public interface IWatcherListener
    {
        void FileSystemEventHandle(object s, FileSystemEventArgs e);
        void ErrorEventHandler(object s, ErrorEventArgs e);
    }
}
