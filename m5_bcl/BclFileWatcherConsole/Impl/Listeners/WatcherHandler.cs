using BclFileWatcherConsole.Configuration;
using BclFileWatcherConsole.Configuration.Rules;
using BclFileWatcherConsole.Helpers;
using BclFileWatcherConsole.Interfaces;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace BclFileWatcherConsole.Impl.Listeners
{
    public class WatcherHandler : ISubscriber, IHandler
    {

        private CultureHelper _culture;
        private AppConfigurationSection _config;
        private int _counter;
        
        public WatcherHandler(AppConfigurationSection config)
        {
            _culture = CultureHelper.Source;
            _config = config;
            _culture.SetCurrentCulture(_config);
            _counter = 0;
        }

        public void Subscribe(IWatcher watcher)
        {
            watcher.FileSystemEvent += FileSystemEventHandle;
            watcher.RenamedEvent += Watcher_RenamedEvent;
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
            FileInfo file = new FileInfo(e.FullPath);
            bool isFileMatch = false;
            foreach (RuleElement rule in _config.Rules)
            {
                string pattern = rule.FileNamePattern; 
                if (Regex.IsMatch(e.Name, pattern))
                {
                    isFileMatch = true;
                    NotifyService.OnFileFound(file, rule.FileNamePattern);
                    Rename(ref file, rule);
                    Move(file, rule.DirectoryMoveTo);
                }
                
            }
            if(!isFileMatch)
            {
                Move(file, _config.DefaultDirectory.Path);
            }
        }

        private void Move(FileInfo file, string destFolder)
        {

            string sourceFileName = file.FullName;
            try
            {
                if (String.IsNullOrEmpty(destFolder))
                {
                    throw new ArgumentNullException();
                }
                destFolder = !Directory.Exists(destFolder) ? Directory.CreateDirectory(destFolder).FullName : destFolder;
                string destFileName = Path.Combine(destFolder, file.Name);
                if (File.Exists(file.FullName))
                {
                    try
                    {
                        if (File.Exists(file.FullName))
                        {
                            file.MoveTo(destFileName, true);
                            NotifyService.OnFileMoved(sourceFileName, file.FullName);
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        NotifyService.OnError(ex.Message);
                    }

                }
            }
            catch (ArgumentNullException ex)
            {
                NotifyService.OnError(ex.Message);
            }
            catch(IOException ex)
            {
                NotifyService.OnError(ex.Message);
            }

        }

        private void Rename(ref FileInfo file, RuleElement rule)
        {
            string oldFileName = $"{Path.GetFileNameWithoutExtension(file.Name)}";
            string destFileName = file.FullName;
            string sourceFileName = new string(file.FullName);
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
                try
                {
                    if (File.Exists(sourceFileName))
                    {
                        File.Move(sourceFileName, destFileName, true);
                        file = new FileInfo(destFileName);
                        NotifyService.OnFileRenamed(new RenamedEventArgs(WatcherChangeTypes.Renamed, Path.GetDirectoryName(destFileName), Path.GetFileName(destFileName), Path.GetFileName(sourceFileName)));
                    }
                }
                catch (FileNotFoundException ex)
                {
                    NotifyService.OnError(ex.Message);
                }
            }
        }
    }
}
