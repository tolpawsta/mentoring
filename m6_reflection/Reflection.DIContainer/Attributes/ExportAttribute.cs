using System;

namespace Reflection.DIContainer.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface,AllowMultiple = false)]
    public class ExportAttribute:Attribute
    {
        public Type InheretedType { get; }

        public ExportAttribute()
        {
        }

        public ExportAttribute(Type inheretedType)
        {
            InheretedType = inheretedType;
        }
    }
}