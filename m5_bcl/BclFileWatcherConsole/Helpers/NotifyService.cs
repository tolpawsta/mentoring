using BclFileWatcherConsole.Configuration;
using BclFileWatcherConsole.Logger;
using Serilog;
using System;
using System.Globalization;
using System.IO;
using messages = BclFileWatcherConsole.Resources.Messages;

namespace BclFileWatcherConsole.Helpers
{
    public static class NotifyService
    {
        private static CultureHelper _culture =CultureHelper.Source;
        private static AppConfigurationSection _config;
        private static ILogger _logger;
        private static AppLoggerManager _manager;
        static NotifyService()
        {
            _config = AppConfigManager.GetConfiguration("appSection");
            _culture.SetCurrentCulture(_config);
            _manager = AppLoggerManager.Source;
            _logger = _manager.Logger;
        }
        public static void OnFileDeleted(FileSystemEventArgs file)
        {
            _logger.Information($"{Path.GetDirectoryName(file.FullPath)}: {messages.File}: {file.Name} {messages.deleted}");
        }
        public static void OnFileCreated(FileSystemEventArgs file)
        {
            _logger.Information($"{Path.GetDirectoryName(file.FullPath)}: {messages.File}: {file.Name} {messages.created}");
        }
        public static void OnFileChanged(FileSystemEventArgs file)
        {
            _logger.Information($"{Path.GetDirectoryName(file.FullPath)}: {messages.File}: {file.Name} {messages.changed}");
        }
        public static void OnFileRenamed(RenamedEventArgs file)
        {           
                _logger.Information($"{Path.GetDirectoryName(file?.FullPath)}: {messages.File}: {file?.OldName} { messages.renamed} {messages.to} {file.Name}");
        }
        public static void OnFileMoved(string oldfilePath,string newFilePath)
        {
            _logger.Information($"{messages.File}: {Path.GetFileName(oldfilePath)} {messages.moved_from} {Path.GetDirectoryName(oldfilePath)} {messages.to} {Path.GetDirectoryName(newFilePath)}");
        }

        internal static void OnFileFound(FileInfo file, string fileNamePattern)
        {
            _logger.Information($"{messages.Found} {messages.File.ToLower(new CultureInfo(_config.Culture.Name))}: {file?.Name} {messages.matching_pattern}: {fileNamePattern}");
        }

        public static void OnStartWatch(FileSystemInfo file)
        {
            _logger.Information($"{messages.The_watcher_started_watching_the_folder}: {file?.Name}");
        }
        public static void OnStopWatch(FileSystemInfo file)
        {
            _logger.Information($"{messages.The_watcher_stopped_watching_the_folder}: {file?.Name}");
        }

    }
}
