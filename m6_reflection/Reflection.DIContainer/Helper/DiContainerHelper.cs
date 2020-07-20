using System;
using System.Linq;
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
    }
}