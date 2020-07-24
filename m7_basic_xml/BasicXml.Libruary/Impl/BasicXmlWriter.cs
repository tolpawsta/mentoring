using BasicXml.Libruary.Interfaces;
using BasicXml.Libruary.Interfaces.Publications;
using System;
using System.Collections.Generic;
using System.IO;

namespace BasicXml.Libruary.Impl
{
    public class BasicXmlWriter:IXmlWriter
    {
        public void Write(IEnumerable<Publication> publications, string targetDocPath)
        {
            throw new NotImplementedException();
        }

        public void Write(IEnumerable<Publication> publications, Stream targetDocPath)
        {
            throw new NotImplementedException();
        }
    }
}
