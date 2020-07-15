using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BclFileWatcherConsole.Configuration.Rules
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("fileNamePattern", IsKey = true)]
        public string FileNamePattern => (string) base["fileNamePattern"];
        
        [ConfigurationProperty("pathDirMoveTo")]
        public string DirectoryMoveTo => (string) base["pathDirMoveTo"];

        [ConfigurationProperty("shoudAddCounter")]
        public bool ShoudAddCounter => (bool) base["shoudAddCounter"];
        
        [ConfigurationProperty("shoudAddMoveDate")]
        public bool ShoudAddMoveDate => (bool) base["shoudAddMoveDate"];
    }
}
