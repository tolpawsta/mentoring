using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Reflection.DIContainer.Attributes;
using Reflection.DIContainer.Helper;

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
            if (!_types.ContainsValue(typeImpl))
            {

                if (!DiContainerHelper.HasTypeCustomAttributes(typeImpl))
                {
                    throw new TypeLoadException("Additional type doesn't contains any of needed attribute");
                }
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
            if (type.IsAbstract || type.IsInterface)
            {
                throw new TypeLoadException("type is abstract or interface");
            }

            if (type.CustomAttributes.Any(a => a.AttributeType == typeof(ImportConstructorAttribute)))
            {
                var typeCtors = type.GetConstructors(BindingFlags.CreateInstance | BindingFlags.Public);
                var typeCtorToDi = typeCtors.FirstOrDefault(c => c.GetParameters().Length > 0);
                if (typeCtorToDi != null)
                {
                    var dependencies = typeCtorToDi.GetParameters().Select(p => p.HasDefaultValue ? p.DefaultValue : CreateInstance(p.GetType())).ToArray();
                    typeCtorToDi.Invoke(dependencies);
                }
            }
            else
            {
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.SetProperty)
                    .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(ImportAttribute)));
                foreach (var property in properties)
                {
                    property.SetValue(type,CreateInstance(property.GetType()));
                }
            }

            return Activator.CreateInstance(type);
        }

        private object[] GetDependencies(IEnumerable<Type> types)
        {
            return types.Select(t => CreateInstance(t)).ToArray();
        }
    }
}
