using BclFileWatcherConsole.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace BclFileWatcherConsole.Helpers
{
    public class CultureHelper
    {
        private AppConfigurationSection _config;
        private static CultureHelper _source=null;
        private CultureHelper(){  }

        public static CultureHelper Source
        {
            get
            {
                if (_source == null)
                {
                    _source = new CultureHelper();
                }
                return _source;
            }
        }
        public void SetCurrentCulture(AppConfigurationSection config = null)
        {
            _config = config;
            if (_config==null)
            {
                _config = AppConfigManager.GetConfiguration("appSection");
            }
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_config.Culture.Name);
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(_config.Culture.Name);
        }
    }
}
