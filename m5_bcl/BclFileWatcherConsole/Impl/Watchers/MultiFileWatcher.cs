using BclFileWatcherConsole.Interfaces;
using System.Collections.Generic;

namespace BclFileWatcherConsole.Impl.Watchers
{
    public class MultiFileWatcher : IMultiWatcher
    {
        private List<IWatcher> _watchers;
        private IWatcherManager _manager;
        
        public MultiFileWatcher()
        {
            _watchers =new List<IWatcher>();
        }

        public void StartWatch(IEnumerable<string> directoiesPath)
        {
            foreach (var dir in directoiesPath)
            {
                IWatcher watcher = _manager.CreateWatcher(dir);
                watcher.StartWatch();
            }
        }

        public void StopWatch()
        {
            foreach (var watcher in _watchers)
            {
                watcher.StopWatch();                
            }
        }
    }
}
