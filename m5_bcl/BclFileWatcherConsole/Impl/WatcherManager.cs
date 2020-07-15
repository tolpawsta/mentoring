using BclFileWatcherConsole.Impl.Watchers;
using BclFileWatcherConsole.Interfaces;

namespace BclFileWatcherConsole.Impl
{
    public class WatcherManager : IWatcherManager
    {
        public IWatcher CreateWatcher(string dir)
        {
            return new FileWatcher(dir);
        }
    }
}
