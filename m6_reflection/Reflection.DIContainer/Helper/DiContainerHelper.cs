using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Reflection.DIContainer.Attributes;

namespace Reflection.DIContainer.Helper
{
    public static class DiContainerHelper
    {
        public static bool HasTypeCustomAttributes(Type type)
        {
            var hasTypeCustomAttributes = type.CustomAttributes.Any(a => a.AttributeType == typeof(ExportAttribute)
                                                                             || a.AttributeType == typeof(ImportConstructorAttribute));
            var hasTypePropsWithCustomAttr = type.GetProperties()
                .Any(p => p.CustomAttributes
                    .Any(a => a.AttributeType == typeof(ImportAttribute)));
            return hasTypePropsWithCustomAttr || hasTypeCustomAttributes;
        }

        public static IEnumerable<Type> GetTypes(Assembly assembly)
        {
            
            return assembly.GetTypes();
        }

        public static IEnumerable<Assembly> GetAssemblies(Assembly assembly)
        {
            var assembliesPath = Directory.GetFiles(Path.GetDirectoryName(assembly?.Location), "*.dll");
            return assembliesPath.Select(a => Assembly.LoadFrom(a));
           
        }

        public static Type GetTypeAbstract(Type typeImpl)
        {
            return typeImpl.GetCustomAttribute<ExportAttribute>()?.InheretedType;
            
        }
    }
}