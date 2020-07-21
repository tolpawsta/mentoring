using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Reflection.DIContainer.Attributes;
using Reflection.DIContainer.Exceptions;
using Reflection.DIContainer.Helper;

namespace Reflection.DIContainer
{
    public class DiContainer
    {
        private Assembly _assembly;
        private Dictionary<Type, Type> _types;

        public DiContainer()
        {
            _types = new Dictionary<Type, Type>();
        }

        public IEnumerable<Assembly> Assemblies => DiContainerHelper.GetAssemblies(_assembly);

        public void AddAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            _assembly = assembly;
            var types = DiContainerHelper.GetAssemblies(assembly).SelectMany(am => DiContainerHelper.GetTypes(am)
                    .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(ImportConstructorAttribute) || a.AttributeType == typeof(ExportAttribute))
                                || t.GetProperties().Any(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(ImportAttribute)))))
                .ToList();
            types.ForEach(t => AddType(t));
        }
        public void AddType(Type typeImpl, Type typeAbstract = null)
        {
            if (typeImpl.IsInterface)
            {
                throw new DiException($"Cannot add interface or abstract class type {nameof(typeImpl)} e.g. cannot create instance!!!");
            }
            if (typeImpl.CustomAttributes.Any(a => a.AttributeType == typeof(ImportAttribute)) && typeImpl.GetConstructors().Any(c => c.GetParameters().Length > 0))
            {
                throw new DiException($"Error. Additional type has {nameof(ImportAttribute)} but contain constructor with parameters", typeImpl);
            }
            if (!DiContainerHelper.HasTypeCustomAttributes(typeImpl))
            {
                Console.WriteLine($"Additional type {typeImpl.Name} doesn't contains any of needed attributes");
            }
            if (typeAbstract!=null && typeImpl.GetInterface(typeAbstract.FullName) == null)
            {
                Console.WriteLine($"Additional type {typeImpl.Name} don't implement {typeAbstract.Name} interface");
            }
            if (typeAbstract == null)
            {
                typeAbstract = typeImpl;
            }
            if (!_types.TryAdd(typeAbstract, typeImpl))
            {
                Console.WriteLine($"Additional types {typeAbstract.Name} - {typeImpl.Name} are already exists");
            }
        }
        public object CreateInstance(Type type)
        {
            if (!_types.ContainsKey(type))
            {
                throw new DiException($"Creating type {nameof(type)} not found. Please add it");
            }
            return Resolve(_types[type]);
        }
        public T CreateInstance<T>()
        {
            return (T) CreateInstance(typeof(T));
        }
        private object Resolve(Type type)
        {
            object currentType=null;
            if (type.CustomAttributes.Any(a => a.AttributeType == typeof(ImportConstructorAttribute)))
            {
                var typeCtors = type.GetConstructors();
                var typeCtorToDi = typeCtors.FirstOrDefault(c => c.GetParameters().Length > 0);
                if (typeCtorToDi != null)
                {
                    var parameters = typeCtorToDi.GetParameters();
                    var dependencies = GetDependencies(parameters).ToArray();
                    currentType = Activator.CreateInstance(type, dependencies);
                }
            }
            else
            {
                var properties = type.GetProperties()
                    .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(ImportAttribute)));
                currentType = Activator.CreateInstance(type);
                foreach (var property in properties)
                {
                    var propertyType = property.PropertyType;
                    var hasPropertyTypeCustomAttr = DiContainerHelper.HasTypeCustomAttributes(propertyType);
                    property.SetValue(currentType,  CreateInstance(propertyType));
                }
            }
            return currentType;
        }

        private IEnumerable<object> GetDependencies(IEnumerable<ParameterInfo> parameters)
        {
            foreach (var parameter in parameters)
            {
                var parametrType = parameter.ParameterType;
                var typeAbstract = DiContainerHelper.GetTypeAbstract(parametrType);
                yield return CreateInstance(typeAbstract ?? parametrType);
            }
        }

        public List<Type> GetTypes(Assembly assembly)
        {
            return DiContainerHelper.GetTypes(assembly).ToList();
        }
    }
}
