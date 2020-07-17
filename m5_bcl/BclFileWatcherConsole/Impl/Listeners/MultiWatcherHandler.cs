using BclFileWatcherConsole.Configuration;
using BclFileWatcherConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BclFileWatcherConsole.Impl.Listeners
{
    public class MultiWatcherHandler:ISubscriber
    {
        private List<WatcherHandler> _handlers;
        private AppConfigurationSection _config;

        public MultiWatcherHandler(AppConfigurationSection config)
        {
            _handlers = new List<WatcherHandler>();
            _config = config;
        }
        public void Subscribe(IMultiWatcher multiWatcher)
        {
            foreach (var watcher in multiWatcher?.Watchers)
            {
                var handler =new WatcherHandler(_config);
                handler.Subscribe(watcher);
                _handlers?.Add(handler);
            }
        }

        public void Subscribe(IWatcher watcher)
        {
            var handler = new WatcherHandler(_config);
            handler.Subscribe(watcher);
            _handlers?.Add(handler);
        }
    }
}
