using BclFileWatcherConsole.Configuration.WatchDirectories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BclFileWatcherConsole.Helpers
{
    public static class WatcherHelper
    {
        public static List<string> GetDirectoriesPath(WatchDirectoryCollection watchDirectories)
        {
            return watchDirectories.Cast<WatchDirectoryElement>().Select(w => w.Path).ToList();
        }
    }
}
