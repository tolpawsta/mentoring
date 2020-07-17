using BclFileWatcherConsole.Helpers;
using BclFileWatcherConsole.Interfaces;
using System;
using System.IO;
using messages = BclFileWatcherConsole.Resources.Messages;
namespace BclFileWatcherConsole.Impl.Listeners
{
    public class WatcherListener : ISubscriber, IWatcherLogger
    {
        public void Subscribe(IMultiWatcher multiWatcher)
        {
            foreach (var watcher in multiWatcher?.Watchers)
            {
                watcher.FileSystemEvent += FileSystemEventHandle;
                watcher.RenamedEvent += RenamedEventHandler;
                watcher.ErrorEvent += ErrorEventHandler;
                watcher.StartWatchEvent += Watcher_StartWatchEvent;
                watcher.StopWatchEvent += Watcher_StopWatchEvent;
            }
        }
        public void ErrorEventHandler(object s, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Watcher_StopWatchEvent(object sender, FileSystemInfo e)
        {
            NotifyService.OnStopWatch(e);
        }
        private void Watcher_StartWatchEvent(object sender, FileSystemInfo e)
        {
            NotifyService.OnStartWatch(e);
        }

        public void RenamedEventHandler(object s, RenamedEventArgs e)
        {
            NotifyService.OnFileRenamed(e);
        }

        public void FileSystemEventHandle(object s, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                NotifyService.OnFileCreated(e);
            }
            else if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                NotifyService.OnFileDeleted(e);
            }
            else if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                NotifyService.OnFileChanged(e);
            }
        }
    }
}
