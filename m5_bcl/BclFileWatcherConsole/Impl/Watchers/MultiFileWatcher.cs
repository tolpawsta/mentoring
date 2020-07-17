using BclFileWatcherConsole.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BclFileWatcherConsole.Impl.Watchers
{
    public class MultiFileWatcher : IMultiWatcher
    {
        private List<IWatcher> _watchers;
        private List<string> _directoiesPath = null;        
        public List<IWatcher> Watchers => _watchers;
        private IWatcherManager _manager;

        public MultiFileWatcher(IWatcherManager manager, IEnumerable<string> directoiesPath)
        {
            _manager = manager;
            _directoiesPath = InitializeWatchDirs(directoiesPath);
            _watchers = InitializeWatchers();
        }

        public void StartWatch()
        {
            foreach (var watcher in _watchers)
            {
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
        private List<string> InitializeWatchDirs(IEnumerable<string> directoiesPath)
        {
            return directoiesPath.Distinct().ToList();
        }
        private List<IWatcher> InitializeWatchers()
        {
            List<IWatcher> tempWatchers = new List<IWatcher>();
            foreach (var dir in _directoiesPath)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                IWatcher watcher = _manager.CreateWatcher(dir);
                tempWatchers.Add(watcher);
            }
            return tempWatchers;
        }
    }
}
