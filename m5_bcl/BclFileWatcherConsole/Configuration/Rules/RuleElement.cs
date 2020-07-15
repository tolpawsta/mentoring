using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BclFileWatcherConsole.Configuration.Rules
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("fileNamePattern", IsKey = true,DefaultValue ="")]
        public string FileNamePattern => (string) base["fileNamePattern"];
        
        [ConfigurationProperty("pathDirMoveTo",DefaultValue ="dirMoveTo")]
        public string DirectoryMoveTo => (string) base["pathDirMoveTo"];

        [ConfigurationProperty("shoudAddCounter",DefaultValue =false)]
        public bool ShoudAddCounter => (bool) base["shoudAddCounter"];
        
        [ConfigurationProperty("shoudAddMoveDate",DefaultValue =false)]
        public bool ShoudAddMoveDate => (bool) base["shoudAddMoveDate"];
    }
}
