using System;

namespace Reflection.DIContainer.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false)]
    public class ImportAttribute:Attribute
    {
    }
}