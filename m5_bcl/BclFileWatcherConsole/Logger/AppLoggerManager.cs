using Serilog;

namespace BclFileWatcherConsole.Logger
{
    public class AppLoggerManager
    {
        private static ILogger _logger;
        private static AppLoggerManager _appLogger;
        private AppLoggerManager()
        {
            _logger = new LoggerConfiguration()
                                    .ReadFrom.AppSettings()
                                    .CreateLogger();
        }
        public static AppLoggerManager Source
        {
            get
            {
                if (_logger == null)
                {
                    _appLogger = new AppLoggerManager();
                }
                return _appLogger;
            }
        }
        public ILogger Logger => _logger;
    }
}
