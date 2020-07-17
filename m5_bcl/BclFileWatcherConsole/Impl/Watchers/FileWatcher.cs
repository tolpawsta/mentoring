using BclFileWatcherConsole.Interfaces;
using System;
using System.IO;

namespace BclFileWatcherConsole.Impl.Watchers
{
    public class FileWatcher : IWatcher, IDisposable
    {
        public event FileSystemEventHandler FileSystemEvent;
        public event ErrorEventHandler ErrorEvent;
        public event EventHandler<FileSystemInfo> StartWatchEvent;
        public event EventHandler<FileSystemInfo> StopWatchEvent;
        public event EventHandler<RenamedEventArgs> RenamedEvent;

        private FileSystemWatcher _watcher;
        private string _directoryPath;
        public FileWatcher(string directoryPath)
        {
            _directoryPath = directoryPath;
            _watcher = new FileSystemWatcher(_directoryPath);
        }

        public void StartWatch()
        {
            SubscribeOnEvents();
            _watcher.NotifyFilter = NotifyFilters.FileName                                 
                                 | NotifyFilters.DirectoryName;
            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
            StartWatchEvent?.Invoke(this, new DirectoryInfo(_directoryPath));
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
        public void StopWatch()
        {
            StopWatchEvent?.Invoke(this, new DirectoryInfo(_directoryPath));
            UnSubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _watcher.Created += (s, e) => FileSystemEvent?.Invoke(s, e);
            _watcher.Deleted += (s, e) => FileSystemEvent?.Invoke(s, e);
            _watcher.Changed += (s, e) => FileSystemEvent?.Invoke(s, e);
            _watcher.Renamed += (s, e) => RenamedEvent?.Invoke(s, e);
            _watcher.Error += (s, e) => ErrorEvent?.Invoke(s, e);
        }
        private void UnSubscribeOnEvents()
        {
            _watcher.Created -= (s, e) => FileSystemEvent?.Invoke(s, e);
            _watcher.Deleted -= (s, e) => FileSystemEvent?.Invoke(s, e);
            _watcher.Changed -= (s, e) => FileSystemEvent?.Invoke(s, e);
            _watcher.Renamed -= (s, e) => RenamedEvent?.Invoke(s, e);
            _watcher.Error -= (s, e) => ErrorEvent?.Invoke(s, e);
        }
    }
}
