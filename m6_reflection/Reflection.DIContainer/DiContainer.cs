using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Reflection.DIContainer.Attributes;

namespace Reflection.DIContainer
{
    public class DiContainer
    {
        private List<Assembly> _assemblies;
        private Dictionary<Type, Type> _types;

        public DiContainer()
        {
            _assemblies = new List<Assembly>();
            _types = new Dictionary<Type, Type>();
        }

        public void AddAssembly(Assembly assembly)
        {
            if (assembly != null)
            {
                _assemblies.Add(assembly);
            }
            //TODO: get all types from addition assembly
        }

        public void AddType(Type typeImpl, Type typeAbstract = null)
        {
            var hasTypeCustomAttributes = typeImpl.CustomAttributes.Any(a => a.AttributeType == typeof(ExportAttribute)
                                                                                     || a.AttributeType == typeof(ImportConstructorAttribute));
            var hasTypePropsWithCustomAttr = typeImpl.GetProperties()
                .Any(p => p.CustomAttributes
                    .Any(a => a.AttributeType == typeof(ImportAttribute)));
            
            if (!hasTypePropsWithCustomAttr || !hasTypeCustomAttributes)
            {
                throw new TypeLoadException("Additional type doesn't contains any of needed attribute");
            }
            if (typeAbstract == null)
            {
                typeAbstract = typeImpl;
            }
            _types.Add(typeAbstract, typeImpl);
        }

        public object CreateInstance(Type type)
        {
            if (!_types.ContainsKey(type))
            {
                AddType(type);
            }
            return Resolve(_types[type]);
        }

        public T CreateInstance<T>()
        {
            return (T) CreateInstance(typeof(T));
        }

        private object Resolve(Type type)
        {
            var attributes = type.Attributes;
        }
        private
    }
}
