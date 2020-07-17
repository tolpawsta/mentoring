using BclFileWatcherConsole.Configuration;
using BclFileWatcherConsole.Configuration.Rules;
using BclFileWatcherConsole.Helpers;
using BclFileWatcherConsole.Interfaces;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace BclFileWatcherConsole.Impl.Listeners
{
    public class WatcherHandler : ISubscriber, IHandler
    {

        private CultureHelper _culture;
        private AppConfigurationSection _config;
        private int _counter;
        private object _lockObject = new object();

        public WatcherHandler(AppConfigurationSection config)
        {
            _culture = CultureHelper.Source;
            _config = config;
            _culture.SetCurrentCulture(_config);
            _counter = 0;
        }

        public void Subscribe(IMultiWatcher multiWatcher)
        {
            foreach (var watcher in multiWatcher?.Watchers)
            {
                watcher.FileSystemEvent += FileSystemEventHandle;
                watcher.RenamedEvent += Watcher_RenamedEvent;
            }
        }

        private void Watcher_RenamedEvent(object sender, RenamedEventArgs e)
        {
            FileSystemEventHandle(sender, e);
        }

        public void FileSystemEventHandle(object s, FileSystemEventArgs e)
        {
            if (!(e?.ChangeType == WatcherChangeTypes.Created || e?.ChangeType == WatcherChangeTypes.Renamed))
            {
                return;
            }

            foreach (RuleElement rule in _config.Rules)
            {
                string pattern = rule.FileNamePattern;
                if (Regex.IsMatch(e.Name, pattern))
                {
                    FileInfo file = new FileInfo(e.FullPath);
                    NotifyService.OnFileFound(file, rule.FileNamePattern);
                    Move(file, rule, _config);
                    Rename(ref file, rule);

                }
            }
        }

        private void Move(FileInfo file, RuleElement rule, AppConfigurationSection config)
        {
            string destFolder = rule.DirectoryMoveTo;
            string sourceFileName = file.FullName;
            if (String.IsNullOrEmpty(destFolder))
            {
                string defaultDir = config.DefaultDirectory.Path;
                destFolder = !Directory.Exists(defaultDir) ? Directory.CreateDirectory(defaultDir).FullName : defaultDir;
            }
            else
            {
                destFolder = !Directory.Exists(destFolder) ? Directory.CreateDirectory(destFolder).FullName : destFolder;
            }
            string destFileName = Path.Combine(destFolder, file.Name);
            if (File.Exists(file.FullName))
            {
                if (File.Exists(file.FullName))
                {
                    file.MoveTo(destFileName, true);
                }
            }
            NotifyService.OnFileMoved(sourceFileName, file.FullName);
        }

        private void Rename(ref FileInfo file, RuleElement rule)
        {
            string oldFileName = $"{Path.GetFileNameWithoutExtension(file.Name)}";
            string destFileName = file.FullName;
            string sourceFileName =new string(file.FullName);
            if (rule.ShoudAddMoveDate)
            {
                string date = DateTime.Now.ToShortDateString();
                date = date.Replace('/', '-');
                string newFileName = $"{oldFileName}_{date}{file.Extension}";
                destFileName = Path.Combine(Path.GetDirectoryName(file.FullName), newFileName);
                while (File.Exists(destFileName))
                {
                    oldFileName = $"{Path.GetFileNameWithoutExtension(destFileName)}";
                    newFileName = $"{oldFileName}_{date}{file.Extension}";
                    destFileName = Path.Combine(Path.GetDirectoryName(file.FullName), newFileName);
                }
                oldFileName = $"{Path.GetFileNameWithoutExtension(destFileName)}";
            }
            if (rule.ShoudAddCounter)
            {
                string newFileName = $"{oldFileName}_{++_counter}{file.Extension}";
                destFileName = Path.Combine(Path.GetDirectoryName(file.FullName), newFileName);
                while (File.Exists(destFileName))
                {
                    oldFileName = $"{Path.GetFileNameWithoutExtension(destFileName)}";
                    newFileName = $"{oldFileName}_{++_counter}{file.Extension}";
                    destFileName = Path.Combine(Path.GetDirectoryName(file.FullName), newFileName);
                }                
            }            
            if (destFileName != sourceFileName)
            {
                if (File.Exists(sourceFileName))
                {
                    File.Move(sourceFileName, destFileName, true);
                }
            }            
            NotifyService.OnFileRenamed(new RenamedEventArgs(WatcherChangeTypes.Renamed, Path.GetDirectoryName(destFileName), Path.GetFileName(destFileName), Path.GetFileName(sourceFileName)));
        }
    }
}
