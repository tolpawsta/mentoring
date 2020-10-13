using System;
using System.Collections.Generic;
using System.Text;

namespace MongoBDCore.Interfaces.Configuration
{
    public interface IAppDbConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
