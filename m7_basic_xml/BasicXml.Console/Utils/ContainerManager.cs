using Autofac;
using Autofac.Core;
using BasicXml.Library.Impl;
using BasicXml.Library.Interfaces;

namespace BasicXml.Console.Utils
{
    public static class ContainerManager
    {
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BasicXmlReader>().As<IXmlReader>();
            builder.RegisterType<BasicXmlParser>().As<IXmlParser>();
            builder.RegisterType<BasicXmlWriter>().As<IXmlWriter>();
            builder.RegisterType<XmlLibrary>().As<XmlLibrary>();
            builder.RegisterType<BasicViewer>().As<BasicViewer>();
            return builder.Build();
        }
    }
}