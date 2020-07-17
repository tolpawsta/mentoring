using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BclFileWatcherConsole.Interfaces
{
    public interface IWatcherLogger:IHandler,ISubscriber
    {        
        void RenamedEventHandler(object s, RenamedEventArgs e);
        void ErrorEventHandler(object s, ErrorEventArgs e);
        
    }
}
